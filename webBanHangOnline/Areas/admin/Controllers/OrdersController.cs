using Microsoft.Ajax.Utilities;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using PagedList;
using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Common.CommandTrees.ExpressionBuilder;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Mime;
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
        public ActionResult Index(int? page)
        {
            IEnumerable<Order> item = db.orders.Where(x => x.TypePayment == 1).OrderByDescending(x => x.CreatedDate);
            var pageSize = 10;
            if (page == null)
            {
                page = 1;
            }
            var pageIndex = page.HasValue ? Convert.ToInt32(page) : 1;
            item = item.ToPagedList(pageIndex, pageSize);
            ViewBag.PageSize = pageSize;
            ViewBag.Page = page;
            return View(item);
        }

        public ActionResult ViewPaied(int? page)
        {
            IEnumerable<Order> item = db.orders.Where(x=>x.TypePayment==2).OrderByDescending(x => x.CreatedDate);
            var pageSize = 10;
            if (page == null)
            {
                page = 1;
            }
            var pageIndex = page.HasValue ? Convert.ToInt32(page) : 1;
            item = item.ToPagedList(pageIndex, pageSize);
            ViewBag.PageSize = pageSize;
            ViewBag.Page = page;
            return View(item);
        }

        /*public ActionResult unViewPaied(int? page)
        {
            IEnumerable<Order> item = db.orders.Where(x => x.TypePayment == 1).OrderByDescending(x => x.CreatedDate);
            var pageSize = 10;
            if (page == null)
            {
                page = 1;
            }
            var pageIndex = page.HasValue ? Convert.ToInt32(page) : 1;
            item = item.ToPagedList(pageIndex, pageSize);
            ViewBag.PageSize = pageSize;
            ViewBag.Page = page;
            return View(item);
        }*/

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

        public ActionResult ExportFile()
        {
            var items = db.orders.Where(x => x.TypePayment == 2).OrderByDescending(x => x.CreatedDate);
            // Tạo 1 package của epplus
            var package = new ExcelPackage();
            // Tạo file wordsheet
            var wordsheet = package.Workbook.Worksheets.Add("Danh sách danh thu");
            wordsheet.Cells[1, 1].Value = "Mã đơn hàng";
            wordsheet.Cells[1, 2].Value = "Họ và tên khách hàng";
            wordsheet.Cells[1, 3].Value = "Địa chỉ email";
            wordsheet.Cells[1, 4].Value = "Số điện thoại";
            wordsheet.Cells[1, 5].Value = "Thành tiền";
            wordsheet.Cells[1, 6].Value = "Ngày thanh toán";
            int row = 2;
            foreach (var item in items)
            {                
                // Thêm dữ liệu vào wordsheet
                wordsheet.Cells[row, 1].Value = item.Code;
                wordsheet.Cells[row, 2].Value = item.CustomerName;
                wordsheet.Cells[row, 3].Value = item.Email;
                wordsheet.Cells[row, 4].Value = item.Phone;
                wordsheet.Cells[row, 5].Value = item.TotalAmount;
                wordsheet.Cells[row, 6].Value = item.CreatedDate;
                row++;
            }
            // Chỉnh kích thước các cột 
            wordsheet.Column(1).Width = 20;
            wordsheet.Column(2).Width = 25;
            wordsheet.Column(3).Width = 27;
            wordsheet.Column(4).Width = 15;
            wordsheet.Column(5).Width = 15;
            wordsheet.Column(6).Width = 15;

            // định dạng giá trị trong cột
            var headerRange = wordsheet.Cells["A1:F1"];
            headerRange.Style.Font.Bold = true;
            headerRange.Style.Fill.PatternType = ExcelFillStyle.Solid;
            headerRange.Style.Fill.BackgroundColor.SetColor(Color.Aqua);

            //var dataRange = wordsheet.Cells[2, 1, row - 1, 6]; 
            // tham số đầu là hàng đầu tiên,
            // tham số 2 là cột đầu tiên của ô dữ liệu
            // tham số 3 row - 1 chỉ số hàng cuối cùng của ô dữ liệu 
            // tham số 4 là chỉ số cột
            /*dataRange.Style.Font.Name = "Arial";
            dataRange.Style.Font.Size = 10;
            dataRange.Style.Numberformat.Format = "#,##0.00";*/

            wordsheet.Column(5).Style.Numberformat.Format = "#,##0 VNĐ";
            wordsheet.Column(6).Style.Numberformat.Format = "mm-dd-yyyy";



            // Lưu file
            var stream = new MemoryStream(package.GetAsByteArray());
            var fileName = "data.xlsx";
            var contentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            return File(stream, contentType, fileName);
        }
    }
}