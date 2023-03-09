using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using webBangHangOnline.Models;
using webBangHangOnline.Models.EF;

namespace webBangHangOnline.Controllers
{
    public class ShoppingCartController : Controller
    {
        // GET: ShoppingCart
        private ApplicationDbContext db = new ApplicationDbContext();
        public int count = 0;
        public ActionResult Index()
        {
            ShoppingCart cart = (ShoppingCart)Session["cart"];
            if (cart != null && cart.items.Any())
            {
                ViewBag.CheckCart = cart;
            }
            return View();
        }

        public ActionResult CheckOut()
        {
            ShoppingCart cart = (ShoppingCart)Session["cart"];
            if (cart != null && cart.items.Any())
            {
                ViewBag.CheckCart = cart;
            }
            return View();
        }

        public ActionResult CheckOutSuccess()
        {
            return View();
        }

        public ActionResult PartialItemPay()
        {
            ShoppingCart cart = (ShoppingCart)Session["cart"];
            if (cart != null && cart.items.Any())
            {
                count = cart.items.Count;
                return PartialView(cart.items);
            }
            return PartialView();
        }

        public ActionResult PartialItemCart()
        {
            ShoppingCart cart = (ShoppingCart)Session["cart"];
            if (cart != null && cart.items.Any() )
            {
                count = cart.items.Count;
                return PartialView(cart.items);
            }
            return PartialView();
        }

        public ActionResult PartialItemCartHome()
        {
            ShoppingCart cart = (ShoppingCart)Session["cart"];
            if (cart != null && cart.items.Any())
            {
                count = cart.items.Count;
                return PartialView(cart.items);
            }
            return PartialView();
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

        public ActionResult PartialCheckOut()
        {
            return PartialView();   
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CheckOut(CustomerViewModel req)
        {
            var code = new {success = false, code = -1 };
            if (ModelState.IsValid)
            {
                ShoppingCart cart = (ShoppingCart)Session["cart"];
                if (cart != null)
                {
                    Order order = new Order();
                    order.CustomerName= req.CustomerName;
                    order.Phone= req.Phone;
                    order.Address= req.Address;
                    order.Email = req.Email;
                    cart.items.ForEach(x => order.Details.Add(new OrderDetail
                    {
                        ProductId = x.ProductId,
                        Quantity = x.Quantity,
                        Price = x.Price,

                    }));
                    order.TotalAmount = cart.items.Sum(x=>(x.Quantity*x.Price));
                    order.TypePayment = req.TypePayment;
                    order.CreatedDate = DateTime.Now;
                    order.ModifierDate = DateTime.Now;
                    order.CreatedBy = req.Phone;
                    Random rd = new Random();
                    order.Code = "DH" + rd.Next(0,9) + rd.Next(0, 9) + rd.Next(0, 9) + rd.Next(0, 9);
                    db.orders.Add(order);
                    db.SaveChanges();
                    //send mail
                    var strSanpham = "";
                    decimal thanhtien = 0;
                    decimal tongtien = 0;
                    foreach (var item in cart.items)
                    {
                        strSanpham += "<tr>";
                        strSanpham += "<td>"+ item.ProductName +"</td>";
                        strSanpham += "<td>"+ item.Quantity +"</td>";
                        strSanpham += "<td>"+ item.Price.ToString("###,###,###.##") + "</td>";
                        strSanpham += "</tr>";
                        thanhtien += item.Price * item.Quantity;
                    }
                    tongtien = thanhtien;
                    string contentCustomer = System.IO.File.ReadAllText(Server.MapPath("~/Content/templates/send2.html"));
                    contentCustomer = contentCustomer.Replace("{{madonhang}}", order.Code);
                    contentCustomer = contentCustomer.Replace("{{sanpham}}", strSanpham);
                    contentCustomer = contentCustomer.Replace("{{tenkhachhang}}", order.CustomerName);
                    contentCustomer = contentCustomer.Replace("{{sdt}}", order.Phone);
                    contentCustomer = contentCustomer.Replace("{{diachi}}", order.Address);
                    contentCustomer = contentCustomer.Replace("{{email}}", req.Email);
                    contentCustomer = contentCustomer.Replace("{{ngaydat}}", order.CreatedDate.ToString("dd/MM/yyyy"));
                    contentCustomer = contentCustomer.Replace("{{thanhtien}}", thanhtien.ToString("###,###,###.##"));
                    contentCustomer = contentCustomer.Replace("{{tongtien}}", tongtien.ToString("###,###,###.##"));
                    webBangHangOnline.Common.Common.SendMail("shopOnline","Đơn hàng #"+order.Code,contentCustomer.ToString(),req.Email);

                    string contentAdmin = System.IO.File.ReadAllText(Server.MapPath("~/Content/templates/send1.html"));
                    contentAdmin = contentAdmin.Replace("{{madonhang}}", order.Code);
                    contentAdmin = contentAdmin.Replace("{{sanpham}}", strSanpham);
                    contentAdmin = contentAdmin.Replace("{{tenkhachhang}}", order.CustomerName);
                    contentAdmin = contentAdmin.Replace("{{sdt}}", order.Phone);
                    contentAdmin = contentAdmin.Replace("{{diachi}}", order.Address);
                    contentAdmin = contentAdmin.Replace("{{email}}", req.Email);
                    contentAdmin = contentAdmin.Replace("{{ngaydat}}", order.CreatedDate.ToString("dd/MM/yyyy"));
                    contentAdmin = contentAdmin.Replace("{{thanhtien}}", thanhtien.ToString("###,###,###.##"));
                    contentAdmin = contentAdmin.Replace("{{tongtien}}", tongtien.ToString("###,###,###.##"));
                    webBangHangOnline.Common.Common.SendMail("shopOnline", "Đơn hàng #" + order.Code, contentAdmin.ToString(), ConfigurationManager.AppSettings["EmailAdmin"]);

                    cart.clearCart();
                }
                    return RedirectToAction("CheckOutSuccess");
            }
            return Json(code);
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
        public ActionResult Update(int id, int quantity)
        {
            ShoppingCart cart = (ShoppingCart)Session["cart"];
            if (cart != null)
            {
                cart.updateQuantity(id,quantity);
                return Json(new { success = true });
            }
            return Json(new { success = false });
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

        /*public ActionResult changeQuantity(int id)
        {
            var code = new { success = false, msg = "", code = -1, count = 0 };
            ShoppingCart cart = (ShoppingCart)Session["cart"];
            var checkProduct = cart.items.FirstOrDefault(x => x.ProductId == id);
            if (cart != null)
            {
                code = new { success = true, msg = "", code = 1, count = cart.items.Count };
            }
            return Json(code);
        }*/
        [HttpPost]
        public ActionResult DeleteAll()
        {
            ShoppingCart cart = (ShoppingCart)Session["cart"];
            if (cart != null)
            {
                cart.clearCart();
                return Json(new { success = true });
            }
            return Json(new { success = false });
        }
    }
}