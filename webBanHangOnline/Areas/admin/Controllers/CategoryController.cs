using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using webBangHangOnline.Models;
using webBangHangOnline.Models.EF;

namespace webBangHangOnline.Areas.admin.Controllers
{
    public class CategoryController : Controller
    {
        ApplicationDbContext _dbcontext = new ApplicationDbContext();
        // GET: admin/Category
        public ActionResult Index()
        {
            var items = _dbcontext.categories;
            return View(items);
        }

        public ActionResult Add()
        {
            return View(); 
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Add(Category model)
        {
            if(ModelState.IsValid) {
                model.CreatedDate= DateTime.Now;
                model.ModifierDate= DateTime.Now;
                model.Alias = webBangHangOnline.Models.Common.Fillter.LocDau(model.Title);
                _dbcontext.categories.Add(model);
                _dbcontext.SaveChanges();
                return RedirectToAction("index");
            }
            return View(model);
        }

        public ActionResult Edit (int id)
        {
            var item = _dbcontext.categories.Find(id);
            return View(item);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Category model)
        {
            if (ModelState.IsValid)
            {
                _dbcontext.categories.Attach(model);
                model.ModifierDate = DateTime.Now;
                model.Alias = webBangHangOnline.Models.Common.Fillter.LocDau(model.Title);
                _dbcontext.Entry(model).Property(x => x.Title).IsModified = true;
                _dbcontext.Entry(model).Property(x => x.Description).IsModified = true;
                _dbcontext.Entry(model).Property(x => x.Position).IsModified = true;
                _dbcontext.Entry(model).Property(x => x.SeoKeywords).IsModified = true;
                _dbcontext.Entry(model).Property(x => x.SeoDescription).IsModified = true;
                _dbcontext.Entry(model).Property(x => x.SeoTitle).IsModified = true;
                _dbcontext.Entry(model).Property(x => x.Alias).IsModified = true;
                _dbcontext.Entry(model).Property(x => x.ModifierBy).IsModified = true;
                _dbcontext.Entry(model).Property(x => x.ModifierDate).IsModified = true;
                _dbcontext.SaveChanges();
                return RedirectToAction("index");
            }
            return View(model);
        }
    }
}