using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace issDMKSite.Models
{
    public class Village
    {
        public int Id { get; set; }
        public string villageName { get; set; }
        public string villageSecretaryName { get; set; }
        public string villageSecretaryAddress { get; set; }
        public string mobileNo { get; set; }
        public string email { get; set; }
        public int blockId { get; set; }
        public virtual Block blockName { get; set; }
        public int panchayatId { get; set; }
        public virtual Panchayat panchayatName { get; set; }
    }
}