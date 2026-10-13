namespace Analytics.Sales.SalesAnalysis.Models
{
    public class SalesSummary
    {
        public int CurrentGrossSales { get; set; }
        public int CurrentReturns { get; set; }
        public int CurrentNetSales { get { return CurrentGrossSales - CurrentReturns; } }
        public decimal CurrentReturnRate { get { return CurrentGrossSales == 0 ? 0m : (decimal)CurrentReturns / CurrentGrossSales * 100m; } }

        public int PreviousGrossSales { get; set; }
        public int PreviousReturns { get; set; }
        public int PreviousNetSales { get { return PreviousGrossSales - PreviousReturns; } }
        public decimal PreviousReturnRate { get { return PreviousGrossSales == 0 ? 0m : (decimal)PreviousReturns / PreviousGrossSales * 100m; } }
        public decimal NetGrowthPercentage { get { return PreviousNetSales == 0 ? 0m : ((decimal)CurrentNetSales - PreviousNetSales) / PreviousNetSales * 100m; } }
    }
}
