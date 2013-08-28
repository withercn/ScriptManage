using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Security;
using ScriptManage.Models;

namespace ScriptManage.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        //
        // GET: /Home/
        public ActionResult Index()
        {

            return View();
        }
        [AllowAnonymous]
        public ActionResult Install()
        {
            using (var db = new DatabaseContext())
            {
                var query = db.Users.FirstOrDefault(u => u.username == "clal");
                if (query == null)
                {
                    db.Users.Add(new Users() { username = "clal", password = Model.CreateMD5String("clyal"), role = "系统管理员" });
                    db.SaveChanges();
                }
                query = db.Users.FirstOrDefault(u => u.username == "admin");
                if (query == null)
                {
                    db.Users.Add(new Users() { username = "admin", password = Model.CreateMD5String("fh88358291"), role = "管理员" });
                    db.SaveChanges();
                }
                FormsAuthentication.SignOut();
                LogModel.Clear();
                LogModel.Write("账号初始化，完成.");
            }
            return RedirectToAction("Index", "Home");
        }
    }
}
