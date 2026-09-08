using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Analytics.Knitting.KnittingAnalysis.Models
{
    public class KnittingProcessLossMachineRow
    {
        public string Machine { get; set; }

        public string Product { get; set; }

        public decimal OrderQty { get; set; }

        public decimal TotalKnitted { get; set; }

        public decimal YarnConsumed { get; set; }

        public decimal ProcessLoss { get; set; }
    }
}