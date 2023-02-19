using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using webBangHangOnline.Models;

namespace webBangHangOnline.Controllers
{
    public class ShoppingCartController : Controller
    {
        // GET: ShoppingCart
        public int count = 0;
        public ActionResult Index()
        {
            ShoppingCart cart = (ShoppingCart)Session["cart"];
            if(cart != null)
            {
                count = cart.items.Count; 
                return View(cart.items);
            }
            return View();
        }

        public ActionResult ShowCount()
        {
            ShoppingCart cart = (ShoppingCart)Session["cart"];
            if (cart != null)
            {
                return Json(new { count = cart.items.Count },JsonRequestBehavior.AllowGet);
            }
            return Json(new {count=0}, JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        public ActionResult AddToCart(int id, int quantity) {
            var code = new { success = false, msg = "", code = -1, count=0 };
            var db = new ApplicationDbContext();
            var checkProduct = db.products.FirstOrDefault(x => x.Id == id);
            if (checkProduct != null)
            {
                ShoppingCart cart = (ShoppingCart)Session["cart"];
                if(cart == null)
                {
                    cart= new ShoppingCart();
                }
                ShoppingCartItem item = new ShoppingCartItem
                {
                    ProductId = checkProduct.Id,
                    ProductName = checkProduct.Title,
                    CategoryName = checkProduct.ProductCategory.Title,
                    Quantity = quantity,
                    Alias= checkProduct.Alias
                };
                if (checkProduct.ProductImage.FirstOrDefault(x => x.IsDefault) != null)
                {
                    item.ProductImg = checkProduct.ProductImage.FirstOrDefault(x => x.IsDefault).Image;
                }
                item.Price = checkProduct.Price;
                if (checkProduct.PriceSale > 0)
                {
                    item.Price = checkProduct.PriceSale;
                }
                item.PriceTotal = item.Price * item.Quantity;
                cart.AddToCart(item, quantity);
                Session["cart"] = cart;
                code = new { success = true, msg = "Thêm sản phẩm vào giỏ hàng thành công!", code = 1, count =cart.items.Count };
            }
            return Json(code);
        }

        [HttpPost]
        public ActionResult Delete(int id)
        {
            var code = new { success = false, msg = "", code = -1, count = 0 };
            ShoppingCart cart = (ShoppingCart)Session["cart"]; 
            var checkProduct = cart.items.FirstOrDefault(x=>x.ProductId== id);
            if (cart != null) {
                cart.Remove(id);
                code = new { success = true, msg = "", code = 1, count = cart.items.Count };
            }
            return Json(code);
        }
    }
}