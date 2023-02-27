using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using webBangHangOnline.Models;

namespace webBangHangOnline.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult ReFresh()
        {
            var item = new StatisticalModel();
            ViewBag.visitors_online = HttpContext.Application["visitors_online"];
            item.homnay = HttpContext.Application["homnay"].ToString();
            item.homqua = HttpContext.Application["homqua"].ToString();
            item.tuannay = HttpContext.Application["tuannay"].ToString();
            item.tuantruoc = HttpContext.Application["tuantruoc"].ToString();
            item.thangnay = HttpContext.Application["thangnay"].ToString();
            item.thangtruoc = HttpContext.Application["thangtruoc"].ToString();
            item.tatca = HttpContext.Application["tatca"].ToString();
            return PartialView(item);
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}