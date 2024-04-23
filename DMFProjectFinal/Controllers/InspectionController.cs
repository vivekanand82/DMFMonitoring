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
                List<InspectionCheckListAnswerMaster> objP = objP = db.InspectionCheckListAnswerMasters.Where(x => x.ProjectPreparationID == ProjectId).ToList();


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







    }
}