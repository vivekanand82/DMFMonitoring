using DMFProjectFinal.Models;
using DMFProjectFinal.Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DMFProjectFinal.Controllers
{
    [Authorize]
    public class CommonController : Controller
    {
        private dfm_dbEntities db = new dfm_dbEntities();
        [HttpPost]
        public JsonResult FetchDDLInfos(int DepID, string Flag = "001") // its a GET, not a POST
        {
            if (Flag == "001")
            {
                var lstData = db.DistrictMasters.Where(x => x.IsActive == true && x.StateID == DepID).Select(c => new
                {
                    ID = c.DistrictId,
                    Text = c.DistrictName
                }).ToList();
                return Json(lstData, JsonRequestBehavior.AllowGet);
            }
            else if (Flag == "002")
            {
                var lstData = db.TehsilMasters.Where(x => x.IsActive == true && x.DistrictId == DepID).Select(c => new
                {
                    ID = c.TehsilId,
                    Text = c.TehsilName
                }).ToList();
                return Json(lstData, JsonRequestBehavior.AllowGet);
            }
            else if (Flag == "003")
            {
                var lstData = db.VillageMasters.Where(x => x.IsActive == true && x.TehsilId == DepID).Select(c => new
                {
                    ID = c.VillageId,
                    Text = c.VillageNameInEnglish
                }).ToList();
                return Json(lstData, JsonRequestBehavior.AllowGet);
            }
            else if (Flag == "004")
            {
                var lstData = db.AgenciesInfoes.Where(x => x.IsActive == true && x.DistID == DepID).Select(c => new
                {
                    ID = c.AgencyID,
                    Text = c.Name + " / " + c.OwnerName
                }).ToList();
                return Json(lstData, JsonRequestBehavior.AllowGet);
            }
            else if (Flag == "005")
            {
                var lstData = db.ProjectMasters.Where(x => x.IsActive == true && x.DistID == DepID).Select(c => new
                {
                    ID = c.ProjectID,
                    Text = c.ProjectName + " (" + c.ProjectCode + ")"
                }).ToList();
                return Json(lstData, JsonRequestBehavior.AllowGet);
            }

            else if (Flag == "006")
            {
                var lstData = db.SectorNameMasters.Where(x => x.IsActive == true && x.SectorTypeId == DepID).Select(c => new
                {
                    ID = c.SectorNameId,
                    Text = c.SectorName
                }).ToList();
                return Json(lstData, JsonRequestBehavior.AllowGet);
            }

            else  if (Flag == "007")
            {
                var lstData = (from pm in db.ProjectMasters
                               join dst in db.DistrictMasters on pm.DistID equals dst.DistrictId
                               join st in db.SectorTypeMasters on pm.SectorTypeId equals st.SectorTypeID
                              
                               where pm.IsActive == true && pm.DistID == DepID
                               select new DTO_ProjectMaster
                               {
                                   SectorTypeId = st.SectorTypeID,

                                   SectorType = st.SectorType,


                               }

                              ).ToList();

                return Json(lstData, JsonRequestBehavior.AllowGet);
            }
            else if (Flag == "008")
            {
                var lstData = db.BlockMasters.Where(x => x.IsActive == true && x.DistrictId == DepID).Select(c => new
                {
                    ID = c.BlockId,
                    Text = c.BlockName
                }).ToList();
                return Json(lstData, JsonRequestBehavior.AllowGet);
            }







            return Json(null, JsonRequestBehavior.AllowGet);
        }
        public JsonResult BindDistrictname(int DepID)
        {
            var lstData = (from pm in db.ProjectMasters
                           join dst in db.DistrictMasters on pm.DistID equals dst.DistrictId
                           join st in db.SectorNameMasters on pm.SectorNameId equals st.SectorNameId

                           where pm.IsActive == true && pm.DistID == DepID
                           select new DTO_ProjectMaster
                           {
                               SectorNameId = st.SectorNameId,

                               SectorName = st.SectorName,


                           }

                                         ).ToList();

            return Json(lstData, JsonRequestBehavior.AllowGet);


        }


    }
}