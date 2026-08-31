using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Analytics.CMT.CompletedWorkAnalysis.Models
{
    public class CMTDyeingByStyleQuality
    {
        public string Style { get; set; }

        public string GreigeQuality { get; set; }

        public int TotalUnits { get; set; }

        public int Stains { get; set; }

        public int Ospec { get; set; }

        public int Shading { get; set; }

        public int TotalDyeingDefects
        {
            get
            {
                return Stains + Ospec + Shading;
            }
        }

        public decimal DyeingBGradePercentage
        {
            get
            {
                if (TotalUnits == 0)
                    return 0;

                return
                    (decimal)TotalDyeingDefects /
                    TotalUnits * 100M;
            }
        }
    }
}
