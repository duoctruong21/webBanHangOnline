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
            var items = db.categories.Where(x=>x.isActive).OrderBy(x=>x.Position).ToList();
            return PartialView("_MenuTop",items);
        }

        public ActionResult MenuProductCategory() {
            var items = db.productsCategory.Where(x => x.isActive && x.isDeleted == false).ToList();
            return PartialView("_MenuProductCategory", items);
        }
        public ActionResult MenuLeft(int? id)
        {
            if (id != null)
            {
                ViewBag.CateId = id;
            }
            var items = db.productsCategory.Where(x => x.isActive && x.isDeleted == false).ToList();
            return PartialView("_MenuLeft", items);
        }

        public ActionResult MenuArrivals()
        {
            var items = db.productsCategory.Where(x=>x.isActive && x.isDeleted == false).Take(4).ToList();
            return PartialView("_MenuArrivals", items);
        }

        
    }
}