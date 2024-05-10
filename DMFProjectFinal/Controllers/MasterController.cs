using DMFProjectFinal.Models;
using DMFProjectFinal.Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace DMFProjectFinal.Controllers
{
    [SessionFilter]   
    public class MasterController : Controller
    {
        private dfm_dbEntities db = new dfm_dbEntities();

        #region StateMaster
        public ActionResult StateMaster()
        {
            ViewBag.LstData = db.StateMasters.Where(x => x.IsActive == true).ToList();
            return View();
        }
        public ActionResult CreateStateMaster()
        {
            DTO_StateMaster model = new DTO_StateMaster();
            return View(model);
        }
        [HttpPost]
        public JsonResult CreateStateMaster(DTO_StateMaster model)
        {
            JsonResponse JR = new JsonResponse();
            if (!ModelState.IsValid)
            {
                JR.Data = ModelState.Select(x => x.Value.Errors).Where(y => y.Count > 0).ToList();
                return Json(JR, JsonRequestBehavior.AllowGet);
            }
            if (db.StateMasters.Where(x => x.StateName == model.StateName).Any())
            {
                JR.Message = "State Name " + model.StateName + " Aready exists !";
                return Json(JR, JsonRequestBehavior.AllowGet);
            }
            db.StateMasters.Add(new Models.StateMaster
            {
                CountryID = null,
                CreatedOn = DateTime.Now,
                IsActive = true,
                StateName = model.StateName
            });

            int res = db.SaveChanges();
            if (res > 0)
            {
                JR.IsSuccess = true;
                JR.Message = "Data Saved Successfully";
                JR.RedURL = "/Master/StateMaster";
            }
            else
            {
                JR.Message = "Some Error Occured, Contact to Admin";
            }
            return Json(JR, JsonRequestBehavior.AllowGet);
        }

        public ActionResult EditStateMaster(string id)
        {
            if (String.IsNullOrEmpty(id))
            {
                return RedirectToAction("Login", "Account");
            }
            try
            {
                int _id = int.Parse(CryptoEngine.Decrypt(id));
                var Info = db.StateMasters.Where(x => x.StateID == _id).FirstOrDefault();
                if (Info != null)
                {
                    return View("~/Views/Master/CreateStateMaster.cshtml", new DTO_StateMaster { StateID = CryptoEngine.Encrypt(Info.StateID.ToString()), StateName = Info.StateName });
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
        public JsonResult EditStateMaster(DTO_StateMaster model)
        {
            JsonResponse JR = new JsonResponse();
            if (!ModelState.IsValid)
            {
                JR.Data = ModelState.Select(x => x.Value.Errors).Where(y => y.Count > 0).ToList();
                return Json(JR, JsonRequestBehavior.AllowGet);
            }
            if (!String.IsNullOrEmpty(model.StateID))
            {
                int _id = int.Parse(CryptoEngine.Decrypt(model.StateID));
                if (db.StateMasters.Where(x => x.StateName == model.StateName && x.StateID != _id).Any())
                {
                    JR.Message = "State Name " + model.StateName + " Aready exists !";
                    return Json(JR, JsonRequestBehavior.AllowGet);
                }
                var Info = db.StateMasters.Where(x => x.StateID == _id).FirstOrDefault();
                if (Info != null)
                {
                    db.DataLogs.Add(new DataLog
                    {
                        CreatedBy = UserManager.GetUserLoginInfo(User.Identity.Name).LoginID,
                        CreatedOn = DateTime.Now,
                        LogInfo = BusinessLogics.ConvertModelToJSONString(Info),
                        URLInfo = "EditStateMaster /POST"
                    });

                    Info.StateName = model.StateName;
                }
            }
            int res = db.SaveChanges();
            if (res > 0)
            {
                JR.IsSuccess = true;
                JR.Message = "Data Updated Successfully";
                JR.RedURL = "/Master/StateMaster";
            }
            else
            {
                JR.Message = "Some Error Occured, Contact to Admin";
            }
            return Json(JR, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult DeleteStateMaster(string id)
        {
            JsonResponse JR = new JsonResponse();
            if (!String.IsNullOrEmpty(id))
            {
                int _id = int.Parse(CryptoEngine.Decrypt(id));
                var Info = db.StateMasters.Where(x => x.StateID == _id).FirstOrDefault();
                if (Info != null)
                {
                    db.DataLogs.Add(new DataLog
                    {
                        CreatedBy = UserManager.GetUserLoginInfo(User.Identity.Name).LoginID,
                        CreatedOn = DateTime.Now,
                        LogInfo = BusinessLogics.ConvertModelToJSONString(Info),
                        URLInfo = "DeleteStateMaster /POST"
                    });

                    db.StateMasters.Remove(Info);
                }
            }
            int res = db.SaveChanges();
            if (res > 0)
            {
                JR.IsSuccess = true;
                JR.Message = "Data Deleted Successfully";
                JR.RedURL = "/Master/StateMaster";
            }
            else
            {
                JR.Message = "Some Error Occured, Contact to Admin";
            }
            return Json(JR, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region SectorType
        public ActionResult SectorTypeMaster()
        {
            ViewBag.LstData = db.SectorTypeMasters.Where(x => x.IsActive == true).ToList();
            return View();
        }
        public ActionResult CreateSectorTypeMaster()
        {
            DTO_SectorTypeMaster model = new DTO_SectorTypeMaster();
            return View(model);
        }
        #region Ramdhyan 02.04.2024
        [HttpPost]
        public JsonResult CreateSectorTypeMaster(DTO_SectorTypeMaster model)
        {
            JsonResponse JR = new JsonResponse();
            if (!ModelState.IsValid)
            {
                JR.Data = ModelState.Select(x => x.Value.Errors).Where(y => y.Count > 0).ToList();
                return Json(JR, JsonRequestBehavior.AllowGet);
            }
            if (db.SectorTypeMasters.Where(x => x.SectorType == model.SectorType).Any())
            {
                JR.Message = "Sector Type Name " + model.SectorType + " Aready exists !";
                return Json(JR, JsonRequestBehavior.AllowGet);
            }
            db.SectorTypeMasters.Add(new Models.SectorTypeMaster
            {
                IsActive = true,
                SectorType = model.SectorType,
                Percentage=model.Percentage
            });

            int res = db.SaveChanges();
            if (res > 0)
            {
                JR.IsSuccess = true;
                JR.Message = "Data Saved Successfully";
                JR.RedURL = "/Master/SectorTypeMaster";
            }
            else
            {
                JR.Message = "Some Error Occured, Contact to Admin";
            }
            return Json(JR, JsonRequestBehavior.AllowGet);
        }
        #endregion
        public ActionResult EditSectorTypeMaster(string id)
        {
            if (String.IsNullOrEmpty(id))
            {
                return RedirectToAction("Login", "Account");
            }
            try
            {
                int _id = int.Parse(CryptoEngine.Decrypt(id));
                var Info = db.SectorTypeMasters.Where(x => x.SectorTypeID == _id).FirstOrDefault();
                if (Info != null)
                {
                    return View("~/Views/Master/CreateSectorTypeMaster.cshtml", new DTO_SectorTypeMaster { SectorTypeID = CryptoEngine.Encrypt(Info.SectorTypeID.ToString()), SectorType = Info.SectorType,Percentage=Info.Percentage });
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
        public JsonResult EditSectorTypeMaster(DTO_SectorTypeMaster model)
        {
            JsonResponse JR = new JsonResponse();
            if (!ModelState.IsValid)
            {
                JR.Data = ModelState.Select(x => x.Value.Errors).Where(y => y.Count > 0).ToList();
                return Json(JR, JsonRequestBehavior.AllowGet);
            }
            if (!String.IsNullOrEmpty(model.SectorTypeID))
            {
                int _id = int.Parse(CryptoEngine.Decrypt(model.SectorTypeID));
                if (db.SectorTypeMasters.Where(x => x.SectorType == model.SectorType && x.SectorTypeID != _id).Any())
                {
                    JR.Message = "Sector Type Name " + model.SectorType + " Aready exists !";
                    return Json(JR, JsonRequestBehavior.AllowGet);
                }
                var Info = db.SectorTypeMasters.Where(x => x.SectorTypeID == _id).FirstOrDefault();
                if (Info != null)
                {
                    db.DataLogs.Add(new DataLog
                    {
                        CreatedBy = UserManager.GetUserLoginInfo(User.Identity.Name).LoginID,
                        CreatedOn = DateTime.Now,
                        LogInfo = BusinessLogics.ConvertModelToJSONString(Info),
                        URLInfo = "EditSectorTypeMaster /POST"
                    });

                    Info.SectorType = model.SectorType;
                    Info.Percentage = model.Percentage;
                }
            }
            int res = db.SaveChanges();
            if (res > 0)
            {
                JR.IsSuccess = true;
                JR.Message = "Data Updated Successfully";
                JR.RedURL = "/Master/SectorTypeMaster";
            }
            else
            {
                JR.Message = "Some Error Occured, Contact to Admin";
            }
            return Json(JR, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult DeleteSectorTypeMaster(string id)
        {
            JsonResponse JR = new JsonResponse();
            if (!String.IsNullOrEmpty(id))
            {
                int _id = int.Parse(CryptoEngine.Decrypt(id));
                var Info = db.SectorTypeMasters.Where(x => x.SectorTypeID == _id).FirstOrDefault();
                if (Info != null)
                {
                    db.DataLogs.Add(new DataLog
                    {
                        CreatedBy = UserManager.GetUserLoginInfo(User.Identity.Name).LoginID,
                        CreatedOn = DateTime.Now,
                        LogInfo = BusinessLogics.ConvertModelToJSONString(Info),
                        URLInfo = "DeleteSectorTypeMaster /POST"
                    });

                    db.SectorTypeMasters.Remove(Info);
                }
            }
            int res = db.SaveChanges();
            if (res > 0)
            {
                JR.IsSuccess = true;
                JR.Message = "Data Deleted Successfully";
                JR.RedURL = "/Master/SectorTypeMaster";
            }
            else
            {
                JR.Message = "Some Error Occured, Contact to Admin";
            }
            return Json(JR, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region RegisterAgency
        public ActionResult RegisterAgency(int? StateID, int? DistID)
        {
            var LstData = (from ag in db.AgenciesInfoes
                           join dst in db.DistrictMasters on ag.DistID equals dst.DistrictId
                           join state in db.StateMasters on ag.StateID equals state.StateID
                           where ag.IsActive == true
                           && ag.StateID == (StateID == null ? ag.StateID : StateID)
                           && ag.DistID == (DistID == null ? ag.DistID : DistID)
                           select new DTO_AgenciesInfo
                           {
                               Address = ag.Address,
                               AgencyID = ag.AgencyID.ToString(),
                               DistName = dst.DistrictName,
                               EmailID = ag.EmailID,
                               MobileNo = ag.MobileNo,
                               Name = ag.Name,
                               OwnerName = ag.OwnerName,
                               StateName = state.StateName
                           }).ToList();
            ViewBag.LstData = LstData;
            return View();
        }
        public ActionResult CreateRegisterAgency()
        {
            DTO_AgenciesInfo model = new DTO_AgenciesInfo();
            ViewBag.StateID = new SelectList(db.StateMasters.Where(x => x.IsActive == true), "StateID", "StateName", null); //db.StateMasters.Where(x=>x.IsActive==true)
            ViewBag.DistID = new SelectList(db.DistrictMasters.Where(x => x.IsActive == true), "DistrictId", "DistrictName", null); //db.StateMasters.Where(x=>x.IsActive==true)
            ViewBag.BankID = new SelectList(db.BankMasters.Where(x => x.IsActive == true), "BankID", "Name", null);
            return View(model);
        }
        [HttpPost]
        public JsonResult CreateRegisterAgency(DTO_AgenciesInfo model)
        {
            JsonResponse JR = new JsonResponse();
            if (!ModelState.IsValid)
            {
                JR.Data = ModelState.Select(x => x.Value.Errors).Where(y => y.Count > 0).ToList();
                return Json(JR, JsonRequestBehavior.AllowGet);
            }
            if (db.AgenciesInfoes.Where(x => x.OwnerName == model.OwnerName && x.Name == model.Name && x.StateID == model.StateID && x.DistID == model.DistID).Any())
            {
                JR.Message = "Agency Name " + model.Name + " Aready exists !";
                return Json(JR, JsonRequestBehavior.AllowGet);
            }
            db.AgenciesInfoes.Add(new Models.AgenciesInfo
            {
                CreatedOn = DateTime.Now,
                IsActive = true,
                Address = model.Address,
                CreatedBy = UserManager.GetUserLoginInfo(User.Identity.Name).LoginID,
                DistID = model.DistID,
                EmailID = model.EmailID,
                MobileNo = model.MobileNo,
                Name = model.Name,
                OwnerName = model.OwnerName,
                StateID = model.StateID,
                AccNo = model.AccNo,
                AlternateMobNo = model.AlternateMobNo,
                BankID = model.BankID,
                BranchName = model.BranchName,
                GSTIN = model.GSTIN,
                LandLineNo = model.LandLineNo,
                PANNo = model.PANNo,
                IFSCCode = model.IFSCCode
            });

            int res = db.SaveChanges();
            if (res > 0)
            {
                JR.IsSuccess = true;
                JR.Message = "Data Saved Successfully";
                JR.RedURL = "/Master/RegisterAgency";
            }
            else
            {
                JR.Message = "Some Error Occured, Contact to Admin";
            }
            return Json(JR, JsonRequestBehavior.AllowGet);
        }

        public ActionResult EditRegisterAgency(string id)
        {
            if (String.IsNullOrEmpty(id))
            {
                return RedirectToAction("Login", "Account");
            }
            try
            {
                int _id = int.Parse(CryptoEngine.Decrypt(id));
                var Info = db.AgenciesInfoes.Where(x => x.AgencyID == _id).FirstOrDefault();
                if (Info != null)
                {
                    ViewBag.StateID = new SelectList(db.StateMasters.Where(x => x.IsActive == true), "StateID", "StateName", Info.StateID);
                    ViewBag.DistID = new SelectList(db.DistrictMasters.Where(x => x.IsActive == true && (x.StateID == Info.StateID)), "DistrictId", "DistrictName", Info.DistID);
                    ViewBag.BankID = new SelectList(db.BankMasters.Where(x => x.IsActive == true), "BankID", "Name", Info.BankID);
                    return View("~/Views/Master/CreateRegisterAgency.cshtml", new DTO_AgenciesInfo
                    {
                        AgencyID = CryptoEngine.Encrypt(Info.AgencyID.ToString()),
                        Address = Info.Address,
                        DistID = Info.DistID,
                        EmailID = Info.EmailID,
                        MobileNo = Info.MobileNo,
                        Name = Info.Name,
                        OwnerName = Info.OwnerName,
                        StateID = Info.StateID,
                        BankID = Info.BankID,
                        AccNo = Info.AccNo,
                        AlternateMobNo = Info.AlternateMobNo,
                        BranchName = Info.BranchName,
                        GSTIN = Info.GSTIN,
                        IFSCCode = Info.IFSCCode,
                        LandLineNo = Info.LandLineNo,
                        PANNo = Info.PANNo
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
        public JsonResult EditRegisterAgency(DTO_AgenciesInfo model)
        {
            JsonResponse JR = new JsonResponse();
            if (!ModelState.IsValid)
            {
                JR.Data = ModelState.Select(x => x.Value.Errors).Where(y => y.Count > 0).ToList();
                return Json(JR, JsonRequestBehavior.AllowGet);
            }
            if (!String.IsNullOrEmpty(model.AgencyID))
            {
                long _id = long.Parse(CryptoEngine.Decrypt(model.AgencyID));
                if (db.AgenciesInfoes.Where(x => x.OwnerName == model.OwnerName && x.Name == model.Name && x.StateID == model.StateID && x.DistID == model.DistID && x.AgencyID != _id).Any())
                {
                    JR.Message = "Agency Name " + model.Name + " already exists !";
                    return Json(JR, JsonRequestBehavior.AllowGet);
                }
                var Info = db.AgenciesInfoes.Where(x => x.AgencyID == _id).FirstOrDefault();
                if (Info != null)
                {
                    db.DataLogs.Add(new DataLog
                    {
                        CreatedBy = UserManager.GetUserLoginInfo(User.Identity.Name).LoginID,
                        CreatedOn = DateTime.Now,
                        LogInfo = BusinessLogics.ConvertModelToJSONString(Info),
                        URLInfo = "EditRegisterAgency /POST"
                    });

                    Info.Address = model.Address;
                    Info.DistID = model.DistID;
                    Info.EmailID = model.EmailID;
                    Info.MobileNo = model.MobileNo;
                    Info.ModifyBy = UserManager.GetUserLoginInfo(User.Identity.Name).LoginID;
                    Info.ModifyDate = DateTime.Now;
                    Info.Name = model.Name;
                    Info.OwnerName = model.OwnerName;
                    Info.StateID = model.StateID;

                    Info.AccNo = model.AccNo;
                    Info.AlternateMobNo = model.AlternateMobNo;
                    Info.BankID = model.BankID;
                    Info.BranchName = model.BranchName;
                    Info.GSTIN = model.GSTIN;
                    Info.LandLineNo = model.LandLineNo;
                    Info.PANNo = model.PANNo;
                    Info.IFSCCode = model.IFSCCode;
                }
            }
            int res = db.SaveChanges();
            if (res > 0)
            {
                JR.IsSuccess = true;
                JR.Message = "Data Updated Successfully";
                JR.RedURL = "/Master/RegisterAgency";
            }
            else
            {
                JR.Message = "Some Error Occured, Contact to Admin";
            }
            return Json(JR, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult DeleteRegisterAgency(string id)
        {
            JsonResponse JR = new JsonResponse();
            if (!String.IsNullOrEmpty(id))
            {
                int _id = int.Parse(CryptoEngine.Decrypt(id));
                var Info = db.AgenciesInfoes.Where(x => x.AgencyID == _id).FirstOrDefault();
                if (Info != null)
                {
                    db.DataLogs.Add(new DataLog
                    {
                        CreatedBy = UserManager.GetUserLoginInfo(User.Identity.Name).LoginID,
                        CreatedOn = DateTime.Now,
                        LogInfo = BusinessLogics.ConvertModelToJSONString(Info),
                        URLInfo = "DeleteRegisterAgency /POST"
                    });

                    db.AgenciesInfoes.Remove(Info);
                }
            }
            int res = db.SaveChanges();
            if (res > 0)
            {
                JR.IsSuccess = true;
                JR.Message = "Data Deleted Successfully";
                JR.RedURL = "/Master/RegisterAgency";
            }
            else
            {
                JR.Message = "Some Error Occured, Contact to Admin";
            }
            return Json(JR, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region CommitteeMaster
        public ActionResult CommitteeMaster(int? CommitteeTypeID, int? DesignationID)
        {
            var LstData = (from cm in db.CommitteeMasters
                           join ctm in db.CommitteeTypeMasters on cm.CommitteeTypeID equals ctm.CommitteeTypeID
                           join desg in db.DesignationMasters on cm.DesignationID equals desg.DesingantionID
                           join cmd in db.CommitteeDesignationMasters on cm.CommitteeDesignationId equals
                           cmd.CommitteeDesignationId join
                           d in db.DistrictMasters on cm.DistID equals d.DistrictId
                           where cm.IsActive == true
                           && cm.CommitteeTypeID == (CommitteeTypeID == null ? cm.CommitteeTypeID : CommitteeTypeID)
                           && cm.DesignationID == (DesignationID == null ? cm.DesignationID : DesignationID)
                           select new DTO_CommitteeMaster
                           {
                               CommitteeID = cm.CommitteeID.ToString(),
                               CommitteeName = cm.CommitteeName,
                               CommitteeType = ctm.CommitteeType,
                               Designation = desg.Designation,
                               EmailID = cm.EmailID,
                               MobileNo = cm.MobileNo,
                               CommitteeDesignationName= cmd.CommitteeDesignationName,
                               DistrictName=d.DistrictName
                               
                           }).ToList();
            ViewBag.LstData = LstData;
            return View();
        }
        public ActionResult CreateCommitteeMaster()
        {
            DTO_CommitteeMaster model = new DTO_CommitteeMaster();
            ViewBag.CommitteeTypeID = new SelectList(db.CommitteeTypeMasters.Where(x => x.IsActive == true), "CommitteeTypeID", "CommitteeType", null);
            ViewBag.DesignationID = new SelectList(db.DesignationMasters.Where(x => x.IsActive == true), "DesingantionID", "Designation", null);
            ViewBag.DistID = new SelectList(db.DistrictMasters.Where(x => x.IsActive == true), "DistrictId", "DistrictName", null);

            ViewBag.CommitteeDesignationId = new SelectList(db.CommitteeDesignationMasters.Where(x => x.IsActive == true), "CommitteeDesignationId", "CommitteeDesignationName", null);


            return View(model);
        }
        [HttpPost]
        public JsonResult CreateCommitteeMaster(DTO_CommitteeMaster model)
        {
            JsonResponse JR = new JsonResponse();
            if (!ModelState.IsValid)
            {
                JR.Data = ModelState.Select(x => x.Value.Errors).Where(y => y.Count > 0).ToList();
                return Json(JR, JsonRequestBehavior.AllowGet);
            }
            if (db.CommitteeMasters.Where(x => x.CommitteeName == model.CommitteeName).Any())
            {
                JR.Message = "Committee Name " + model.CommitteeName + " Aready exists !";
                return Json(JR, JsonRequestBehavior.AllowGet);
            }
            db.CommitteeMasters.Add(new Models.CommitteeMaster
            {
                CreatedOn = DateTime.Now,
                IsActive = true,
                CreatedBy = UserManager.GetUserLoginInfo(User.Identity.Name).LoginID,
                CommitteeName = model.CommitteeName,
                CommitteeTypeID = model.CommitteeTypeID,
                DesignationID = model.DesignationID,
                EmailID = model.EmailID,
                MobileNo = model.MobileNo,
                CommitteeDesignationId=model.CommitteeDesignationId,
                DistID=model.DistID

               
            });

            int res = db.SaveChanges();
            if (res > 0)
            {
                JR.IsSuccess = true;
                JR.Message = "Data Saved Successfully";
                JR.RedURL = "/Master/CommitteeMaster";
            }
            else
            {
                JR.Message = "Some Error Occured, Contact to Admin";
            }
            return Json(JR, JsonRequestBehavior.AllowGet);
        }

        public ActionResult EditCommitteeMaster(string id)
        {
            if (String.IsNullOrEmpty(id))
            {
                return RedirectToAction("Login", "Account");
            }
            try
            {
                int _id = int.Parse(CryptoEngine.Decrypt(id));
                var Info = db.CommitteeMasters.Where(x => x.CommitteeID == _id).FirstOrDefault();
                if (Info != null)
                {
                    ViewBag.CommitteeTypeID = new SelectList(db.CommitteeTypeMasters.Where(x => x.IsActive == true), "CommitteeTypeID", "CommitteeType", Info.CommitteeTypeID);
                    ViewBag.DesignationID = new SelectList(db.DesignationMasters.Where(x => x.IsActive == true), "DesingantionID", "Designation", Info.DesignationID);
                    ViewBag.DistID = new SelectList(db.DistrictMasters.Where(x => x.IsActive == true), "DistrictId", "DistrictName", Info.DistID);

                    ViewBag.CommitteeDesignationId = new SelectList(db.CommitteeDesignationMasters.Where(x => x.IsActive == true), "CommitteeDesignationId", "CommitteeDesignationName", Info.CommitteeDesignationId);




                    return View("~/Views/Master/CreateCommitteeMaster.cshtml", new DTO_CommitteeMaster
                    {
                        CommitteeID = CryptoEngine.Encrypt(Info.CommitteeID.ToString()),
                        CommitteeName = Info.CommitteeName,
                        CommitteeTypeID = Info.CommitteeTypeID,
                        DesignationID = Info.DesignationID,
                        EmailID = Info.EmailID,
                        MobileNo = Info.MobileNo,
                        CommitteeDesignationId=Info.CommitteeDesignationId,
                        DistID=Info.DistID


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
        public JsonResult EditCommitteeMaster(DTO_CommitteeMaster model)
        {
            JsonResponse JR = new JsonResponse();
            if (!ModelState.IsValid)
            {
                JR.Data = ModelState.Select(x => x.Value.Errors).Where(y => y.Count > 0).ToList();
                return Json(JR, JsonRequestBehavior.AllowGet);
            }
            if (!String.IsNullOrEmpty(model.CommitteeID))
            {
                long _id = long.Parse(CryptoEngine.Decrypt(model.CommitteeID));
                if (db.CommitteeMasters.Where(x => x.CommitteeName == model.CommitteeName && x.CommitteeID != _id).Any())
                {
                    JR.Message = "Committee Name " + model.CommitteeName + " Aready exists !";
                    return Json(JR, JsonRequestBehavior.AllowGet);
                }
                var Info = db.CommitteeMasters.Where(x => x.CommitteeID == _id).FirstOrDefault();
                if (Info != null)
                {
                    db.DataLogs.Add(new DataLog
                    {
                        CreatedBy = UserManager.GetUserLoginInfo(User.Identity.Name).LoginID,
                        CreatedOn = DateTime.Now,
                        LogInfo = BusinessLogics.ConvertModelToJSONString(Info),
                        URLInfo = "EditCommitteeMaster /POST"
                    });

                    Info.EmailID = model.EmailID;
                    Info.MobileNo = model.MobileNo;
                    Info.ModifyBy = UserManager.GetUserLoginInfo(User.Identity.Name).LoginID;
                    Info.ModifiedOn = DateTime.Now;
                    Info.CommitteeName = model.CommitteeName;
                    Info.CommitteeTypeID = model.CommitteeTypeID;
                    Info.DesignationID = model.DesignationID;
                    Info.CommitteeDesignationId = model.CommitteeDesignationId;
                    Info.DistID = model.DistID;


                }
            }
            int res = db.SaveChanges();
            if (res > 0)
            {
                JR.IsSuccess = true;
                JR.Message = "Data Updated Successfully";
                JR.RedURL = "/Master/CommitteeMaster";
            }
            else
            {
                JR.Message = "Some Error Occured, Contact to Admin";
            }
            return Json(JR, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult DeleteCommitteeMaster(string id)
        {
            JsonResponse JR = new JsonResponse();
            if (!String.IsNullOrEmpty(id))
            {
                int _id = int.Parse(CryptoEngine.Decrypt(id));
                var Info = db.CommitteeMasters.Where(x => x.CommitteeID == _id).FirstOrDefault();
                if (Info != null)
                {
                    db.DataLogs.Add(new DataLog
                    {
                        CreatedBy = UserManager.GetUserLoginInfo(User.Identity.Name).LoginID,
                        CreatedOn = DateTime.Now,
                        LogInfo = BusinessLogics.ConvertModelToJSONString(Info),
                        URLInfo = "DeleteCommitteeMaster /POST"
                    });

                    db.CommitteeMasters.Remove(Info);
                }
            }
            int res = db.SaveChanges();
            if (res > 0)
            {
                JR.IsSuccess = true;
                JR.Message = "Data Deleted Successfully";
                JR.RedURL = "/Master/CommitteeMaster";
            }
            else
            {
                JR.Message = "Some Error Occured, Contact to Admin";
            }
            return Json(JR, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region LesseeMaster
        public ActionResult LesseeMaster(int? DisID, int? TehsilID, long? VillageID, int? MinralID)
        {
            var LstData = (from lm in db.LesseeMasters
                           join dm in db.DistrictMasters on lm.DistID equals dm.DistrictId
                           join sm in db.StateMasters on lm.StateID equals sm.StateID
                           join th in db.TehsilMasters on lm.TehsilID equals th.TehsilId
                           join vm in db.VillageMasters on lm.VillageID equals vm.VillageId
                           join mm in db.MineralNameMasters on lm.MinralID equals mm.MineralId
                           where lm.IsActive == true
                           && lm.DistID == (DisID == null ? lm.DistID : DisID)
                           && lm.TehsilID == (TehsilID == null ? lm.TehsilID : TehsilID)
                           && lm.VillageID == (VillageID == null ? lm.VillageID : VillageID)
                           && lm.MinralID == (MinralID == null ? lm.MinralID : MinralID)
                           select new DTO_LesseeMaster
                           {
                               Areacode = lm.Areacode,
                               BidRate = lm.BidRate,
                               DistName = dm.DistrictName,
                               EmailID = lm.EmailID,
                               GataNo = lm.GataNo,
                               LeaseFromDate = lm.LeaseFromDate,
                               LeaseToDate = lm.LeaseToDate,
                               LesseeID = lm.LesseeID.ToString(),
                               LeaseID = lm.LeaseID,
                               LesseeName = lm.LesseeName,
                               Minral = mm.MineralName,
                               MobNo = lm.MobNo,
                               State = sm.StateName,
                               Tehsil = th.TehsilName,
                               TotalAreainHect = lm.TotalAreainHect,
                               Village = vm.VillageNameInEnglish
                           }).ToList();
            ViewBag.LstData = LstData;
            return View();
        }
        public ActionResult CreateLesseeMaster()
        {
            DTO_LesseeMaster model = new DTO_LesseeMaster();
            ViewBag.StateID = new SelectList(db.StateMasters.Where(x => x.IsActive == true), "StateID", "StateName", null);
            ViewBag.DistID = new SelectList(db.DistrictMasters.Where(x => x.IsActive == true), "DistrictId", "DistrictName", null);
            if(model.VillageID!=null)
            {
               ViewBag.VillageID = new SelectList(db.VillageMasters.Where(x => x.IsActive == true), "VillageId", "VillageNameInEnglish", null);

            }

            else
            {

         List<SelectListItem> mySkills = new List<SelectListItem>() {
        new SelectListItem {
            Text = "-select-", Value = "0"
        },
        
    };

                ViewBag.VillageID = mySkills;

            }

            //ViewBag.VillageID = new SelectList(db.VillageMasters.Where(x => x.IsActive == true), "VillageId", "VillageNameInEnglish", null);
            ViewBag.TehsilID = new SelectList(db.TehsilMasters.Where(x => x.IsActive == true), "TehsilId", "TehsilName", null);
            ViewBag.MinralID = new SelectList(db.MineralNameMasters.Where(x => x.IsActive == true), "MineralId", "MineralName", null);
            return View(model);
        }
        [HttpPost]
        public JsonResult CreateLesseeMaster(DTO_LesseeMaster model)
        {
            JsonResponse JR = new JsonResponse();
            if (!ModelState.IsValid)
            {
                JR.Data = ModelState.Select(x => x.Value.Errors).Where(y => y.Count > 0).ToList();
                return Json(JR, JsonRequestBehavior.AllowGet);
            }
            if (db.LesseeMasters.Where(x => x.LesseeName == model.LesseeName && x.LeaseID == model.LeaseID).Any())
            {
                JR.Message = "Lessee Name " + model.LesseeName + " with Lease ID " + model.LeaseID + " Aready exists !";
                return Json(JR, JsonRequestBehavior.AllowGet);
            }
            db.LesseeMasters.Add(new Models.LesseeMaster
            {
                CreatedOn = DateTime.Now,
                IsActive = true,
                CreatedBy = UserManager.GetUserLoginInfo(User.Identity.Name).LoginID,
                Areacode = model.Areacode,
                BidRate = model.BidRate,
                DistID = model.DistID,
                EmailID = model.EmailID,
                GataNo = model.GataNo,
                LeaseFromDate = model.LeaseFromDate,
                LeaseToDate = model.LeaseToDate,
                LeaseID = model.LeaseID,
                LesseeName = model.LesseeName,
                MinralID = model.MinralID,
                MobNo = model.MobNo,
                StateID = model.StateID,
                TehsilID = model.TehsilID,
                TotalAreainHect = model.TotalAreainHect,
                VillageID = model.VillageID
            });

            int res = db.SaveChanges();
            if (res > 0)
            {
                JR.IsSuccess = true;
                JR.Message = "Data Saved Successfully";
                JR.RedURL = "/Master/LesseeMaster";
            }
            else
            {
                JR.Message = "Some Error Occured, Contact to Admin";
            }
            return Json(JR, JsonRequestBehavior.AllowGet);
        }

        public ActionResult EditLesseeMaster(string id)
        {
            if (String.IsNullOrEmpty(id))
            {
                return RedirectToAction("Login", "Account");
            }
            try
            {
                long _id = long.Parse(CryptoEngine.Decrypt(id));
                var Info = db.LesseeMasters.Where(x => x.LesseeID == _id).FirstOrDefault();
                if (Info != null)
                {
                    ViewBag.StateID = new SelectList(db.StateMasters.Where(x => x.IsActive == true), "StateID", "StateName", Info.StateID);
                    ViewBag.DistID = new SelectList(db.DistrictMasters.Where(x => x.IsActive == true && x.StateID == (Info.StateID == null ? x.StateID : Info.StateID)), "DistrictId", "DistrictName", Info.DistID);
                    ViewBag.VillageID = new SelectList(db.VillageMasters.Where(x => x.IsActive == true && x.TehsilId == (Info.TehsilID == null ? x.TehsilId : Info.TehsilID)), "VillageId", "VillageNameInEnglish", Info.VillageID);
                    ViewBag.TehsilID = new SelectList(db.TehsilMasters.Where(x => x.IsActive == true && x.DistrictId == (Info.DistID == null ? x.DistrictId : Info.DistID)), "TehsilId", "TehsilName", Info.TehsilID);
                    ViewBag.MinralID = new SelectList(db.MineralNameMasters.Where(x => x.IsActive == true), "MineralId", "MineralName", Info.MinralID);

                    return View("~/Views/Master/CreateLesseeMaster.cshtml", new DTO_LesseeMaster
                    {
                        Areacode = Info.Areacode,
                        BidRate = Info.BidRate,
                        DistID = Info.DistID,
                        EmailID = Info.EmailID,
                        GataNo = Info.GataNo,
                        LeaseFromDate = Info.LeaseFromDate,
                        LeaseID = Info.LeaseID,
                        LeaseToDate = Info.LeaseToDate,
                        LesseeName = Info.LesseeName,
                        LesseeID = CryptoEngine.Encrypt(Info.LesseeID.ToString()),
                        MinralID = Info.MinralID,
                        MobNo = Info.MobNo,
                        StateID = Info.StateID,
                        TehsilID = Info.TehsilID,
                        TotalAreainHect = Info.TotalAreainHect,
                        VillageID = Info.VillageID
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
        public JsonResult EditLesseeMaster(DTO_LesseeMaster model)
        {
            JsonResponse JR = new JsonResponse();
            if (!ModelState.IsValid)
            {
                JR.Data = ModelState.Select(x => x.Value.Errors).Where(y => y.Count > 0).ToList();
                return Json(JR, JsonRequestBehavior.AllowGet);
            }
            if (!String.IsNullOrEmpty(model.LesseeID))
            {
                long _id = long.Parse(CryptoEngine.Decrypt(model.LesseeID));
                if (db.LesseeMasters.Where(x => x.LesseeName == model.LesseeName && x.LeaseID==model.LeaseID && x.LesseeID != _id).Any())
                {
                    JR.Message = "Lessee Name " + model.LesseeName + " with Lease ID " + model.LeaseID + " Aready exists !";
                    return Json(JR, JsonRequestBehavior.AllowGet);
                }
                var Info = db.LesseeMasters.Where(x => x.LesseeID == _id).FirstOrDefault();
                if (Info != null)
                {
                    db.DataLogs.Add(new DataLog
                    {
                        CreatedBy = UserManager.GetUserLoginInfo(User.Identity.Name).LoginID,
                        CreatedOn = DateTime.Now,
                        LogInfo = BusinessLogics.ConvertModelToJSONString(Info),
                        URLInfo = "EditLesseeMaster /POST"
                    });

                    Info.Areacode = model.Areacode;
                    Info.BidRate = model.BidRate;
                    Info.DistID = model.DistID;
                    Info.EmailID = model.EmailID;
                    Info.GataNo = model.GataNo;
                    Info.LeaseFromDate = model.LeaseFromDate;
                    Info.LeaseID = model.LeaseID;
                    Info.LeaseToDate = model.LeaseToDate;
                    Info.LesseeName = model.LesseeName;
                    Info.MinralID = model.MinralID;
                    Info.MobNo = model.MobNo;
                    Info.StateID = model.StateID;
                    Info.TehsilID = model.TehsilID;
                    Info.TotalAreainHect = model.TotalAreainHect;
                    Info.VillageID = model.VillageID;
                    Info.ModifyBy = UserManager.GetUserLoginInfo(User.Identity.Name).LoginID;
                    Info.ModifiedOn = DateTime.Now;
                }

                
            }
            int res = db.SaveChanges();
            if (res > 0)
            {
                JR.IsSuccess = true;
                JR.Message = "Data Updated Successfully";
                JR.RedURL = "/Master/LesseeMaster";
            }
            else
            {
                JR.Message = "Some Error Occured, Contact to Admin";
            }
            return Json(JR, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult DeleteLesseeMaster(string id)
        {
            JsonResponse JR = new JsonResponse();
            if (!String.IsNullOrEmpty(id))
            {
                long _id = long.Parse(CryptoEngine.Decrypt(id));
                var Info = db.LesseeMasters.Where(x => x.LesseeID == _id).FirstOrDefault();
                if (Info != null)
                {
                    db.DataLogs.Add(new DataLog
                    {
                        CreatedBy = UserManager.GetUserLoginInfo(User.Identity.Name).LoginID,
                        CreatedOn = DateTime.Now,
                        LogInfo = BusinessLogics.ConvertModelToJSONString(Info),
                        URLInfo = "DeleteLesseeMaster /POST"
                    });

                    db.LesseeMasters.Remove(Info);
                }
            }
            int res = db.SaveChanges();
            if (res > 0)
            {
                JR.IsSuccess = true;
                JR.Message = "Data Deleted Successfully";
                JR.RedURL = "/Master/LesseeMaster";
            }
            else
            {
                JR.Message = "Some Error Occured, Contact to Admin";
            }
            return Json(JR, JsonRequestBehavior.AllowGet);
        }
        #endregion



        #region ProjectMaster
        public ActionResult ProjectMaster(int? DistID)
        {
            if (UserManager.GetUserLoginInfo(User.Identity.Name).RoleID == 2)
            {
                DistID = UserManager.GetUserLoginInfo(User.Identity.Name).DistID;
            }
            var LstData = (from pm in db.ProjectMasters
                           join dst in db.DistrictMasters on pm.DistID equals dst.DistrictId
                           join st in db.SectorTypeMasters on pm.SectorTypeId equals st.SectorTypeID
                           join sn in db.SectorNameMasters on pm.SectorNameId equals sn.SectorNameId
                           where pm.IsActive == true
                           && pm.DistID == (DistID == null ? pm.DistID : DistID)
                           select new DTO_ProjectMaster
                           {
                               DistName=dst.DistrictName,
                               ProjectCode=pm.ProjectCode,
                               ProjectNo = pm.ProjectNo,
                               ProjectDescription=pm.ProjectDescription,
                               ProjectName=pm.ProjectName,
                               ProjectID=pm.ProjectID.ToString(),
                               SectorType=st.SectorType,
                               SectorName=sn.SectorName
                              

                           }).ToList();
            ViewBag.LstData = LstData;
            return View();
        }
        public ActionResult CreateProjectMaster()
        {
            DTO_ProjectMaster model = new DTO_ProjectMaster();
            int? CDist = null;
            if (UserManager.GetUserLoginInfo(User.Identity.Name).RoleID == 2)
            {
                CDist = UserManager.GetUserLoginInfo(User.Identity.Name).DistID;
            }
            ViewBag.DistID = new SelectList(db.DistrictMasters.Where(x => x.IsActive == true && x.DistrictId==(CDist==null?x.DistrictId: CDist)), "DistrictId", "DistrictName", CDist);

            ViewBag.SectorTypeId = new SelectList(db.SectorTypeMasters.Where(x => x.IsActive == true), "SectorTypeID", "SectorType", null);

            ViewBag.SectorNameId = new SelectList(db.SectorNameMasters.Where(x => x.IsActive == true), "SectorNameId", "SectorName", null);



            return View(model);
        }
        [HttpPost]
        public JsonResult CreateProjectMaster(DTO_ProjectMaster model)
        {
            JsonResponse JR = new JsonResponse();
            if (!ModelState.IsValid)
            {
                JR.Data = ModelState.Select(x => x.Value.Errors).Where(y => y.Count > 0).ToList();
                return Json(JR, JsonRequestBehavior.AllowGet);
            }
            if (UserManager.GetUserLoginInfo(User.Identity.Name).RoleID == 2 && UserManager.GetUserLoginInfo(User.Identity.Name).DistID != model.DistID)
            {
                JR.Message = "Data Tempered !";
                return Json(JR, JsonRequestBehavior.AllowGet);
            }
            if (db.ProjectMasters.Where(x => x.ProjectName == model.ProjectName && x.DistID==model.DistID).Any())
            {
                JR.Message = "Project Name " + model.ProjectName + " Aready exists !";
                return Json(JR, JsonRequestBehavior.AllowGet);
            }

            if (db.ProjectMasters.Where(x => x.ProjectName == model.ProjectName && x.ProjectCode == model.ProjectCode).Any())
            {
                JR.Message = "Project Code " + model.ProjectName + " Aready exists !";
                return Json(JR, JsonRequestBehavior.AllowGet);
            }



            db.ProjectMasters.Add(new Models.ProjectMaster
            {
                IsActive = true,
                CreatedBy= UserManager.GetUserLoginInfo(User.Identity.Name).LoginID,
                CreatedOn=DateTime.Now,
                DistID=model.DistID,
                ProjectCode=model.ProjectCode,
                ProjectDescription=model.ProjectDescription,
                ProjectName=model.ProjectName,
                SectorTypeId=model.SectorTypeId,
                SectorNameId = model.SectorNameId


            });

            int res = db.SaveChanges();
            if (res > 0)
            {
                JR.IsSuccess = true;
                JR.Message = "Data Saved Successfully";
                JR.RedURL = "/Master/ProjectMaster";
            }
            else
            {
                JR.Message = "Some Error Occured, Contact to Admin";
            }
            return Json(JR, JsonRequestBehavior.AllowGet);
        }

        public ActionResult EditProjectMaster(string id)
        {
            if (String.IsNullOrEmpty(id))
            {
                return RedirectToAction("Login", "Account");
            }
            try
            {
                int _id = int.Parse(CryptoEngine.Decrypt(id));
                var Info = db.ProjectMasters.Where(x => x.ProjectID == _id).FirstOrDefault();
                if (Info != null)
                {
                    int? CDist = null;
                    if (UserManager.GetUserLoginInfo(User.Identity.Name).RoleID == 2)
                    {
                        CDist = UserManager.GetUserLoginInfo(User.Identity.Name).DistID;
                    }
                    ViewBag.DistID = new SelectList(db.DistrictMasters.Where(x => x.IsActive == true && x.DistrictId==(CDist==null?x.DistrictId: CDist)), "DistrictId", "DistrictName", Info.DistID);

                    ViewBag.SectorTypeId = new SelectList(db.SectorTypeMasters.Where(x => x.IsActive == true), "SectorTypeID", "SectorType", Info.SectorTypeId);

                    ViewBag.SectorNameId = new SelectList(db.SectorNameMasters.Where(x => x.IsActive == true), "SectorNameId", "SectorName", Info.SectorNameId);

                    return View("~/Views/Master/CreateProjectMaster.cshtml", new DTO_ProjectMaster { ProjectID = CryptoEngine.Encrypt(Info.ProjectID.ToString()), DistID = Info.DistID,
                        SectorTypeId=Info.SectorTypeId,
                        SectorNameId=Info.SectorNameId,
                        ProjectCode=Info.ProjectCode,ProjectDescription=Info.ProjectDescription,ProjectName=Info.ProjectName });
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
        public JsonResult EditProjectMaster(DTO_ProjectMaster model)
        {
            JsonResponse JR = new JsonResponse();
            if (!ModelState.IsValid)
            {
                JR.Data = ModelState.Select(x => x.Value.Errors).Where(y => y.Count > 0).ToList();
                return Json(JR, JsonRequestBehavior.AllowGet);
            }
            if (UserManager.GetUserLoginInfo(User.Identity.Name).RoleID == 2 && UserManager.GetUserLoginInfo(User.Identity.Name).DistID !=model.DistID)
            {
                JR.Message = "Data Tempered !";
                return Json(JR, JsonRequestBehavior.AllowGet);
            }
            if (!String.IsNullOrEmpty(model.ProjectID))
            {
                int _id = int.Parse(CryptoEngine.Decrypt(model.ProjectID));
                if (db.ProjectMasters.Where(x => x.ProjectName == model.ProjectName && x.DistID == model.DistID && x.ProjectID !=_id).Any())
                {
                    JR.Message = "Project Name " + model.ProjectName + " Aready exists !";
                    return Json(JR, JsonRequestBehavior.AllowGet);
                }
                var Info = db.ProjectMasters.Where(x => x.ProjectID == _id).FirstOrDefault();
                if (Info != null)
                {
                    db.DataLogs.Add(new DataLog
                    {
                        CreatedBy = UserManager.GetUserLoginInfo(User.Identity.Name).LoginID,
                        CreatedOn = DateTime.Now,
                        LogInfo = BusinessLogics.ConvertModelToJSONString(Info),
                        URLInfo = "EditProjectMaster /POST"
                    });

                    Info.DistID = model.DistID;
                    Info.ModifyBy = UserManager.GetUserLoginInfo(User.Identity.Name).LoginID;
                    Info.ModifyDate = DateTime.Now;
                    Info.ProjectDescription = model.ProjectDescription;
                    Info.ProjectCode = model.ProjectCode;
                    Info.ProjectName = model.ProjectName;
                    Info.SectorTypeId = model.SectorTypeId;
                    Info.SectorNameId = model.SectorNameId;
                }
            }
            int res = db.SaveChanges();
            if (res > 0)
            {
                JR.IsSuccess = true;
                JR.Message = "Data Updated Successfully";
                JR.RedURL = "/Master/ProjectMaster";
            }
            else
            {
                JR.Message = "Some Error Occured, Contact to Admin";
            }
            return Json(JR, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult DeleteProjectMaster(string id)
        {
            JsonResponse JR = new JsonResponse();
            if (!String.IsNullOrEmpty(id))
            {
                int _id = int.Parse(CryptoEngine.Decrypt(id));
                var Info = db.ProjectMasters.Where(x => x.ProjectID == _id).FirstOrDefault();
                if (Info != null)
                {
                    db.DataLogs.Add(new DataLog
                    {
                        CreatedBy = UserManager.GetUserLoginInfo(User.Identity.Name).LoginID,
                        CreatedOn = DateTime.Now,
                        LogInfo = BusinessLogics.ConvertModelToJSONString(Info),
                        URLInfo = "DeleteProjectMaster /POST"
                    });

                    db.ProjectMasters.Remove(Info);
                }
            }
            int res = db.SaveChanges();
            if (res > 0)
            {
                JR.IsSuccess = true;
                JR.Message = "Data Deleted Successfully";
                JR.RedURL = "/Master/ProjectMaster";
            }
            else
            {
                JR.Message = "Some Error Occured, Contact to Admin";
            }
            return Json(JR, JsonRequestBehavior.AllowGet);
        }
        #endregion
        #region SectorName
        [HttpGet]
        public ActionResult CreateSectorNameMaster()
        {
            DTO_SectorNameMaster model = new DTO_SectorNameMaster();
            ViewBag.SectorTypeId = new SelectList(db.SectorTypeMasters.Where(x => x.IsActive == true), "SectorTypeID", "SectorType");
            return View(model);
        }
        [HttpPost]
        public JsonResult CreateSectorNameMaster(DTO_SectorNameMaster model)
        {
            JsonResponse JR = new JsonResponse();
            if (!ModelState.IsValid)
            {
                JR.Data = ModelState.Select(x => x.Value.Errors).Where(y => y.Count > 0).ToList();
                return Json(JR, JsonRequestBehavior.AllowGet);
            }
            if (db.SectorNameMasters.Where(x => (x.SectorTypeId == model.SectorTypeId && x.SectorName == model.SectorName) || x.SectorCode == model.SectorCode).Any())
            {
                JR.Message = "Sector Name Or Sector Code " + model.SectorName + "/" + model.SectorCode + " Already exists !";
                return Json(JR, JsonRequestBehavior.AllowGet);
            }
            db.SectorNameMasters.Add(new Models.SectorNameMaster
            {
                IsActive = true,
                SectorTypeId = model.SectorTypeId,
                SectorName = model.SectorName,
                SectorCode = model.SectorCode
            });

            int res = db.SaveChanges();
            if (res > 0)
            {
                JR.IsSuccess = true;
                JR.Message = "Data Saved Successfully";
                JR.RedURL = "/Master/SectorNameMaster";
            }
            else
            {
                JR.Message = "Some Error Occured, Contact to Admin";
            }
            return Json(JR, JsonRequestBehavior.AllowGet);
        }
        public ActionResult EditSectorNameMaster(string id)
        {
            if (String.IsNullOrEmpty(id))
            {
                return RedirectToAction("Login", "Account");
            }
            try
            {
                int _id = int.Parse(CryptoEngine.Decrypt(id));
                var Info = db.SectorNameMasters.Where(x => x.SectorNameId == _id).FirstOrDefault();
                if (Info != null)
                {
                    ViewBag.SectorTypeId = new SelectList(db.SectorTypeMasters.Where(x => x.IsActive == true), "SectorTypeID", "SectorType", Info.SectorTypeId);
                    return View("~/Views/Master/CreateSectorNameMaster.cshtml", new DTO_SectorNameMaster { SectorNameId = CryptoEngine.Encrypt(Info.SectorNameId.ToString()), SectorName = Info.SectorName, SectorCode = Info.SectorCode });
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
            return View();
        }
        [HttpPost]
        public JsonResult EditSectorNameMaster(DTO_SectorNameMaster model)
        {
            JsonResponse JR = new JsonResponse();
            if (!ModelState.IsValid)
            {
                JR.Data = ModelState.Select(x => x.Value.Errors).Where(y => y.Count > 0).ToList();
                return Json(JR, JsonRequestBehavior.AllowGet);
            }
            if (!String.IsNullOrEmpty(model.SectorNameId))
            {
                int _id = int.Parse(CryptoEngine.Decrypt(model.SectorNameId));
                if (db.SectorNameMasters.Where(x => ((x.SectorName == model.SectorName && x.SectorTypeId == model.SectorTypeId) || x.SectorCode == model.SectorCode) && x.SectorNameId != _id).Any())
                {
                    JR.Message = "Sector Name " + model.SectorName + " Aready exists !";
                    return Json(JR, JsonRequestBehavior.AllowGet);
                }
                var Info = db.SectorNameMasters.Where(x => x.SectorNameId == _id).FirstOrDefault();
                if (Info != null)
                {
                    db.DataLogs.Add(new DataLog
                    {
                        CreatedBy = UserManager.GetUserLoginInfo(User.Identity.Name).LoginID,
                        CreatedOn = DateTime.Now,
                        LogInfo = BusinessLogics.ConvertModelToJSONString(Info),
                        URLInfo = "EditSectorNameMaster /POST"
                    });

                    Info.SectorName = model.SectorName;
                    Info.SectorTypeId = model.SectorTypeId;
                    Info.SectorCode = model.SectorCode;
                }
            }
            int res = db.SaveChanges();
            if (res > 0)
            {
                JR.IsSuccess = true;
                JR.Message = "Data Updated Successfully";
                JR.RedURL = "/Master/SectorNameMaster";
            }
            else
            {
                JR.Message = "Some Error Occured, Contact to Admin";
            }
            return Json(JR, JsonRequestBehavior.AllowGet);
        }
        public JsonResult DeleteSectorNameMaster(string id)
        {
            JsonResponse JR = new JsonResponse();
            if (!String.IsNullOrEmpty(id))
            {
                int _id = int.Parse(CryptoEngine.Decrypt(id));
                var Info = db.SectorNameMasters.Where(x => x.SectorNameId == _id).FirstOrDefault();
                if (Info != null)
                {
                    db.DataLogs.Add(new DataLog
                    {
                        CreatedBy = UserManager.GetUserLoginInfo(User.Identity.Name).LoginID,
                        CreatedOn = DateTime.Now,
                        LogInfo = BusinessLogics.ConvertModelToJSONString(Info),
                        URLInfo = "DeleteSectorNameMaster /POST"
                    });

                    db.SectorNameMasters.Remove(Info);
                }
            }
            int res = db.SaveChanges();
            if (res > 0)
            {
                JR.IsSuccess = true;
                JR.Message = "Data Deleted Successfully";
                JR.RedURL = "/Master/SectorNameMaster";
            }
            else
            {
                JR.Message = "Some Error Occured, Contact to Admin";
            }
            return Json(JR, JsonRequestBehavior.AllowGet);
        }
        public ActionResult SectorNameMaster()
        {
            var data = (from snm in db.SectorNameMasters
                        join stm in db.SectorTypeMasters on snm.SectorTypeId equals stm.SectorTypeID
                        where snm.IsActive == true
                        select new DTO_SectorNameMaster
                        {
                            SectorName = snm.SectorName,
                            SectorNameId = snm.SectorNameId.ToString(),
                            SectorType = stm.SectorType,
                            SectorCode = snm.SectorCode
                        }).ToList();
            ViewBag.LstData = data;
            return View();
        }
        #endregion

        #region MineralName ---changes by Ramdhyan 02.04.2024
        public ActionResult MineralNameMaster()
        {
            //var data = (from mm in db.MineralNameMasters
            //            join mtm in db.MineralTypeMasters on mm.MineralTypeId equals mtm.MineralTypeId
            //            select new DTO_MineralNameMaster
            //            {
            //                MineralId=mm.MineralId.ToString(),
            //                MineralName = mm.MineralName,
            //                MineralTypeId = mm.MineralTypeId,
            //                MineralCode = mm.MineralCode,
            //                MineralType = mtm.MineralType
            //            }).ToList();
            ViewBag.LstData = db.MineralNameMasters.Where(x => x.IsActive == true).ToList();
            //ViewBag.LstData = data;
            return View();
        }
        [HttpGet]
        public ActionResult CreateMineralNameMaster()
        {
            DTO_MineralNameMaster model = new DTO_MineralNameMaster();
            //ViewBag.mimerals = new SelectList(db.MineralTypeMasters.ToList(), "MineralTypeId", "MineralType");
            return View(model);
        }
        [HttpPost]
        public JsonResult CreateMineralNameMaster(DTO_MineralNameMaster model)
        {
            JsonResponse JR = new JsonResponse();
            if (!ModelState.IsValid)
            {
                JR.Data = ModelState.Select(x => x.Value.Errors).Where(y => y.Count > 0).ToList();
                return Json(JR, JsonRequestBehavior.AllowGet);
            }
            if (db.MineralNameMasters.Where(x => x.MineralName == model.MineralName || x.MineralCode == model.MineralCode).Any())
            {
                JR.Message = "Mineral Name/Mineral Code " + model.MineralName + "/" + model.MineralCode + " Aready exists !";
                return Json(JR, JsonRequestBehavior.AllowGet);
            }
            db.MineralNameMasters.Add(new Models.MineralNameMaster
            {
                IsActive = true,
                MineralName = model.MineralName,
                MineralCode = model.MineralCode,
                MineralType=model.MineralType
            });

            int res = db.SaveChanges();
            if (res > 0)
            {
                JR.IsSuccess = true;
                JR.Message = "Data Saved Successfully";
                JR.RedURL = "/Master/MineralNameMaster";
            }
            else
            {
                JR.Message = "Some Error Occured, Contact to Admin";
            }
            return Json(JR, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public ActionResult EditMineralNameMaster(string id)
        {
            if (String.IsNullOrEmpty(id))
            {
                return RedirectToAction("Login", "Account");
            }
            try
            {
                int _id = int.Parse(CryptoEngine.Decrypt(id));
                var Info = db.MineralNameMasters.Where(x => x.MineralId == _id).FirstOrDefault();
                if (Info != null)
                {
                    //ViewBag.mimerals = new SelectList(db.MineralTypeMasters.ToList(), "MineralTypeId", "MineralType",Info.MineralType);

                    return View("~/Views/Master/CreateMineralNameMaster.cshtml", new DTO_MineralNameMaster { MineralId = CryptoEngine.Encrypt(Info.MineralId.ToString()), MineralName = Info.MineralName, MineralType=Info.MineralType, MineralCode = Info.MineralCode });
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
        public JsonResult EditMineralNameMaster(DTO_MineralNameMaster model)
        {
            JsonResponse JR = new JsonResponse();
            if (!ModelState.IsValid)
            {
                JR.Data = ModelState.Select(x => x.Value.Errors).Where(y => y.Count > 0).ToList();
                return Json(JR, JsonRequestBehavior.AllowGet);
            }
            if (!String.IsNullOrEmpty(model.MineralId))
            {
                int _id = int.Parse(CryptoEngine.Decrypt(model.MineralId));
                if (db.MineralNameMasters.Where(x => (x.MineralName == model.MineralName || x.MineralCode == model.MineralCode) && x.MineralId != _id).Any())
                {
                    JR.Message = "Mineral Name/Mineral Code " + model.MineralName + "/" + model.MineralCode + " Aready exists !";
                    return Json(JR, JsonRequestBehavior.AllowGet);
                }
                var Info = db.MineralNameMasters.Where(x => x.MineralId == _id).FirstOrDefault();
                if (Info != null)
                {
                    db.DataLogs.Add(new DataLog
                    {
                        CreatedBy = UserManager.GetUserLoginInfo(User.Identity.Name).LoginID,
                        CreatedOn = DateTime.Now,
                        LogInfo = BusinessLogics.ConvertModelToJSONString(Info),
                        URLInfo = "EditMineralNameMaster /POST"
                    });

                    Info.MineralName = model.MineralName;
                    Info.MineralType = model.MineralType;
                    Info.MineralCode = model.MineralCode;
                }
            }
            int res = db.SaveChanges();
            if (res > 0)
            {
                JR.IsSuccess = true;
                JR.Message = "Data Updated Successfully";
                JR.RedURL = "/Master/MineralNameMaster";
            }
            else
            {
                JR.Message = "Some Error Occured, Contact to Admin";
            }
            return Json(JR, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult DeleteMineralNameMaster(string id)
        {
            JsonResponse JR = new JsonResponse();
            if (!String.IsNullOrEmpty(id))
            {
                int _id = int.Parse(CryptoEngine.Decrypt(id));
                var Info = db.MineralNameMasters.Where(x => x.MineralId == _id).FirstOrDefault();
                if (Info != null)
                {
                    db.DataLogs.Add(new DataLog
                    {
                        CreatedBy = UserManager.GetUserLoginInfo(User.Identity.Name).LoginID,
                        CreatedOn = DateTime.Now,
                        LogInfo = BusinessLogics.ConvertModelToJSONString(Info),
                        URLInfo = "DeleteMineralNameMaster /POST"
                    });

                    db.MineralNameMasters.Remove(Info);
                }
            }
            int res = db.SaveChanges();
            if (res > 0)
            {
                JR.IsSuccess = true;
                JR.Message = "Data Deleted Successfully";
                JR.RedURL = "/Master/MineralNameMaster";
            }
            else
            {
                JR.Message = "Some Error Occured, Contact to Admin";
            }
            return Json(JR, JsonRequestBehavior.AllowGet);
        }


        //created by ramdhyan

        public ActionResult MineralTypeMaster()
        {
            ViewBag.LstData = db.MineralTypeMasters.Where(x => x.IsActive == true).ToList();
            return View();
        }
        [HttpGet]
        public ActionResult CreateMineralTypeMaster()
        {
            DTO_MineralTypeMaster model = new DTO_MineralTypeMaster();
            return View(model);
        }
        [HttpPost]
        public JsonResult CreateMineralTypeMaster(DTO_MineralTypeMaster model)
        {
            JsonResponse JR = new JsonResponse();
            if (!ModelState.IsValid)
            {
                JR.Data = ModelState.Select(x => x.Value.Errors).Where(y => y.Count > 0).ToList();
                return Json(JR, JsonRequestBehavior.AllowGet);
            }
            if (db.MineralTypeMasters.Where(x => x.MineralType == model.MineralType ).Any())
            {
                JR.Message = "MineralType " + model.MineralType + " Aready exists !";
                return Json(JR, JsonRequestBehavior.AllowGet);
            }
            db.MineralTypeMasters.Add(new Models.MineralTypeMaster
            {
                IsActive = true,
                MineralType = model.MineralType
            });

            int res = db.SaveChanges();
            if (res > 0)
            {
                JR.IsSuccess = true;
                JR.Message = "Data Saved Successfully";
                JR.RedURL = "/Master/MineralTypeMaster";
            }
            else
            {
                JR.Message = "Some Error Occured, Contact to Admin";
            }
            return Json(JR, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public ActionResult EditMineralTypeMaster(string id)
        {
            if (String.IsNullOrEmpty(id))
            {
                return RedirectToAction("Login", "Account");
            }
            try
            {
                int _id = int.Parse(CryptoEngine.Decrypt(id));
                var Info = db.MineralTypeMasters.Where(x => x.MineralTypeId == _id).FirstOrDefault();
                if (Info != null)
                {
                    return View("~/Views/Master/CreateMineralTypeMaster.cshtml", new DTO_MineralTypeMaster { MineralTypeId = CryptoEngine.Encrypt(Info.MineralTypeId.ToString()), MineralType  = Info.MineralType});
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
        public JsonResult EditMineralTypeMaster(DTO_MineralTypeMaster model)
        {
            JsonResponse JR = new JsonResponse();
            if (!ModelState.IsValid)
            {
                JR.Data = ModelState.Select(x => x.Value.Errors).Where(y => y.Count > 0).ToList();
                return Json(JR, JsonRequestBehavior.AllowGet);
            }
            if (!String.IsNullOrEmpty(model.MineralTypeId))
            {
                int _id = int.Parse(CryptoEngine.Decrypt(model.MineralTypeId));
                if (db.MineralTypeMasters.Where(x => (x.MineralType == model.MineralType ) && x.MineralTypeId != _id).Any())
                {
                    JR.Message = "MineralType " + model.MineralType + " Aready exists !";
                    return Json(JR, JsonRequestBehavior.AllowGet);
                }
                var Info = db.MineralTypeMasters.Where(x => x.MineralTypeId == _id).FirstOrDefault();
                if (Info != null)
                {
                    db.DataLogs.Add(new DataLog
                    {
                        CreatedBy = UserManager.GetUserLoginInfo(User.Identity.Name).LoginID,
                        CreatedOn = DateTime.Now,
                        LogInfo = BusinessLogics.ConvertModelToJSONString(Info),
                        URLInfo = "EditMineralTypeMaster /POST"
                    });

                    Info.MineralType = model.MineralType;
                }
            }
            int res = db.SaveChanges();
            if (res > 0)
            {
                JR.IsSuccess = true;
                JR.Message = "Data Updated Successfully";
                JR.RedURL = "/Master/MineralTypeMaster";
            }
            else
            {
                JR.Message = "Some Error Occured, Contact to Admin";
            }
            return Json(JR, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult DeleteMineralTypeMaster(string id)
        {
            JsonResponse JR = new JsonResponse();
            if (!String.IsNullOrEmpty(id))
            {
                int _id = int.Parse(CryptoEngine.Decrypt(id));
                var Info = db.MineralTypeMasters.Where(x => x.MineralTypeId == _id).FirstOrDefault();
                if (Info != null)
                {
                    db.DataLogs.Add(new DataLog
                    {
                        CreatedBy = UserManager.GetUserLoginInfo(User.Identity.Name).LoginID,
                        CreatedOn = DateTime.Now,
                        LogInfo = BusinessLogics.ConvertModelToJSONString(Info),
                        URLInfo = "DeleteMineralTypeMaster /POST"
                    });

                    db.MineralTypeMasters.Remove(Info);
                }
            }
            int res = db.SaveChanges();
            if (res > 0)
            {
                JR.IsSuccess = true;
                JR.Message = "Data Deleted Successfully";
                JR.RedURL = "/Master/MineralTypeMaster";
            }
            else
            {
                JR.Message = "Some Error Occured, Contact to Admin";
            }
            return Json(JR, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region MenuMaster
        [HttpGet]
        public ActionResult MenuMaster()
        {
            ViewBag.LstData = db.MenuMasters.Where(x => x.IsActive == true).ToList();
            return View();
        }
        [HttpGet]
        public ActionResult CreateMenuMaster()
        {
            DTO_MenuMaster model = new DTO_MenuMaster();
            ViewBag.ParentId = new SelectList(db.MenuMasters.Where(x => x.IsParent == true), "MenuId", "MenuName");
            return View(model);
        }
        [HttpPost]
        public JsonResult CreateMenuMaster(DTO_MenuMaster model)
        {
            JsonResponse JR = new JsonResponse();
            if (!ModelState.IsValid)
            {
                JR.Data = ModelState.Select(x => x.Value.Errors).Where(y => y.Count > 0).ToList();
                return Json(JR, JsonRequestBehavior.AllowGet);
            }
            if (db.MenuMasters.Where(x => x.MenuName == model.MenuName).Any())
            {
                JR.Message = "Menu Name " + model.MenuName + " Already exists !";
                return Json(JR, JsonRequestBehavior.AllowGet);
            }
            db.MenuMasters.Add(new Models.MenuMaster
            {
                IsActive = true,
                IsParent = model.IsParent,
                ParentId = model.ParentId,
                ActionName = model.ActionName,
                ControllerName = model.ControllerName,
                MenuName = model.MenuName,
                CreatedOn = DateTime.Now,
            });

            int res = db.SaveChanges();
            if (res > 0)
            {
                JR.IsSuccess = true;
                JR.Message = "Data Saved Successfully";
                JR.RedURL = "/Master/MenuMaster";
            }
            else
            {
                JR.Message = "Some Error Occured, Contact to Admin";
            }
            return Json(JR, JsonRequestBehavior.AllowGet);
        }
        public ActionResult EditMenuMaster(string id)
        {
            if (String.IsNullOrEmpty(id))
            {
                return RedirectToAction("Login", "Account");
            }
            try
            {
                int _id = int.Parse(CryptoEngine.Decrypt(id));
                var Info = db.MenuMasters.Where(x => x.MenuID == _id).FirstOrDefault();
                if (Info != null)
                {
                    ViewBag.ParentId = new SelectList(db.MenuMasters.Where(x => x.IsParent == true), "MenuId", "MenuName", Info.ParentId);
                    return View("~/Views/Master/CreateMenuMaster.cshtml", new DTO_MenuMaster { MenuID = CryptoEngine.Encrypt(Info.MenuID.ToString()), MenuName = Info.MenuName, ActionName = Info.ActionName, ControllerName = Info.ControllerName });
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
        public JsonResult EditMenuMaster(DTO_MenuMaster model)
        {
            JsonResponse JR = new JsonResponse();
            if (!ModelState.IsValid)
            {
                JR.Data = ModelState.Select(x => x.Value.Errors).Where(y => y.Count > 0).ToList();
                return Json(JR, JsonRequestBehavior.AllowGet);
            }
            if (!String.IsNullOrEmpty(model.MenuID))
            {
                int _id = int.Parse(CryptoEngine.Decrypt(model.MenuID));
                if (db.MenuMasters.Where(x => x.MenuName == model.MenuName && x.MenuID != _id).Any())
                {
                    JR.Message = "Menu Name " + model.MenuName + " Already exists !";
                    return Json(JR, JsonRequestBehavior.AllowGet);
                }
                var Info = db.MenuMasters.Where(x => x.MenuID == _id).FirstOrDefault();
                if (Info != null)
                {
                    db.DataLogs.Add(new DataLog
                    {
                        CreatedBy = UserManager.GetUserLoginInfo(User.Identity.Name).LoginID,
                        CreatedOn = DateTime.Now,
                        LogInfo = BusinessLogics.ConvertModelToJSONString(Info),
                        URLInfo = "EditMenuMaster /POST"
                    });

                    Info.MenuName = model.MenuName;
                    Info.ActionName = model.ActionName;
                    Info.ControllerName = model.ControllerName;
                    Info.IsParent = model.IsParent;
                    Info.ParentId = model.ParentId;
                }
            }
            int res = db.SaveChanges();
            if (res > 0)
            {
                JR.IsSuccess = true;
                JR.Message = "Data Updated Successfully";
                JR.RedURL = "/Master/MenuMaster";
            }
            else
            {
                JR.Message = "Some Error Occured, Contact to Admin";
            }
            return Json(JR, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult DeleteMenuMaster(string id)
        {
            JsonResponse JR = new JsonResponse();
            if (!String.IsNullOrEmpty(id))
            {
                int _id = int.Parse(CryptoEngine.Decrypt(id));
                var Info = db.MenuMasters.Where(x => x.MenuID == _id).FirstOrDefault();
                if (Info != null)
                {
                    db.DataLogs.Add(new DataLog
                    {
                        CreatedBy = UserManager.GetUserLoginInfo(User.Identity.Name).LoginID,
                        CreatedOn = DateTime.Now,
                        LogInfo = BusinessLogics.ConvertModelToJSONString(Info),
                        URLInfo = "DeleteMenuMaster /POST"
                    });

                    db.MenuMasters.Remove(Info);
                }
            }
            int res = db.SaveChanges();
            if (res > 0)
            {
                JR.IsSuccess = true;
                JR.Message = "Data Deleted Successfully";
                JR.RedURL = "/Master/MenuMaster";
            }
            else
            {
                JR.Message = "Some Error Occured, Contact to Admin";
            }
            return Json(JR, JsonRequestBehavior.AllowGet);
        }


        public string ManageAC_PC_ULBMapping(int ACID,int PCID,int ULBID,bool IsAdd)
        {
            string Msg = "";
            try
            {
                if (IsAdd)
                {
                    //var res=db.AC
                }
            }
            catch (Exception Ex)
            {
                Msg = "Expp:: " + Ex.Message;
            }
            return Msg;
        }

        #endregion
        #region RoleMappung
        [HttpGet]
        public ActionResult RoleMappingMaster()
        {
            return View();
        }
        [HttpPost]
        public JsonResult RoleMappingMaster(FormCollection frm)
        {
            JsonResponse JR = new JsonResponse();
            int UserId = Convert.ToInt32(CryptoEngine.Decrypt(frm["RoleID"]));
            using (var transaction = db.Database.BeginTransaction())
            {
                try
                {
                    var Info = db.RoleMenuMappings.Where(x => x.UserRoleID == UserId).ToList();
                    if (Info != null)
                    {
                        Info.ForEach(x => db.RoleMenuMappings.Remove(x));
                    }

                    foreach (string key in frm.AllKeys)
                    {
                        if (key.Contains("ChildMenuID_"))
                        {
                            string[] temp = key.Split('_');
                            string row = temp[1];
                            //long? MenuId = null;

                            RoleMenuMapping rmm = new RoleMenuMapping()
                            {
                                UserRoleID = UserId,
                                MenuID = Convert.ToInt64(frm["ChildMenuID_" + row]),
                                CanDelete = (frm["CanDelete_" + row] ?? "").Equals("true", StringComparison.CurrentCultureIgnoreCase),
                                CanUpdate = (frm["CanEdit_" + row] ?? "").Equals("true", StringComparison.CurrentCultureIgnoreCase),
                                CanInsert = (frm["CanAdd_" + row] ?? "").Equals("true", StringComparison.CurrentCultureIgnoreCase),
                                CanView = (frm["CanView_" + row] ?? "").Equals("true", StringComparison.CurrentCultureIgnoreCase),
                                CreatedOn = DateTime.Now,
                                CreatedBy = UserManager.GetUserLoginInfo(User.Identity.Name).LoginID,
                                IsActive = true
                            };
                            db.RoleMenuMappings.Add(rmm);
                            int res = db.SaveChanges();
                            if (res > 0)
                            {
                                JR.IsSuccess = true;
                                JR.Message = "Permission change successfuly";
                                JR.RedURL = "/Master/RoleMappingMaster";
                            }
                            else
                            {
                                JR.Message = "Some Error Occured, Contact to Admin";
                            }


                        }
                    }
                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    JR.Message = "Some Error Occured, Contact to Admin";
                    JR.RedURL = "/Master/RoleMappingMaster";
                    transaction.Rollback();
                }
            }
            return Json(JR, JsonRequestBehavior.AllowGet);
        }

        #endregion

        public ActionResult StateMasterTest()
        {
            DTO_DemoStateId model = new DTO_DemoStateId();
            ViewBag.StateID = new SelectList(db.StateMasters.Where(x => x.IsActive == true), "StateID", "StateName", null);
            //ViewBag.DistID = new SelectList(db.DistrictMasters.Where(x => x.IsActive == true), "DistrictId", "DistrictName", null);
            //ViewBag.VillageID = new SelectList(db.VillageMasters.Where(x => x.IsActive == true), "VillageId", "VillageNameInEnglish", null);
            //ViewBag.TehsilID = new SelectList(db.TehsilMasters.Where(x => x.IsActive == true), "TehsilId", "TehsilName", null);
            //ViewBag.MinralID = new SelectList(db.MineralNameMasters.Where(x => x.IsActive == true), "MineralId", "MineralName", null);

           

            return View(model);
        }
        [HttpPost]
        public JsonResult StateMasterTest(DTO_DemoStateId model)
        {
            //db.DemoStates.Add(model);
            //db.SaveChanges();

            JsonResponse JR = new JsonResponse();

            if (!ModelState.IsValid)
            {
                JR.Data = ModelState.Select(x => x.Value.Errors).Where(y => y.Count > 0).ToList();
                return Json(JR, JsonRequestBehavior.AllowGet);
            }

            db.DemoStates.Add(new Models.DemoState
            {

                StateID = model.StateID
            });
            int res = db.SaveChanges();
            if (res > 0)
            {
                JR.IsSuccess = true;
                JR.Message = "Data Saved Successfully";
                JR.RedURL = "/Master/ListRecorddata";
            }
            else
            {
                JR.Message = "Some Error Occured, Contact to Admin";
            }
            return Json(JR, JsonRequestBehavior.AllowGet);
            //return View();
        }

        public ActionResult ListRecorddata(int? stateid)
        {
            var LstData = (from lm in db.DemoStates

                           join sm in db.StateMasters on lm.StateID equals sm.StateID
                           where 
                            lm.StateID == (stateid == null ? lm.StateID : stateid)


                           select new DTO_DemoStateId
                           {

                               Name = sm.StateName,
                               Id = lm.Id.ToString()


                           }).ToList();
                          ViewBag.LstData = LstData;

            return View();
        }
        public ActionResult CreateDistrcyNameMaster()
        {
            DTO_District model = new DTO_District();
            ViewBag.StateID = new SelectList(db.StateMasters.Where(x => x.IsActive == true), "StateID", "StateName", null);

            return View(model);
        }
        [HttpPost]

        public JsonResult CreateDistrcyNameMaster(DTO_District model)
        {
            JsonResponse JR = new JsonResponse();

            if (!ModelState.IsValid)
            {
                JR.Data = ModelState.Select(x => x.Value.Errors).Where(y => y.Count > 0).ToList();
                return Json(JR, JsonRequestBehavior.AllowGet);
            }

            db.DistrictMasters.Add(new Models.DistrictMaster
            {

                StateID = model.StateID,
                DistrictName = model.DistrictName,
                DistrictCode = model.DistrictCode,
                IsActive =true


            });
            int res = db.SaveChanges();
            if (res > 0)
            {
                JR.IsSuccess = true;
                JR.Message = "Data Saved Successfully";
                JR.RedURL = "/Master/CreateDistrcyNameMaster";
            }
            else
            {
                JR.Message = "Some Error Occured, Contact to Admin";
            }
            return Json(JR, JsonRequestBehavior.AllowGet);
            //return View();
        }

        public ActionResult   DistrictNameMaster(int? stateid)
        {
            var LstData = (from d in db.DistrictMasters
                           join st in db.StateMasters on d.StateID equals
                                st.StateID
                           where d.IsActive == true
                            && d.StateID == (stateid == null ? d.StateID : stateid)
                           select new DTO_District
                           {
                               DistrictName = d.DistrictName,
                               DistrictId = d.DistrictId.ToString(),
                               DistrictCode = d.DistrictCode,
                               StateID = d.StateID,
                               StateName = st.StateName

                           }
                         ).ToList();

            ViewBag.LstData = LstData;

            return View();
        }
        public ActionResult EditDistrictNameMaster(string id)
        {
            if (String.IsNullOrEmpty(id))
            {
                return RedirectToAction("Login", "Account");
            }
            int _id = int.Parse(CryptoEngine.Decrypt(id));

            try
            {
                var Info = db.DistrictMasters.Where(x => x.DistrictId == _id).FirstOrDefault();
                if (Info != null)

                {
                    int? CDist = null;
                    if (UserManager.GetUserLoginInfo(User.Identity.Name).RoleID == 2)
                    {
                        CDist = UserManager.GetUserLoginInfo(User.Identity.Name).DistID;
                    }

                    ViewBag.StateID = new SelectList(db.StateMasters.Where(x => x.IsActive == true), "StateID", "StateName", Info.StateID);


                    return View("~/Views/Master/CreateDistrcyNameMaster.cshtml", new DTO_District
                    {
                        DistrictId = CryptoEngine.Encrypt(Info.DistrictId.ToString()),
                        DistrictName=Info.DistrictName,
                        DistrictCode=Info.DistrictCode,
                        StateID=Info.StateID
                 
                    });
                }

            }
            catch(Exception ex)
            {
}

            return View();
        }

        [HttpPost]
        public ActionResult EditDistrictNameMaster(DTO_District model)
        {

            JsonResponse JR = new JsonResponse();

            if (!ModelState.IsValid)
            {
                JR.Data = ModelState.Select(x => x.Value.Errors).Where(y => y.Count > 0).ToList();
                return Json(JR, JsonRequestBehavior.AllowGet);
            }

            if (!String.IsNullOrEmpty(model.DistrictId))
            {
                int _id = int.Parse(CryptoEngine.Decrypt(model.DistrictId));

                var Info = db.DistrictMasters.Where(x => x.DistrictId == _id).FirstOrDefault();

                if(Info!=null)
                {
                    db.DataLogs.Add(new DataLog
                    {
                        CreatedBy = UserManager.GetUserLoginInfo(User.Identity.Name).LoginID,
                        CreatedOn = DateTime.Now,
                        LogInfo = BusinessLogics.ConvertModelToJSONString(Info),
                        URLInfo = "EditDistrictNameMaster/POST"
                    });
                    Info.StateID = model.StateID;
                    Info.DistrictName = model.DistrictName;
                    Info.DistrictCode = model.DistrictCode;
                    Info.IsActive = true;

                }
            }
           
                int res = db.SaveChanges();
            if (res > 0)
            {
                JR.IsSuccess = true;
                JR.Message = "Data Updated Successfully";
                JR.RedURL = "/Master/CreateDistrcyNameMaster";
            }
            else
            {
                JR.Message = "Some Error Occured, Contact to Admin";
            }
            
            return Json(JR, JsonRequestBehavior.AllowGet);
        }
       
        [HttpPost]
        public JsonResult DeleteDistrictMaster(string id)
        {
            JsonResponse JR = new JsonResponse();
            if (!String.IsNullOrEmpty(id))
            {
                int _id = int.Parse(CryptoEngine.Decrypt(id));
                var Info = db.DistrictMasters.Where(x => x.DistrictId == _id).FirstOrDefault();
                if (Info != null)
                {
                    db.DataLogs.Add(new DataLog
                    {
                        CreatedBy = UserManager.GetUserLoginInfo(User.Identity.Name).LoginID,
                        CreatedOn = DateTime.Now,
                        LogInfo = BusinessLogics.ConvertModelToJSONString(Info),
                        URLInfo = "DeleteDistrictMaster/POST"
                    });
                   

                    db.DistrictMasters.Remove(Info);
                }
            }
            int res = db.SaveChanges();
            if (res > 0)
            {
                JR.IsSuccess = true;
                JR.Message = "Data Deleted Successfully";
                JR.RedURL = "/Master/DistrictNameMaster";
            }
            else
            {
                JR.Message = "Some Error Occured, Contact to Admin";
            }
            return Json(JR, JsonRequestBehavior.AllowGet);
        }
        public ActionResult CreateCommitteeDesignation()
        {
             DTO_CommitteeDesignation model = new DTO_CommitteeDesignation();

            return View(model);
        }

        [HttpPost]
        public JsonResult CreateCommitteeDesignation(DTO_CommitteeDesignation model)
        {
            JsonResponse JR = new JsonResponse();

            if (!ModelState.IsValid)
            {
                JR.Data = ModelState.Select(x => x.Value.Errors).Where(y => y.Count > 0).ToList();
                return Json(JR, JsonRequestBehavior.AllowGet);
            }

            db.CommitteeDesignationMasters.Add(new Models.CommitteeDesignationMaster
            {

                 CommitteeDesignationName = model.CommitteeDesignationName,
                IsActive = true


            });
            int res = db.SaveChanges();
            if (res > 0)
            {
                JR.IsSuccess = true;
                JR.Message = "Data Saved Successfully";
                JR.RedURL = "/Master/CommitteeDesignationMaster";
            }
            else
            {
                JR.Message = "Some Error Occured, Contact to Admin";
            }
            return Json(JR, JsonRequestBehavior.AllowGet);
            //return View();
        }

        public ActionResult CommitteeDesignationMaster()
        {
            var LstData = (from d in db.CommitteeDesignationMasters
                          
                           where d.IsActive == true
                         
                           select new DTO_CommitteeDesignation
                           {
                               CommitteeDesignationName = d.CommitteeDesignationName,
                               CommitteeDesignationId = d.CommitteeDesignationId.ToString(),
                         
                           }
                         ).ToList();

            ViewBag.LstData = LstData;


            return View();
        }

        public ActionResult EditCommitteeDesignationMaster(string id)
        {
            if (String.IsNullOrEmpty(id))
            {
                return RedirectToAction("Login", "Account");
            }


            int _id = int.Parse(CryptoEngine.Decrypt(id));

            try
            {
                var Info = db.CommitteeDesignationMasters.Where(x => x.CommitteeDesignationId == _id).FirstOrDefault();
                if (Info != null)

                {
                    int? CDist = null;
                    if (UserManager.GetUserLoginInfo(User.Identity.Name).RoleID == 2)
                    {
                        CDist = UserManager.GetUserLoginInfo(User.Identity.Name).DistID;
                    }



                    return View("~/Views/Master/CreateCommitteeDesignation.cshtml", new DTO_CommitteeDesignation
                    {
                        CommitteeDesignationId= CryptoEngine.Encrypt(Info.CommitteeDesignationId.ToString()),
                        CommitteeDesignationName = Info.CommitteeDesignationName,
                     
                    });
                }

            }
            catch (Exception ex)
            {
            }

            return View();
        }
        [HttpPost]
        public ActionResult EditCommitteeDesignationMaster(DTO_CommitteeDesignation model)
        {

            JsonResponse JR = new JsonResponse();

            if (!ModelState.IsValid)
            {
               
                JR.Data = ModelState.Select(x => x.Value.Errors).Where(y => y.Count > 0).ToList();
                return Json(JR, JsonRequestBehavior.AllowGet);
            }
            if (!String.IsNullOrEmpty(model.CommitteeDesignationId))
            {
                int _id = int.Parse(CryptoEngine.Decrypt(model.CommitteeDesignationId));

                var Info = db.CommitteeDesignationMasters.Where(x => x.CommitteeDesignationId == _id).FirstOrDefault();

                if (Info != null)
                {

                    db.DataLogs.Add(new DataLog
                    {
                        CreatedBy = UserManager.GetUserLoginInfo(User.Identity.Name).LoginID,
                        CreatedOn = DateTime.Now,
                        LogInfo = BusinessLogics.ConvertModelToJSONString(Info),
                        URLInfo = "EditCommitteeDesignationMaster/POST"
                    });

                    Info.CommitteeDesignationName = model.CommitteeDesignationName;
                    
                    Info.IsActive = true;


                }


            }
           
            int res = db.SaveChanges();
            if (res > 0)
            {
                JR.IsSuccess = true;
                JR.Message = "Data Updated Successfully";
                JR.RedURL = "/Master/CommitteeDesignationMaster";
            }
            else
            {
                JR.Message = "Some Error Occured, Contact to Admin";
            }
            return Json(JR, JsonRequestBehavior.AllowGet);

        }

        [HttpPost]
        public JsonResult DeleteCommitteeDesignationMaster(string id)
        {
            JsonResponse JR = new JsonResponse();
            if (!String.IsNullOrEmpty(id))
            {
                int _id = int.Parse(CryptoEngine.Decrypt(id));
                var Info = db.CommitteeDesignationMasters.Where(x => x.CommitteeDesignationId == _id).FirstOrDefault();
                if (Info != null)
                {
                    db.DataLogs.Add(new DataLog
                    {
                        CreatedBy = UserManager.GetUserLoginInfo(User.Identity.Name).LoginID,
                        CreatedOn = DateTime.Now,
                        LogInfo = BusinessLogics.ConvertModelToJSONString(Info),
                        URLInfo = "DeleteCommitteeDesignationMaster/POST"
                    });

                    db.CommitteeDesignationMasters.Remove(Info);
                }
            }
            int res = db.SaveChanges();
            if (res > 0)
            {
                JR.IsSuccess = true;
                JR.Message = "Data Deleted Successfully";
                JR.RedURL = "/Master/CommitteeDesignationMaster";
            }
            else
            {
                JR.Message = "Some Error Occured, Contact to Admin";
            }
            return Json(JR, JsonRequestBehavior.AllowGet);
        }
    }
}