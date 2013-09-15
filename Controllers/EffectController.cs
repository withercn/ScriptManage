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
        public ActionResult New()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult New(EffectNewModel model)
        {
            if (ModelState.IsValid)
            {

            }
            return View();
        }
    }
}
