using DMFProjectFinal.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace DMFProjectFinal.DAL
{
    public class DashBoardReportDB
    {
        internal DataSet GetCollectionandExpediture(int? distID, int? YearId)
        {
            SqlParameter[] para =
           {
                new SqlParameter("@Action","GetCollectionAndExpenditure"),
                new SqlParameter("@DistrictId",distID),
                new SqlParameter("@YearId",YearId),

            };
            DataSet ds = DBHelper.ExecuteQuery("dbo.sp_DetailsOnDashBoard", para);
            return ds;
        }

        internal DataSet GetProposalAndApprovalCount(int? distID, int? YearId)
        {
            SqlParameter[] para =
           {
                new SqlParameter("@Action","ProjectDetailsOnDashBoard"),
                new SqlParameter("@DistrictId",distID),
                new SqlParameter("@YearId",YearId),
            };
            DataSet ds = DBHelper.ExecuteQuery("dbo.sp_DetailsOnDashBoard", para);
            return ds;
        }
    }
}