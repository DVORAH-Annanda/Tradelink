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
    
    public partial class TLCSV_RePackTransactions
    {
        public int REPACT_Pk { get; set; }
        public int REPACT_BoxedQty { get; set; }
        public int REPACT_RePackConfig_FK { get; set; }
        public int REPACT_StockOnHand_FK { get; set; }
        public int REPACT_PurchaseOrderDetail_FK { get; set; }
        public int REPACT_PurchaseOrder_FK { get; set; }
    }
}
