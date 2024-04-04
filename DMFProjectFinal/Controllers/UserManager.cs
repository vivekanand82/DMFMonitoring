using DMFProjectFinal.Models;
using System;
using System.Linq;

namespace DMFProjectFinal.Controllers
{
    public class UserManager
    {
        public static UserLogin GetUserLoginInfo(string _LoginID)
        {
            try
            {
                dfm_dbEntities db = new dfm_dbEntities();
                var LoginID = long.Parse(_LoginID.Replace("MDFProject",""));
                var Info = db.UserLogins.Where(x => x.LoginID == LoginID).FirstOrDefault();
                return Info;
            }
            catch (Exception Ex)
            {
                return new UserLogin {
                RoleID=0,
                LoginID=0
                };
            }
        }
    }
}