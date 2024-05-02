using DMFProjectFinal.DAL;
using DMFProjectFinal.Models.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DMFProjectFinal.Controllers
{
    [SessionFilter]
    public class ReportController : Controller
    {
        // GET: Report
        DMFFundCollectionDB CollectionDB=new DMFFundCollectionDB();
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult DMFFundCollectionReport(DTO_FundCollectionReport model)
        {
            int? DistID = null;
            if (UserManager.GetUserLoginInfo(User.Identity.Name).RoleID == 2)
            {
                DistID = UserManager.GetUserLoginInfo(User.Identity.Name).DistID;
                model.DistrictId =DistID;
            }
            List<DTO_FundCollectionReport> lst = new List<DTO_FundCollectionReport>();
            DataSet ds = CollectionDB.GetTotalFundCollection(model);
            if (ds != null && ds.Tables[0].Rows.Count > 0 && ds.Tables.Count > 0)
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    lst.Add(new DTO_FundCollectionReport
                    {
                        DistrictId = (int)dr["DistrictId"],
                        DistrictName = dr["DistrictName"].ToString(),
                      TotalFundCollection=Convert.ToDecimal(dr["TotalFundCollection"]),
                    });
                    ViewBag.data = lst;
                }
            }
                return View();
        }
        [HttpPost]
        public JsonResult FundCollectionMineralTypeWise(int DistrictId)
        {
            //int? DistID = null;
            //if (UserManager.GetUserLoginInfo(User.Identity.Name).RoleID == 2)
            //{
            //    DistID = UserManager.GetUserLoginInfo(User.Identity.Name).DistID;
            //    model.DistrictId = DistID;
            //}
            List<DTO_FundCollectionReport> lst = new List<DTO_FundCollectionReport>();
            DataSet ds = CollectionDB.GetCollectionMineralTypeWise(DistrictId);
            if (ds != null && ds.Tables[0].Rows.Count > 0 && ds.Tables.Count > 0)
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    lst.Add(new DTO_FundCollectionReport
                    {
                        DistrictId = (int)dr["DistrictId"],
                        DistrictName = dr["DistrictName"].ToString(),
                        MineralType = dr["MineralType"].ToString(),
                        TotalFundCollection = Convert.ToDecimal(dr["TotalFundCollection"]),
                    });
                   
                }
            }
            return Json(lst, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult FundCollectionMineralNameWise(int DistrictId, string MineralType)
        {
            //int? DistID = null;
            //if (UserManager.GetUserLoginInfo(User.Identity.Name).RoleID == 2)
            //{
            //    DistID = UserManager.GetUserLoginInfo(User.Identity.Name).DistID;
            //    model.DistrictId = DistID;
            //}
            List<DTO_FundCollectionReport> lst = new List<DTO_FundCollectionReport>();
            DataSet ds = CollectionDB.GetCollectionMineralNameWise(DistrictId, MineralType);
            if (ds != null && ds.Tables[0].Rows.Count > 0 && ds.Tables.Count > 0)
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    lst.Add(new DTO_FundCollectionReport
                    {
                        MineralId = (int)dr["MineralId"],
                        DistrictId = (int)dr["DistrictId"],
                        DistrictName = dr["DistrictName"].ToString(),
                        MineralType = dr["MineralType"].ToString(),
                        MineralName = dr["MineralName"].ToString(),
                        TotalFundCollection = Convert.ToDecimal(dr["TotalFundCollection"]),
                    });

                }
            }
            return Json(lst, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult FundCollectionLesseeWise(int DistrictId,int MineralId, string MineralType)
        {
            //int? DistID = null;
            //if (UserManager.GetUserLoginInfo(User.Identity.Name).RoleID == 2)
            //{
            //    DistID = UserManager.GetUserLoginInfo(User.Identity.Name).DistID;
            //    model.DistrictId = DistID;
            //}
            List<DTO_FundCollectionReport> lst = new List<DTO_FundCollectionReport>();
            DataSet ds = CollectionDB.GetCollectionMineralLesseeWise(DistrictId, MineralId, MineralType);
            if (ds != null && ds.Tables[0].Rows.Count > 0 && ds.Tables.Count > 0)
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    lst.Add(new DTO_FundCollectionReport
                    {
                        DistrictId = (int)dr["DistrictId"],
                        MineralId = (int)dr["MineralId"],
                        LesseeId = (long)dr["LesseeId"],
                        DistrictName = dr["DistrictName"].ToString(),
                        MineralType = dr["MineralType"].ToString(),
                        MineralName = dr["MineralName"].ToString(),
                        LeseeName = dr["LeseeName"].ToString(),
                        TotalFundCollection = Convert.ToDecimal(dr["COLLECTIONT"]),
                    });

                }
            }
            return Json(lst, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult FundCollectionReportByLessee(int DistrictId, int MineralId,int LesseeId)
        {
            //int? DistID = null;
            //if (UserManager.GetUserLoginInfo(User.Identity.Name).RoleID == 2)
            //{
            //    DistID = UserManager.GetUserLoginInfo(User.Identity.Name).DistID;
            //    model.DistrictId = DistID;
            //}
            List<DTO_FundCollectionReport> lst = new List<DTO_FundCollectionReport>();
            DataSet ds = CollectionDB.GetCollectionReportByLessee(DistrictId, MineralId, LesseeId);
            if (ds != null && ds.Tables[0].Rows.Count > 0 && ds.Tables.Count > 0)
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    lst.Add(new DTO_FundCollectionReport
                    {
                        DistrictId = (int)dr["DistrictId"],
                        DepositedDMFAmt =Convert.ToDecimal(dr["DepositedDMFAmt"]),
                        ChallanDate = Convert.ToDateTime(dr["ChallanDate"]),
                        ChallanNo = dr["ChallanNo"].ToString(),
                        DistrictName = dr["DistrictName"].ToString(),
                        LesseeId = (long)dr["LesseeId"],
                        MineralName = dr["MineralName"].ToString(),
                        MineralType = dr["MineralType"].ToString(),
                        LeseeName = dr["LesseeName"].ToString(),
                        MineralId = (int)dr["MineralId"],
                        challanDOc = dr["challanDOc"].ToString(),
                        Areacode = dr["Areacode"].ToString(),
                        GataNo = dr["GataNo"].ToString(),
                        TotalAreainHect = Convert.ToDecimal(dr["TotalAreainHect"]),
                        LeaseID = dr["LeaseID"].ToString(),
                        MobNo = dr["MobNo"].ToString(),
                        EmailID = dr["EmailID"].ToString(),
                        BidRate = Convert.ToDecimal(dr["BidRate"]),
                        LeaseFromDate = Convert.ToDateTime(dr["LeaseFromDate"]),
                        LeaseToDate = Convert.ToDateTime(dr["LeaseToDate"]),
                        RoyaltyAmt = Convert.ToDecimal(dr["RoyaltyAmt"]),
                        DMFAmt = Convert.ToDecimal(dr["DMFAmt"]),
                        RemainingDMFAmt = Convert.ToDecimal(dr["RemainingDMFAmt"]),
                        YearName = dr["YearName"].ToString(),
                        MonthName = dr["MonthName"].ToString(),

                    });

                }
            }
            return Json(lst, JsonRequestBehavior.AllowGet);
        }
        public ActionResult SanctionFundReport()
        {
            return View();
        }
    }
}