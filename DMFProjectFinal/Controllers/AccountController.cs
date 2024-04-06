using DMFProjectFinal.Models;
using System.Linq;
using System.Web.Mvc;
using System.Web.Security;

namespace DMFProjectFinal.Controllers
{
    public class AccountController : Controller
    {
        private dfm_dbEntities db = new dfm_dbEntities();
        // GET: Account
        public ActionResult Login()
        {
            var IsLogIn = User.Identity.IsAuthenticated;
            if (IsLogIn && User.Identity.Name.Contains("MDFProject"))
            {
                if (UserManager.GetUserLoginInfo(User.Identity.Name).RoleID == 1)
                {
                    return RedirectToAction("Index", "Admin");
                }
                else
                {
                    return RedirectToAction("Index", "Home");
                }
            }
            P_Login Lgn = new P_Login();
            return View(Lgn);
        }
        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult Login(P_Login Lgn)
        {
            try
            {
                if (!ModelState.IsValid || Lgn.Email == null || Lgn.Password == null)
                {
                    ModelState.AddModelError("", "Please enter Required Details");
                    return View(Lgn);
                }
                else
                {
                    //var password= CryptoEngine.Decrypt(Lgn.Password);
                    var CPass = CryptoEngine.Encrypt(Lgn.Password);
                    var Data = db.UserLogins.Where(x => x.UserName.ToLower() == Lgn.Email.ToLower() && x.Password == CPass).FirstOrDefault();

                    if (Data != null)
                    {
                        FormsAuthentication.SetAuthCookie("MDFProject"+Data.LoginID.ToString(), Lgn.RememberMe);
                        if (Data.RoleID == 1 || Data.RoleID == 3 || Data.RoleID == 2 || Data.RoleID == 4)
                        {
                            return RedirectToAction("Index", "Admin");
                        }
                        else if(Data.RoleID == 5)
                        {
                            return RedirectToAction("Index", "Admin");
                        }
                        else{
                            return RedirectToAction("Index", "Home");
                        }
                    }
                    else
                    {
                        ModelState.AddModelError("", "Invalid Credentials");
                        return View(Lgn);
                    }
                }
            }
            catch (System.Exception Ex)
            {
                ModelState.AddModelError("", "Exp :: "+Ex.Message);
                return View(Lgn);
            }
        }
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login", "Account");
        }
    }
}