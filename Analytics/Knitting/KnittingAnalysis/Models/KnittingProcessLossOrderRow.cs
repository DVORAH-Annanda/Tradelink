using System;

namespace Analytics.Knitting.KnittingAnalysis.Models
{
    public class KnittingProcessLossOrderRow
    {
        public int PrimaryKey { get; set; }

        public string OrderNumber { get; set; }

        public DateTime? DateClosed { get; set; }

        public DateTime? ProductionDate { get; set; }

        public string Machine { get; set; }

        public string Product { get; set; }

        public decimal OrderQty { get; set; }

        public decimal TotalKnitted { get; set; }

        public decimal YarnConsumed { get; set; }

        public decimal ProcessLoss { get; set; }

        public string YarnType { get; set; }

        public decimal YarnTex { get; set; }

        public decimal YarnTwist { get; set; }
    }
}
