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
    
    public partial class TLSPN_YarnTransactions
    {
        public int YarnTrx_Pk { get; set; }
        public int YarnTrx_YarnOrder_FK { get; set; }
        public int YarnTrx_PalletNo_Fk { get; set; }
        public int YarnTrx_TranType_FK { get; set; }
        public System.DateTime YarnTrx_Date { get; set; }
        public int YarnTrx_SequenceNo { get; set; }
        public Nullable<int> YarnTrx_Customer_FK { get; set; }
        public string YarnTrx_OrderNo { get; set; }
        public int YarnTrx_Cones { get; set; }
        public string YarnTrx_ApprovedBy { get; set; }
        public string YarnTrx_Reasons { get; set; }
        public Nullable<decimal> YarnTrx_NettWeight { get; set; }
        public bool YarnTrx_WriteOff { get; set; }
        public Nullable<int> YarnTrx_FromDep_FK { get; set; }
        public Nullable<int> YarnTrx_ToDep_FK { get; set; }
    }
}
