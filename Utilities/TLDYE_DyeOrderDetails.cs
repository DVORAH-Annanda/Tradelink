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
    
    public partial class TLDYE_DyeOrderDetails
    {
        public int TLDYOD_Pk { get; set; }
        public int TLDYOD_DyeOrder_Fk { get; set; }
        public Nullable<int> TLDYOD_Labels_FK { get; set; }
        public decimal TLDYOD_Rating { get; set; }
        public decimal TLDYOD_Yield { get; set; }
        public Nullable<decimal> TLDYOD_Kgs { get; set; }
        public bool TLDYOD_BodyOrTrim { get; set; }
        public int TLDYOD_Units { get; set; }
        public int TLDYOD_Greige_FK { get; set; }
        public int TLDYOD_MarkerRating_FK { get; set; }
        public Nullable<int> TLDYOD_Trims_FK { get; set; }
        public int TLDYOD_OriginalUnit { get; set; }
        public int TLDYOD_Styles_FK { get; set; }
        public int TLDYOD_Sizes_FK { get; set; }
    }
}
