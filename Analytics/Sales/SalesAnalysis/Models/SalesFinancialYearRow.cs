namespace Analytics.Sales.SalesAnalysis.Models
{
    public class SalesFinancialYearRow
    {
        // Financial year is labelled by its end year: FY 2025/26 => 2026.
        public int EndYear { get; set; }
        public long GrossSales { get; set; }
        public long Returns { get; set; }
        public long NetSales { get { return GrossSales - Returns; } }
    }
}
