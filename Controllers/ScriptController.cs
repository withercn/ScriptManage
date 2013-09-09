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
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public void Update(int id)
        {
            string code = Request.Form["code"];
            int sid = int.Parse(Request.Form["sid"]);
            string name = Request.Form["name"];
            if (ModelState.IsValid)
            {
                ScriptModel.Update(id, name, code);
            }

            Model.ResponseAjaxHtml(Url.Action("Index", "Script", new { id = sid }));
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
            
            //var code = db.Database.ExecuteSqlCommand(string.Format("select top 1 c.id,code,c.dates,[name] from ScriptsCode c inner join [scripts] s on c.sid=s.id  where s.id={0} and s.del=0 order by c.dates desc", id));
            return View(ScriptModel.GetCode(id));
        }
    }
}