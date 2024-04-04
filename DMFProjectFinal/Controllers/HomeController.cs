using DMFProjectFinal.Models;
using DMFProjectFinal.Models.DTO;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace DMFProjectFinal.Controllers
{
    [SessionFilter]
    public class HomeController : Controller
    {
        private dfm_dbEntities db = new dfm_dbEntities();

        public ActionResult Index()
        {
            return View();
        }
        public ActionResult ChangePassword()
        {
            return View(new P_ChangePassword());
        }
        [HttpPost]
        public ActionResult ChangePassword(P_ChangePassword model)
        {
            var Lgn = UserManager.GetUserLoginInfo(User.Identity.Name);
            if (Lgn.Password != CryptoEngine.Encrypt(model.OldPass))
            {
                ViewBag.Msg = "Old Password is incorrect !";
                return View(model);
            }
            else
            {
                var _Login = db.UserLogins.Where(x => x.LoginID == Lgn.LoginID).FirstOrDefault();
                _Login.Password = CryptoEngine.Encrypt(model.NewPass);
                db.SaveChanges();
                ViewBag.Msg = "Password Updated Successfully !";
                return View(new P_ChangePassword());
            }
        }
    }
}