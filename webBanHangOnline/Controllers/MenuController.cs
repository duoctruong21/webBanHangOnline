using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using webBangHangOnline.Models;

namespace webBangHangOnline.Controllers
{
    public class MenuController : Controller
    {
        // GET: Menu
        ApplicationDbContext db = new ApplicationDbContext();
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult MenuTop()
        {
            var items = db.categories.OrderBy(x=>x.Position).ToList();
            return PartialView("_MenuTop",items);
        }

        public ActionResult MenuProductCategory() {
            var items = db.productsCategory.ToList();
            return PartialView("_MenuProductCategory", items);
        }

        public ActionResult MenuArrivals()
        {
            var items = db.productsCategory.ToList();
            return PartialView("_MenuArrivals", items);
        }
    }
}