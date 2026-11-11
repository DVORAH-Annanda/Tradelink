using System;

namespace Analytics.Spinning.QaMeasurements.Models
{
    // MeasureNo 1 = Card; MeasureNo 3 = RSB. The time-named columns are
    // legacy generic slots, not measurement times.
    public sealed class SpinningQaMeasurementRow
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public string Machine { get; set; }
        public string MachineDescription { get; set; }
        public int? TestNumber { get; set; }
        public decimal? DailyAverage { get; set; }  // Card YarnQA_08H00
        public decimal? CvPercentage { get; set; }  // Card YarnQA_10H00
        public decimal? Canister { get; set; }      // RSB YarnQA_10H00
        public decimal? Measurement { get; set; }   // RSB YarnQA_12H00
        public bool HasMachine { get; set; }
    }
}
