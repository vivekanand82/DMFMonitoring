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
    
    public partial class FundRelease
    {
        public int FundReleaseID { get; set; }
        public Nullable<int> DistrictID { get; set; }
        public Nullable<int> ProjectID { get; set; }
        public Nullable<System.DateTime> RelaeseDate { get; set; }
        public Nullable<decimal> ReleaseAmount { get; set; }
        public string FundReleaseCopy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public string ModifiedBy { get; set; }
        public Nullable<System.DateTime> ModifyDate { get; set; }
        public Nullable<bool> IsActive { get; set; }
        public Nullable<int> InstallmentID { get; set; }
        public string ProjectNo { get; set; }
    }
}
