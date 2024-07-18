using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utilities
{
    public class TLDYE_GarmentDyeingProduction
    {
        public int Id { get; set; }
        public int GarmentDyeingTransactionNo { get; set; }
        public int Size { get; set; }
        public string Grade { get; set; }
        public string BoxNo { get; set; }
        public int BoxQuantity { get; set; }
        public bool Closed { get; set; }
    }
}
