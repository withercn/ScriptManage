using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ScriptManage.Models
{
    public class Sites
    {
        public static List<Site> GetSite()
        {
            using (var db = new DatabaseContext())
            {
                var query = db.Site.OrderBy(site => site.id).ToList();
                return query;
            }
        }
    }
}