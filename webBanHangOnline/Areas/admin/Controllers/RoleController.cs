using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using webBangHangOnline.Models;

namespace webBangHangOnline.Areas.admin.Controllers
{
    [Authorize(Roles ="admin")]
    public class RoleController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        // GET: admin/Role
        public ActionResult Index()
        {
            var items = db.Roles.ToList();
            return View(items);
        }

        public ActionResult Create() { 
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]

        public ActionResult Create(IdentityRole model)
        {
            if(ModelState.IsValid)
            {
                var roleMessage = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(db));
                roleMessage.Create(model);
                return RedirectToAction("Index");
            }
            return View(model);
        }

        public ActionResult Edit(string id)
        {
            var item = db.Roles.Find(id);
            return View(item);
        }
        [HttpPost]
        public ActionResult Edit(IdentityRole item)
        {
            if (ModelState.IsValid)
            {
                db.Roles.Attach(item);
                db.Entry(item).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(item);
        }
        [HttpPost]
        public ActionResult Delete(string id)
        {
            var item = db.Roles.Find(id);
            if(item != null)
            {
                db.Roles.Remove(item);
                db.SaveChanges();
                return Json(new {success = true});
            }
            return Json(new { success = false });
        }
    }
}