using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using ScriptManage.Models;

namespace ScriptManage.Controllers
{
    [Authorize(Roles="系统管理员")]
    public class HomeController : Controller
    {
        //
        // GET: /Home/
        public ActionResult Index()
        {
            
            return View();
        }
    }
}
