using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using webBangHangOnline.Models;
using webBangHangOnline.Models.EF;

namespace webBangHangOnline.Controllers
{
    public class NewsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        // GET: News
        public ActionResult Index(int? page)
        {
            IEnumerable<News> items = db.news.ToList();
            var pageSize = 10;
            if(page == null)
            {
                page = 1;
            }
            var pageIndex = page.HasValue ? Convert.ToInt32(page) : 1;
            items = items.ToPagedList(pageIndex,pageSize);
            ViewBag.PageSize = pageSize;
            ViewBag.Page = page;
            return View(items);
        }

        public ActionResult Details(int id)
        {
            var item = db.news.Find(id);
            @ViewBag.Title = item.Title;
            return View(item);
        }

        public ActionResult PartialNewsHome()
        {
            var item = db.news.Take(3).ToList();
            return PartialView(item);
        }
    }
}