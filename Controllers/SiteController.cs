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
    public class SiteController : Controller
    {
        public ActionResult Index(int? id)
        {
            int pageindex = 1;
            if (id != null)
                pageindex = (int)id;
            var pagesize = 10;
            var db = new DatabaseContext();
            var record = db.Site.Count();
            HtmlPager pager = new HtmlPager(record, pageindex, pagesize);
            pager.Conntroller = "Site";
            pager.Action = "Index";
            ViewBag.Pager = pager;
            var query = db.Site.OrderByDescending(s => s.id).Skip((pageindex - 1) * pagesize).Take(pagesize);
            return View(query);
        }
        [Authorize(Roles="系统管理员")]
        public ActionResult Del(int id)
        {
            SiteModel.DelSite(id.ToString());
            return RedirectToAction("Index", "Site");
        }
        [HttpPost]
        [Authorize(Roles = "系统管理员")]
        public ActionResult Del()
        {
            string idlist = Request.Form["id"];
            SiteModel.DelSite(idlist);
            return RedirectToAction("Index", "Site");
        }

        public ActionResult New()
        { return View(); }
        [HttpPost]
        public ActionResult New(SiteNewModel model)
        {
            return View();
        }
    }
}
