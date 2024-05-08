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
    public class IssueAndFeedBackDB
    {
        internal DataSet SaveProjectAssesment(DTO_ProjectAssessment model)
        {
            SqlParameter[] para =
            {
                new SqlParameter("@Action","SaveProjectAssessment"),
                new SqlParameter("@DistrictId",model.DistrictId),
                new SqlParameter("@SectorTypeId",model.SectorTypeId),
                new SqlParameter("@SectorId",model.SectorID),
                new SqlParameter("@ProjectPreparationID",model.ProjectPreparationID),
                new SqlParameter("@ProjectNo",model.ProjectNo),
                new SqlParameter("@feedBack",model.feedBack),
                new SqlParameter("@Photos",model.Photos),
                new SqlParameter("@NoOfBeneficiaries",model.NoOfBeneficiaries),
                new SqlParameter("@CreatedBy",model.CreatedBy),
                new SqlParameter("@ModifiedBy",model.ModifiedBy),
            };
            DataSet ds = DBHelper.ExecuteQuery("dbo.sp_projectIssueAndFeedBack", para);
            return ds;
        }

        internal DataSet GetAllProjectsForAssessment(int? distID)
        {
            SqlParameter[] para =
        {
                new SqlParameter("@Action","GetCompletedprojects"),
                new SqlParameter("@DistrictId",distID),
            };
            DataSet ds = DBHelper.ExecuteQuery("dbo.sp_projectIssueAndFeedBack", para);
            return ds;
        }

        internal DataSet GetAllProjectsAssessmentList(int? distID)
        {
            SqlParameter[] para =
        {
                new SqlParameter("@Action","GetAllAssessmentList"),
                new SqlParameter("@DistrictId",distID),
            };
            DataSet ds = DBHelper.ExecuteQuery("dbo.sp_projectIssueAndFeedBack", para);
            return ds;
        }

        internal DataSet UpdateProjectAssesment(DTO_ProjectAssessment model)
        {
            SqlParameter[] para =
            {
                new SqlParameter("@Action","UpdateProjectAssessment"),
                new SqlParameter("@AssesmentId",model.AssesmentId),
                new SqlParameter("@DistrictId",model.DistrictId),
                new SqlParameter("@SectorTypeId",model.SectorTypeId),
                new SqlParameter("@SectorId",model.SectorID),
                new SqlParameter("@ProjectPreparationID",model.ProjectPreparationID),
                new SqlParameter("@ProjectNo",model.ProjectNo),
                new SqlParameter("@feedBack",model.feedBack),
                new SqlParameter("@Photos",model.Photos),
                new SqlParameter("@NoOfBeneficiaries",model.NoOfBeneficiaries),
                new SqlParameter("@CreatedBy",model.CreatedBy),
                new SqlParameter("@ModifiedBy",model.ModifiedBy),
            };
            DataSet ds = DBHelper.ExecuteQuery("dbo.sp_projectIssueAndFeedBack", para);
            return ds;
        }
    }
}