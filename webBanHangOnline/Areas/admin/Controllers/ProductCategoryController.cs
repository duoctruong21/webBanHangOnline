using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using webBangHangOnline.Models.EF;
using webBangHangOnline.Models;
using System.Text.RegularExpressions;

namespace webBangHangOnline.Areas.admin.Controllers
{
    [Authorize(Roles = "admin")]
    public class ProductCategoryController : Controller
    {
        // GET: admin/ProductCategory
        ApplicationDbContext db = new ApplicationDbContext();
        // GET: admin/Category
        public ActionResult Index()
        {
            var items = db.productsCategory;
            return View(items);
        }

        public ActionResult Add()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Add(ProductCategory model)
        {
            if (ModelState.IsValid)
            {
                model.CreatedDate = DateTime.Now;
                model.ModifierDate = DateTime.Now;
                model.Alias = Regex.Replace(model.Title, "[^\\p{L}\\p{N}]", "").ToLowerInvariant();
                db.productsCategory.Add(model);
                db.SaveChanges();
                return RedirectToAction("index");
            }
            return View(model);
        }

        public ActionResult Edit(int id)
        {
            var item = db.productsCategory.Find(id);
            return View(item);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ProductCategory model)
        {
            if (ModelState.IsValid)
            {
                db.productsCategory.Attach(model);
                model.ModifierDate = DateTime.Now;
                //model.Alias = webBangHangOnline.Models.Common.Fillter.LocDau(model.Title).ToLowerInvariant();
                /*db.Entry(model).Property(x => x.Title).IsModified = true;
                db.Entry(model).Property(x => x.Description).IsModified = true;
                db.Entry(model).Property(x => x.Position).IsModified = true;
                db.Entry(model).Property(x => x.SeoKeywords).IsModified = true;
                db.Entry(model).Property(x => x.SeoDescription).IsModified = true;
                db.Entry(model).Property(x => x.SeoTitle).IsModified = true;
                db.Entry(model).Property(x => x.Alias).IsModified = true;
                db.Entry(model).Property(x => x.ModifierBy).IsModified = true;
                db.Entry(model).Property(x => x.ModifierDate).IsModified = true;*/
                model.Alias = Regex.Replace(model.Title, "[^\\p{L}\\p{N}]", "").ToLowerInvariant();

                db.Entry(model).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("index");
            }
            return View(model);
        }

        public ActionResult Delete(int id)
        {
            var item = db.productsCategory.Find(id);
            if (item != null)
            {
                //var DeleteItem = db.categories.Attach(item);
                db.productsCategory.Remove(item);
                db.SaveChanges();
                return Json(new { success = true });
            }
            return Json(new { success = false });
        }

        public ActionResult IsActive(int id)
        {
            var item = db.productsCategory.Find(id);
            if (item != null)
            {
                item.isActive = !item.isActive;
                db.Entry(item).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return Json(new { success = true, IsActive = item.isActive });
            }
            return Json(new { success = false });
        }
    }
}