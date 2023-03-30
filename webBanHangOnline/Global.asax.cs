using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace webBangHangOnline
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            Application["homnay"] = 0;
            Application["homqua"] = 0;
            Application["tuannay"] = 0;
            Application["tuantruoc"] = 0;
            Application["thangnay"] = 0;
            Application["thangtruoc"] = 0;
            Application["tongso"] = 0;
            Application["visitors_online"] = 0;
        }
        protected void Session_Start(object sender, EventArgs e)
        {
            Session.Timeout = 150;
            Application.Lock();
            Application["visitors_online"] = Convert.ToInt32(Application["visitors_online"]) +1;
            Application.UnLock();
            try
            {
                var item = webBangHangOnline.Models.Common.Thongke.Statistical();
                if(item != null)
                {
                    Application["homnay"] = long.Parse("0"+item.homnay.ToString("#,###"));
                    Application["homqua"] = long.Parse("0"+item.homqua.ToString("#,###"));
                    Application["tuannay"] = long.Parse("0"+item.tuannay.ToString("#,###"));
                    Application["tuantruoc"] = long.Parse("0"+item.tuantruoc.ToString("#,###"));
                    Application["thangnay"] = long.Parse("0"+item.thangnay.ToString("#,###"));
                    Application["thangtruoc"] = long.Parse("0"+item.thangtruoc.ToString("#,###"));
                    Application["tongso"] = (int.Parse(item.tongso.ToString())).ToString("#,###");
                }
            }
            catch
            {

            }
        }
        protected void Session_end(object sender, EventArgs e)
        {
            Application.Lock();
            Application["visitors_online"] = Convert.ToInt32(Application["visitors_online"]) - 1;
            Application.UnLock();
        }
    }
}
