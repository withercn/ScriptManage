using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.Optimization;
using System.Text;
using System.Security.Cryptography;
using System.Web.Mvc;
using System.Web;

namespace ScriptManage
{
    public class Model
    {
        public static string GetScript(string scriptName,params string[] scriptBlocks)
        {
            BundleTable.Bundles.Add(new Bundle(scriptName, new JsMinify()).Include(scriptBlocks));
            return Scripts.Render(scriptName).ToHtmlString();
        }

        public static string CreateMD5String(string desString)
        {
            MD5 md5 = MD5.Create();
            byte[] des = Encoding.UTF8.GetBytes(desString);
            byte[] hash = md5.ComputeHash(des);
            md5.Clear();
            StringBuilder builder = new StringBuilder();
            foreach (var item in hash)
                builder.Append(item.ToString("x2"));
            return builder.ToString();
        }
        public static bool CheckDataSet(DataSet ds)
        {
            return ((ds != null) && ((ds.Tables.Count > 0) && (ds.Tables[0].Rows.Count > 0)));
        }
        public static void ResponseAjaxHtml(string context)
        {
            HttpContext.Current.Response.ContentType = "text/html";
            HttpContext.Current.Response.ExpiresAbsolute = System.DateTime.Now.AddSeconds(-1);
            HttpContext.Current.Response.Expires = 0;
            HttpContext.Current.Response.AddHeader("pragma", "no-cache");
            HttpContext.Current.Response.CacheControl = "no-cache";
            HttpContext.Current.Response.Write(context);
        }
    }
}