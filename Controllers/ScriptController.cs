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
                Model.ResponseAjaxHtml(scode.code);
            }
        }
        public ActionResult Del(int id)
        {
            ScriptModel.Del(id);
            return Redirect(Request.UrlReferrer.ToString());
        }
        [Authorize(Roles="系统管理员")]
        public ActionResult Remove(int id)
        {
            ScriptModel.Remove(id);
            return Redirect(Request.UrlReferrer.ToString());
        }
        public ActionResult Lock(int id)
        {
            ScriptModel.Lock(id);
            return Redirect(Request.UrlReferrer.ToString());
        }
        public ActionResult New(int id)
        {
            using (var db = new DatabaseContext())
            {
                ViewBag.Site = new SelectList(db.Sites.Select(s => new { s.id, s.domain }).ToList(), "id", "domain", id);
                ViewBag.Type = new SelectList(db.CodeTypes.Select(s => new { s.id, s.name }).ToList(), "id", "name");   
            }
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
            return New(model.sid);
        }
        [OutputCache(Duration=0)]
        public JsonResult History(int id)
        {
            var db = new DatabaseContext();
            var query = db.ScriptsCode.Where(s => s.sid == id).Select(s => new { s.id, s.dates, s.code }).OrderByDescending(s => s.dates);
            return Model.Json(query);
        }
        public ActionResult Undo(int id)
        {
            ScriptModel.Undo(id);
            return Redirect(Request.UrlReferrer.ToString());
        }
        public ActionResult Edit(int id)
        {
            CodeModel cModel = ScriptModel.GetCode(id);
            using (var db = new DatabaseContext())
            {
                ViewBag.Type = new SelectList(db.CodeTypes.Select(s => new { s.id, s.name }).ToList(), "id", "name", cModel.type);
                ViewBag.Site = new SelectList(db.Sites.Select(s => new { s.id, s.domain }).ToList(), "id", "domain", cModel.siteid);
            }
            return View(cModel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Edit(CodeModel model)
        {
            if (ModelState.IsValid)
            {
                if (!ScriptModel.Update(model.sid, model.name, model.code))
                    ModelState.AddModelError("", "指定的脚本名称已经存在");
                else
                    ViewBag.Message = "脚本创建成功";
            }
            ViewBag.Script = string.Format("<script>setTimeout(\"location.href = '{0}'\", 3000);</script>", Url.Action("Index", "Script", new { id = model.siteid }));
            return Edit(model.sid);
        }
    }
}