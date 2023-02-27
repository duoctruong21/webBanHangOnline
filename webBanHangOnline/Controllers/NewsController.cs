using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using webBangHangOnline.Models;

namespace webBangHangOnline.Controllers
{
    public class NewsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        // GET: News
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult PartialNewsHome()
        {
            var item = db.news.Take(3).ToList();
            return PartialView(item);
        }
    }
}