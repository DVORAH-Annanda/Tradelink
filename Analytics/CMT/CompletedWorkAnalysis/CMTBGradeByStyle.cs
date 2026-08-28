using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Analytics.CMT.CompletedWorkAnalysis
{
    public class CMTBGradeByStyle
    {
        public string Style { get; set; }

        public int TotalUnits { get; set; }

        public int AGrade { get; set; }

        public int BGrade { get; set; }

        public decimal BGradePercentage
        {
            get
            {
                int totalGrades = AGrade + BGrade;

                if (totalGrades == 0)
                    return 0;

                return (decimal)BGrade / totalGrades * 100M;
            }
        }
    }
}
