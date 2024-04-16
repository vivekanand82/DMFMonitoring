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
    public class InspectionController : Controller
    {
        // GET: Inspection
        [SessionFilter]
        public ActionResult Index()
        {
            return View();
        }
    }
}