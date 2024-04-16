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

    public class UtilizationController : Controller
    {
        // GET: Utilization
        private dfm_dbEntities db = new dfm_dbEntities();
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult UtilizationList()
        {
            var data = (from uc in db.UtilizationMasters
                        join
                            pm in db.ProjectMasters on uc.ProjectNo equals pm.ProjectNo
                        join dm in db.DistrictMasters on uc.DistrictID equals dm.DistrictId
                        select new DTO_UtilizationMaster
                        {
                            DistrictName = dm.DistrictName,
                            ProjectName = pm.ProjectName,
                            DistrictID = uc.DistrictID,
                            UtilizationID = uc.UtilizationID.ToString(),
                            UtilizationCopy = uc.UtilizationCopy,
                            UtilizationDate = uc.UtilizationDate,
                            Remarks = uc.Remarks,
                            UtilizationNo = uc.UtilizationNo

                        }).ToList();
            ViewBag.LstData = data;
            return View();
        }
        public ActionResult CreateUtilization()
        {
            int? DistID = null;
            if (UserManager.GetUserLoginInfo(User.Identity.Name).RoleID == 2)
            {
                DistID = UserManager.GetUserLoginInfo(User.Identity.Name).DistID;
            }
            DTO_UtilizationMaster model = new DTO_UtilizationMaster();
            ViewBag.ProjectID = new SelectList((from ph in db.PhysicalProgressMasters
                                                join pm in db.ProjectMasters on ph.ProjectNo equals pm.ProjectNo into pms_left
                                                from pm in pms_left.DefaultIfEmpty()
                                                where pm.IsActive == true && ph.DistrictID == DistID && ph.ProjectNo == pm.ProjectNo && ph.Phyicalintsallmentflag!=null
                                                select new
                                                {
                                                    ProjectNo = ph.ProjectNo,
                                                    ProjectName = pm.ProjectName
                                                }).Distinct(), "ProjectNo", "ProjectName", null);

            model.DistrictID = DistID;
            return View(model);
        }
        [HttpPost]
        public JsonResult CreateUtilization(DTO_UtilizationMaster model)
        {
            string msg = string.Empty;
            JsonResponse JR = new JsonResponse();
            if (!ModelState.IsValid)
            {
                JR.Data = ModelState.Select(x => x.Value.Errors).Where(y => y.Count > 0).ToList();
                return Json(JR, JsonRequestBehavior.AllowGet);
            }
            var physicalflag = db.PhysicalProgressMasters.Where(x => x.ProjectNo == model.ProjectNo && x.DistrictID == model.DistrictID && x.Phyicalintsallmentflag != null).FirstOrDefault();

            if (db.UtilizationMasters.Where(x => x.DistrictID == model.DistrictID && x.ProjectNo == model.ProjectNo && x.PhysicalProgressID== physicalflag.PhysicalprogressID).Any())
            {
                msg ="Utilization Certificate  for "+ model.ProjectNo + " Aready Uploaded for this Progress !";
                return Json(msg, JsonRequestBehavior.AllowGet);
            }
            if (!String.IsNullOrEmpty(model.UtilizationCopy))
            {
                model.UtilizationCopy = BusinessLogics.UploadFileDMF(model.UtilizationCopy);
                if (model.UtilizationCopy.Contains("Expp::"))
                {
                    msg = model.UtilizationCopy;
                    return Json(msg, JsonRequestBehavior.AllowGet);
                }
            }
            db.UtilizationMasters.Add(new UtilizationMaster
            {
                DistrictID = model.DistrictID,
                ProjectNo = model.ProjectNo,
                UtilizationDate = model.UtilizationDate,
                UtilizationNo = model.UtilizationNo,
                UtilizationCopy = model.UtilizationCopy,
                Remarks = model.Remarks,
                CreatedBy = UserManager.GetUserLoginInfo(User.Identity.Name).LoginID.ToString(),
                CreatedDate = DateTime.Now,
                IsActive=true,
                Phyicalintsallmentflag = physicalflag.Phyicalintsallmentflag,
                ProjectPreparationID= physicalflag.ProjectPreparationID,
                PhysicalProgressID=physicalflag.PhysicalprogressID,
                FundReleaseID=physicalflag.FundReleaseID
            });
            var res = db.SaveChanges();
            if (res > 0)
            {
                msg = "1";
            }
            else
            {
                msg = "0";
            }
            return Json(msg, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public ActionResult EditUtilization(int UtilizationID)
        {
            int? DistID = null;
            if (UserManager.GetUserLoginInfo(User.Identity.Name).RoleID == 2)
            {
                DistID = UserManager.GetUserLoginInfo(User.Identity.Name).DistID;
            }
            var data = db.UtilizationMasters.Where(x => x.UtilizationID == UtilizationID).FirstOrDefault();

            ViewBag.ProjectID = new SelectList((from ph in db.PhysicalProgressMasters
                                                join pm in db.ProjectMasters on ph.ProjectNo equals pm.ProjectNo into pms_left
                                                from pm in pms_left.DefaultIfEmpty()
                                                where pm.IsActive == true && ph.DistrictID == DistID && ph.ProjectNo == pm.ProjectNo
                                                select new
                                                {
                                                    ProjectNo = ph.ProjectNo,
                                                    ProjectName = pm.ProjectName
                                                }).Distinct(), "ProjectNo", "ProjectName", data.ProjectNo);
            //return View("~/Views/FundRelease/CreateFundRelease.cshtml", new DTO_FundRelease { FundReleaseID = Info.FundReleaseID.ToString(), RelaeseDate = Info.RelaeseDate, ReleaseAmount = Info.ReleaseAmount, InstallmentID = Info.InstallmentID, FundReleaseCopy = Info.FundReleaseCopy, DistrictID = Info.DistrictID });

            return View("~/Views/Utilization/CreateUtilization.cshtml",new DTO_UtilizationMaster { UtilizationID =data.UtilizationID.ToString(),UtilizationNo=data.UtilizationNo,DistrictID=data.DistrictID,ProjectNo=data.ProjectNo,UtilizationCopy=data.UtilizationCopy,UtilizationDate=data.UtilizationDate,Remarks=data.Remarks});
        }
        [HttpPost]
        public JsonResult UpdateUtilization(DTO_UtilizationMaster model)
        {
            string msg = string.Empty;
            JsonResponse JR = new JsonResponse();
            if (!ModelState.IsValid)
            {
                JR.Data = ModelState.Select(x => x.Value.Errors).Where(y => y.Count > 0).ToList();
                return Json(JR, JsonRequestBehavior.AllowGet);
            }
            if (!String.IsNullOrEmpty(model.UtilizationCopy))
            {
                model.UtilizationCopy = BusinessLogics.UploadFileDMF(model.UtilizationCopy);
                if (model.UtilizationCopy.Contains("Expp::"))
                {
                    msg = model.UtilizationCopy;
                    return Json(msg, JsonRequestBehavior.AllowGet);
                }
            }
            var Utilization = Convert.ToInt32(model.UtilizationID);
            var data = db.UtilizationMasters.Where(x => x.UtilizationID == Utilization).FirstOrDefault();
                data.ProjectNo = model.ProjectNo;
                data.UtilizationDate = model.UtilizationDate;
                data.UtilizationNo = model.UtilizationNo;
                data.UtilizationCopy = model.UtilizationCopy;
                data.Remarks = model.Remarks;
                data.ModifiedBy = UserManager.GetUserLoginInfo(User.Identity.Name).LoginID.ToString();
                data.ModifiedDate = DateTime.Now;
            
            db.Entry(data).State = System.Data.Entity.EntityState.Modified;
            var res = db.SaveChanges();
            if (res > 0)
            {
                msg = "1";
            }
            else
            {
                msg = "0";
            }
            return Json(msg, JsonRequestBehavior.AllowGet);
        }

        public JsonResult DeleteUtilization(int UtilizationID)
        {
            JsonResponse JR = new JsonResponse();
            if (UtilizationID > 0)
            {
                var Info = db.UtilizationMasters.Where(x => x.UtilizationID == UtilizationID).FirstOrDefault();
                if (Info != null)
                {
                    db.UtilizationMasters.Remove(Info);
                }
            }
            int res = db.SaveChanges();
            if (res > 0)
            {
                JR.IsSuccess = true;
                JR.Message = "Data Deleted Successfully";
                JR.RedURL = "/Utilization/UtilizationList";
            }
            else
            {
                JR.Message = "Some Error Occured, Contact to Admin";
            }
            return Json(JR, JsonRequestBehavior.AllowGet);
        }
    }
}