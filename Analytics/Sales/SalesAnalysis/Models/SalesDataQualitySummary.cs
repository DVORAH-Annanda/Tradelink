using System;

namespace Analytics.Sales.SalesAnalysis.Models
{
    public class SalesDataQualitySummary
    {
        public int SoldRows { get; set; }
        public int ZeroOrNegativeQtyRows { get; set; }
        public int MissingMasterDataRows { get; set; }
        public int MissingSoldDateRows { get; set; }
        public DateTime? LatestSoldDate { get; set; }
        public DateTime? LatestReturnDate { get; set; }
    }
}
