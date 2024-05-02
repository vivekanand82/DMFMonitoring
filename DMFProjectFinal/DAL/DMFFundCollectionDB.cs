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

        internal DataSet GetCollectionMineralTypeWise(int DistrictId)
        {
            SqlParameter[] para =
          {
                new SqlParameter("@Action","DMFCOLLECTIONMINERALTYPEWISE"),
                new SqlParameter("@DistrictId",DistrictId),
            };
            DataSet ds = DBHelper.ExecuteQuery("dbo.DMF_FundCollectionDetails", para);
            return ds;
        }
        internal DataSet GetCollectionMineralNameWise(int DistrictId, string MineralType)
        {
            SqlParameter[] para =
          {
                new SqlParameter("@Action","DMFCOLLECTIONMINERALNAMEWISE"),
                new SqlParameter("@DistrictId",DistrictId),
                new SqlParameter("@MineralType",MineralType),
            };
            DataSet ds = DBHelper.ExecuteQuery("dbo.DMF_FundCollectionDetails", para);
            return ds;
        }
        internal DataSet GetCollectionMineralLesseeWise(int DistrictId, int MineralId, string MineralType)
        {
            SqlParameter[] para =
          {
                new SqlParameter("@Action","DMFCOLLECTIONMINERALESSEEWISE"),
                new SqlParameter("@DistrictId",DistrictId),
                new SqlParameter("@MineralId",MineralId),
                new SqlParameter("@MineralType",MineralType),
            };
            DataSet ds = DBHelper.ExecuteQuery("dbo.DMF_FundCollectionDetails", para);
            return ds;
        }

        internal DataSet GetCollectionReportByLessee(int DistrictId, int MineralId, int LesseeId)
        {
            SqlParameter[] para =
         {
                new SqlParameter("@Action","DmfCollectionByLesse"),
                new SqlParameter("@DistrictId",DistrictId),
                new SqlParameter("@MineralId",MineralId),
                new SqlParameter("@LesseeId",LesseeId),
            };
            DataSet ds = DBHelper.ExecuteQuery("dbo.DMF_FundCollectionDetails", para);
            return ds;
        }
    }
}