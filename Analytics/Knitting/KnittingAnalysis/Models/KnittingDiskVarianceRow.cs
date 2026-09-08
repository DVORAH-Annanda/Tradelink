using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Analytics.Knitting.KnittingAnalysis.Models
{
    public class KnittingDiskVarianceRow
    {
        public string GreigeQuality { get; set; }

        public int PieceCount { get; set; }

        public decimal AverageStandardDisk { get; set; }

        public decimal AverageActualDisk { get; set; }


        public decimal DiskVariancePercentage
        {
            get
            {
                if (AverageStandardDisk == 0)
                    return 0;

                return
                    ((AverageActualDisk - AverageStandardDisk)
                    / AverageStandardDisk) * 100;
            }
        }
    }
}
