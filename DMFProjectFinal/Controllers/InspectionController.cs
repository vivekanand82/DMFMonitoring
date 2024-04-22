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
    public class InspectionController : Controller
    {
        // GET: Inspection
     
        private dfm_dbEntities db = new dfm_dbEntities();
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ChecklistInspection(int? DistID)
        {
            if (UserManager.GetUserLoginInfo(User.Identity.Name).RoleID == 2)
            {
                DistID = UserManager.GetUserLoginInfo(User.Identity.Name).DistID;
            }

            InspectionCheckListQuestionMaster abc = new InspectionCheckListQuestionMaster();
         
            return View();
        }


        public JsonResult Addanswer(string lis, string InspectionQuestion)
        {
            int res = 0;
            JsonResponse JR = new JsonResponse();
            JavaScriptSerializer js = new JavaScriptSerializer();
            List<DTO_InspectionCheckListAnswerMaster> list = js.Deserialize<List<DTO_InspectionCheckListAnswerMaster>>(lis);

            try
            {
                foreach (var item in list)
                {
                    InspectionCheckListAnswerMaster obj = new InspectionCheckListAnswerMaster();
                    obj.InspectionAnswer = item.InspectionAnswer;
                    obj.InspectionQuestion =InspectionQuestion;
                    db.InspectionCheckListAnswerMasters.Add(obj);
                    obj.CreatedBy = "1";
                    obj.CreatedDate = DateTime.Now;

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

                JR.Message = "Some Error Occured, Contact to Admin";
            }

            return Json(JR, JsonRequestBehavior.AllowGet);

        }

        public JsonResult Addqustion(string lis)
        {
            int res = 0;
            JsonResponse JR = new JsonResponse();
            JavaScriptSerializer js = new JavaScriptSerializer();
            List<DTO_InspectionCheckListQuestionMaster> list = js.Deserialize<List<DTO_InspectionCheckListQuestionMaster>>(lis);

            try
            {
                foreach (var item in list)
                {
                    InspectionCheckListQuestionMaster obj = new InspectionCheckListQuestionMaster();
                    obj.InspectionQuestion = item.InspectionQuestion;

                    if(item.IsChecked=="1")
                    {
                        obj.IsChecked = true;

                    }
                    else
                    {

                        obj.IsChecked = false;
                    }
                    obj.CreatedBy = "1";
                    obj.CreatedDate = DateTime.Now;


                    db.InspectionCheckListQuestionMasters.Add(obj);
                    res = db.SaveChanges();
                }
                //res = db.SaveChanges();

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

                JR.Message = "Some Error Occured, Contact to Admin";
            }

            return Json(JR, JsonRequestBehavior.AllowGet);

        }





    }
}