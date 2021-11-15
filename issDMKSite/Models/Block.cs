using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace issDMKSite.Models
{
    public class Block
    {
        public int Id { get; set; }
        public string blockName { get; set; }
        public string blockSecretaryName { get; set; }
        public string blockSecretaryAddress { get; set; }
        public string mobileNo { get; set; }
        public string email { get; set; }
    }
}