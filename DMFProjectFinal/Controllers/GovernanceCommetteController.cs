using DMFProjectFinal.Models;
using DMFProjectFinal.Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DMFProjectFinal.Controllers
{
    [SessionFilter]   
    public class GovernanceCommetteController : Controller
    {
        private dfm_dbEntities db = new dfm_dbEntities();
        public ActionResult Index()//coment
        {
            return View();
        }
        public ActionResult ViewProjectProposals(int? DistID, int? AgencyID, int? SectorID , string ProjectName, string SectorName, string SectorType)
        {
            if (UserManager.GetUserLoginInfo(User.Identity.Name).RoleID == 5)
            {
                DistID = UserManager.GetUserLoginInfo(User.Identity.Name).DistID;
                var LstData = (from ppp in db.ProjectProposalPreprations
                               join dm in db.DistrictMasters on ppp.DistID equals dm.DistrictId
                               join tm in db.TehsilMasters on ppp.TehsilId equals tm.TehsilId
                               join bm in db.BlockMasters on ppp.BlockId equals bm.BlockId
                               join vm in db.VillageMasters on ppp.VillageId equals vm.VillageId
                               join snm in db.SectorNameMasters on ppp.SectorID equals snm.SectorNameId
                               join stm in db.SectorTypeMasters on ppp.SectorTypeId equals stm.SectorTypeID
                               join ag in db.AgenciesInfoes on ppp.AgencyID equals ag.AgencyID
                               where ppp.IsActive == true
                               && ppp.DistID == (DistID == null ? ppp.DistID : DistID)
                               && ppp.AgencyID == (AgencyID == null ? ppp.AgencyID : AgencyID)
                               && ppp.SectorID == (SectorID == null ? ppp.SectorID : SectorID)
                             && (ppp.ProjectName.StartsWith(ProjectName) || String.IsNullOrEmpty(ProjectName))
                             && (snm.SectorName.StartsWith(SectorName) || String.IsNullOrEmpty(SectorName))
                             && (stm.SectorType.StartsWith(SectorType) || String.IsNullOrEmpty(SectorType))
                               select new DTO_ProjectProposalPrepration
                               {
                                   AgencyName = ag.Name,
                                   DistrictName = dm.DistrictName,
                                   GSTAndOthers = ppp.GSTAndOthers,
                                   ProjectCost = ppp.ProjectCost,
                                   ProjectName = ppp.ProjectName,
                                   //   ProjectStatus = psm.ProjectStatus,
                                   ProjectPreparationID = ppp.ProjectPreparationID.ToString(),
                                   ProposalCopy = ppp.ProposalCopy,
                                   ProposalDate = ppp.ProposalDate,
                                   ProposedBy = ppp.ProposedBy,
                                   ProsposalNo = ppp.ProsposalNo,
                                   SectorName = snm.SectorName,
                                   SectorType=stm.SectorType,
                                   SanctionedProjectCost = ppp.SanctionedProjectCost,
                                   TenderDate = ppp.TenderDate,
                                   TenderNo = ppp.TenderNo,
                                   WorkOrderDate = ppp.WorkOrderDate,
                                   WorkOrderNo = ppp.WorkOrderNo,
                                   RunningStatus = ppp.RunningStatus,
                                   FinalStatus = ppp.FinalStatus,
                                   Stageid=ppp.Stageid
                               }).ToList();
                    ViewBag.LstData = LstData;
            }
            else
            {
                var LstData = (from ppp in db.ProjectProposalPreprations
                               join dm in db.DistrictMasters on ppp.DistID equals dm.DistrictId
                               join tm in db.TehsilMasters on ppp.TehsilId equals tm.TehsilId
                               join bm in db.BlockMasters on ppp.BlockId equals bm.BlockId
                               join vm in db.VillageMasters on ppp.VillageId equals vm.VillageId
                               join snm in db.SectorNameMasters on ppp.SectorID equals snm.SectorNameId
                               join stm in db.SectorTypeMasters on ppp.SectorTypeId equals stm.SectorTypeID
                               join ag in db.AgenciesInfoes on ppp.AgencyID equals ag.AgencyID
                               where ppp.IsActive == true 
                               && (ppp.ProjectName.StartsWith(ProjectName) || String.IsNullOrEmpty(ProjectName))
                             && (snm.SectorName.StartsWith(SectorName) || String.IsNullOrEmpty(SectorName))
                             && (stm.SectorType.StartsWith(SectorType) || String.IsNullOrEmpty(SectorType))
                               select new DTO_ProjectProposalPrepration
                               {
                                   AgencyName = ag.Name,
                                   DistrictName = dm.DistrictName,
                                   GSTAndOthers = ppp.GSTAndOthers,
                                   ProjectCost = ppp.ProjectCost,
                                   ProjectName = ppp.ProjectName,
                                   //   ProjectStatus = psm.ProjectStatus,
                                   ProjectPreparationID = ppp.ProjectPreparationID.ToString(),
                                   ProposalCopy = ppp.ProposalCopy,
                                   ProposalDate = ppp.ProposalDate,
                                   ProposedBy = ppp.ProposedBy,
                                   ProsposalNo = ppp.ProsposalNo,
                                   SectorName = snm.SectorName,
                                   SectorType = stm.SectorType,
                                   SanctionedProjectCost = ppp.SanctionedProjectCost,
                                   TenderDate = ppp.TenderDate,
                                   TenderNo = ppp.TenderNo,
                                   WorkOrderDate = ppp.WorkOrderDate,
                                   WorkOrderNo = ppp.WorkOrderNo,
                                   RunningStatus = ppp.RunningStatus,
                                   FinalStatus = ppp.FinalStatus,
                                   Stageid = ppp.Stageid
                               }).ToList();
                ViewBag.LstData = LstData;
            }

            return View();
        }
        [HttpGet]
        public ActionResult ViewDetailProjectProposal(string id)//added by ramdhyan 05.04.2024 for view details on the modal popup usinf project preparation id
        {
            if (String.IsNullOrEmpty(id))
            {
                return RedirectToAction("Login", "Account");
            }
            try
            {
                long _id = long.Parse(id);
                //long _id = long.Parse(id);  use this code when u use server side datatable for display
                var Info = db.ProjectProposalPreprations.Where(x => x.ProjectPreparationID == _id).FirstOrDefault();
                if (Info != null)
                {
                    int? DistID = null;
                    if (UserManager.GetUserLoginInfo(User.Identity.Name).RoleID == 5)
                    {
                        DistID = UserManager.GetUserLoginInfo(User.Identity.Name).DistID;
                        var LstData = (from ppp in db.ProjectProposalPreprations
                                       join dm in db.DistrictMasters on ppp.DistID equals dm.DistrictId
                                       join tm in db.TehsilMasters on ppp.TehsilId equals tm.TehsilId
                                       join bm in db.BlockMasters on ppp.BlockId equals bm.BlockId
                                       join vm in db.VillageMasters on ppp.VillageId equals vm.VillageId
                                       join snm in db.SectorNameMasters on ppp.SectorID equals snm.SectorNameId
                                       join stm in db.SectorTypeMasters on ppp.SectorTypeId equals stm.SectorTypeID
                                       join ag in db.AgenciesInfoes on ppp.AgencyID equals ag.AgencyID
                                       //join pm in db.ProposalStatusMasters on ppp.ProjectPreparationID equals pm.ProjectID
                                       where ppp.IsActive == true &&  ppp.ProjectPreparationID == Info.ProjectPreparationID && ppp.DistID==DistID
                                       select new DTO_ProjectProposalPrepration
                                       {
                                           DistrictName = dm.DistrictName,
                                           TehsilName = tm.TehsilName,
                                           BlockName = bm.BlockName,
                                           VillageNameInEnglish = vm.VillageNameInEnglish,
                                           VillageNameInHindi = vm.VillageNameInHindi,
                                           SectorType = stm.SectorType,
                                           SectorName = snm.SectorName,
                                           GSTAndOthers = ppp.GSTAndOthers,
                                           ProjectCost = ppp.ProjectCost,
                                           ProjectName = ppp.ProjectName,
                                           WorkLatitude = ppp.WorkLatitude,
                                           WorkLongitude = ppp.WorkLongitude,
                                           ProjectDescription = ppp.ProjectDescription,
                                           //   ProjectStatus = psm.ProjectStatus,
                                           ProjectPreparationID = ppp.ProjectPreparationID.ToString(),
                                           ProposalCopy = ppp.ProposalCopy,
                                           ProposalDate = ppp.ProposalDate,
                                           ProposedBy = ppp.ProposedBy,
                                           ProsposalNo = ppp.ProsposalNo,
                                           TenderDate = ppp.TenderDate,
                                           TenderNo = ppp.TenderNo,
                                           WorkOrderDate = ppp.WorkOrderDate,
                                           WorkOrderNo = ppp.WorkOrderNo,
                                           WorkOrderCopy = ppp.WorkOrderCopy,
                                           AgencyName = ag.Name,
                                           SanctionedProjectCost = ppp.SanctionedProjectCost
                                       }).ToList();

                        return Json(LstData, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        var LstData = (from ppp in db.ProjectProposalPreprations
                                       join dm in db.DistrictMasters on ppp.DistID equals dm.DistrictId
                                       join tm in db.TehsilMasters on ppp.TehsilId equals tm.TehsilId
                                       join bm in db.BlockMasters on ppp.BlockId equals bm.BlockId
                                       join vm in db.VillageMasters on ppp.VillageId equals vm.VillageId
                                       join snm in db.SectorNameMasters on ppp.SectorID equals snm.SectorNameId
                                       join stm in db.SectorTypeMasters on ppp.SectorTypeId equals stm.SectorTypeID
                                       join ag in db.AgenciesInfoes on ppp.AgencyID equals ag.AgencyID
                                       //join pm in db.ProposalStatusMasters on ppp.ProjectPreparationID equals pm.ProjectID
                                       where ppp.IsActive == true && ppp.ProjectPreparationID == Info.ProjectPreparationID 
                                       select new DTO_ProjectProposalPrepration
                                       {
                                           DistrictName = dm.DistrictName,
                                           TehsilName = tm.TehsilName,
                                           BlockName = bm.BlockName,
                                           VillageNameInEnglish = vm.VillageNameInEnglish,
                                           VillageNameInHindi = vm.VillageNameInHindi,
                                           SectorType = stm.SectorType,
                                           SectorName = snm.SectorName,
                                           GSTAndOthers = ppp.GSTAndOthers,
                                           ProjectCost = ppp.ProjectCost,
                                           ProjectName = ppp.ProjectName,
                                           WorkLatitude = ppp.WorkLatitude,
                                           WorkLongitude = ppp.WorkLongitude,
                                           ProjectDescription = ppp.ProjectDescription,
                                           //   ProjectStatus = psm.ProjectStatus,
                                           ProjectPreparationID = ppp.ProjectPreparationID.ToString(),
                                           ProposalCopy = ppp.ProposalCopy,
                                           ProposalDate = ppp.ProposalDate,
                                           ProposedBy = ppp.ProposedBy,
                                           ProsposalNo = ppp.ProsposalNo,
                                           TenderDate = ppp.TenderDate,
                                           TenderNo = ppp.TenderNo,
                                           WorkOrderDate = ppp.WorkOrderDate,
                                           WorkOrderNo = ppp.WorkOrderNo,
                                           WorkOrderCopy = ppp.WorkOrderCopy,
                                           AgencyName = ag.Name,
                                           SanctionedProjectCost = ppp.SanctionedProjectCost
                                       }).ToList();

                        return Json(LstData, JsonRequestBehavior.AllowGet);
                    }
                }
                else
                {
                    return RedirectToAction("Login", "Account");
                }
            }
            catch (Exception ex)
            {
                return RedirectToAction("Login", "Account");
            }
        }
            public JsonResult MeetingInfo(string ProjectPreparationId)
            {
            //CommitteeMaster membernames =new  CommitteeMaster();
            string MemberDemo = string.Empty;
            var id = Convert.ToInt32(ProjectPreparationId);
                ProjectMetting obj = new ProjectMetting();
                var data = db.ProjectMettings.Where(x => x.ProjectPreparationID == id).FirstOrDefault();
            if (data != null)
            {
                string[] Members = data.Memberlist.TrimEnd().Split(',');
                foreach (var item in Members)
                {
                    if (item !="")
                    {
                        int CommitteeID = int.Parse(item);
                        CommitteeMaster membernames = db.CommitteeMasters.Where(x => x.CommitteeID == CommitteeID).FirstOrDefault();
                        MemberDemo += membernames.CommitteeName+",";
                    }
                }

                //ViewBag.memberlists = MemberDemo;
                data.Memberlist = MemberDemo;


            }
            return Json(data, JsonRequestBehavior.AllowGet);
            }
        [HttpPost]
        public ActionResult SaveMeetingInfo(DTO_ProjectProposalPrepration model, HttpPostedFileBase MinutesofMeetingfile, HttpPostedFileBase Memberattendancefile, HttpPostedFileBase Approvelletterfile, List<string> CommitteeID, string Status)
        {
            JsonResponse JR = new JsonResponse();
            DTO_ProjectProposalPrepration obj = new DTO_ProjectProposalPrepration();
            ProjectMetting abc = new ProjectMetting();
            long _id = long.Parse(model.ProjectPreparationID);
            var Info = db.ProjectProposalPreprations.Where(x => x.ProjectPreparationID == _id).FirstOrDefault();
            Info.Stageid = 2;
            Info.RunningStatus = Status;
            Info.ModifyDate = DateTime.Now;
            Info.CreatedBy = UserManager.GetUserLoginInfo(User.Identity.Name).LoginID;

            if (!String.IsNullOrEmpty(model.MinutesofMeetingfile))
            {
                model.MinutesofMeetingfile = BusinessLogics.UploadFileDMF(model.MinutesofMeetingfile);
                if (model.MinutesofMeetingfile.Contains("Expp::"))
                {
                    JR.Message = model.MinutesofMeetingfile;
                    return Json(JR, JsonRequestBehavior.AllowGet);
                }
            }
            if (!String.IsNullOrEmpty(model.Memberattendancefile))
            {
                model.Memberattendancefile = BusinessLogics.UploadFileDMF(model.Memberattendancefile);
                if (model.Memberattendancefile.Contains("Expp::"))
                {
                    JR.Message = model.Memberattendancefile;
                    return Json(JR, JsonRequestBehavior.AllowGet);
                }
            }

            if (!String.IsNullOrEmpty(model.Approvelletterfile))
            {
                model.Approvelletterfile = BusinessLogics.UploadFileDMF(model.Approvelletterfile);
                if (model.Approvelletterfile.Contains("Expp::"))
                {
                    JR.Message = model.Approvelletterfile;
                    return Json(JR, JsonRequestBehavior.AllowGet);
                }
            }
            string memberlist = "";
            foreach (string s in CommitteeID)
            {
                if (s != null)
                {
                    memberlist += s + ",";
                }
            }
            abc.ProjectPreparationID = Convert.ToInt32(model.ProjectPreparationID);
            abc.MeetingNo = model.MeetingNo;
            abc.MettingDate = model.MettingDate;
            abc.IsActive = true;
            abc.MinutesofMeeting = model.MinutesofMeeting;
            abc.MinutesofMeetingfile = model.MinutesofMeetingfile;
            abc.Memberattendancefile = model.Memberattendancefile;
            abc.Approvelletterfile = model.Approvelletterfile;
            abc.Attendancedate = model.Attendancedate;
            abc.Stageid = 2;
            abc.Memberlist = memberlist;
            abc.Status = Status;
            abc.CreatedBy = 1;
            abc.Createddate = DateTime.Now;
            abc.DistID = Info.DistID;
            abc.Remark = model.Remark;
            db.ProjectMettings.Add(abc);
            int res = db.SaveChanges();
            if (res > 0)
            {
                JR.IsSuccess = true;
                JR.Message = "Data Saved Successfully";
                JR.RedURL = "/GovernanceCommette/ViewProjectProposals";
            }
            else
            {
                JR.Message = "Some Error Occured, Contact to Admin";
            }
              return Json(JR, JsonRequestBehavior.AllowGet);
        }//bfdjkfdhjfkdsfsd
        public JsonResult CommetteDropDown(int id)
        {
            db.Configuration.ProxyCreationEnabled = false;
            var data = db.ProjectProposalPreprations.Where(x => x.ProjectPreparationID == id).FirstOrDefault().DistID;
            var commetedata = db.CommitteeMasters.Where(x => x.CommitteeTypeID == 2 && x.DistID == data).ToList();
            return Json(commetedata, JsonRequestBehavior.AllowGet);
        }
    }
}