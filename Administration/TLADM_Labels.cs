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
    
    public partial class TLADM_Labels
    {
        public TLADM_Labels()
        {
            this.TLADM_GarmentDef = new HashSet<TLADM_GarmentDef>();
            this.TLADM_ProductRating = new HashSet<TLADM_ProductRating>();
        }
    
        public int Lbl_Id { get; set; }
        public string Lbl_Description { get; set; }
        public Nullable<bool> Lbl_Discontinued { get; set; }
        public Nullable<System.DateTime> Lbl_Discontinued_Date { get; set; }
        public int Lbl_PowerN { get; set; }
    
        public virtual ICollection<TLADM_GarmentDef> TLADM_GarmentDef { get; set; }
        public virtual ICollection<TLADM_ProductRating> TLADM_ProductRating { get; set; }
    }
}
