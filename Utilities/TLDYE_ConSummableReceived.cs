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
    
    public partial class TLDYE_ConSummableReceived
    {
        public int DYECON_Pk { get; set; }
        public int DYECON_Consumable_FK { get; set; }
        public System.DateTime DYECON_TransactionDate { get; set; }
        public int DYECON_WhseStore_FK { get; set; }
        public decimal DYECON_Amount { get; set; }
        public int DYECON_UOM_FK { get; set; }
        public int DYECON_Supplier_FK { get; set; }
        public string DYECON_OrderNo { get; set; }
        public string DYECON_ContainerId { get; set; }
        public bool DYECON_Pass { get; set; }
        public int DYECON_TransNumber { get; set; }
    }
}
