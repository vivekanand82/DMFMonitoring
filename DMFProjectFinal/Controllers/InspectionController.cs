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
    public class InspectionController : Controller
    {
        // GET: Inspection
     
        private dfm_dbEntities db = new dfm_dbEntities();
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ChecklistInspection(int? DistID)
        {
            if (UserManager.GetUserLoginInfo(User.Identity.Name).RoleID == 2)
            {
                DistID = UserManager.GetUserLoginInfo(User.Identity.Name).DistID;
            }

            InspectionCheckListQuestionMaster abc = new InspectionCheckListQuestionMaster();

            DTO_InspectionCheckListQuestionMaster model = new DTO_InspectionCheckListQuestionMaster();
            var list = (from ab in db.InspectionCheckListQuestionMasters
                        select new DTO_InspectionCheckListQuestionMaster
                        {
                            InspectionCheckListQuestionID = ab.InspectionCheckListQuestionID,
                            InspectionQuestion = ab.InspectionQuestion
                        }
                ).Distinct().ToList();

            ViewBag.msg = list;
         
            return View(model);
        }


        public ActionResult DeleteData(int? InspectionCheckListQuestionID)
        {
            var list = db.InspectionCheckListQuestionMasters.Where(x => x.InspectionCheckListQuestionID == InspectionCheckListQuestionID).FirstOrDefault();
            db.InspectionCheckListQuestionMasters.Remove(list);
            db.SaveChanges();
            Response.Redirect("/Inspection/ChecklistInspection");

            return View();
        }



        public JsonResult Addanswer(string lis, string InspectionQuestion)
        {
            int res = 0;
            JsonResponse JR = new JsonResponse();
            JavaScriptSerializer js = new JavaScriptSerializer();
            List<DTO_InspectionCheckListAnswerMaster> list = js.Deserialize<List<DTO_InspectionCheckListAnswerMaster>>(lis);

            try
            {
                foreach (var item in list)
                {
                    InspectionCheckListAnswerMaster obj = new InspectionCheckListAnswerMaster();
                    obj.InspectionAnswer = item.InspectionAnswer;
                    obj.InspectionQuestion =InspectionQuestion;
                    db.InspectionCheckListAnswerMasters.Add(obj);
                    obj.CreatedBy = "1";
                    obj.CreatedDate = DateTime.Now;

                }
                res = db.SaveChanges();

                if (res > 0)
                {


                    JR.IsSuccess = true;
                    JR.Message = "1";
                    //JR.RedURL = "/ProjectWorkApproval/ProjectProposalPrepration";
                }
                else if (res == 0)
                {
                    JR.IsSuccess = true;
                    JR.Message = "0";

                }

                else
                {

                    JR.Message = "Some Error Occured, Contact to Admin";
                }




            }

            catch (Exception ex)
            {

                JR.Message = "Some Error Occured, Contact to Admin";
            }

            return Json(JR, JsonRequestBehavior.AllowGet);

        }

        public JsonResult Addqustion(string lis)
        {
            int res = 0;
            JsonResponse JR = new JsonResponse();
            JavaScriptSerializer js = new JavaScriptSerializer();
            List<DTO_InspectionCheckListQuestionMaster> list = js.Deserialize<List<DTO_InspectionCheckListQuestionMaster>>(lis);

            try
            {
                foreach (var item in list)
                {
                    InspectionCheckListQuestionMaster obj = new InspectionCheckListQuestionMaster();
                    obj.InspectionQuestion = item.InspectionQuestion;

                    if(item.IsChecked=="1")
                    {
                        obj.IsChecked = true;

                    }
                    else
                    {

                        obj.IsChecked = false;
                    }
                    obj.CreatedBy = "1";
                    obj.CreatedDate = DateTime.Now;


                    db.InspectionCheckListQuestionMasters.Add(obj);
                    res = db.SaveChanges();
                }
                //res = db.SaveChanges();

                if (res > 0)
                {


                    JR.IsSuccess = true;
                    JR.Message = "1";
                    //JR.RedURL = "/ProjectWorkApproval/ProjectProposalPrepration";
                }
                else if (res == 0)
                {
                    JR.IsSuccess = true;
                    JR.Message = "0";

                }

                else
                {

                    JR.Message = "Some Error Occured, Contact to Admin";
                }




            }

            catch (Exception ex)
            {

                JR.Message = "Some Error Occured, Contact to Admin";
            }

            return Json(JR, JsonRequestBehavior.AllowGet);

        }



        public ActionResult ViewInspectionProject(int? DistID, int? AgencyID, long? ProjectID, int? SectorID, int? SectorTypeId)
        {
            if (UserManager.GetUserLoginInfo(User.Identity.Name).RoleID == 2)
            {
                DistID = UserManager.GetUserLoginInfo(User.Identity.Name).DistID;
            }

            DTO_ProjectProposalPrepration model = new DTO_ProjectProposalPrepration();
            var LstData = (from ppp in db.ProjectProposalPreprations
                           join dm in db.DistrictMasters on ppp.DistID equals dm.DistrictId
                           join tm in db.TehsilMasters on ppp.TehsilId equals tm.TehsilId
                           join bm in db.BlockMasters on ppp.BlockId equals bm.BlockId
                           join vm in db.VillageMasters on ppp.VillageId equals vm.VillageId
                           join snm in db.SectorNameMasters on ppp.SectorID equals snm.SectorNameId
                           join stm in db.SectorTypeMasters on ppp.SectorTypeId equals stm.SectorTypeID
                           join ag in db.AgenciesInfoes on ppp.AgencyID equals ag.AgencyID
                          
                           join u in db.UtilizationMasters on ppp.ProjectPreparationID equals
                           u.ProjectPreparationID
                           where ppp.IsActive==true && ppp.Stageid==2
                           && ppp.DistID == (DistID == null ? ppp.DistID : DistID)
                           && ppp.AgencyID == (AgencyID == null ? ppp.AgencyID : AgencyID)
                           //&& ppp.ProjectID == (ProjectID == null ? ppp.ProjectID : ProjectID)
                            && ppp.SectorTypeId == (SectorTypeId == null ? ppp.SectorTypeId : SectorTypeId)
                           && ppp.SectorID == (SectorID == null ? ppp.SectorID : SectorID)



                           //where u.inspectionflag == null 


                           /*ppp.IsActive == true*/ /*&& ppp.Stageid == 2*/

                           //&& u.Phyicalintsallmentflag == "1" || u.Phyicalintsallmentflag == "2" ||
                           //u.Phyicalintsallmentflag == "3" || u.Phyicalintsallmentflag == "4"

                           //&& ppp.DistID == (DistID == null ? ppp.DistID : DistID)
                           //&& ppp.AgencyID == (AgencyID == null ? ppp.AgencyID : AgencyID)
                           ////&& ppp.ProjectID == (ProjectID == null ? ppp.ProjectID : ProjectID)
                           // && ppp.SectorTypeId == (SectorTypeId == null ? ppp.SectorTypeId : SectorTypeId)
                           //&& ppp.SectorID == (SectorID == null ? ppp.SectorID : SectorID)
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
                               ProjectNo = ppp.ProjectNo,
                               SectorType = stm.SectorType



                           }).Distinct().ToList();
            ViewBag.LstData = LstData;

            ViewBag.CommitteeID = new SelectList(db.CommitteeMasters.Where(x => x.IsActive == true && x.CommitteeTypeID == 1 && x.DistID == (DistID == null ? x.DistID : DistID)), "CommitteeID", "CommitteeName", null);

            ViewBag.ProjectPreparationID = new SelectList((from ppp in db.ProjectProposalPreprations


                                                               //join pm in db.ProposalStatusMasters on ppp.ProjectPreparationID equals pm.ProjectID
                                                           where ppp.IsActive == true && ppp.Stageid == 2
                                                           && ppp.DistID == (DistID == null ? ppp.DistID : DistID)

                                                           select new DTO_MileStoneMaster
                                                           {
                                                               ProjectPreparationID = ppp.ProjectPreparationID,

                                                               ProjectNo = ppp.ProjectNo + " / " + ppp.ProjectName


                                                           }).Distinct().ToList(), "ProjectPreparationID", "ProjectNo", null);





            ViewBag.DistID = new SelectList(db.DistrictMasters.Where(x => x.IsActive == true && x.DistrictId == (DistID == null ? x.DistrictId : DistID)), "DistrictId", "DistrictName", null);

            ViewBag.InstallmentID = new SelectList((from inst in db.InstallmentMasters
                                                    select new DTO_MileStoneMaster
                                                    {
                                                        InstallmentID = inst.InstallmentID,
                                                        InstallmentName = inst.InstallmentName

                                                    }).Distinct().ToList(), "InstallmentID", "InstallmentName", null);


            ViewBag.SectorTypeId = new SelectList(db.SectorTypeMasters.Where(x => x.IsActive == true), "SectorTypeId", "SectorType", null).ToList();
            ViewBag.SectorID = new SelectList(db.SectorNameMasters.Where(x => x.IsActive == true), "SectorNameId", "SectorName", null).ToList();


            return View(model);
        }


        public JsonResult listbindQustion()
        {

            var list = (from inst in db.InspectionCheckListQuestionMasters
                        select new DTO_InspectionCheckListQuestionMaster
                        {
                            InspectionCheckListQuestionID = inst.InspectionCheckListQuestionID,
                            InspectionQuestion = inst.InspectionQuestion


                        }
            ).Distinct().ToList();
            return Json(list, JsonRequestBehavior.AllowGet);

           
        }

        public JsonResult listbindanswer(int ProjectPreparationID)
        {

            var list = (from inst in db.InspectionCheckListAnswerMasters 
                        join u in db.UtilizationMasters on  inst.ProjectPreparationID equals
                        u.ProjectPreparationID
                        where inst.ProjectPreparationID == ProjectPreparationID

                     && u.inspectionflag!=null
                        select new DTO_InspectionCheckListAnswerMaster
                        {
                            InspectionQuestionID = inst.InspectionQuestionID,

                            InspectionQuestion = inst.InspectionQuestion,
                            InspectionAnswer = inst.InspectionAnswer,
                            Remark = inst.Remark,
                            InstallentId=u.inspectionflag
                           



                        }
            ).Distinct().ToList();

            list = list.OrderByDescending(x => x.InstallentId).ToList();

            return Json(list, JsonRequestBehavior.AllowGet);


        }



        public JsonResult InsertRecord(string lis, int Districtid, int ProjectPreparationID)
        {

            JsonResponse JR = new JsonResponse();
            int res = 0;
            JavaScriptSerializer js = new JavaScriptSerializer();
            DTO_InspectionCheckListAnswerMaster model = new DTO_InspectionCheckListAnswerMaster();
            List<DTO_InspectionCheckListAnswerMaster> list = js.Deserialize<List<DTO_InspectionCheckListAnswerMaster>>(lis);
            try
            {
                foreach (var item in list)
                {
                    InspectionCheckListAnswerMaster ax = new InspectionCheckListAnswerMaster();
                    ax.ProjectPreparationID = Convert.ToInt32(ProjectPreparationID);
                    ProjectProposalPrepration abc = db.ProjectProposalPreprations.Where(x => x.ProjectPreparationID == ProjectPreparationID).FirstOrDefault();
                    model.ProjectNo = abc.ProjectNo;

                    ax.ProjectNo = model.ProjectNo;
                    ax.Districtid =Districtid;
                    ax.ProjectPreparationID = ProjectPreparationID;
                    ax.InspectionQuestionID = Convert.ToInt32(item.InspectionQuestionID);
                    ax.InspectionAnswer = item.InspectionAnswer;
                    ax.InspectionQuestion = item.InspectionQuestion;
                    ax.Remark = item.Remark;
                    ax.IsActive = true;
                    ax.CreatedBy = "1";
                    ax.CreatedDate = DateTime.Now;
                    db.InspectionCheckListAnswerMasters.Add(ax);




                }

                //MileStoneMaster m = db.MileStoneMasters.Where(x => x.ProjectPreparationID == ProjectPreparationID).FirstOrDefault();
                //m.IsInspectionDone = true;
                //m.ModifiedDate = DateTime.Now;

                var updateFlag = db.FundReleases.Where(x => x.DistrictID == Districtid && x.ProjectNo == model.ProjectNo && x.IsInspectionDone == null).FirstOrDefault();
                UtilizationMaster ab = db.UtilizationMasters.Where(x => x.ProjectPreparationID == ProjectPreparationID).OrderByDescending(x => x.UtilizationID).FirstOrDefault();
                string phyicalflag = ab.Phyicalintsallmentflag;
                ab.inspectionflag = Convert.ToInt32(phyicalflag);
                res = db.SaveChanges();


                if (res > 0)
                {
                    var milestone = db.MileStoneMasters.Where(x => x.DistrictID ==Districtid && x.ProjectNo == model.ProjectNo && x.InstallmentID == updateFlag.InstallmentID).FirstOrDefault();
                    var fundupdt = db.FundReleases.Where(x => x.DistrictID == Districtid && x.ProjectNo == model.ProjectNo && x.InstallmentID == updateFlag.InstallmentID).FirstOrDefault();
                    if (milestone != null)
                    {
                        //DTO_MileStoneMaster mile = new DTO_MileStoneMaster();
                        milestone.IsInspectionDone = true;
                        db.Entry(milestone).State = System.Data.Entity.EntityState.Modified;
                        db.SaveChanges();
                    }
                    if (fundupdt != null)
                    {
                        //DTO_MileStoneMaster mile = new DTO_MileStoneMaster();
                        fundupdt.IsInspectionDone = true;
                        db.Entry(fundupdt).State = System.Data.Entity.EntityState.Modified;
                        db.SaveChanges();
                    }
                    //#region Ramdhyan----code for final status update
                    //var milestonecount = db.MileStoneMasters.Where(x => x.ProjectPreparationID == ProjectPreparationID).Count();
                    //if (milestone.InstallmentID == milestonecount)
                    //{
                    //    var updtfinalstatus = db.ProjectProposalPreprations.Where(x => x.ProjectPreparationID == ProjectPreparationID).FirstOrDefault();
                    //    updtfinalstatus.FinalStatus = "Completed";
                    //    updtfinalstatus.Stageid = 3;
                    //    updtfinalstatus.RunningStatus = "Closed";
                    //    updtfinalstatus.ModifyDate = DateTime.Now;
                    //    db.Entry(updtfinalstatus).State = System.Data.Entity.EntityState.Modified;
                    //    db.SaveChanges();
                    //}
                    //#endregion
                    JR.IsSuccess = true;
                    JR.Message = "1";
                    //JR.RedURL = "/ProjectWorkApproval/ProjectProposalPrepration";
                }
                else if (res == 0)
                {
                    JR.IsSuccess = true;
                    JR.Message = "0";

                }

                else
                {

                    JR.Message = "Some Error Occured, Contact to Admin";
                }




            }

            catch (Exception ex)
            {

            }
            return Json(JR, JsonRequestBehavior.AllowGet);
        }


     public JsonResult UpdateRecord(string lis, int Districtid, int ProjectPreparationID)
        {
            JsonResponse JR = new JsonResponse();
            int res = 0;
            JavaScriptSerializer js = new JavaScriptSerializer();
            DTO_InspectionCheckListAnswerMaster model = new DTO_InspectionCheckListAnswerMaster();
            List<DTO_InspectionCheckListAnswerMaster> list = js.Deserialize<List<DTO_InspectionCheckListAnswerMaster>>(lis);
            try
            {
                foreach (var item in list)
                {
                    InspectionCheckListAnswerMaster ax = db.InspectionCheckListAnswerMasters.Where(x => x.ProjectPreparationID == ProjectPreparationID && x.InspectionQuestionID == item.InspectionQuestionID).FirstOrDefault(); ;
                    ax.ProjectPreparationID = Convert.ToInt32(ProjectPreparationID);
                    ProjectProposalPrepration abc = db.ProjectProposalPreprations.Where(x => x.ProjectPreparationID == ProjectPreparationID).FirstOrDefault();
                    model.ProjectNo = abc.ProjectNo;

                    ax.ProjectNo = model.ProjectNo;
                    ax.Districtid = Districtid;
                    ax.ProjectPreparationID = ProjectPreparationID;
                    ax.InspectionQuestionID = Convert.ToInt32(item.InspectionQuestionID);
                    ax.InspectionAnswer = item.InspectionAnswer;
                    ax.InspectionQuestion = item.InspectionQuestion;
                    ax.Remark = item.Remark;
                    ax.IsActive = true;
                    ax.CreatedBy = "1";
                    ax.CreatedDate = DateTime.Now;
                    //db.InspectionCheckListAnswerMasters.Add(ax);


                    res = db.SaveChanges();

                }

                MileStoneMaster m = db.MileStoneMasters.Where(x => x.ProjectPreparationID == ProjectPreparationID).FirstOrDefault();
                m.IsInspectionDone = true;
                m.ModifiedDate = DateTime.Now;
                UtilizationMaster ab = db.UtilizationMasters.Where(x => x.ProjectPreparationID == ProjectPreparationID).OrderByDescending(x => x.UtilizationID).FirstOrDefault();
                string phyicalflag = ab.Phyicalintsallmentflag;
                ab.inspectionflag = Convert.ToInt32(phyicalflag);




                res = db.SaveChanges();


                if (res > 0)
                {


                    JR.IsSuccess = true;
                    JR.Message = "1";
                    //JR.RedURL = "/ProjectWorkApproval/ProjectProposalPrepration";
                }
                else if (res == 0)
                {
                    JR.IsSuccess = true;
                    JR.Message = "0";

                }

                else
                {

                    JR.Message = "Some Error Occured, Contact to Admin";
                }




            }

            catch (Exception ex)
            {

            }
            return Json(JR, JsonRequestBehavior.AllowGet);
        }



        [HttpPost]
        public ActionResult UploadPhotograph(int ProjectId)

        {

            if (Request.Files.Count > 0)
            {
                List<InspectionCheckListAnswerMaster> objP = db.InspectionCheckListAnswerMasters.Where(x => x.ProjectPreparationID == ProjectId).ToList();

               

               


                foreach (var user in objP)
                {
                    HttpPostedFileBase mainPic = Request.Files[0];
                    string fileExt = Path.GetExtension(mainPic.FileName);
                    var id = Guid.NewGuid();
                    string fName = "Photograph" + DateTime.Now.ToString("yyyy-dd-MM-HH-mm-ss") + id + fileExt;
                    var path = Path.Combine(Server.MapPath("~/Documents"), fName);

                    int answerid = user.InspectionCheckListAnswerID;


                    mainPic.SaveAs(path);
                    InspectionCheckListAnswerMaster abc = db.InspectionCheckListAnswerMasters.Where(x => x.ProjectPreparationID == ProjectId && x.InspectionCheckListAnswerID== answerid).FirstOrDefault();

                    abc.Photographuploadfile = "/Documents/" + fName;
                    db.SaveChanges();


                }




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
        public ActionResult uploadAttachmentfile(int ProjectId)

        {

            if (Request.Files.Count > 0)
            {
                List<InspectionCheckListAnswerMaster> objP = db.InspectionCheckListAnswerMasters.Where(x => x.ProjectPreparationID == ProjectId).ToList();


                foreach(var user in objP)
                {
                    HttpPostedFileBase mainPic = Request.Files[0];
                    string fileExt = Path.GetExtension(mainPic.FileName);
                    var id = Guid.NewGuid();
                    string fName = "Attachmentfile" + DateTime.Now.ToString("yyyy-dd-MM-HH-mm-ss") + id + fileExt;
                    var path = Path.Combine(Server.MapPath("~/Documents"), fName);


                    mainPic.SaveAs(path);
                    int answerid = user.InspectionCheckListAnswerID;
                    InspectionCheckListAnswerMaster abc = db.InspectionCheckListAnswerMasters.Where(x => x.ProjectPreparationID == ProjectId && x.InspectionCheckListAnswerID == answerid).FirstOrDefault();



                    abc.Attachmentfile = "/Documents/" + fName;
                    db.SaveChanges();


                }



               




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

        #region Ramdhyan

        //public ActionResult CreateInspection(int ProjectPreparationID)
        //{
        //    int? DistID = null;
        //    if (UserManager.GetUserLoginInfo(User.Identity.Name).RoleID == 2)
        //    {
        //        DistID = UserManager.GetUserLoginInfo(User.Identity.Name).DistID;
        //    }
        //    DTO_InspectionMaster model = new DTO_InspectionMaster();
        //    var ProjectNo = db.ProjectProposalPreprations.Where(x => x.ProjectPreparationID == ProjectPreparationID).FirstOrDefault().ProjectNo;
        //    model.DistrictID = DistID;
        //    model.ProjectNo = ProjectNo;
        //    model.ProjectPreparationID = ProjectPreparationID;
        //    ViewBag.ProjectID = new SelectList((from mm in db.MileStoneMasters
        //                                        join ppp in db.ProjectProposalPreprations on mm.ProjectPreparationID equals ppp.ProjectPreparationID into pps_left
        //                                        from pm in pps_left.DefaultIfEmpty()
        //                                        where pm.IsActive == true && mm.DistrictID == DistID && mm.ProjectPreparationID == pm.ProjectPreparationID && mm.IsPhProgressDone == true && mm.IsInspectionDone != true
        //                                        select new
        //                                        {
        //                                            ProjectNo = mm.ProjectNo,
        //                                            ProjectName = pm.ProjectName
        //                                        }).Distinct(), "ProjectNo", "ProjectName", ProjectNo);

        //    return View(model);
        //}

        //[HttpPost]
        //public ActionResult CreateInspection(DTO_InspectionMaster model)
        //{
        //    return View();
        //}

        public ActionResult CreateInspection(int ProjectPreparationID)
        {
            int? DistID = null;
            if (UserManager.GetUserLoginInfo(User.Identity.Name).RoleID == 2)
            {
                DistID = UserManager.GetUserLoginInfo(User.Identity.Name).DistID;
            }
            DTO_InspectionMaster model = new DTO_InspectionMaster();
            var ProjectNo = db.ProjectProposalPreprations.Where(x => x.ProjectPreparationID == ProjectPreparationID).FirstOrDefault().ProjectNo;
            model.DistrictID = DistID;
            model.ProjectNo = ProjectNo;
            model.ProjectPreparationID = ProjectPreparationID;
            ViewBag.ProjectID = new SelectList((from mm in db.MileStoneMasters
                                                join ppp in db.ProjectProposalPreprations on mm.ProjectPreparationID equals ppp.ProjectPreparationID into pps_left
                                                from pm in pps_left.DefaultIfEmpty()
                                                where pm.IsActive == true && mm.DistrictID == DistID && mm.ProjectPreparationID == pm.ProjectPreparationID && mm.IsPhProgressDone == true && mm.IsInspectionDone != true
                                                select new
                                                {
                                                    ProjectNo = mm.ProjectNo,
                                                    ProjectName = pm.ProjectName
                                                }).Distinct(), "ProjectNo", "ProjectName", ProjectNo);

            return View(model);
        }

        [HttpPost]
        public ActionResult CreateInspection(DTO_InspectionMaster model)
        {
            string msg = string.Empty;
            JsonResponse JR = new JsonResponse();
            if (!ModelState.IsValid)
            {
                JR.Data = ModelState.Select(x => x.Value.Errors).Where(y => y.Count > 0).ToList();
                return Json(JR, JsonRequestBehavior.AllowGet);
            }
            using (var transaction = db.Database.BeginTransaction())
            {
                try
                {
                    var installment = db.PhysicalProgressMasters.Where(x => x.ProjectPreparationID == model.ProjectPreparationID && x.DistrictID == model.DistrictID).OrderByDescending(x=>x.Phyicalintsallmentflag).FirstOrDefault();
                    var installmentID = Convert.ToInt32(installment.Phyicalintsallmentflag);
                    var milestone = db.MileStoneMasters.Where(x => x.ProjectPreparationID == model.ProjectPreparationID && x.InstallmentID == installmentID).FirstOrDefault();
                    foreach (var item in model.lis)
                    {
                        InspectionCheckListAnswerMaster obj = new InspectionCheckListAnswerMaster();
                        obj.InspectionAnswer = item.InspectionAnswer;
                        obj.InspectionQuestionID = item.InspectionQuestionID;
                        obj.InspectionQuestion = item.InspectionQuestion;
                        obj.ProjectNo = model.ProjectNo;
                        obj.ProjectPreparationID = model.ProjectPreparationID;
                        obj.Districtid = model.DistrictID;
                        obj.FundReleaseID = installment.FundReleaseID;
                        obj.InstallmentID = installmentID;
                        obj.CreatedBy = UserManager.GetUserLoginInfo(User.Identity.Name).LoginID.ToString();
                        obj.CreatedDate = DateTime.Now;
                        obj.IsActive = true;
                        db.InspectionCheckListAnswerMasters.Add(obj);
                        db.SaveChanges();
                    }
                    if (!String.IsNullOrEmpty(model.InspectionCopy))
                    {
                        if (model.InspectionCopy.Contains("Expp::"))
                        {
                            msg = model.InspectionCopy;
                            return Json(msg, JsonRequestBehavior.AllowGet);
                        }
                        //HttpPostedFileBase file = Request.Files["InspectionCopy"];
                        //model.InspectionCopy = file.FileName;
                        //file.SaveAs(Server.MapPath("~/Documents/")+file.FileName);
                    }
                    if (!String.IsNullOrEmpty(model.InspectionImage1))
                    {
                        model.InspectionImage1 = BusinessLogics.UploadFileDMF(model.InspectionImage1);
                        if (model.InspectionImage1.Contains("Expp::"))
                        {
                            msg = model.InspectionImage1;
                            return Json(msg, JsonRequestBehavior.AllowGet);
                        }
                    }
                    if (!String.IsNullOrEmpty(model.InspectionImage2))
                    {
                        model.InspectionImage2 = BusinessLogics.UploadFileDMF(model.InspectionImage2);
                        if (model.InspectionImage2.Contains("Expp::"))
                        {
                            msg = model.InspectionImage2;
                            return Json(msg, JsonRequestBehavior.AllowGet);
                        }
                    }
                    if (!String.IsNullOrEmpty(model.InspectionImage3))
                    {
                        model.InspectionImage3 = BusinessLogics.UploadFileDMF(model.InspectionImage3);
                        if (model.InspectionImage3.Contains("Expp::"))
                        {
                            msg = model.InspectionImage3;
                            return Json(msg, JsonRequestBehavior.AllowGet);
                        }
                    }
                    if (!String.IsNullOrEmpty(model.InspectionImage4))
                    {
                        model.InspectionImage4 = BusinessLogics.UploadFileDMF(model.InspectionImage4);
                        if (model.InspectionImage4.Contains("Expp::"))
                        {
                            msg = model.InspectionImage4;
                            return Json(msg, JsonRequestBehavior.AllowGet);
                        }
                    }
                    var utilization = db.UtilizationMasters.Where(x => x.ProjectPreparationID == model.ProjectPreparationID && x.DistrictID == model.DistrictID && x.FundReleaseID == installment.FundReleaseID).FirstOrDefault();
                    db.InspectionMasters.Add(new InspectionMaster
                    {
                        ProjectNo = model.ProjectNo,
                        ProjectPreparationID = model.ProjectPreparationID
                    ,
                        DistrictID = model.DistrictID,
                        FundReleaseID = installment.FundReleaseID,
                        MileStoneID = milestone.MileStoneID,
                        InspectionCopy = model.InspectionCopy,
                        InspectionImage1 = model.InspectionImage1,
                        InspectionImage2 = model.InspectionImage2,
                        InspectionImage3 = model.InspectionImage3,
                        InspectionImage4 = model.InspectionImage4,
                        InspectionDate = model.InspectionDate,
                        UtilizationID = utilization.UtilizationID,
                        InstallmentID = installmentID,
                        Remark = model.Remark,
                        CreatedDate = DateTime.Now,
                        CreatedBy = UserManager.GetUserLoginInfo(User.Identity.Name).LoginID.ToString(),
                        IsActive = true,


                    });
                    var res = db.SaveChanges();
                    if (res > 0)
                    {
                        msg = "1";
                        var fundupdt = db.FundReleases.Where(x => x.DistrictID == model.DistrictID && x.ProjectPreparationID == model.ProjectPreparationID && x.InstallmentID == installmentID).FirstOrDefault();
                        if (milestone != null)
                        {
                            //DTO_MileStoneMaster mile = new DTO_MileStoneMaster();
                            milestone.IsInspectionDone = true;
                            db.Entry(milestone).State = System.Data.Entity.EntityState.Modified;
                            db.SaveChanges();
                        }
                        if (fundupdt != null)
                        {
                            //DTO_MileStoneMaster mile = new DTO_MileStoneMaster();
                            fundupdt.IsInspectionDone = true;
                            db.Entry(fundupdt).State = System.Data.Entity.EntityState.Modified;
                            db.SaveChanges();
                        }
                        #region Ramdhyan----code for final status update
                        var milestonecount = db.MileStoneMasters.Where(x => x.ProjectPreparationID == model.ProjectPreparationID).Count();
                        if (milestone.InstallmentID == milestonecount)
                        {
                            var updtfinalstatus = db.ProjectProposalPreprations.Where(x => x.ProjectPreparationID == model.ProjectPreparationID).FirstOrDefault();
                            updtfinalstatus.FinalStatus = "Closed";
                            updtfinalstatus.Stageid = 3;
                            updtfinalstatus.RunningStatus = "Completed";
                            updtfinalstatus.ModifyDate = DateTime.Now;
                            db.Entry(updtfinalstatus).State = System.Data.Entity.EntityState.Modified;
                            db.SaveChanges();
                        }
                        #endregion
                        transaction.Commit();
                    }
                    else
                    {
                        msg = "0";
                        transaction.Rollback();
                    }
                    return Json(msg, JsonRequestBehavior.AllowGet);
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                }
            }
            return Json(msg, JsonRequestBehavior.AllowGet);
        }

        public ActionResult InspectcionList(string SectorType, string SectorName, string DistrictName, string ProjectName)
        {
            ViewBag.SectorType = new SelectList(db.SectorTypeMasters, "SectorType", "SectorType", null);
            ViewBag.SectorName = new SelectList(db.SectorNameMasters, "SectorName", "SectorName", null);
            int? DistID = null;
            if (UserManager.GetUserLoginInfo(User.Identity.Name).RoleID == 2)
            {
                DistID = UserManager.GetUserLoginInfo(User.Identity.Name).DistID;
                var district = db.DistrictMasters.Where(x => x.DistrictId == DistID).FirstOrDefault();
                ViewBag.DistrictName = new SelectList(db.DistrictMasters.Where(x => x.DistrictName == district.DistrictName), "DistrictName", "DistrictName", district.DistrictName);
                ViewBag.ProjectName = new SelectList(db.ProjectProposalPreprations.Where(x => x.DistID == DistID && x.Stageid == 2), "ProjectName", "ProjectName", null);

                var groupedData = (from mm in db.MileStoneMasters
                                   join dm in db.DistrictMasters on mm.DistrictID equals dm.DistrictId
                                   join ppp in db.ProjectProposalPreprations on mm.ProjectPreparationID equals ppp.ProjectPreparationID
                                   into ppps
                                   from ppp in ppps.DefaultIfEmpty()
                                   join stm in db.SectorTypeMasters on ppp.SectorTypeId equals stm.SectorTypeID into stms
                                   from stm in stms.DefaultIfEmpty()
                                   join snm in db.SectorNameMasters on ppp.SectorID equals snm.SectorNameId into snms
                                   from snm in snms.DefaultIfEmpty()
                                   where ppp.IsActive == true && ppp.DistID == DistID && ppp.ProjectNo != null && ppp.Stageid == 2 && mm.IsFundReleased == true && mm.IsPhProgressDone == true && mm.IsUtilizationUploaded==true
                                   && (stm.SectorType == SectorType || String.IsNullOrEmpty(SectorType))
                                   && (snm.SectorName == SectorName || String.IsNullOrEmpty(SectorName))
                                   && (dm.DistrictName == DistrictName || String.IsNullOrEmpty(DistrictName))
                                   && (ppp.ProjectName == ProjectName || String.IsNullOrEmpty(ProjectName))
                                   // orderby mm.MileStoneStatus descending
                                   select new DTO_InspectionMaster
                                   {
                                       ProjectPreparationID = ppp.ProjectPreparationID,
                                       DistrictID = ppp.DistID,
                                       ProjectNo = ppp.ProjectNo,
                                       DistrictName = dm.DistrictName,
                                       ProjectName = ppp.ProjectName,
                                       SectorName = snm.SectorName,
                                       SectorType = stm.SectorType,
                                       SanctionedProjectCost = ppp.SanctionedProjectCost,
                                       IsPhProgressDone = mm.IsPhProgressDone,
                                       IsUtilizationUploaded = mm.IsUtilizationUploaded,
                                       IsInspectionDone = mm.IsInspectionDone,
                                       IsFundReleased = mm.IsFundReleased,
                                       MileStoneStatus = mm.MileStoneStatus
                                   })
                 .GroupBy(x => x.ProjectPreparationID)
                 .ToList();

                //var data = groupedData.OrderByDescending(x=>x.mil);
                var data = groupedData.Select(group => group.ToList().OrderByDescending(x => x.MileStoneStatus));
                var processedData = data.Select(group => group.FirstOrDefault()).ToList();
                ViewBag.LstData = processedData;
            }
            else
            {
                ViewBag.DistrictName = new SelectList(db.DistrictMasters, "DistrictName", "DistrictName", null);
                ViewBag.ProjectName = new SelectList(db.ProjectProposalPreprations.Where(x => x.Stageid == 2), "ProjectName", "ProjectName", null);
                var groupedData = (from mm in db.MileStoneMasters
                                   join dm in db.DistrictMasters on mm.DistrictID equals dm.DistrictId
                                   join ppp in db.ProjectProposalPreprations on mm.ProjectPreparationID equals ppp.ProjectPreparationID
                                   into ppps
                                   from ppp in ppps.DefaultIfEmpty()
                                   join stm in db.SectorTypeMasters on ppp.SectorTypeId equals stm.SectorTypeID into stms
                                   from stm in stms.DefaultIfEmpty()
                                   join snm in db.SectorNameMasters on ppp.SectorID equals snm.SectorNameId into snms
                                   from snm in snms.DefaultIfEmpty()
                                   where ppp.IsActive == true && ppp.ProjectNo != null && ppp.Stageid == 2 && mm.IsFundReleased == true && mm.IsPhProgressDone == true && mm.IsUtilizationUploaded == true
                                   && (stm.SectorType == SectorType || String.IsNullOrEmpty(SectorType))
                                   && (snm.SectorName == SectorName || String.IsNullOrEmpty(SectorName))
                                   && (dm.DistrictName == DistrictName || String.IsNullOrEmpty(DistrictName))
                                   && (ppp.ProjectName == ProjectName || String.IsNullOrEmpty(ProjectName))
                                   // orderby mm.MileStoneStatus descending
                                   select new DTO_InspectionMaster
                                   {
                                       ProjectPreparationID = ppp.ProjectPreparationID,
                                       DistrictID = ppp.DistID,
                                       ProjectNo = ppp.ProjectNo,
                                       DistrictName = dm.DistrictName,
                                       ProjectName = ppp.ProjectName,
                                       SectorName = snm.SectorName,
                                       SectorType = stm.SectorType,
                                       SanctionedProjectCost = ppp.SanctionedProjectCost,
                                       IsPhProgressDone = mm.IsPhProgressDone,
                                       IsUtilizationUploaded = mm.IsUtilizationUploaded,
                                       IsInspectionDone = mm.IsInspectionDone,
                                       IsFundReleased = mm.IsFundReleased,
                                       MileStoneStatus = mm.MileStoneStatus
                                   })
                   .GroupBy(x => x.ProjectPreparationID)
                   .ToList();

                //var data = groupedData.OrderByDescending(x=>x.mil);
                var data = groupedData.Select(group => group.ToList().OrderByDescending(x => x.MileStoneStatus));
                var processedData = data.Select(group => group.FirstOrDefault()).ToList();
                ViewBag.LstData = processedData;
            }
            return View();
        }
        public JsonResult GetInspectiondetails(int ProjectPreparationID)
        {
            var data = (from ipm in db.InspectionMasters
                        join ins in db.InstallmentMasters on ipm.InstallmentID equals ins.InstallmentID into insa
                        from ins in insa.DefaultIfEmpty()
                        join ppp in db.ProjectProposalPreprations on ipm.ProjectPreparationID equals ppp.ProjectPreparationID into pps from ppp in pps.DefaultIfEmpty()
                        join dm in db.DistrictMasters on ipm.DistrictID equals dm.DistrictId into dms
                        from dm in dms.DefaultIfEmpty()
                        //join ica in db.InspectionCheckListAnswerMasters on ipm.ProjectPreparationID equals ica.ProjectPreparationID
                        where ipm.ProjectPreparationID == ProjectPreparationID
                        select new DTO_InspectionMaster
                        {
                            //DistrictName = dm.DistrictName,
                            ProjectPreparationID = ipm.ProjectPreparationID,
                            ProjectName = ppp.ProjectName,
                            InstallmentID = ipm.InstallmentID,
                            InstallmentName = ins.InstallmentName,
                            //InspectionQuestion = ica.InspectionQuestion,
                            //InspectionAnswer = ica.InspectionAnswer,
                            InspectionCopy = ipm.InspectionCopy,
                            InspectionImage1 = ipm.InspectionImage1,
                            InspectionImage2 = ipm.InspectionImage2,
                            InspectionImage3 = ipm.InspectionImage3,
                            InspectionImage4 = ipm.InspectionImage4,
                            InspectionDate = ipm.InspectionDate,
                            Remark = ipm.Remark
                        }).ToList();
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult GetQuestionAnswerDetails(int ProjectPreparationID,int InstallmentID)
        {
            var data = (from icam in db.InspectionCheckListAnswerMasters
                        join ppp in db.ProjectProposalPreprations on icam.ProjectPreparationID equals ppp.ProjectPreparationID into pps
                        from ppp in pps.DefaultIfEmpty()
                        join ins in db.InstallmentMasters on icam.InstallmentID equals ins.InstallmentID
                        where icam.ProjectPreparationID == ProjectPreparationID && icam.InstallmentID==InstallmentID
                        select new DTO_InspectionMaster
                        {
                            ProjectName = ppp.ProjectName,
                            InspectionAnswer = icam.InspectionAnswer,
                            InspectionQuestion = icam.InspectionQuestion,
                            InstallmentName=ins.InstallmentName
                        }).ToList();
            return Json(data, JsonRequestBehavior.AllowGet);
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
                        where ppp.Stageid == 2
                        && ppp.SectorTypeId == (SectorTypeId.SectorTypeID == 0 ? ppp.SectorTypeId : SectorTypeId.SectorTypeID)
                        && ppp.SectorID == (sectorID.SectorNameId == 0 ? ppp.SectorID : sectorID.SectorNameId)
                        && ppp.DistID == (District.DistrictId == 0 ? ppp.DistID : District.DistrictId)
                        select new DTO_ProjectProposalPrepration
                        {
                            ProjectName = ppp.ProjectName

                        }).ToList();

            return Json(data, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult GetMilestoneByInstallment(int ProjectPreparationID)
        {
            //var data1 = db.MileStoneMasters.Where(x => x.ProjectPreparationID == ProjectPreparationID && x.IsFundReleased == true && x.IsPhProgressDone == null).FirstOrDefault();
            var data = (from mm in db.MileStoneMasters
                        join ppp in db.ProjectProposalPreprations on mm.ProjectPreparationID equals ppp.ProjectPreparationID into pps
                        from ppp in pps.DefaultIfEmpty()
                        join ins in db.InstallmentMasters on mm.InstallmentID equals ins.InstallmentID
                        where mm.ProjectPreparationID == ProjectPreparationID && mm.IsFundReleased == true && mm.IsPhProgressDone == true && mm.IsUtilizationUploaded == true && mm.IsInspectionDone==null
                        select new DTO_MileStoneMaster
                        {
                            InstallmentName = ins.InstallmentName,
                            SanctionedProjectCost = ppp.SanctionedProjectCost,
                            Instext = mm.Instext,
                            InsPercentage = mm.InsPercentage
                        }).FirstOrDefault();
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        #endregion

    }
}