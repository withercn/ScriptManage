﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ScriptManage.Models;
using WebMatrix.WebData;
using System.IO;

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
            var record = db.Sites.Count();
            HtmlPager pager = new HtmlPager(record, pageindex, pagesize);
            ViewBag.Pager = pager;
            var query = db.Sites.OrderByDescending(s => s.id).Skip((pageindex - 1) * pagesize).Take(pagesize);
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
        [ValidateAntiForgeryToken]
        public ActionResult New(SiteNewModel model)
        {
            if (ModelState.IsValid)
            {
                if (!SiteModel.NewSite(new Sites() { domain = model.domain, name = model.name }))
                {
                    ModelState.AddModelError("", "域名已经存在。");
                }
                ViewBag.Message = "添加站点成功。";
            }
            Model.ScriptRedirect(ViewBag, Url.Action("Index", "Site"));
            return View();
        }
        public ActionResult Download(int id)
        {
            Sites site = SiteModel.GetSite(id);
            string dPath = Server.MapPath("/block/" + site.domain);
            if (!Directory.Exists(dPath))
                Directory.CreateDirectory(dPath);
            var scripts = SiteModel.GetLocalScript(id);
            var remotes = SiteModel.GetRemoteScript(id);
            string[] block = new string[scripts.Count()];
            var i=0;
            foreach (var code in scripts)
            {
                string fPath = dPath + "\\" + code.id + ".js";
                using (StreamWriter sw = System.IO.File.CreateText(fPath))
                {
                    sw.Write(code.code);
                    sw.Flush();
                }
                block[i] = "~/block/" + site.domain + "\\" + code.id + ".js";
                i++;
            }
            var guid = Guid.NewGuid().ToString();
            string scriptUrl = "~/" + guid;
            foreach (var code in remotes)
            {
                ViewBag.ScriptUrl += string.Format("<script type=\"text/javascript\" src=\"{0}\"></script>\r\n", code.code);
            }
            Model.RegisterScript(scriptUrl, block);
            ViewBag.DownloadUrl = System.Web.Optimization.Scripts.Url(scriptUrl);
            ViewBag.ScriptUrl += string.Format("<script type=\"text/javascript\" src=\"{0}.js\"></script>\r\n", guid);
            return View();
        }
    }
}
