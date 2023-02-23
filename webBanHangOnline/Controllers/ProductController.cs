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
        public ActionResult Index(int? id)
        {
            var items = db.products.ToList();
            if(id != null)
            {
                items = items.Where(x => x.ProductCategoryId == id).ToList();
            }
            return View(items);
        }

        public ActionResult ProductCategory(string alias, int? id)
        {
            var items = db.products.ToList();
            if (id > 0)
            {
                items = items.Where(x => x.ProductCategoryId == id).ToList();
            }
            var cate = db.productsCategory.Find(id);
            if(cate != null)
            {
                ViewBag.CateName = cate.Title;

            }

            ViewBag.CateId = id;
            return View(items);
        }

        public ActionResult Details(string alias,int id) {
            var items = db.products.Find(id);
            if(items != null)
            {
                db.products.Attach(items);
                items.ViewCount = items.ViewCount + 1;
                db.Entry(items).Property(x=>x.ViewCount).IsModified = true;
                db.SaveChanges();

            }
            return View(items);
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