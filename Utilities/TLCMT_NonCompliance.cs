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
    
    public partial class TLCMT_NonCompliance
    {
        public int CMTNCD_Pk { get; set; }
        public bool CMTNCD_Applicable { get; set; }
        public int CMTNCD_NonCompliance_Fk { get; set; }
        public int CMTNCD_CutSheet_Fk { get; set; }
        public System.DateTime CMTNCD_TransDate { get; set; }
        public int CMTNCD_WeekNumber { get; set; }
        public int CMTNCD_Year { get; set; }
        public int CMTNCD_Style_FK { get; set; }
        public int CMTNCD_Line_FK { get; set; }
    }
}
