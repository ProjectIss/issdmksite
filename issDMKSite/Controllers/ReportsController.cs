using issDMKSite.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace issDMKSite.Controllers
{
    public class ReportsController : Controller
    {
        private Models.issModel db = new Models.issModel();
        // GET: Reports
        public ActionResult ComplainStatus()
        {
            return View(db.Applications.Where(x => x.Id == 0).ToList());
        }


        public ActionResult ApplicationStatus()
        {
            return View();
        }
        public ActionResult Dashboard()
        {
            return View();
        }
        [HttpGet]
        public JsonResult Dashboards()
        {
            try
            {
                DateTime todayDate = DateTime.Today;
                DateTime yesterday = todayDate.AddDays(-1);
                var NewData = db.Applications.Where(x => x.Status == "New" && x.Dateofapplied >= yesterday).ToList();
                foreach (var item in NewData)
                {
                    item.Status = "Pending";
                    db.Entry(item).State = System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();
                }
                var response = new object();
                var leftOuterJoin = new object();
                if (Custom.Display.Role == "Admin")
                {
                    var New = db.Applications.Where(x => x.Status == "New").Count();
                    var Pending = db.Applications.Where(x => x.Status == "Pending").Count();
                    var InProcess = db.Applications.Where(x => x.Status == "InProcess").Count();
                    var Completed = db.Applications.Where(x => x.Status == "Completed").Count();
                    response = new { New, Pending, InProcess, Completed };
                    leftOuterJoin = (from e in db.Applications
                                     join b in db.Blocks on e.blockId equals b.Id
                                     select new
                                     {
                                         blockName = b.blockName,
                                         status = e.Status,
                                         id = e.Id
                                     }).GroupBy(x => x.blockName).ToList();
                }
                else
                {
                    var res = db.Applications.Where(x => x.blockId == Custom.Display.blockId).ToList();
                    response = new DashboardDtos()
                    {
                        New = res.Where(x => x.Status == "New").Select(x => x.Id).Count().ToString(),
                        Pending = res.Where(x => x.Status == "Pending").Select(x => x.Id).Count().ToString(),
                        Completed = res.Where(x => x.Status == "Completed").Select(x => x.Id).Count().ToString(),
                    };
                    //var New = db.Applications.Where(x => x.Status == "New" && Custom.Display.blockId == x.blockId).Count();
                    //var Pending = db.Applications.Where(x => x.Status == "Pending" && Custom.Display.blockId == x.blockId).Count();
                    //var InProcess = db.Applications.Where(x => x.Status == "InProcess" && Custom.Display.blockId == x.blockId).Count();
                    //var Completed = db.Applications.Where(x => x.Status == "Completed" && Custom.Display.blockId == x.blockId).Count();
                    //response = new { New, Pending, InProcess, Completed };
                    leftOuterJoin = (from e in db.Applications
                                     join b in db.Villages on e.blockId equals b.Id
                                     // join p in db.Panchayats on b.Id equals p.blockId
                                     where e.blockId == Custom.Display.blockId
                                     select new
                                     {
                                         blockName = b.panchayatName.panchayatName,
                                         status = e.Status,
                                         id = e.Id
                                     }).GroupBy(x => x.blockName).ToList();

                    ViewBag.blockName = db.Blocks.Where(x => x.Id == Custom.Display.blockId).Select(x => x.blockName).First();
                }

                var data = new { leftOuterJoin, response };
                return Json(data, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json("Invalid", JsonRequestBehavior.AllowGet);

            }

        }

        public ActionResult UserLogin()
        {

            return View();
        }

        [HttpPost]
        public JsonResult UserLogin(string mobileNo)
        {
            Session["MobileNo"] = mobileNo;
            if (!string.IsNullOrEmpty(Session["MobileNo"].ToString()))
            {
                var enUser = db.SignUps.FirstOrDefault(x => x.Mobile == mobileNo);
                if (enUser != null)
                {
                    // var enEmployee = db.Companies.FirstOrDefault(x => x.id == enUser.companyId);
                    CustomSerializeModel userModel = new Models.CustomSerializeModel()
                    {
                        id = enUser.Id,
                        name = enUser.Name,
                        role = "",
                        //companyId = enUser.companyId,
                    };
                    Session["BlockId"] = enUser.blockId;
                    Session["VillageId"] = enUser.villageId;

                    string userData = JsonConvert.SerializeObject(userModel);
                    FormsAuthenticationTicket authTicket =
                        new FormsAuthenticationTicket(1, enUser.Name, DateTime.Now, DateTime.Now.AddMinutes(30), false, userData);

                    string enTicket = FormsAuthentication.Encrypt(authTicket);
                    HttpCookie faCookie = new HttpCookie("issDMK", enTicket);
                    Response.Cookies.Add(faCookie);
                    return Json("ApplicationHome", JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json("signUps", JsonRequestBehavior.AllowGet);
                }

            }
            else
            {
                return Json("signUps", JsonRequestBehavior.AllowGet);
            }
            return Json("same", JsonRequestBehavior.AllowGet);
        }
        public JsonResult SendOTP()
        {
            int otpValue = new Random().Next(100000, 999999);
            var status = "";
            try
            {
                string recipient = "919489653881";//ConfigurationManager.AppSettings["RecipientNumber"].ToString();
                string APIKey = ConfigurationManager.AppSettings["APIKey"].ToString();

                string message = "Your OTP Number is " + otpValue + " ( Sent By : Inspire Software Solutions )";
                String encodedMessage = HttpUtility.UrlEncode(message);

                using (var webClient = new WebClient())
                {
                    byte[] response = webClient.UploadValues("https://api.textlocal.in/send/", new NameValueCollection(){

                                         {"apikey" , APIKey},
                                         {"numbers" , recipient},
                                         {"message" , encodedMessage},
                                         {"sender" , "TXTLCL"}});

                    string result = System.Text.Encoding.UTF8.GetString(response);

                    var jsonObject = JObject.Parse(result);

                    status = jsonObject["status"].ToString();

                    Session["CurrentOTP"] = otpValue;
                }


                return Json(status, JsonRequestBehavior.AllowGet);


            }
            catch (Exception e)
            {

                throw (e);

            }

        }

        public ActionResult EnterOTP()
        {
            return View();
        }

        [HttpPost]
        public JsonResult VerifyOTP(string otp)
        {
            bool result = false;

            string sessionOTP = Session["CurrentOTP"].ToString();

            if (otp == sessionOTP)
            {
                result = true;

            }

            return Json(result, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult ComplainStatus(string status)
        {
            if (!string.IsNullOrEmpty(status))
            {
                var TblComplainStatus = db.Applications.Where(x => x.Status == status).ToList();
                return View(TblComplainStatus);
            }
            return View(new Application());
        }

        public ActionResult Area()
        {
            List<SelectListItem> Area = new List<SelectListItem>();
            foreach (var item in db.Applications.GroupBy(x => x.blockId).Select(g => g.FirstOrDefault()).ToList())
            {
                Area.Add(new SelectListItem { Text = item.Block.blockName, Value = item.Block.Id.ToString() });

            }
            ViewBag.Area = Area;
            return View();
        }
        [HttpPost]
        public JsonResult Areas(string fromDate, string toDate, int block)
        {

            List<SelectListItem> Area = new List<SelectListItem>();
            foreach (var item in db.Applications.GroupBy(x => x.blockId).Select(g => g.FirstOrDefault()).ToList())
            {
                Area.Add(new SelectListItem { Text = item.Block.blockName, Value = item.Block.Id.ToString() });

            }
            ViewBag.Area = Area;
            if (!string.IsNullOrEmpty(fromDate) && !string.IsNullOrEmpty(toDate) && block > 0)
            {
                DateTime fDate = DateTime.ParseExact(fromDate, "dd/MM/yyyy", null);
                DateTime tDate = DateTime.ParseExact(toDate, "dd/MM/yyyy", null);

                var data = db.Applications.Where(x => x.Dateofapplied >= fDate && x.DateandTimeofReact <= tDate && x.Id == block).ToList();
                return Json(JsonRequestBehavior.AllowGet);
            }
            else return Json("Faild", JsonRequestBehavior.AllowGet);


        }
        public ActionResult Department()
        {
            List<SelectListItem> Department = new List<SelectListItem>();
           
            foreach (var item in db.Applications.GroupBy(x => x.Department.departmentName).Select(g => g.FirstOrDefault()).ToList())
            {
                Department.Add(new SelectListItem { Text = item.Department.departmentName, Value = item.Department.id.ToString() });

            }
            ViewBag.Department = Department;

            List<SelectListItem> Area = new List<SelectListItem>();
            foreach (var item in db.Applications.GroupBy(x => x.Block.blockName).Select(g => g.FirstOrDefault()).ToList())
            {
                Area.Add(new SelectListItem { Text = item.Block.blockName.ToString(), Value = item.Block.Id.ToString() });

            }
            ViewBag.Area = Area;
            List<SelectListItem> Village = new List<SelectListItem>();
            foreach (var item in db.Applications.GroupBy(x => x.Village.villageName).Select(g => g.FirstOrDefault()).ToList())
            {
                Village.Add(new SelectListItem { Text = item.Village.villageName, Value = item.Village.Id.ToString() });

            }
            ViewBag.Village = Village;
            return View(db.Applications.Where(x => x.Id == 0).ToList());


        }
        [HttpPost]
        public ActionResult Department(string fromDate, string toDate, string block, string dpartment, string village)
        {
            List<SelectListItem> Department = new List<SelectListItem>();
            
            foreach (var item in db.Applications.GroupBy(x => x.Department.departmentName).Select(g => g.FirstOrDefault()).ToList())
            {
                Department.Add(new SelectListItem { Text = item.Department.departmentName, Value = item.Department.id.ToString() });

            }
            ViewBag.Department = Department;

            List<SelectListItem> Area = new List<SelectListItem>();
            foreach (var item in db.Applications.GroupBy(x => x.blockId).Select(g => g.FirstOrDefault()).ToList())
            {
                Area.Add(new SelectListItem { Text = item.Block.blockName, Value = item.Block.Id.ToString() });

            }
            ViewBag.Area = Area;

            List<SelectListItem> Village = new List<SelectListItem>();
            foreach (var item in db.Applications.GroupBy(x => x.Village.villageName).Select(g => g.FirstOrDefault()).ToList())
            {
                Village.Add(new SelectListItem { Text = item.Village.villageName, Value = item.Village.Id.ToString() });

            }
            ViewBag.Village = Village;
            if (!string.IsNullOrEmpty(fromDate) && !string.IsNullOrEmpty(toDate))
            {
                DateTime fDate = Convert.ToDateTime(fromDate);
                DateTime tDate = Convert.ToDateTime(toDate);
                var data = db.Applications.Where(x => x.Dateofapplied >= fDate && x.Dateofapplied <= tDate).ToList();

                if (!string.IsNullOrEmpty(dpartment) && !string.IsNullOrEmpty(block) && !string.IsNullOrEmpty(village))
                {
                    data =data.Where(x => x.blockId == Convert.ToInt32(block) && x.departmentId == Convert.ToInt32(dpartment) && x.villageId == Convert.ToInt32(village)).ToList();
                }
                else if (!string.IsNullOrEmpty(dpartment) && string.IsNullOrEmpty(block))
                {
                    data = data.Where(x =>  x.departmentId == Convert.ToInt32(dpartment)).ToList();

                }
                else if (!string.IsNullOrEmpty(dpartment) && string.IsNullOrEmpty(village))
                {
                    data = data.Where(x => x.departmentId == Convert.ToInt32(dpartment)).ToList();

                }
                else if (!string.IsNullOrEmpty(block) && string.IsNullOrEmpty(dpartment))
                {
                    data = data.Where(x => x.blockId == Convert.ToInt32(block)).ToList();

                }
                else if (!string.IsNullOrEmpty(block) && string.IsNullOrEmpty(village))
                {
                    data = data.Where(x => x.blockId == Convert.ToInt32(block)).ToList();

                }
                else if (!string.IsNullOrEmpty(village) && string.IsNullOrEmpty(block))
                {
                    data = data.Where(x => x.villageId == Convert.ToInt32(village)).ToList();

                }
                else if (!string.IsNullOrEmpty(village) && string.IsNullOrEmpty(dpartment))
                {
                    data = data.Where(x => x.villageId == Convert.ToInt32(village)).ToList();

                }


                return View(data);

            }

            else return View(new Application());


        }
        [HttpPost]
        public JsonResult Block(int NAME)
        {
            if (NAME > 0)
            {
                var resp = db.Applications.Where(x => x.departmentId == NAME).ToList();
                return Json(resp, JsonRequestBehavior.AllowGet);
            }
            else return Json("NoData", JsonRequestBehavior.AllowGet);
        }


    }
    public class UserLogin
    {
        public string MobileNo { get; set; }
        public string OTP { get; set; }
    }
    public class DashboardDtos
    {
        public string New { get; set; }
        public string Pending { get; set; }
        public string InProgress { get; set; }
        public string Completed { get; set; }
        public string panchayatName { get; set; }
        public string Blocked { get; set; }
    }
}