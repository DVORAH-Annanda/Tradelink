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
    
    public partial class TLPPS_InterDept
    {
        public int TLInter_Pk { get; set; }
        public string TLInter_Description { get; set; }
        public int TLInter_Knitting_Fk { get; set; }
        public int TLInter_Dying_Fk { get; set; }
        public int TLInter_CMT_Fk { get; set; }
    }
}