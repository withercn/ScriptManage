using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using System.Web.Mvc.Ajax;

namespace ScriptManage.Models
{
    public class HtmlPager
    {
        public long TotalRecord { get; set; }
        public int PageSize { get; set; }
        public int PageIndex { get; set; }
        public int PageTotal { get; set; }
        public int First { get; set; }
        public int Prev { get; set; }
        public int Next { get; set; }
        public int Last { get; set; }
        public int Start { get; set; }
        public int End { get; set; }
        public string Conntroller { get; set; }
        public string Action { get; set; }

        public HtmlPager(int total, int pageindex, int pagesize)
        {
            this.TotalRecord = total;
            this.PageSize = pagesize;
            this.PageIndex = pageindex;
            this.PageTotal = int.Parse((this.TotalRecord / this.PageSize).ToString());
            if (this.TotalRecord % this.PageSize != 0) this.PageTotal++;

            this.First = 1;
            this.Prev = (pageindex > 1) ? (pageindex - 1) : 1;
            this.Next = (pageindex < this.PageTotal) ? (pageindex + 1) : this.PageTotal;
            this.Last = this.PageTotal;

            this.Start = this.First;
            this.End = this.Last;
            for (var i = 3; i >= 1; i--)
            {
                if ((this.PageIndex - this.First) >= i)
                {
                    this.Start = this.PageIndex - i;
                    break;
                }
            }
            for (var i = 3; i >= 1; i--)
            {
                if ((this.PageIndex + i) <= this.PageTotal)
                {
                    this.End = this.PageIndex + i;
                    break;
                }
            }
        }
    }
}