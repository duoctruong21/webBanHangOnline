using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using webBangHangOnline.Models;

namespace webBangHangOnline.Common
{
    public class SettingHelper
    {
        private static ApplicationDbContext db = new ApplicationDbContext();

        public static string GetValue(string key)
        {
            var items = db.systemSetting.SingleOrDefault(x=>x.SettingKey.Equals(key));
            if(items!= null)
            {
                return items.SettingValue;
            }
            return "";
        }
    }
}