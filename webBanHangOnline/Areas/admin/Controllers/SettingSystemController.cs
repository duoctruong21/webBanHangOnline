using Microsoft.Ajax.Utilities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using webBangHangOnline.Models;
using webBangHangOnline.Models.EF;

namespace webBangHangOnline.Areas.admin.Controllers
{
    [Authorize]
    public class SettingSystemController : Controller
    {
        public ApplicationDbContext db = new ApplicationDbContext();
        // GET: admin/SettingSystem
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult PartialSetting()
        {
            return PartialView();
        }

        [HttpPost]
        public ActionResult AddSetting(SettingSystemViewModel setting) {
            if(ModelState.IsValid)
            {
                SystemSetting set = null;
                var checkTitle = db.systemSetting.FirstOrDefault(x => x.SettingKey.Contains("SettingTitle"));
                if (checkTitle == null)
                {
                    set = new SystemSetting();
                    set.SettingKey = "SettingTitle";
                    set.SettingValue = setting.SettingTitle;
                    db.systemSetting.Add(set);
                }
                else
                {
                    checkTitle.SettingValue = setting.SettingTitle;
                    db.Entry(checkTitle).State = System.Data.Entity.EntityState.Modified;

                }
                //logo
                var checkLogo = db.systemSetting.FirstOrDefault(x => x.SettingKey.Contains("SettingLogo"));
                if (checkLogo == null)
                {
                    set = new SystemSetting();
                    set.SettingKey = "SettingLogo";
                    set.SettingValue = setting.SettingLogo;
                    db.systemSetting.Add(set);
                }
                else
                {
                    checkLogo.SettingValue = setting.SettingLogo;
                    db.Entry(checkLogo).State = System.Data.Entity.EntityState.Modified;

                }
                //hotline
                var checkHotline = db.systemSetting.FirstOrDefault(x => x.SettingKey.Contains("SettingHotline"));
                if (checkHotline == null)
                {
                    set = new SystemSetting();
                    set.SettingKey = "SettingHotline";
                    set.SettingValue = setting.SettingHotline;
                    db.systemSetting.Add(set);
                }
                else
                {
                    checkHotline.SettingValue = setting.SettingHotline;
                    db.Entry(checkHotline).State = System.Data.Entity.EntityState.Modified;

                }
                //Email
                var checkEmail = db.systemSetting.FirstOrDefault(x => x.SettingKey.Contains("SettingEmail"));
                if (checkEmail == null)
                {
                    set = new SystemSetting();
                    set.SettingKey = "SettingEmail";
                    set.SettingValue = setting.SettingEmail;
                    db.systemSetting.Add(set);
                }
                else
                {
                    checkEmail.SettingValue = setting.SettingEmail;
                    db.Entry(checkEmail).State = System.Data.Entity.EntityState.Modified;

                }
                //TitleSeo
                var checkTitleSeo = db.systemSetting.FirstOrDefault(x => x.SettingKey.Contains("SettingTitleSeo"));
                if (checkTitleSeo == null)
                {
                    set = new SystemSetting();
                    set.SettingKey = "SettingTitleSeo";
                    set.SettingValue = setting.SettingTitleSeo;
                    db.systemSetting.Add(set);
                }
                else
                {
                    checkTitleSeo.SettingValue = setting.SettingTitleSeo;
                    db.Entry(checkTitleSeo).State = System.Data.Entity.EntityState.Modified;

                }
                //DesSeo
                var checkDesSeo = db.systemSetting.FirstOrDefault(x => x.SettingKey.Contains("SettingDesSeo"));
                if (checkDesSeo == null)
                {
                    set = new SystemSetting();
                    set.SettingKey = "SettingDesSeo";
                    set.SettingValue = setting.SettingDesSeo;
                    db.systemSetting.Add(set);
                }
                else
                {
                    checkDesSeo.SettingValue = setting.SettingDesSeo;
                    db.Entry(checkDesSeo).State = System.Data.Entity.EntityState.Modified;

                }
                //KeySeo
                var checkKeySeo = db.systemSetting.FirstOrDefault(x => x.SettingKey.Contains("SettingKeySeo"));
                if (checkKeySeo == null)
                {
                    set = new SystemSetting();
                    set.SettingKey = "SettingKeySeo";
                    set.SettingValue = setting.SettingKeySeo;
                    db.systemSetting.Add(set);
                }
                else
                {
                    checkKeySeo.SettingValue = setting.SettingKeySeo;
                    db.Entry(checkKeySeo).State = System.Data.Entity.EntityState.Modified;

                }
                db.SaveChanges();
            }
            return View("PartialSetting");
        }
    }
}