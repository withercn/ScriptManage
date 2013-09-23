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
            ViewBag.SiteID = id;
            ViewBag.ReturnUrl = Url.Action("Index", "Site");
            if (User.IsInRole("系统管理员"))
                return View(db.Scripts.Where(s => ((s.sid == id || s.shared) && !s.purge)).OrderBy(s => s.shared).ThenByDescending(s => s.id));
            else
                return View(db.Scripts.Where(s => ((s.sid == id || s.shared) && !s.del && !s.purge)).OrderBy(s => s.shared).ThenByDescending(s => s.id));
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
        [HttpPost]
        public ActionResult Remove(int id)
        {
            ScriptModel.Remove(id);
            return new EmptyResult();
        }
        [Authorize(Roles="系统管理员")]
        public ActionResult Purge(int id,int siteid)
        {
            if (ScriptModel.Purge(id))
                ViewBag.Message = "脚本删除成功";
            Model.ScriptRedirect(ViewBag, Url.Action("Index", "Script", new { id = siteid }));
            return RedirectToAction("Index", "Script", new { id = siteid });
        }
        public ActionResult Lock(int id)
        {
            ScriptModel.Lock(id);
            return Redirect(Request.UrlReferrer.ToString());
        }
        public RedirectResult Shared(int id)
        {
            ScriptModel.Shared(id);
            return Redirect(Request.UrlReferrer.ToString());
        }
        public ActionResult New(int id)
        {
            using (var db = new DatabaseContext())
            {
                ViewBag.Type = new SelectList(db.CodeTypes.Select(s => new { s.id, s.name }).ToList(), "id", "name");   
            }
            return View(new NewScriptModel() { siteid = id });
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
                    sid = model.siteid,
                    type = model.type,
                    locks = false,
                    del = false,
                    dates = DateTime.Now,
                };
                if (!ScriptModel.NewScript(script, model.code))
                    ModelState.AddModelError("", "指定的脚本名称已经存在");
                else
                {
                    ViewBag.Message = "脚本创建成功";
                    Model.ScriptRedirect(ViewBag, Url.Action("Index", "Script", new { id = model.siteid }));
                }
                
            }
            return New(model.siteid);
        }
        [OutputCache(Duration=0)]
        public JsonResult History(int id)
        {
            var db = new DatabaseContext();
            return Model.Json(db.ScriptsCode.Where(s => s.sid == id).Select(s => new { s.id, s.dates, s.code,s.type }).OrderByDescending(s => s.dates));
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
                ScriptModel.Update(model);
                ViewBag.Message = "脚本修改成功";
                Model.ScriptRedirect(ViewBag, Url.Action("Index", "Script", new { id = model.siteid }));
            }
            return Edit(model.sid);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Download(int id)
        {
            var scriptid = Request.Form["scriptid"];
            var enname = Request.Form["enname"];
            if (string.IsNullOrEmpty(enname)) enname = "1";
            ViewBag.ReturnUrl = Url.Action("Index", "Script", new { id = id });
            if (string.IsNullOrEmpty(scriptid)) return View();
            var remotes = SiteModel.GetRemoteScript(scriptid);
            foreach (var code in remotes)
            {
                ViewBag.ScriptUrl += string.Format("<script type=\"text/javascript\" src=\"{0}\"></script>\r\n", code.code);
            }
            ViewBag.DownloadUrl = Url.Action("DownloadFile", "Script", new { scriptid = scriptid, scriptname = enname });
            ViewBag.ScriptUrl += string.Format("<script type=\"text/javascript\" src=\"/js/{0}.js\"></script>\r\n", enname);
            return View();
        }
        public FileContentResult DownloadFile(string scriptid, string scriptname)
        {
            if (string.IsNullOrEmpty(scriptid)) return new FileContentResult(null, null);
            return File(System.Text.Encoding.UTF8.GetBytes(Model.BundleScript(SiteModel.GetLocalScript(scriptid).Select(s => s.code).ToList<string>())), "application/x-javascript", scriptname + ".js");
        }
    }
}