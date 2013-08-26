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
            foreach (var site in Sites.GetSite())
            {

            }
            return View();
        }
        [AllowAnonymous]
        public ActionResult Install()
        {
            using (var db = new DatabaseContext())
            {
                db.Role.Add(new Role() { name = "系统管理员" });
                db.Role.Add(new Role() { name = "管理员" });
                db.Role.Add(new Role() { name = "匿名" });
                db.Users.Add(new Users() { username = "clal", password = Model.CreateMD5String("clyal"), role = new Role() { name = "系统管理员" } });
                db.SaveChanges();
            }
            return View();
        }
    }
}
