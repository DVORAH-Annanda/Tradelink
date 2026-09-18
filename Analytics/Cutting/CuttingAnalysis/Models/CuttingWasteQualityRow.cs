namespace Analytics.Cutting.CuttingAnalysis.Models
{
    public class CuttingWasteQualityRow
    {
        public string Quality { get; set; }
        public decimal FabricWeight { get; set; }
        public decimal RecordedCuttingWaste { get; set; }
        public decimal RecordedPanelWaste { get; set; }
        public decimal TotalWaste { get; set; }

        public decimal CuttingWastePercentage
        {
            get
            {
                if (FabricWeight == 0)
                    return 0;

                return (RecordedCuttingWaste / FabricWeight) * 100M;
            }
        }

        public decimal PanelWastePercentage
        {
            get
            {
                if (FabricWeight == 0)
                    return 0;

                return (RecordedPanelWaste / FabricWeight) * 100M;
            }
        }

        public decimal TotalWastePercentage
        {
            get
            {
                if (FabricWeight == 0)
                    return 0;

                return (TotalWaste / FabricWeight) * 100M;
            }
        }
    }
}
