using DMFProjectFinal.Models;
using DMFProjectFinal.Models.DTO;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web.Mvc;

namespace DMFProjectFinal.Controllers
{
    [SessionFilter]
    public class ProjectWorkApprovalController : Controller
    {
        private dfm_dbEntities db = new dfm_dbEntities();
        //public ActionResult ProjectProposalPrepration()
        //{
        //    return View();
        //}
        public static string coonectionstrings = ConfigurationManager.ConnectionStrings["constr"].ToString();
        #region ProjectProposalPrepration
        public ActionResult ProjectProposalPrepration(int? DistID, int? AgencyID, long? ProjectID, int? SectorID)
        {
            if (UserManager.GetUserLoginInfo(User.Identity.Name).RoleID == 2)
            {
                DistID = UserManager.GetUserLoginInfo(User.Identity.Name).DistID;
            }
            //var LstData = (from ppp in db.ProjectProposalPreprations
            //               join dm in db.DistrictMasters on ppp.DistID equals dm.DistrictId
            //               join ag in db.AgenciesInfoes on ppp.AgencyID equals ag.AgencyID
            //               join pm in db.ProjectMasters on ppp.ProjectID equals pm.ProjectID
            //               join psm in db.ProjectStatusMasters on ppp.ProjectStatusID equals psm.ProjectStatusID
            //               join sm in db.SectorNameMasters on ppp.SectorID equals sm.SectorNameId
            //               where ppp.IsActive == true
            //               && ppp.DistID == (DistID == null ? ppp.DistID : DistID)
            //               && ppp.AgencyID == (AgencyID == null ? ppp.AgencyID : AgencyID)
            //               && ppp.ProjectID == (ProjectID == null ? ppp.ProjectID : ProjectID)
            //               && ppp.SectorID == (SectorID == null ? ppp.SectorID : SectorID)
            //               select new DTO_ProjectProposalPrepration
            //               {
            //                   AgencyName = ag.Name,
            //                   DistrictName = dm.DistrictName,
            //                   GSTAndOthers = ppp.GSTAndOthers,
            //                   ProjectCost = ppp.ProjectCost,
            //                   ProjectName = pm.ProjectName,
            //                //   ProjectStatus = psm.ProjectStatus,
            //                   ProjectPreparationID = ppp.ProjectPreparationID.ToString(),
            //                   ProposalCopy = ppp.ProposalCopy,
            //                   ProposalDate = ppp.ProposalDate,
            //                   ProposedBy = ppp.ProposedBy,
            //                   ProsposalNo = ppp.ProsposalNo,
            //                   SectorName = sm.SectorName,
            //                   SanctionedProjectCost = ppp.SanctionedProjectCost,
            //                   TenderDate = ppp.TenderDate,
            //                   TenderNo = ppp.TenderNo,
            //                   WorkOrderDate = ppp.WorkOrderDate,
            //                   WorkOrderNo = ppp.WorkOrderNo
            //               }).ToList();
            var LstData = (from ppp in db.ProjectProposalPreprations
                           join dm in db.DistrictMasters on ppp.DistID equals dm.DistrictId
                           join tm in db.TehsilMasters on ppp.TehsilId equals tm.TehsilId
                           join bm in db.BlockMasters on ppp.BlockId equals bm.BlockId
                          //join vm in db.VillageMasters on ppp.VillageId equals vm.VillageId
                           join snm in db.SectorNameMasters on ppp.SectorID equals snm.SectorNameId
                           join stm in db.SectorTypeMasters on ppp.SectorTypeId equals stm.SectorTypeID
                           join ag in db.AgenciesInfoes on ppp.AgencyID equals ag.AgencyID
                          // join pm in db.ProjectMasters on ppp.ProjectID equals pm.ProjectID
                          // join psm in db.ProjectStatusMasters on ppp.ProjectStatusID equals psm.ProjectStatusID
                           //join sm in db.SectorNameMasters on ppp.SectorID equals sm.SectorNameId
                           where ppp.IsActive == true
                           && ppp.DistID == (DistID == null ? ppp.DistID : DistID)
                           && ppp.AgencyID == (AgencyID == null ? ppp.AgencyID : AgencyID)
                           //&& ppp.ProjectID == (ProjectID == null ? ppp.ProjectID : ProjectID)
                           && ppp.SectorID == (SectorID == null ? ppp.SectorID : SectorID)
                           select new DTO_ProjectProposalPrepration
                           {
                               AgencyName = ag.Name,
                               DistrictName = dm.DistrictName,
                               GSTAndOthers = ppp.GSTAndOthers,
                               ProjectCost = ppp.ProjectCost,
                               ProjectName = ppp.ProjectName,
                               //   ProjectStatus = psm.ProjectStatus,
                               ProjectPreparationID = ppp.ProjectPreparationID.ToString(),
                               ProposalCopy = ppp.ProposalCopy,
                               ProposalDate = ppp.ProposalDate,
                               ProposedBy = ppp.ProposedBy,
                               ProsposalNo = ppp.ProsposalNo,
                               SectorName = snm.SectorName,
                               SanctionedProjectCost = ppp.SanctionedProjectCost,
                               TenderDate = ppp.TenderDate,
                               TenderNo = ppp.TenderNo,
                               WorkOrderDate = ppp.WorkOrderDate,
                               WorkOrderNo = ppp.WorkOrderNo
                           }).ToList();
            ViewBag.LstData = LstData;
            return View();
        }
        public ActionResult CreateProjectProposalPrepration()
        {
            int? DistID = null;
            if (UserManager.GetUserLoginInfo(User.Identity.Name).RoleID == 2)
            {
                DistID = UserManager.GetUserLoginInfo(User.Identity.Name).DistID;
            }
            var Tehsil = db.TehsilMasters.Where(x => x.DistrictId == DistID).FirstOrDefault();
            DTO_ProjectProposalPrepration model = new DTO_ProjectProposalPrepration();
            ViewBag.DistID = new SelectList(db.DistrictMasters.Where(x => x.IsActive == true && x.DistrictId == (DistID == null ? x.DistrictId : DistID)), "DistrictId", "DistrictName", null);
            ViewBag.TehsilId = new SelectList(db.TehsilMasters.Where(x => x.IsActive == true && x.DistrictId == (DistID == null ? x.DistrictId : DistID)), "TehsilId", "TehsilName", null);
            ViewBag.BlockId = new SelectList(db.BlockMasters.Where(x => x.IsActive == true && x.DistrictId == (DistID == null ? x.DistrictId : DistID)), "BlockId", "BlockName", null);
            //ViewBag.VillageId = new SelectList(db.VillageMasters.Where(x => x.IsActive == true), "VillageId", "VillageNameInEnglish", null);
            ViewBag.AgencyID = new SelectList(db.AgenciesInfoes.Where(x => x.IsActive == true && x.DistID == (DistID == null ? x.DistID : DistID)).ToList().Select(x => new { ID = x.AgencyID, Text = x.Name + " / " + x.OwnerName }), "ID", "Text", null);
            ViewBag.ProjectID = new SelectList(db.ProjectMasters.Where(x => x.IsActive == true && x.DistID == (DistID == null ? x.DistID : DistID)).ToList().Select(x => new { ID = x.ProjectID, Text = x.ProjectName + " (" + x.ProjectCode + ")" }), "ID", "Text", null);
            //ViewBag.SectorID = new SelectList(db.SectorNameMasters.Where(x => x.IsActive == true), "SectorNameId", "SectorName", null);
            ViewBag.SectorTypeId = new SelectList(db.SectorTypeMasters.Where(x => x.IsActive == true), "SectorTypeId", "SectorType", null).ToList();
            ViewBag.SectorID = new SelectList(db.SectorNameMasters.Where(x => x.IsActive == true), "SectorNameId", "SectorName", null).ToList();
            ViewBag.ProjectStatusID = new SelectList(db.ProjectStatusMasters.Where(x => x.IsActive == true), "ProjectStatusID", "ProjectStatus", null);


            //var lstData = new SelectList(from pm in db.ProjectMasters
            //                             join dst in db.DistrictMasters on pm.DistID equals dst.DistrictId
            //                             join st in db.SectorTypeMasters on pm.SectorTypeId equals st.SectorTypeID
            //                             select new DTO_ProjectMaster
            //                             {
            //                                 SectorTypeId = st.SectorTypeID,

            //                                 SectorType = st.SectorType,


            //                             }


            //              ).ToList().Select(st => new { SectorTypeId = st.Selected });






            return View(model);
        }
        [HttpPost]
        public JsonResult CreateProjectProposalPrepration(DTO_ProjectProposalPrepration model)
        {
            JsonResponse JR = new JsonResponse();
            if (!ModelState.IsValid)
            {
                JR.Data = ModelState.Select(x => x.Value.Errors).Where(y => y.Count > 0).ToList();
                return Json(JR, JsonRequestBehavior.AllowGet);
            }
            if (!String.IsNullOrEmpty(model.ProposalCopy))
            {
                model.ProposalCopy = BusinessLogics.UploadFileDMF(model.ProposalCopy);
                if (model.ProposalCopy.Contains("Expp::"))
                {
                    JR.Message = model.ProposalCopy;
                    return Json(JR, JsonRequestBehavior.AllowGet);
                }
            }
           
            if (!String.IsNullOrEmpty(model.WorkOrderCopy)) //added by ramdhyan 03.04.2024
            {
                model.WorkOrderCopy = BusinessLogics.UploadFileDMF(model.WorkOrderCopy);
                if (model.WorkOrderCopy.Contains("Expp::"))
                {
                    JR.Message = model.WorkOrderCopy;
                    return Json(JR, JsonRequestBehavior.AllowGet);
                }
            }
            //model.ProposalCopy = BusinessLogics.UploadFileDMF(model.ProposalCopy);
            //if (model.ProposalCopy.Contains("Expp::"))
            //{
            //    JR.Message = model.ProposalCopy;
            //    return Json(JR, JsonRequestBehavior.AllowGet);
            //}

            db.ProjectProposalPreprations.Add(new Models.ProjectProposalPrepration
            {
                CreatedOn = DateTime.Now,
                IsActive = true,
                CreatedBy = UserManager.GetUserLoginInfo(User.Identity.Name).LoginID,
                DistID = model.DistID,
                TehsilId=model.TehsilId,
                BlockId=model.BlockId,
                VillageId =model.VillageId,
                SectorID = model.SectorID,
                SectorTypeId=model.SectorTypeId,
                ProjectName=model.ProjectName,
                WorkLatitude=model.WorkLatitude,
                WorkLongitude=model.WorkLongitude,
                ProjectDescription=model.ProjectDescription,
                ProsposalNo = model.ProsposalNo,
                ProposalDate = model.ProposalDate,
                ProposalCopy = model.ProposalCopy,
                ProposedBy = model.ProposedBy,
                ProjectCost = model.ProjectCost,
                GSTAndOthers = model.GSTAndOthers,
                TenderNo = model.TenderNo,
                TenderDate = model.TenderDate,
                WorkOrderNo = model.WorkOrderNo,
                WorkOrderDate = model.WorkOrderDate,
                WorkOrderCopy=model.WorkOrderCopy,
                AgencyID = model.AgencyID,
                SanctionedProjectCost = model.SanctionedProjectCost
                //ProjectID = model.ProjectID,
                //ProjectStatusID = model.ProjectStatusID,
                
               
            });

            int res = db.SaveChanges();
            if (res > 0)
            {
                //Response.Write("<script>alert('Data saved successfully');window.location.href='/ProjectWorkApproval/ProjectProposalPrepration'</script>");
                
                JR.IsSuccess = true;
                JR.Message = "Data Saved Successfully";
                JR.RedURL = "/ProjectWorkApproval/ProjectProposalPrepration";
            }
            else
            {
              //  Response.Write("<script>alert('Error');window.location.href='/ProjectWorkApproval/CreateProjectProposalPrepration'</script>");
                JR.Message = "Some Error Occured, Contact to Admin";
            }
            return Json(JR, JsonRequestBehavior.AllowGet);
        }
        public ActionResult EditProjectProposalPrepration(string id)
        {
            if (String.IsNullOrEmpty(id))
            {
                return RedirectToAction("Login", "Account");
            }
            try
            {
               long _id = long.Parse(CryptoEngine.Decrypt(id));
                //long _id = long.Parse(id);  use this code when u use server side datatable for display
                var Info = db.ProjectProposalPreprations.Where(x => x.ProjectPreparationID == _id).FirstOrDefault();
                if (Info != null)
                {
                    int? DistID = null;
                    if (UserManager.GetUserLoginInfo(User.Identity.Name).RoleID == 2)
                    {
                        DistID = UserManager.GetUserLoginInfo(User.Identity.Name).DistID;
                    }
                   // var Tehsil = db.TehsilMasters.Where(x => x.DistrictId == DistID).FirstOrDefault();
                    ViewBag.DistID = new SelectList(db.DistrictMasters.Where(x => x.IsActive == true && x.DistrictId == (DistID == null ? x.DistrictId : DistID)), "DistrictId", "DistrictName", null);
                    ViewBag.TehsilId = new SelectList(db.TehsilMasters.Where(x => x.IsActive == true && x.DistrictId == (DistID == null ? x.DistrictId : DistID)), "TehsilId", "TehsilName", null);
                    ViewBag.BlockId = new SelectList(db.BlockMasters.Where(x => x.IsActive == true && x.DistrictId == (DistID == null ? x.DistrictId : DistID)), "BlockId", "BlockName", null);
                    ViewBag.VillageId = new SelectList(db.VillageMasters.Where(x => x.IsActive == true && x.TehsilId == (Info.TehsilId == null ? x.TehsilId : Info.TehsilId)), "VillageId", "VillageNameInEnglish", null);
                    ViewBag.AgencyID = new SelectList(db.AgenciesInfoes.Where(x => x.IsActive == true && x.DistID == (DistID == null ? x.DistID : DistID)).ToList().Select(x => new { ID = x.AgencyID, Text = x.Name + " / " + x.OwnerName }), "ID", "Text", Info.AgencyID);
                    ViewBag.ProjectID = new SelectList(db.ProjectMasters.Where(x => x.IsActive == true && x.DistID == (DistID == null ? x.DistID : DistID)).ToList().Select(x => new { ID = x.ProjectID, Text = x.ProjectName + " (" + x.ProjectCode + ")" }), "ID", "Text", Info.ProjectID);
                    ViewBag.SectorTypeId = new SelectList(db.SectorTypeMasters.Where(x => x.IsActive == true && x.SectorTypeID == (Info.SectorTypeId == null ? x.SectorTypeID : Info.SectorTypeId)), "SectorTypeId", "SectorType", null).ToList();
                    ViewBag.SectorID = new SelectList(db.SectorNameMasters.Where(x => x.IsActive == true), "SectorNameId", "SectorName", Info.SectorID);
                   // ViewBag.ProjectStatusID = new SelectList(db.ProjectStatusMasters.Where(x => x.IsActive == true), "ProjectStatusID", "ProjectStatus", Info.ProjectStatusID);

                    return View("~/Views/ProjectWorkApproval/CreateProjectProposalPrepration.cshtml", new DTO_ProjectProposalPrepration
                    {
                        
                        CreatedOn = DateTime.Now,
                        IsActive = true,
                        CreatedBy = UserManager.GetUserLoginInfo(User.Identity.Name).LoginID,
                        DistID = Info.DistID,
                        TehsilId = Info.TehsilId,
                        BlockId = Info.BlockId,
                        VillageId = Info.VillageId,
                        SectorID = Info.SectorID,
                        SectorTypeId = Info.SectorTypeId,
                        ProjectName = Info.ProjectName,
                        WorkLatitude = Convert.ToDecimal(Info.WorkLatitude),
                        WorkLongitude = Convert.ToDecimal(Info.WorkLongitude),
                        ProjectDescription = Info.ProjectDescription,
                        ProsposalNo = Info.ProsposalNo,
                        ProposalDate = Info.ProposalDate,
                        ProposalCopy = Info.ProposalCopy,
                        ProposedBy = Info.ProposedBy,
                        ProjectCost = Info.ProjectCost,
                        GSTAndOthers = Info.GSTAndOthers,
                        TenderNo = Info.TenderNo,
                        TenderDate = Info.TenderDate,
                        WorkOrderNo = Info.WorkOrderNo,
                        WorkOrderDate = Info.WorkOrderDate,
                        WorkOrderCopy = Info.WorkOrderCopy,
                        AgencyID = Info.AgencyID,
                        SanctionedProjectCost = Info.SanctionedProjectCost,
                        ProjectPreparationID = CryptoEngine.Encrypt(Info.ProjectPreparationID.ToString())
                    });
                }
                else
                {
                    return RedirectToAction("Login", "Account");
                }
            }
            catch (Exception)
            {
                return RedirectToAction("Login", "Account");
            }
        }
        [HttpPost]
        public JsonResult EditProjectProposalPrepration(DTO_ProjectProposalPrepration model)
        {
            JsonResponse JR = new JsonResponse();
            if (!ModelState.IsValid)
            {
                JR.Data = ModelState.Select(x => x.Value.Errors).Where(y => y.Count > 0).ToList();
                return Json(JR, JsonRequestBehavior.AllowGet);
            }
            if (!String.IsNullOrEmpty(model.ProjectPreparationID))
            {
                long _id = long.Parse(CryptoEngine.Decrypt(model.ProjectPreparationID));
                if (!String.IsNullOrEmpty(model.ProposalCopy))
                {
                    if (model.ProposalCopy != "prev")
                    {
                        model.ProposalCopy = BusinessLogics.UploadFileDMF(model.ProposalCopy);
                        if (model.ProposalCopy.Contains("Expp::"))
                        {
                            JR.Message = model.ProposalCopy;
                            return Json(JR, JsonRequestBehavior.AllowGet);
                        }
                    }
                }
                if (!String.IsNullOrEmpty(model.WorkOrderCopy))//added by ramdhyan 03.04.2024
                {
                    if (model.WorkOrderCopy != "prev")
                    {
                        model.WorkOrderCopy = BusinessLogics.UploadFileDMF(model.WorkOrderCopy);
                        if (model.WorkOrderCopy.Contains("Expp::"))
                        {
                            JR.Message = model.WorkOrderCopy;
                            return Json(JR, JsonRequestBehavior.AllowGet);
                        }
                    }
                }
                var Info = db.ProjectProposalPreprations.Where(x => x.ProjectPreparationID == _id).FirstOrDefault();
                if (Info != null)
                {
                
                   
                    
                  
                    
                    //    AgencyID = Info.AgencyID,
                    //    SanctionedProjectCost = Info.SanctionedProjectCost,
                    Info.DistID = model.DistID;
                    Info.TehsilId = model.TehsilId;
                    Info.BlockId = model.BlockId;
                    Info.VillageId = model.VillageId;
                    Info.SectorID = model.SectorID;
                    Info.SectorTypeId = model.SectorTypeId;
                    Info.ProjectName = model.ProjectName;
                    Info.WorkLatitude = model.WorkLatitude;
                    Info.WorkLongitude = model.WorkLongitude;
                    Info.ProjectDescription = model.ProjectDescription;
                    Info.ProposalDate = model.ProposalDate;
                    Info.ProposalCopy = (model.ProposalCopy != null && model.ProposalCopy == "prev") ? Info.ProposalCopy : model.ProposalCopy;
                    Info.ProposedBy = model.ProposedBy;
                    Info.ProsposalNo = model.ProsposalNo;
                    Info.ProjectCost = model.ProjectCost;
                    Info.GSTAndOthers = model.GSTAndOthers;
                    Info.TenderDate = model.TenderDate;
                    Info.TenderNo = model.TenderNo;
                    Info.WorkOrderDate = model.WorkOrderDate;
                    Info.WorkOrderNo = model.WorkOrderNo;
                    //Info.ProjectID = model.ProjectID;
                    //Info.ProjectStatusID = model.ProjectStatusID;
                    Info.WorkOrderCopy = (model.WorkOrderCopy != null && model.WorkOrderCopy == "prev") ? Info.WorkOrderCopy : model.WorkOrderCopy;
                    Info.AgencyID = model.AgencyID;
                    Info.SanctionedProjectCost = model.SanctionedProjectCost;
                    Info.ModifyBy = UserManager.GetUserLoginInfo(User.Identity.Name).LoginID;
                    Info.ModifyDate = DateTime.Now;
                }
            }
            int res = db.SaveChanges();
            if (res > 0)
            {
                JR.IsSuccess = true;
                JR.Message = "Data Updated Successfully";
                JR.RedURL = "/ProjectWorkApproval/ProjectProposalPrepration";
            }
            else
            {
                JR.Message = "Some Error Occured, Contact to Admin";
            }
            return Json(JR, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult DeleteProjectProposalPrepration(string id)
        {
            JsonResponse JR = new JsonResponse();
            if (!String.IsNullOrEmpty(id))
            {
                long _id = long.Parse(CryptoEngine.Decrypt(id));
                //long _id = long.Parse(id);  use this code when u use server side datatable for display
                var Info = db.ProjectProposalPreprations.Where(x => x.ProjectPreparationID == _id).FirstOrDefault();
                if (Info != null)
                {
                    db.ProjectProposalPreprations.Remove(Info);
                }
            }
            int res = db.SaveChanges();
            if (res > 0)
            {
                JR.IsSuccess = true;
                JR.Message = "Data Deleted Successfully";
                JR.RedURL = "/ProjectWorkApproval/ProjectProposalPrepration";
            }
            else
            {
                JR.Message = "Some Error Occured, Contact to Admin";
            }
            return Json(JR, JsonRequestBehavior.AllowGet);
        }
        #endregion
        #region Ramdhyan 30.03.2024 for server side listing on the projectproposal page
        //public JsonResult GetProjectProposalPreprationList(int? DistID, int? AgencyID, long? ProjectID, int? SectorID)
        //{
        //    if (UserManager.GetUserLoginInfo(User.Identity.Name).RoleID == 2)
        //    {
        //        DistID = UserManager.GetUserLoginInfo(User.Identity.Name).DistID;
        //    }
        //    List<DTO_ProjectProposalPrepration> lst = new List<DTO_ProjectProposalPrepration>();
        //    using (SqlConnection con = new SqlConnection(coonectionstrings))
        //    using (SqlCommand cmd = new SqlCommand("sp_GetProjectProposalPreprationList", con))
        //    {
        //        cmd.CommandType = System.Data.CommandType.StoredProcedure;
        //        cmd.Parameters.AddWithValue("@DistID", DistID);
        //        cmd.Parameters.AddWithValue("@AgencyID", AgencyID);
        //        cmd.Parameters.AddWithValue("@ProjectID", ProjectID);
        //        cmd.Parameters.AddWithValue("@SectorID", SectorID);
        //        con.Open();
        //        SqlDataReader dr = cmd.ExecuteReader();
        //        while (dr.Read())
        //        {
        //            lst.Add(new DTO_ProjectProposalPrepration
        //            {
        //                AgencyName = dr["Name"].ToString(),
        //                DistrictName = dr["DistrictName"].ToString(),
        //                GSTAndOthers = Convert.ToDecimal(dr["GSTAndOthers"]),
        //                ProjectCost = Convert.ToDecimal(dr["ProjectCost"]),
        //                ProjectName = dr["ProjectName"].ToString(),
        //                ProjectStatus = dr["ProjectStatus"].ToString(),
        //                ProjectPreparationID = dr["ProjectPreparationID"].ToString(),
        //                ProposalCopy = dr["ProposalCopy"].ToString(),
        //                ProposalDate = Convert.ToDateTime(dr["ProposalDate"]),
        //                ProposedBy = dr["ProposedBy"].ToString(),
        //                ProsposalNo = dr["ProsposalNo"].ToString(),
        //                SectorName = dr["SectorName"].ToString(),
        //                SanctionedProjectCost = Convert.ToDecimal(dr["SanctionedProjectCost"]),
        //                TenderDate = Convert.ToDateTime(dr["TenderDate"]),
        //                TenderNo = dr["TenderNo"].ToString(),
        //                WorkOrderDate = Convert.ToDateTime(dr["WorkOrderDate"]),
        //                WorkOrderNo = dr["WorkOrderNo"].ToString()
        //            });
        //        }
        //        con.Close();
        //    }

        //    return Json(lst, JsonRequestBehavior.AllowGet);
        //}
        #endregion
    }
}