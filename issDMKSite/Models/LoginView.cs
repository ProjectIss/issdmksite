using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace issDMKSite.Models
{
    public class LoginView
    {
        [Required]
        [Display(Name = "User Name")]
        public string username { get; set; }
        [Required]
        [Display(Name = "Password")]
        public string password { get; set; }
        [Display(Name = "Remember Me")]
        public bool rememberMe { get; set; }
    }
    public class CustomSerializeModel
    {
        public int id { get; set; }
        public string name { get; set; }
        public string email { get; set; }
        public string role { get; set; }
        public int EId { get; set; }
        public int companyId { get; set; }
        public int blockId { get; set; }
        public string MobileNo { get; set; }

    }
}