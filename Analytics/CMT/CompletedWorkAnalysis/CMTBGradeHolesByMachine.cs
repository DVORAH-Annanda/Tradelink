using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Analytics.CMT.CompletedWorkAnalysis
{
    public class CMTBGradeHolesByMachine
    {
        public string KnittingMachine { get; set; }

        public int TotalUnits { get; set; }

        public int Holes { get; set; }

        public decimal HolesPercentage
        {
            get
            {
                if (TotalUnits == 0)
                    return 0;

                return (decimal)Holes / TotalUnits * 100M;
            }
        }
    }
}
