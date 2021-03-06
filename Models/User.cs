﻿using System;
using System.Collections.Generic;
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
        public static bool ChangePassword(string olds, string news)
        {
            using (var db = new DatabaseContext())
            {
                var username = System.Web.HttpContext.Current.User.Identity.Name;
                var password = Model.CreateMD5String(olds);
                var user = db.Users.FirstOrDefault(u => (u.username == username && u.password == password));
                if (user == null) return false;
                user.password = Model.CreateMD5String(news);
                db.SaveChanges();
                return true;
            }
        }
        public static string[] GetRoles(string username)
        {
            using (var db = new DatabaseContext())
            {
                var user = db.Users.FirstOrDefault(u => u.username == username);
                if (user == null) return null;
                return new string[] { user.role };
            }
        }
        public static Users GetUser(int userid)
        {
            using (var db = new DatabaseContext())
            {
                var user = db.Users.FirstOrDefault(u => u.id == userid);
                return user;
            }
        }
        public static Users GetUser(string username)
        {
            using (var db = new DatabaseContext())
            {
                var user = db.Users.FirstOrDefault(u => u.username == username);
                return user;
            }
        }
        public static List<Users> GetUser(int page, int pagesize)
        {
            List<Users> list = new List<Users>();
            using (var db = new DatabaseContext())
            {
                return null;
            }
        }
        public static void DeleteUser(string id)
        {
            using (var db = new DatabaseContext())
            {
                db.Database.ExecuteSqlCommand(string.Format("delete [users] where id in ({0}) and role<>'系统管理员'", id));
            }
        }
        public static bool NewUser(string username,string password)
        {
            using (var db = new DatabaseContext())
            {
                var query = db.Users.FirstOrDefault(u => u.username == username);
                if (query != null)
                    return false;
                else
                {
                    db.Users.Add(new Users() { username = username, password = Model.CreateMD5String(password), role = "管理员" });
                    db.SaveChanges();
                    return true;
                }
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
    public class RegisterModel
    {
        [Required]
        [Display(Name = "账号")]
        [StringLength(20, MinimumLength = 4, ErrorMessage = "{0} 必须至少包含 {2} 个字符。")]
        public string username { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "密码")]
        [StringLength(20, MinimumLength = 4, ErrorMessage = "{0} 必须至少包含 {2} 个字符。")]
        public string password { get; set; }

        [Required]
        [Display(Name = "重复密码")]
        [DataType(DataType.Password)]
        [Compare("password", ErrorMessage = "新密码与重复密码不相同。")]
        public string rePassword { get; set; }
    }
    public class PasswordModel
    {
        [Required]
        [Display(Name = "旧密码")]
        [DataType(DataType.Password)]
        public string oldPassword { get; set; }

        [Required]
        [Display(Name = "新密码")]
        [StringLength(20, MinimumLength = 4, ErrorMessage = "{0} 必须至少包含 {2} 个字符。")]
        [DataType(DataType.Password)]
        public string password { get; set; }

        [Required]
        [Display(Name = "重复密码")]
        [DataType(DataType.Password)]
        [Compare("password", ErrorMessage = "新密码与重复密码不相同。")]
        public string rePassword { get; set; }
    }
}