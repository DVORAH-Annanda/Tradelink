namespace Analytics.Sales.SalesAnalysis.Models
{
    public class SalesMonthlyComparisonRow
    {
        public string Month { get; set; }
        public int CurrentGrossSales { get; set; }
        public int CurrentReturns { get; set; }
        public int CurrentNetSales { get { return CurrentGrossSales - CurrentReturns; } }
        public int PreviousGrossSales { get; set; }
        public int PreviousReturns { get; set; }
        public int PreviousNetSales { get { return PreviousGrossSales - PreviousReturns; } }
        public decimal NetGrowthPercentage { get { return PreviousNetSales == 0 ? 0m : ((decimal)CurrentNetSales - PreviousNetSales) / PreviousNetSales * 100m; } }
    }
}
