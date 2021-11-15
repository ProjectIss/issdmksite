using issDMKSite.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace issDMKSite.Controllers
{
    public class AuthController : Controller
    {
        private issDMKSite.Models.issModel db = new Models.issModel();
        // GET: Auth
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Login(string ReturnUrl = "")
        {
            if (User.Identity.IsAuthenticated)
            {
                return LogOut();
            }
            ViewBag.ReturnUrl = ReturnUrl;
            return View();
        }

        [HttpPost]
        public ActionResult Login(LoginView loginView, string ReturnUrl = "")
        {
            if (ModelState.IsValid)
            {
                var enUser = db.Users.FirstOrDefault(x => x.username == loginView.username && x.password == loginView.password && x.status== "Active");
                if (enUser != null)
                {
                    // var enEmployee = db.Companies.FirstOrDefault(x => x.id == enUser.companyId);
                    CustomSerializeModel userModel = new Models.CustomSerializeModel()
                    {
                        id = enUser.id,
                        name = enUser.name,
                        role = enUser.role,
                      blockId=enUser.blockId,
                        //companyId = enUser.companyId,

                    };
                    Session["MobileNo"] = userModel.MobileNo;
                    string userData = JsonConvert.SerializeObject(userModel);
                    FormsAuthenticationTicket authTicket =
                        new FormsAuthenticationTicket(1, loginView.username, DateTime.Now, DateTime.Now.AddMinutes(30), false, userData);

                    string enTicket = FormsAuthentication.Encrypt(authTicket);
                    HttpCookie faCookie = new HttpCookie("issDMK", enTicket);
                    Response.Cookies.Add(faCookie);
                }

                if (Url.IsLocalUrl(ReturnUrl))
                {
                    return Redirect(ReturnUrl);
                }
                else
                {
                    return RedirectToAction("Dashboard", "Reports");
                }
            }
            ModelState.AddModelError("", "Something Wrong : Username or Password invalid ^_^ ");
            return View(loginView);
        }

        public ActionResult LogOut()
        {
            HttpCookie cookie = new HttpCookie("issDMK", "");
            cookie.Expires = DateTime.Now.AddYears(-1);
            Response.Cookies.Add(cookie);

            FormsAuthentication.SignOut();
            Session.Clear();
            return RedirectToAction("Index", "Home", null);

        }
        

        public ActionResult AccessDenied()
        {
            return View();
        }
    }
}