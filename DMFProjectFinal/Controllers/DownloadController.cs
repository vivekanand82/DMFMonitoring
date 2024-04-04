using DMFProjectFinal.Models;
using System;
using System.Linq;
using System.Web.Mvc;

namespace DMFProjectFinal.Controllers
{
    [Authorize]
    public class DownloadController : Controller
    {
        private dfm_dbEntities db = new dfm_dbEntities();
        // GET: Download
        public ActionResult Index(string id)
        {
            return View();
        }
    }
}