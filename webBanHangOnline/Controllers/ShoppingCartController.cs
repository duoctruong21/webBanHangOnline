using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using webBangHangOnline.Models;
using webBangHangOnline.Models.EF;
using MoMo;
using Momo;

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
            if(cart != null)
            {
                var items = db.products;
                foreach (var item in items)
                {
                    foreach(var i in cart.items)
                    {
                        if(item.Id == i.ProductId)
                        {
                            i.Count = item.Quantity;
                        }
                    }
                }
                if (cart != null && cart.items.Any())
                {
                    count = cart.items.Count;
                    return PartialView(cart.items);
                }
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
            if (User.Identity.Name != null && User.Identity.Name != "")
            {
                var items = db.Users.SingleOrDefault(x => x.Email.Contains(User.Identity.Name));
                return PartialView(items);
            }
            return PartialView();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CheckOut(CustomerViewModel req)
        {
            var code = new {success = false, code = -1 };
            if (ModelState.IsValid)
            {
               if(req.TypePayment == 1)
                {
                    ShoppingCart cart = (ShoppingCart)Session["cart"];
                    if (cart != null)
                    {
                        Order order = new Order();
                        order.CustomerName = req.CustomerName;
                        order.Phone = req.Phone;
                        order.Address = req.Address;
                        order.Email = req.Email;
                        cart.items.ForEach(x => order.Details.Add(new OrderDetail
                        {
                            ProductId = x.ProductId,
                            Quantity = x.Quantity,
                            Price = x.Price,

                        }));
                        order.TotalAmount = cart.items.Sum(x => (x.Quantity * x.Price));
                        order.TypePayment = req.TypePayment;
                        order.CreatedDate = DateTime.Now;
                        order.ModifierDate = DateTime.Now;
                        order.CreatedBy = req.Phone;
                        Random rd = new Random();
                        order.Code = "DH" + rd.Next(0, 9) + rd.Next(0, 9) + rd.Next(0, 9) + rd.Next(0, 9);
                        db.orders.Add(order);
                        db.SaveChanges();
                        // cập nhật lại kho
                        var prd = db.products;
                        foreach (var product in prd)
                        {
                            foreach (var item in cart.items)
                            {
                                if (product.Id == item.ProductId)
                                {
                                    product.Quantity = product.Quantity - item.Quantity;
                                }
                            }
                        }
                        db.products.AddOrUpdate();
                        db.SaveChanges();
                        //send mail
                        var strSanpham = "";
                        decimal thanhtien = 0;
                        decimal tongtien = 0;
                        foreach (var item in cart.items)
                        {
                            strSanpham += "<tr>";
                            strSanpham += "<td>" + item.ProductName + "</td>";
                            strSanpham += "<td>" + item.Quantity + "</td>";
                            strSanpham += "<td>" + item.Price.ToString("###,###,###.##") + "</td>";
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
                        webBangHangOnline.Common.Common.SendMail("shopOnline", "Đơn hàng #" + order.Code, contentCustomer.ToString(), req.Email);

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
                else
                {
                    ShoppingCart cart = (ShoppingCart)Session["cart"];

                    Order order = new Order();
                    order.CustomerName = req.CustomerName;
                    order.Phone = req.Phone;
                    order.Address = req.Address;
                    order.Email = req.Email;
                    cart.items.ForEach(x => order.Details.Add(new OrderDetail
                    {
                        ProductId = x.ProductId,
                        Quantity = x.Quantity,
                        Price = x.Price,

                    }));
                    order.TotalAmount = cart.items.Sum(x => (x.Quantity * x.Price));
                    order.TypePayment = req.TypePayment;
                    order.CreatedDate = DateTime.Now;
                    order.ModifierDate = DateTime.Now;
                    order.CreatedBy = req.Phone;
                    Random rd = new Random();
                    order.Code = "DH" + rd.Next(0, 9) + rd.Next(0, 9) + rd.Next(0, 9) + rd.Next(0, 9);
                    db.orders.Add(order);
                    db.SaveChanges();
                    //request params need to request to MoMo system
                    string endpoint = "https://test-payment.momo.vn/gw_payment/transactionProcessor";
                    string partnerCode = "MOMONGQV20220528";
                    string accessKey = "PZrO5Yokc9hWdTEp";
                    string serectkey = "K1bZa1rR0ZYH1ZbfMwUn3ZywT8qCgzH4";
                    string orderInfo = req.CustomerName;
                    string returnUrl = "https://localhost:44394/shoppingcart/CheckOutSuccess";
                    string notifyurl = "https://localhost:44394/shoppingcart/saveOrder"; //lưu ý: notifyurl không được sử dụng localhost, có thể sử dụng ngrok để public localhost trong quá trình test

                    string amount = order.TotalAmount.ToString("###");
                    string orderid = order.Code.ToString(); //mã đơn hàng
                    string requestId = DateTime.Now.Ticks.ToString();
                    string extraData = "";

                    //Before sign HMAC SHA256 signature
                    string rawHash = "partnerCode=" +
                        partnerCode + "&accessKey=" +
                        accessKey + "&requestId=" +
                        requestId + "&amount=" +
                        amount + "&orderId=" +
                        orderid + "&orderInfo=" +
                        orderInfo + "&returnUrl=" +
                        returnUrl + "&notifyUrl=" +
                        notifyurl + "&extraData=";

                    MoMoSecurity crypto = new MoMoSecurity();
                    //sign signature SHA256
                    string signature = crypto.signSHA256(rawHash, serectkey);

                    //build body json request
                    JObject message = new JObject
            {
                    { "partnerCode", partnerCode },
                    { "accessKey", accessKey },
                    { "requestId", requestId },
                    { "amount", amount },
                    { "orderId", orderid },
                    { "orderInfo", orderInfo },
                    { "returnUrl", returnUrl },
                    { "notifyUrl", notifyurl },
                    { "requestType", "captureMoMoWallet" },
                    { "signature", signature }

            };

                    string responseFromMomo = PaymentRequest.sendPaymentRequest(endpoint, message.ToString());

                    JObject jmessage = JObject.Parse(responseFromMomo);
                    return Redirect(jmessage.GetValue("payUrl").ToString());
                }
            }
            return Json(code);
        }

        [HttpPost]
        public ActionResult saveOrder(CustomerViewModel req)
        {
            ShoppingCart cart = (ShoppingCart)Session["cart"];

            Order order = new Order();
            order.CustomerName = req.CustomerName;
            order.Phone = req.Phone;
            order.Address = req.Address;
            order.Email = req.Email;
            cart.items.ForEach(x => order.Details.Add(new OrderDetail
            {
                ProductId = x.ProductId,
                Quantity = x.Quantity,
                Price = x.Price,

            }));
            order.TotalAmount = cart.items.Sum(x => (x.Quantity * x.Price));
            order.TypePayment = req.TypePayment;
            order.CreatedDate = DateTime.Now;
            order.ModifierDate = DateTime.Now;
            order.CreatedBy = req.Phone;
            Random rd = new Random();
            order.Code = "DH" + rd.Next(0, 9) + rd.Next(0, 9) + rd.Next(0, 9) + rd.Next(0, 9);
            db.orders.Add(order);
            db.SaveChanges();
            // cập nhật lại kho
            var prd = db.products;
            foreach (var product in prd)
            {
                foreach (var item in cart.items)
                {
                    if (product.Id == item.ProductId)
                    {
                        product.Quantity = product.Quantity - item.Quantity;
                    }
                }
            }
            db.products.AddOrUpdate();
            db.SaveChanges();
            //send mail
            var strSanpham = "";
            decimal thanhtien = 0;
            decimal tongtien = 0;
            foreach (var item in cart.items)
            {
                strSanpham += "<tr>";
                strSanpham += "<td>" + item.ProductName + "</td>";
                strSanpham += "<td>" + item.Quantity + "</td>";
                strSanpham += "<td>" + item.Price.ToString("###,###,###.##") + "</td>";
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
            webBangHangOnline.Common.Common.SendMail("shopOnline", "Đơn hàng #" + order.Code, contentCustomer.ToString(), req.Email);

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
            return RedirectToAction("CheckOutSuccess");
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
                var items = db.products;
                foreach (var itm in items)
                {
                    foreach (var i in cart.items)
                    {
                        if (itm.Id == i.ProductId)
                        {
                            i.Count = itm.Quantity;
                        }
                    }
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


        //Khi thanh toán xong ở cổng thanh toán Momo, Momo sẽ trả về một số thông tin, trong đó có errorCode để check thông tin thanh toán
        //errorCode = 0 : thanh toán thành công (Request.QueryString["errorCode"])
        //Tham khảo bảng mã lỗi tại: https://developers.momo.vn/#/docs/aio/?id=b%e1%ba%a3ng-m%c3%a3-l%e1%bb%97i

    }
}