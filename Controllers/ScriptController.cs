using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ScriptManage.Models;
using WebMatrix.WebData;

namespace ScriptManage.Controllers
{
    [Authorize]
    public class ScriptController : Controller
    {
        public ActionResult Index(int id)
        {
            var db = new DatabaseContext();
            if (User.IsInRole("系统管理员"))
                return View(db.Scripts.Where(s => (s.sid == id)).OrderByDescending(s => s.id));
            else
                return View(db.Scripts.Where(s => (s.sid == id && !s.del)).OrderByDescending(s => s.id));
        }
        public void Code(int id)
        {
            using (var db = new DatabaseContext())
            {
                var scode = db.ScriptsCode.OrderByDescending(s => s.dates).FirstOrDefault(s => s.sid == id);
                Response.ContentType = "text/html";
                Response.ExpiresAbsolute = System.DateTime.Now.AddSeconds(-1);
                Response.Expires = 0;
                Response.AddHeader("pragma", "no-cache");
                Response.CacheControl = "no-cache";
                Response.Write(scode.code);
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public void Update(int id)
        {
            string code = Request.Form["code"];
            string sid = Request.Form["sid"];
            if (ModelState.IsValid)
            {
                ScriptModel.Update(id, code);
            }
            Response.ContentType = "text/html";
            Response.Write(Url.Action("Index", "Script", new { id = sid }));
        }
        public ActionResult Del(int id)
        {
            ScriptModel.Del(id);
            return Redirect(Request.UrlReferrer.ToString());
        }
        public ActionResult Lock(int id)
        {
            ScriptModel.Lock(id);
            return Redirect(Request.UrlReferrer.ToString());
        }
        public ActionResult New()
        {
            using (var db = new DatabaseContext())
            {
                SelectList list = new SelectList(db.Sites.Select(s => new { s.id, s.domain }).ToList(), "id", "domain");
                ViewBag.Site = list;
            }
            List<SelectListItem> nList = new List<SelectListItem>();
            nList.Add(new SelectListItem() { Text = "本地代码块", Value = "0" });
            nList.Add(new SelectListItem() { Text = "远程脚本文件", Value = "1" });
            SelectList sList = new SelectList(nList.ToList(), "Value", "Text");
            ViewBag.Type = sList;
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult New(NewScriptModel model)
        {
            if (ModelState.IsValid)
            {
                Scripts script = new Scripts()
                {
                    name = model.name,
                    sid = model.sid,
                    type = model.type,
                    locks = false,
                    del = false,
                    dates = DateTime.Now,
                };
                if (!ScriptModel.NewScript(script, model.code))
                    ModelState.AddModelError("", "指定的脚本名称已经存在");
                else
                    ViewBag.Message = "脚本创建成功";
            }
            return New();
        }
    }
}