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
    
    public partial class TLADM_FabricProduct
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public TLADM_FabricProduct()
        {
            this.TLADM_FabricType = new HashSet<TLADM_FabricType>();
        }
    
        public int FP_Id { get; set; }
        public string FP_Description { get; set; }
        public int FP_PowerN { get; set; }
        public Nullable<bool> FP_Discontinued { get; set; }
        public Nullable<System.DateTime> FP_Discontinued_Date { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TLADM_FabricType> TLADM_FabricType { get; set; }
    }
}
