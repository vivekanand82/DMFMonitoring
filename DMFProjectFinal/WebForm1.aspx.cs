using DMFProjectFinal.Controllers;
using System;
using System.Linq;

namespace DMFProjectFinal
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Response.Write(CryptoEngine.Decrypt("v9Mqvqcv0f8="));
                // DateTime FDTTM = new DateTime(DateTime.UtcNow.AddMinutes(330).Year, DateTime.UtcNow.AddMinutes(330).Month, 1);
                ////FDTTM=FDTTM.AddDays(1);
                ////var Days = new string[] { "Sun", "Mon", "Tue", "Wed", "Thu", "Fri", "Sat" };
                //var Days = new string[] { "January", "February", "March", "April", "May", "June", "July", "August", "September", "October", "November", "December" };
                ////Response.Write(FDTTM.DayOfWeek);
                ////Response.Write("<br/>");
                //// Response.Write(Days.ToList().IndexOf(FDTTM.DayOfWeek.ToString().Substring(0,3)));
                //Response.Write(FDTTM.DayOfYear);
            }
        }
    }
}