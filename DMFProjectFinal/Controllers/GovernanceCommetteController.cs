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
        public ActionResult ViewProjectProposals(int? DistID, int? AgencyID, int? SectorID , string SectorType, string SectorName, string DistrictName, string ProjectName)
        {
            ViewBag.SectorType = new SelectList(db.SectorTypeMasters, "SectorType", "SectorType", null);
            ViewBag.SectorName = new SelectList(db.SectorNameMasters, "SectorName", "SectorName", null);
            if (UserManager.GetUserLoginInfo(User.Identity.Name).RoleID == 5)
            {
                DistID = UserManager.GetUserLoginInfo(User.Identity.Name).DistID;
                var district = db.DistrictMasters.Where(x => x.DistrictId == DistID).FirstOrDefault();
                ViewBag.DistrictName = new SelectList(db.DistrictMasters.Where(x => x.DistrictName == district.DistrictName), "DistrictName", "DistrictName", district.DistrictName);
                ViewBag.ProjectName = new SelectList(db.ProjectProposalPreprations.Where(x => x.DistID == DistID && !String.IsNullOrEmpty(x.ProjectName)), "ProjectName", "ProjectName", null);

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
                             && (stm.SectorType == SectorType || String.IsNullOrEmpty(SectorType))
                            && (snm.SectorName == SectorName || String.IsNullOrEmpty(SectorName))
                            && (dm.DistrictName == DistrictName || String.IsNullOrEmpty(DistrictName))
                            && (ppp.ProjectName == ProjectName || String.IsNullOrEmpty(ProjectName))
                            //&& (ppp.RunningStatus == RunningStatus || String.IsNullOrEmpty(RunningStatus))
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
                ViewBag.DistrictName = new SelectList(db.DistrictMasters, "DistrictName", "DistrictName", null);
                ViewBag.ProjectName = new SelectList(db.ProjectProposalPreprations.Where(x=> !String.IsNullOrEmpty(x.ProjectName)), "ProjectName", "ProjectName", null);
                var LstData = (from ppp in db.ProjectProposalPreprations
                               join dm in db.DistrictMasters on ppp.DistID equals dm.DistrictId
                               join tm in db.TehsilMasters on ppp.TehsilId equals tm.TehsilId
                               join bm in db.BlockMasters on ppp.BlockId equals bm.BlockId
                               join vm in db.VillageMasters on ppp.VillageId equals vm.VillageId
                               join snm in db.SectorNameMasters on ppp.SectorID equals snm.SectorNameId
                               join stm in db.SectorTypeMasters on ppp.SectorTypeId equals stm.SectorTypeID
                               join ag in db.AgenciesInfoes on ppp.AgencyID equals ag.AgencyID
                               where ppp.IsActive == true
                              && (stm.SectorType == SectorType || String.IsNullOrEmpty(SectorType))
                            && (snm.SectorName == SectorName || String.IsNullOrEmpty(SectorName))
                            && (dm.DistrictName == DistrictName || String.IsNullOrEmpty(DistrictName))
                            && (ppp.ProjectName == ProjectName || String.IsNullOrEmpty(ProjectName))
                             //&& (ppp.RunningStatus == RunningStatus || String.IsNullOrEmpty(RunningStatus))
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

        public JsonResult BindSector(string SectorType)
        {
            var sectortypeID = db.SectorTypeMasters.Where(x => x.SectorType == SectorType).FirstOrDefault() ?? new SectorTypeMaster();

            var data = (from st in db.SectorNameMasters
                        where st.SectorTypeId == sectortypeID.SectorTypeID
                        select new DTO_SectorNameMaster
                        {
                            SectorName = st.SectorName
                        }).ToList();

            return Json(data, JsonRequestBehavior.AllowGet);
        }
        public JsonResult BindProject(string SectorType, string SectorName, string DistrictName)
        {
            var sectorID = db.SectorNameMasters.Where(x => x.SectorName == SectorName).FirstOrDefault() ?? new SectorNameMaster();
            var SectorTypeId = db.SectorTypeMasters.Where(x => x.SectorType == SectorType).FirstOrDefault() ?? new SectorTypeMaster();
            var District = db.DistrictMasters.Where(x => x.DistrictName == DistrictName).FirstOrDefault() ?? new DistrictMaster();

            var data = (from ppp in db.ProjectProposalPreprations
                        where !String.IsNullOrEmpty(ppp.ProjectName) &&  ppp.SectorTypeId == (SectorTypeId.SectorTypeID == 0 ? ppp.SectorTypeId : SectorTypeId.SectorTypeID)
                        && ppp.SectorID == (sectorID.SectorNameId == 0 ? ppp.SectorID : sectorID.SectorNameId)
                        && ppp.DistID == (District.DistrictId == 0 ? ppp.DistID : District.DistrictId)
                        select new DTO_ProjectProposalPrepration
                        {
                            ProjectName = ppp.ProjectName

                        }).ToList();

            return Json(data, JsonRequestBehavior.AllowGet);
        }
    }
}