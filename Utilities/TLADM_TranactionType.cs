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
    
    public partial class TLADM_TranactionType
    {
        public int TrxT_Pk { get; set; }
        public int TrxT_Department_FK { get; set; }
        public int TrxT_Number { get; set; }
        public string TrxT_Description { get; set; }
        public Nullable<int> TrxT_FromWhse_FK { get; set; }
        public Nullable<int> TrxT_ToWhse_FK { get; set; }
        public int TrxT_FinishedGoods_FK { get; set; }
        public bool TrxT_Discontinued { get; set; }
        public Nullable<System.DateTime> TrxT_DiscontinuedDate { get; set; }
    }
}
