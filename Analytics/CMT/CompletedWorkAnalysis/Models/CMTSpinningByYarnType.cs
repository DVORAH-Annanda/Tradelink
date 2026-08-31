using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Analytics.CMT.CompletedWorkAnalysis
{
    public class CMTSpinningByYarnType
    {
        public string YarnType { get; set; }

        public int TotalUnits { get; set; }

        public int BarreLines { get; set; }

        public int Fflaw { get; set; }

        public int Contam { get; set; }

        public int TotalSpinningDefects
        {
            get
            {
                return BarreLines + Fflaw + Contam;
            }
        }

        public decimal SpinningBGradePercentage
        {
            get
            {
                if (TotalUnits == 0)
                    return 0;

                return
                    (decimal)TotalSpinningDefects /
                    TotalUnits * 100M;
            }
        }
    }
}