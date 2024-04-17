﻿using DMFProjectFinal.Models;
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

        public ActionResult ReleaseFund(string ProjectName,string SectorName,string SectorType)
        {
            ViewBag.SectorType = new SelectList(db.SectorTypeMasters, "SectorType", "SectorType", null);
            ViewBag.SectorName = new SelectList(db.SectorNameMasters, "SectorName", "SectorName", null);
            int? DistID = null;
            if (UserManager.GetUserLoginInfo(User.Identity.Name).RoleID == 2)
            {
                DistID = UserManager.GetUserLoginInfo(User.Identity.Name).DistID;
                var data = (from fr in db.FundReleases
                            join dm in db.DistrictMasters on fr.DistrictID equals dm.DistrictId
                            join pm in db.ProjectMasters on fr.ProjectNo equals pm.ProjectNo
                            //join ins in db.InstallmentMasters on fr.InstallmentID equals ins.InstallmentID
                            join stm in db.SectorTypeMasters on pm.SectorTypeId equals stm.SectorTypeID
                            join snm in db.SectorNameMasters on pm.SectorNameId equals snm.SectorNameId
                            join ppp in db.ProjectProposalPreprations on fr.ProjectNo equals ppp.ProjectNo
                            where fr.IsActive == true && fr.DistrictID == DistID && fr.ProjectNo != null && ppp.Stageid == 2 
                            && (pm.ProjectName==ProjectName || String.IsNullOrEmpty(ProjectName)) 
                            && (snm.SectorName==SectorName || String.IsNullOrEmpty(SectorName)) 
                            && (stm.SectorType==SectorType || String.IsNullOrEmpty(SectorType))
                            select new DTO_FundRelease
                            {
                                //FundReleaseID = fr.FundReleaseID.ToString(),
                                ProjectPreparationID=fr.ProjectPreparationID,
                                DistrictID = fr.DistrictID,
                                ProjectNo = fr.ProjectNo,
                                DistrictName = dm.DistrictName,
                                ProjectName = pm.ProjectName,
                                //RelaeseDate = fr.RelaeseDate,
                                //ReleaseAmount = fr.ReleaseAmount,
                                //FundReleaseCopy = fr.FundReleaseCopy,
                                //InstallmentID = fr.InstallmentID,
                                //InstallmentName = ins.InstallmentName,
                                SectorName = snm.SectorName,
                                SectorType = stm.SectorType,
                                SanctionedProjectCost = ppp.SanctionedProjectCost
                            }).Distinct().ToList();
            ViewBag.LstData = data;

            }
            else
            {
                var data = (from fr in db.FundReleases
                            join dm in db.DistrictMasters on fr.DistrictID equals dm.DistrictId
                            join pm in db.ProjectMasters on fr.ProjectNo equals pm.ProjectNo
                            //join ins in db.InstallmentMasters on fr.InstallmentID equals ins.InstallmentID
                            join stm in db.SectorTypeMasters on pm.SectorTypeId equals stm.SectorTypeID
                            join snm in db.SectorNameMasters on pm.SectorNameId equals snm.SectorNameId
                            join ppp in db.ProjectProposalPreprations on fr.ProjectNo equals ppp.ProjectNo
                            where fr.IsActive == true && fr.ProjectNo != null && ppp.Stageid == 2 && (pm.ProjectName == ProjectName || String.IsNullOrEmpty(ProjectName))
                            && (snm.SectorName == SectorName || String.IsNullOrEmpty(SectorName))
                            && (stm.SectorType == SectorType || String.IsNullOrEmpty(SectorType))
                            select new DTO_FundRelease
                            {
                                //FundReleaseID = fr.FundReleaseID.ToString(),
                                ProjectPreparationID = fr.ProjectPreparationID,
                                DistrictID = fr.DistrictID,
                                ProjectNo = fr.ProjectNo,
                                DistrictName = dm.DistrictName,
                                ProjectName = pm.ProjectName,
                                //RelaeseDate = fr.RelaeseDate,
                                //ReleaseAmount = fr.ReleaseAmount,
                                //FundReleaseCopy = fr.FundReleaseCopy,
                                //InstallmentID = fr.InstallmentID,
                                //InstallmentName = ins.InstallmentName,
                                SectorName = snm.SectorName,
                                SectorType = stm.SectorType,
                                SanctionedProjectCost = ppp.SanctionedProjectCost
                            }).Distinct().ToList();
                ViewBag.LstData = data;
            }
            return View();
        }
        public ActionResult CreateFundRelease()
        {
            int? DistID = null;
            if (UserManager.GetUserLoginInfo(User.Identity.Name).RoleID == 2)
            {
                DistID = UserManager.GetUserLoginInfo(User.Identity.Name).DistID;
            }
            DTO_FundRelease model = new DTO_FundRelease();
            ViewBag.DistrictID = new SelectList(db.DistrictMasters.Where(x => x.IsActive == true && x.DistrictId == (DistID == null ? x.DistrictId : DistID)), "DistrictId", "DistrictName", null);
            ViewBag.ProjectID = new SelectList(db.ProjectProposalPreprations.Where(x => x.IsActive == true && x.ProjectNo !=null  && x.Stageid==2 && x.DistID == (DistID == null ? x.DistID : DistID)), "ProjectNo", "ProjectName", null);
            ViewBag.InstallmentID = new SelectList(db.InstallmentMasters.Where(x => x.IsActive == true), "InstallmentID", "InstallmentName", null);
            model.DistrictID = DistID;
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
            if (db.FundReleases.Where(x => x.DistrictID == model.DistrictID && x.ProjectNo == model.ProjectNo && x.InstallmentID== model.InstallmentID).Any())
            {
                JR.Message =  model.InstallmentID + "st Installment Aready Release for this Project !";
                return Json(JR, JsonRequestBehavior.AllowGet);
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
            var ProjectPrepID = db.ProjectProposalPreprations.Where(x => x.ProjectNo == model.ProjectNo && x.DistID==model.DistrictID).FirstOrDefault();

            db.FundReleases.Add(new FundRelease
            {
                DistrictID = model.DistrictID,
                ProjectPreparationID= ProjectPrepID.ProjectPreparationID,
                ProjectNo = model.ProjectNo,
                RelaeseDate = model.RelaeseDate,
                ReleaseAmount = model.ReleaseAmount,
                InstallmentID = model.InstallmentID,
                FundReleaseCopy = model.FundReleaseCopy,
                CreatedDate = DateTime.Now,
                IsActive=true,
                CreatedBy= UserManager.GetUserLoginInfo(User.Identity.Name).LoginID.ToString()
            });
            int res = db.SaveChanges();
            if (res > 0)
            {
                JR.IsSuccess = true;
                JR.Message = "Data Saved Successfully";
                JR.RedURL = "/FundRelease/ReleaseFund";
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
            JsonResponse JR = new JsonResponse();
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
                JR.IsSuccess = true;
                JR.Message = "Data Deleted Successfully";
                JR.RedURL = "/FundRelease/ReleaseFund";
            }
            else
            {
                JR.Message = "Some Error Occured, Contact to Admin";
            }
            return Json(JR, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult GetProjectDetails(int DistrictID, string ProjectNo)
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
                join pm in db.ProjectMasters on ppp.ProjectNo equals pm.ProjectNo
                //join fr in db.FundReleases on ppp.ProjectPreparationID equals fr.ProjectPreparationID
                //join ins in db.InstallmentMasters on fr.InstallmentID equals ins.InstallmentID
                where ppp.IsActive == true && ppp.DistID == DistrictID && ppp.ProjectNo == ProjectNo
                        select new DTO_ProjectProposalPrepration
                        {
                            DistID = ppp.DistID,
                            ProjectNo = ppp.ProjectNo,
                            DistrictName = dm.DistrictName,
                            ProjectName = pm.ProjectName,
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

        [HttpPost]
        public JsonResult MileStoneByInstallment(int DistrictID, string ProjectNo,int InstallmentID)
        {

            var data = (from mm in db.MileStoneMasters
                        join ppp in db.ProjectProposalPreprations on mm.ProjectPreparationID equals ppp.ProjectPreparationID
                        join ins in db.InstallmentMasters on mm.InstallmentID equals ins.InstallmentID
                        where mm.ProjectNo == ProjectNo && mm.DistrictID == DistrictID && mm.InstallmentID == InstallmentID
                        select new DTO_MileStoneMaster
                        {
                           SanctionedProjectCost=ppp.SanctionedProjectCost,
                           InsPercentage=mm.InsPercentage,
                           InstallmentName=ins.InstallmentName
                        }).ToList();
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult BindInstallment(int DistrictID, string ProjectNo)
        {
            var data = (from mm in db.MileStoneMasters
                        join ins in db.InstallmentMasters on mm.InstallmentID equals ins.InstallmentID
                        where mm.DistrictID==DistrictID && mm.ProjectNo==ProjectNo
                        select new DTO_MileStoneMaster
                        {
                            InstallmentID = mm.InstallmentID,
                            InstallmentName = ins.InstallmentName
                        }).Distinct().ToList();
            return Json(data,JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetFundReleaseDetails(int ProjectPreparationID)
        {
            var data = (from fr in db.FundReleases
                        join ins in db.InstallmentMasters on fr.InstallmentID equals ins.InstallmentID
                        join pm in db.ProjectMasters on fr.ProjectNo equals pm.ProjectNo
                        where fr.ProjectPreparationID == ProjectPreparationID
                        select new DTO_FundRelease
                        {
                            FundReleaseID = fr.FundReleaseID.ToString(),
                            InstallmentName = ins.InstallmentName,
                            RelaeseDate = fr.RelaeseDate,
                            ReleaseAmount = fr.ReleaseAmount,
                            FundReleaseCopy = fr.FundReleaseCopy,
                            Physicalinstallmentflag=fr.Phyicalinstallmentflag,
                            ProjectName=pm.ProjectName
                        }).ToList();
            return Json(data,JsonRequestBehavior.AllowGet);
        }

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
        //public JsonResult BindProject(string SectorName)
        //{
        //    var sectorID = db.SectorNameMasters.Where(x => x.SectorName == SectorName).FirstOrDefault() ?? new SectorTypeMaster();

        //    var data = (from st in db.SectorNameMasters
        //                where st.SectorTypeId == sectortypeID.SectorTypeID
        //                select new DTO_SectorNameMaster
        //                {
        //                    SectorName = st.SectorName
        //                }).ToList();

        //    return Json(data, JsonRequestBehavior.AllowGet);
        //}
    }
}