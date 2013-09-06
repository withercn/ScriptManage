using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web.Mvc;

namespace ScriptManage.Models
{
    public class ScriptModel
    {
        public static bool NewScript(Scripts script, string code)
        {
            using (var db = new DatabaseContext())
            {
                var query = db.Scripts.FirstOrDefault(s => s.name == script.name);
                if (query != null) return false;
                db.Scripts.Add(script);
                db.SaveChanges();
                var id = script.id;
                ScriptsCode scode = new ScriptsCode() { sid = id, code = code, dates = DateTime.Now };
                db.ScriptsCode.Add(scode);
                db.SaveChanges();
                return true;
            }
        }
        public static void Update(int id,string name,string code)
        {
            using (var db = new DatabaseContext())
            {
                var scripts = db.Scripts.FirstOrDefault(s => s.id == id);
                if (scripts != null)
                {
                    scripts.name = name;
                    db.SaveChanges();
                }
                ScriptsCode scode = new ScriptsCode() { sid = id, code = code, dates = DateTime.Now };
                db.ScriptsCode.Add(scode);
                db.SaveChanges();
            }
        }
        public static void Del(int id)
        {
            using (var db = new DatabaseContext())
            {
                var script = db.Scripts.FirstOrDefault(s => s.id == id);
                if (script != null)
                {
                    script.del = !script.del;
                    db.SaveChanges();
                }
            }
        }
        public static void Lock(int id)
        {
            using (var db = new DatabaseContext())
            {
                var script = db.Scripts.FirstOrDefault(s => s.id == id);
                if (script != null)
                {
                    script.locks = !script.locks;
                    db.SaveChanges();
                }
            }
        }
    }

    public class NewScriptModel
    {
        [Required]
        [Display(Name = "脚本名称")]
        public string name { get; set; }

        [Required]
        [Display(Name = "站点域名")]
        public int sid { get; set; }

        [Required]
        [Display(Name = "代码类型")]
        public int type { get; set; }

        [Required]
        [Display(Name = "脚本内容")]
        public string code { get; set; }
    }
}