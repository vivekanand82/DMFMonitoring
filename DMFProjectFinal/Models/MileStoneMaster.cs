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
    
    public partial class MileStoneMaster
    {
        public int MileStoneID { get; set; }
        public Nullable<int> DistrictID { get; set; }
        public Nullable<int> FundReleaseID { get; set; }
        public Nullable<int> ProjectID { get; set; }
        public Nullable<int> InstallmentID { get; set; }
        public string Instext { get; set; }
        public string InsPercentage { get; set; }
        public Nullable<bool> IsActive { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
        public string MofifiedBy { get; set; }
        public string ProjectNo { get; set; }
        public Nullable<long> ProjectPreparationID { get; set; }
        public Nullable<decimal> Releaseperamount { get; set; }
        public string Phyicalflag { get; set; }
    }
}
