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
    public class SiteModel
    {
        public static void DelSite(string id)
        {
            using (var db = new DatabaseContext())
            {
                db.Database.ExecuteSqlCommand(string.Format("delete site where id in ({0})", id));
            }
        }
    }
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
    public class Domain : ValidationAttribute, IClientValidatable
    {
        public const string DefaultErrorMessage = "{0}必须是一个有效域名";
        string helpURL = "http://kms.lenovots.com/kb/article.php?id=14797";
    }
    public class SiteNewModel
    {
        [Required]
        [Display(Name = "域名")]
        [DataType(DataType.Url)]
        [Url(ErrorMessage = "必需输入有效网址")]
        public string domain { get; set; }

        [Required]
        [Display(Name = "网站名称")]
        public string name { get; set; }
    }
}