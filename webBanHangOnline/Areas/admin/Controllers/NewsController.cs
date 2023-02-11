using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using webBangHangOnline.Models;
using webBangHangOnline.Models.EF;

namespace webBangHangOnline.Areas.admin.Controllers
{
    public class NewsController : Controller
    {
        // GET: admin/News
        ApplicationDbContext db = new ApplicationDbContext();
        public ActionResult Index()
        {
            var items = db.news.OrderByDescending(x => x.Id).ToList();
            return View(items);
        }

        public ActionResult Add()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Add(News model)
        {
            if (ModelState.IsValid)
            {
                model.CreatedDate = DateTime.Now;
                model.ModifierDate = DateTime.Now;
                model.CategoryId = 12;
                model.Alias = webBangHangOnline.Models.Common.Fillter.LocDau(model.Title);
                db.news.Add(model);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(model);
        }
        public ActionResult Edit(int id)
        {
            var item = db.news.Find(id);
            return View(item);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(News model) { 
            if(ModelState.IsValid)
            {
                model.ModifierDate = DateTime.Now;
                model.Alias = webBangHangOnline.Models.Common.Fillter.LocDau(model.Title);
                db.news.Attach(model);
                db.Entry(model).State= System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(model);
        }

        public ActionResult Delete(int id)
        {
            var item = db.news.Find(id);
            if(item != null)
            {
                db.news.Remove(item);
                db.SaveChanges();
                return Json(new { success = true });
            }
            return Json(new { success = false });
        }

        public ActionResult IsActive(int id)
        {
            var item = db.news.Find(id);
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