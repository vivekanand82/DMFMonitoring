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

        public ActionResult UtilizationList(string SectorType, string SectorName, string DistrictName, string ProjectName)
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

                var data = (from uc in db.UtilizationMasters
                            join
                                pm in db.ProjectMasters on uc.ProjectNo equals pm.ProjectNo
                            join dm in db.DistrictMasters on uc.DistrictID equals dm.DistrictId
                            join ppp in db.ProjectProposalPreprations on uc.ProjectPreparationID equals ppp.ProjectPreparationID
                            join stm in db.SectorNameMasters on ppp.SectorID equals stm.SectorNameId
                            join sym in db.SectorTypeMasters on ppp.SectorTypeId equals sym.SectorTypeID
                            where uc.IsActive==true && uc.DistrictID==DistID
                              && (sym.SectorType == SectorType || String.IsNullOrEmpty(SectorType))
                            && (stm.SectorName == SectorName || String.IsNullOrEmpty(SectorName))
                            && (dm.DistrictName == DistrictName || String.IsNullOrEmpty(DistrictName))
                            && (ppp.ProjectName == ProjectName || String.IsNullOrEmpty(ProjectName))
                            select new DTO_UtilizationMaster
                            {
                                ProjectPreparationID = uc.ProjectPreparationID,
                                DistrictName = dm.DistrictName,
                                ProjectName = pm.ProjectName,
                                DistrictID = uc.DistrictID,
                                //UtilizationID = uc.UtilizationID.ToString(),
                                //UtilizationCopy = uc.UtilizationCopy,
                                //UtilizationDate = uc.UtilizationDate,
                                //Remarks = uc.Remarks,
                                //UtilizationNo = uc.UtilizationNo
                                SectorName = stm.SectorName,
                                SectorType = sym.SectorType
                            }).Distinct().ToList();
                ViewBag.LstData = data;
            }
            else
            {
                ViewBag.DistrictName = new SelectList(db.DistrictMasters, "DistrictName", "DistrictName", null);
                ViewBag.ProjectName = new SelectList(db.ProjectProposalPreprations.Where(x => x.Stageid == 2), "ProjectName", "ProjectName", null);
                var data = (from uc in db.UtilizationMasters
                            join
                                pm in db.ProjectMasters on uc.ProjectNo equals pm.ProjectNo
                            join dm in db.DistrictMasters on uc.DistrictID equals dm.DistrictId
                            join ppp in db.ProjectProposalPreprations on uc.ProjectPreparationID equals ppp.ProjectPreparationID
                            join stm in db.SectorNameMasters on ppp.SectorID equals stm.SectorNameId
                            join sym in db.SectorTypeMasters on ppp.SectorTypeId equals sym.SectorTypeID
                            where
                               (sym.SectorType == SectorType || String.IsNullOrEmpty(SectorType))
                            && (stm.SectorName == SectorName || String.IsNullOrEmpty(SectorName))
                            && (dm.DistrictName == DistrictName || String.IsNullOrEmpty(DistrictName))
                            && (ppp.ProjectName == ProjectName || String.IsNullOrEmpty(ProjectName))
                            select new DTO_UtilizationMaster
                            {
                                ProjectPreparationID = uc.ProjectPreparationID,
                                DistrictName = dm.DistrictName,
                                ProjectName = pm.ProjectName,
                                DistrictID = uc.DistrictID,
                                //UtilizationID = uc.UtilizationID.ToString(),
                                //UtilizationCopy = uc.UtilizationCopy,
                                //UtilizationDate = uc.UtilizationDate,
                                //Remarks = uc.Remarks,
                                //UtilizationNo = uc.UtilizationNo
                                SectorName = stm.SectorName,
                                SectorType = sym.SectorType
                            }).Distinct().ToList();
                ViewBag.LstData = data;
            }
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
            ViewBag.ProjectID = new SelectList((from mm in db.MileStoneMasters
                                                join ppp in db.ProjectProposalPreprations on mm.ProjectNo equals ppp.ProjectNo into pps_left
                                                from pm in pps_left.DefaultIfEmpty()
                                                where pm.IsActive == true && mm.DistrictID == DistID && mm.ProjectNo == pm.ProjectNo && mm.IsPhProgressDone==true && mm.IsUtilizationUploaded!=true
                                                select new
                                                {
                                                    ProjectNo = mm.ProjectNo,
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
            var physicalflag = db.PhysicalProgressMasters.Where(x => x.ProjectNo == model.ProjectNo && x.DistrictID == model.DistrictID && x.Phyicalintsallmentflag != null).OrderByDescending(x=>x.Phyicalintsallmentflag).FirstOrDefault();
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
                UC_Against_ReleaseAmount=model.UC_Against_ReleaseAmount,
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
                var installment = Convert.ToInt32(physicalflag.Phyicalintsallmentflag);
                var milestone = db.MileStoneMasters.Where(x => x.DistrictID == model.DistrictID && x.ProjectNo == model.ProjectNo && x.InstallmentID == installment).FirstOrDefault();
                var fundupdt = db.FundReleases.Where(x => x.DistrictID == model.DistrictID && x.ProjectNo == model.ProjectNo && x.InstallmentID == installment).FirstOrDefault();
                if (milestone != null)
                {
                    //DTO_MileStoneMaster mile = new DTO_MileStoneMaster();
                    milestone.IsUtilizationUploaded = true;
                    db.Entry(milestone).State = System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();
                }
                if (fundupdt != null)
                {
                    //DTO_MileStoneMaster mile = new DTO_MileStoneMaster();
                    fundupdt.IsUtilizationUploaded = true;
                    db.Entry(fundupdt).State = System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();
                }
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

            return View("~/Views/Utilization/CreateUtilization.cshtml",new DTO_UtilizationMaster { UtilizationID =data.UtilizationID.ToString(),UtilizationNo=data.UtilizationNo,DistrictID=data.DistrictID,ProjectNo=data.ProjectNo,UtilizationCopy=data.UtilizationCopy,UtilizationDate=data.UtilizationDate,Remarks=data.Remarks, UC_Against_ReleaseAmount =data.UC_Against_ReleaseAmount});
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
            data.UC_Against_ReleaseAmount = model.UC_Against_ReleaseAmount;
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
            //JsonResponse JR = new JsonResponse();
            string msg = "";
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
                msg = "1";
            }
            else
            {
                msg = "0";
            }
            return Json(msg, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetUtilizationDetails(int ProjectPreparationID)
        {
            var data = (from uc in db.UtilizationMasters
                            //join ins in db.InstallmentMasters on fr.InstallmentID equals ins.InstallmentID
                        join pm in db.ProjectMasters on uc.ProjectNo equals pm.ProjectNo
                        join dm in db.DistrictMasters on uc.DistrictID equals dm.DistrictId
                        where uc.ProjectPreparationID == ProjectPreparationID
                        select new DTO_UtilizationMaster
                        {
                            //InstallmentName = fr.InstallmentName,
                            UtilizationID = uc.UtilizationID.ToString(),
                            ProjectName = pm.ProjectName,
                            DistrictName = dm.DistrictName,
                            UtilizationNo=uc.UtilizationNo,
                            UtilizationDate=uc.UtilizationDate,
                            UtilizationCopy=uc.UtilizationCopy,
                            Remarks=uc.Remarks,
                            UC_Against_ReleaseAmount=uc.UC_Against_ReleaseAmount
                        }).ToList();
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult MileStoneByProject(int DistrictID, string ProjectNo)
        {
            var data = (from mm in db.MileStoneMasters
                        join ppp in db.ProjectProposalPreprations on mm.ProjectPreparationID equals ppp.ProjectPreparationID
                        join ins in db.InstallmentMasters on mm.InstallmentID equals ins.InstallmentID
                        join dm in db.DistrictMasters on mm.DistrictID equals dm.DistrictId
                        where mm.ProjectNo == ProjectNo && mm.DistrictID == DistrictID && mm.IsFundReleased == true && mm.IsPhProgressDone == true
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
        //code start for binding dropdowns using filter
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