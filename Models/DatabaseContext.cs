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
        public DbSet<Site> Site { get; set; }
        public DbSet<Script> Script { get; set; }
        public DbSet<Logs> Logs { get; set; }
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
    [Table("Site")]
    public class Site
    {
        [Key]
        [DatabaseGenerated(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.Identity)]
        public int id { get; set; }
        public string domain { get; set; }
        public string name { get; set; }
    }
    [Table("Script")]
    public class Script
    {
        [Key]
        [DatabaseGenerated(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.Identity)]
        public int id { get; set; }
        public string code { get; set; }
        public int siteid { get; set; }
    }
}