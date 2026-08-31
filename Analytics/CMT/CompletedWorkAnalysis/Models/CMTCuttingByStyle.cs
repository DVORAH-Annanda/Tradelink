using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Analytics.CMT.CompletedWorkAnalysis.Models
{
    public class CMTCuttingByStyle
    {
        public string Style { get; set; }

        public int TotalUnits { get; set; }

        public int MNFF { get; set; }

        public int PoorCutting { get; set; }

        public int TotalCuttingDefects
        {
            get
            {
                return MNFF + PoorCutting;
            }
        }

        public decimal CuttingBGradePercentage
        {
            get
            {
                if (TotalUnits == 0)
                    return 0;

                return
                    (decimal)TotalCuttingDefects /
                    TotalUnits * 100M;
            }
        }
    }
}
