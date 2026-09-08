using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Analytics.Knitting.KnittingAnalysis.Models
{
    public class KnittingQualityRow
    {
        public string GreigeProduct { get; set; }

        public string Machine { get; set; }

        public int TotalPieces { get; set; }

        public int AGrade { get; set; }

        public int BGrade { get; set; }

        public int CGrade { get; set; }


        public decimal BrokenNeedle { get; set; }

        public decimal DroppedStitches { get; set; }

        public decimal Holes { get; set; }

        public decimal OilMarks { get; set; }

        public decimal PressOff { get; set; }

        public decimal SlubMarks { get; set; }

        public decimal Thick { get; set; }

        public decimal Thin { get; set; }


        public decimal TotalFaults
        {
            get
            {
                return
                    BrokenNeedle +
                    DroppedStitches +
                    Holes +
                    OilMarks +
                    PressOff +
                    SlubMarks +
                    Thick +
                    Thin;
            }
        }


        public decimal AverageFaultsPerPiece
        {
            get
            {
                if (TotalPieces == 0)
                    return 0;

                return TotalFaults / TotalPieces;
            }
        }


        public decimal AverageHolesPerPiece
        {
            get
            {
                if (TotalPieces == 0)
                    return 0;

                return Holes / TotalPieces;
            }
        }


        public decimal AverageSlubsPerPiece
        {
            get
            {
                if (TotalPieces == 0)
                    return 0;

                return SlubMarks / TotalPieces;
            }
        }


        public decimal AverageFaultsExcludingSlubs
        {
            get
            {
                if (TotalPieces == 0)
                    return 0;

                return
                    (TotalFaults - SlubMarks)
                    / TotalPieces;
            }
        }
    }
}
