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
    
    public partial class TehsilMaster
    {
        public int TehsilId { get; set; }
        public string TehsilName { get; set; }
        public string TehsilCode { get; set; }
        public Nullable<int> DistrictId { get; set; }
        public Nullable<bool> IsActive { get; set; }
        public string ISPTehsilCode { get; set; }
    
        public virtual DistrictMaster DistrictMaster { get; set; }
    }
}
