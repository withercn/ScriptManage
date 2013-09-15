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
        [Authorize(Roles = "系统管理员")]
        public ActionResult Index(int? id)
        {
            int pageindex = 1;
            if (id != null)
                pageindex = (int)id;
            var pagesize = 10;
            var db = new DatabaseContext();
            var record = db.Users.Count();
            HtmlPager pager = new HtmlPager(record, pageindex, pagesize);
            ViewBag.Pager = pager;
            var query = db.Users.OrderByDescending(u => u.id).Skip((pageindex - 1) * pagesize).Take(pagesize);
            return View(query);
        }
        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }
        [Authorize(Roles = "系统管理员")]
        public ActionResult Del(int id)
        {
            UserModel.DeleteUser(id.ToString());
            return this.RedirectToAction("index", "User");
        }
        [Authorize(Roles = "系统管理员")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Del()
        {
            string idlist = Request.Form["id"];
            UserModel.DeleteUser(idlist);
            return this.RedirectToAction("index", "User");
        }
        [Authorize(Roles = "系统管理员")]
        public ActionResult New()
        {
            return View();
        }
        [Authorize(Roles = "系统管理员")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult New(RegisterModel model)
        {
            if (ModelState.IsValid)
            {
                if (UserModel.NewUser(model.username, model.password))
                {
                    ViewBag.Message = "账号添加成功。";
                }
                else
                {
                    ModelState.AddModelError("", "账号已经存在。");
                }
            }
            Model.ScriptRedirect(ViewBag, Url.Action("Index", "User"));
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
                    ViewBag.Message = string.Format("用户：{0} 登陆成功.", username);
                    LogModel.Write(string.Format("用户：{0} 登陆成功.", username));
                    Model.ScriptRedirect(ViewBag, returnUrl);
                }
            }
            LogModel.Write(string.Format("用户：{0} 登陆失败.", username));
            return View(model);
        }
        [Authorize]
        public ActionResult Password(string message)
        {
            ViewBag.Message = message;
            return View();
        }
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Password(PasswordModel model)
        {
            if (ModelState.IsValid)
            {
                if (UserModel.ChangePassword(model.oldPassword, model.password))
                {
                    return RedirectToAction("Password", new { message = "密码修改成功。" });
                }
                ModelState.AddModelError("", "修改失败");
            }
            Model.ScriptRedirect(ViewBag, Url.Action("Index", "User"));
            return View(model);
        }
        public ActionResult Logout(string returnUrl)
        {
            FormsAuthentication.SignOut();
            return this.RedirectToLocal(returnUrl);
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
