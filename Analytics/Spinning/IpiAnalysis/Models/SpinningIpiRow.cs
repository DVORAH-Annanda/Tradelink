using System;

namespace Analytics.Spinning.IpiAnalysis.Models
{
    public sealed class SpinningIpiRow
    {
        public int MeasurementId { get; set; }
        public DateTime TestDate { get; set; }
        public string Merge { get; set; }
        public int? YarnOrderNumber { get; set; }
        public string Machine { get; set; }
        public string MachineDescription { get; set; }
        public decimal? NominalTex { get; set; }
        public decimal? MeasuredTex { get; set; }
        public decimal? CvPercentage { get; set; }
        public decimal? Thin { get; set; }
        public decimal? Thick { get; set; }
        public decimal? Nep { get; set; }
        public decimal? Ipi { get; set; }
        public bool HasYarnOrder { get; set; }
        public bool HasYarnType { get; set; }
        public bool HasMachine { get; set; }
    }
}
