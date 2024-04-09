using DMFProjectFinal.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DMFProjectFinal.Models.DTO;

namespace DMFProjectFinal.Controllers
{
    [SessionFilter]
    public class UserManagementController : Controller
    {
        // GET: UserManagement
        private dfm_dbEntities db = new dfm_dbEntities();
        #region UserRegistration
        public ActionResult UserRegistration()
        {
            var data = (from ur in db.UserLogins
                        join dm in db.DistrictMasters on ur.DistID equals dm.DistrictId
                        join rol in db.UserRoles on ur.RoleID equals rol.RoleID
                        where ur.IsActive == true
                        select new DTO_UserRegistration
                        {
                            LoginID = ur.LoginID.ToString(),
                            Name = ur.Name,
                            DistrictName = dm.DistrictName,
                            PhoneNumber = ur.PhoneNumber,
                            EmailId = ur.EmailId,
                            RoleName=rol.RoleName
                        }).ToList();
            ViewBag.LstData = data;
            return View();
        }
        [HttpGet]
        public ActionResult CreateUserRegistration()
        {
            DTO_UserRegistration model = new DTO_UserRegistration();
            ViewBag.RoleID= new SelectList(db.UserRoles, "RoleID", "RoleName");
            ViewBag.DistrictId = new SelectList(db.DistrictMasters.Where(x=>x.IsActive==true),"DistrictId","DistrictName");
            return View(model);
        }
        [HttpPost]
        public JsonResult CreateUserRegistration(DTO_UserRegistration model)
        {
            JsonResponse JR = new JsonResponse();
            if (!ModelState.IsValid)
            {
                JR.Data = ModelState.Select(x => x.Value.Errors).Where(y => y.Count > 0).ToList();
                return Json(JR, JsonRequestBehavior.AllowGet);
            }
            if (db.UserLogins.Where(x => x.UserName == model.UserName || x.EmailId == model.EmailId || x.PhoneNumber == model.PhoneNumber).Any())
            {
                JR.Message = "Email/Phone Number " + model.EmailId + "/" + model.PhoneNumber + " Already exists !";
                return Json(JR, JsonRequestBehavior.AllowGet);
            }
            db.UserLogins.Add(new Models.UserLogin
            {
               CreatedOn=DateTime.Now,
               DistID=model.DistrictId,
               EmailId=model.EmailId,
               PhoneNumber=model.PhoneNumber,
               Name=model.Name,
               UserName=model.EmailId,
               Password= CryptoEngine.Encrypt(model.Password),
               RoleID=model.RoleID,
               IsActive=true
            });

            int res = db.SaveChanges();
            if (res > 0)
            {
                JR.IsSuccess = true;
                JR.Message = "Data Saved Successfully";
                JR.RedURL = "/UserManagement/UserRegistration";
            }
            else
            {
                JR.Message = "Some Error Occured, Contact to Admin";
            }
            return Json(JR, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public ActionResult EditUserRegistration(string id)
        {
            if (String.IsNullOrEmpty(id))
            {
                return RedirectToAction("Login", "Account");
            }
            try
            {
                int _id = int.Parse(CryptoEngine.Decrypt(id));
                var Info = db.UserLogins.Where(x => x.LoginID == _id).FirstOrDefault();
                if (Info != null)
                {
                    ViewBag.DistrictId = new SelectList(db.DistrictMasters.Where(x => x.IsActive == true), "DistrictId", "DistrictName",Info.DistID);
                    ViewBag.RoleID = new SelectList(db.UserRoles, "RoleID", "RoleName",Info.RoleID);
                    return View("~/Views/UserManagement/CreateUserRegistration.cshtml", new DTO_UserRegistration
                    { LoginID = CryptoEngine.Encrypt(Info.LoginID.ToString()), Name = Info.Name,EmailId=Info.EmailId,UserName=Info.UserName,
                        DistrictId=Info.DistID,Password= CryptoEngine.Decrypt(Info.Password),
                        PhoneNumber=Info.PhoneNumber,RoleID=Info.RoleID });
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
        public JsonResult EditUserRegistration(DTO_UserRegistration model)
        {
            JsonResponse JR = new JsonResponse();
            if (!ModelState.IsValid)
            {
                JR.Data = ModelState.Select(x => x.Value.Errors).Where(y => y.Count > 0).ToList();
                return Json(JR, JsonRequestBehavior.AllowGet);
            }
            if (!String.IsNullOrEmpty(model.LoginID))
            {
                int _id = int.Parse(CryptoEngine.Decrypt(model.LoginID));
                if (db.UserLogins.Where(x => (x.UserName == model.UserName || x.EmailId == model.EmailId || x.PhoneNumber == model.PhoneNumber) && x.LoginID!=_id).Any())
                {
                    JR.Message = "Email/Phone Number " + model.EmailId + "/" + model.PhoneNumber + " Already exists !";
                    return Json(JR, JsonRequestBehavior.AllowGet);
                }
                var Info = db.UserLogins.Where(x => x.LoginID == _id).FirstOrDefault();
                if (Info != null)
                {
                    Info.UserName = model.UserName;
                    Info.Name = model.Name;
                    Info.Password = CryptoEngine.Encrypt(model.Password);
                    Info.PhoneNumber = model.PhoneNumber;
                    Info.EmailId = model.EmailId;
                    Info.DistID = model.DistrictId;
                    Info.RoleID = model.RoleID;
                }
            }
            int res = db.SaveChanges();
            if (res > 0)
            {
                JR.IsSuccess = true;
                JR.Message = "Data Updated Successfully";
                JR.RedURL = "/UserManagement/UserRegistration";
            }
            else
            {
                JR.Message = "Some Error Occured, Contact to Admin";
            }
            return Json(JR, JsonRequestBehavior.AllowGet);
        }
        public JsonResult DeleteUserRegistration(string id)
        {
            JsonResponse JR = new JsonResponse();
            if (!String.IsNullOrEmpty(id))
            {
                int _id = int.Parse(CryptoEngine.Decrypt(id));
                var Info = db.UserLogins.Where(x => x.LoginID == _id).FirstOrDefault();
                if (Info != null)
                {
                    db.UserLogins.Remove(Info);
                }
            }
            int res = db.SaveChanges();
            if (res > 0)
            {
                JR.IsSuccess = true;
                JR.Message = "Data Deleted Successfully";
                JR.RedURL = "/UserManagement/UserRegistration";
            }
            else
            {
                JR.Message = "Some Error Occured, Contact to Admin";
            }
            return Json(JR, JsonRequestBehavior.AllowGet);
        }
        #endregion
        #region Ramdhyan
        [HttpPost]
        public JsonResult GetDatabyRole(int DistrictID,int RoleID)
        {
            
            if (RoleID == 3)
            {
                var lesse = (from u in db.LesseeMasters
                             where u.DistID == DistrictID
                             select new DTO_LesseeMaster
                             {
                                EmailID=u.EmailID,
                                LesseeID=u.LesseeID.ToString(),
                                LesseeName=u.LesseeName,
                                RoleID=RoleID,

                             }).ToList();

               //var lessee = db.LesseeMasters.Where(x => x.DistID == DistrictID).ToList();
                return Json(lesse, JsonRequestBehavior.AllowGet);
            }
            else if(RoleID == 6)
            {
                var data = (from u in db.AgenciesInfoes
                            where u.DistID == DistrictID
                             select new DTO_AgenciesInfo
                             {
                                 AgencyID = u.AgencyID.ToString(),
                                 OwnerName = u.OwnerName,
                                 EmailID=u.EmailID,
                                 RoleID = RoleID

                             }).ToList();
                //var  data = db.AgenciesInfoes.Where(x => x.DistID == DistrictID).ToList();
                return Json(data, JsonRequestBehavior.AllowGet);
            }
            return Json("None", JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult GetDatabyUserID(int DistrictID, int RoleID,int UserID)
        {

            if (RoleID == 3)
            {
                var lesse = (from u in db.LesseeMasters
                             where u.DistID == DistrictID && u.LesseeID==UserID
                             select new DTO_LesseeMaster
                             {
                                 MobNo=u.MobNo,
                                 EmailID = u.EmailID,
                                 LesseeID = u.LesseeID.ToString(),
                                 LesseeName = u.LesseeName,
                                 RoleID = RoleID,

                             }).ToList();

                //var lessee = db.LesseeMasters.Where(x => x.DistID == DistrictID).ToList();
                return Json(lesse, JsonRequestBehavior.AllowGet);
            }
            else if (RoleID == 6)
            {
                var data = (from u in db.AgenciesInfoes
                            where u.DistID == DistrictID && u.AgencyID == UserID
                            select new DTO_AgenciesInfo
                            {
                                MobileNo=u.MobileNo,
                                AgencyID = u.AgencyID.ToString(),
                                OwnerName = u.OwnerName,
                                EmailID = u.EmailID,
                                RoleID = RoleID

                            }).ToList();
                //var  data = db.AgenciesInfoes.Where(x => x.DistID == DistrictID).ToList();
                return Json(data, JsonRequestBehavior.AllowGet);
            }
            return Json("None", JsonRequestBehavior.AllowGet);
        }
        #endregion
    }

}