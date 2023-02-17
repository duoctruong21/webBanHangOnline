using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Web;
using System.Web.Mvc;
using webBangHangOnline.Models;
using webBangHangOnline.Models.EF;

namespace webBangHangOnline.Areas.admin.Controllers
{
    public class ProductImageController : Controller
    {
        // GET: admin/ProductImage
        ApplicationDbContext db = new ApplicationDbContext();
        public ActionResult Index(int id)
        {
            var items = db.productImage.Where(x=>x.ProductId == id).ToList();
            ViewBag.productId = id;
            return View(items);
        }
        [HttpPost]
        public ActionResult AddImg(int productid, string url)
        {
            db.productImage.Add(new Models.EF.ProductImage
            {
                ProductId = productid,
                Image = url,
                IsDefault= false
            });
            db.SaveChanges();
            return Json( new {success =true}); 
        }

        [HttpPost]
        public ActionResult ChangeDefault(int id)
        {
            List<Product> product = db.products.ToList();
            List<ProductImage> productImg = db.productImage.ToList();
            List<bool> temp = new List<bool>();
            var item = db.productImage.Find(id);
            if (item.IsDefault == true)
            {
                foreach (var item2 in product)
                {
                    if (item2.Id==item.ProductId)
                    {
                        foreach(var item3 in productImg)
                        {
                            if (item3.ProductId == item2.Id)
                            {
                                for (int i = 0; i < productImg.Count; i++)
                                {
                                    if (productImg[i].IsDefault == true)
                                    {
                                        temp.Add(true);
                                    }
                                    if (item3.IsDefault == true)
                                    {
                                        if (temp.Count > 1)
                                        {
                                            item2.Image = item3.Image;
                                            item.IsDefault = false;
                                        }
                                        else
                                        {
                                            item2.Image = "";
                                            item.IsDefault = false;
                                        }                                      
                                    }                                    
                                }
                                                               
                            }
                                                        
                        }                       
                        
                    }
                }
            }
            else
            {
                foreach (var item2 in product)
                {
                    if (item2.Id == item.ProductId)
                    {
                        item2.Image = item.Image;
                        item.IsDefault = true;
                    }
                }
            }
            db.SaveChanges();
            return Json(new { success = true, IsActive = item.IsDefault });
        }

        [HttpPost]
        public ActionResult Delete(int id)
        {
            List<Product> product = db.products.ToList();
            var item = db.productImage.Find(id);
            if(item.IsDefault)
            {
                foreach(var item2 in product)
                {
                    if (item.ProductId == item2.Id)
                    {
                        item2.Image = "";
                    }
                }
            }
            db.productImage.Remove(item);
            db.SaveChanges();
            return Json(new { success = true});
        }
    }
}