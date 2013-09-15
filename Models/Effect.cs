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
    }

    public class EffectNewModel
    {
        [Required]
        [Display(Name = "特效名称")]
        public string name { get; set; }

        [Required]
        [Display(Name = "特效代码")]
        public string code { get; set; }
    }
}