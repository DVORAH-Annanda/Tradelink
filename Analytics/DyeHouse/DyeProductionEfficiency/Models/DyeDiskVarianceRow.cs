namespace Analytics.DyeHouse.DyeProductionEfficiency.Models
{
    public class DyeDiskVarianceRow
    {
        public string GreigeQuality { get; set; }
        public int PieceCount { get; set; }
        public decimal AverageStandardDisk { get; set; }
        public decimal AverageActualDisk { get; set; }
        public decimal DiskVariancePercentage
        {
            get { return AverageStandardDisk == 0 ? 0 : ((AverageActualDisk / AverageStandardDisk) - 1m) * 100m; }
        }
    }
}
