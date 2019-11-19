using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TTI2_WF.KPIs.Spinning
{
    public class YarnProduction
    {
        public YarnProduction()
        {
        }

        public DateTime fromDate { get; set; }
        public DateTime toDate { get; set; }
        public bool IncludeClosed { get; set; }
        public bool SpecificOrder { get; set; }
        public Int32 orderKey { get; set; }
        public bool QASummary { get; set; }

    }
}
