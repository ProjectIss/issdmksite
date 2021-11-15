
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace issDMKSite.Custom
{
    public static class Display
    {
        public static string Name
        {
            get
            {
                if (HttpContext.Current.User.Identity.IsAuthenticated)
                {
                    return (HttpContext.Current.User as CustomPrincipal).Name;
                }
                else return "";
            }
        }

        public static string Role
        {
            get
            {
                if (HttpContext.Current.User.Identity.IsAuthenticated)
                {
                    return (HttpContext.Current.User as CustomPrincipal).Role;
                }
                else return "";
            }
        }
        public static int UserId
        {
            get
            {
                if (HttpContext.Current.User.Identity.IsAuthenticated)
                {
                    return (HttpContext.Current.User as CustomPrincipal).EId;
                }
                else return 0;
            }
        }
        public static string company
        {
            get
            {
                if (HttpContext.Current.User.Identity.IsAuthenticated)
                {
                    return (HttpContext.Current.User as CustomPrincipal).Email;
                }
                else return "ISS";
            }
        }
        public static int companyId
        {
            get
            {
                if (HttpContext.Current.User.Identity.IsAuthenticated)
                {
                    return (HttpContext.Current.User as CustomPrincipal).companyId;
                }
                else return 0;
            }
        }
        public static int blockId
        {
            get
            {
                if (HttpContext.Current.User.Identity.IsAuthenticated)
                {
                    return (HttpContext.Current.User as CustomPrincipal).blockId;
                }
                else return 0;
            }
        }

        public static string MobileNo
        {
            get
            {
                if (HttpContext.Current.User.Identity.IsAuthenticated)
                {
                    return (HttpContext.Current.User as CustomPrincipal).MobileNo;
                }
                else return "";
            }
        }
    }
    public class CustomAuthorizeAttribute : AuthorizeAttribute
    {
        protected virtual CustomPrincipal CurrentUser
        {
            get { return HttpContext.Current.User as CustomPrincipal; }
        }

        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            return ((CurrentUser != null && !CurrentUser.IsInRole(Roles)) || CurrentUser == null) ? false : true;
        }

        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            RedirectToRouteResult routeData = null;

            if (CurrentUser == null)
            {
                routeData = new RedirectToRouteResult
                    (new System.Web.Routing.RouteValueDictionary
                    (new
                    {
                        controller = "Auth",
                        action = "Login",
                    }
                    ));
            }
            else
            {
                routeData = new RedirectToRouteResult
                (new System.Web.Routing.RouteValueDictionary
                 (new
                 {
                     controller = "Auth",
                     action = "AccessDenied"
                 }
                 ));
            }

            filterContext.Result = routeData;
        }

    }
}