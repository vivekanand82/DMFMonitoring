using DMFProjectFinal.DAL;
using DMFProjectFinal.Models;
using DMFProjectFinal.Models.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace DMFProjectFinal.Controllers
{
    [SessionFilter]
    public class AdminController : Controller
    {
        DashBoardReportDB DashBoardDB = new DashBoardReportDB();
        private dfm_dbEntities db = new dfm_dbEntities();
        public ActionResult Index()
        {
            var selectedyear = db.YearMasters.Where(x => x.YearName == DateTime.Now.Year.ToString()).FirstOrDefault().YearId;
           //var currentyear = selectedyear + "-" + (selectedyear + 1);
            ViewBag.finnyear =new SelectList(db.YearMasters.ToList(),"YearId","YearName", selectedyear);
            return View();
        }
        [HttpPost]
        public JsonResult DeleteDesig(string id)
        {
            try
            {
                int DesingantionID = int.Parse(CryptoEngine.Decrypt(id));
          var itm = db.DesignationMasters.Where(x => x.DesingantionID == DesingantionID).FirstOrDefault();
                if (itm != null)
                {
                    db.DesignationMasters.Remove(itm);
                    db.SaveChanges();
                    return Json("Data Deleted Successfully !");
                }
                else
                {
                    return Json("Invalid Data !");
                }
            }
            catch (Exception Ex)
            {
                return Json("Exp: " + Ex.Message, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult ChangePassword()
        {
            return View(new P_ChangePassword());
        }
        [HttpPost]
        public ActionResult ChangePassword(P_ChangePassword model)
        {
            var Lgn = UserManager.GetUserLoginInfo(User.Identity.Name);
            if (Lgn.Password != CryptoEngine.Encrypt(model.OldPass))
            {
                ViewBag.Msg = "Old Password is incorrect !";
                return View(model);
            }
            else
            {
                var _Login = db.UserLogins.Where(x => x.LoginID == Lgn.LoginID).FirstOrDefault();
                _Login.Password = CryptoEngine.Encrypt(model.NewPass);
                db.SaveChanges();
                ViewBag.Msg = "Password Updated Successfully !";
                return View(new P_ChangePassword());
            }
        }
        #region Get total counts on dashboard
        public JsonResult GetTotalCollectionAndExpendature(int? YearId)
        {
            int? DistID = null;
            if (UserManager.GetUserLoginInfo(User.Identity.Name).RoleID == 2)
            {
                DistID = UserManager.GetUserLoginInfo(User.Identity.Name).DistID;
                //model.DistrictId = DistID;
            }
            DTO_CollectionAndExpenditure obj = new DTO_CollectionAndExpenditure();
            DataSet ds = DashBoardDB.GetCollectionandExpediture(DistID, YearId);
            if (ds != null && ds.Tables[0].Rows.Count > 0 && ds.Tables.Count > 0)
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    obj.Total_DMFCollection = Convert.ToDecimal(dr["Total_DMFCollection"]);
                    obj.SanctionAmount = Convert.ToDecimal(dr["SanctionAmount"]);
                    obj.ReleasedAmount = Convert.ToDecimal(dr["ReleasedAmount"]);
                    obj.Expenditure = Convert.ToDecimal(dr["Expenditure"]);
                    obj.BalanceAmount = Convert.ToDecimal(dr["BalanceAmount"]);

                }
            }
            return Json(obj, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetProposalAndApprovalCount(int? YearId)
        {
            int? DistID = null;
            if (UserManager.GetUserLoginInfo(User.Identity.Name).RoleID == 2)
            {
                DistID = UserManager.GetUserLoginInfo(User.Identity.Name).DistID;
                //model.DistrictId = DistID;
            }
            DTO_ProposalAndApprovalCount obj = new DTO_ProposalAndApprovalCount();
            DataSet ds = DashBoardDB.GetProposalAndApprovalCount(DistID, YearId);
            if (ds != null && ds.Tables[0].Rows.Count > 0 && ds.Tables.Count > 0)
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    obj.ProjectProposals = Convert.ToInt32(dr["ProjectProposals"]);
                    obj.ApprovedProjects = Convert.ToInt32(dr["ApprovedProjects"]);
                    obj.CompletedProjects = Convert.ToInt32(dr["CompletedProjects"]);
                    obj.PendingProjects = Convert.ToInt32(dr["PendingProjects"]);

                }
            }
            return Json(obj, JsonRequestBehavior.AllowGet);
        }
        #endregion

    }
}