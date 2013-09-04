using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web.Mvc;
using System.Net;
using System.IO;
using System.Web;

namespace ScriptManage.Models
{
    public class SiteModel
    {
        public static void DelSite(string id)
        {
            using (var db = new DatabaseContext())
            {
                db.Database.ExecuteSqlCommand(string.Format("delete sites where id in ({0})", id));
            }
        }
        public static bool NewSite(Sites site)
        {
            using (var db = new DatabaseContext())
            {
                var query = db.Sites.FirstOrDefault(s => s.domain == site.domain);
                if (query == null)
                {
                    db.Sites.Add(site);
                    db.SaveChanges();
                    if (!Directory.Exists(HttpContext.Current.Server.MapPath("/block/" + site.domain)))
                        Directory.CreateDirectory(HttpContext.Current.Server.MapPath("/block/" + site.domain));
                    return true;
                }
                return false;
            }
        }
    }

    public class Domain : ValidationAttribute
    {
        public const string DefaultErrorMessage = "{0}必须是一个可访问的域名";
        public Domain() : base(DefaultErrorMessage) { }
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value != null)
            {
                try
                {
                    IPHostEntry host = Dns.GetHostEntry(value.ToString());
                }
                catch { return new ValidationResult(FormatErrorMessage(validationContext.DisplayName)); }
            }
            return ValidationResult.Success;
        }
    }
    public class SiteNewModel
    {
        [Required]
        [Display(Name = "域名")]
        [DataType(DataType.Url)]
        [Domain]
        public string domain { get; set; }

        [Required]
        [Display(Name = "网站名称")]
        public string name { get; set; }
    }
}