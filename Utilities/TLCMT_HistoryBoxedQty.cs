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
    
    public partial class TLCMT_HistoryBoxedQty
    {
        public int BoxHist_Pk { get; set; }
        public int BoxHist_CutSheet_FK { get; set; }
        public int BoxHist_CompletedWork_FK { get; set; }
        public System.DateTime BoxHist_DateTime { get; set; }
        public int BoxHist_Orignal_Qty { get; set; }
        public int BoxHist_New_Qty { get; set; }
        public int BoxHist_Size_FK { get; set; }
        public string BoxHist_No { get; set; }
    }
}
