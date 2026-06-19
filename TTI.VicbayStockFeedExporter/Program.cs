using System;

namespace TTI.VicbayStockFeedExporter
{
    internal static class Program
    {
        private static int Main(string[] args)
        {
            try
            {
                Console.WriteLine("Vicbay stock feed export started: " + DateTime.Now);

                var exporter = new VicbayStockFeedExporter();
                var result = exporter.Export();

                Console.WriteLine("Export completed successfully.");
                Console.WriteLine("Output file: " + result.OutputPath);
                Console.WriteLine("Products exported: " + result.ProductCount);
                Console.WriteLine("Size rows exported: " + result.SizeRowCount);
                Console.WriteLine("Total available quantity: " + result.TotalAvailableQuantity);
                Console.WriteLine("Generated at: " + result.GeneratedAt.ToString("yyyy-MM-dd HH:mm:ss"));

                return 0;
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine("Export failed.");
                Console.Error.WriteLine(ex.ToString());
                return 1;
            }
        }
    }
}
