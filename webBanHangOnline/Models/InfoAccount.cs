using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace webBangHangOnline.Models
{
    public class InfoAccount
    {
        public string role { get; set; }
        public string name { get; set; }
        public string phone { get; set; }
        public string email { get; set; }
        public string address { get; set; }
        public DateTime createdDate { get; set; }
    }
}