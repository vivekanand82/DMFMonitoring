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

        public ActionResult ReleaseFund( )
        {
            int? DistID = null;
            if (UserManager.GetUserLoginInfo(User.Identity.Name).RoleID == 2)
            {
                DistID = UserManager.GetUserLoginInfo(User.Identity.Name).DistID;

                var data = (from fr in db.FundReleases
                            join dm in db.DistrictMasters on fr.DistrictID equals dm.DistrictId
                            join pm in db.ProjectMasters on fr.ProjectNo equals pm.ProjectNo
                            join ins in db.InstallmentMasters on fr.InstallmentID equals ins.InstallmentID
                            join stm in db.SectorTypeMasters on pm.SectorTypeId equals stm.SectorTypeID
                            join snm in db.SectorNameMasters on pm.SectorNameId equals snm.SectorNameId
                            join ppp in db.ProjectProposalPreprations on fr.ProjectNo equals ppp.ProjectNo
                            where fr.IsActive == true && fr.DistrictID == DistID && fr.ProjectNo != null && ppp.Stageid == 2
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
            ViewBag.LstData = data;
            }
            else
            {
                var data = (from fr in db.FundReleases
                            join dm in db.DistrictMasters on fr.DistrictID equals dm.DistrictId
                            join pm in db.ProjectMasters on fr.ProjectNo equals pm.ProjectNo
                            join ins in db.InstallmentMasters on fr.InstallmentID equals ins.InstallmentID
                            join stm in db.SectorTypeMasters on pm.SectorTypeId equals stm.SectorTypeID
                            join snm in db.SectorNameMasters on pm.SectorNameId equals snm.SectorNameId
                            join ppp in db.ProjectProposalPreprations on fr.ProjectNo equals ppp.ProjectNo
                            where fr.IsActive == true && fr.ProjectNo != null && ppp.Stageid == 2
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
            db.FundReleases.Add(new FundRelease
            {
                DistrictID = model.DistrictID,
                ProjectNo = model.ProjectNo,
                RelaeseDate = model.RelaeseDate,
                ReleaseAmount = model.ReleaseAmount,
                InstallmentID = model.InstallmentID,
                FundReleaseCopy = model.FundReleaseCopy,
                CreatedDate = DateTime.Now,
                IsActive=true,
                CreatedBy= UserManager.GetUserLoginInfo(User.Identity.Name).LoginID.ToString(),
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
                        where fr.IsActive == true && fr.DistrictID == DistrictID && fr.ProjectNo == ProjectNo
                        join stm in db.SectorTypeMasters on pm.SectorTypeId equals stm.SectorTypeID
                        join snm in db.SectorNameMasters on pm.SectorNameId equals snm.SectorNameId
                        join ppp in db.ProjectProposalPreprations on fr.ProjectNo equals ppp.ProjectNo
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
    }
}