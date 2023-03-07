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
            if (Session["email"] != null)
            {
                string email = (string)Session["email"];

                var item = db.Users.FirstOrDefault(x => x.Email.Contains(email));
                Session["fullName"] = item.Fullname;
                /*var item = db.Users.ToList();
                foreach (var user in item)
                {
                    if (user.Email == email)
                    {
                        return View(user);
                    }
                }*/
                foreach(var user in item.Roles) {
                    foreach(var model in db.Roles)
                    {
                        if(user.RoleId.Equals(model.Id))
                        {
                            ViewBag.Role = model.Name;
                        }
                    }
                }
                
                return View(item);
            }
            return View();
        }
    }
}