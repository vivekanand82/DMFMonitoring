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
    public class DMFFundCollectionController : Controller
    {
        private dfm_dbEntities db = new dfm_dbEntities();
        // GET: DMFFundCollection
        //public ActionResult LesseeOpeningDMFAmt()
        //{
        //    var data = (from lda in db.LesseeOpeningDMFAmts
        //                join lm in db.LesseeMasters on lda.LesseeId equals lm.LesseeID
        //                where lda.IsActive == true
        //                select new DTO_LesseeOpeningDMFAmt
        //                {
        //                    OpeningDMFAmtId = lda.OpeningDMFAmtId.ToString(),
        //                    DMFOpeningAmt = lda.DMFOpeningAmt,
        //                    EffectiveDate = lda.EffectiveDate,
        //                    LeseeName = lm.LesseeName
        //                }).ToList();
        //    ViewBag.LstData = data;
        //    return View();
        //}
        public ActionResult LesseeOpeningDMFAmt()
        {
            int? DistID = null;
            if (UserManager.GetUserLoginInfo(User.Identity.Name).RoleID == 2)
            {
                DistID = UserManager.GetUserLoginInfo(User.Identity.Name).DistID;
            }
                var data = (from lda in db.LesseeOpeningDMFAmts
                        join dm in db.DistrictMasters on lda.DistrictId equals dm.DistrictId
                        join m in db.MineralNameMasters  on lda.MineralId equals m.MineralId
                        join mm in db.MineralTypeMasters on m.MineralTypeId equals mm.MineralTypeId
                        where lda.IsActive == true && dm.DistrictId == (DistID == null ? dm.DistrictId : DistID)
                            select new DTO_LesseeOpeningDMFAmt
                        {
                            OpeningDMFAmtId = lda.OpeningDMFAmtId.ToString(),
                            DMFOpeningAmt = lda.DMFOpeningAmt,
                            EffectiveDate = lda.EffectiveDate,
                            DistrictName = dm.DistrictName,
                            MineralId=m.MineralId,
                            MineralName=m.MineralName,
                                MineralType=mm.MineralType
                            }).ToList();
            ViewBag.LstData = data;
            return View();
        }
        //[HttpGet]
        //public ActionResult CreateLesseeOpeningDMFAmt()
        //{
        //    DTO_LesseeOpeningDMFAmt model = new DTO_LesseeOpeningDMFAmt();
        //    ViewBag.LesseeId = new SelectList(db.LesseeMasters.Where(x => x.IsActive == true).ToList().Select(x => new { ID = x.LesseeID, Text = x.LesseeName + " / " + x.LeaseID + " / " + x.Areacode }), "ID", "Text");
        //    ViewBag.MineralId = new SelectList(db.MineralNameMasters.Where(x => x.IsActive == true).ToList().Select(x => new { ID = x.MineralId, Text = x.MineralName}), "ID", "Text");
        //    return View(model);
        //}
        [HttpGet]
        public ActionResult CreateLesseeOpeningDMFAmt()
        {
            DTO_LesseeOpeningDMFAmt model = new DTO_LesseeOpeningDMFAmt();
            ViewBag.DistrictId = new SelectList(db.DistrictMasters.Where(x => x.IsActive == true).ToList().Select(x => new { ID = x.DistrictId, Text = x.DistrictName }), "ID", "Text");
            ViewBag.LesseeId = new SelectList(db.LesseeMasters.Where(x => x.IsActive == true).ToList().Select(x => new { ID = x.LesseeID, Text = x.LesseeName + " / " + x.LeaseID + " / " + x.Areacode }), "ID", "Text");
            ViewBag.MineralId = new SelectList(db.MineralNameMasters.Where(x => x.IsActive == true).ToList().Select(x => new { ID = x.MineralId, Text = x.MineralName }), "ID", "Text");
            return View(model);
        }
        [HttpPost]
        public JsonResult CreateLesseeOpeningDMFAmt(DTO_LesseeOpeningDMFAmt model)
        {
            JsonResponse JR = new JsonResponse();
            if (!ModelState.IsValid)
            {
                JR.Data = ModelState.Select(x => x.Value.Errors).Where(y => y.Count > 0).ToList();
                return Json(JR, JsonRequestBehavior.AllowGet);
            }
            //if (db.LesseeOpeningDMFAmts.Where(x => x.LesseeId == model.LesseeId).Any())
            //{
            //    JR.Message = "Lessee " + model.LesseeId + " Aready exists !";
            //    return Json(JR, JsonRequestBehavior.AllowGet);
            //}
            var district = db.DistrictMasters.Where(x => x.DistrictId == model.DistrictId).FirstOrDefault().DistrictName;
            if (db.LesseeOpeningDMFAmts.Where(x => x.DistrictId == model.DistrictId && x.MineralId==model.MineralId).Any())
            {
                JR.Message = " Opening Amount Aready exists for  District " + district + " !!";
                return Json(JR, JsonRequestBehavior.AllowGet);
            }
            db.LesseeOpeningDMFAmts.Add(new LesseeOpeningDMFAmt
            {
                LesseeId = model.LesseeId,
                MineralId=model.MineralId,
                DistrictId=model.DistrictId,
                CreatedDate = DateTime.Now,
                IsActive = true,
                DMFOpeningAmt = model.DMFOpeningAmt,
                EffectiveDate = model.EffectiveDate,
                CreatedBy = UserManager.GetUserLoginInfo(User.Identity.Name).LoginID.ToString()
            });

            int res = db.SaveChanges();
            if (res > 0)
            {
                JR.IsSuccess = true;
                JR.Message = "Data Saved Successfully";
                JR.RedURL = "/DMFFundCollection/LesseeOpeningDMFAmt";
            }
            else
            {
                JR.Message = "Some Error Occured, Contact to Admin";
            }
            return Json(JR, JsonRequestBehavior.AllowGet);
        }
        public ActionResult EditLesseeOpeningDMFAmt(string id)
        {
            if (String.IsNullOrEmpty(id))
            {
                return RedirectToAction("Login", "Account");
            }
            try
            {
                int _id = int.Parse(CryptoEngine.Decrypt(id));
                var Info = db.LesseeOpeningDMFAmts.Where(x => x.OpeningDMFAmtId == _id).FirstOrDefault();
                if (Info != null)
                {
                    ViewBag.DistrictId = new SelectList(db.DistrictMasters.Where(x => x.IsActive == true).ToList().Select(x => new { ID = x.DistrictId, Text = x.DistrictName }), "ID", "Text");
                    ViewBag.LesseeId = new SelectList(db.LesseeMasters.Where(x => x.IsActive == true).ToList().Select(x => new { ID = x.LesseeID, Text = x.LesseeName + " / " + x.LeaseID + " / " + x.Areacode }), "ID", "Text", Info.LesseeId);
                    ViewBag.MineralId = new SelectList(db.MineralNameMasters.Where(x => x.IsActive == true).ToList().Select(x => new { ID = x.MineralId, Text = x.MineralName }), "ID", "Text");
                    return View("~/Views/DMFFundCollection/CreateLesseeOpeningDMFAmt.cshtml", new DTO_LesseeOpeningDMFAmt { OpeningDMFAmtId = CryptoEngine.Encrypt(Info.OpeningDMFAmtId.ToString()), DMFOpeningAmt = Info.DMFOpeningAmt, EffectiveDate = Info.EffectiveDate, LesseeId = Convert.ToInt64(Info.LesseeId),MineralId=Convert.ToInt32(Info.MineralId), DistrictId = Convert.ToInt32(Info.DistrictId) });
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
        public JsonResult EditLesseeOpeningDMFAmt(DTO_LesseeOpeningDMFAmt model)
        {
            JsonResponse JR = new JsonResponse();
            if (!ModelState.IsValid)
            {
                JR.Data = ModelState.Select(x => x.Value.Errors).Where(y => y.Count > 0).ToList();
                return Json(JR, JsonRequestBehavior.AllowGet);
            }
            if (!String.IsNullOrEmpty(model.OpeningDMFAmtId))
            {
                int _id = int.Parse(CryptoEngine.Decrypt(model.OpeningDMFAmtId));
                //if (db.LesseeOpeningDMFAmts.Where(x => x.LesseeId == model.LesseeId && x.OpeningDMFAmtId != _id).Any())
                //{
                //    JR.Message = "Lessee " + model.LesseeId + " Already exists !";
                //    return Json(JR, JsonRequestBehavior.AllowGet);
                //}
                if (db.LesseeOpeningDMFAmts.Where(x => x.DistrictId == model.DistrictId && x.MineralId==model.MineralId  && x.OpeningDMFAmtId != _id).Any())
                {
                    //JR.Message = "District " + model.DistrictId + " Already exists !";
                    JR.Message = "Opening Amount Already Exists for this Mineral !!";
                    return Json(JR, JsonRequestBehavior.AllowGet);
                }
                var Info = db.LesseeOpeningDMFAmts.Where(x => x.OpeningDMFAmtId == _id).FirstOrDefault();
                if (Info != null)
                {
                    Info.LesseeId = model.LesseeId;
                    Info.DistrictId = model.DistrictId;
                    Info.MineralId = model.MineralId;
                    Info.DMFOpeningAmt = model.DMFOpeningAmt;
                    Info.EffectiveDate = model.EffectiveDate;
                }
            }
            int res = db.SaveChanges();
            if (res > 0)
            {
                JR.IsSuccess = true;
                JR.Message = "Data Updated Successfully";
                JR.RedURL = "/DMFFundCollection/LesseeOpeningDMFAmt";
            }
            else
            {
                JR.Message = "Some Error Occured, Contact to Admin";
            }
            return Json(JR, JsonRequestBehavior.AllowGet);
        }
        public JsonResult DeleteLesseeOpeningDMFAmt(string id)
        {
            JsonResponse JR = new JsonResponse();
            if (!String.IsNullOrEmpty(id))
            {
                int _id = int.Parse(CryptoEngine.Decrypt(id));
                var Info = db.LesseeOpeningDMFAmts.Where(x => x.OpeningDMFAmtId == _id).FirstOrDefault();
                if (Info != null)
                {
                    db.LesseeOpeningDMFAmts.Remove(Info);
                }
            }
            int res = db.SaveChanges();
            if (res > 0)
            {
                JR.IsSuccess = true;
                JR.Message = "Data Deleted Successfully";
                JR.RedURL = "/DMFFundCollection/DMFFundCollection";
            }
            else
            {
                JR.Message = "Some Error Occured, Contact to Admin";
            }
            return Json(JR, JsonRequestBehavior.AllowGet);
        }
        public ActionResult RoyaltyCollection()
        {
            var data = (from rc in db.RoyaltyCollections
                        join lm in db.LesseeMasters on rc.LesseeId equals lm.LesseeID
                        join ym in db.YearMasters on rc.YearId equals ym.YearId
                        join mm in db.MonthMasters on rc.MonthId equals mm.MonthId
                        where rc.IsActive == true
                        select new DTO_RoyaltyCollection
                        {
                           ChallanDate=rc.ChallanDate,
                           ChallanNo=rc.ChallanNo,
                           MonthName=mm.MonthName,
                           YearName=ym.YearName,
                           DMFAmt=rc.DMFAmt,
                           RoyaltyAmt=rc.RoyaltyAmt,
                           ChallanDoc=rc.ChallanDoc,
                           RoyaltyCollectionId=rc.RoyaltyCollectionId.ToString(),
                           LeseeName=lm.LesseeName
                        }).ToList();
            ViewBag.LstData = data;
            return View();
         }
        public ActionResult CreateRoyaltyCollection()
        {
            ViewBag.MonthId = new SelectList(db.MonthMasters.Where(x => x.IsActive == true), "MonthId", "MonthName");
            ViewBag.YearId = new SelectList(db.YearMasters.Where(x => x.IsActive == true), "YearId", "YearName");
            ViewBag.LesseeId = new SelectList(db.LesseeMasters.Where(x => x.IsActive == true).ToList().Select(x => new { ID = x.LesseeID, Text = x.LesseeName + " / " + x.LeaseID + " / " + x.Areacode }), "ID", "Text");
            DTO_RoyaltyCollection model = new DTO_RoyaltyCollection();
            return View(model);
        }
        [HttpPost]
        public JsonResult CreateRoyaltyCollection(DTO_RoyaltyCollection model)
        {
            JsonResponse JR = new JsonResponse();
            if (!ModelState.IsValid)
            {
                JR.Data = ModelState.Select(x => x.Value.Errors).Where(y => y.Count > 0).ToList();
                return Json(JR, JsonRequestBehavior.AllowGet);
            }
            if (db.RoyaltyCollections.Where(x => x.ChallanNo == model.ChallanNo).Any())
            {
                JR.Message = "Challan Number " + model.ChallanNo + " Already exists !";
                return Json(JR, JsonRequestBehavior.AllowGet);
            }
            if (!String.IsNullOrEmpty(model.ChallanDoc))
            {
                model.ChallanDoc = BusinessLogics.UploadFileDMF(model.ChallanDoc);
                if (model.ChallanDoc.Contains("Expp::"))
                {
                    JR.Message = model.ChallanDoc;
                    return Json(JR, JsonRequestBehavior.AllowGet);
                }
            }
            db.RoyaltyCollections.Add(new RoyaltyCollection
            {
                LesseeId = model.LesseeId,
                CreatedDate = DateTime.Now,
                IsActive = true,
                ChallanDate = model.ChallanDate,
                ChallanDoc = model.ChallanDoc,
                ChallanNo = model.ChallanNo.Trim(),
                DMFAmt = model.DMFAmt,
                RoyaltyAmt = model.RoyaltyAmt,
                MonthId = model.MonthId,
                YearId = model.YearId,
                CreatedBy = UserManager.GetUserLoginInfo(User.Identity.Name).LoginID.ToString()
            });

            int res = db.SaveChanges();
            if (res > 0)
            {
                JR.IsSuccess = true;
                JR.Message = "Data Saved Successfully";
                JR.RedURL = "/DMFFundCollection/RoyaltyCollection";
            }
            else
            {
                JR.Message = "Some Error Occured, Contact to Admin";
            }
            return Json(JR, JsonRequestBehavior.AllowGet);
        }
        public ActionResult EditRoyaltyCollection(string id)
        {
            if (String.IsNullOrEmpty(id))
            {
                return RedirectToAction("Login", "Account");
            }
            try
            {
                int _id = int.Parse(CryptoEngine.Decrypt(id));
                var Info = db.RoyaltyCollections.Where(x => x.RoyaltyCollectionId == _id).FirstOrDefault();
                if (Info != null)
                {
                    ViewBag.LesseeId = new SelectList(db.LesseeMasters.Where(x => x.IsActive == true).ToList().Select(x => new { ID = x.LesseeID, Text = x.LesseeName + " / " + x.LeaseID + " / " + x.Areacode }), "ID", "Text", Info.LesseeId);
                    ViewBag.MonthId = new SelectList(db.MonthMasters.Where(x => x.IsActive == true), "MonthId", "MonthName",Info.MonthId);
                    ViewBag.YearId = new SelectList(db.YearMasters.Where(x => x.IsActive == true), "YearId", "YearName",Info.YearId);

                    return View("~/Views/DMFFundCollection/CreateRoyaltyCollection.cshtml", new DTO_RoyaltyCollection { RoyaltyCollectionId = CryptoEngine.Encrypt(Info.RoyaltyCollectionId.ToString()), ChallanDate = Info.ChallanDate, ChallanNo = Info.ChallanNo, DMFAmt = Info.DMFAmt, RoyaltyAmt = Info.RoyaltyAmt, LesseeId = Info.LesseeId, YearId = Info.YearId, MonthId = Info.MonthId,ChallanDoc=Info.ChallanDoc });
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
        public JsonResult EditRoyaltyCollection(DTO_RoyaltyCollection model)
        {
            JsonResponse JR = new JsonResponse();
            if (!ModelState.IsValid)
            {
                JR.Data = ModelState.Select(x => x.Value.Errors).Where(y => y.Count > 0).ToList();
                return Json(JR, JsonRequestBehavior.AllowGet);
            }
            if (!String.IsNullOrEmpty(model.RoyaltyCollectionId))
            {
                int _id = int.Parse(CryptoEngine.Decrypt(model.RoyaltyCollectionId));
                if (db.RoyaltyCollections.Where(x => x.ChallanNo == model.ChallanNo && x.RoyaltyCollectionId != _id).Any())
                {
                    JR.Message = "Challan Number " + model.ChallanNo + " Already exists !";
                    return Json(JR, JsonRequestBehavior.AllowGet);
                }
                if (!String.IsNullOrEmpty(model.ChallanDoc))
                {
                    if (model.ChallanDoc != "prev")
                    {
                        model.ChallanDoc = BusinessLogics.UploadFileDMF(model.ChallanDoc);
                        if (model.ChallanDoc.Contains("Expp::"))
                        {
                            JR.Message = model.ChallanDoc;
                            return Json(JR, JsonRequestBehavior.AllowGet);
                        }
                    }
                }
                var Info = db.RoyaltyCollections.Where(x => x.RoyaltyCollectionId == _id).FirstOrDefault();
                if (Info != null)
                {
                    Info.LesseeId = model.LesseeId;
                    Info.ChallanDate = model.ChallanDate;
                    Info.ChallanDoc = (model.ChallanDoc != null && model.ChallanDoc == "prev") ? Info.ChallanDoc : model.ChallanDoc;
                    Info.ChallanNo = model.ChallanNo.Trim();
                    Info.DMFAmt = model.DMFAmt;
                    Info.RoyaltyAmt = model.RoyaltyAmt;
                    Info.MonthId = model.MonthId;
                    Info.YearId = model.YearId;
                }
            }
            int res = db.SaveChanges();
            if (res > 0)
            {
                JR.IsSuccess = true;
                JR.Message = "Data Updated Successfully";
                JR.RedURL = "/DMFFundCollection/RoyaltyCollection";
            }
            else
            {
                JR.Message = "Some Error Occured, Contact to Admin";
            }
            return Json(JR, JsonRequestBehavior.AllowGet);
        }
        public JsonResult DeleteRoyaltyCollection(string id)
        {
            JsonResponse JR = new JsonResponse();
            if (!String.IsNullOrEmpty(id))
            {
                int _id = int.Parse(CryptoEngine.Decrypt(id));
                var Info = db.RoyaltyCollections.Where(x => x.RoyaltyCollectionId == _id).FirstOrDefault();
                if (Info != null)
                {
                    db.RoyaltyCollections.Remove(Info);
                }
            }
            int res = db.SaveChanges();
            if (res > 0)
            {
                JR.IsSuccess = true;
                JR.Message = "Data Deleted Successfully";
                JR.RedURL = "/DMFFundCollection/RoyaltyCollection";
            }
            else
            {
                JR.Message = "Some Error Occured, Contact to Admin";
            }
            return Json(JR, JsonRequestBehavior.AllowGet);
        }
        public ActionResult FundCollection()
        {
            if (UserManager.GetUserLoginInfo(User.Identity.Name).RoleID == 3)
            {
                var loginid = UserManager.GetUserLoginInfo(User.Identity.Name).UserName;
                var data = (from dmf in db.DMFFundCollections
                            join lm in db.LesseeMasters on dmf.LesseeId equals lm.LesseeID
                            join ym in db.YearMasters on dmf.YearId equals ym.YearId
                            join mm in db.MonthMasters on dmf.MonthId equals mm.MonthId
                            where lm.EmailID == loginid && dmf.IsActive == true
                            select new DTO_DMFFundCOllection
                            {
                                ChallanDate = dmf.ChallanDate,
                                ChallanNo = dmf.ChallanNo,
                                MonthName = mm.MonthName,
                                YearName = ym.YearName,
                                DMFAmt = dmf.DMFAmt,
                                RoyaltyAmt = dmf.RoyaltyAmt,
                                ChallanDoc = dmf.ChallanDoc,
                                FundCollectionId = dmf.FundCollectionId.ToString(),
                                RemainingDMFAmt = dmf.RemainingDMFAmt,
                                DepositedDMFAmt = dmf.DepositedDMFAmt,
                                LeseeName = lm.LesseeName
                            }).ToList();
                ViewBag.LstData = data;

            }
            else

            {
                var data = (from dmf in db.DMFFundCollections
                            join lm in db.LesseeMasters on dmf.LesseeId equals lm.LesseeID
                            join ym in db.YearMasters on dmf.YearId equals ym.YearId
                            join mm in db.MonthMasters on dmf.MonthId equals mm.MonthId
                            where dmf.IsActive == true
                            select new DTO_DMFFundCOllection
                            {
                                ChallanDate = dmf.ChallanDate,
                                ChallanNo = dmf.ChallanNo,
                                MonthName = mm.MonthName,
                                YearName = ym.YearName,
                                DMFAmt = dmf.DMFAmt,
                                RoyaltyAmt = dmf.RoyaltyAmt,
                                ChallanDoc = dmf.ChallanDoc,
                                FundCollectionId = dmf.FundCollectionId.ToString(),
                                RemainingDMFAmt = dmf.RemainingDMFAmt,
                                DepositedDMFAmt = dmf.DepositedDMFAmt,
                                LeseeName = lm.LesseeName
                            }).ToList();
            ViewBag.LstData = data;
            }
            return View();
        }
        [HttpGet]
        public ActionResult CreateFundCollection()
        {
            var selectedMonth = db.MonthMasters.Where(x => x.MonthId == DateTime.Now.Month).FirstOrDefault().MonthId;

            ViewBag.MonthId = new SelectList(db.MonthMasters.Where(x => x.IsActive == true), "MonthId", "MonthName", selectedMonth);
            var selectedyear = db.YearMasters.Where(x => x.YearName == DateTime.Now.Year.ToString()).FirstOrDefault().YearId;
            ViewBag.YearId = new SelectList(db.YearMasters.Where(x => x.IsActive == true), "YearId", "YearName",selectedyear);
            ViewBag.LesseeId = new SelectList(db.LesseeMasters.Where(x => x.IsActive == true).ToList().Select(x => new { ID = x.LesseeID, Text = x.LesseeName + " / " + x.LeaseID + " / " + x.Areacode }), "ID", "Text");
            DTO_DMFFundCOllection model = new DTO_DMFFundCOllection();
            return View(model);
        }
        [HttpPost]
        public JsonResult CreateFundCollection(DTO_DMFFundCOllection model)
        {
            JsonResponse JR = new JsonResponse();
            if (!ModelState.IsValid)
            {
                JR.Data = ModelState.Select(x => x.Value.Errors).Where(y => y.Count > 0).ToList();
                return Json(JR, JsonRequestBehavior.AllowGet);
            }
            if (db.DMFFundCollections.Where(x => x.ChallanNo == model.ChallanNo).Any())
            {
                JR.Message = "Challan Number " + model.ChallanNo + " Already exists !";
                return Json(JR, JsonRequestBehavior.AllowGet);
            }
            if (!String.IsNullOrEmpty(model.ChallanDoc))
            {
                model.ChallanDoc = BusinessLogics.UploadFileDMF(model.ChallanDoc);
                if (model.ChallanDoc.Contains("Expp::"))
                {
                    JR.Message = model.ChallanDoc;
                    return Json(JR, JsonRequestBehavior.AllowGet);
                }
            }
            var mineral = db.LesseeMasters.Where(x => x.LesseeID == model.LesseeId).FirstOrDefault();

            db.DMFFundCollections.Add(new DMFFundCollection
            {
                LesseeId = model.LesseeId,
                MineralId=mineral.MinralID,
                CreatedDate = DateTime.Now,
                IsActive = true,
                ChallanDate = model.ChallanDate,
                ChallanDoc = model.ChallanDoc,
                ChallanNo = model.ChallanNo.Trim(),
                DMFAmt = model.DMFAmt,
                RoyaltyAmt = model.RoyaltyAmt,
                MonthId = model.MonthId,
                YearId = model.YearId,
                RemainingDMFAmt=model.RemainingDMFAmt,
                DepositedDMFAmt=model.DepositedDMFAmt,
                CreatedBy = UserManager.GetUserLoginInfo(User.Identity.Name).LoginID.ToString(),
                DistrictId=model.DistrictId
                
            });

            int res = db.SaveChanges();
            if (res > 0)
            {
                JR.IsSuccess = true;
                JR.Message = "Data Saved Successfully";
                JR.RedURL = "/DMFFundCollection/FundCollection";
            }
            else
            {
                JR.Message = "Some Error Occured, Contact to Admin";
            }
            return Json(JR, JsonRequestBehavior.AllowGet);
        }
        public ActionResult EditFundCollection(string id)
        {
            if (String.IsNullOrEmpty(id))
            {
                return RedirectToAction("Login", "Account");
            }
            try
            {
                int _id = int.Parse(CryptoEngine.Decrypt(id));
                var Info = db.DMFFundCollections.Where(x => x.FundCollectionId == _id).FirstOrDefault();
                if (Info != null)
                {
                    ViewBag.LesseeId = new SelectList(db.LesseeMasters.Where(x => x.IsActive == true).ToList().Select(x => new { ID = x.LesseeID, Text = x.LesseeName + " / " + x.LeaseID + " / " + x.Areacode }), "ID", "Text", Info.LesseeId);
                    ViewBag.MonthId = new SelectList(db.MonthMasters.Where(x => x.IsActive == true), "MonthId", "MonthName", Info.MonthId);
                    ViewBag.YearId = new SelectList(db.YearMasters.Where(x => x.IsActive == true), "YearId", "YearName", Info.YearId);

                    return View("~/Views/DMFFundCollection/CreateFundCollection.cshtml", new DTO_DMFFundCOllection { FundCollectionId = CryptoEngine.Encrypt(Info.FundCollectionId.ToString()), ChallanDate = Info.ChallanDate, ChallanNo = Info.ChallanNo, DMFAmt = Info.DMFAmt, RoyaltyAmt = Info.RoyaltyAmt, LesseeId = Info.LesseeId, YearId = Info.YearId, MonthId = Info.MonthId,DepositedDMFAmt=Info.DepositedDMFAmt,RemainingDMFAmt=Info.RemainingDMFAmt,ChallanDoc=Info.ChallanDoc });
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
        public JsonResult EditFundCollection(DTO_DMFFundCOllection model)
        {
            JsonResponse JR = new JsonResponse();
            if (!ModelState.IsValid)
            {
                JR.Data = ModelState.Select(x => x.Value.Errors).Where(y => y.Count > 0).ToList();
                return Json(JR, JsonRequestBehavior.AllowGet);
            }
            if (!String.IsNullOrEmpty(model.ChallanDoc))
            {
                if (model.ChallanDoc != "prev")
                {
                    model.ChallanDoc = BusinessLogics.UploadFileDMF(model.ChallanDoc);
                    if (model.ChallanDoc.Contains("Expp::"))
                    {
                        JR.Message = model.ChallanDoc;
                        return Json(JR, JsonRequestBehavior.AllowGet);
                    }
                }
            }

            if (!String.IsNullOrEmpty(model.FundCollectionId))
            {
                int _id = int.Parse(CryptoEngine.Decrypt(model.FundCollectionId));
                if (db.DMFFundCollections.Where(x => x.ChallanNo == model.ChallanNo && x.FundCollectionId != _id).Any())
                {
                    JR.Message = "Challan Number " + model.ChallanNo + " Already exists !";
                    return Json(JR, JsonRequestBehavior.AllowGet);
                }
                var Info = db.DMFFundCollections.Where(x => x.FundCollectionId == _id).FirstOrDefault();
                if (Info != null)
                {
                    Info.LesseeId = model.LesseeId;
                    Info.ChallanDate = model.ChallanDate;
                    Info.ChallanDoc = (model.ChallanDoc != null && model.ChallanDoc == "prev") ? Info.ChallanDoc : model.ChallanDoc;
                    Info.ChallanNo = model.ChallanNo.Trim();
                    Info.DMFAmt = model.DMFAmt;
                    Info.RoyaltyAmt = model.RoyaltyAmt;
                    Info.MonthId = model.MonthId;
                    Info.YearId = model.YearId;
                    Info.RemainingDMFAmt = model.RemainingDMFAmt;
                    Info.RemainingDMFAmt = model.RemainingDMFAmt;
                }
            }
            int res = db.SaveChanges();
            if (res > 0)
            {
                JR.IsSuccess = true;
                JR.Message = "Data Updated Successfully";
                JR.RedURL = "/DMFFundCollection/FundCollection";
            }
            else
            {
                JR.Message = "Some Error Occured, Contact to Admin";
            }
            return Json(JR, JsonRequestBehavior.AllowGet);
        }
        public JsonResult DeleteFundCollection(string id)
        {
            JsonResponse JR = new JsonResponse();
            if (!String.IsNullOrEmpty(id))
            {
                int _id = int.Parse(CryptoEngine.Decrypt(id));
                var Info = db.DMFFundCollections.Where(x => x.FundCollectionId == _id).FirstOrDefault();
                if (Info != null)
                {
                    db.DMFFundCollections.Remove(Info);
                }
            }
            int res = db.SaveChanges();
            if (res > 0)
            {
                JR.IsSuccess = true;
                JR.Message = "Data Deleted Successfully";
                JR.RedURL = "/DMFFundCollection/FundCollection";
            }
            else
            {
                JR.Message = "Some Error Occured, Contact to Admin";
            }
            return Json(JR, JsonRequestBehavior.AllowGet);
        }
        #region Ramdhyan 02.02.2024 05:21 PM 
        public JsonResult GetRoyaltyByLessee(int LesseeId)
        {
            //var data = (from u in db.LesseeOpeningDMFAmts
            //            join r in db.RoyaltyCollections on u.LesseeId equals (r.LesseeId)
            //            where r.LesseeId == LesseeId
            //            select new DTO_LesseeOpeningDMFAmt
            //            {

            //            }).ToList();
           var data = db.DMFFundCollections.Where(x => x.LesseeId == LesseeId).ToList();
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetDistrictByLesse(int LesseeId)
        {
            var data = db.LesseeMasters.Where(x => x.LesseeID == LesseeId).FirstOrDefault();
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        public ActionResult TotalFundCollection()
        {
            int? DistID = null;
            if (UserManager.GetUserLoginInfo(User.Identity.Name).RoleID == 2)
            {
                DistID = UserManager.GetUserLoginInfo(User.Identity.Name).DistID;
            }
            //var openingamt = db.LesseeOpeningDMFAmts.ToList();
            //var fundcollection = db.DMFFundCollections.ToList();
            //var res = (from o in openingamt
            //           join f in fundcollection on o.DistrictId equals f.DistrictId
            //           where o.DistrictId == (DistID == null ? o.DistrictId : DistID)
            //           group new { o, f } by o.DistrictId into grouped
            //           select new DTO_LesseeOpeningDMFAmt
            //           {
            //               DistrictId = grouped.Key,
            //               DMFOpeningAmt = grouped.Sum(x => x.o.DMFOpeningAmt),
            //               DepositedDMFAmt = grouped.Sum(x => x.f.DepositedDMFAmt)
            //           }
            //         ).ToList();
            var result = (from op in db.LesseeOpeningDMFAmts
                              //join lm in db.LesseeMasters on op.DistrictId equals lm.DistID into lmGroup
                              //from lm in lmGroup.DefaultIfEmpty()
                          join fd in db.DMFFundCollections on op.DistrictId equals fd.DistrictId into fdGroup
                          from fd in fdGroup.DefaultIfEmpty()
                          join dm in db.DistrictMasters on op.DistrictId equals dm.DistrictId into dms
                          from dm in dms.DefaultIfEmpty()
                          where op.DistrictId == (DistID == null ? op.DistrictId : DistID)
                          group new { op, fd, dm } by dm.DistrictName into grouped
                          select new DTO_LesseeOpeningDMFAmt
                          {
                              DistrictName = grouped.Key,
                              DMFOpeningAmt = grouped.Sum(x => x.op.DMFOpeningAmt),
                              DepositedDMFAmt = grouped.Sum(x => x.fd.DepositedDMFAmt)
                          }).ToList();
            ViewBag.LstData = result;
            //ViewBag.LstData = res;
            return View();
        }
        #endregion
    }
}