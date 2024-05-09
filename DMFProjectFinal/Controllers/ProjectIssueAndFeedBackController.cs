using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DMFProjectFinal.DAL;
using DMFProjectFinal.Models;
using DMFProjectFinal.Models.DTO;

namespace DMFProjectFinal.Controllers
{
    [SessionFilter]
    public class ProjectIssueAndFeedBackController : Controller
    {
        // GET: ProjectIssueAndFeedBack
        IssueAndFeedBackDB IssueDb = new IssueAndFeedBackDB();
        dfm_dbEntities db = new dfm_dbEntities();
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult ProjectAssesment()//thsi is the data from project preparation table which proiject is completed
        {
            List<DTO_ProjectAssessment> lst = new List<DTO_ProjectAssessment>();
            int? DistID = null;
            if (UserManager.GetUserLoginInfo(User.Identity.Name).RoleID == 2)
            {
                DistID = UserManager.GetUserLoginInfo(User.Identity.Name).DistID;
            }
            DataSet ds = IssueDb.GetAllProjectsForAssessment(DistID);
            if (ds != null && ds.Tables[0].Rows.Count > 0 && ds.Tables.Count > 0)
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    lst.Add(new DTO_ProjectAssessment
                    {
                        ProjectPreparationID = (int)dr["ProjectPreparationID"],
                        DistrictId = (int)dr["DistrictId"],
                        SectorID = (int)dr["SectorId"],
                        SectorTypeId = (int)dr["SectorTypeId"],
                        DistrictName = dr["DistrictName"].ToString(),
                        SectorName = dr["SectorName"].ToString(),
                        SectorType = dr["SectorType"].ToString(),
                        ProjectName = dr["ProjectName"].ToString(),
                    });
                    ViewBag.LstData = lst;
                }
            }
            return View();
        }
        public ActionResult ProjectAssesmentList()// This is the data which have completed project assesments
        {
            List<DTO_ProjectAssessment> lst = new List<DTO_ProjectAssessment>();
            int? DistID = null;
            if (UserManager.GetUserLoginInfo(User.Identity.Name).RoleID == 2)
            {
                DistID = UserManager.GetUserLoginInfo(User.Identity.Name).DistID;
            }
            DataSet ds = IssueDb.GetAllProjectsAssessmentList(DistID);
            if (ds != null && ds.Tables[0].Rows.Count > 0 && ds.Tables.Count > 0)
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    lst.Add(new DTO_ProjectAssessment
                    {
                        AssesmentId = dr["AssesmentId"].ToString(),
                        ProjectPreparationID = (int)dr["ProjectPreparationID"],
                        DistrictId = (int)dr["DistrictId"],
                        SectorID = (int)dr["SectorId"],
                        SectorTypeId = (int)dr["SectorTypeId"],
                        DistrictName = dr["DistrictName"].ToString(),
                        SectorName = dr["SectorName"].ToString(),
                        SectorType = dr["SectorType"].ToString(),
                        ProjectName = dr["ProjectName"].ToString(),
                        feedBack = dr["feedBack"].ToString(),
                        Photos = dr["Photos"].ToString(),
                        NoOfBeneficiaries = (int)dr["NoOfBeneficiaries"],


                    });
                    ViewBag.LstData = lst;
                }
            }
            return View();
        }
        public ActionResult CreateProjectAssesment(int ProjectPreparationID)
        {
            DTO_ProjectAssessment model =new  DTO_ProjectAssessment();
            int? DistID = null;
            if (UserManager.GetUserLoginInfo(User.Identity.Name).RoleID == 2)
            {
                DistID = UserManager.GetUserLoginInfo(User.Identity.Name).DistID;
            }
            var data = db.ProjectProposalPreprations.Where(x => x.ProjectPreparationID == ProjectPreparationID).FirstOrDefault();
            ViewBag.ProjectID = new SelectList(db.ProjectProposalPreprations.Where(x => x.ProjectPreparationID == ProjectPreparationID).ToList(), "ProjectPreparationID", "ProjectName", ProjectPreparationID);
            ViewBag.SectorType = new SelectList(db.SectorTypeMasters.Where(x => x.SectorTypeID == data.SectorTypeId).ToList(), "SectorTypeId", "SectorType", data.SectorTypeId);
            ViewBag.SectorId = new SelectList(db.SectorNameMasters.Where(x => x.SectorNameId == data.SectorID).ToList(), "SectorNameId", "SectorName", data.SectorID);
            model.DistrictId = data.DistID;
            model.ProjectNo = data.ProjectNo;
            model.SectorTypeId = data.SectorTypeId;
            model.SectorID = data.SectorID;
            ViewBag.projectData = db.ProjectProposalPreprations.Where(x => x.ProjectPreparationID == ProjectPreparationID).FirstOrDefault();
            return View(model);
        } 
        [HttpPost]
        public ActionResult CreateProjectAssesment(DTO_ProjectAssessment model)
        {
            string msg = "";
            int? DistID = null;
            if (UserManager.GetUserLoginInfo(User.Identity.Name).RoleID == 2)
            {
                DistID = UserManager.GetUserLoginInfo(User.Identity.Name).DistID;
            }
            if (!String.IsNullOrEmpty(model.Photos))
            {
                model.Photos = BusinessLogics.UploadFileDMF(model.Photos);
                if (model.Photos.Contains("Expp::"))
                {
                    msg = model.Photos;
                    return Json(msg, JsonRequestBehavior.AllowGet);
                }
            }
            model.CreatedBy = UserManager.GetUserLoginInfo(User.Identity.Name).LoginID.ToString();
            DataSet ds = IssueDb.SaveProjectAssesment(model);
            if (ds.Tables[0].Rows[0]["Code"].ToString() == "1")
            {
                msg= ds.Tables[0].Rows[0]["Code"].ToString();
            }
            else
            {
                msg = ds.Tables[0].Rows[0]["Code"].ToString();
            }
            return Json(msg,JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult EditProjectAssesment(int AssesmentId)
        {
            int? DistID = null;
            if (UserManager.GetUserLoginInfo(User.Identity.Name).RoleID == 2)
            {
                DistID = UserManager.GetUserLoginInfo(User.Identity.Name).DistID;
            }
            var data = db.ProjectAssessments.Where(x => x.AssesmentId == AssesmentId).FirstOrDefault();
            ViewBag.ProjectID = new SelectList(db.ProjectProposalPreprations.Where(x => x.ProjectPreparationID == data.ProjectPreparationID).ToList(), "ProjectPreparationID", "ProjectName", data.ProjectPreparationID);
            ViewBag.SectorType = new SelectList(db.SectorTypeMasters.Where(x => x.SectorTypeID == data.SectorTypeId).ToList(), "SectorTypeId", "SectorType", data.SectorTypeId);
            ViewBag.SectorId = new SelectList(db.SectorNameMasters.Where(x => x.SectorNameId == data.SectorId).ToList(), "SectorNameId", "SectorName", data.SectorId);
            ViewBag.projectData = db.ProjectProposalPreprations.Where(x => x.ProjectPreparationID == data.ProjectPreparationID).FirstOrDefault();

            return View("~/Views/ProjectIssueAndFeedBack/CreateProjectAssesment.cshtml", new DTO_ProjectAssessment { AssesmentId = data.AssesmentId.ToString(), DistrictId = data.DistrictId, SectorID = data.SectorId, ProjectNo = data.ProjectNo, SectorTypeId = data.SectorTypeId, ProjectPreparationID = data.ProjectPreparationID, feedBack = data.feedBack, Photos = data.Photos, NoOfBeneficiaries = data.NoOfBeneficiaries });
        }
        [HttpPost]
        public ActionResult UpdateProjectAssessment(DTO_ProjectAssessment model)
        {
            string msg = "";
            int? DistID = null;
            if (UserManager.GetUserLoginInfo(User.Identity.Name).RoleID == 2)
            {
                DistID = UserManager.GetUserLoginInfo(User.Identity.Name).DistID;
            }
            if (!String.IsNullOrEmpty(model.Photos))
            {
                model.Photos = BusinessLogics.UploadFileDMF(model.Photos);
                if (model.Photos.Contains("Expp::"))
                {
                    msg = model.Photos;
                    return Json(msg, JsonRequestBehavior.AllowGet);
                }
            }
            model.CreatedBy = UserManager.GetUserLoginInfo(User.Identity.Name).LoginID.ToString();
            DataSet ds = IssueDb.UpdateProjectAssesment(model);
            if (ds.Tables[0].Rows[0]["Code"].ToString() == "1")
            {
                msg = ds.Tables[0].Rows[0]["Code"].ToString();
            }
            else
            {
                msg = ds.Tables[0].Rows[0]["Code"].ToString();
            }
            return Json(msg, JsonRequestBehavior.AllowGet);
        }
        public JsonResult DeleteProjectAssessment(int AssesmentId)
        {
            //JsonResponse JR = new JsonResponse();
            string msg = "";
            if (AssesmentId > 0)
            {
                var Info = db.ProjectAssessments.Where(x => x.AssesmentId == AssesmentId).FirstOrDefault();
                if (Info != null)
                {
                    db.ProjectAssessments.Remove(Info);
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
        public ActionResult StuckedProjectList()
        {
            int? DistID = null;
            if (UserManager.GetUserLoginInfo(User.Identity.Name).RoleID == 2)
            {
                DistID = UserManager.GetUserLoginInfo(User.Identity.Name).DistID;
            }
            List<DTO_ProjectIssueDetail> lst = new List<DTO_ProjectIssueDetail>();
            DataSet ds = IssueDb.GetStuckedProject(DistID);
            if (ds != null && ds.Tables[0].Rows.Count > 0 && ds.Tables.Count > 0)
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    lst.Add(new DTO_ProjectIssueDetail
                    {
                        ProjectPreparationID = (int)dr["ProjectPreparationID"],
                        DistrictId = (int)dr["DistrictId"],
                        SectorID = (int)dr["SectorId"],
                        SectorTypeId = (int)dr["SectorTypeId"],
                        DistrictName = dr["DistrictName"].ToString(),
                        SectorName = dr["SectorName"].ToString(),
                        SectorType = dr["SectorType"].ToString(),
                        ProjectName = dr["ProjectName"].ToString(),
                        ProjectCompletionDate =Convert.ToDateTime(dr["ProjectCompletionDate"]),
                    });
                    ViewBag.LstData = lst;
                }
            }
            return View();
        }
        public ActionResult CreateProjectIssue(int ProjectPreparationID)
        {
            DTO_ProjectIssueDetail model = new DTO_ProjectIssueDetail();
            int? DistID = null;
            if (UserManager.GetUserLoginInfo(User.Identity.Name).RoleID == 2)
            {
                DistID = UserManager.GetUserLoginInfo(User.Identity.Name).DistID;
            }
            var data = db.ProjectProposalPreprations.Where(x => x.ProjectPreparationID == ProjectPreparationID).FirstOrDefault();
            ViewBag.ProjectID = new SelectList(db.ProjectProposalPreprations.Where(x => x.ProjectPreparationID == ProjectPreparationID).ToList(), "ProjectPreparationID", "ProjectName", ProjectPreparationID);
            ViewBag.SectorType = new SelectList(db.SectorTypeMasters.Where(x => x.SectorTypeID == data.SectorTypeId).ToList(), "SectorTypeId", "SectorType", data.SectorTypeId);
            ViewBag.SectorId = new SelectList(db.SectorNameMasters.Where(x => x.SectorNameId == data.SectorID).ToList(), "SectorNameId", "SectorName", data.SectorID);
            model.DistrictId = data.DistID;
            model.ProjectNo = data.ProjectNo;
            model.SectorTypeId = data.SectorTypeId;
            model.SectorID = data.SectorID;
            ViewBag.projectData = db.ProjectProposalPreprations.Where(x => x.ProjectPreparationID == ProjectPreparationID).FirstOrDefault();
            return View(model);
        }
        [HttpPost]
        public ActionResult CreateProjectIssue(DTO_ProjectIssueDetail model)
        {
            string msg = "";
            int? DistID = null;
            if (UserManager.GetUserLoginInfo(User.Identity.Name).RoleID == 2)
            {
                DistID = UserManager.GetUserLoginInfo(User.Identity.Name).DistID;
            }
            
            model.CreatedBy = UserManager.GetUserLoginInfo(User.Identity.Name).LoginID.ToString();
            DataSet ds = IssueDb.SaveProjectIssue(model);
            if (ds.Tables[0].Rows[0]["Code"].ToString() == "1")
            {
                msg = ds.Tables[0].Rows[0]["Code"].ToString();
            }
            else
            {
                msg = ds.Tables[0].Rows[0]["Code"].ToString();
            }
            return Json(msg, JsonRequestBehavior.AllowGet);
        }
        public ActionResult UpdatedProjectIssueList()// This is the data which have completed project assesments
        {
            List<DTO_ProjectIssueDetail> lst = new List<DTO_ProjectIssueDetail>();
            int? DistID = null;
            if (UserManager.GetUserLoginInfo(User.Identity.Name).RoleID == 2)
            {
                DistID = UserManager.GetUserLoginInfo(User.Identity.Name).DistID;
            }
            DataSet ds = IssueDb.GetUpdatedProjectIssueList(DistID);
            if (ds != null && ds.Tables[0].Rows.Count > 0 && ds.Tables.Count > 0)
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    lst.Add(new DTO_ProjectIssueDetail
                    {
                        ProjectIssueId = dr["ProjectIssueId"].ToString(),
                        ProjectPreparationID = (int)dr["ProjectPreparationID"],
                        DistrictId = (int)dr["DistrictId"],
                        SectorID = (int)dr["SectorId"],
                        SectorTypeId = (int)dr["SectorTypeId"],
                        DistrictName = dr["DistrictName"].ToString(),
                        SectorName = dr["SectorName"].ToString(),
                        SectorType = dr["SectorType"].ToString(),
                        ProjectName = dr["ProjectName"].ToString(),
                        IssueCategory = dr["IssueCategory"].ToString(),
                        Reason = dr["Reason"].ToString(),
                        IssueDate = (DateTime)dr["IssueDate"],
                    });
                    ViewBag.LstData = lst;
                }
            }
            return View();
        }
        public ActionResult EditProjectIssue(int ProjectIssueId)
        {
            int? DistID = null;
            if (UserManager.GetUserLoginInfo(User.Identity.Name).RoleID == 2)
            {
                DistID = UserManager.GetUserLoginInfo(User.Identity.Name).DistID;
            }
            var data = db.ProjectIssueDetails.Where(x => x.ProjectIssueId == ProjectIssueId).FirstOrDefault();
            ViewBag.ProjectID = new SelectList(db.ProjectProposalPreprations.Where(x => x.ProjectPreparationID == data.ProjectPreparationID).ToList(), "ProjectPreparationID", "ProjectName", data.ProjectPreparationID);
            ViewBag.SectorType = new SelectList(db.SectorTypeMasters.Where(x => x.SectorTypeID == data.SectorTypeId).ToList(), "SectorTypeId", "SectorType", data.SectorTypeId);
            ViewBag.SectorId = new SelectList(db.SectorNameMasters.Where(x => x.SectorNameId == data.SectorId).ToList(), "SectorNameId", "SectorName", data.SectorId);
            ViewBag.projectData = db.ProjectProposalPreprations.Where(x => x.ProjectPreparationID == data.ProjectPreparationID).FirstOrDefault();

            return View("~/Views/ProjectIssueAndFeedBack/CreateProjectIssue.cshtml", new DTO_ProjectIssueDetail { ProjectIssueId = data.ProjectIssueId.ToString(), DistrictId = data.DistrictId, SectorID = data.SectorId, ProjectNo = data.ProjectNo, SectorTypeId = data.SectorTypeId, ProjectPreparationID = data.ProjectPreparationID, IssueCategory = data.IssueCategory, Reason = data.Reason, IssueDate = data.IssueDate });
        }
        [HttpPost]
        public ActionResult UpdateProjectIssue(DTO_ProjectIssueDetail model)
        {
            string msg = "";
            int? DistID = null;
            if (UserManager.GetUserLoginInfo(User.Identity.Name).RoleID == 2)
            {
                DistID = UserManager.GetUserLoginInfo(User.Identity.Name).DistID;
            }

            model.CreatedBy = UserManager.GetUserLoginInfo(User.Identity.Name).LoginID.ToString();
            DataSet ds = IssueDb.UpdateProjectIssue(model);
            if (ds.Tables[0].Rows[0]["Code"].ToString() == "1")
            {
                msg = ds.Tables[0].Rows[0]["Code"].ToString();
            }
            else
            {
                msg = ds.Tables[0].Rows[0]["Code"].ToString();
            }
            return Json(msg, JsonRequestBehavior.AllowGet);
        }
        public JsonResult DeleteProjectIssue(int ProjectIssueId)
        {
            //JsonResponse JR = new JsonResponse();
            string msg = "";
            if (ProjectIssueId > 0)
            {
                var Info = db.ProjectIssueDetails.Where(x => x.ProjectIssueId == ProjectIssueId).FirstOrDefault();
                if (Info != null)
                {
                    db.ProjectIssueDetails.Remove(Info);
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
    }
}