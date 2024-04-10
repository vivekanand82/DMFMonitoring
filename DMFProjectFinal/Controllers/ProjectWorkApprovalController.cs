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

namespace DMFProjectFinal.Controllers
{
    [SessionFilter]
    public class ProjectWorkApprovalController : Controller
    {
        private dfm_dbEntities db = new dfm_dbEntities();
        //public ActionResult ProjectProposalPrepration()
        //{
        //    return View();
        //}
        public static string coonectionstrings = ConfigurationManager.ConnectionStrings["constr"].ToString();
        #region ProjectProposalPrepration
        public ActionResult ProjectProposalPrepration(int? DistID, int? AgencyID, long? ProjectID, int? SectorID)
        {
            if (UserManager.GetUserLoginInfo(User.Identity.Name).RoleID == 2)
            {
                DistID = UserManager.GetUserLoginInfo(User.Identity.Name).DistID;
            }
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



           if(Request.QueryString["id"]!=null)
            {



            }



            var LstData = (from ppp in db.ProjectProposalPreprations
                           join dm in db.DistrictMasters on ppp.DistID equals dm.DistrictId
                           join tm in db.TehsilMasters on ppp.TehsilId equals tm.TehsilId
                           join bm in db.BlockMasters on ppp.BlockId equals bm.BlockId
                           join vm in db.VillageMasters on ppp.VillageId equals vm.VillageId
                           join snm in db.SectorNameMasters on ppp.SectorID equals snm.SectorNameId
                           join stm in db.SectorTypeMasters on ppp.SectorTypeId equals stm.SectorTypeID
                           join ag in db.AgenciesInfoes on ppp.AgencyID equals ag.AgencyID
                           //join pm in db.ProposalStatusMasters on ppp.ProjectPreparationID equals pm.ProjectID
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
                               RunningStatus=ppp.RunningStatus,
                               FinalStatus=ppp.FinalStatus,
                               Stageid=ppp.Stageid,
                               ProjectNo=ppp.ProjectNo
                              
                           }).ToList();
            ViewBag.LstData = LstData;
            return View();
        }
        public ActionResult CreateProjectProposalPrepration()
        {
            int? DistID = null;
            if (UserManager.GetUserLoginInfo(User.Identity.Name).RoleID == 2)
            {
                DistID = UserManager.GetUserLoginInfo(User.Identity.Name).DistID;
            }
            var Tehsil = db.TehsilMasters.Where(x => x.DistrictId == DistID).FirstOrDefault();
            DTO_ProjectProposalPrepration model = new DTO_ProjectProposalPrepration();
            ViewBag.DistID = new SelectList(db.DistrictMasters.Where(x => x.IsActive == true && x.DistrictId == (DistID == null ? x.DistrictId : DistID)), "DistrictId", "DistrictName", null);
            //ViewBag.TehsilId = new SelectList(db.TehsilMasters.Where(x => x.IsActive == true && x.DistrictId == (DistID == null ? x.DistrictId : DistID)), "TehsilId", "TehsilName", null);
            //ViewBag.BlockId = new SelectList(db.BlockMasters.Where(x => x.IsActive == true && x.DistrictId == (DistID == null ? x.DistrictId : DistID)), "BlockId", "BlockName", null);
            //ViewBag.VillageId = new SelectList(db.VillageMasters.Where(x => x.IsActive == true), "VillageId", "VillageNameInEnglish", null);
            ViewBag.AgencyID = new SelectList(db.AgenciesInfoes.Where(x => x.IsActive == true && x.DistID == (DistID == null ? x.DistID : DistID)).ToList().Select(x => new { ID = x.AgencyID, Text = x.Name + " / " + x.OwnerName }), "ID", "Text", null);
            ViewBag.ProjectID = new SelectList(db.ProjectMasters.Where(x => x.IsActive == true && x.DistID == (DistID == null ? x.DistID : DistID)).ToList().Select(x => new { ID = x.ProjectID, Text = x.ProjectName + " (" + x.ProjectCode + ")" }), "ID", "Text", null);
            //ViewBag.SectorID = new SelectList(db.SectorNameMasters.Where(x => x.IsActive == true), "SectorNameId", "SectorName", null);
            ViewBag.SectorTypeId = new SelectList(db.SectorTypeMasters.Where(x => x.IsActive == true), "SectorTypeId", "SectorType", null).ToList();
            ViewBag.SectorID = new SelectList(db.SectorNameMasters.Where(x => x.IsActive == true), "SectorNameId", "SectorName", null).ToList();
            //ViewBag.ProjectStatusID = new SelectList(db.ProjectStatusMasters.Where(x => x.IsActive == true), "ProjectStatusID", "ProjectStatus", null);


            //var lstData = new SelectList(from pm in db.ProjectMasters
            //                             join dst in db.DistrictMasters on pm.DistID equals dst.DistrictId
            //                             join st in db.SectorTypeMasters on pm.SectorTypeId equals st.SectorTypeID
            //                             select new DTO_ProjectMaster
            //                             {
            //                                 SectorTypeId = st.SectorTypeID,

            //                                 SectorType = st.SectorType,


            //                             }


            //              ).ToList().Select(st => new { SectorTypeId = st.Selected });



            ProjectProposalPrepration abc = new ProjectProposalPrepration();

            abc.ProjectDescription = Convert.ToString((from o in db.ProjectProposalPreprations select (int?)o.ProjectPreparationID).Count());
            int max;

            //if (string.IsNullOrEmpty(model.ProjectNo))
            //{
            //    max = 1;
            //    abc.ProjectDescription = "P000" + max.ToString();


            //}

            //else
            //{
            //    max = 1;
            //    abc.ProjectDescription = "P000" + max.ToString();


            //}

            string id = "P000";
            string Programid = id + abc.ProjectDescription;



            ViewBag.acq =Programid;



            return View(model);
        }
        [HttpPost]
        public JsonResult CreateProjectProposalPrepration(DTO_ProjectProposalPrepration model)
        {
            JsonResponse JR = new JsonResponse();
            if (!ModelState.IsValid)
            {
                JR.Data = ModelState.Select(x => x.Value.Errors).Where(y => y.Count > 0).ToList();
                return Json(JR, JsonRequestBehavior.AllowGet);
            }
            //HttpPostedFileBase file = Request.Files["ProposalCopy"];
            //model.ProposalCopy = file.FileName;
            //file.SaveAs(Server.MapPath("~/Documents/" + file.FileName+DateTime.Now.ToString("_yyyy_MM_dd_HH:mm:ss")));
            //HttpPostedFileBase file1 = Request.Files["WorkOrderCopy"];
            //model.WorkOrderCopy = file.FileName;
            //file.SaveAs(Server.MapPath("~/Documents/" + file.FileName + DateTime.Now.ToString("_yyyy_MM_dd_HH:mm:ss")));
            if (!String.IsNullOrEmpty(model.ProposalCopy))
            {
                model.ProposalCopy = BusinessLogics.UploadFileDMF(model.ProposalCopy);
                if (model.ProposalCopy.Contains("Expp::"))
                {
                    JR.Message = model.ProposalCopy;
                    return Json(JR, JsonRequestBehavior.AllowGet);
                }
            }

            if (!String.IsNullOrEmpty(model.WorkOrderCopy)) //added by ramdhyan 03.04.2024
            {
                model.WorkOrderCopy = BusinessLogics.UploadFileDMF(model.WorkOrderCopy);
                if (model.WorkOrderCopy.Contains("Expp::"))
                {
                    JR.Message = model.WorkOrderCopy;
                    return Json(JR, JsonRequestBehavior.AllowGet);
                }
            }
            //db.ProjectMasters.Add(new ProjectMaster
            //{
            //    DistID = model.DistID,
            //    ProjectName = model.ProjectName,
            //    ProjectDescription = model.ProjectDescription,
            //    CreatedBy = UserManager.GetUserLoginInfo(User.Identity.Name).LoginID,
            //    CreatedOn = DateTime.Now,
            //    IsActive = true,
            //    SectorTypeId = model.SectorTypeId,
            //    SectorNameId = model.SectorID
            //});
            //int res = db.SaveChanges();
            db.ProjectProposalPreprations.Add(new Models.ProjectProposalPrepration
            {
                CreatedOn = DateTime.Now,
                IsActive = true,
                CreatedBy = UserManager.GetUserLoginInfo(User.Identity.Name).LoginID,
                DistID = model.DistID,
                TehsilId=model.TehsilId,
                BlockId=model.BlockId,
                VillageId =model.VillageId,
                SectorID = model.SectorID,
                SectorTypeId=model.SectorTypeId,
                ProjectName=model.ProjectName,
                WorkLatitude=model.WorkLatitude,
                WorkLongitude=model.WorkLongitude,
                ProjectDescription=model.ProjectDescription,
                ProsposalNo = model.ProsposalNo,
                ProposalDate = model.ProposalDate,
                ProposalCopy = model.ProposalCopy,
                ProposedBy = model.ProposedBy,
                ProjectCost = model.ProjectCost,
                GSTAndOthers = model.GSTAndOthers,
                TenderNo = model.TenderNo,
                TenderDate = model.TenderDate,
                WorkOrderNo = model.WorkOrderNo,
                WorkOrderDate = model.WorkOrderDate,
                WorkOrderCopy=model.WorkOrderCopy,
                AgencyID = model.AgencyID,
                SanctionedProjectCost = model.SanctionedProjectCost
                //ProjectID = model.ProjectID,
                //ProjectStatusID = model.ProjectStatusID,
                
               
            });

            int res = db.SaveChanges();
            if (res > 0)
            {
                //Response.Write("<script>alert('Data saved successfully');window.location.href='/ProjectWorkApproval/ProjectProposalPrepration'</script>");
                
                JR.IsSuccess = true;
                JR.Message = "Data Saved Successfully";
                JR.RedURL = "/ProjectWorkApproval/ProjectProposalPrepration";
            }
            else
            {
              //  Response.Write("<script>alert('Error');window.location.href='/ProjectWorkApproval/CreateProjectProposalPrepration'</script>");
                JR.Message = "Some Error Occured, Contact to Admin";
            }
            return Json(JR, JsonRequestBehavior.AllowGet);
        }


        public JsonResult insertProjectworkproposal(DTO_ProjectProposalPrepration model)
        {
            JsonResponse JR = new JsonResponse();
            db.ProjectProposalPreprations.Add(new Models.ProjectProposalPrepration
            {
                CreatedOn = DateTime.Now,
                IsActive = true,
                CreatedBy = UserManager.GetUserLoginInfo(User.Identity.Name).LoginID,
                DistID = model.DistID,
                TehsilId = model.TehsilId,
                BlockId = model.BlockId,
                VillageId = model.VillageId,
                SectorID = model.SectorID,
                SectorTypeId = model.SectorTypeId,
                ProjectName = model.ProjectName,
                WorkLatitude = model.WorkLatitude,
                WorkLongitude = model.WorkLongitude,
                ProjectDescription = model.ProjectDescription,
                ProsposalNo = model.ProsposalNo,
                ProposalDate = model.ProposalDate,
                ProposalCopy = model.ProposalCopy,
                ProposedBy = model.ProposedBy,
                ProjectCost = model.ProjectCost,
                GSTAndOthers = model.GSTAndOthers,
                TenderNo = model.TenderNo,
                TenderDate = model.TenderDate,
                WorkOrderNo = model.WorkOrderNo,
                WorkOrderDate = model.WorkOrderDate,
                WorkOrderCopy = model.WorkOrderCopy,
                AgencyID = model.AgencyID,
                SanctionedProjectCost = model.SanctionedProjectCost,
                ProjectNo = model.ProjectNo




            });

            db.ProjectMasters.Add(new Models.ProjectMaster
            {
                CreatedOn = DateTime.Now,
                IsActive = true,
                CreatedBy = UserManager.GetUserLoginInfo(User.Identity.Name).LoginID,
                DistID = model.DistID,

                SectorTypeId = model.SectorTypeId,
                SectorNameId = model.SectorID,
                ProjectName = model.ProjectName,
                ProjectDescription = model.ProjectDescription,
                ProjectNo = model.ProjectNo




            }); ;


            int res = db.SaveChanges();
            if (res > 0)
            {


                JR.IsSuccess = true;
                JR.Message = "1";
                JR.RedURL = "/ProjectWorkApproval/ProjectProposalPrepration";
            }
            else
            {

                JR.Message = "Some Error Occured, Contact to Admin";
            }




            return Json(JR, JsonRequestBehavior.AllowGet);
        }


        public JsonResult Updateworkprposla(DTO_ProjectProposalPrepration model)
        {
            JsonResponse JR = new JsonResponse();
            ProjectProposalPrepration abc = new ProjectProposalPrepration();

            int ProjectPreparationID = Convert.ToInt32(model.ProjectPreparationID);
            abc = db.ProjectProposalPreprations.Where(x => x.ProjectPreparationID == ProjectPreparationID).FirstOrDefault();
                      abc.CreatedOn = DateTime.Now;


                    abc.CreatedBy = UserManager.GetUserLoginInfo(User.Identity.Name).LoginID;
                         abc.DistID = model.DistID;
            abc.TehsilId = model.TehsilId;
            abc.BlockId = model.BlockId;
            abc.VillageId = model.VillageId;
            abc.SectorID = model.SectorID;
            abc.SectorTypeId = model.SectorTypeId;
            abc.ProjectName = model.ProjectName;
            abc.WorkLatitude = model.WorkLatitude;
            abc.WorkLongitude = model.WorkLongitude;
            abc.ProjectDescription = model.ProjectDescription;
            abc.ProsposalNo = model.ProsposalNo;
            abc.ProposalDate = model.ProposalDate;
            abc.ProposalCopy = model.ProposalCopy;
            abc.ProposedBy = model.ProposedBy;
            abc.ProjectCost = model.ProjectCost;
            abc.GSTAndOthers = model.GSTAndOthers;
            abc.TenderNo = model.TenderNo;
            abc.TenderDate = model.TenderDate;
            abc.WorkOrderNo = model.WorkOrderNo;
            abc.WorkOrderDate = model.WorkOrderDate;
            abc.WorkOrderCopy = model.WorkOrderCopy;
            abc.AgencyID = model.AgencyID;
            abc.SanctionedProjectCost = model.SanctionedProjectCost;
            abc.ProjectNo = model.ProjectNo;

            ProjectMaster abe = db.ProjectMasters.Where(x => x.ProjectNo == model.ProjectNo).FirstOrDefault();

            abe.CreatedOn = DateTime.Now;
            abe.IsActive = true;
            abe.DistID = model.DistID;
            abe.SectorTypeId = model.SectorTypeId;
            abe.SectorNameId = model.SectorID;
            abe.ProjectName = model.ProjectName;
            abe.ProjectDescription = model.ProjectDescription;
            abe.ProjectNo= model.ProjectNo;

            //db.ProjectMasters.Add(new Models.ProjectMaster
            //{
            //    CreatedOn = DateTime.Now,
            //    IsActive = true,
            //    CreatedBy = UserManager.GetUserLoginInfo(User.Identity.Name).LoginID,
            //    DistID = model.DistID,

            //    SectorTypeId = model.SectorTypeId,
            //    SectorNameId = model.SectorID,
            //    ProjectName = model.ProjectName,
            //    ProjectDescription = model.ProjectDescription,
            //    ProjectNo = model.ProjectNo




            //}); ;

            int res = db.SaveChanges();
            if (res > 0)
            {


                JR.IsSuccess = true;
                JR.Message = "1";
                JR.RedURL = "/ProjectWorkApproval/ProjectProposalPrepration";
            }
            else
            {

                JR.Message = "Some Error Occured, Contact to Admin";
            }

            return Json(JR, JsonRequestBehavior.AllowGet);

        }





        [HttpPost]
        public ActionResult UploadProposalcopy(string ProjectNo)

        {

            if (Request.Files.Count > 0)
            {
                ProjectProposalPrepration objP = new ProjectProposalPrepration();

                objP= db.ProjectProposalPreprations.Where(x => x.ProjectNo == ProjectNo).FirstOrDefault();

                 HttpPostedFileBase mainPic = Request.Files[0];
                string fileExt = Path.GetExtension(mainPic.FileName);
                string fName = ProjectNo + "_" + DateTime.Now.Ticks + fileExt;
                var path = Path.Combine(Server.MapPath("~/Documents"), fName);

              


                mainPic.SaveAs(path);


                objP.ProposalCopy = "/Documents/"+fName;
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


        public ActionResult Uploadworkordercopy(string ProjectNo)

        {

            if (Request.Files.Count > 0)
            {
                ProjectProposalPrepration objP = new ProjectProposalPrepration();

                objP = db.ProjectProposalPreprations.Where(x => x.ProjectNo == ProjectNo).FirstOrDefault();

                HttpPostedFileBase mainPic = Request.Files[0];
                string fileExt = Path.GetExtension(mainPic.FileName);
                string fName = ProjectNo + "_" + DateTime.Now.Ticks + fileExt;
                var path = Path.Combine(Server.MapPath("~/Documents"), fName);

                mainPic.SaveAs(path);


                objP.WorkOrderCopy= "/Documents/" + fName;
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



        public JsonResult EditData(int ProjectPreparationID)
        {
          ProjectProposalPrepration abc = new ProjectProposalPrepration();
            abc = db.ProjectProposalPreprations.Where(x => x.ProjectPreparationID == ProjectPreparationID).FirstOrDefault();
            DTO_ProjectProposalPrepration model = new DTO_ProjectProposalPrepration();
            model.ProjectPreparationID = Convert.ToString(abc.ProjectPreparationID);

            model.DistID = abc.DistID;
            model.TehsilId = abc.TehsilId;
            model.BlockId = abc.BlockId;
            model.VillageId = abc.VillageId;
            model.SectorTypeId = abc.SectorTypeId;
            model.SectorID = abc.SectorID;

            model.ProjectName = abc.ProjectName;

            model.WorkLatitude = abc.WorkLatitude;
            model.WorkLongitude = abc.WorkLongitude;
            model.ProjectDescription = abc.ProjectDescription;
            model.ProsposalNo = abc.ProsposalNo;
            model.ProposalDate = abc.ProposalDate;
            model.ProposedBy = abc.ProposedBy;
            model.ProposalCopy = abc.ProposalCopy;
            model.ProposedBy = abc.ProposedBy;
            model.ProjectCost = abc.ProjectCost;
            model.GSTAndOthers = abc.GSTAndOthers;
            model.TenderNo = abc.TenderNo;
            model.TenderDate = abc.TenderDate;
            model.TenderNo = abc.TenderNo;

            model.WorkOrderNo = abc.WorkOrderNo;
            model.WorkOrderDate = abc.WorkOrderDate;
            model.WorkOrderCopy = abc.WorkOrderCopy;
            model.AgencyID = abc.AgencyID;
            model.SanctionedProjectCost = abc.SanctionedProjectCost;
            model.ProjectNo = abc.ProjectNo;
          
            return Json(model, JsonRequestBehavior.AllowGet);
        }





        public ActionResult EditProjectProposalPrepration(string id)
        {
            if (String.IsNullOrEmpty(id))
            {
                return RedirectToAction("Login", "Account");
            }


            if (Request.QueryString["id"] != null)
            {
                ViewBag.id = CryptoEngine.Decrypt(id);


            }



            try
            {
               long _id = long.Parse(CryptoEngine.Decrypt(id));
                //long _id = long.Parse(id);  use this code when u use server side datatable for display
                var Info = db.ProjectProposalPreprations.Where(x => x.ProjectPreparationID == _id).FirstOrDefault();
                if (Info != null)
                {
                    int? DistID = null;
                    if (UserManager.GetUserLoginInfo(User.Identity.Name).RoleID == 2)
                    {
                        DistID = UserManager.GetUserLoginInfo(User.Identity.Name).DistID;
                    }
                   // var Tehsil = db.TehsilMasters.Where(x => x.DistrictId == DistID).FirstOrDefault();
                    ViewBag.DistID = new SelectList(db.DistrictMasters.Where(x => x.IsActive == true && x.DistrictId == (DistID == null ? x.DistrictId : DistID)), "DistrictId", "DistrictName", null);
                    ViewBag.TehsilId = new SelectList(db.TehsilMasters.Where(x => x.IsActive == true && x.DistrictId == (DistID == null ? x.DistrictId : DistID)), "TehsilId", "TehsilName", null);
                    ViewBag.BlockId = new SelectList(db.BlockMasters.Where(x => x.IsActive == true && x.DistrictId == (DistID == null ? x.DistrictId : DistID)), "BlockId", "BlockName", null);
                    ViewBag.VillageId = new SelectList(db.VillageMasters.Where(x => x.IsActive == true && x.TehsilId == (Info.TehsilId == null ? x.TehsilId : Info.TehsilId)), "VillageId", "VillageNameInEnglish", null);
                    ViewBag.AgencyID = new SelectList(db.AgenciesInfoes.Where(x => x.IsActive == true && x.DistID == (DistID == null ? x.DistID : DistID)).ToList().Select(x => new { ID = x.AgencyID, Text = x.Name + " / " + x.OwnerName }), "ID", "Text", Info.AgencyID);
                    ViewBag.ProjectID = new SelectList(db.ProjectMasters.Where(x => x.IsActive == true && x.DistID == (DistID == null ? x.DistID : DistID)).ToList().Select(x => new { ID = x.ProjectID, Text = x.ProjectName + " (" + x.ProjectCode + ")" }), "ID", "Text", Info.ProjectID);
                    ViewBag.SectorTypeId = new SelectList(db.SectorTypeMasters.Where(x => x.IsActive == true ), "SectorTypeId", "SectorType", Info.SectorTypeId).ToList();
                    ViewBag.SectorID = new SelectList(db.SectorNameMasters.Where(x => x.IsActive == true && x.SectorTypeId==Info.SectorTypeId), "SectorNameId", "SectorName", Info.SectorID);
                   // ViewBag.ProjectStatusID = new SelectList(db.ProjectStatusMasters.Where(x => x.IsActive == true), "ProjectStatusID", "ProjectStatus", Info.ProjectStatusID);

                    return View("~/Views/ProjectWorkApproval/CreateProjectProposalPrepration.cshtml", new DTO_ProjectProposalPrepration
                    {
                        
                        //CreatedOn = DateTime.Now,
                        //IsActive = true,
                        //CreatedBy = UserManager.GetUserLoginInfo(User.Identity.Name).LoginID,
                        //DistID = Info.DistID,
                        //TehsilId = Info.TehsilId,
                        //BlockId = Info.BlockId,
                        //VillageId = Info.VillageId,
                        //SectorID = Info.SectorID,
                        //SectorTypeId = Info.SectorTypeId,
                        //ProjectName = Info.ProjectName,
                        //WorkLatitude = Convert.ToDecimal(Info.WorkLatitude),
                        //WorkLongitude = Convert.ToDecimal(Info.WorkLongitude),
                        //ProjectDescription = Info.ProjectDescription,
                        //ProsposalNo = Info.ProsposalNo,
                        //ProposalDate = Info.ProposalDate,
                        //ProposalCopy = Info.ProposalCopy,
                        //ProposedBy = Info.ProposedBy,
                        //ProjectCost = Info.ProjectCost,
                        //GSTAndOthers = Info.GSTAndOthers,
                        //TenderNo = Info.TenderNo,
                        //TenderDate = Info.TenderDate,
                        //WorkOrderNo = Info.WorkOrderNo,
                        //WorkOrderDate = Info.WorkOrderDate,
                        //WorkOrderCopy = Info.WorkOrderCopy,
                        //AgencyID = Info.AgencyID,
                        //SanctionedProjectCost = Info.SanctionedProjectCost,
                        ProjectPreparationID = CryptoEngine.Encrypt(Info.ProjectPreparationID.ToString())
                    });
                }
                else
                {
                    return RedirectToAction("Login", "Account");
                }
            }
            catch (Exception)
            {
                return RedirectToAction("Login", "Account");
            }
        }
        [HttpPost]
        public JsonResult EditProjectProposalPrepration(DTO_ProjectProposalPrepration model)
        {
            JsonResponse JR = new JsonResponse();
            if (!ModelState.IsValid)
            {
                JR.Data = ModelState.Select(x => x.Value.Errors).Where(y => y.Count > 0).ToList();
                return Json(JR, JsonRequestBehavior.AllowGet);
            }
            if (!String.IsNullOrEmpty(model.ProjectPreparationID))
            {
                long _id = long.Parse(CryptoEngine.Decrypt(model.ProjectPreparationID));
                if (!String.IsNullOrEmpty(model.ProposalCopy))
                {
                    if (model.ProposalCopy != "prev")
                    {
                        model.ProposalCopy = BusinessLogics.UploadFileDMF(model.ProposalCopy);
                        if (model.ProposalCopy.Contains("Expp::"))
                        {
                            JR.Message = model.ProposalCopy;
                            return Json(JR, JsonRequestBehavior.AllowGet);
                        }
                    }
                }
                if (!String.IsNullOrEmpty(model.WorkOrderCopy))//added by ramdhyan 03.04.2024
                {
                    if (model.WorkOrderCopy != "prev")
                    {
                        model.WorkOrderCopy = BusinessLogics.UploadFileDMF(model.WorkOrderCopy);
                        if (model.WorkOrderCopy.Contains("Expp::"))
                        {
                            JR.Message = model.WorkOrderCopy;
                            return Json(JR, JsonRequestBehavior.AllowGet);
                        }
                    }
                }
                var Info = db.ProjectProposalPreprations.Where(x => x.ProjectPreparationID == _id).FirstOrDefault();
                if (Info != null)
                {
                
                   
                    
                  
                    
                    //    AgencyID = Info.AgencyID,
                    //    SanctionedProjectCost = Info.SanctionedProjectCost,
                    Info.DistID = model.DistID;
                    Info.TehsilId = model.TehsilId;
                    Info.BlockId = model.BlockId;
                    Info.VillageId = model.VillageId;
                    Info.SectorID = model.SectorID;
                    Info.SectorTypeId = model.SectorTypeId;
                    Info.ProjectName = model.ProjectName;
                    Info.WorkLatitude = model.WorkLatitude;
                    Info.WorkLongitude = model.WorkLongitude;
                    Info.ProjectDescription = model.ProjectDescription;
                    Info.ProposalDate = model.ProposalDate;
                    Info.ProposalCopy = (model.ProposalCopy != null && model.ProposalCopy == "prev") ? Info.ProposalCopy : model.ProposalCopy;
                    Info.ProposedBy = model.ProposedBy;
                    Info.ProsposalNo = model.ProsposalNo;
                    Info.ProjectCost = model.ProjectCost;
                    Info.GSTAndOthers = model.GSTAndOthers;
                    Info.TenderDate = model.TenderDate;
                    Info.TenderNo = model.TenderNo;
                    Info.WorkOrderDate = model.WorkOrderDate;
                    Info.WorkOrderNo = model.WorkOrderNo;
                    //Info.ProjectID = model.ProjectID;
                    //Info.ProjectStatusID = model.ProjectStatusID;
                    Info.WorkOrderCopy = (model.WorkOrderCopy != null && model.WorkOrderCopy == "prev") ? Info.WorkOrderCopy : model.WorkOrderCopy;
                    Info.AgencyID = model.AgencyID;
                    Info.SanctionedProjectCost = model.SanctionedProjectCost;
                    Info.ModifyBy = UserManager.GetUserLoginInfo(User.Identity.Name).LoginID;
                    Info.ModifyDate = DateTime.Now;
                }
            }
            int res = db.SaveChanges();
            if (res > 0)
            {
                JR.IsSuccess = true;
                JR.Message = "Data Updated Successfully";
                JR.RedURL = "/ProjectWorkApproval/ProjectProposalPrepration";
            }
            else
            {
                JR.Message = "Some Error Occured, Contact to Admin";
            }
            return Json(JR, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult DeleteProjectProposalPrepration(string id)
        {
            JsonResponse JR = new JsonResponse();
            if (!String.IsNullOrEmpty(id))
            {
                long _id = long.Parse(CryptoEngine.Decrypt(id));
                //long _id = long.Parse(id);  use this code when u use server side datatable for display
                var Info = db.ProjectProposalPreprations.Where(x => x.ProjectPreparationID == _id).FirstOrDefault();
                if (Info != null)
                {
                    db.ProjectProposalPreprations.Remove(Info);
                }
            }
            int res = db.SaveChanges();
            if (res > 0)
            {
                JR.IsSuccess = true;
                JR.Message = "Data Deleted Successfully";
                JR.RedURL = "/ProjectWorkApproval/ProjectProposalPrepration";
            }
            else
            {
                JR.Message = "Some Error Occured, Contact to Admin";
            }
            return Json(JR, JsonRequestBehavior.AllowGet);
        }
        #endregion
        #region Ramdhyan 30.03.2024 for server side listing on the projectproposal page
        //public JsonResult GetProjectProposalPreprationList(int? DistID, int? AgencyID, long? ProjectID, int? SectorID)
        //{
        //    if (UserManager.GetUserLoginInfo(User.Identity.Name).RoleID == 2)
        //    {
        //        DistID = UserManager.GetUserLoginInfo(User.Identity.Name).DistID;
        //    }
        //    List<DTO_ProjectProposalPrepration> lst = new List<DTO_ProjectProposalPrepration>();
        //    using (SqlConnection con = new SqlConnection(coonectionstrings))
        //    using (SqlCommand cmd = new SqlCommand("sp_GetProjectProposalPreprationList", con))
        //    {
        //        cmd.CommandType = System.Data.CommandType.StoredProcedure;
        //        cmd.Parameters.AddWithValue("@DistID", DistID);
        //        cmd.Parameters.AddWithValue("@AgencyID", AgencyID);
        //        cmd.Parameters.AddWithValue("@ProjectID", ProjectID);
        //        cmd.Parameters.AddWithValue("@SectorID", SectorID);
        //        con.Open();
        //        SqlDataReader dr = cmd.ExecuteReader();
        //        while (dr.Read())
        //        {
        //            lst.Add(new DTO_ProjectProposalPrepration
        //            {
        //                AgencyName = dr["Name"].ToString(),
        //                DistrictName = dr["DistrictName"].ToString(),
        //                GSTAndOthers = Convert.ToDecimal(dr["GSTAndOthers"]),
        //                ProjectCost = Convert.ToDecimal(dr["ProjectCost"]),
        //                ProjectName = dr["ProjectName"].ToString(),
        //                ProjectStatus = dr["ProjectStatus"].ToString(),
        //                ProjectPreparationID = dr["ProjectPreparationID"].ToString(),
        //                ProposalCopy = dr["ProposalCopy"].ToString(),
        //                ProposalDate = Convert.ToDateTime(dr["ProposalDate"]),
        //                ProposedBy = dr["ProposedBy"].ToString(),
        //                ProsposalNo = dr["ProsposalNo"].ToString(),
        //                SectorName = dr["SectorName"].ToString(),
        //                SanctionedProjectCost = Convert.ToDecimal(dr["SanctionedProjectCost"]),
        //                TenderDate = Convert.ToDateTime(dr["TenderDate"]),
        //                TenderNo = dr["TenderNo"].ToString(),
        //                WorkOrderDate = Convert.ToDateTime(dr["WorkOrderDate"]),
        //                WorkOrderNo = dr["WorkOrderNo"].ToString()
        //            });
        //        }
        //        con.Close();
        //    }

        //    return Json(lst, JsonRequestBehavior.AllowGet);
        //}
        public ActionResult ViewAndApproveProjectProposal(string id)//added by ramdhyan 04.04.2024
        {
            if (String.IsNullOrEmpty(id))
            {
                return RedirectToAction("Login", "Account");
            }
            try
            {
                long _id = long.Parse(CryptoEngine.Decrypt(id));
                //long _id = long.Parse(id);  use this code when u use server side datatable for display
                var Info = db.ProjectProposalPreprations.Where(x => x.ProjectPreparationID == _id).FirstOrDefault();
                if (Info != null)
                {
                    int? DistID = null;
                    if (UserManager.GetUserLoginInfo(User.Identity.Name).RoleID == 2)
                    {
                        DistID = UserManager.GetUserLoginInfo(User.Identity.Name).DistID;
                    // var Tehsil = db.TehsilMasters.Where(x => x.DistrictId == DistID).FirstOrDefault();
                    ViewBag.DistID = new SelectList(db.DistrictMasters.Where(x => x.IsActive == true && x.DistrictId == (DistID == null ? x.DistrictId : DistID)), "DistrictId", "DistrictName", null);
                    //ViewBag.TehsilId = new SelectList(db.TehsilMasters.Where(x => x.IsActive == true && x.DistrictId == (DistID == null ? x.DistrictId : DistID)), "TehsilId", "TehsilName", null);
                    //ViewBag.BlockId = new SelectList(db.BlockMasters.Where(x => x.IsActive == true && x.DistrictId == (DistID == null ? x.DistrictId : DistID)), "BlockId", "BlockName", null);
                    ViewBag.VillageId = new SelectList(db.VillageMasters.Where(x => x.IsActive == true && x.TehsilId == (Info.TehsilId == null ? x.TehsilId : Info.TehsilId)), "VillageId", "VillageNameInEnglish", null);
                    ViewBag.AgencyID = new SelectList(db.AgenciesInfoes.Where(x => x.IsActive == true && x.DistID == (DistID == null ? x.DistID : DistID)).ToList().Select(x => new { ID = x.AgencyID, Text = x.Name + " / " + x.OwnerName }), "ID", "Text", Info.AgencyID);
                    ViewBag.ProjectID = new SelectList(db.ProjectMasters.Where(x => x.IsActive == true && x.DistID == (DistID == null ? x.DistID : DistID)).ToList().Select(x => new { ID = x.ProjectID, Text = x.ProjectName + " (" + x.ProjectCode + ")" }), "ID", "Text", Info.ProjectID);
                    ViewBag.SectorTypeId = new SelectList(db.SectorTypeMasters.Where(x => x.IsActive == true && x.SectorTypeID == (Info.SectorTypeId == null ? x.SectorTypeID : Info.SectorTypeId)), "SectorTypeId", "SectorType", null).ToList();
                    ViewBag.SectorID = new SelectList(db.SectorNameMasters.Where(x => x.IsActive == true), "SectorNameId", "SectorName", Info.SectorID);
                    // ViewBag.ProjectStatusID = new SelectList(db.ProjectStatusMasters.Where(x => x.IsActive == true), "ProjectStatusID", "ProjectStatus", Info.ProjectStatusID);
                    return View(new DTO_ProjectProposalPrepration
                    {
                        CreatedOn = DateTime.Now,
                        IsActive = true,
                        CreatedBy = UserManager.GetUserLoginInfo(User.Identity.Name).LoginID,
                        DistID = Info.DistID,
                        TehsilId = Info.TehsilId,
                        BlockId = Info.BlockId,
                        VillageId = Info.VillageId,
                        SectorID = Info.SectorID,
                        SectorTypeId = Info.SectorTypeId,
                        ProjectName = Info.ProjectName,
                        WorkLatitude = Convert.ToDecimal(Info.WorkLatitude),
                        WorkLongitude = Convert.ToDecimal(Info.WorkLongitude),
                        ProjectDescription = Info.ProjectDescription,
                        ProsposalNo = Info.ProsposalNo,
                        ProposalDate = Info.ProposalDate,
                        ProposalCopy = Info.ProposalCopy,
                        ProposedBy = Info.ProposedBy,
                        ProjectCost = Info.ProjectCost,
                        GSTAndOthers = Info.GSTAndOthers,
                        TenderNo = Info.TenderNo,
                        TenderDate = Info.TenderDate,
                        WorkOrderNo = Info.WorkOrderNo,
                        WorkOrderDate = Info.WorkOrderDate,
                        WorkOrderCopy = Info.WorkOrderCopy,
                        AgencyID = Info.AgencyID,
                        SanctionedProjectCost = Info.SanctionedProjectCost,
                        ProjectPreparationID = CryptoEngine.Encrypt(Info.ProjectPreparationID.ToString())
                    });
                    }
                    else
                    {
                        return RedirectToAction("Login", "Account");
                    }
                }
                else
                {
                    return RedirectToAction("Login", "Account");
                }
            }
            catch (Exception)
            {
                return RedirectToAction("Login", "Account");
            }
        }
         [HttpGet]
        public ActionResult ViewDetails(string id)//added by ramdhyan 05.04.2024 for view details on the modal popup usinf project preparation id
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
                   // int? DistID = null;
                    if (UserManager.GetUserLoginInfo(User.Identity.Name).RoleID == 2 || UserManager.GetUserLoginInfo(User.Identity.Name).RoleID == 1)
                    {
                      //  DistID = UserManager.GetUserLoginInfo(User.Identity.Name).DistID;
                        var LstData = (from ppp in db.ProjectProposalPreprations
                                       join dm in db.DistrictMasters on ppp.DistID equals dm.DistrictId
                                       join tm in db.TehsilMasters on ppp.TehsilId equals tm.TehsilId
                                       join bm in db.BlockMasters on ppp.BlockId equals bm.BlockId
                                       join vm in db.VillageMasters on ppp.VillageId equals vm.VillageId
                                       join snm in db.SectorNameMasters on ppp.SectorID equals snm.SectorNameId
                                       join stm in db.SectorTypeMasters on ppp.SectorTypeId equals stm.SectorTypeID
                                       join ag in db.AgenciesInfoes on ppp.AgencyID equals ag.AgencyID
                                       //join pm in db.ProposalStatusMasters on ppp.ProjectPreparationID equals pm.ProjectID
                                       where ppp.IsActive == true && ppp.ProjectPreparationID ==Info.ProjectPreparationID
                                       select new DTO_ProjectProposalPrepration
                                       {
                                           DistrictName = dm.DistrictName,
                                           TehsilName=tm.TehsilName,
                                           BlockName=bm.BlockName,
                                           VillageNameInEnglish=vm.VillageNameInEnglish,
                                           VillageNameInHindi=vm.VillageNameInHindi,
                                           SectorType=stm.SectorType,
                                           SectorName=snm.SectorName,
                                           GSTAndOthers = ppp.GSTAndOthers,
                                           ProjectCost = ppp.ProjectCost,
                                           ProjectName = ppp.ProjectName,
                                           WorkLatitude=ppp.WorkLatitude,
                                           WorkLongitude=ppp.WorkLongitude,
                                           ProjectDescription=ppp.ProjectDescription,
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
                                           WorkOrderCopy=ppp.WorkOrderCopy,
                                           AgencyName = ag.Name,
                                           SanctionedProjectCost = ppp.SanctionedProjectCost
                                       }).ToList();

                        return Json(LstData, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        return RedirectToAction("Login", "Account");
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
        #endregion
        [HttpPost]
        //public JsonResult BindSectorType(int SectorTypeId)
        //{
        //    var data = db.SectorNameMasters.Where(x => x.SectorTypeId == SectorTypeId).ToList();
        //    return Json(data, JsonRequestBehavior.AllowGet);
        //}
        public JsonResult BindSectorType(int SectorTypeId)
        {
            var lstData = (from snm in db.SectorNameMasters


                           where snm.IsActive == true && snm.SectorTypeId == SectorTypeId
                           select new DTO_ProjectMaster
                           {
                               SectorNameId = snm.SectorNameId,

                               SectorName = snm.SectorName,


                           }

                                         ).ToList();

            return Json(lstData, JsonRequestBehavior.AllowGet);


        }

        public JsonResult BindAgencey(int? Districtid)
        {

            var lstData =
            (
                from t in db.AgenciesInfoes
                where t.IsActive == true && t.DistID == Districtid
                select new DTO_ProjectProposalPrepration
                {
                    AgencyID = t.AgencyID,
                    AgencyName = t.Name + " / " + t.OwnerName

                }


            ).ToList();

            return Json(lstData, JsonRequestBehavior.AllowGet);

        }





        public JsonResult BindTeshil(int? Districtid)
        {

            var lstData =
            (
                from t in db.TehsilMasters
                where t.IsActive == true && t.DistrictId == Districtid
                select new DTO_ProjectProposalPrepration
                {
                    TehsilId = t.TehsilId,
                    TehsilName = t.TehsilName

                }


            ).ToList();

            return Json(lstData, JsonRequestBehavior.AllowGet);

        }

        public JsonResult BindBlock(int? Districtid)
        {

            var lstData =
            (
                from t in db.BlockMasters
                where t.IsActive == true && t.DistrictId == Districtid
                select new DTO_ProjectProposalPrepration
                {
                    BlockId = t.BlockId,
                    BlockName = t.BlockName

                }


            ).ToList();

            return Json(lstData, JsonRequestBehavior.AllowGet);

        }

        public JsonResult BindVillage(int? TehsilId)
        {

            var lstData =
            (
                from t in db.VillageMasters
                where t.IsActive == true && t.TehsilId == TehsilId
                select new DTO_ProjectProposalPrepration
                {
                    VillageId = t.VillageId,
                    VillageNameInEnglish =t.VillageNameInEnglish

                }


            ).ToList();

            return Json(lstData, JsonRequestBehavior.AllowGet);

        }






    }
}