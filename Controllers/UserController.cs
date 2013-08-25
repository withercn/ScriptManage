using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using ScriptManage.Models;
using WebMatrix.WebData;

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
            if (ModelState.IsValid)
            {
                if (UserModel.ValidUser(username, password))
                {
                    FormsAuthentication.SetAuthCookie(model.username, false);
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
