﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.Security;
using ScriptManage.Models;

namespace ScriptManage
{
    // 注意: 有关启用 IIS6 或 IIS7 经典模式的说明，
    // 请访问 http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication
    {
        public MvcApplication()
        {
            AuthorizeRequest += MvcApplication_AuthorizeRequest;
        }
        
        void MvcApplication_AuthorizeRequest(object sender, EventArgs e)
        {
            IIdentity identity = Context.User.Identity;
            if (identity.IsAuthenticated)
            {
                var roles = Models.UserModel.GetRoles(identity.Name);
                if (roles == null)
                {
                    FormsAuthentication.SignOut();
                    return;
                }
                Context.User = new GenericPrincipal(identity, roles);
            }
        }
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
    }
}