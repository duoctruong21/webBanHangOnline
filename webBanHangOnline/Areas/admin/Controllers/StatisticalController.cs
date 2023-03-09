using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using webBangHangOnline.Models;

namespace webBangHangOnline.Areas.admin.Controllers
{
    public class StatisticalController : Controller
    {

        private ApplicationDbContext db = new ApplicationDbContext();
        // GET: admin/Statistical
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ActionResult GetStatistical(string fromDate, string toDate) {
            var query = from o in db.orders
                        join od in db.ordersDetail on o.Id equals od.OrderId
                        join p in db.products on od.ProductId equals p.Id
                        select new
                        {
                            createDate = o.CreatedDate,
                            quantity = od.Quantity,
                            price = od.Price
                        };
            if (!string.IsNullOrEmpty(fromDate))
            {
                DateTime startDate = DateTime.ParseExact(fromDate, "dd/MM/yyy", null);
                query = query.Where(x=>x.createDate >= startDate);
            }

            if (!string.IsNullOrEmpty(toDate))
            {
                DateTime endDate = DateTime.ParseExact(toDate, "dd/MM/yyy", null);
                query = query.Where(x => x.createDate < endDate);
            }

            var rs = query.GroupBy(x=>DbFunctions.TruncateTime(x.createDate)).Select(x=>new
            {
                Date = x.Key.Value,
                TotalSell = x.Sum(y=>y.quantity * y.price),
            }).Select(x=>new
            {
                Date =x.Date,
                Danhthu = x.TotalSell
            });

            return Json(new { data = rs }, JsonRequestBehavior.AllowGet);
        }
    }
}