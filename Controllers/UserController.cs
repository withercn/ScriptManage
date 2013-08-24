using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using ScriptManage.Models;

namespace ScriptManage.Controllers
{

    public class UserController : Controller
    {
        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginModel model, string returnUrl)
        {
            string username = model.username;
            string password = Model.CreateMD5String(model.password);

            using (var db = new UserContext())
            {
                var query = db.Users.FirstOrDefault(u => (u.username == username && u.password == password));
                
                //var query = db.UserProfiles
                //    .Where(s => s.username == username)
                //    .FirstOrDefault();
                //var query = from s in db.UserProfiles
                //            where (s.username == username)
                //            select s;
                if (query == null)
                    ModelState.AddModelError("", "提供的用户名或密码不正确。");
                else
                {
                    Session.Add("User", username);
                    return RedirectToLocal(returnUrl);
                }
            }
            return View(model);
        }

        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }
    }
}
