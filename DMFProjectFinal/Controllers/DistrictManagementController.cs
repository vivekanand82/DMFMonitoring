using DMFProjectFinal.Models;
using DMFProjectFinal.Models.DTO;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace DMFProjectFinal.Controllers
{
    [SessionFilter]
    public class DistrictManagementController : Controller
    {
        // GET: DistrictManagement

        private dfm_dbEntities db = new dfm_dbEntities();
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ProjectProposalPrepration()
        {

            return View();
        }

        public ActionResult ViewProjectProposalPrepration(int? DistID, int? AgencyID, long? ProjectID, int? SectorID)



        {
            if (UserManager.GetUserLoginInfo(User.Identity.Name).RoleID == 4)
            {
                DistID = UserManager.GetUserLoginInfo(User.Identity.Name).DistID;
            }
            DTO_ProjectProposalPrepration model = new DTO_ProjectProposalPrepration();


            //var LstData = (from ppp in db.ProjectProposalPreprations
            //               join dm in db.DistrictMasters on ppp.DistID equals dm.DistrictId
            //               join ag in db.AgenciesInfoes on ppp.AgencyID equals ag.AgencyID
            //               join pm in db.ProjectMasters on ppp.ProjectID equals pm.ProjectID
            //               join psm in db.ProjectStatusMasters on ppp.ProjectStatusID equals psm.ProjectStatusID
            //               join sm in db.SectorNameMasters on ppp.SectorID equals sm.SectorNameId
            //               where ppp.IsActive == true
            //               && ppp.DistID == (DistID == null ? ppp.DistID : DistID)
            //               && ppp.AgencyID == (AgencyID == null ? ppp.AgencyID : AgencyID)
            //               && ppp.ProjectID == (ProjectID == null ? ppp.ProjectID : ProjectID)
            //               && ppp.SectorID == (SectorID == null ? ppp.SectorID : SectorID)
            //               select new DTO_ProjectProposalPrepration
            //               {
            //                   AgencyName = ag.Name,
            //                   DistrictName = dm.DistrictName,
            //                   GSTAndOthers = ppp.GSTAndOthers,
            //                   ProjectCost = ppp.ProjectCost,
            //                   ProjectName = pm.ProjectName,
            //                //   ProjectStatus = psm.ProjectStatus,
            //                   ProjectPreparationID = ppp.ProjectPreparationID.ToString(),
            //                   ProposalCopy = ppp.ProposalCopy,
            //                   ProposalDate = ppp.ProposalDate,
            //                   ProposedBy = ppp.ProposedBy,
            //                   ProsposalNo = ppp.ProsposalNo,
            //                   SectorName = sm.SectorName,
            //                   SanctionedProjectCost = ppp.SanctionedProjectCost,
            //                   TenderDate = ppp.TenderDate,
            //                   TenderNo = ppp.TenderNo,
            //                   WorkOrderDate = ppp.WorkOrderDate,
            //                   WorkOrderNo = ppp.WorkOrderNo
            //               }).ToList();
            var LstData = (from ppp in db.ProjectProposalPreprations
                           join dm in db.DistrictMasters on ppp.DistID equals dm.DistrictId
                           join tm in db.TehsilMasters on ppp.TehsilId equals tm.TehsilId
                           join bm in db.BlockMasters on ppp.BlockId equals bm.BlockId
                           join vm in db.VillageMasters on ppp.VillageId equals vm.VillageId
                           join snm in db.SectorNameMasters on ppp.SectorID equals snm.SectorNameId
                           join stm in db.SectorTypeMasters on ppp.SectorTypeId equals stm.SectorTypeID
                           join ag in db.AgenciesInfoes on ppp.AgencyID equals ag.AgencyID
                           // join pm in db.ProjectMasters on ppp.ProjectID equals pm.ProjectID
                           where ppp.IsActive == true
                           && ppp.DistID == (DistID == null ? ppp.DistID : DistID)
                           && ppp.AgencyID == (AgencyID == null ? ppp.AgencyID : AgencyID)
                           //&& ppp.ProjectID == (ProjectID == null ? ppp.ProjectID : ProjectID)
                           && ppp.SectorID == (SectorID == null ? ppp.SectorID : SectorID)
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
                               SanctionedProjectCost = ppp.SanctionedProjectCost,
                               TenderDate = ppp.TenderDate,
                               TenderNo = ppp.TenderNo,
                               WorkOrderDate = ppp.WorkOrderDate,
                               WorkOrderNo = ppp.WorkOrderNo,
                               Status = ppp.RunningStatus,
                               ProjectNo = ppp.ProjectNo



                           }).ToList();
            ViewBag.LstData = LstData;

            ViewBag.CommitteeID = new SelectList(db.CommitteeMasters.Where(x => x.IsActive == true && x.CommitteeTypeID == 1 && x.DistID == (DistID == null ? x.DistID : DistID)), "CommitteeID", "CommitteeName", null);



            return View(model);
        }

        [HttpPost]

        public ActionResult ViewProjectProposalPrepration(DTO_ProjectProposalPrepration model, HttpPostedFileBase MinutesofMeetingfile, HttpPostedFileBase Memberattendancefile, HttpPostedFileBase Approvelletterfile, List<string> CommitteeID, string Status)
        {
            JsonResponse JR = new JsonResponse();
            //if (!ModelState.IsValid)
            //{
            //    JR.Data = ModelState.Select(x => x.Value.Errors).Where(y => y.Count > 0).ToList();
            //    return Json(JR, JsonRequestBehavior.AllowGet);
            //}


            DTO_ProjectProposalPrepration obj = new DTO_ProjectProposalPrepration();
            ProjectMetting abc = new ProjectMetting();
            long _id = long.Parse(model.ProjectPreparationID);
            var Info = db.ProjectProposalPreprations.Where(x => x.ProjectPreparationID == _id).FirstOrDefault();

            Info.Stageid = 1;
            Info.RunningStatus = Status;
            Info.ModifyDate = DateTime.Now;
            Info.CreatedBy = UserManager.GetUserLoginInfo(User.Identity.Name).LoginID;

            //string Minutesfile = "";
            //string Memberfile = "";
            //string attendancefile = "";
            //string file1 = "";
            //string file2 = "";
            //string file3 = "";

            //HttpPostedFileBase postedFile = Request.Files["MinutesofMeetingfile"];


            //if (MinutesofMeetingfile.ContentLength > 0)

            //{


            //    Minutesfile = Path.GetFileName(MinutesofMeetingfile.FileName);
            //    file1 = Minutesfile + DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss");
            //    var path = Path.Combine(Server.MapPath("~/Documents"), file1);
            //    MinutesofMeetingfile.SaveAs(path);


            //}


            //if (Memberattendancefile.ContentLength > 0)

            //{


            //    Memberfile = Path.GetFileName(Memberattendancefile.FileName);
            //    file2 = Memberfile + DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss");
            //    var path1 = Path.Combine(Server.MapPath("~/Documents"), file2);
            //    Memberattendancefile.SaveAs(path1);


            //}







            //if (Approvelletterfile.ContentLength > 0)

            //{


            //    attendancefile = Path.GetFileName(Approvelletterfile.FileName);
            //    file3 = attendancefile + DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss");
            //    var path1 = Path.Combine(Server.MapPath("~/Documents"), file3);
            //    Approvelletterfile.SaveAs(path1);


            //}


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
            abc.Stageid = 1;
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
                //Response.Write("<script>alert('Data saved successfully');window.location.href='/ProjectWorkApproval/ProjectProposalPrepration'</script>");

                JR.IsSuccess = true;
                JR.Message = "Data Saved Successfully";
                JR.RedURL = "/DistrictManagement/ViewProjectProposalPrepration";
            }
            else
            {
                //  Response.Write("<script>alert('Error');window.location.href='/ProjectWorkApproval/CreateProjectProposalPrepration'</script>");
                JR.Message = "Some Error Occured, Contact to Admin";
            }



            //string MeetingNo = model.Meeti
            //string MeetingNo = model.Meeti
            //string MeetingNo = model.Meet




            //return new JsonResult { Data = JR, JsonRequestBehavior = JsonRequestBehavior.AllowGet, ContentType = "application/json", MaxJsonLength = Int32.MaxValue };
            return Json(JR, JsonRequestBehavior.AllowGet);
        }


        public JsonResult insertmeetingdata(DTO_ProjectProposalPrepration model,
  string lis)
        {

            JsonResponse JR = new JsonResponse();
            ProjectMetting abc = new ProjectMetting();
            JavaScriptSerializer js = new JavaScriptSerializer();


            List<DTO_ProjectProposalPrepration> list = js.Deserialize<List<DTO_ProjectProposalPrepration>>(lis);
            string memberlist = "";
            foreach (var item in list)
            {
                //if (s !="")
                //{
                //    memberlist += s + ",";


                //}
                memberlist += item.Memberlist + ",";

            }



            long _id = long.Parse(model.ProjectPreparationID);
            var Info = db.ProjectProposalPreprations.Where(x => x.ProjectPreparationID == _id).FirstOrDefault();







            Info.Stageid = 1;
            Info.RunningStatus = model.Status;
            Info.ModifyDate = DateTime.Now;
            Info.CreatedBy = UserManager.GetUserLoginInfo(User.Identity.Name).LoginID;


            abc.ProjectPreparationID = Convert.ToInt32(model.ProjectPreparationID);
            abc.MeetingNo = model.MeetingNo;
            abc.MettingDate = model.MettingDate;
            abc.IsActive = true;
            abc.MinutesofMeeting = model.MinutesofMeeting;
            //abc.MinutesofMeetingfile = model.MinutesofMeetingfile;
            //abc.Memberattendancefile = model.Memberattendancefile;
            //abc.Approvelletterfile = model.Approvelletterfile;

            abc.Attendancedate = model.Attendancedate;
            abc.Stageid = 1;
            abc.Memberlist = memberlist;
            abc.Status = model.Status;
            abc.CreatedBy = 1;
            abc.Createddate = DateTime.Now;
            abc.DistID = Info.DistID;
            abc.Remark = model.Remark;

            db.ProjectMettings.Add(abc);

            int res = db.SaveChanges();
            if (res > 0)
            {
                //Response.Write("<script>alert('Data saved successfully');window.location.href='/ProjectWorkApproval/ProjectProposalPrepration'</script>");

                JR.IsSuccess = true;
                JR.Message = "1";
                JR.RedURL = "/DistrictManagement/ViewProjectProposalPrepration";
            }
            else
            {
                //  Response.Write("<script>alert('Error');window.location.href='/ProjectWorkApproval/CreateProjectProposalPrepration'</script>");
                JR.Message = "Some Error Occured, Contact to Admin";
            }
            return Json(JR, JsonRequestBehavior.AllowGet);



        }

        [HttpPost]
        public ActionResult UploadMinutesofMeeting(int ProjectPreparationID)

        {

            if (Request.Files.Count > 0)
            {
                ProjectMetting objP = new ProjectMetting();

                objP = db.ProjectMettings.Where(x => x.ProjectPreparationID == ProjectPreparationID).FirstOrDefault();
                var info = db.ProjectProposalPreprations.Where(x => x.ProjectPreparationID == ProjectPreparationID).FirstOrDefault();
                HttpPostedFileBase mainPic = Request.Files[0];
                string fileExt = Path.GetExtension(mainPic.FileName);
                var id = Guid.NewGuid();
                string fName = info.ProjectNo + "_" + DateTime.Now.ToString("yyyy-dd-MM-HH-mm-ss") + id + fileExt;
                var path = Path.Combine(Server.MapPath("~/Documents"), fName);




                mainPic.SaveAs(path);


                objP.MinutesofMeetingfile = "/Documents/" + fName;
                db.SaveChanges();




            }
            else
            {
                //SaleEntry objP = new SaleEntry();
                //objP.SessionId = Convert.ToString(Session["SessionId"]);
                //objP.formno = formno;

                //objP.Action = "updtimg";
                //objP.photos = "../studentpic/userimg.jpg";
                //int r = objL.updateStudentImage(objP);

                //Common objP = new Common();

                //objP.UserContactno = MobileNo;


                //objP.Org_logo = "userimg.jpg";
                //DataTable dt = objL.InsertCandidate(objP, "sp_cat_Reg_Id");




            }


            if (Request.IsAjaxRequest())
            {
                return Json("", JsonRequestBehavior.AllowGet);
            }
            else
            {
                return View();
            }


            //return View();

        }
        [HttpPost]
        public ActionResult UploadMemberAttendances(int ProjectPreparationID)

        {

            if (Request.Files.Count > 0)
            {
                ProjectMetting objP = new ProjectMetting();

                objP = db.ProjectMettings.Where(x => x.ProjectPreparationID == ProjectPreparationID).FirstOrDefault();
                var info = db.ProjectProposalPreprations.Where(x => x.ProjectPreparationID == ProjectPreparationID).FirstOrDefault();
                HttpPostedFileBase mainPic = Request.Files[0];
                string fileExt = Path.GetExtension(mainPic.FileName);
                var id = Guid.NewGuid();
                string fName = info.ProjectNo + "_" + DateTime.Now.ToString("yyyy-dd-MM-HH-mm-ss") + id + fileExt;
                var path = Path.Combine(Server.MapPath("~/Documents"), fName);




                mainPic.SaveAs(path);


                objP.Memberattendancefile = "/Documents/" + fName;
                db.SaveChanges();




            }
            else
            {
                //SaleEntry objP = new SaleEntry();
                //objP.SessionId = Convert.ToString(Session["SessionId"]);
                //objP.formno = formno;

                //objP.Action = "updtimg";
                //objP.photos = "../studentpic/userimg.jpg";
                //int r = objL.updateStudentImage(objP);

                //Common objP = new Common();

                //objP.UserContactno = MobileNo;


                //objP.Org_logo = "userimg.jpg";
                //DataTable dt = objL.InsertCandidate(objP, "sp_cat_Reg_Id");




            }


            if (Request.IsAjaxRequest())
            {
                return Json("", JsonRequestBehavior.AllowGet);
            }
            else
            {
                return View();
            }


            //return View();

        }



        [HttpPost]
        public ActionResult UploadApprovalletter(int ProjectPreparationID)

        {

            if (Request.Files.Count > 0)
            {
                ProjectMetting objP = new ProjectMetting();

                objP = db.ProjectMettings.Where(x => x.ProjectPreparationID == ProjectPreparationID).FirstOrDefault();
                var info = db.ProjectProposalPreprations.Where(x => x.ProjectPreparationID == ProjectPreparationID).FirstOrDefault();
                var id = Guid.NewGuid();
                HttpPostedFileBase mainPic = Request.Files[0];
                string fileExt = Path.GetExtension(mainPic.FileName);
                string fName = info.ProjectNo + "_" + DateTime.Now.ToString("yyyy-dd-MM-HH-mm-ss") + id + fileExt;
                var path = Path.Combine(Server.MapPath("~/Documents"), fName);




                mainPic.SaveAs(path);


                objP.Approvelletterfile = "/Documents/" + fName;
                db.SaveChanges();




            }
            else
            {
                //SaleEntry objP = new SaleEntry();
                //objP.SessionId = Convert.ToString(Session["SessionId"]);
                //objP.formno = formno;

                //objP.Action = "updtimg";
                //objP.photos = "../studentpic/userimg.jpg";
                //int r = objL.updateStudentImage(objP);

                //Common objP = new Common();

                //objP.UserContactno = MobileNo;


                //objP.Org_logo = "userimg.jpg";
                //DataTable dt = objL.InsertCandidate(objP, "sp_cat_Reg_Id");




            }


            if (Request.IsAjaxRequest())
            {
                return Json("", JsonRequestBehavior.AllowGet);
            }
            else
            {
                return View();
            }


            //return View();

        }














        public JsonResult GetProjectDetailes(long ProjectId)
        {

            var LstData = (from ppp in db.ProjectProposalPreprations
                           join dm in db.DistrictMasters on ppp.DistID equals dm.DistrictId
                           join tm in db.TehsilMasters on ppp.TehsilId equals tm.TehsilId
                           join bm in db.BlockMasters on ppp.BlockId equals bm.BlockId
                           join vm in db.VillageMasters on ppp.VillageId equals vm.VillageId
                           join snm in db.SectorNameMasters on ppp.SectorID equals snm.SectorNameId
                           join stm in db.SectorTypeMasters on ppp.SectorTypeId equals stm.SectorTypeID
                           join ag in db.AgenciesInfoes on ppp.AgencyID equals ag.AgencyID
                           //join pm in db.ProjectMasters on ppp.ProjectID equals pm.ProjectID
                           where

                            ppp.ProjectPreparationID == ProjectId

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
                               SanctionedProjectCost = ppp.SanctionedProjectCost,
                               TenderDate = ppp.TenderDate,
                               TenderNo = ppp.TenderNo,
                               WorkOrderDate = ppp.WorkOrderDate,
                               WorkOrderNo = ppp.WorkOrderNo,
                               TehsilName = tm.TehsilName,
                               VillageNameInHindi = vm.VillageNameInHindi,
                               BlockName = bm.BlockName,
                               WorkOrderCopy = ppp.WorkOrderCopy,

                               SectorType = stm.SectorType,
                               ProjectDescription = ppp.ProjectDescription,
                               WorkLatitude = ppp.WorkLatitude,
                               WorkLongitude = ppp.WorkLongitude,




                           }).ToList();

            //demo





            return Json(LstData, JsonRequestBehavior.AllowGet);
        }


    }
}