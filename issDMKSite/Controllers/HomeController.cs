using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using issDMKSite.Custom;
using issDMKSite.Models;
using System.Data;
using System.Data.Entity;
using System.Net;


namespace issDMKSite.Controllers
{
    public class HomeController : Controller
    {
        private issModel db = new issModel();

        public ActionResult Index()
        {
            try
            {
                ViewBag.dailyupdate = db.DailyUpdates.FirstOrDefault()?.dailyDetail;
                getPhotos();
            }
            catch (NullReferenceException ex)
            {

            }
       
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        //[HttpPost]
        public JsonResult getPhotos()
        {
            //var images = Directory.GetFiles(Server.MapPath("~/assets/MLA"), ".jpeg");

            DirectoryInfo dir = new DirectoryInfo(Server.MapPath("~/assets/MLA"));
            FileInfo[] file = dir.GetFiles();
            ArrayList list = new ArrayList();
            foreach (FileInfo file2 in file)
            {
                if (file2.Extension == ".jpg" || file2.Extension == ".jpeg" || file2.Extension == ".gif" || file2.Extension == ".png")
                {
                    list.Add(file2);
                }
            }
            ViewBag.Images = list;
            //TempData["Images"] = file;
            return Json(list, JsonRequestBehavior.AllowGet);
        }
    }
}