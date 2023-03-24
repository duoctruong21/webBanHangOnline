using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using webBangHangOnline.Models;

namespace webBangHangOnline.Areas.admin.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        // GET: admin/Home

        private ApplicationDbContext db = new ApplicationDbContext();
        public ActionResult Index()        
        {
            if (User.Identity.Name != "")
            {
                var items = db.Users.SingleOrDefault(x => x.Email.Contains(User.Identity.Name));
                List<InfoAccount> infoAccounts = (from user in items.Roles
                               join role in db.Roles on user.RoleId equals role.Id
                               select new InfoAccount
                               {
                                   role = role.Name,
                                   name = items.Fullname,
                                   phone = items.Phone,
                                   address = items.Address,
                                   email= items.Email,
                                   createdDate = items.CreatedDate

                               }).ToList();
                return View(infoAccounts);
            }
            return View();
        }
        public ActionResult Partial_home() {
            if (User.Identity.Name != "")
            {
                var items = db.Users.SingleOrDefault(x => x.Email.Contains(User.Identity.Name));
                var roleName = from user in items.Roles
                               join role in db.Roles.ToList() on user.RoleId equals role.Id
                               select new
                               {
                                   role = role.Name,
                                   hovaten = items.Fullname,
                                   email = items.Email

                               };
                foreach (var item in roleName)
                {
                    ViewBag.role = item.role;
                    ViewBag.hovaten = item.hovaten;
                }
                return PartialView(roleName);
            }
            return PartialView();
        }
    }
}