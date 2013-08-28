using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ScriptManage.Models
{
    public class LogModel
    {
        public static void Write(string msg)
        {
            using (var db = new DatabaseContext())
            {
                var log = new Logs() { logs = msg, dates = DateTime.Now };
                db.Logs.Add(log);
                db.SaveChanges();
            }
        }
        public static void Clear()
        {
            using (var db = new DatabaseContext())
            {
                db.Database.ExecuteSqlCommand("delete logs");
            }
        }
    }
}