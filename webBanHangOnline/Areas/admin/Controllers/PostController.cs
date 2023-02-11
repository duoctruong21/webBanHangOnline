﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using webBangHangOnline.Models;
using webBangHangOnline.Models.EF;

namespace webBangHangOnline.Areas.admin.Controllers
{
    public class PostController : Controller
    {
        ApplicationDbContext db = new ApplicationDbContext();
        // GET: admin/Post
        public ActionResult Index()
        {
            var item = db.posts;
            return View(item);
        }

        public ActionResult Add() {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Add(Post model)
        {
            if(ModelState.IsValid)
            {
                model.CreatedDate = DateTime.Now;
                model.ModifierDate = DateTime.Now;
                model.CategoryId = 12;
                model.Alias = webBangHangOnline.Models.Common.Fillter.LocDau(model.Title);
                db.posts.Add(model);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(model);
        }

        public ActionResult Edit(int id)
        {
            var items = db.posts.Find(id);
            return View(items);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Post model) {
            if (ModelState.IsValid)
            {
                db.posts.Attach(model);
                model.ModifierDate = DateTime.Now;
                model.Alias = webBangHangOnline.Models.Common.Fillter.LocDau(model.Title);
                db.Entry(model).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(model);
        }
        public ActionResult Delete(int id)
        {
            var item = db.posts.Find(id);
            if(item != null)
            {
                db.posts.Remove(item);
                db.SaveChanges();
                return Json(new { success = true });
            }
            return Json(new { success = false });
        }
        public ActionResult IsActive(int id)
        {
            var items = db.posts.Find(id);
            if(items != null)
            {
                items.isActive = !items.isActive;
                db.Entry(items).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return Json(new {success = true, IsActive = items.isActive});
            }
            return Json(new {success = false});
        }
        public ActionResult DeleteAll(string ids)
        {
            if(!string.IsNullOrEmpty(ids))
            {
                var items = ids.Split(',');
                if(items != null && items.Any())
                {
                    foreach (var item in items)
                    {
                        var obj = db.posts.Find(Convert.ToInt32(item));
                        db.posts.Remove(obj); 
                        db.SaveChanges();                        
                    }
                }
                return Json(new {success = true});
            }
            return Json(new {success = false});
        }
    }
}