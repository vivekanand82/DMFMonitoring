using DMFProjectFinal.Models;
using DMFProjectFinal.Models.DTO;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace DMFProjectFinal.Controllers
{
    [SessionFilter]
    public class MileStoneController : Controller
    {
        // GET: MileStone
        private dfm_dbEntities db = new dfm_dbEntities();
        public ActionResult Index()
        {
            return View();
        }


        public ActionResult CreateProjectMilestone(int? DistID, int? id)
        {
            if (UserManager.GetUserLoginInfo(User.Identity.Name).RoleID == 2)
            {
                DistID = UserManager.GetUserLoginInfo(User.Identity.Name).DistID;
            }


            ViewBag.id = id.ToString();


            DTO_MileStoneMaster model = new DTO_MileStoneMaster();
            //ViewBag.ProjectPreparationID= new SelectList(( from ppp in db.ProjectProposalPreprations join fm in db.FundReleases on ppp.ProjectPreparationID equals fm.ProjectPreparationID where ppp.DistID == (DistID == null ? ppp.DistID : DistID) ), "DistrictId", "DistrictName", null);

            ViewBag.ProjectPreparationID = new SelectList((from ppp in db.ProjectProposalPreprations


                                                               //join pm in db.ProposalStatusMasters on ppp.ProjectPreparationID equals pm.ProjectID
                                                           where ppp.IsActive == true && ppp.Stageid == 2
                                                           && ppp.DistID == (DistID == null ? ppp.DistID : DistID)

                                                           select new DTO_MileStoneMaster
                                                           {
                                                               ProjectPreparationID = ppp.ProjectPreparationID,

                                                               ProjectNo = ppp.ProjectNo + " / " + ppp.ProjectName


                                                           }).Distinct().ToList(), "ProjectPreparationID", "ProjectNo", null);





            ViewBag.DistrictID = new SelectList(db.DistrictMasters.Where(x => x.IsActive == true && x.DistrictId == (DistID == null ? x.DistrictId : DistID)), "DistrictId", "DistrictName", null);

            ViewBag.InstallmentID = new SelectList((from inst in db.InstallmentMasters
                                                    select new DTO_MileStoneMaster
                                                    {
                                                        InstallmentID = inst.InstallmentID,
                                                        InstallmentName = inst.InstallmentName

                                                    }).Distinct().ToList(), "InstallmentID", "InstallmentName", null);


            MileStoneMaster abc = new MileStoneMaster();





            return View(model);
        }

        [HttpPost]
        public JsonResult Bindamount(int DistID, int InstallmentID)
        {
            var list = (from ppp in db.FundReleases
                        where ppp.DistrictID == DistID && ppp.InstallmentID == InstallmentID

                        select new DTO_MileStoneMaster
                        {

                            Releaseperamount = ppp.ReleaseAmount


                        }).ToList();


            return Json(list, JsonRequestBehavior.AllowGet);


        }



        public JsonResult insertMilestone(DTO_MileStoneMaster model)
        {
            JsonResponse JR = new JsonResponse();

            int res = 0;
            ProjectProposalPrepration abc = db.ProjectProposalPreprations.Where(x => x.ProjectPreparationID == model.ProjectPreparationID).FirstOrDefault();
            model.ProjectNo = abc.ProjectNo;

            //FundRelease fund = db.FundReleases.Where(x => x.ProjectPreparationID == model.ProjectPreparationID).FirstOrDefault();
            MileStoneMaster ak = db.MileStoneMasters.Where(x => x.ProjectPreparationID == model.ProjectPreparationID && x.InstallmentID == model.InstallmentID).FirstOrDefault();

            if (ak != null)
            {
                res = 0;

            }
            else
            {
                MileStoneMaster ax = new MileStoneMaster();
                ax.ProjectPreparationID = model.ProjectPreparationID;
                ax.ProjectNo = model.ProjectNo;
                ax.DistrictID = model.DistrictID;
                ax.InstallmentID = model.InstallmentID;
                ax.InsPercentage = model.InsPercentage;
                ax.Releaseperamount = model.Releaseperamount;
                ax.CreatedBy = "1";
                ax.CreatedDate = DateTime.Now;
                ax.Instext = model.Instext;
                ax.FundReleaseID = 1;
                ax.IsActive = true;
                ax.MileStoneStatus = 0;
                ax.SectorTypeId = abc.SectorTypeId;
                ax.SectorID = abc.SectorID;
                db.MileStoneMasters.Add(ax);
                res = db.SaveChanges();
            }


            //db.SaveChanges();

            if (res > 0)
            {


                JR.IsSuccess = true;
                JR.Message = "1";
                //JR.RedURL = "/ProjectWorkApproval/ProjectProposalPrepration";
            }
            else if (res == 0)
            {
                JR.IsSuccess = true;
                JR.Message = "0";

            }

            else
            {

                JR.Message = "Some Error Occured, Contact to Admin";
            }


            return Json(JR, JsonRequestBehavior.AllowGet);

        }


        public JsonResult InsertRecord(string lis, int DistrictID, int ProjectPreparationID)
        {

            JsonResponse JR = new JsonResponse();
            int res = 0;
            JavaScriptSerializer js = new JavaScriptSerializer();
            DTO_MileStoneMaster model = new DTO_MileStoneMaster();
            List<DTO_MileStoneMaster> list = js.Deserialize<List<DTO_MileStoneMaster>>(lis);
            try
            {
                foreach (var item in list)
                {
                    MileStoneMaster ax = new MileStoneMaster();
                    ax.ProjectPreparationID = Convert.ToInt32(ProjectPreparationID);
                    ProjectProposalPrepration abc = db.ProjectProposalPreprations.Where(x => x.ProjectPreparationID == ProjectPreparationID).FirstOrDefault();
                    model.ProjectNo = abc.ProjectNo;

                    ax.ProjectNo = model.ProjectNo;
                    ax.DistrictID = DistrictID;
                    ax.InstallmentID = item.InstallmentID;
                    ax.InsPercentage = item.InsPercentage;
                    ax.Releaseperamount = 0;
                    ax.CreatedBy = "1";
                    ax.CreatedDate = DateTime.Now;
                    ax.Instext = item.Instext;
                    ax.FundReleaseID = 1;
                    ax.IsActive = true;
                    ax.MileStoneStatus = 0;
                    ax.SectorTypeId = abc.SectorTypeId;
                    ax.SectorID = abc.SectorID;
                    db.MileStoneMasters.Add(ax);
                }

                res = db.SaveChanges();
                if (res > 0)
                {
                    JR.IsSuccess = true;
                    JR.Message = "1";
                    //JR.RedURL = "/ProjectWorkApproval/ProjectProposalPrepration";
                }
                else if (res == 0)
                {
                    JR.IsSuccess = true;
                    JR.Message = "0";
                }
                else
                {
                    JR.Message = "Some Error Occured, Contact to Admin";
                }
            }
            catch (Exception ex)
            {

            }
            return Json(JR, JsonRequestBehavior.AllowGet);
        }

        public JsonResult UpdateInsertRecord(string lis, int? DistrictID, int? ProjectPreparationID)
        {

            JsonResponse JR = new JsonResponse();
            int res = 0;
            JavaScriptSerializer js = new JavaScriptSerializer();
            DTO_MileStoneMaster model = new DTO_MileStoneMaster();
            List<DTO_MileStoneMaster> list = js.Deserialize<List<DTO_MileStoneMaster>>(lis);
            try
            {
                foreach (var item in list)
                {
                    MileStoneMaster ax = db.MileStoneMasters.Where(x => x.ProjectPreparationID == ProjectPreparationID && x.InstallmentID == item.InstallmentID).FirstOrDefault();
                    ax.ProjectPreparationID = Convert.ToInt32(ProjectPreparationID);
                    ProjectProposalPrepration abc = db.ProjectProposalPreprations.Where(x => x.ProjectPreparationID == ProjectPreparationID).FirstOrDefault();
                    model.ProjectNo = abc.ProjectNo;

                    ax.ProjectNo = model.ProjectNo;
                    ax.DistrictID = DistrictID;
                    //ax.InstallmentID = item.InstallmentID;
                    ax.InsPercentage = item.InsPercentage;
                    ax.Releaseperamount = 0;
                    ax.CreatedBy = "1";
                    ax.CreatedDate = DateTime.Now;
                    ax.Instext = item.Instext;
                    ax.FundReleaseID = 1;
                    ax.IsActive = true;
                    ax.SectorID = abc.SectorID;
                    ax.SectorTypeId = abc.SectorTypeId;
                    //db.MileStoneMasters.Add(ax);
                    db.Entry(ax).State = System.Data.Entity.EntityState.Modified;
                    res = db.SaveChanges();
                }
                if (res > 0)
                {
                    JR.IsSuccess = true;
                    JR.Message = "1";
                    //JR.RedURL = "/ProjectWorkApproval/ProjectProposalPrepration";
                }
                else if (res == 0)
                {
                    JR.IsSuccess = true;
                    JR.Message = "0";

                }
                else
                {
                    JR.Message = "Some Error Occured, Contact to Admin";
                }
            }
            catch (Exception ex)
            {

            }
            return Json(JR, JsonRequestBehavior.AllowGet);
        }





        public JsonResult UpdatedMilestone(DTO_MileStoneMaster model)
        {
            JsonResponse JR = new JsonResponse();
            MileStoneMaster abc = db.MileStoneMasters.Where(x => x.MileStoneID == model.MileStoneID).FirstOrDefault();

            abc.DistrictID = model.DistrictID;


            ProjectProposalPrepration abc1 = db.ProjectProposalPreprations.Where(x => x.ProjectPreparationID == model.ProjectPreparationID).FirstOrDefault();
            model.ProjectNo = abc1.ProjectNo;
            abc.ProjectPreparationID = model.ProjectPreparationID;
            abc.InsPercentage = model.InsPercentage;
            //abc.InsPercentage = model.InsPercentage;
            abc.Releaseperamount = model.Releaseperamount;
            abc.CreatedBy = "1";
            abc.CreatedDate = DateTime.Now;
            abc.Instext = model.Instext;
            //abc.FundReleaseID = 1;
            abc.InstallmentID = model.InstallmentID;
            abc.IsActive = true;

            int res = db.SaveChanges();
            if (res > 0)
            {


                JR.IsSuccess = true;
                JR.Message = "1";
                //JR.RedURL = "/ProjectWorkApproval/ProjectProposalPrepration";
            }
            else
            {

                JR.Message = "Some Error Occured, Contact to Admin";
            }

            return Json(JR, JsonRequestBehavior.AllowGet);

        }





        public ActionResult ViewMileStoneProject(int? DistID)
        {
            if (UserManager.GetUserLoginInfo(User.Identity.Name).RoleID == 2)
            {
                DistID = UserManager.GetUserLoginInfo(User.Identity.Name).DistID;
            }

            var lsit = (from ppp in db.ProjectProposalPreprations
                        join mt in

                   db.MileStoneMasters on ppp.ProjectPreparationID
                         equals mt.ProjectPreparationID
                        join dm in db.DistrictMasters
                          on ppp.DistID equals dm.DistrictId
                        join inst in db.InstallmentMasters
                        on mt.InstallmentID equals inst.InstallmentID
                        where ppp.IsActive == true
                           && ppp.DistID == (DistID == null ? ppp.DistID : DistID)
                        select new DTO_MileStoneMaster
                        {

                            ProjectNo = ppp.ProjectNo + " / " + ppp.ProjectName,
                            InstallmentName = inst.InstallmentName,
                            Releaseperamount = mt.Releaseperamount,

                            Instext = mt.Instext,
                            InsPercentage = mt.InsPercentage,
                            MileStoneID = mt.MileStoneID,
                            Districtname = dm.DistrictName



                        }

                      ).Distinct().ToList();


            ViewBag.LstData = lsit;


            DTO_MileStoneMaster model = new DTO_MileStoneMaster();

            ViewBag.ProjectPreparationID = new SelectList((from ppp in db.ProjectProposalPreprations


                                                               //join pm in db.ProposalStatusMasters on ppp.ProjectPreparationID equals pm.ProjectID
                                                           where ppp.IsActive == true && ppp.Stageid == 2
                                                           && ppp.DistID == (DistID == null ? ppp.DistID : DistID)

                                                           select new DTO_MileStoneMaster
                                                           {
                                                               ProjectPreparationID = ppp.ProjectPreparationID,

                                                               ProjectNo = ppp.ProjectNo + " / " + ppp.ProjectName


                                                           }).Distinct().ToList(), "ProjectPreparationID", "ProjectNo", null);



            ViewBag.SectorTypeId = new SelectList(db.SectorTypeMasters.Where(x => x.IsActive == true), "SectorTypeId", "SectorType", null).ToList();
            ViewBag.SectorID = new SelectList(db.SectorNameMasters.Where(x => x.IsActive == true), "SectorNameId", "SectorName", null).ToList();




            return View(model);
        }


        [HttpPost]

        public ActionResult ViewMileStoneProject(int? DistID, int? ProjectPreparationID)
        {
            if (UserManager.GetUserLoginInfo(User.Identity.Name).RoleID == 2)
            {
                DistID = UserManager.GetUserLoginInfo(User.Identity.Name).DistID;
            }


            var lsit = (from ppp in db.ProjectProposalPreprations
                        join mt in

                   db.MileStoneMasters on ppp.ProjectPreparationID
                         equals mt.ProjectPreparationID
                        join dm in db.DistrictMasters
                          on ppp.DistID equals dm.DistrictId
                        join inst in db.InstallmentMasters
                        on mt.InstallmentID equals inst.InstallmentID
                        where ppp.IsActive == true && mt.ProjectPreparationID == (ProjectPreparationID == null ? mt.ProjectPreparationID : ProjectPreparationID)

                         && ppp.DistID == (DistID == null ? ppp.DistID : DistID)

                        select new DTO_MileStoneMaster
                        {

                            ProjectNo = ppp.ProjectNo + " / " + ppp.ProjectName,
                            InstallmentName = inst.InstallmentName,
                            Releaseperamount = mt.Releaseperamount,

                            Instext = mt.Instext,
                            InsPercentage = mt.InsPercentage,
                            MileStoneID = mt.MileStoneID,
                            Districtname = dm.DistrictName



                        }

                      ).Distinct().ToList();


            ViewBag.LstData = lsit;


            DTO_MileStoneMaster model = new DTO_MileStoneMaster();

            ViewBag.ProjectPreparationID = new SelectList((from ppp in db.ProjectProposalPreprations


                                                               //join pm in db.ProposalStatusMasters on ppp.ProjectPreparationID equals pm.ProjectID
                                                           where ppp.IsActive == true && ppp.Stageid == 2
                                                           && ppp.DistID == (DistID == null ? ppp.DistID : DistID)

                                                           select new DTO_MileStoneMaster
                                                           {
                                                               ProjectPreparationID = ppp.ProjectPreparationID,

                                                               ProjectNo = ppp.ProjectNo + " / " + ppp.ProjectName


                                                           }).Distinct().ToList(), "ProjectPreparationID", "ProjectNo", null);


            ViewBag.SectorTypeId = new SelectList(db.SectorTypeMasters.Where(x => x.IsActive == true), "SectorTypeId", "SectorType", null).ToList();
            ViewBag.SectorID = new SelectList(db.SectorNameMasters.Where(x => x.IsActive == true), "SectorNameId", "SectorName", null).ToList();





            return View(model);
        }

        public JsonResult EdidData(int MileStoneID)
        {
            MileStoneMaster abc = db.MileStoneMasters.Where(x => x.MileStoneID == MileStoneID).FirstOrDefault();


            DTO_MileStoneMaster model = new DTO_MileStoneMaster();
            model.MileStoneID = Convert.ToInt32(abc.MileStoneID);
            model.ProjectPreparationID = Convert.ToInt32(abc.ProjectPreparationID);
            model.InstallmentID = Convert.ToInt32(abc.InstallmentID);
            model.Instext = abc.Instext;
            model.InsPercentage = abc.InsPercentage;

            model.DistrictID = abc.DistrictID;
            return Json(model, JsonRequestBehavior.AllowGet);


        }


        public JsonResult ViewProject(int ProjectPreparationID)
        {
            DTO_ProjectProposalPrepration model = new DTO_ProjectProposalPrepration();
            //ProjectProposalPrepration abc = db.ProjectProposalPreprations.Where(x => x.ProjectPreparationID == ProjectPreparationID).FirstOrDefault();
            //model.ProjectName = abc.ProjectName;
            //model.

            var data = (from ppp in db.ProjectProposalPreprations
                        join mt in db.SectorTypeMasters on ppp.SectorTypeId equals mt.SectorTypeID
                        join se in db.SectorNameMasters on ppp.SectorID equals se.SectorNameId
                        where ppp.ProjectPreparationID == ProjectPreparationID
                        select new DTO_ProjectProposalPrepration
                        {
                            ProjectNo = ppp.ProjectNo + " / " + ppp.ProjectName,
                            SectorType = mt.SectorType,
                            SectorName = se.SectorName,
                            SanctionedProjectCost = ppp.SanctionedProjectCost


                        }

                      ).Distinct().ToList();


            return Json(data, JsonRequestBehavior.AllowGet);



        }





        [HttpGet]
        public ActionResult DeleteProject(int MileStoneID)
        {
            MileStoneMaster abc = db.MileStoneMasters.Where(x => x.MileStoneID == MileStoneID).FirstOrDefault();
            db.MileStoneMasters.Remove(abc);
            db.SaveChanges();
            Response.Redirect("/MileStone/ViewMileStoneProject");

            return View();
        }


        public ActionResult Viewprojectapprovel(int? DistID, int? AgencyID, long? ProjectID, int? SectorID, int? SectorTypeId)
        {
            if (UserManager.GetUserLoginInfo(User.Identity.Name).RoleID == 2)
            {
                DistID = UserManager.GetUserLoginInfo(User.Identity.Name).DistID;
            }

            DTO_ProjectProposalPrepration model = new DTO_ProjectProposalPrepration();
            var LstData = (from ppp in db.ProjectProposalPreprations
                           join dm in db.DistrictMasters on ppp.DistID equals dm.DistrictId
                           join tm in db.TehsilMasters on ppp.TehsilId equals tm.TehsilId
                           join bm in db.BlockMasters on ppp.BlockId equals bm.BlockId
                           join vm in db.VillageMasters on ppp.VillageId equals vm.VillageId
                           join snm in db.SectorNameMasters on ppp.SectorID equals snm.SectorNameId
                           join stm in db.SectorTypeMasters on ppp.SectorTypeId equals stm.SectorTypeID
                           join ag in db.AgenciesInfoes on ppp.AgencyID equals ag.AgencyID
                           // join pm in db.ProjectMasters on ppp.ProjectID equals pm.ProjectID
                           where ppp.IsActive == true && ppp.Stageid == 2
                           && ppp.DistID == (DistID == null ? ppp.DistID : DistID)
                           && ppp.AgencyID == (AgencyID == null ? ppp.AgencyID : AgencyID)
                           //&& ppp.ProjectID == (ProjectID == null ? ppp.ProjectID : ProjectID)
                            && ppp.SectorTypeId == (SectorTypeId == null ? ppp.SectorTypeId : SectorTypeId)
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
                               WorkOrderNo = ppp.WorkOrderNo,
                               Status = ppp.RunningStatus,
                               ProjectNo = ppp.ProjectNo,
                               SectorType = stm.SectorType



                           }).ToList();
            ViewBag.LstData = LstData;

            ViewBag.CommitteeID = new SelectList(db.CommitteeMasters.Where(x => x.IsActive == true && x.CommitteeTypeID == 1 && x.DistID == (DistID == null ? x.DistID : DistID)), "CommitteeID", "CommitteeName", null);

            ViewBag.ProjectPreparationID = new SelectList((from ppp in db.ProjectProposalPreprations


                                                               //join pm in db.ProposalStatusMasters on ppp.ProjectPreparationID equals pm.ProjectID
                                                           where ppp.IsActive == true && ppp.Stageid == 2
                                                           && ppp.DistID == (DistID == null ? ppp.DistID : DistID)

                                                           select new DTO_MileStoneMaster
                                                           {
                                                               ProjectPreparationID = ppp.ProjectPreparationID,

                                                               ProjectNo = ppp.ProjectNo + " / " + ppp.ProjectName


                                                           }).Distinct().ToList(), "ProjectPreparationID", "ProjectNo", null);





            ViewBag.DistID = new SelectList(db.DistrictMasters.Where(x => x.IsActive == true && x.DistrictId == (DistID == null ? x.DistrictId : DistID)), "DistrictId", "DistrictName", null);

            ViewBag.InstallmentID = new SelectList((from inst in db.InstallmentMasters
                                                    select new DTO_MileStoneMaster
                                                    {
                                                        InstallmentID = inst.InstallmentID,
                                                        InstallmentName = inst.InstallmentName

                                                    }).Distinct().ToList(), "InstallmentID", "InstallmentName", null);


            ViewBag.SectorTypeId = new SelectList(db.SectorTypeMasters.Where(x => x.IsActive == true), "SectorTypeId", "SectorType", null).ToList();
            ViewBag.SectorID = new SelectList(db.SectorNameMasters.Where(x => x.IsActive == true), "SectorNameId", "SectorName", null).ToList();



            return View(model);
        }


        public JsonResult BindProjectName(int ProjectPreparationID)
        {

            var list = (from ppp in db.ProjectProposalPreprations
                        where ppp.IsActive == true && ppp.Stageid == 2
                        && ppp.ProjectPreparationID == ProjectPreparationID

                        select new DTO_ProjectProposalPrepration
                        {
                            ProjectPreparationIDother = ppp.ProjectPreparationID,

                            ProjectNo = ppp.ProjectNo + " / " + ppp.ProjectName

                        }).Distinct().ToList();



            return Json(list, JsonRequestBehavior.AllowGet);

        }


        public JsonResult ViewMilestone(int ProjectPreparationID)
        {
            var lsit = (from ppp in db.ProjectProposalPreprations
                        join mt in

                   db.MileStoneMasters on ppp.ProjectPreparationID
                         equals mt.ProjectPreparationID
                        join dm in db.DistrictMasters
                          on ppp.DistID equals dm.DistrictId
                        join inst in db.InstallmentMasters
                        on mt.InstallmentID equals inst.InstallmentID
                        where ppp.IsActive == true && mt.ProjectPreparationID == (ProjectPreparationID == null ? mt.ProjectPreparationID : ProjectPreparationID)



                        select new DTO_MileStoneMaster
                        {

                            ProjectNo = ppp.ProjectNo + " / " + ppp.ProjectName,
                            InstallmentName = inst.InstallmentName,
                            Releaseperamount = mt.Releaseperamount,

                            Instext = mt.Instext,
                            InsPercentage = mt.InsPercentage,
                            MileStoneID = mt.MileStoneID,
                            Districtname = dm.DistrictName,
                            InstallmentID = inst.InstallmentID



                        }

                      ).Distinct().ToList();


            return Json(lsit, JsonRequestBehavior.AllowGet);
        }

        public JsonResult BindInstallentData()
        {
            var list = (from inst in db.InstallmentMasters
                        select new DTO_MileStoneMaster
                        {
                            InstallmentID = inst.InstallmentID,
                            InstallmentName = inst.InstallmentName

                        }).Distinct().ToList();

            return Json(list, JsonRequestBehavior.AllowGet);


        }








    }
}