using DMFProjectFinal.Models;
using DMFProjectFinal.Models.DTO;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DMFProjectFinal.Controllers
{
    [SessionFilter]
    public class PhysicalProgressController : Controller
    {
        private dfm_dbEntities db = new dfm_dbEntities();
        public static string coonectionstrings = ConfigurationManager.ConnectionStrings["constr"].ToString();
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

                var groupedData = (from mm in db.MileStoneMasters
                                   join dm in db.DistrictMasters on mm.DistrictID equals dm.DistrictId
                                   join ppp in db.ProjectProposalPreprations on mm.ProjectPreparationID equals ppp.ProjectPreparationID
                                   into ppps
                                   from ppp in ppps.DefaultIfEmpty()
                                   join stm in db.SectorTypeMasters on ppp.SectorTypeId equals stm.SectorTypeID into stms
                                   from stm in stms.DefaultIfEmpty()
                                   join snm in db.SectorNameMasters on ppp.SectorID equals snm.SectorNameId into snms
                                   from snm in snms.DefaultIfEmpty()
                                   where ppp.IsActive == true && ppp.DistID == DistID && ppp.ProjectNo != null && ppp.Stageid == 2 && mm.IsFundReleased==true
                                   && (stm.SectorType == SectorType || String.IsNullOrEmpty(SectorType))
                                   && (snm.SectorName == SectorName || String.IsNullOrEmpty(SectorName))
                                   && (dm.DistrictName == DistrictName || String.IsNullOrEmpty(DistrictName))
                                   && (ppp.ProjectName == ProjectName || String.IsNullOrEmpty(ProjectName))
                                   // orderby mm.MileStoneStatus descending
                                   select new DTO_PhysicalProgressMaster
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
                                   where ppp.IsActive == true && ppp.ProjectNo != null && ppp.Stageid == 2 && mm.IsFundReleased == true
                                   && (stm.SectorType == SectorType || String.IsNullOrEmpty(SectorType))
                                   && (snm.SectorName == SectorName || String.IsNullOrEmpty(SectorName))
                                   && (dm.DistrictName == DistrictName || String.IsNullOrEmpty(DistrictName))
                                   && (ppp.ProjectName == ProjectName || String.IsNullOrEmpty(ProjectName))
                                   // orderby mm.MileStoneStatus descending
                                   select new DTO_PhysicalProgressMaster
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

        public ActionResult CreatePhysicalProgress(int ProjectPreparationID)
        {
            int? DistID = null;
            if (UserManager.GetUserLoginInfo(User.Identity.Name).RoleID == 2)
            {
                DistID = UserManager.GetUserLoginInfo(User.Identity.Name).DistID;
            }
            DTO_PhysicalProgressMaster model = new DTO_PhysicalProgressMaster();
            //ViewBag.ProjectID = new SelectList(db.MileStoneMasters.Where(x => x.IsActive == true && x.ProjectNo != null  && x.DistrictID == (DistID == null ? x.DistrictID : DistID)), "ProjectNo", "ProjectName", null);
            var ProjectNo = db.ProjectProposalPreprations.Where(x => x.ProjectPreparationID == ProjectPreparationID).FirstOrDefault().ProjectNo;
            model.DistrictID = DistID;
            model.ProjectNo = ProjectNo;
            model.ProjectPreparationID = ProjectPreparationID;
            ViewBag.ProjectID = new SelectList((from ppp in db.ProjectProposalPreprations
                                                   join mm in db.MileStoneMasters on ppp.ProjectPreparationID equals mm.ProjectPreparationID
                                                //join pm in db.ProjectMasters on ppp.ProjectNo equals pm.ProjectNo
                                               where ppp.IsActive == true && ppp.DistID == DistID && mm.ProjectPreparationID == ppp.ProjectPreparationID && ppp.Stageid==2 && mm.IsPhProgressDone==null && mm.IsFundReleased==true
                                               select new 
                                               {
                                                    ProjectNo = ppp.ProjectNo,
                                                   ProjectName = ppp.ProjectName
                                               }
                                             ).Distinct(), "ProjectNo", "ProjectName", ProjectNo);

            
            //model.DistrictID = DistID;
            //model.ProjectNo = ProjectNo;
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
            if (!String.IsNullOrEmpty(model.PhysicalProgressImages))
            {
                model.PhysicalProgressImages = BusinessLogics.UploadFileDMF(model.PhysicalProgressImages);
                if (model.PhysicalProgressImages.Contains("Expp::"))
                {
                    JR.Message = model.PhysicalProgressImages;
                    return Json(JR, JsonRequestBehavior.AllowGet);
                }
            }
            //var milestone = db.MileStoneMasters.Where(x => x.ProjectNo == model.ProjectNo && x.DistrictID == model.DistrictID).FirstOrDefault();
            var updateFlag= db.FundReleases.Where(x => x.DistrictID == model.DistrictID && x.ProjectPreparationID == model.ProjectPreparationID && x.IsPhProgressDone == null).FirstOrDefault();

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
                PhysicalProgressImages = model.PhysicalProgressImages,
                PhysicalPInPer = model.PhysicalPInPer,
                AmountSpend = model.AmountSpend,
                ProgressStatus=model.ProgressStatus,
                CreatedDate = DateTime.Now,
                CreatedBy = UserManager.GetUserLoginInfo(User.Identity.Name).LoginID.ToString(),
                IsActive = true,
                Phyicalintsallmentflag = updateFlag.InstallmentID.ToString(),
                SectorID=updateFlag.SectorID,
                SectorTypeId=updateFlag.SectorTypeId,
            }) ;
            int res = db.SaveChanges();
            if (res > 0)
            {
                JR.IsSuccess = true;
                JR.Message = "Data Saved Successfully";
                JR.RedURL = "/PhysicalProgress/PhysicalProgressList";
                var milestone = db.MileStoneMasters.Where(x => x.DistrictID == model.DistrictID && x.ProjectPreparationID == model.ProjectPreparationID && x.InstallmentID == updateFlag.InstallmentID).FirstOrDefault();
                var fundupdt = db.FundReleases.Where(x => x.DistrictID == model.DistrictID && x.ProjectPreparationID == model.ProjectPreparationID && x.InstallmentID == updateFlag.InstallmentID).FirstOrDefault();
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
                    return View("~/Views/PhysicalProgress/CreatePhysicalProgress.cshtml", new DTO_PhysicalProgressMaster { PhysicalprogressID = Info.PhysicalprogressID.ToString(), PhysicalProgressDate = Info.PhysicalProgressDate, AmountSpend = Info.AmountSpend, PhysicalPInPer = Info.PhysicalPInPer, PhysicalProgressCopy = Info.PhysicalProgressCopy, DistrictID = Info.DistrictID,Remark=Info.Remark ,PhysicalProgressImages=Info.PhysicalProgressImages,ProgressStatus=Info.ProgressStatus});
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
        public JsonResult MileStoneByProject(int DistrictID, int ProjectPreparationID)
        {
            var data = (from mm in db.MileStoneMasters
                        join ppp in db.ProjectProposalPreprations on mm.ProjectPreparationID equals ppp.ProjectPreparationID
                        join ins in db.InstallmentMasters on mm.InstallmentID equals ins.InstallmentID
                        join dm in db.DistrictMasters on mm.DistrictID equals dm.DistrictId
                        where mm.ProjectPreparationID == ProjectPreparationID && mm.DistrictID == DistrictID /*&& mm.IsFundReleased==true && mm.IsPhProgressDone !=true*/
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
            var data = (from fr in db.FundReleases
                            // join ins in db.InstallmentMasters on fr.InstallmentID equals ins.InstallmentID
                        join php in db.PhysicalProgressMasters on fr.FundReleaseID equals php.FundReleaseID into ps from php in ps.DefaultIfEmpty()
                        join ppp in db.ProjectProposalPreprations on fr.ProjectPreparationID equals ppp.ProjectPreparationID
                        join dm in db.DistrictMasters on fr.DistrictID equals dm.DistrictId
                        where php.ProjectPreparationID == ProjectPreparationID
                        select new DTO_PhysicalProgressMaster
                        {
                           // InstallmentName = fr.InstallmentName,
                         PhysicalprogressID= php.PhysicalprogressID.ToString(),
                            ProjectName = ppp.ProjectName
                            ,DistrictName =dm.DistrictName,
                            PhysicalPInPer= php.PhysicalPInPer,
                            PhysicalProgressDate= php.PhysicalProgressDate,
                            Remark= php.Remark,
                            PhysicalProgressCopy= php.PhysicalProgressCopy,
                            AmountSpend= php.AmountSpend,
                            IsFundReleased=fr.IsFundReleased,
                            IsPhProgressDone=fr.IsPhProgressDone,
                            IsUtilizationUploaded=fr.IsUtilizationUploaded,
                            IsInspectionDone=fr.IsInspectionDone,
                            //Phyicalintsallmentflag = php.Phyicalintsallmentflag
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
        [HttpPost]
        public JsonResult GetProgressPercentage(int ProjectPreparationID)
        {
            DTO_PhysicalProgressMaster obj = new DTO_PhysicalProgressMaster();
           using(SqlConnection con=new SqlConnection(coonectionstrings))
           using(SqlCommand cmd=new SqlCommand("sp_getProgressPercentage", con))
            {
                con.Open();
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ProjectPreparationID", ProjectPreparationID);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    obj.PhysicalPInPer = reader["PhysicalPInPer"].ToString();
                }
            }
           
            return Json(obj, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult GetMilestoneByInstallment(int ProjectPreparationID)
        {
            var data1 = db.MileStoneMasters.Where(x => x.ProjectPreparationID == ProjectPreparationID && x.IsFundReleased == true && x.IsPhProgressDone == null).FirstOrDefault();
            var data = (from mm in db.MileStoneMasters
                        join ppp in db.ProjectProposalPreprations on mm.ProjectPreparationID equals ppp.ProjectPreparationID into pps from ppp in pps.DefaultIfEmpty()
                        join ins in db.InstallmentMasters on  mm.InstallmentID equals ins.InstallmentID
                        where mm.ProjectPreparationID == ProjectPreparationID && mm.IsFundReleased == true && mm.IsPhProgressDone == null
                        select new DTO_MileStoneMaster
                        {
                            InstallmentName=ins.InstallmentName,
                            SanctionedProjectCost=ppp.SanctionedProjectCost,
                            Instext=mm.Instext,
                            InsPercentage=mm.InsPercentage
                        }).FirstOrDefault();
            return Json(data,JsonRequestBehavior.AllowGet);
        }
    }
}