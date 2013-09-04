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
        public static void Update(int id,string code)
        {
            using (var db = new DatabaseContext())
            {
                var script = db.Scripts.FirstOrDefault(s => s.id == id);
                if (script != null)
                {
                    script.code = code;
                    db.SaveChanges();
                }
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
}