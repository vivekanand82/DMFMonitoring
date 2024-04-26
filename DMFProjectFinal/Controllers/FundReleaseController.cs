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
    public class FundReleaseController : Controller
    {
        // GET: FundRelease
        private dfm_dbEntities db = new dfm_dbEntities();

        public ActionResult ReleaseFund(string SectorType, string SectorName, string DistrictName, string ProjectName)
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
                                   where ppp.IsActive == true && ppp.DistID == DistID && ppp.ProjectNo != null && ppp.Stageid == 2
                                   && (stm.SectorType == SectorType || String.IsNullOrEmpty(SectorType))
                                   && (snm.SectorName == SectorName || String.IsNullOrEmpty(SectorName))
                                   && (dm.DistrictName == DistrictName || String.IsNullOrEmpty(DistrictName))
                                   && (ppp.ProjectName == ProjectName || String.IsNullOrEmpty(ProjectName))
                                  // orderby mm.MileStoneStatus descending
                                   select new DTO_FundRelease
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
                                   where ppp.IsActive == true  && ppp.ProjectNo != null && ppp.Stageid == 2
                                   && (stm.SectorType == SectorType || String.IsNullOrEmpty(SectorType))
                                   && (snm.SectorName == SectorName || String.IsNullOrEmpty(SectorName))
                                   && (dm.DistrictName == DistrictName || String.IsNullOrEmpty(DistrictName))
                                   && (ppp.ProjectName == ProjectName || String.IsNullOrEmpty(ProjectName))
                                  // orderby mm.MileStoneStatus descending
                                   select new DTO_FundRelease
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
                var data = groupedData.Select(group => group.ToList().OrderByDescending(x => x.MileStoneStatus));
                var processedData = data.Select(group => group.FirstOrDefault()).ToList();

                ViewBag.LstData = processedData;

            }
            return View();
        }
        public ActionResult CreateFundRelease(int ProjectPreparationID)
        {
            int? DistID = null;
            if (UserManager.GetUserLoginInfo(User.Identity.Name).RoleID == 2)
            {
                DistID = UserManager.GetUserLoginInfo(User.Identity.Name).DistID;
            }
            DTO_FundRelease model = new DTO_FundRelease();
            ViewBag.DistrictID = new SelectList(db.DistrictMasters.Where(x => x.IsActive == true && x.DistrictId == (DistID == null ? x.DistrictId : DistID)), "DistrictId", "DistrictName", null);
            //ViewBag.ProjectID = new SelectList(db.ProjectProposalPreprations.Where(x => x.IsActive == true && x.ProjectNo !=null  && x.Stageid==2 && x.DistID == (DistID == null ? x.DistID : DistID)), "ProjectNo", "ProjectName", null);
            ViewBag.InstallmentID = new SelectList(db.InstallmentMasters.Where(x => x.IsActive == true), "InstallmentID", "InstallmentName", null);
            var ProjectNo = db.ProjectProposalPreprations.Where(x => x.ProjectPreparationID == ProjectPreparationID).FirstOrDefault().ProjectNo;
            model.DistrictID = DistID;
            model.ProjectNo = ProjectNo;
            model.ProjectPreparationID = ProjectPreparationID;
            ViewBag.ProjectID = new SelectList((from ppp in db.ProjectProposalPreprations
                                       join ml in db.MileStoneMasters on ppp.ProjectPreparationID equals ml.ProjectPreparationID
                                                select new
                                       {
                                           ProjectNo = ppp.ProjectNo,
                                           ProjectName = ppp.ProjectName
                                       }).Distinct(), "ProjectNo", "ProjectName", ProjectNo);
            return View(model);
        }
        [HttpPost]
        public ActionResult CreateFundRelease(DTO_FundRelease model)
        {
            JsonResponse JR = new JsonResponse();
            if (!ModelState.IsValid)
            {
                JR.Data = ModelState.Select(x => x.Value.Errors).Where(y => y.Count > 0).ToList();
                return Json(JR, JsonRequestBehavior.AllowGet);
            }
            var installment = model.InstallmentID - 1;
            if (installment != 0)
            {
                var validate = db.FundReleases.Where(x => x.DistrictID == model.DistrictID && x.ProjectPreparationID == model.ProjectPreparationID && x.InstallmentID == installment).FirstOrDefault();
                if (validate.IsInspectionDone == null || validate.IsPhProgressDone == null || validate.IsUtilizationUploaded == null)
                {
                    if (installment == 1)
                    {
                        JR.Message = "First complete the All Steps for " + installment + "st Installment  !!";
                    }
                    else if (installment == 2)
                    {
                        JR.Message = "First complete the All Steps for " + installment + "nd Installment  !!";
                    }
                    else if (installment == 3)
                    {
                        JR.Message = "First complete the All Steps for " + installment + "rd Installment  !!";
                    }
                    else if (installment == 4)
                    {
                        JR.Message = "First complete the All Steps for " + installment + "th Installment  !!";
                    }
                    else if (installment == 5)
                    {
                        JR.Message = "First complete the All Steps for " + installment + "th Installment  !!";
                    }
                    else
                    {
                        JR.Message = "First complete the All Steps for Previous Installment  !!";

                    }
                    return Json(JR, JsonRequestBehavior.AllowGet);
                }
            }
           
            if (!String.IsNullOrEmpty(model.FundReleaseCopy))
            {
                model.FundReleaseCopy = BusinessLogics.UploadFileDMF(model.FundReleaseCopy);
                if (model.FundReleaseCopy.Contains("Expp::"))
                {
                    JR.Message = model.FundReleaseCopy;
                    return Json(JR, JsonRequestBehavior.AllowGet);
                }
            }
            //var ProjectPrepID = db.ProjectProposalPreprations.Where(x => x.ProjectPreparationID == model.ProjectPreparationID && x.DistID==model.DistrictID).FirstOrDefault();

            db.FundReleases.Add(new FundRelease
            {
                DistrictID = model.DistrictID,
                ProjectPreparationID= model.ProjectPreparationID,
                ProjectNo = model.ProjectNo,
                RelaeseDate = model.RelaeseDate,
                ReleaseAmount = model.ReleaseAmount,
                InstallmentID = model.InstallmentID,
                FundReleaseCopy = model.FundReleaseCopy,
                CreatedDate = DateTime.Now,
                IsActive=true,
                IsFundReleased=true,
                CreatedBy= UserManager.GetUserLoginInfo(User.Identity.Name).LoginID.ToString()
            });
            int res = db.SaveChanges();
            if (res > 0)
            {
                JR.IsSuccess = true;
                JR.Message = "Data Saved Successfully";
                JR.RedURL = "/FundRelease/ReleaseFund";
                //code added for update the milestone flag 18-04-2024
                var milestone = db.MileStoneMasters.Where(x => x.DistrictID == model.DistrictID && x.ProjectPreparationID == model.ProjectPreparationID && x.InstallmentID == model.InstallmentID).FirstOrDefault();
                if (milestone != null)
                {
                //DTO_MileStoneMaster mile = new DTO_MileStoneMaster();
                    milestone.IsFundReleased = true;
                    milestone.MileStoneStatus = model.InstallmentID;
                    milestone.ModifiedDate = DateTime.Now;
                    db.Entry(milestone).State = System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();
                }
            }
            else
            {
                JR.Message = "Some Error Occured, Contact to Admin";
            }

            return Json(JR, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult GetProjectAndInstallmentDetails( int DistrictID,string ProjectNo)
        {
            //int? DistID = null;
            //if (UserManager.GetUserLoginInfo(User.Identity.Name).RoleID == 2)
            //{
            //    DistID = UserManager.GetUserLoginInfo(User.Identity.Name).DistID;
            //}
            var data = (from fr in db.FundReleases
                        join dm in db.DistrictMasters on fr.DistrictID equals dm.DistrictId
                        join pm in db.ProjectMasters on fr.ProjectNo equals pm.ProjectNo
                        join ins in db.InstallmentMasters on fr.InstallmentID equals ins.InstallmentID
                        join stm in db.SectorTypeMasters on pm.SectorTypeId equals stm.SectorTypeID
                        join snm in db.SectorNameMasters on pm.SectorNameId equals snm.SectorNameId
                        join ppp in db.ProjectProposalPreprations on fr.ProjectNo equals ppp.ProjectNo
                        where fr.IsActive == true && fr.DistrictID == DistrictID && fr.ProjectNo == ProjectNo
                        select new DTO_FundRelease
                        {
                            FundReleaseID = fr.FundReleaseID.ToString(),
                            DistrictID = fr.DistrictID,
                            ProjectNo = fr.ProjectNo,
                            DistrictName = dm.DistrictName,
                            ProjectName = pm.ProjectName,
                            RelaeseDate = fr.RelaeseDate,
                            ReleaseAmount = fr.ReleaseAmount,
                            FundReleaseCopy = fr.FundReleaseCopy,
                            InstallmentID = fr.InstallmentID,
                            InstallmentName = ins.InstallmentName,
                            SectorName = snm.SectorName,
                            SectorType = stm.SectorType,
                            SanctionedProjectCost = ppp.SanctionedProjectCost
                        }).ToList();
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public ActionResult EditReleasedFund(int FundReleaseID)
        {
            if (FundReleaseID == 0)
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
                var Info = db.FundReleases.Where(x => x.FundReleaseID == FundReleaseID).FirstOrDefault();
                if (Info != null)
                    {
                    ViewBag.DistrictID = new SelectList(db.DistrictMasters.Where(x => x.IsActive == true && x.DistrictId == (DistID == null ? x.DistrictId : DistID)), "DistrictId", "DistrictName", Info.DistrictID);
                    ViewBag.ProjectID = new SelectList(db.ProjectProposalPreprations.Where(x => x.IsActive == true && x.ProjectNo != null && x.Stageid == 2 && x.DistID == (DistID == null ? x.DistID : DistID)), "ProjectNo", "ProjectName", Info.ProjectNo);
                    ViewBag.InstallmentID = new SelectList(db.InstallmentMasters.Where(x => x.IsActive == true), "InstallmentID", "InstallmentName",selectedValue:Info.InstallmentID);
                    return View("~/Views/FundRelease/CreateFundRelease.cshtml", new DTO_FundRelease { FundReleaseID =Info.FundReleaseID.ToString(), RelaeseDate = Info.RelaeseDate, ReleaseAmount = Info.ReleaseAmount, InstallmentID = Info.InstallmentID, FundReleaseCopy = Info.FundReleaseCopy,DistrictID=Info.DistrictID });
                }
                else
                {
                    return RedirectToAction("Login", "Account");
                }
            }
            catch(Exception ex)
            {
                return RedirectToAction("Login", "Account");
            }
        }
        [HttpPost]
        public JsonResult EditReleasedFund(DTO_FundRelease model)
        {
            JsonResponse JR = new JsonResponse();
            if (!ModelState.IsValid)
            {
                JR.Data = ModelState.Select(x => x.Value.Errors).Where(y => y.Count > 0).ToList();
                return Json(JR, JsonRequestBehavior.AllowGet);
            }
            if (!String.IsNullOrEmpty(model.FundReleaseID))
            {
                //if (db.FundReleases.Where(x => x.DistrictID == model.DistrictID && x.ProjectNo == model.ProjectNo && x.InstallmentID == model.InstallmentID).Any())
                //{
                //    JR.Message = model.InstallmentID + "st Installment Aready Release for this Project !";
                //    return Json(JR, JsonRequestBehavior.AllowGet);
                //}
                if (!String.IsNullOrEmpty(model.FundReleaseCopy))
                {
                    if (model.FundReleaseCopy != "prev")
                    {
                        model.FundReleaseCopy = BusinessLogics.UploadFileDMF(model.FundReleaseCopy);
                        if (model.FundReleaseCopy.Contains("Expp::"))
                        {
                            JR.Message = model.FundReleaseCopy;
                            return Json(JR, JsonRequestBehavior.AllowGet);
                        }
                    }
                }
                int id = Convert.ToInt32(model.FundReleaseID);
                var Info = db.FundReleases.Where(x => x.FundReleaseID == id).FirstOrDefault();
                if (Info != null)
                {
                 Info.DistrictID = model.DistrictID;
                 Info.ProjectNo = model.ProjectNo;
                 Info.RelaeseDate = model.RelaeseDate;
                 Info.ReleaseAmount = model.ReleaseAmount;
                 Info.InstallmentID = model.InstallmentID;
                 Info.FundReleaseCopy = (model.FundReleaseCopy != null && model.FundReleaseCopy == "prev") ? Info.FundReleaseCopy : model.FundReleaseCopy;
                    Info.ModifyDate = DateTime.Now;
                 Info.IsActive = true;
                 Info.ModifiedBy = UserManager.GetUserLoginInfo(User.Identity.Name).LoginID.ToString();
                }
            }
            int res = db.SaveChanges();
            if (res > 0)
            {
                JR.IsSuccess = true;
                JR.Message = "Data Updated Successfully";
                JR.RedURL = "/FundRelease/ReleaseFund";
            }
            else
            {
                JR.Message = "Some Error Occured, Contact to Admin";
            }
            return Json(JR, JsonRequestBehavior.AllowGet);
        }

        public JsonResult DeleteReleasedFund(int FundReleaseID)
        {
            string msg = "";
            //JsonResponse JR = new JsonResponse();
            if (FundReleaseID > 0)
            {
                var Info = db.FundReleases.Where(x => x.FundReleaseID == FundReleaseID).FirstOrDefault();
                if (Info != null)
                {
                    db.FundReleases.Remove(Info);
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
        [HttpPost]
        public JsonResult GetProjectDetails(int DistrictID, int ProjectPreparationID)
        {
            //int? DistID = null;
            //if (UserManager.GetUserLoginInfo(User.Identity.Name).RoleID == 2)
            //{
            //    DistID = UserManager.GetUserLoginInfo(User.Identity.Name).DistID;
            //}
            var data = (from ppp in db.ProjectProposalPreprations
                  join stm in db.SectorTypeMasters on ppp.SectorTypeId equals stm.SectorTypeID
                    join snm in db.SectorNameMasters on ppp.SectorID equals snm.SectorNameId
                join dm in db.DistrictMasters on ppp.DistID equals dm.DistrictId
                //join pm in db.ProjectMasters on ppp.ProjectNo equals pm.ProjectNo
                //join fr in db.FundReleases on ppp.ProjectPreparationID equals fr.ProjectPreparationID
                //join ins in db.InstallmentMasters on fr.InstallmentID equals ins.InstallmentID
                where ppp.IsActive == true && ppp.DistID == DistrictID && ppp.ProjectPreparationID == ProjectPreparationID
                        select new DTO_ProjectProposalPrepration
                        {
                            DistID = ppp.DistID,
                            ProjectNo = ppp.ProjectNo,
                            DistrictName = dm.DistrictName,
                            ProjectName = ppp.ProjectName,
                            SectorName = snm.SectorName,
                            SectorType = stm.SectorType,
                            SanctionedProjectCost = ppp.SanctionedProjectCost,
                            ProsposalNo=ppp.ProsposalNo,
                            ProposedBy=ppp.ProposedBy
                            //ReleaseAmount=fr.ReleaseAmount,
                            //InstallmentName=ins.InstallmentName
                            
                        }).ToList();
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        //[HttpPost]
        //public JsonResult MileStoneByInstallment(int DistrictID, string ProjectNo,int InstallmentID)
        //{
        //    var data = (from mm in db.MileStoneMasters
        //                join ppp in db.ProjectProposalPreprations on mm.ProjectPreparationID equals ppp.ProjectPreparationID
        //                join ins in db.InstallmentMasters on mm.InstallmentID equals ins.InstallmentID
        //                where mm.ProjectNo == ProjectNo && mm.DistrictID == DistrictID && mm.InstallmentID == InstallmentID
        //                select new DTO_MileStoneMaster
        //                {
        //                   SanctionedProjectCost=ppp.SanctionedProjectCost,
        //                   InsPercentage=mm.InsPercentage,
        //                   InstallmentName=ins.InstallmentName
        //                }).ToList();
        //    return Json(data, JsonRequestBehavior.AllowGet);
        //}

        [HttpPost]
        public JsonResult MileStoneByProject(int DistrictID, int ProjectPreparationID)
        {
            var data = (from mm in db.MileStoneMasters
                        join ppp in db.ProjectProposalPreprations on mm.ProjectPreparationID equals ppp.ProjectPreparationID
                        join ins in db.InstallmentMasters on mm.InstallmentID equals ins.InstallmentID
                        join dm in db.DistrictMasters on mm.DistrictID equals dm.DistrictId
                        where mm.ProjectPreparationID == ProjectPreparationID && mm.DistrictID == DistrictID
                        select new DTO_MileStoneMaster
                        {
                            Districtname=dm.DistrictName,
                            ProjectName=ppp.ProjectName,
                            Instext=mm.Instext,
                            SanctionedProjectCost = ppp.SanctionedProjectCost,
                            InsPercentage = mm.InsPercentage,
                            InstallmentName = ins.InstallmentName,
                            IsFundReleased=mm.IsFundReleased,
                            IsPhProgressDone=mm.IsPhProgressDone,
                            IsUtilizationUploaded=mm.IsUtilizationUploaded,
                            IsInspectionDone=mm.IsInspectionDone
                        }).ToList();
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult BindInstallment(int DistrictID, int ProjectPreparationID)
        {
            var data = (from mm in db.MileStoneMasters
                        join ins in db.InstallmentMasters on mm.InstallmentID equals ins.InstallmentID
                        join ppp in db.ProjectProposalPreprations on mm.ProjectPreparationID equals ppp.ProjectPreparationID
                        where mm.DistrictID == DistrictID && mm.ProjectPreparationID == ProjectPreparationID
                        where mm.IsFundReleased != true && mm.IsPhProgressDone != true && mm.IsUtilizationUploaded != true
                        select new DTO_MileStoneMaster
                        {
                            InstallmentID = mm.InstallmentID,
                            InstallmentName = ins.InstallmentName,
                            SanctionedProjectCost=ppp.SanctionedProjectCost,
                            InsPercentage=mm.InsPercentage,
                            Instext=mm.Instext
                        }).FirstOrDefault();
            return Json(data,JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetFundReleaseDetails(int ProjectPreparationID)
        {
            var data = (from fr in db.FundReleases
                        join ins in db.InstallmentMasters on fr.InstallmentID equals ins.InstallmentID
                        join ppp in db.ProjectProposalPreprations on fr.ProjectPreparationID equals ppp.ProjectPreparationID
                        join ph in db.PhysicalProgressMasters on fr.FundReleaseID equals ph.FundReleaseID into php from ph in php.DefaultIfEmpty()
                        join uc in db.UtilizationMasters on ph.PhysicalprogressID equals uc.PhysicalProgressID into ucc from uc in ucc.DefaultIfEmpty()
                        //join inpec in db.inr on ppp.ProjectPreparationID equals inpec.ProjectPreparationID
                        where fr.ProjectPreparationID == ProjectPreparationID
                        select new DTO_FundRelease
                        {
                            //FundReleaseID = fr.FundReleaseID.ToString(),
                            InstallmentName = ins.InstallmentName,
                            RelaeseDate = fr.RelaeseDate,
                            ReleaseAmount = fr.ReleaseAmount,
                            FundReleaseCopy = fr.FundReleaseCopy,
                            //Physicalinstallmentflag = fr.Phyicalinstallmentflag,
                            ProjectName = ppp.ProjectName,
                            IsInspectionDone=fr.IsInspectionDone,
                            IsUtilizationUploaded=fr.IsUtilizationUploaded,
                            IsPhProgressDone=fr.IsPhProgressDone,
                            PhysicalProgressCopy=ph.PhysicalProgressCopy,
                            UtilizationCopy=uc.UtilizationCopy,
                            //InspectionCopy=inpec.InspectionCopy
                        }).ToList();
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        //public JsonResult GetFundReleaseDetails(int ProjectPreparationID)
        //{
        //    var data = (from fr in db.FundReleases
        //                join ins in db.InstallmentMasters on fr.InstallmentID equals ins.InstallmentID
        //                join pm in db.ProjectMasters on fr.ProjectNo equals pm.ProjectNo
        //                where fr.ProjectPreparationID == ProjectPreparationID
        //                select new DTO_FundRelease
        //                {
        //                    FundReleaseID = fr.FundReleaseID.ToString(),
        //                    InstallmentName = ins.InstallmentName,
        //                    RelaeseDate = fr.RelaeseDate,
        //                    ReleaseAmount = fr.ReleaseAmount,
        //                    FundReleaseCopy = fr.FundReleaseCopy,
        //                    Physicalinstallmentflag=fr.Phyicalinstallmentflag,
        //                    ProjectName=pm.ProjectName
        //                }).ToList();
        //    return Json(data,JsonRequestBehavior.AllowGet);
        //}

        public JsonResult BindSector(string SectorType)
        {
            var sectortypeID = db.SectorTypeMasters.Where(x => x.SectorType == SectorType).FirstOrDefault()?? new SectorTypeMaster();
          
                var data = (from st in db.SectorNameMasters
                            where st.SectorTypeId == sectortypeID.SectorTypeID
                            select new DTO_SectorNameMaster
                            {
                                SectorName = st.SectorName
                            }).ToList();

            return Json(data,JsonRequestBehavior.AllowGet);
        }
        public JsonResult BindProject(string SectorType, string SectorName,string DistrictName)
        {
            var sectorID = db.SectorNameMasters.Where(x => x.SectorName == SectorName).FirstOrDefault() ?? new SectorNameMaster();
            var SectorTypeId = db.SectorTypeMasters.Where(x => x.SectorType == SectorType).FirstOrDefault() ?? new SectorTypeMaster();
            var District = db.DistrictMasters.Where(x => x.DistrictName == DistrictName).FirstOrDefault() ?? new DistrictMaster();

            var data = (from ppp in db.ProjectProposalPreprations
                        where ppp.Stageid == 2 
                        
                        && ppp.SectorTypeId == (SectorTypeId.SectorTypeID == 0? ppp.SectorTypeId:SectorTypeId.SectorTypeID) 
                        && ppp.SectorID == (sectorID.SectorNameId == 0 ? ppp.SectorID : sectorID.SectorNameId) 
                        && ppp.DistID==(District.DistrictId == 0 ? ppp.DistID : District.DistrictId)
                        select new DTO_ProjectProposalPrepration
                        {
                           ProjectName=ppp.ProjectName
                     
                        }).ToList();

            return Json(data, JsonRequestBehavior.AllowGet);
        }
        public JsonResult BindSectorname(int? SectorTypeId)
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
        //[HttpPost]
        //public JsonResult BindMilestoneByInstallment(int ProjectPreparationID,int InstallmentID)
        //{
        //    var data = (from mm in db.MileStoneMasters
        //                join ins in db.InstallmentMasters on mm.InstallmentID equals ins.InstallmentID
        //                where mm.ProjectPreparationID == ProjectPreparationID && mm.InstallmentID == InstallmentID
        //                select new DTO_MileStoneMaster
        //                {
        //                    MileStoneID=mm.MileStoneID,
        //                    Instext=mm.Instext,
        //                    InstallmentID=mm.InstallmentID
        //                }).FirstOrDefault();
        //    return Json(data,JsonRequestBehavior.AllowGet);
        //}
    }
}