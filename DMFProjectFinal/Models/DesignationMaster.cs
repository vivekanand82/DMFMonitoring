//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DMFProjectFinal.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class DesignationMaster
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public DesignationMaster()
        {
            this.CommitteeMasters = new HashSet<CommitteeMaster>();
        }
    
        public int DesingantionID { get; set; }
        public string DesignationCode { get; set; }
        public string Designation { get; set; }
        public Nullable<bool> IsActive { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CommitteeMaster> CommitteeMasters { get; set; }
    }
}
