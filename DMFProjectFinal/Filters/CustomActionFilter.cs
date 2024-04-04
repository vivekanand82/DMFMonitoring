using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MinutesOfMeeting.Filters
{
    public class CustomActionFilter : ActionFilterAttribute, IActionFilter
    {
        void IActionFilter.OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (filterContext.HttpContext.Request.Url.ToString().ToLower().Contains("/admin"))
            {
                var LoginID = long.Parse(User.Identity.Name);
            }
            OnActionExecuting(filterContext);
        }
    }
}