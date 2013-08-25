using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Security.Principal;

namespace ScriptManage.Models
{
    public class BaseAuthorizeAttribute : AuthorizeAttribute
    {
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            if (httpContext.Session["CurrentUser"] != null)
            {
                Users user = httpContext.Session["CurrentUser"] as Users;
                var identity = new GenericIdentity(user.username);
                HttpContext.Current.User = new GenericPrincipal(identity, GetRoles(user));
                return true;
            }
            return false;
        }

        private string[] GetRoles(Users user)
        {
            if(user.flag)
                return new string[] { "系统管理员", "管理员" };
            else
                return new string[] { "管理员" };
        }
    }
}