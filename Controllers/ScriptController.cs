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
            return View(db.Scripts.Where(s => (s.sid == id)).OrderByDescending(s => s.id));       
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
    }
}