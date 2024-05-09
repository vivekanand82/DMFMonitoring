using DMFProjectFinal.DAL;
using DMFProjectFinal.Models.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DMFProjectFinal.Controllers
{
    [SessionFilter]
    public class ReportController : Controller
    {
        // GET: Report
        DMFFundCollectionDB CollectionDB=new DMFFundCollectionDB();
        DistrictWiseProjectDB DistrictWisedb = new DistrictWiseProjectDB();
        DashBoardReportDB DashBoardDB = new DashBoardReportDB();
        public ActionResult Index()
        {
            return View();
        }
        #region FundCollection
        public ActionResult DMFFundCollectionReport(DTO_FundCollectionReport model)
        {
            int? DistID = null;
            if (UserManager.GetUserLoginInfo(User.Identity.Name).RoleID == 2)
            {
                DistID = UserManager.GetUserLoginInfo(User.Identity.Name).DistID;
                model.DistrictId =DistID;
            }
            List<DTO_FundCollectionReport> lst = new List<DTO_FundCollectionReport>();
            DataSet ds = CollectionDB.GetTotalFundCollection(DistID);
            if (ds != null && ds.Tables[0].Rows.Count > 0 && ds.Tables.Count > 0)
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    lst.Add(new DTO_FundCollectionReport
                    {
                        DistrictId = (int)dr["DistrictId"],
                        DistrictName = dr["DistrictName"].ToString(),
                      TotalFundCollection=Convert.ToDecimal(dr["TotalFundCollection"]),
                    });
                    ViewBag.data = lst;
                }
            }
                return View();
        }
        [HttpPost]
        public JsonResult FundCollectionMineralTypeWise(int DistrictId)
        {
            //int? DistID = null;
            //if (UserManager.GetUserLoginInfo(User.Identity.Name).RoleID == 2)
            //{
            //    DistID = UserManager.GetUserLoginInfo(User.Identity.Name).DistID;
            //    model.DistrictId = DistID;
            //}
            List<DTO_FundCollectionReport> lst = new List<DTO_FundCollectionReport>();
            DataSet ds = CollectionDB.GetCollectionMineralTypeWise(DistrictId);
            if (ds != null && ds.Tables[0].Rows.Count > 0 && ds.Tables.Count > 0)
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    lst.Add(new DTO_FundCollectionReport
                    {
                        DistrictId = (int)dr["DistrictId"],
                        DistrictName = dr["DistrictName"].ToString(),
                        MineralType = dr["MineralType"].ToString(),
                        TotalFundCollection = Convert.ToDecimal(dr["TotalFundCollection"]),
                    });
                   
                }
            }
            return Json(lst, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult FundCollectionMineralNameWise(int DistrictId, string MineralType)
        {
            //int? DistID = null;
            //if (UserManager.GetUserLoginInfo(User.Identity.Name).RoleID == 2)
            //{
            //    DistID = UserManager.GetUserLoginInfo(User.Identity.Name).DistID;
            //    model.DistrictId = DistID;
            //}
            List<DTO_FundCollectionReport> lst = new List<DTO_FundCollectionReport>();
            DataSet ds = CollectionDB.GetCollectionMineralNameWise(DistrictId, MineralType);
            if (ds != null && ds.Tables[0].Rows.Count > 0 && ds.Tables.Count > 0)
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    lst.Add(new DTO_FundCollectionReport
                    {
                        MineralId = (int)dr["MineralId"],
                        DistrictId = (int)dr["DistrictId"],
                        DistrictName = dr["DistrictName"].ToString(),
                        MineralType = dr["MineralType"].ToString(),
                        MineralName = dr["MineralName"].ToString(),
                        TotalFundCollection = Convert.ToDecimal(dr["TotalFundCollection"]),
                    });

                }
            }
            return Json(lst, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult FundCollectionLesseeWise(int DistrictId,int MineralId, string MineralType)
        {
            //int? DistID = null;
            //if (UserManager.GetUserLoginInfo(User.Identity.Name).RoleID == 2)
            //{
            //    DistID = UserManager.GetUserLoginInfo(User.Identity.Name).DistID;
            //    model.DistrictId = DistID;
            //}
            List<DTO_FundCollectionReport> lst = new List<DTO_FundCollectionReport>();
            DataSet ds = CollectionDB.GetCollectionMineralLesseeWise(DistrictId, MineralId, MineralType);
            if (ds != null && ds.Tables[0].Rows.Count > 0 && ds.Tables.Count > 0)
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    lst.Add(new DTO_FundCollectionReport
                    {
                        DistrictId = (int)dr["DistrictId"],
                        MineralId = (int)dr["MineralId"],
                        LesseeId = (long)dr["LesseeId"],
                        DistrictName = dr["DistrictName"].ToString(),
                        MineralType = dr["MineralType"].ToString(),
                        MineralName = dr["MineralName"].ToString(),
                        LeseeName = dr["LeseeName"].ToString(),
                        TotalFundCollection = Convert.ToDecimal(dr["COLLECTIONT"]),
                    });

                }
            }
            return Json(lst, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult FundCollectionReportByLessee(int DistrictId, int MineralId,int LesseeId)
        {
            //int? DistID = null;
            //if (UserManager.GetUserLoginInfo(User.Identity.Name).RoleID == 2)
            //{
            //    DistID = UserManager.GetUserLoginInfo(User.Identity.Name).DistID;
            //    model.DistrictId = DistID;
            //}
            List<DTO_FundCollectionReport> lst = new List<DTO_FundCollectionReport>();
            DataSet ds = CollectionDB.GetCollectionReportByLessee(DistrictId, MineralId, LesseeId);
            if (ds != null && ds.Tables[0].Rows.Count > 0 && ds.Tables.Count > 0)
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    lst.Add(new DTO_FundCollectionReport
                    {
                        DistrictId = (int)dr["DistrictId"],
                        DepositedDMFAmt =Convert.ToDecimal(dr["DepositedDMFAmt"]),
                        ChallanDate = Convert.ToDateTime(dr["ChallanDate"]),
                        ChallanNo = dr["ChallanNo"].ToString(),
                        DistrictName = dr["DistrictName"].ToString(),
                        LesseeId = (long)dr["LesseeId"],
                        MineralName = dr["MineralName"].ToString(),
                        MineralType = dr["MineralType"].ToString(),
                        LeseeName = dr["LesseeName"].ToString(),
                        MineralId = (int)dr["MineralId"],
                        challanDOc = dr["challanDOc"].ToString(),
                        Areacode = dr["Areacode"].ToString(),
                        GataNo = dr["GataNo"].ToString(),
                        TotalAreainHect = Convert.ToDecimal(dr["TotalAreainHect"]),
                        LeaseID = dr["LeaseID"].ToString(),
                        MobNo = dr["MobNo"].ToString(),
                        EmailID = dr["EmailID"].ToString(),
                        BidRate = Convert.ToDecimal(dr["BidRate"]),
                        LeaseFromDate = Convert.ToDateTime(dr["LeaseFromDate"]),
                        LeaseToDate = Convert.ToDateTime(dr["LeaseToDate"]),
                        RoyaltyAmt = Convert.ToDecimal(dr["RoyaltyAmt"]),
                        DMFAmt = Convert.ToDecimal(dr["DMFAmt"]),
                        RemainingDMFAmt = Convert.ToDecimal(dr["RemainingDMFAmt"]),
                        YearName = dr["YearName"].ToString(),
                        MonthName = dr["MonthName"].ToString(),

                    });

                }
            }
            return Json(lst, JsonRequestBehavior.AllowGet);
        }
        #endregion
        #region District and Sector Wise Project Report
        public ActionResult DistrictWiseProjectReport(DTO_DistrictWiseProjectReport model)
        {
            int? DistID = null;
            if (UserManager.GetUserLoginInfo(User.Identity.Name).RoleID == 2)
            {
                DistID = UserManager.GetUserLoginInfo(User.Identity.Name).DistID;
                model.DistID = DistID;
            }
            List<DTO_DistrictWiseProjectReport> lst = new List<DTO_DistrictWiseProjectReport>();
            DataSet ds = DistrictWisedb.GetAllProjects(model);
            if (ds != null && ds.Tables[0].Rows.Count > 0 && ds.Tables.Count > 0)
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    lst.Add(new DTO_DistrictWiseProjectReport
                    {
                        DistID = (int)dr["DistID"],
                        DistrictName = dr["DistrictName"].ToString(),
                        Total_Project = dr["Total_Project"].ToString(),
                    });
                    ViewBag.data = lst;
                }
            }
            return View();
        }
        [HttpPost]
        public JsonResult SectorTypeWiseProjectReport(int DistID)
        {
          
            List<DTO_DistrictWiseProjectReport> lst = new List<DTO_DistrictWiseProjectReport>();
            DataSet ds = DistrictWisedb.SectorTypeWiseProject(DistID);
            if (ds != null && ds.Tables[0].Rows.Count > 0 && ds.Tables.Count > 0)
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    lst.Add(new DTO_DistrictWiseProjectReport
                    {
                        DistID = (int)dr["DistID"],
                        SectorTypeId = (int)dr["SectorTypeId"],
                        DistrictName = dr["DistrictName"].ToString(),
                        SectorType = dr["SectorType"].ToString(),
                        Total_Project = dr["Total_Project"].ToString(),
                    });

                }
            }
            return Json(lst, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult SectorNameWiseProjectReport(int DistID, int SectorTypeId)
        {
            List<DTO_DistrictWiseProjectReport> lst = new List<DTO_DistrictWiseProjectReport>();
            DataSet ds = DistrictWisedb.SectorNameWiseProject(DistID, SectorTypeId);
            if (ds != null && ds.Tables[0].Rows.Count > 0 && ds.Tables.Count > 0)
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    lst.Add(new DTO_DistrictWiseProjectReport
                    {
                        DistID = (int)dr["DistID"],
                        SectorTypeId = (int)dr["SectorTypeId"],
                        SectorID = (int)dr["SectorID"],
                        DistrictName = dr["DistrictName"].ToString(),
                        SectorType = dr["SectorType"].ToString(),
                        SectorName = dr["SectorName"].ToString(),
                        Total_Project = dr["Total_Project"].ToString(),
                    });

                }
            }
            return Json(lst, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult SectorAndProjectReport(int DistID, int SectorTypeId,int SectorID)
        {
            List<DTO_DistrictWiseProjectReport> lst = new List<DTO_DistrictWiseProjectReport>();
          
            DataSet ds = DistrictWisedb.SectorAndProjectReport(DistID, SectorTypeId, SectorID);
            if (ds != null && ds.Tables[0].Rows.Count > 0 && ds.Tables.Count > 0)
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    lst.Add(new DTO_DistrictWiseProjectReport
                    {
                        DistID = (int)dr["DistID"],
                        SectorTypeId = (int)dr["SectorTypeId"],
                        SectorID = (int)dr["SectorID"],
                        DistrictName = dr["DistrictName"].ToString(),
                        SectorType = dr["SectorType"].ToString(),
                        SectorName = dr["SectorName"].ToString(),
                        ProjectName = dr["ProjectName"].ToString(),
                        ProjectNo = dr["ProjectNo"].ToString(),
                        ProjectPreparationID = dr["ProjectPreparationID"].ToString(),
                        
                    });

                }
                
            }
         
            return Json(lst, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult ProjectWiseReport(int DistID, int SectorTypeId, int SectorID, string ProjectPreparationID)
        {
            List<DTO_DistrictWiseProjectReport> lst = new List<DTO_DistrictWiseProjectReport>();
            List<DTO_ProjectMeeting> lst1 = new List<DTO_ProjectMeeting>();
            List<DTO_MileStoneMaster> lst2 = new List<DTO_MileStoneMaster>();
            List<DTO_FundRelease> lst3 = new List<DTO_FundRelease>();
            List<DTO_PhysicalProgressMaster> lst4 = new List<DTO_PhysicalProgressMaster>();
            List<DTO_UtilizationMaster> lst5 = new List<DTO_UtilizationMaster>();
            List<DTO_InspectionMaster> lst6 = new List<DTO_InspectionMaster>();
            DataSet ds = DistrictWisedb.ProjectWiseReport(DistID, SectorTypeId, SectorID, ProjectPreparationID);
            if (ds != null && ds.Tables[0].Rows.Count > 0 && ds.Tables.Count > 0)
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    lst.Add(new DTO_DistrictWiseProjectReport
                    {
                        //SectorTypeId = (int)dr["SectorTypeId"],
                        //SectorID = (int)dr["SectorID"],
                        DistrictName = dr["DistrictName"].ToString(),
                        TehsilName = dr["TehsilName"].ToString(),
                        BlockName = dr["BlockName"].ToString(),
                        VillageNameInEnglish = dr["VillageNameInEnglish"].ToString(),
                        ProjectName = dr["ProjectName"].ToString(),
                        ProjectNo = dr["ProjectNo"].ToString(),
                        SectorName = dr["SectorName"].ToString(),
                        SectorType = dr["SectorType"].ToString(),
                        WorkLatitude = Convert.ToDecimal(dr["WorkLatitude"]),
                        WorkLongitude = Convert.ToDecimal(dr["WorkLongitude"]),
                        AgencyName = dr["Name"].ToString(),
                        SanctionedProjectCost = Convert.ToDecimal(dr["SanctionedProjectCost"]),
                        ProjectDescription = dr["ProjectDescription"].ToString(),
                        ProsposalNo = dr["ProsposalNo"].ToString(),
                        ProposalDate = Convert.ToDateTime(dr["ProposalDate"]),
                        ProposalCopy = dr["ProposalCopy"].ToString(),
                        ProposedBy = dr["ProposedBy"].ToString(),
                        ProjectCost = Convert.ToDecimal(dr["ProjectCost"]),
                        GSTAndOthers = Convert.ToDecimal(dr["GSTAndOthers"]),
                        TenderNo = dr["TenderNo"].ToString(),
                        TenderDate = Convert.ToDateTime(dr["TenderDate"]),
                        WorkOrderNo = dr["WorkOrderNo"].ToString(),
                        WorkOrderDate = Convert.ToDateTime(dr["WorkOrderDate"]),
                        WorkOrderCopy = dr["WorkOrderCopy"].ToString(),
                        RunningStatus = dr["RunningStatus"].ToString(),
                        FinalStatus = dr["FinalStatus"].ToString(),
                        Stageid = (int)dr["Stageid"],
                        ProjectStartDate = Convert.ToDateTime(dr["ProjectStartDate"]),
                        ProjectCompletionDate = Convert.ToDateTime(dr["ProjectCompletionDate"]),

                    });

                }
            }
            if (ds != null && ds.Tables[1].Rows.Count > 0 && ds.Tables.Count > 0)
            {
                foreach (DataRow dr in ds.Tables[1].Rows)
                {
                    lst1.Add(new DTO_ProjectMeeting
                    {
                        MettingDate = Convert.ToDateTime(dr["MettingDate"]),
                        Attendancedate = Convert.ToDateTime(dr["Attendancedate"]),
                        MeetingNo = dr["MeetingNo"].ToString(),
                        MinutesofMeeting = dr["MinutesofMeeting"].ToString(),
                        MinutesofMeetingfile = dr["MinutesofMeetingfile"].ToString(),
                        Memberattendancefile = dr["Memberattendancefile"].ToString(),
                        Approvelletterfile = dr["Approvelletterfile"].ToString(),
                        Status = dr["Status"].ToString(),
                        Memberlist = dr["Memberlist"].ToString(),
                        Remark = dr["Remark"].ToString(),
                    });

                }
            }
            if (ds != null && ds.Tables[2].Rows.Count > 0 && ds.Tables.Count > 0)
            {
                foreach (DataRow dr in ds.Tables[2].Rows)
                {
                    lst1.Add(new DTO_ProjectMeeting
                    {
                        MettingDate = Convert.ToDateTime(dr["MettingDate"]),
                        Attendancedate = Convert.ToDateTime(dr["Attendancedate"]),
                        MeetingNo = dr["MeetingNo"].ToString(),
                        MinutesofMeeting = dr["MinutesofMeeting"].ToString(),
                        MinutesofMeetingfile = dr["MinutesofMeetingfile"].ToString(),
                        Memberattendancefile = dr["Memberattendancefile"].ToString(),
                        Approvelletterfile = dr["Approvelletterfile"].ToString(),
                        Status = dr["Status"].ToString(),
                        Memberlist = dr["Memberlist"].ToString(),
                        Remark = dr["Remark"].ToString(),
                    });

                }
            }
            if (ds != null && ds.Tables[3].Rows.Count > 0 && ds.Tables.Count > 0)
            {
                foreach (DataRow dr in ds.Tables[3].Rows)
                {
                    lst2.Add(new DTO_MileStoneMaster
                    {
                        Instext = dr["Instext"].ToString(),
                        InsPercentage = dr["InsPercentage"].ToString(),
                        InstallmentName = dr["InstallmentName"].ToString(),
                        IsFundReleased = Convert.ToBoolean(dr["IsFundReleased"]),
                        IsPhProgressDone = Convert.ToBoolean(dr["IsPhProgressDone"]),
                        IsUtilizationUploaded = Convert.ToBoolean(dr["IsUtilizationUploaded"]),
                        IsInspectionDone = Convert.ToBoolean(dr["IsInspectionDone"]),
                    });

                }
            }
            if (ds != null && ds.Tables[4].Rows.Count > 0 && ds.Tables.Count > 0)
            {
                foreach (DataRow dr in ds.Tables[4].Rows)
                {
                    lst3.Add(new DTO_FundRelease
                    {
                        FundReleaseCopy = dr["FundReleaseCopy"].ToString(),
                        InstallmentName = dr["InstallmentName"].ToString(),
                        IsPhProgressDone = Convert.ToBoolean(dr["IsPhProgressDone"]),
                        IsUtilizationUploaded = Convert.ToBoolean(dr["IsUtilizationUploaded"]),
                        IsInspectionDone = Convert.ToBoolean(dr["IsInspectionDone"]),
                        ReleaseAmount = Convert.ToDecimal(dr["ReleaseAmount"]),
                        RelaeseDate = Convert.ToDateTime(dr["RelaeseDate"]),
                    });

                }
            }
            if (ds != null && ds.Tables[5].Rows.Count > 0 && ds.Tables.Count > 0)
            {
                foreach (DataRow dr in ds.Tables[5].Rows)
                {
                    lst4.Add(new DTO_PhysicalProgressMaster
                    {
                        ProgressStatus = (int)dr["ProgressStatus"],
                        Remark = dr["Remark"].ToString(),
                        PhysicalProgressCopy = dr["PhysicalProgressCopy"].ToString(),
                        InstallmentName = dr["InstallmentName"].ToString(),
                        PhysicalPInPer = dr["PhysicalPInPer"].ToString(),
                        PhysicalProgressImages = dr["PhysicalProgressImages"].ToString(),
                        IsUtilizationUploaded = Convert.ToBoolean(dr["IsUtilizationUploaded"]),
                        IsInspectionDone = Convert.ToBoolean(dr["IsInspectionDone"]),
                        AmountSpend = Convert.ToDecimal(dr["AmountSpend"]),
                        PhysicalProgressDate = Convert.ToDateTime(dr["PhysicalProgressDate"]),
                    });

                }
            }
            if (ds != null && ds.Tables[6].Rows.Count > 0 && ds.Tables.Count > 0)
            {
                foreach (DataRow dr in ds.Tables[6].Rows)
                {
                    lst5.Add(new DTO_UtilizationMaster
                    {
                        Remarks = dr["Remarks"].ToString(),
                        UtilizationCopy = dr["UtilizationCopy"].ToString(),
                        InstallmentName = dr["InstallmentName"].ToString(),
                        UtilizationNo = dr["UtilizationNo"].ToString(),
                        UC_Against_ReleaseAmount = dr["UC_Against_ReleaseAmount"].ToString(),
                        UtilizationDate =Convert.ToDateTime( dr["UtilizationDate"]),
                      
                    });

                }
            }
            if (ds != null && ds.Tables[7].Rows.Count > 0 && ds.Tables.Count > 0)
            {
                foreach (DataRow dr in ds.Tables[7].Rows)
                {
                    lst6.Add(new DTO_InspectionMaster
                    {
                        InstallmentName = dr["InstallmentName"] != DBNull.Value ? dr["InstallmentName"].ToString() : string.Empty,
                        InspectionQuestion = dr["InspectionQuestion"].ToString(),
                        InspectionAnswer = dr["InspectionAnswer"].ToString(),
                        InspectionCopy = dr["InspectionCopy"] != DBNull.Value ? dr["InspectionCopy"].ToString() : string.Empty,
                        InspectionImage1 = dr["InspectionImage1"] != DBNull.Value ? dr["InspectionImage1"].ToString() : string.Empty,
                        InspectionImage2 = dr["InspectionImage2"] != DBNull.Value ? dr["InspectionImage2"].ToString() : string.Empty,
                        InspectionImage3 = dr["InspectionImage3"] != DBNull.Value ? dr["InspectionImage3"].ToString() : string.Empty,
                        InspectionImage4 = dr["InspectionImage4"] != DBNull.Value ? dr["InspectionImage4"].ToString() : string.Empty,
                        InspectionDate = Convert.ToDateTime(dr["InspectionDate"] != DBNull.Value ? dr["InspectionDate"] : null),
                        Remark = dr["Remark"] != DBNull.Value ? dr["Remark"].ToString() : string.Empty,
                        

               

                });

                }
            }
            lst[0].mettings = lst1;
            lst[0].milestones = lst2;
            lst[0].Funds = lst3;
            lst[0].Progress = lst4;
            lst[0].Utilizations = lst5;
            lst[0].Inspections = lst6;
            
                return Json(lst, JsonRequestBehavior.AllowGet);
            }
        #endregion
        #region Show the Details Of Sanction and Expanditure Sector and project wise
        public ActionResult DistrictWiseSanctionAndProjectList()
        {
            return View();
        }
        [HttpPost]
        public JsonResult DistrictWiseSanctionAndProject()
        {
            List<DTO_SanctionCostAndProjectDetails> lst = new List<DTO_SanctionCostAndProjectDetails>();
            int? DistID = null;
            if (UserManager.GetUserLoginInfo(User.Identity.Name).RoleID == 2)
            {
                DistID = UserManager.GetUserLoginInfo(User.Identity.Name).DistID;
                //model.DistrictId = DistID;
            }
            DataSet ds = DashBoardDB.GetDistrictSanctionExpenditureAndProjectDetails(DistID);
            if (ds != null && ds.Tables[0].Rows.Count > 0 && ds.Tables.Count > 0)
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    lst.Add(new DTO_SanctionCostAndProjectDetails
                    {
                        DistrictId = (int)dr["DistrictId"],
                        DistrictName = dr["DistrictName"].ToString(),
                        SanctionedProjectCost = Convert.ToDecimal(dr["SanctionedProjectCost"]),
                        ReleaseAmount = Convert.ToDecimal(dr["ReleaseAmount"]),
                        AmountSpend = Convert.ToDecimal(dr["AmountSpend"]),
                        Total_Project = (int)dr["Total_Project"],
                        CompletedProject = (int)dr["CompletedProject"],
                        InProgressProject = (int)dr["InProgressProject"],

                    });
                    //ViewBag.data = lst;
                }
            }
            return Json(lst, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public ActionResult SectorTypeWiseSanctionAndProjectList(int DistrictId)
        {
            List<DTO_SanctionCostAndProjectDetails> lst = new List<DTO_SanctionCostAndProjectDetails>();
            DataSet ds = DashBoardDB.GetSectorTypeSanctionExpenditureAndProjectDetails(DistrictId);
            if (ds != null && ds.Tables[0].Rows.Count > 0 && ds.Tables.Count > 0)
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    lst.Add(new DTO_SanctionCostAndProjectDetails
                    {
                        DistrictId = (int)dr["DistrictId"],
                        SectorTypeId = (int)dr["SectorTypeId"],
                        SectorType = dr["SectorType"].ToString(),
                        DistrictName = dr["DistrictName"].ToString(),
                        SanctionedProjectCost = Convert.ToDecimal(dr["SanctionedProjectCost"]),
                        ReleaseAmount = Convert.ToDecimal(dr["ReleaseAmount"]),
                        AmountSpend = Convert.ToDecimal(dr["AmountSpend"]),
                        Total_Project = (int)dr["Total_Project"],
                        CompletedProject = (int)dr["CompletedProject"],
                        InProgressProject = (int)dr["InProgressProject"],

                    });
                    ViewBag.data = lst;
                }
            }
            return View();
        }

        [HttpGet]
        public ActionResult SectorWiseSanctionAndProjectList(int DistrictId, int SectorTypeId)
        {
            List<DTO_SanctionCostAndProjectDetails> lst = new List<DTO_SanctionCostAndProjectDetails>();
            //int? DistID = null;
            //if (UserManager.GetUserLoginInfo(User.Identity.Name).RoleID == 2)
            //{
            //    DistID = UserManager.GetUserLoginInfo(User.Identity.Name).DistID;
            //    //model.DistrictId = DistID;
            //}
            DataSet ds = DashBoardDB.GetSectorSanctionExpenditureAndProjectDetails(DistrictId, SectorTypeId);
            if (ds != null && ds.Tables[0].Rows.Count > 0 && ds.Tables.Count > 0)
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    lst.Add(new DTO_SanctionCostAndProjectDetails
                    {
                        DistrictId = (int)dr["DistrictId"],
                        SectorID = (int)dr["SectorID"],
                        SectorTypeId = (int)dr["SectorTypeId"],
                        SectorName = dr["SectorName"].ToString(),
                        SectorType = dr["SectorType"].ToString(),
                        DistrictName = dr["DistrictName"].ToString(),
                        SanctionedProjectCost = Convert.ToDecimal(dr["SanctionedProjectCost"]),
                        ReleaseAmount = Convert.ToDecimal(dr["ReleaseAmount"]),
                        AmountSpend = Convert.ToDecimal(dr["AmountSpend"]),
                        Total_Project = (int)dr["Total_Project"],
                        CompletedProject = (int)dr["CompletedProject"],
                        InProgressProject = (int)dr["InProgressProject"],

                    });
                    ViewBag.data = lst;
                }
            }
            return View();
        }
        [HttpGet]
        public ActionResult GetPDetailSectorWiseList(int DistrictId, int SectorTypeId, int SectorID)
        {
            List<DTO_SanctionCostAndProjectDetails> lst = new List<DTO_SanctionCostAndProjectDetails>();
            //int? DistID = null;
            //if (UserManager.GetUserLoginInfo(User.Identity.Name).RoleID == 2)
            //{
            //    DistID = UserManager.GetUserLoginInfo(User.Identity.Name).DistID;
            //    //model.DistrictId = DistID;
            //}
            DataSet ds = DashBoardDB.GetProjectDetailsSectorWiseList(DistrictId, SectorTypeId, SectorID);
            if (ds != null && ds.Tables[0].Rows.Count > 0 && ds.Tables.Count > 0)
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    lst.Add(new DTO_SanctionCostAndProjectDetails
                    {
                        SectorID = (int)dr["SectorID"],
                        SectorTypeId = (int)dr["SectorTypeId"],
                        DistrictId = (int)dr["DistrictId"],
                        SectorType = dr["SectorType"].ToString(),
                        SectorName = dr["SectorName"].ToString(),
                        ProjectName = dr["ProjectName"].ToString(),
                        DistrictName = dr["DistrictName"].ToString(),
                        ProjectDescription = dr["ProjectDescription"].ToString(),
                        AgencyName = dr["AgencyName"].ToString(),
                        Status = dr["Status"].ToString(),
                        SanctionedProjectCost = Convert.ToDecimal(dr["SanctionedProjectCost"]),
                        AmountSpend = Convert.ToDecimal(dr["AmountSpend"]),
                    });
                    ViewBag.data = lst;
                }
            }
            return View();
        }
        #endregion

    }
}