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
    
    public partial class TLDYE_DyeBatchDetails
    {
        public int DYEBD_Pk { get; set; }
        public int DYEBD_DyeBatch_FK { get; set; }
        public int DYEBD_DyeOrderDet_FK { get; set; }
        public int DYEBD_GreigeProduction_FK { get; set; }
        public decimal DYEBD_GreigeProduction_Weight { get; set; }
        public bool DYEBD_BodyTrim { get; set; }
        public int DYEBD_QualityKey { get; set; }
        public Nullable<int> DYEBO_TrimKey { get; set; }
        public int DYEBO_GVRowNumber { get; set; }
        public decimal DYEBO_Nett { get; set; }
        public decimal DYEBO_DiskWeight { get; set; }
        public decimal DYEBO_Width { get; set; }
        public decimal DYEBO_Meters { get; set; }
        public Nullable<System.DateTime> DYEBO_DyeDate { get; set; }
        public Nullable<System.DateTime> DYEBO_TransDate { get; set; }
        public bool DYEBO_QAApproved { get; set; }
        public Nullable<System.DateTime> DYEBO_ApprovalDate { get; set; }
        public bool DYEBO_Rejected { get; set; }
        public Nullable<System.DateTime> DYEBO_RejectedDate { get; set; }
        public Nullable<bool> DYEBO_TransferPrinted { get; set; }
        public bool DYEBO_WriteOff { get; set; }
        public int DYEBO_CurrentStore_FK { get; set; }
        public bool DYEBO_Sold { get; set; }
        public Nullable<System.DateTime> DYEBO_DateSold { get; set; }
        public string DYEBO_TransactionNo { get; set; }
        public int DYEBO_ProductRating_FK { get; set; }
        public bool DYEBO_CutSheet { get; set; }
        public decimal DYEBO_AdjustedWeight { get; set; }
        public bool DYEBO_BIFInTransit { get; set; }
        public bool DYEBO_WasRejected { get; set; }
        public string DYEBO_Notes { get; set; }
        public decimal DYEBO_FWAtCutting { get; set; }
        public int DYEBO_PurchaseOrderDetail_FK { get; set; }
        public bool DYEBO_PendingDelivery { get; set; }
        public bool DYEBO_SaleConfirmed { get; set; }
        public bool DYEBO_FabricDespatched { get; set; }
        public bool DYEBO_MarkedForDeletion { get; set; }
    }
}
