using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using Newtonsoft.Json;

namespace TTI.VicbayStockFeedExporter
{
    public class VicbayStockFeedExporter
    {
        private readonly string _connectionString;
        private readonly int _centralWarehouseId;
        private readonly string _outputPath;
        private readonly bool _includeZeroStock;
        private readonly bool _excludeSoldStock;
        private readonly string _warehouseCode;
        private readonly string _warehouseDescription;

        private readonly GitRepositoryPublisher _gitPublisher;

        public VicbayStockFeedExporter()
        {
            var connection = ConfigurationManager.ConnectionStrings["TTISqlConnection"];
            if (connection == null || string.IsNullOrWhiteSpace(connection.ConnectionString))
            {
                throw new ConfigurationErrorsException("Missing connection string: TTISqlConnection");
            }

            _connectionString = connection.ConnectionString;
            _centralWarehouseId = ReadRequiredIntSetting("CentralWarehouseId");
            _outputPath = ReadRequiredStringSetting("OutputPath");
            _includeZeroStock = ReadBoolSetting("IncludeZeroStock", true);
            _excludeSoldStock = ReadBoolSetting("ExcludeSoldStock", true);
            _warehouseCode = ReadStringSetting("WarehouseCode", "CENTRAL");
            _warehouseDescription = ReadStringSetting("WarehouseDescription", "Central Warehouse");

            bool publishToGitHub = ReadBoolSetting("PublishToGitHub", false);

            if (publishToGitHub)
            {
                _gitPublisher = new GitRepositoryPublisher(
                    ReadRequiredStringSetting("GitExecutablePath"),
                    ReadRequiredStringSetting("GitRepositoryPath"),
                    ReadStringSetting("GitBranch", "main"));
            }
        }

        public VicbayExportResult Export()
        {
            DateTimeOffset generatedAt = DateTimeOffset.Now;
            List<VicbayStockRow> rows = LoadStockRows();

            _gitPublisher?.PrepareForExport();

            if (!_includeZeroStock)
            {
                rows = rows.Where(x => x.AvailableQuantity > 0).ToList();
            }

            var items = rows
                .Select(x => new VicbayStockItem
                {
                    ProductCode = x.ProductCode,
                    Description = x.StyleDescription,
                    Colour = x.ColourDescription,
                    Size = x.SizeDescription,
                    AvailableQuantity = x.AvailableQuantity,
                    LastUpdated = generatedAt
                })
                .OrderBy(x => x.ProductCode)
                .ToList();

            var feed = new VicbayStockFeed
            {
                Source = "TradeLink",
                FeedType = "central_warehouse_available_stock",
                GeneratedAt = generatedAt,
                Cadence = "daily", //"twice_daily"
                Warehouse = new VicbayWarehouse
                {
                    Code = _warehouseCode,
                    Description = _warehouseDescription
                },
                Items = items
            };

            WriteJsonFile(feed);

            _gitPublisher?.CommitAndPush(_outputPath, generatedAt);

            return new VicbayExportResult
            {
                OutputPath = _outputPath,
                ProductCount = items.Count,
                SizeRowCount = rows.Count,
                TotalAvailableQuantity = items.Sum(x => x.AvailableQuantity),
                GeneratedAt = generatedAt
            };
        }

        private List<VicbayStockRow> LoadStockRows()
        {
            var rows = new List<VicbayStockRow>();

            string soldFilter = _excludeSoldStock
                ? "AND soh.TLSOH_Sold = 0"
                : string.Empty;

            string query = @"
        ;WITH AvailableStock AS
        (
            SELECT
                soh.TLSOH_Style_FK,
                soh.TLSOH_Colour_FK,
                soh.TLSOH_Size_FK,
                SUM(ISNULL(soh.TLSOH_BoxedQty, 0)) AS AvailableQuantity
            FROM TLCSV_StockOnHand soh
            WHERE
                soh.TLSOH_WareHouse_FK = @CentralWarehouseId
                AND soh.TLSOH_Picked = 0
                AND soh.TLSOH_Is_A = 1
                AND soh.TLSOH_Write_Off = 0
                AND soh.TLSOH_Returned = 0
                AND soh.TLSOH_Split = 0
                " + soldFilter + @"
            GROUP BY
                soh.TLSOH_Style_FK,
                soh.TLSOH_Colour_FK,
                soh.TLSOH_Size_FK
        )
        SELECT
            pc.ProductCode,
            sty.Sty_Description AS StyleDescription,
            col.Col_Display AS ColourDescription,
            siz.SI_Description AS SizeDescription,
            ISNULL(siz.SI_DisplayOrder, 9999) AS SizeDisplayOrder,
            ISNULL(stock.AvailableQuantity, 0) AS AvailableQuantity
        FROM TLADM_ProductCodes pc
        INNER JOIN TLADM_Styles sty
            ON sty.Sty_Id = pc.StyleId
        INNER JOIN TLADM_Colours col
            ON col.Col_Id = pc.ColourId
        INNER JOIN TLADM_Sizes siz
            ON siz.SI_id = pc.SizeId
        LEFT JOIN AvailableStock stock
            ON stock.TLSOH_Style_FK = pc.StyleId
            AND stock.TLSOH_Colour_FK = pc.ColourId
            AND stock.TLSOH_Size_FK = pc.SizeId
        WHERE
            sty.Sty_Discontinued = 0
            AND col.Col_Discontinued = 0
            AND siz.SI_Discontinued = 0
            AND NULLIF(LTRIM(RTRIM(pc.ProductCode)), '') IS NOT NULL
        ORDER BY
            pc.ProductCode,
            siz.SI_DisplayOrder,
            siz.SI_Description;";

            using (var conn = new SqlConnection(_connectionString))
            using (var cmd = new SqlCommand(query, conn))
            {
                cmd.CommandType = CommandType.Text;

                // Keep this generous while testing, but the query itself should now be much quicker.
                cmd.CommandTimeout = 300;

                cmd.Parameters.Add("@CentralWarehouseId", SqlDbType.Int).Value =
                    _centralWarehouseId;

                conn.Open();

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        rows.Add(new VicbayStockRow
                        {
                            ProductCode = ReadString(reader, "ProductCode"),
                            StyleDescription = ReadString(reader, "StyleDescription"),
                            ColourDescription = ReadString(reader, "ColourDescription"),
                            SizeDescription = ReadString(reader, "SizeDescription"),
                            SizeDisplayOrder = ReadInt(reader, "SizeDisplayOrder"),
                            AvailableQuantity = ReadInt(reader, "AvailableQuantity")
                        });
                    }
                }
            }

            return rows;
        }
        private void WriteJsonFile(VicbayStockFeed feed)
        {
            string directory = Path.GetDirectoryName(_outputPath);
            if (!string.IsNullOrWhiteSpace(directory))
            {
                Directory.CreateDirectory(directory);
            }

            string json = JsonConvert.SerializeObject(feed, Formatting.Indented);

            // Safer write: write to temp file first, then replace the real file.
            // This prevents the website from reading a half-written JSON file.
            string tempPath = _outputPath + ".tmp";
            File.WriteAllText(tempPath, json);

            if (File.Exists(_outputPath))
            {
                File.Delete(_outputPath);
            }

            File.Move(tempPath, _outputPath);
        }

        private static string ReadString(SqlDataReader reader, string columnName)
        {
            object value = reader[columnName];
            return value == DBNull.Value ? string.Empty : value.ToString().Trim();
        }

        private static int ReadInt(SqlDataReader reader, string columnName)
        {
            object value = reader[columnName];
            return value == DBNull.Value ? 0 : Convert.ToInt32(value);
        }

        private static string ReadRequiredStringSetting(string key)
        {
            string value = ConfigurationManager.AppSettings[key];
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new ConfigurationErrorsException("Missing appSetting: " + key);
            }

            return value;
        }

        private static string ReadStringSetting(string key, string defaultValue)
        {
            string value = ConfigurationManager.AppSettings[key];
            return string.IsNullOrWhiteSpace(value) ? defaultValue : value;
        }

        private static int ReadRequiredIntSetting(string key)
        {
            string value = ReadRequiredStringSetting(key);
            int parsed;
            if (!int.TryParse(value, out parsed))
            {
                throw new ConfigurationErrorsException("appSetting must be an integer: " + key);
            }

            return parsed;
        }

        private static bool ReadBoolSetting(string key, bool defaultValue)
        {
            string value = ConfigurationManager.AppSettings[key];
            bool parsed;
            return bool.TryParse(value, out parsed) ? parsed : defaultValue;
        }
    }
}
