using System;

namespace MinutesOfMeeting.Models
{
    public class P_Contractor
    {
        public string Name { get; set; }
        public bool? IsAllowEdit { get; set; }
        public string FatherName { get; set; }
        public DateTime? DOB { get; set; }
        public string EmailID { get; set; }
        public string MobileNo { get; set; }
        public string ApplicationNo { get; set; }
        public string AadharNo { get; set; }
        public string ProfilePic { get; set; }
        public string PAN { get; set; }
        public int? DistrictID { get; set; }
        public int? BlockID { get; set; }
        public string Address { get; set; }
        public string Nationality { get; set; }
        public int? StatusID { get; set; }
        public DateTime? CreatedOn { get; set; }
        public long RegistrationID { get; set; }
        public int? BankID { get; set; }
        public string AccountNo { get; set; }
        public string IFSCCode { get; set; }
        public string Attachment { get; set; }
        public int? CategoryID { get; set; }
        public string Organization { get; set; }
        public string CompanyName { get; set; }
        public string CompanyRegAddress { get; set; }
        public string RegisteredNo { get; set; }
        public string StateName { get; set; }
        public string PAN_TAN { get; set; }
        public string Pincode { get; set; }
        public string I_Aadhar { get; set; }
        public string I_PAN { get; set; }
        public string I_Reservoir { get; set; }
        public string C_RegistrationCertificate { get; set; }
        public string C_HaisiyatProof { get; set; }
        public string C_PropHouseIncometaxReturn { get; set; }
        public string PaymentReferenceNo { get; set; }
        public string C_OtherReturn { get; set; }
        public string PaymentReceipt { get; set; }
        public string HaisiyatType { get; set; }
    }
}