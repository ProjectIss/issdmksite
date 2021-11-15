using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace issDMKSite.Models
{
    public class Panchayat
    {
        public int Id { get; set; }
        public string panchayatName { get; set; }
        public string panchayatSecretaryName { get; set; }
        public string panchayatSecretaryAddress { get; set; }
        public string mobileNo { get; set; }
        public string email { get; set; }
        public int blockId { get; set; }
        public virtual Block blockName { get; set; }
    }
}