using PagedList;
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
    public class OrdersController : Controller
    {
        // GET: admin/Orders
        private ApplicationDbContext db = new ApplicationDbContext();
        private bool check = true;
        public ActionResult Index(int? page, string typePayment)
        {
            IEnumerable<Order> item = db.orders.OrderByDescending(x => x.CreatedDate);
            var pageSize = 10;
            if(page == null)
            {
                page = 1;
            }
            if (!string.IsNullOrEmpty(typePayment))
            {
                if (typePayment.Equals("1"))
                {
                    check = false;
                    item = item.Where(x => x.TypePayment.ToString().Contains(typePayment));
                }
                else
                {
                    item = item.Where(x => x.TypePayment.ToString().Contains(typePayment));
                    check = true;
                }
            }
            var pageIndex = page.HasValue? Convert.ToInt32(page) : 1;
            item = item.ToPagedList(pageIndex, pageSize);
            ViewBag.PageSize = pageSize;
            ViewBag.Page = page;
            ViewBag.check = check;
            return View(item);
        }

        public ActionResult View(int id)
        {
            var item = db.orders.Find(id);

            return View(item);
        }

        public ActionResult PartialOrderDetail(int id)
        {
            var item = db.ordersDetail.Where(x => x.OrderId ==id);
            return PartialView(item);
        }
        [HttpPost]
        public ActionResult UpdateTT(int id, int trangthai) {
            var item = db.orders.Find(id);
            if (item != null)
            {
                db.orders.Attach(item);
                item.TypePayment= trangthai;
                db.Entry(item).Property(x => x.TypePayment).IsModified = true;
                db.SaveChanges();
                return Json(new { message = "success", success = true });
            }
            return Json(new { message = "success", success = true });
        }
    }
}