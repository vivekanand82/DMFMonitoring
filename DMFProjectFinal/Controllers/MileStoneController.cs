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


        public ActionResult CreateProjectMilestone(int? DistID)
        {
            if (UserManager.GetUserLoginInfo(User.Identity.Name).RoleID == 2)
            {
                DistID = UserManager.GetUserLoginInfo(User.Identity.Name).DistID;
            }

            DTO_MileStoneMaster model = new DTO_MileStoneMaster();
            //ViewBag.ProjectPreparationID= new SelectList(( from ppp in db.ProjectProposalPreprations join fm in db.FundReleases on ppp.ProjectPreparationID equals fm.ProjectPreparationID where ppp.DistID == (DistID == null ? ppp.DistID : DistID) ), "DistrictId", "DistrictName", null);

            ViewBag.ProjectPreparationID = new SelectList((from ppp in db.ProjectProposalPreprations
                                                           join dm in db.FundReleases on ppp.ProjectPreparationID equals dm.ProjectPreparationID

                                                           //join pm in db.ProposalStatusMasters on ppp.ProjectPreparationID equals pm.ProjectID
                                                           where ppp.IsActive == true
                                                           && ppp.DistID == (DistID == null ? ppp.DistID : DistID)

                                                           select new DTO_MileStoneMaster
                                                           {
                                                               ProjectPreparationID = ppp.ProjectPreparationID,

                                                               ProjectNo = ppp.ProjectNo + " / " + ppp.ProjectDescription


                                                           }).Distinct().ToList(), "ProjectPreparationID", "ProjectNo", null);

            ViewBag.DistrictID = new SelectList(db.DistrictMasters.Where(x => x.IsActive == true && x.DistrictId == (DistID == null ? x.DistrictId : DistID)), "DistrictId", "DistrictName", null);

            ViewBag.InstallmentID = new SelectList((from ppp in db.FundReleases
                                                    join inst in db.InstallmentMasters on ppp.InstallmentID equals inst.InstallmentID
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
            ProjectProposalPrepration abc = db.ProjectProposalPreprations.Where(x => x.ProjectPreparationID == model.ProjectPreparationID).FirstOrDefault();
            model.ProjectNo = abc.ProjectNo;

            FundRelease fund = db.FundReleases.Where(x => x.ProjectPreparationID == model.ProjectPreparationID).FirstOrDefault();

            
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
            ax.FundReleaseID = fund.FundReleaseID;
            db.MileStoneMasters.Add(ax);
            //db.SaveChanges();
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

                            ProjectNo = ppp.ProjectNo + " / " + ppp.ProjectDescription,
                            InstallmentName = inst.InstallmentName,
                            Releaseperamount = mt.Releaseperamount,

                            Instext = mt.Instext,
                            InsPercentage = mt.InsPercentage,
                            MileStoneID =mt.MileStoneID,
                            Districtname = dm.DistrictName



                        }

                      ).Distinct().ToList();


            ViewBag.LstData = lsit;

            return View();
        }






    }
}