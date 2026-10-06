namespace Analytics.DyeHouse.DyeProductionEfficiency.Models
{
    public class DyeProcessLossRow
    {
        public string Colour { get; set; }
        public decimal GrossWeight { get; set; }
        public decimal NettWeight { get; set; }
        public decimal ProcessLossPercentage
        {
            get { return GrossWeight == 0 ? 0 : ((NettWeight / GrossWeight) - 1m) * 100m; }
        }
    }
}
