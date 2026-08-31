using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Analytics.CMT.CompletedWorkAnalysis
{
    public class CMTKnittingByMachine
    {
        public string KnittingMachine { get; set; }

        public int TotalUnits { get; set; }

        public int Twisting { get; set; }

        public int Holes { get; set; }

        public int OilMarks { get; set; }

        public int NeedleLines { get; set; }

        public int TotalKnittingDefects
        {
            get
            {
                return
                    Twisting +
                    Holes +
                    OilMarks +
                    NeedleLines;
            }
        }

        public decimal KnittingBGradePercentage
        {
            get
            {
                if (TotalUnits == 0)
                    return 0;

                return
                    (decimal)TotalKnittingDefects /
                    TotalUnits * 100M;
            }
        }
    }
}