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
    public class PhysicalProgressController : Controller
    {
        private dfm_dbEntities db = new dfm_dbEntities();

        // GET: PhysicalProgress
        public ActionResult PhysicalProgressList()
        {
            int? DistID = null;
            if (UserManager.GetUserLoginInfo(User.Identity.Name).RoleID == 2)
            {
                DistID = UserManager.GetUserLoginInfo(User.Identity.Name).DistID;

                var data = (from fr in db.PhysicalProgressMasters
                            join dm in db.DistrictMasters on fr.DistrictID equals dm.DistrictId
                            join pm in db.ProjectMasters on fr.ProjectNo equals pm.ProjectNo
                            //join ins in db.InstallmentMasters on ppp.InstallmkentID equals ins.InstallmentID
                            join stm in db.SectorTypeMasters on pm.SectorTypeId equals stm.SectorTypeID
                            join snm in db.SectorNameMasters on pm.SectorNameId equals snm.SectorNameId
                            join ppp in db.ProjectProposalPreprations on fr.ProjectNo equals ppp.ProjectNo
                            where fr.IsActive == true && fr.DistrictID == DistID && fr.ProjectNo != null 
                            select new DTO_PhysicalProgressMaster
                            {
                                FundReleaseID = fr.FundReleaseID,
                                DistrictID = fr.DistrictID,
                                ProjectNo = fr.ProjectNo,
                                DistrictName = dm.DistrictName,
                                ProjectName = pm.ProjectName,
                                PhysicalProgressDate = fr.PhysicalProgressDate,
                                Remark = fr.Remark,
                                PhysicalProgressCopy = fr.PhysicalProgressCopy,
                                PhysicalPInPer = fr.PhysicalPInPer,
                                //InstallmentName = ins.InstallmentName,
                                AmountSpend = fr.AmountSpend,
                                SectorType = stm.SectorType,
                                SectorName=snm.SectorName,
                                PhysicalprogressID=fr.PhysicalprogressID.ToString()
                                //SanctionedProjectCost = ppp.SanctionedProjectCost
                            }).ToList();
                ViewBag.LstData = data;
            }
            else
            {
                var data = (from fr in db.PhysicalProgressMasters
                            join dm in db.DistrictMasters on fr.DistrictID equals dm.DistrictId
                            join pm in db.ProjectMasters on fr.ProjectNo equals pm.ProjectNo
                            //join ins in db.InstallmentMasters on ppp.InstallmkentID equals ins.InstallmentID
                            join stm in db.SectorTypeMasters on pm.SectorTypeId equals stm.SectorTypeID
                            join snm in db.SectorNameMasters on pm.SectorNameId equals snm.SectorNameId
                            join ppp in db.ProjectProposalPreprations on fr.ProjectNo equals ppp.ProjectNo
                            where fr.IsActive == true && fr.ProjectNo != null
                            select new DTO_PhysicalProgressMaster
                            {
                                FundReleaseID = fr.FundReleaseID,
                                DistrictID = fr.DistrictID,
                                ProjectNo = fr.ProjectNo,
                                DistrictName = dm.DistrictName,
                                ProjectName = pm.ProjectName,
                                PhysicalProgressDate = fr.PhysicalProgressDate,
                                Remark = fr.Remark,
                                PhysicalProgressCopy = fr.PhysicalProgressCopy,
                                PhysicalPInPer = fr.PhysicalPInPer,
                                //InstallmentName = ins.InstallmentName,
                                AmountSpend = fr.AmountSpend,
                                SectorType = stm.SectorType,
                                SectorName = snm.SectorName,
                                PhysicalprogressID = fr.PhysicalprogressID.ToString()
                                //SanctionedProjectCost = ppp.SanctionedProjectCost
                            }).ToList();
                ViewBag.LstData = data;
            }
                return View();
        }

        public ActionResult CreatePhysicalProgress()
        {
            int? DistID = null;
            if (UserManager.GetUserLoginInfo(User.Identity.Name).RoleID == 2)
            {
                DistID = UserManager.GetUserLoginInfo(User.Identity.Name).DistID;
            }
            DTO_PhysicalProgressMaster model = new DTO_PhysicalProgressMaster();
            //ViewBag.ProjectID = new SelectList(db.MileStoneMasters.Where(x => x.IsActive == true && x.ProjectNo != null  && x.DistrictID == (DistID == null ? x.DistrictID : DistID)), "ProjectNo", "ProjectName", null);
            ViewBag.ProjectID = new SelectList((from ms in db.MileStoneMasters
                                               join fr in db.FundReleases on ms.ProjectNo equals fr.ProjectNo
                                               join ppp in db.ProjectProposalPreprations on ms.ProjectNo equals ppp.ProjectNo
                                               where ms.IsActive == true && ppp.DistID == ms.DistrictID

                                               select new DTO_MileStoneMaster
                                               {
                                                   ProjectNo = ms.ProjectNo,
                                                   ProjectName = ppp.ProjectName
                                               }
                                             ), "ProjectNo", "ProjectName", null);
            model.DistrictID = DistID;
            return View(model);
        }

        [HttpPost]
        public ActionResult CreatePhysicalProgress(DTO_PhysicalProgressMaster model)
        {
            JsonResponse JR = new JsonResponse();
            if (!ModelState.IsValid)
            {
                JR.Data = ModelState.Select(x => x.Value.Errors).Where(y => y.Count > 0).ToList();
                return Json(JR, JsonRequestBehavior.AllowGet);

            }
            var milestone = db.MileStoneMasters.Where(x => x.ProjectNo == model.ProjectNo && x.DistrictID == model.DistrictID).FirstOrDefault();
            if (db.PhysicalProgressMasters.Where(x => x.DistrictID == model.DistrictID && x.ProjectNo == model.ProjectNo && x.MileStoneID == milestone.MileStoneID).Any())
            {
                JR.Message =  "Physical Progress Aready Created  for this Milestone !";
                return Json(JR, JsonRequestBehavior.AllowGet);
            }
            if (!String.IsNullOrEmpty(model.PhysicalProgressCopy))
            {
                model.PhysicalProgressCopy = BusinessLogics.UploadFileDMF(model.PhysicalProgressCopy);
                if (model.PhysicalProgressCopy.Contains("Expp::"))
                {
                    JR.Message = model.PhysicalProgressCopy;
                    return Json(JR, JsonRequestBehavior.AllowGet);
                }
            }
            //var milestone = db.MileStoneMasters.Where(x => x.ProjectNo == model.ProjectNo && x.DistrictID == model.DistrictID).FirstOrDefault();
            db.PhysicalProgressMasters.Add(new PhysicalProgressMaster
            {
                DistrictID = model.DistrictID,
                ProjectID = model.ProjectID,
                ProjectNo = model.ProjectNo,
                MileStoneID = milestone.MileStoneID,
                FundReleaseID = milestone.FundReleaseID,
                ProjectPreparationID = milestone.ProjectPreparationID,
                PhysicalProgressDate = model.PhysicalProgressDate,
                Remark = model.Remark,
                PhysicalProgressCopy = model.PhysicalProgressCopy,
                PhysicalPInPer = model.PhysicalPInPer,
                AmountSpend = model.AmountSpend,
                CreatedDate = DateTime.Now,
                CreatedBy = UserManager.GetUserLoginInfo(User.Identity.Name).LoginID.ToString(),
                IsActive=true
                            });
            int res = db.SaveChanges();
            if (res > 0)
            {
                JR.IsSuccess = true;
                JR.Message = "Data Saved Successfully";
                JR.RedURL = "/PhysicalProgress/PhysicalProgressList";
            }
            else
            {
                JR.Message = "Some Error Occured, Contact to Admin";
            }
            return Json(JR, JsonRequestBehavior.AllowGet);
          
        }
        [HttpGet]
        public ActionResult EditPhysicalProgress(int PhysicalProgressID)
        {
            if (PhysicalProgressID == 0)
            {
                return RedirectToAction("Login", "Account");
            }
            int? DistID = null;
            if (UserManager.GetUserLoginInfo(User.Identity.Name).RoleID == 2)
            {
                DistID = UserManager.GetUserLoginInfo(User.Identity.Name).DistID;
            }
            try
            {
                var Info = db.PhysicalProgressMasters.Where(x => x.PhysicalprogressID == PhysicalProgressID).FirstOrDefault();
                if (Info != null)
                {
                    ViewBag.ProjectID = new SelectList((from ms in db.MileStoneMasters
                                                        join fr in db.FundReleases on ms.ProjectNo equals fr.ProjectNo
                                                        join ppp in db.ProjectProposalPreprations on ms.ProjectNo equals ppp.ProjectNo
                                                        where ms.IsActive == true && ppp.DistID == ms.DistrictID
                                                        select new DTO_MileStoneMaster
                                                        {
                                                            ProjectNo = ms.ProjectNo,
                                                            ProjectName = ppp.ProjectName
                                                        }
                                           ), "ProjectNo", "ProjectName", Info.ProjectNo);
                    return View("~/Views/PhysicalProgress/CreatePhysicalProgress.cshtml", new DTO_PhysicalProgressMaster { PhysicalprogressID = Info.PhysicalprogressID.ToString(), PhysicalProgressDate = Info.PhysicalProgressDate, AmountSpend = Info.AmountSpend, PhysicalPInPer = Info.PhysicalPInPer, PhysicalProgressCopy = Info.PhysicalProgressCopy, DistrictID = Info.DistrictID,Remark=Info.Remark });
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
        [HttpPost]
        public JsonResult EditPhysicalProgress(DTO_PhysicalProgressMaster model)
        {
            JsonResponse JR = new JsonResponse();
            if (!ModelState.IsValid)
            {
                JR.Data = ModelState.Select(x => x.Value.Errors).Where(y => y.Count > 0).ToList();
                return Json(JR, JsonRequestBehavior.AllowGet);
            }
            if (!String.IsNullOrEmpty(model.PhysicalprogressID))
            {
                //if (db.FundReleases.Where(x => x.DistrictID == model.DistrictID && x.ProjectNo == model.ProjectNo && x.InstallmentID == model.InstallmentID).Any())
                //{
                //    JR.Message = model.InstallmentID + "st Installment Aready Release for this Project !";
                //    return Json(JR, JsonRequestBehavior.AllowGet);
                //}
                if (!String.IsNullOrEmpty(model.PhysicalProgressCopy))
                {
                    if (model.PhysicalProgressCopy != "prev")
                    {
                        model.PhysicalProgressCopy = BusinessLogics.UploadFileDMF(model.PhysicalProgressCopy);
                        if (model.PhysicalProgressCopy.Contains("Expp::"))
                        {
                            JR.Message = model.PhysicalProgressCopy;
                            return Json(JR, JsonRequestBehavior.AllowGet);
                        }
                    }
                }
                int id = Convert.ToInt32(model.PhysicalprogressID);
                var Info = db.PhysicalProgressMasters.Where(x => x.PhysicalprogressID == id).FirstOrDefault();
                if (Info != null)
                {



                 Info.DistrictID = model.DistrictID;
                 Info.ProjectID = model.ProjectID;
                 Info.ProjectNo = model.ProjectNo;
                 //Info.MileStoneID = milestone.MileStoneID;
                 //Info.FundReleaseID = milestone.FundReleaseID;
                 //Info.ProjectPreparationID = milestone.ProjectPreparationID;
                 Info.PhysicalProgressDate = model.PhysicalProgressDate;
                 Info.Remark = model.Remark;
                 //Info.PhysicalProgressCopy = model.PhysicalProgressCopy;
                 Info.PhysicalPInPer = model.PhysicalPInPer;
                 Info.AmountSpend = model.AmountSpend;
                 Info.CreatedDate = DateTime.Now;
                 Info.CreatedBy = UserManager.GetUserLoginInfo(User.Identity.Name).LoginID.ToString();
                 Info.PhysicalProgressCopy = (model.PhysicalProgressCopy != null && model.PhysicalProgressCopy == "prev") ? Info.PhysicalProgressCopy : model.PhysicalProgressCopy;
                 Info.ModifiedDate = DateTime.Now;
                 Info.IsActive = true;
                 Info.ModifiedBy = UserManager.GetUserLoginInfo(User.Identity.Name).LoginID.ToString();
                }
            }
            int res = db.SaveChanges();
            if (res > 0)
            {
                JR.IsSuccess = true;
                JR.Message = "Data Updated Successfully";
                JR.RedURL = "/PhysicalProgress/PhysicalProgressList";
            }
            else
            {
                JR.Message = "Some Error Occured, Contact to Admin";
            }
            return Json(JR, JsonRequestBehavior.AllowGet);
        }
        public JsonResult BindMileStoneData(int DistrictID, string ProjectNo)
        {
            var data = (from ms in db.MileStoneMasters
                        join fr in db.FundReleases on ms.FundReleaseID equals fr.FundReleaseID
                        join dm in db.DistrictMasters on ms.DistrictID equals dm.DistrictId
                        join insM in db.InstallmentMasters on ms.InstallmentID equals insM.InstallmentID
                        join pm in db.ProjectMasters on ms.ProjectNo equals pm .ProjectNo
                        where ms.IsActive == true && ms.DistrictID == DistrictID && ms.ProjectNo == ProjectNo
                        select new DTO_PhysicalProgressMaster
                        {
                            MileStoneID = ms.MileStoneID,
                            FundReleaseID = ms.FundReleaseID,
                            ProjectNo = ms.ProjectNo,
                            DistrictID = ms.DistrictID,
                            DistrictName = dm.DistrictName,
                            Remark = ms.Instext,
                            Releaseperamount = ms.Releaseperamount,
                            ProjectPreparationID = ms.ProjectPreparationID,
                            CreatedDate=ms.CreatedDate,
                            ProjectName=pm.ProjectName,
                            InstallmentName=insM.InstallmentName,
                            MileStonePercentage=ms.InsPercentage

                        }).ToList();
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        public JsonResult DeletePhysicalProgress(int PhysicalprogressID)
        {
            JsonResponse JR = new JsonResponse();
            if (PhysicalprogressID > 0)
            {
                var Info = db.PhysicalProgressMasters.Where(x => x.PhysicalprogressID == PhysicalprogressID).FirstOrDefault();
                if (Info != null)
                {
                    db.PhysicalProgressMasters.Remove(Info);
                }
            }
            int res = db.SaveChanges();
            if (res > 0)
            {
                JR.IsSuccess = true;
                JR.Message = "Data Deleted Successfully";
                JR.RedURL = "/PhysicalProgress/PhysicalProgressList";
            }
            else
            {
                JR.Message = "Some Error Occured, Contact to Admin";
            }
            return Json(JR, JsonRequestBehavior.AllowGet);
        }
    }
}