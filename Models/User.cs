using System;
using System.Data;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace ScriptManage.Models
{
    public class UserContext : DbContext
    {
        public UserContext() : base("Scripts") { }
        public DbSet<Users> Users { get; set; }
    }
    [Table("Users")]
    public class Users
    {
        [Key]
        [DatabaseGenerated(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.Identity)]
        public int id { get; set; }
        public string username { get; set; }
        public string password { get; set; }
        public bool flag { get; set; }
    }
    public class UserModel
    {
        public static bool ValidUser(string username,string password)
        {
            using (var db = new UserContext())
            {
                var query = db.Users.FirstOrDefault(u => (u.username == username && u.password == password));
                if (query != null)
                    return true;
                return false;
            }
        }
        public static string[] GetRoles(string username)
        {
            using (var db = new UserContext())
            {
                var user = db.Users.FirstOrDefault(u => u.username == username);
                if (user.flag)
                    return new string[] { "系统管理员", "管理员" };
                else
                    return new string[] { "管理员" };
            }
        }
    }

    public class LoginModel
    {
        [Required]
        [Display(Name = "账号")]
        public string username { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "密码")]
        public string password { get; set; }
    }
}