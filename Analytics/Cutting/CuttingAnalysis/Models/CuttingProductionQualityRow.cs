using System;

namespace Analytics.Cutting.CuttingAnalysis.Models
{
    public class CuttingProductionQualityRow
    {
        public string Quality { get; set; }
        public decimal ExpectedQty { get; set; }
        public decimal ActualQty { get; set; }

        public decimal VarianceQty
        {
            get { return ActualQty - ExpectedQty; }
        }

        public decimal ExpectedVsActualPercentage
        {
            get
            {
                if (ExpectedQty == 0)
                    return 0;

                return (ActualQty / ExpectedQty) * 100M;
            }
        }

        public decimal VariancePercentage
        {
            get
            {
                if (ExpectedQty == 0)
                    return 0;

                return ((ActualQty - ExpectedQty) / ExpectedQty) * 100M;
            }
        }
    }
}
