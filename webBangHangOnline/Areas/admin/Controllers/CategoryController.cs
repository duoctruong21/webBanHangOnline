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
    }
}