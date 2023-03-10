using System.Web.Mvc;

namespace webBangHangOnline.Areas.admin
{
    public class adminAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "admin";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "admin_default",
                "admin/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional },
                namespaces: new[] { "webBangHangOnline.Areas.admin.Controllers" }
            );

            context.MapRoute(
                "admin",
                "admin/{action}/{id}",
                new { controller = "Home" ,action = "Index", id = UrlParameter.Optional },
                namespaces: new[] { "webBangHangOnline.Areas.admin.Controllers" }
            );

        }
    }
}