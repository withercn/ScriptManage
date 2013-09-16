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
    public class EffectModel
    {
        public static bool New(NewEffectModel model)
        {
            using (var db = new DatabaseContext())
            {
                var query = db.Effect.FirstOrDefault(s => (s.name == model.name || s.action == model.action));
                if (query != null) return false;
                db.Effect.Add(new Effect() { name = model.name, action = model.action, complete = model.complete, description = model.description });
                db.SaveChanges();
                return true;
            }
        }
        public static void Del(string list)
        {
            using (var db = new DatabaseContext())
            {
                db.Database.ExecuteSqlCommand(string.Format("delete effect where id in ({0})", list));
            }
        }
    }
    public class NewEffectModel
    {
        [Required]
        [Display(Name = "脚本名称")]
        public string name { get; set; }

        [Required]
        [Display(Name="Action")]
        public string action { get; set; }

        [Required]
        [Display(Name="是否启用")]
        public bool complete { get; set; }

        [Required]
        [Display(Name = "脚本描述")]
        public string description { get; set; }
    }
}