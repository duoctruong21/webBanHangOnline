using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace webBangHangOnline.Models
{
    public abstract class CommonAbstract
    {
        public DateTime CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public DateTime ModifierDate { get; set; }
        public string ModifierBy { get; set; }
    }
}