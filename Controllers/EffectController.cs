using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ScriptManage.Models;
using WebMatrix.WebData;

namespace ScriptManage.Controllers
{
    [Authorize(Roles="系统管理员")]
    public class EffectController : Controller
    {
        public ActionResult Index(int? id)
        {
            int pageindex = 1;
            if (id != null)
                pageindex = (int)id;
            var pagesize = 10;
            var db = new DatabaseContext();
            var record = db.Effect.Count();
            HtmlPager pager = new HtmlPager(record, pageindex, pagesize);
            ViewBag.Pager = pager;
            var query = db.Effect.OrderByDescending(s => s.id).Skip((pageindex - 1) * pagesize).Take(pagesize);
            return View(query);
        }
        [Authorize(Roles="管理员,系统管理员")]
        public ActionResult Views(int? id)
        {
            ViewBag.SiteID = id;
            var db = new DatabaseContext();
            return View(db.Effect.OrderByDescending(s => s.id));
        }
        public ActionResult Del(int id)
        {
            EffectModel.Del(id.ToString());
            Model.ScriptRedirect(ViewBag, Url.Action("Index", "Effect"));
            return View();
        }
        [HttpPost]
        public RedirectResult Del()
        {
            string list = Request.Form["id"];
            EffectModel.Del(list);
            return Redirect(Url.Action("Index", "Effect"));
        }
        public ActionResult New() { return View(); }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult New(NewEffectModel model)
        {
            if (ModelState.IsValid)
            {
                if (EffectModel.New(model))
                {
                    ViewBag.Message = "特效创建成功";
                    Model.ScriptRedirect(ViewBag, Url.Action("Index", "Effect"));
                }
                else
                    ModelState.AddModelError("", "特效名称或Action已经存在");
            }
            return View();
        }
        public ActionResult Jquery(int? id)
        { return View(); }
        public ActionResult FloatDiv(int? id)
        {
            using (var db = new DatabaseContext())
            {
                var query = db.Effect.FirstOrDefault(e => e.action.ToLower() == "floatdiv");
                if (query != null)
                {
                    ViewBag.Name = query.name;
                    ViewBag.Desctiption = query.description;
                }
            }
            return View();
        }
    }
}
