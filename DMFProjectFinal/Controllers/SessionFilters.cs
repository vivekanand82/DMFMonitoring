using DMFProjectFinal.Models;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System;
using System.Web.Security;

namespace DMFProjectFinal.Controllers
{
    public class SessionFilterAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            dfm_dbEntities db = new dfm_dbEntities();
            if (!filterContext.HttpContext.User.Identity.IsAuthenticated)
            {
                filterContext.Result = new RedirectResult("/Account/Login");
                return;
            }
            else
            {
                try
                {
                    if (!filterContext.HttpContext.User.Identity.Name.StartsWith("MDFProject"))
                    {
                        FormsAuthentication.SignOut();
                        filterContext.Result = new RedirectResult("/Account/Login");
                        return;
                    }
                    var LoginID = long.Parse(filterContext.HttpContext.User.Identity.Name.Replace("MDFProject",""));
                    var Info = db.UserLogins.Where(x => x.LoginID == LoginID).FirstOrDefault();
                    if (Info == null)
                    {
                        FormsAuthentication.SignOut();
                        filterContext.Result = new RedirectResult("/Account/Login");
                        return;
                    }
                    var descriptor = filterContext.ActionDescriptor;
                    var actionName = descriptor.ActionName.ToLower();
                    var controllerName = descriptor.ControllerDescriptor.ControllerName;

                    string RemovedPrefActionName = actionName;
                    string Pref = "";
                    if (actionName.StartsWith("create"))
                    {
                        RemovedPrefActionName = actionName.Substring(actionName.IndexOf("create") + 6);
                        Pref = "c";
                    }
                    else if (actionName.StartsWith("edit"))
                    {
                        RemovedPrefActionName = actionName.Substring(actionName.IndexOf("edit") + 4);
                        Pref = "e";
                    }
                    else if (actionName.StartsWith("delete"))
                    {
                        RemovedPrefActionName = actionName.Substring(actionName.IndexOf("delete") + 6);
                        Pref = "d";
                    }

                    bool IsSub = false;
                    AccessManager AM = new AccessManager();
                    AM.RoleID = Info.RoleID.Value;
                    var MenuData = db.MenuMasters.Where(x => x.ActionName.ToLower() == actionName.ToLower() && x.ControllerName.ToLower() == controllerName && x.IsActive == true).FirstOrDefault();
                    if (MenuData == null)
                    {
                        MenuData = db.MenuMasters.Where(x => x.ActionName.ToLower() == RemovedPrefActionName && x.ControllerName.ToLower() == controllerName && x.IsActive == true).FirstOrDefault();
                        IsSub = true;
                    }
                    if (MenuData != null)
                    {
                        var RoleMenu = db.RoleMenuMappings.Where(x => x.MenuID == MenuData.MenuID && x.UserRoleID == Info.RoleID && x.IsActive == true).FirstOrDefault();
                        if (RoleMenu != null)
                        {
                            AM.CanDelete = RoleMenu.CanDelete == null ? false : RoleMenu.CanDelete.Value;
                            AM.CanInsert = RoleMenu.CanInsert == null ? false : RoleMenu.CanInsert.Value;
                            AM.CanUpdate = RoleMenu.CanUpdate == null ? false : RoleMenu.CanUpdate.Value;
                            AM.CanView = RoleMenu.CanView == null ? false : RoleMenu.CanView.Value;
                            if (IsSub)
                            {
                                if (Pref == "c" && !AM.CanInsert)
                                {
                                    FormsAuthentication.SignOut();
                                    filterContext.Result = new RedirectResult("/Account/Login");
                                    return;
                                }
                                else if (Pref == "e" && !AM.CanUpdate)
                                {
                                    FormsAuthentication.SignOut();
                                    filterContext.Result = new RedirectResult("/Account/Login");
                                    return;
                                }
                                else if (Pref == "d" && !AM.CanDelete)
                                {
                                    FormsAuthentication.SignOut();
                                    filterContext.Result = new RedirectResult("/Account/Login");
                                    return;
                                }
                            }
                            if (!AM.CanDelete && !AM.CanInsert && !AM.CanUpdate && !AM.CanView)
                            {
                                FormsAuthentication.SignOut();
                                filterContext.Result = new RedirectResult("/Account/Login");
                                return;
                            }
                        }
                        else
                        {
                            FormsAuthentication.SignOut();
                            filterContext.Result = new RedirectResult("/Account/Login");
                            return;
                        }
                    }
                    filterContext.Controller.ViewBag.GetAccess = AM;
                }
                catch (Exception)
                {
                    FormsAuthentication.SignOut();
                    filterContext.Result = new RedirectResult("/Account/Login");
                    return;
                }
            }
            base.OnActionExecuting(filterContext);
        }
    }
}