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
    
    public partial class TLADM_ProductRating
    {
        public int Pr_Id { get; set; }
        public int Pr_Label_FK { get; set; }
        public int Pr_Style_FK { get; set; }
        public int Pr_Size_Power { get; set; }
        public int Pr_BodyorRibbing { get; set; }
        public int Pr_PowerN { get; set; }
        public string Pr_Ratio { get; set; }
        public decimal Pr_Marker_Length { get; set; }
        public decimal Pr_numeric_Rating { get; set; }
    
        public virtual TLADM_Labels TLADM_Labels { get; set; }
        public virtual TLADM_Styles TLADM_Styles { get; set; }
    }
}
