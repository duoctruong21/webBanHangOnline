using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using webBangHangOnline.Models;

namespace webBangHangOnline.Controllers
{
    public class ProductController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        // GET: Product
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Partial_ItemsByCateId()
        {
            var items = db.products.Where(x=>x.IsHome && x.isActive).Take(15).ToList();
            return PartialView(items);
        }

        public ActionResult Partial_ProductSales()
        {
            var items = db.products.Where(x => x.IsSale && x.isActive).Take(15).ToList();
            return PartialView(items);
        }
    }
}