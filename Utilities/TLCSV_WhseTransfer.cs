//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Utilities
{
    using System;
    using System.Collections.Generic;
    
    public partial class TLCSV_WhseTransfer
    {
        public int TLCSVWHT_Pk { get; set; }
        public System.DateTime TLCSVWHT_Date { get; set; }
        public int TLCSVWHT_FromWhse_Fk { get; set; }
        public int TLCSVWHT_ToWhse_Fk { get; set; }
        public bool TLCSVWHT_PickList { get; set; }
        public Nullable<System.DateTime> TLCSVWHT_PickListDate { get; set; }
        public int TLCSVWHT_PickListNo { get; set; }
        public bool TLCSVWHT_DeliveryNote { get; set; }
        public int TLCSVWHT_DeliveryNo { get; set; }
        public bool TLCSVWHT_Receipted { get; set; }
        public Nullable<System.DateTime> TLCSVWHT_Receipt_Date { get; set; }
        public int TLCSVWHT_ReceiptNo { get; set; }
    }
}
