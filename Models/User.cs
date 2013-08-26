using System;
using System.Data;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace ScriptManage.Models
{
    public class UserModel
    {
        public static bool ValidUser(string username,string password)
        {
            using (var db = new DatabaseContext())
            {
                var query = db.Users.FirstOrDefault(u => (u.username == username && u.password == password));
                if (query != null)
                    return true;
                return false;
            }
        }
        public static string[] GetRoles(string username)
        {
            using (var db = new DatabaseContext())
            {
                var user = db.Users.FirstOrDefault(u => u.username == username);
                return new string[] { user.role.name };
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