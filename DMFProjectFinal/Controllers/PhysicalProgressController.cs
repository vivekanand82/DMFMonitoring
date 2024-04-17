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
        public ActionResult PhysicalProgressList(string ProjectName, string SectorName, string SectorType)
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
                            where fr.IsActive == true && fr.DistrictID == DistID && fr.ProjectNo != null && (pm.ProjectName.StartsWith(ProjectName) || String.IsNullOrEmpty(ProjectName)) && (snm.SectorName.StartsWith(SectorName) || String.IsNullOrEmpty(SectorName)) && (stm.SectorType.StartsWith(SectorType) || String.IsNullOrEmpty(SectorType))
                            select new DTO_PhysicalProgressMaster
                            {
                                ProjectPreparationID=fr.ProjectPreparationID,
                                DistrictID = fr.DistrictID,
                                ProjectNo = fr.ProjectNo,
                                DistrictName = dm.DistrictName,
                                ProjectName = pm.ProjectName,
                                SectorType = stm.SectorType,
                                SectorName=snm.SectorName,
                                SanctionedProjectCost = ppp.SanctionedProjectCost,
                            }).Distinct().ToList();
                ViewBag.LstData = data;
            }
            else
            {
                var data = (from fr in db.PhysicalProgressMasters
                            join dm in db.DistrictMasters on fr.DistrictID equals dm.DistrictId
                            join pm in db.ProjectMasters on fr.ProjectNo equals pm.ProjectNo
                            join stm in db.SectorTypeMasters on pm.SectorTypeId equals stm.SectorTypeID
                            join snm in db.SectorNameMasters on pm.SectorNameId equals snm.SectorNameId
                            join ppp in db.ProjectProposalPreprations on fr.ProjectNo equals ppp.ProjectNo
                            where fr.IsActive == true  && fr.ProjectNo != null && (pm.ProjectName.StartsWith(ProjectName) || String.IsNullOrEmpty(ProjectName)) && (snm.SectorName.StartsWith(SectorName) || String.IsNullOrEmpty(SectorName)) && (stm.SectorType.StartsWith(SectorType) || String.IsNullOrEmpty(SectorType))
                            select new DTO_PhysicalProgressMaster
                            {
                                ProjectPreparationID = fr.ProjectPreparationID,
                                DistrictID = fr.DistrictID,
                                ProjectNo = fr.ProjectNo,
                                DistrictName = dm.DistrictName,
                                ProjectName = pm.ProjectName,
                                SectorType = stm.SectorType,
                                SectorName = snm.SectorName,
                                SanctionedProjectCost = ppp.SanctionedProjectCost,
                            }).Distinct().ToList();
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
            ViewBag.ProjectID = new SelectList((from ppp in db.ProjectProposalPreprations
                                                   join fr in db.FundReleases on ppp.ProjectPreparationID equals fr.ProjectPreparationID
                                                join pm in db.ProjectMasters on ppp.ProjectNo equals pm.ProjectNo
                                               where ppp.IsActive == true && ppp.DistID == DistID && pm.ProjectNo ==ppp.ProjectNo  && ppp.Stageid==2 && fr.Phyicalinstallmentflag==null
                                               select new 
                                               {
                                                    ProjectNo = ppp.ProjectNo,
                                                   ProjectName = ppp.ProjectName
                                               }
                                             ).Distinct(), "ProjectNo", "ProjectName", null);

            
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
            //var milestone = db.MileStoneMasters.Where(x => x.ProjectNo == model.ProjectNo && x.DistrictID == model.DistrictID && x.MileStoneID==model.MileStoneID).FirstOrDefault();
            //if (db.PhysicalProgressMasters.Where(x => x.DistrictID == model.DistrictID && x.ProjectNo == model.ProjectNo && x.MileStoneID == model.MileStoneID).Any())
            //{
            //    JR.Message =  "Physical Progress Aready Created  for this Milestone !";
            //    return Json(JR, JsonRequestBehavior.AllowGet);
            //}
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
           var updateFlag= db.FundReleases.Where(x => x.DistrictID == model.DistrictID && x.ProjectNo == model.ProjectNo && x.Phyicalinstallmentflag == null).FirstOrDefault();

            db.PhysicalProgressMasters.Add(new PhysicalProgressMaster
            {
                DistrictID = model.DistrictID,
                ProjectID = model.ProjectID,
                ProjectNo = model.ProjectNo,
                //MileStoneID = milestone.MileStoneID,
                FundReleaseID = updateFlag.FundReleaseID,
                ProjectPreparationID = updateFlag.ProjectPreparationID,
                PhysicalProgressDate = model.PhysicalProgressDate,
                Remark = model.Remark,
                PhysicalProgressCopy = model.PhysicalProgressCopy,
                PhysicalPInPer = model.PhysicalPInPer,
                AmountSpend = model.AmountSpend,
                CreatedDate = DateTime.Now,
                CreatedBy = UserManager.GetUserLoginInfo(User.Identity.Name).LoginID.ToString(),
                IsActive = true,
                Phyicalintsallmentflag = updateFlag.InstallmentID.ToString()
            }) ;
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

           //var updateFlag= db.FundReleases.Where(x => x.DistrictID == model.DistrictID && x.ProjectNo == model.ProjectNo && x.Phyicalinstallmentflag == null).FirstOrDefault();
            updateFlag.Phyicalinstallmentflag = updateFlag.InstallmentID.ToString();
            db.Entry(updateFlag).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();
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

        public JsonResult GetPhysicalProgressDetails(int ProjectPreparationID)
        {
            var data = (from fr in db.PhysicalProgressMasters
                        //join ins in db.InstallmentMasters on fr.InstallmentID equals ins.InstallmentID
                        join pm in db.ProjectMasters on fr.ProjectNo equals pm.ProjectNo
                        join dm in db.DistrictMasters on fr.DistrictID equals dm.DistrictId
                        where fr.ProjectPreparationID == ProjectPreparationID
                        select new DTO_PhysicalProgressMaster
                        {
                            //InstallmentName = fr.InstallmentName,
                         PhysicalprogressID=fr.PhysicalprogressID.ToString(),
                            ProjectName = pm.ProjectName
                            ,DistrictName =dm.DistrictName,
                            PhysicalPInPer=fr.PhysicalPInPer,
                            PhysicalProgressDate=fr.PhysicalProgressDate,
                            Remark=fr.Remark,
                            PhysicalProgressCopy=fr.PhysicalProgressCopy,
                            AmountSpend=fr.AmountSpend,
                            Phyicalintsallmentflag = fr.Phyicalintsallmentflag
                        }).ToList();
            return Json(data, JsonRequestBehavior.AllowGet);
        }
    }
}