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
    public class DistrictWiseProjectDB
    {
        internal DataSet GetAllProjects(DTO_DistrictWiseProjectReport model)
        {
            SqlParameter[] para =
            {
                new SqlParameter("@Action","GetAllProjects"),
                new SqlParameter("@DistrictId",model.DistID),
            };
            DataSet ds = DBHelper.ExecuteQuery("dbo.sp_DistrictwiseProjectReport", para);
            return ds;
        }

        internal DataSet SectorTypeWiseProject(int distID)
        {
            SqlParameter[] para =
          {
                new SqlParameter("@Action","GetProjectsDistrictWise"),
                new SqlParameter("@DistrictId",distID),
            };
            DataSet ds = DBHelper.ExecuteQuery("dbo.sp_DistrictwiseProjectReport", para);
            return ds;
        }

        internal DataSet SectorNameWiseProject(int distID, int sectorTypeId)
        {
            SqlParameter[] para =
          {
                new SqlParameter("@Action","GetProjectsSectorTypeWise"),
                new SqlParameter("@DistrictId",distID),
                new SqlParameter("@SectorTypeId",sectorTypeId),
            };
            DataSet ds = DBHelper.ExecuteQuery("dbo.sp_DistrictwiseProjectReport", para);
            return ds;

        }

        internal DataSet SectorAndProjectReport(int distID, int sectorTypeId, int sectorID)
        {

            SqlParameter[] para =
          {
                new SqlParameter("@Action","GetProjectsSectorWise"),
                new SqlParameter("@DistrictId",distID),
                new SqlParameter("@SectorTypeId",sectorTypeId),
                new SqlParameter("@SectorID",sectorID),
            };
            DataSet ds = DBHelper.ExecuteQuery("dbo.sp_DistrictwiseProjectReport", para);
            return ds;
        }

        internal DataSet ProjectWiseReport(int distID, int sectorTypeId, int sectorID, string projectPreparationID)
        {

            SqlParameter[] para =
          {
                new SqlParameter("@Action","GetProjectDetails"),
                new SqlParameter("@DistrictId",distID),
                new SqlParameter("@SectorTypeId",sectorTypeId),
                new SqlParameter("@SectorID",sectorID),
                new SqlParameter("@ProjectPreparationID",projectPreparationID),
            };
            DataSet ds = DBHelper.ExecuteQuery("dbo.sp_DistrictwiseProjectReport", para);
            return ds;
        }
    }
}