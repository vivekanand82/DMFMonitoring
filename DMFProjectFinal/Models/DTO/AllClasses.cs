using DMFProjectFinal.Models.Validations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DMFProjectFinal.Models.DTO
{
    public class P_ChangePassword
    {
        public string OldPass { get; set; }
        public string NewPass { get; set; }
        public string ConfirmPass { get; set; }
    }
    public partial class DTO_StateMaster
    {
        public string StateID { get; set; }
        [Display(Name = "State Name")]
        [Required]
        public string StateName { get; set; }
    }
    public partial class DTO_SectorTypeMaster
    {
        public string SectorTypeID { get; set; }
        [Display(Name = "Sector Type")]
        [Required]
        public string SectorType { get; set; }
       
        [Display(Name = "Percentage")]
        [Required]
        public string Percentage { get; set; }  //added by ramdhyam 02.04.2024
    }
    public partial class DTO_AgenciesInfo
    {
        public string AgencyID { get; set; }

        [Display(Name="Agency Name")]
        [Required]
        public string Name { get; set; }

        [Display(Name = "State")]
        [Required]
        public Nullable<int> StateID { get; set; }

        [Display(Name = "District")]
        [Required]
        public Nullable<int> DistID { get; set; }

        [Display(Name = "Owner Name")]
        [Required]
        [CSTM_OnlyStringsValidation]
        public string OwnerName { get; set; }

        [Display(Name = "Email ID")]
        [Required]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string EmailID { get; set; }

        [Display(Name = "Mobile No")]
        [Required]
        [CSTM_MobileValidation]
        public string MobileNo { get; set; }

        [Required]
        public string Address { get; set; }

        [Display(Name = "Alternate Mobile No")]
        [CSTM_MobileValidation]
        public string AlternateMobNo { get; set; }

        public string LandLineNo { get; set; }

        [Display(Name = "GSTIN No")]
        [Required]
        public string GSTIN { get; set; }

        [Display(Name = "PAN No")]
        [Required]
        [CSTM_PANValidation]
        public string PANNo { get; set; }

        [Display(Name = "Bank")]
        [Required]
        public Nullable<int> BankID { get; set; }

        [Display(Name = "Bank Branch")]
        [Required]
        public string BranchName { get; set; }

        [Display(Name = "Bank IFSC Code")]
        [Required]
        public string IFSCCode { get; set; }

        [Display(Name = "Bank Account No")]
        [Required]
        [CSTM_OnlyDigitsValidation]
        public string AccNo { get; set; }

        public string StateName { get; set; }
        public string DistName { get; set; }
        public string BankName { get; set; }
        public int RoleID { get; set; }
    }

    public partial class DTO_CommitteeMaster
    {
        public string CommitteeID { get; set; }

        [Display(Name = "Committee Type")]
        [Required]
        public Nullable<int> CommitteeTypeID { get; set; }

        [Display(Name = "Committee Name")]
        [Required]
        public string CommitteeName { get; set; }

        [Display(Name = "Designation")]
        [Required]
        public Nullable<int> DesignationID { get; set; }

        [Display(Name = "Mobile No")]
        [Required]
        [CSTM_MobileValidation]
        public string MobileNo { get; set; }

        [Display(Name = "Email ID")]
        [Required]
        [MaxLength(100)]
        [RegularExpression(@"^[a-zA-Z0-9.!#$%&'*+/=?^_`{|}~-]+@[a-zA-Z0-9-]+(?:\.[a-zA-Z0-9-]+)*$", ErrorMessage = "Invalid Email Address.")]

        public string EmailID { get; set; }


        public string CommitteeType { get; set; }
        public string CommitteeDesignationName { get; set; }
        public string Designation { get; set; }
        public string DistrictName { get; set; }

        [Display(Name = "District")]
        [Required]
        public Nullable<int> DistID { get; set; }

        [Display(Name = "CommitteeDesignation")]
        [Required]
        public Nullable<int> CommitteeDesignationId { get; set; }
        

    }






    public partial class DTO_LesseeMaster
    {
        public string LesseeID { get; set; }

        [Display(Name = "State")]
        [Required]
        public Nullable<int> StateID { get; set; }

        [Display(Name = "District")]
        [Required]
        public Nullable<int> DistID { get; set; }

        [Display(Name = "Tehsil")]
        [Required]
        public Nullable<int> TehsilID { get; set; }

        [Display(Name = "Village")]
        [Required]
        public Nullable<long> VillageID { get; set; }

        [Display(Name = "Area Code")]
        [Required]
        public string Areacode { get; set; }

        [Display(Name = "Gata/Khand No")]
        [Required]
        public string GataNo { get; set; }

        [Display(Name = "Total Area in Hect")]
        [Required]
        public Nullable<decimal> TotalAreainHect { get; set; }

        [Display(Name = "Lease ID")]
        [Required]
        public string LeaseID { get; set; }

        [Display(Name = "Minral")]
        [Required]
        public Nullable<int> MinralID { get; set; }

        [Display(Name = "Lessee Name")]
        [Required]
        [CSTM_OnlyStringsValidation]
        public string LesseeName { get; set; }

        [Display(Name = "Mobile No")]
        [Required]
        [CSTM_MobileValidation]
        public string MobNo { get; set; }

        [Display(Name = "Email ID")]
        [Required]
        public string EmailID { get; set; }

        [Display(Name = "Bidrate")]
        [Required]
        public Nullable<decimal> BidRate { get; set; }

        [Display(Name = "Lease From Date")]
        [Required]
        public Nullable<System.DateTime> LeaseFromDate { get; set; }

        [Display(Name = "Lease To Date")]
        [Required]
        public Nullable<System.DateTime> LeaseToDate { get; set; }


        public string State { get; set; }
        public string DistName { get; set; }
        public string Tehsil { get; set; }
        public string Village { get; set; }
        public string Minral { get; set; }
        public int RoleID { get; set; }
    }
    public partial class DTO_ProjectMaster
    {
        public string ProjectID { get; set; }


        [Display(Name = "District")]
        [Required]
        public Nullable<int> DistID { get; set; }

        [Display(Name = "Project Name")]
        [Required]
        public string ProjectName { get; set; }

        [Display(Name = "Project Code")]
        [Required]
        public string ProjectCode { get; set; }

        public string ProjectDescription { get; set; }

        public string DistName { get; set; }

      
        
        public Nullable<int> SectorTypeId { get; set; }
        
        [Display(Name = "Sector Type")]
        [Required]

        public Nullable<int> SectorNameId { get; set; }
        //[Display(Name = "Sector Name")]
        //[Required]
        public string SectorName { get; set; }

        public string SectorType { get; set; }

    }


    public partial class DTO_ProjectProposalPrepration
    {
        public string ProjectPreparationID { get; set; }
        public string VillageNameInHindi { get; set; }
        public string TehsilName { get; set; }
        public string BlockName { get; set; }
        public string VillageNameInEnglish { get; set; }

        [Display(Name = "Sector")]
        [Required]
        public Nullable<int> SectorID { get; set; }

        [Display(Name = "Prosposal No")]
        [Required]
        public string ProsposalNo { get; set; }

        [Display(Name = "Proposal Date")]
        [Required]
        public Nullable<System.DateTime> ProposalDate { get; set; }

        //[Display(Name = "Proposal Copy")]
        //[Required]
        public string ProposalCopy { get; set; }
        //[Display(Name = "WorkOrder Copy")]
        //[Required]
        public string WorkOrderCopy { get; set; }

        [Display(Name = "Proposed By")]
        [Required]
        public string ProposedBy { get; set; }

        //[Display(Name = "Project")]
        //[Required]
        public Nullable<int> ProjectID { get; set; }

        [Display(Name = "Tender No")]
        [Required]
        public string TenderNo { get; set; }

        [Display(Name = "Tender Date")]
        [Required]
        public Nullable<System.DateTime> TenderDate { get; set; }

        [Display(Name = "Work Order No")]
        [Required]
        public string WorkOrderNo { get; set; }

        [Display(Name = "Work Order Date")]
        [Required]
        public Nullable<System.DateTime> WorkOrderDate { get; set; }

        [Display(Name = "Agency")]
        [Required]
        public Nullable<long> AgencyID { get; set; }

        [Display(Name = "Project Cost")]
        [Required]
        public Nullable<decimal> ProjectCost { get; set; }

        [Display(Name = "GST and Other Charges")]
        [Required]
        public Nullable<decimal> GSTAndOthers { get; set; }

        [Display(Name = "Sanctioned Project Cost")]
        [Required]
        public Nullable<decimal> SanctionedProjectCost { get; set; }

        //[Display(Name = "Project Status")]
        //[Required]
        public Nullable<int> ProjectStatusID { get; set; }
        public Nullable<System.DateTime> CreatedOn { get; set; }
        public Nullable<long> CreatedBy { get; set; }
        public Nullable<long> ModifyBy { get; set; }
        public Nullable<System.DateTime> ModifyDate { get; set; }
        public Nullable<bool> IsActive { get; set; }

        public Nullable<int> SectorTypeId { get; set; }

        [Display(Name = "District")]
        [Required]
        public Nullable<int> DistID { get; set; }
        public Nullable<int> TehsilId { get; set; }
        public Nullable<int> BlockId { get; set; }
        public Nullable<long> VillageId { get; set; }
        public string AgencyName { get; set; }
        public string DistrictName { get; set; }
        public string ProjectName { get; set; }
        public string ProjectStatus { get; set; }
        public string SectorName { get; set; }
        public string SectorType { get; set; }
        public decimal? WorkLatitude { get; set; }
        public decimal? WorkLongitude { get; set; }
        public string ProjectDescription { get; set; }
        public string RunningStatus { get; set; }
        public string FinalStatus { get; set; }
        public Nullable<int> Stageid { get; set; }
        public Nullable<int> CommitteeID { get; set; }


        public Nullable<System.DateTime> MettingDate { get; set; }
        public Nullable<System.DateTime> Attendancedate { get; set; }

        public string MeetingNo { get; set; }
        public string MinutesofMeeting { get; set; }
        public string MinutesofMeetingfile { get; set; }
        public string Memberattendancefile { get; set; }
        public string Approvelletterfile { get; set; }
        public string Memberlist { get; set; }
        public string Status { get; set; }

        public string Remark { get; set; }
    }
    public partial class DTO_SectorNameMaster
    {
        public string SectorNameId { get; set; }
        [Display(Name = "Sector Type")]
        [Required]
        public int SectorTypeId { get; set; }
        public string SectorType { get; set; }
        [Display(Name = "Sector Name")]
        [Required]
        public string SectorName { get; set; }
        [Display(Name = "Sector Code")]
        [Required]
        public string SectorCode { get; set; }
    }
    public partial class DTO_MineralNameMaster
    {
        public string MineralId { get; set; }
        [Display(Name = "Mineral Name")]
        [Required]
        public string MineralName { get; set; }
        [Display(Name = "Mineral Type")]
        [Required]
        public string MineralType { get; set; }
        public string MineralCode { get; set; }
    }

    public partial class DTO_UserRegistration
    {
        public string LoginID { get; set; }
        [Display(Name = "Name")]
        [Required]
        public string Name { get; set; }
        [Display(Name = "Email Id")]
        [Required]
        public string EmailId { get; set; }
        [Display(Name = "Phone Number")]
        [RegularExpression("[0-9]{10}", ErrorMessage = "Invalid Mobile Number ")]
        [Required]
        public string PhoneNumber { get; set; }
        [Display(Name = "District Name")]
        [Required]
        public int? DistrictId { get; set; }
        [Display(Name = "Username")]
        [Required]
        public string UserName { get; set; }
        [Display(Name = "Password")]
        [Required]
        public string Password { get; set; }
        public string DistrictName { get; set; }
        public string RoleName { get; set; }
        public int? RoleID { get; set; }
    }
    public partial class DTO_MenuMaster
    {
        public string MenuID { get; set; }
        [Display(Name = "Menu Name")]
        [Required]
        public string MenuName { get; set; }
        public string ControllerName { get; set; }
        public string ActionName { get; set; }
        public long ParentId { get; set; }
        public bool IsParent { get; set; }
    }

    public partial class DTO_LesseeOpeningDMFAmt
    {
        public string OpeningDMFAmtId { get; set; }
        [Display(Name = "Lessee Name")]
        [Required]
        public long LesseeId { get; set; }
        public int MineralId { get; set; } 
        public int DistrictId { get; set; }
        public string DistrictName { get; set; }
        public string LeseeName { get; set; }
        [Display(Name = "DMF Opening Amount")]
        [Required]
        public Nullable<decimal> DMFOpeningAmt { get; set; }
        [Display(Name = "Effective Date")]
        [Required]
        public Nullable<System.DateTime> EffectiveDate { get; set; }
    }
    public partial class DTO_RoyaltyCollection
    {
        public string RoyaltyCollectionId { get; set; }
        [Display(Name = "Lessee Name")]
        [Required]
        public Nullable<long> LesseeId { get; set; }
        [Display(Name = "Year")]
        [Required]
        public Nullable<int> YearId { get; set; }
        public string MonthName { get; set; }
        public string YearName { get; set; }
        [Display(Name = "Month")]
        [Required]
        public Nullable<int> MonthId { get; set; }
        [Display(Name = "Royalty Amount")]
        [Required]
        public Nullable<decimal> RoyaltyAmt { get; set; }
        [Display(Name = "DMF Amount")]
        [Required]
        public Nullable<decimal> DMFAmt { get; set; }
        [Display(Name = "Challan Date")]
        [Required]
        public Nullable<System.DateTime> ChallanDate { get; set; }
        [Display(Name = "Challan Number")]
        [Required]
        public string ChallanNo { get; set; }
        public string ChallanDoc { get; set; }
        public string LeseeName { get; set; }

    }
    public partial class DTO_DMFFundCOllection
    {
        public string FundCollectionId { get; set; }
        [Display(Name = "Lessee Name")]
        [Required]
        public Nullable<long> LesseeId { get; set; }
        public Nullable<long> LesseeName { get; set; }
        [Display(Name = "Year")]
        [Required]
        public Nullable<int> YearId { get; set; }
        public string YearName { get; set; }
        [Display(Name = "Month")]
        [Required]
        public Nullable<int> MonthId { get; set; }
        public string MonthName { get; set; }
        [Display(Name = "Royalty Amount")]
        [Required]
        public Nullable<decimal> RoyaltyAmt { get; set; }
        [Display(Name = "DMF Amount")]
        [Required]
        public Nullable<decimal> DMFAmt { get; set; }
        [Display(Name = "Deposited DMF Amount")]
        [Required]
        public Nullable<decimal> DepositedDMFAmt { get; set; }
        [Display(Name = "Remaining DMF Amount")]
        [Required]
        public Nullable<decimal> RemainingDMFAmt { get; set; }
        [Display(Name = "Challan Date")]
        [Required]
        public Nullable<System.DateTime> ChallanDate { get; set; }
        public string ChallanDoc { get; set; }
        [Display(Name = "Challan Number")]
        [Required]
        public string ChallanNo { get; set; }
        public string LeseeName { get; set; }
    }

    public partial class DTO_DemoStateId
    {

        public string Id { get; set; }
        public string Name { get; set; }

        [Display(Name = "State")]
        [Required]
        public Nullable<int> StateID { get; set; }
    }
    public partial class DTO_District
    {
        [Display(Name = "State")]
        [Required]
        public Nullable<int> StateID { get; set; }

        [Display(Name = " Ditrict Name")]
        [Required]
        public string DistrictName { get; set; }

        public string DistrictId { get; set; }


        [Display(Name = "District Code")]
        [Required]
        public string DistrictCode { get; set; }
        public string StateName { get; set; }






    }

    public partial class DTO_CommitteeDesignation
    {
        [Display(Name = "Name")]
        [Required]

        public string CommitteeDesignationName { get; set; }
        public string CommitteeDesignationId { get; set; }


    }
    public partial class DTO_FundRelease
    {
        public string FundReleaseID { get; set; }
        public string SectorName { get; set; }
        public string SectorType { get; set; }
        public decimal? SanctionedProjectCost { get; set; }
        public string ProjectNo { get; set; }
        public string ProjectName { get; set; }
        public string DistrictName { get; set; }
        public Nullable<int> DistrictID { get; set; }
        public Nullable<int> ProjectID { get; set; }
        public Nullable<System.DateTime> RelaeseDate { get; set; }
        public Nullable<decimal> ReleaseAmount { get; set; }
        public Nullable<int> InstallmentID { get; set; }
        public string InstallmentName { get; set; }
        public string FundReleaseCopy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public Nullable<int> ModifiedBy { get; set; }
        public Nullable<System.DateTime> ModifyDate { get; set; }
        public Nullable<bool> IsActive { get; set; }
    }








}