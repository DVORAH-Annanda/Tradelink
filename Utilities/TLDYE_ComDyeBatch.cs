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
    
    public partial class TLDYE_ComDyeBatch
    {
        public int DYEBC_Pk { get; set; }
        public string DYEBC_BatchNo { get; set; }
        public int DYEBC_Customer_FK { get; set; }
        public int DYEBC_Colour_FK { get; set; }
        public System.DateTime DYEBC_BatchDate { get; set; }
        public System.DateTime DYEBC_DateOrder { get; set; }
        public System.DateTime DYEBC_DateRequired { get; set; }
        public int DYEBC_Greige_FK { get; set; }
        public int DYEBC_Trim_Fk { get; set; }
        public int DYEBC_TransactionType { get; set; }
    }
}
