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
                var query = db.Scripts.FirstOrDefault(s => (s.name == script.name && s.sid == script.sid));
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
        public static bool Update(CodeModel model)
        {
            using (var db = new DatabaseContext())
            {
                var scripts = db.Scripts.FirstOrDefault(s => (s.name == model.name && s.sid == model.siteid));
                if (scripts != null) return false;
                scripts = db.Scripts.FirstOrDefault(s => s.id == model.id);
                if (scripts != null)
                {
                    scripts.name = model.name;
                    scripts.type = model.type;
                    db.SaveChanges();
                }
                db.ScriptsCode.Add(new ScriptsCode() { sid = model.id, code = model.code, dates = DateTime.Now });
                db.SaveChanges();
                return true;
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
        public static void Remove(int id)
        {
            using (var db = new DatabaseContext())
            {
                var code = db.ScriptsCode.FirstOrDefault(s => s.id == id);
                if (code != null)
                {
                    db.ScriptsCode.Remove(code);
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
        public static void Undo(int id)
        {
            using (var db = new DatabaseContext())
            {
                var code = db.ScriptsCode.FirstOrDefault(c => c.id == id);
                if (code != null)
                {
                    code.dates = DateTime.Now;
                    db.SaveChanges();
                }
            }
        }
        public static CodeModel GetCode(int id)
        {
            using (var db = new DatabaseContext())
            {
                var query = (from c in db.ScriptsCode
                             join s in db.Scripts
                             on c.sid equals s.id
                             where s.id == id
                             orderby c.dates descending
                             select new CodeModel
                             {
                                 id = c.id,
                                 sid = s.id,
                                 siteid = s.sid,
                                 locks = s.locks,
                                 type = s.type,
                                 del = s.del,
                                 name = s.name,
                                 code = c.code,
                                 dates = c.dates
                             }).Take(1);
                foreach (var m in query)
                    return m;
                return null;
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
        public int siteid { get; set; }

        [Required]
        [Display(Name = "脚本类型")]
        public int type { get; set; }

        [Required]
        [Display(Name = "脚本内容")]
        public string code { get; set; }
    }
    [Serializable]
    public class CodeModel
    {
        [Required]
        [Display(Name = "代码编号")]
        public int id { get; set; }
        [Required]
        [Display(Name = "脚本编号")]
        public int sid { get; set; }
        [Required]
        [Display(Name = "站点域名")]
        public int siteid { get; set; }
        [Required]
        [Display(Name = "脚本名称")]
        public string name { get; set; }
        [Required]
        [Display(Name = "是否锁定")]
        public bool locks { get; set; }
        [Required]
        [Display(Name = "是否删除")]
        public bool del { get; set; }
        [Required]
        [Display(Name = "脚本类型")]
        public int type { get; set; }
        [Required]
        [Display(Name = "脚本内容")]
        public string code { get; set; }
        [Required]
        [Display(Name = "修改时间")]
        public DateTime dates { get; set; }
    }
}