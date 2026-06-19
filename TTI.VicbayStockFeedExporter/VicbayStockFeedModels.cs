using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace TTI.VicbayStockFeedExporter
{
    public class VicbayStockFeed
    {
        [JsonProperty("source")]
        public string Source { get; set; }

        [JsonProperty("feedType")]
        public string FeedType { get; set; }

        [JsonProperty("generatedAt")]
        public DateTimeOffset GeneratedAt { get; set; }

        [JsonProperty("cadence")]
        public string Cadence { get; set; }

        [JsonProperty("warehouse")]
        public VicbayWarehouse Warehouse { get; set; }

        [JsonProperty("items")]
        public List<VicbayStockItem> Items { get; set; }
    }

    public class VicbayWarehouse
    {
        [JsonProperty("code")]
        public string Code { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }
    }

    public class VicbayStockItem
    {
        [JsonProperty("productCode")]
        public string ProductCode { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("colour")]
        public string Colour { get; set; }

        [JsonProperty("size")]
        public string Size { get; set; }

        [JsonProperty("availableQuantity")]
        public int AvailableQuantity { get; set; }

        [JsonProperty("lastUpdated")]
        public DateTimeOffset LastUpdated { get; set; }


    }

    internal class VicbayStockRow
    {
        public string ProductCode { get; set; }
        public string StyleDescription { get; set; }
        public string ColourDescription { get; set; }
        public string SizeDescription { get; set; }
        public int SizeDisplayOrder { get; set; }
        public int AvailableQuantity { get; set; }
    }

    public class VicbayExportResult
    {
        public string OutputPath { get; set; }
        public int ProductCount { get; set; }
        public int SizeRowCount { get; set; }
        public int TotalAvailableQuantity { get; set; }
        public DateTimeOffset GeneratedAt { get; set; }
    }
}
