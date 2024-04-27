using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DMFProjectFinal.Models
{
    public class AccessManager
    {
        public AccessManager()
        {
            CanView = true;
            CanInsert = true;
            CanUpdate = true;
            CanDelete = true;
            RoleID = 0;
            LoginID = 0;
        }
        public bool CanView { get; set; }
        public bool CanInsert { get; set; }
        public bool CanUpdate { get; set; }
        public bool CanDelete { get; set; }
        public int RoleID { get; set; }
        public int LoginID { get; set; }
    }
    public class JsonResponse
    {
        public bool IsSuccess { get; set; }
        public string RedURL { get; set; }
        public string Message { get; set; }
        public object Data { get; set; }
        public JsonResponse()
        {
            IsSuccess = false;
            RedURL = "";
            Message = "";
            Data = null;
        }
    }
}