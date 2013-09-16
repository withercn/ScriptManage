using System;
using System.Data;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace ScriptManage.Models
{
    public class DatabaseContext:DbContext
    {
        public DatabaseContext() : base("Scripts") { }
        public DbSet<Users> Users { get; set; }
        public DbSet<Sites> Sites { get; set; }
        public DbSet<Scripts> Scripts { get; set; }
        public DbSet<ScriptsCode> ScriptsCode { get; set; }
        public DbSet<Logs> Logs { get; set; }
        public DbSet<CodeTypes> CodeTypes { get; set; }
        public DbSet<Effect> Effect { get; set; }
    }
    [Table("Users")]
    public class Users
    {
        [Key]
        [DatabaseGenerated(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.Identity)]
        public int id { get; set; }
        public string username { get; set; }
        public string password { get; set; }
        public string role { get; set; }
    }
    [Table("Logs")]
    public class Logs
    {
        [Key]
        [DatabaseGenerated(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.Identity)]
        public int id { get; set; }
        public string logs { get; set; }
        public DateTime dates { get; set; }
    }
    [Table("Sites")]
    public class Sites
    {
        [Key]
        [DatabaseGenerated(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.Identity)]
        public int id { get; set; }
        public string domain { get; set; }
        public string name { get; set; }
    }
    [Table("Scripts")]
    public class Scripts
    {
        [Key]
        [DatabaseGenerated(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.Identity)]
        public int id { get; set; }
        public string name { get; set; }
        /// <summary>
        /// 网站id
        /// </summary>
        public int sid { get; set; }
        /// <summary>
        /// 脚本类型
        /// 0、本地脚本(多个本地脚本打包)
        /// 1、远程脚本
        /// </summary>
        public int type { get; set; }
        public bool del { get; set; }
        public bool locks { get; set; }
        public DateTime dates { get; set; }
    }
    [Table("ScriptsCode")]
    public class ScriptsCode
    {
        [Key]
        [DatabaseGenerated(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.Identity)]
        public int id { get; set; }
        public string code { get; set; }
        public DateTime dates { get; set; }
        public int sid { get; set; }
    }
    [Table("CodeType")]
    public class CodeTypes
    {
        public int id { get; set; }
        [Key]
        public string name { get; set; }
    }
    [Table("Effect")]
    public class Effect
    {
        [DatabaseGenerated(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.Identity)]
        public int id { get; set; }
        [Key]
        public string name { get; set; }
        public bool complete { get; set; }
        public string action { get; set; }
        public string description { get; set; }
    }
}