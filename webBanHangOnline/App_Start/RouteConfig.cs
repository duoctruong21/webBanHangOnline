using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace webBangHangOnline
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Contact",
                url: "lien-he",
                defaults: new { controller = "Contact", action = "Index", alias = UrlParameter.Optional },
                namespaces: new[] { "webBangHangOnline.Controllers" }
            );

            routes.MapRoute(
              name: "ShoppingCart",
              url: "gio-hang",
              defaults: new { controller = "ShoppingCart", action = "Index", alias = UrlParameter.Optional },
              namespaces: new[] { "webBangHangOnline.Controllers" }
            );

            routes.MapRoute(
              name: "CheckOutSuccess",
              url: "thanh-cong",
              defaults: new { controller = "ShoppingCart", action = "CheckOutSuccess", alias = UrlParameter.Optional },
              namespaces: new[] { "webBangHangOnline.Controllers" }
            );

            routes.MapRoute(
              name: "CheckOut",
              url: "thanh-toan",
              defaults: new { controller = "ShoppingCart", action = "CheckOut", alias = UrlParameter.Optional },
              namespaces: new[] { "webBangHangOnline.Controllers" }
            );


            routes.MapRoute(
               name: "CategoryProduct",
               url: "danh-muc-san-pham/{alias}-{id}",
               defaults: new { controller = "Product", action = "ProductCategory", alias = UrlParameter.Optional },
               namespaces: new[] { "webBangHangOnline.Controllers" }
            );

            routes.MapRoute(
               name: "DetailsProduct",
               url: "chi-tiet-san-pham/{alias}-{id}",
               defaults: new { controller = "Product", action = "Details", alias = UrlParameter.Optional },
               namespaces: new[] { "webBangHangOnline.Controllers" }
            );           

            routes.MapRoute(
                name: "Product",
                url: "san-pham",
                defaults: new { controller = "Product", action = "Index", alias = UrlParameter.Optional },
                namespaces: new[] { "webBangHangOnline.Controllers" }
            );

            routes.MapRoute(
                name: "News",
                url: "tin-tuc",
                defaults: new { controller = "News", action = "Index", alias = UrlParameter.Optional },
                namespaces: new[] { "webBangHangOnline.Controllers" }
            );

            routes.MapRoute(
                name: "DetailNews",
                url: "tin-tuc/{alias}-{id}",
                defaults: new { controller = "News", action = "Details", alias = UrlParameter.Optional },
                namespaces: new[] { "webBangHangOnline.Controllers" }
            );

            routes.MapRoute(
                name: "Post",
                url: "bai-viet",
                defaults: new { controller = "Post", action = "Index", alias = UrlParameter.Optional },
                namespaces: new[] { "webBangHangOnline.Controllers" }
            );

            routes.MapRoute(
                name: "PostDetails",
                url: "bai-viet/{alias}-{id}",
                defaults: new { controller = "Post", action = "Details", alias = UrlParameter.Optional },
                namespaces: new[] { "webBangHangOnline.Controllers" }
            );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional },
                namespaces: new[] { "webBangHangOnline.Controllers" }
            );

        }
        
    }
}
