using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace webBangHangOnline.Models
{
    public class StatisticalViewModel
    {
        public int homnay { get; set; }
        public int homqua { get; set; }
        public int tuannay { get; set; }
        public int tuantruoc { get; set; }
        public int thangnay { get; set; }
        public int thangtruoc { get; set; }
        public int tongso { get; set; }
    }

    public class StatisticalModel
    {
        public string homnay { get; set; }
        public string homqua { get; set; }
        public string tuannay { get; set; }
        public string tuantruoc { get; set; }
        public string thangnay { get; set; }
        public string thangtruoc { get; set; }
        public string tongso { get; set; }
    }
}