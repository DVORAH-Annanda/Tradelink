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
    
    public partial class TLADM_RejectReasons
    {
        public TLADM_RejectReasons()
        {
            this.TLADM_QualityDefinition = new HashSet<TLADM_QualityDefinition>();
        }
    
        public int RJR_Pk { get; set; }
        public string RJR_Description { get; set; }
    
        public virtual ICollection<TLADM_QualityDefinition> TLADM_QualityDefinition { get; set; }
    }
}
