using DMFProjectFinal.Models;
using DMFProjectFinal.Models.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace DMFProjectFinal.DAL
{
    public class DMFFundCollectionDB
    {
        internal DataSet GetTotalFundCollection(DTO_FundCollectionReport model)
        {
            SqlParameter[] para =
            {
                new SqlParameter("@Action","GETALLCOLLECTION"),
                new SqlParameter("@DistrictId",model.DistrictId),
            };
            DataSet ds = DBHelper.ExecuteQuery("dbo.DMF_FundCollectionDetails", para);
            return ds;
        }

       

    }
}