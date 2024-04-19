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
        public ActionResult PhysicalProgressList(string SectorType, string SectorName, string DistrictName, string ProjectName)
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

                var data = (from fr in db.PhysicalProgressMasters
                            join dm in db.DistrictMasters on fr.DistrictID equals dm.DistrictId
                            join pm in db.ProjectMasters on fr.ProjectNo equals pm.ProjectNo
                            //join ins in db.InstallmentMasters on ppp.InstallmkentID equals ins.InstallmentID
                            join stm in db.SectorTypeMasters on pm.SectorTypeId equals stm.SectorTypeID
                            join snm in db.SectorNameMasters on pm.SectorNameId equals snm.SectorNameId
                            join ppp in db.ProjectProposalPreprations on fr.ProjectNo equals ppp.ProjectNo
                            where fr.IsActive == true && fr.DistrictID == DistID && fr.ProjectNo != null
                              && (stm.SectorType == SectorType || String.IsNullOrEmpty(SectorType))
                            && (snm.SectorName == SectorName || String.IsNullOrEmpty(SectorName))
                            && (dm.DistrictName == DistrictName || String.IsNullOrEmpty(DistrictName))
                            && (ppp.ProjectName == ProjectName || String.IsNullOrEmpty(ProjectName))
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
                ViewBag.DistrictName = new SelectList(db.DistrictMasters, "DistrictName", "DistrictName", null);
                ViewBag.ProjectName = new SelectList(db.ProjectProposalPreprations.Where(x => x.Stageid == 2), "ProjectName", "ProjectName", null);
                var data = (from fr in db.PhysicalProgressMasters
                            join dm in db.DistrictMasters on fr.DistrictID equals dm.DistrictId
                            join pm in db.ProjectMasters on fr.ProjectNo equals pm.ProjectNo
                            join stm in db.SectorTypeMasters on pm.SectorTypeId equals stm.SectorTypeID
                            join snm in db.SectorNameMasters on pm.SectorNameId equals snm.SectorNameId
                            join ppp in db.ProjectProposalPreprations on fr.ProjectNo equals ppp.ProjectNo
                            where fr.IsActive == true  && fr.ProjectNo != null
                             && (stm.SectorType == SectorType || String.IsNullOrEmpty(SectorType))
                            && (snm.SectorName == SectorName || String.IsNullOrEmpty(SectorName))
                            && (dm.DistrictName == DistrictName || String.IsNullOrEmpty(DistrictName))
                            && (ppp.ProjectName == ProjectName || String.IsNullOrEmpty(ProjectName))
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
                                                   join mm in db.MileStoneMasters on ppp.ProjectPreparationID equals mm.ProjectPreparationID
                                                //join pm in db.ProjectMasters on ppp.ProjectNo equals pm.ProjectNo
                                               where ppp.IsActive == true && ppp.DistID == DistID && mm.ProjectNo ==ppp.ProjectNo  && ppp.Stageid==2 && mm.IsPhProgressDone==null && mm.IsFundReleased==true
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
           var updateFlag= db.FundReleases.Where(x => x.DistrictID == model.DistrictID && x.ProjectNo == model.ProjectNo && x.IsPhProgressDone == null).FirstOrDefault();

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
                var milestone = db.MileStoneMasters.Where(x => x.DistrictID == model.DistrictID && x.ProjectNo == model.ProjectNo && x.InstallmentID == updateFlag.InstallmentID).FirstOrDefault();
                var fundupdt = db.FundReleases.Where(x => x.DistrictID == model.DistrictID && x.ProjectNo == model.ProjectNo && x.InstallmentID == updateFlag.InstallmentID).FirstOrDefault();
                if (milestone != null)
                {
                    //DTO_MileStoneMaster mile = new DTO_MileStoneMaster();
                    milestone.IsPhProgressDone = true;
                    db.Entry(milestone).State = System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();
                }
                if (fundupdt != null)
                {
                    //DTO_MileStoneMaster mile = new DTO_MileStoneMaster();
                    fundupdt.IsPhProgressDone = true;
                    db.Entry(fundupdt).State = System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();
                }
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
        //public JsonResult BindMileStoneData(int DistrictID, string ProjectNo)
        //{
        //    var data = (from ms in db.MileStoneMasters
        //                join fr in db.FundReleases on ms.FundReleaseID equals fr.FundReleaseID
        //                join dm in db.DistrictMasters on ms.DistrictID equals dm.DistrictId
        //                join insM in db.InstallmentMasters on ms.InstallmentID equals insM.InstallmentID
        //                join pm in db.ProjectMasters on ms.ProjectNo equals pm .ProjectNo
        //                where ms.IsActive == true && ms.DistrictID == DistrictID && ms.ProjectNo == ProjectNo
        //                select new DTO_PhysicalProgressMaster
        //                {
        //                    MileStoneID = ms.MileStoneID,
        //                    FundReleaseID = ms.FundReleaseID,
        //                    ProjectNo = ms.ProjectNo,
        //                    DistrictID = ms.DistrictID,
        //                    DistrictName = dm.DistrictName,
        //                    Remark = ms.Instext,
        //                    Releaseperamount = ms.Releaseperamount,
        //                    ProjectPreparationID = ms.ProjectPreparationID,
        //                    CreatedDate=ms.CreatedDate,
        //                    ProjectName=pm.ProjectName,
        //                    InstallmentName=insM.InstallmentName,
        //                    MileStonePercentage=ms.InsPercentage

        //                }).ToList();
        //    return Json(data, JsonRequestBehavior.AllowGet);
        //}

        [HttpPost]
        public JsonResult MileStoneByProject(int DistrictID, string ProjectNo)
        {
            var data = (from mm in db.MileStoneMasters
                        join ppp in db.ProjectProposalPreprations on mm.ProjectPreparationID equals ppp.ProjectPreparationID
                        join ins in db.InstallmentMasters on mm.InstallmentID equals ins.InstallmentID
                        join dm in db.DistrictMasters on mm.DistrictID equals dm.DistrictId
                        where mm.ProjectNo == ProjectNo && mm.DistrictID == DistrictID && mm.IsFundReleased==true && mm.IsPhProgressDone !=true
                        select new DTO_MileStoneMaster
                        {
                            Districtname = dm.DistrictName,
                            ProjectName = ppp.ProjectName,
                            Instext = mm.Instext,
                            SanctionedProjectCost = ppp.SanctionedProjectCost,
                            InsPercentage = mm.InsPercentage,
                            InstallmentName = ins.InstallmentName,
                            IsFundReleased = mm.IsFundReleased,
                            IsPhProgressDone = mm.IsPhProgressDone,
                            IsUtilizationUploaded = mm.IsUtilizationUploaded,
                            IsInspectionDone = mm.IsInspectionDone
                        }).ToList();
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        public JsonResult DeletePhysicalProgress(int PhysicalprogressID)
        {
            string msg = "";
            //JsonResponse JR = new JsonResponse();
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
                msg = "1";
            }
            else
            {
                msg = "0";
            }
            return Json(msg, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetPhysicalProgressDetails(int ProjectPreparationID)
        {
            var data = (from fr in db.PhysicalProgressMasters
                       // join ins in db.InstallmentMasters on fr.InstallmentID equals ins.InstallmentID
                        join pm in db.ProjectMasters on fr.ProjectNo equals pm.ProjectNo
                        join dm in db.DistrictMasters on fr.DistrictID equals dm.DistrictId
                        where fr.ProjectPreparationID == ProjectPreparationID
                        select new DTO_PhysicalProgressMaster
                        {
                           // InstallmentName = fr.InstallmentName,
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
        //code start for filter
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
    }
}