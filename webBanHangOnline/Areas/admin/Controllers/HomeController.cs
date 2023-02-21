using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace webBangHangOnline.Areas.admin.Controllers
{
    [RoutePrefix("admin")]
    public class HomeController : Controller
    {
        [Route("")]
        [Route("admin")]
        [Route("home")]
        // GET: admin/Home
        public ActionResult Index()
        {
            return View();
        }
    }
}