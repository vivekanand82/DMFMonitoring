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
        public ActionResult SanctionFundReport()
        {
            return View();
        }
    }
}