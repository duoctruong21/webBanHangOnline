﻿using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using webBangHangOnline.Models;
using webBangHangOnline.Models.EF;

namespace webBangHangOnline.Areas.admin.Controllers
{
    [Authorize]
    public class NewsController : Controller
    {
        // GET: admin/News
        ApplicationDbContext db = new ApplicationDbContext();
        public ActionResult Index(string searchText,int? page)
        {

            var pageSize = 10;
            if(page == null)
            {
                page = 1;
            }
            IEnumerable<News> items = db.news.OrderByDescending(x => x.Id);
            if (!string.IsNullOrEmpty(searchText))
            {
                items = items.Where(x => x.Alias.Contains(searchText) || webBangHangOnline.Models.Common.Fillter.BoDau(x.Title).ToLowerInvariant().Contains(webBangHangOnline.Models.Common.Fillter.BoDau(searchText.ToLowerInvariant())));
            }
            var pageIndex =page.HasValue ? Convert.ToInt32(page) : 1;
            items = items.ToPagedList(pageIndex,pageSize);
            ViewBag.PageSize = pageSize;
            ViewBag.Page = page;
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
                List<Category> category = db.categories.ToList();
                model.Alias = webBangHangOnline.Models.Common.Fillter.LocDau(model.Title);
                foreach(var item in category)
                {
                    if(webBangHangOnline.Models.Common.Fillter.BoDau(item.Title).ToLowerInvariant() == webBangHangOnline.Models.Common.Fillter.BoDau("Bài viết").ToLowerInvariant())
                    {
                        model.CategoryId = item.Id;
                    }
                }
                if(model.SeoTitle == null || model.SeoTitle == "")
                {
                    model.SeoTitle = model.Title;
                }
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
        [HttpPost]
        public ActionResult DeleteAll(string ids)
        {
            if (!string.IsNullOrEmpty(ids))
            {
                var items = ids.Split(',');
                if(items != null && items.Any())
                {
                    foreach(var item in items)
                    {
                        var obj =db.news.Find(Convert.ToInt32(item));
                        db.news.Remove(obj);
                        db.SaveChanges();
                    }
                }
                return Json(new { success = true });
            }
            return Json(new {success = false});
        }
    }
}