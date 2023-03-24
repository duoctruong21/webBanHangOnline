using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using webBangHangOnline.Models;
using webBangHangOnline.Models.EF;

namespace webBangHangOnline.Controllers
{
    public class HomeController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Partial_Subcribe()
        {
            return PartialView();
        }

        [HttpPost]
        public ActionResult Subcribe(Subcribe req)
        {
            if(ModelState.IsValid)
            {
                db.subcribe.Add(new Subcribe { Email = req.Email, CreatedDate = DateTime.Now});
                db.SaveChanges();
                return Json(new {success = true});
            }
            return View("Partial_Subcribe", req);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult ReFresh()
        {
            var item = new StatisticalModel();
            ViewBag.visitors_online = HttpContext.Application["visitors_online"];
            item.homnay = HttpContext.Application["homnay"].ToString();
            item.homqua = HttpContext.Application["homqua"].ToString();
            item.tuannay = HttpContext.Application["tuannay"].ToString();
            item.tuantruoc = HttpContext.Application["tuantruoc"].ToString();
            item.thangnay = HttpContext.Application["thangnay"].ToString();
            item.thangtruoc = HttpContext.Application["thangtruoc"].ToString();
            item.tongso = HttpContext.Application["tongso"].ToString();
            return PartialView(item);
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult partialAccount()
        {
            if (User.Identity.Name != "")
            {
                var items = db.Users.SingleOrDefault(x => x.Email.Contains(User.Identity.Name));
                var roleName = from user in items.Roles
                               join role in db.Roles on user.RoleId equals role.Id
                               select new
                               {
                                   role = role.Name,
                                   hovaten = items.Fullname,                                   

                               };
                foreach (var item in roleName) {
                    ViewBag.role = item.role;
                    ViewBag.hovaten = item.hovaten;
                }
                return PartialView(roleName);
            }
            return PartialView();

        }

    }
}