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
    
    public partial class sp_WorkStatus_Result
    {
        public int DistrictId { get; set; }
        public string DistrictName { get; set; }
        public decimal SanctionedProjectCost { get; set; }
        public decimal ReleaseAmount { get; set; }
        public decimal AmountSpend { get; set; }
        public int Total_Project { get; set; }
        public int CompletedProject { get; set; }
        public int InProgressProject { get; set; }
    }
}
