using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace issDMKSite.Models
{
    public class signUp
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Mobile { get; set; }
        public string FatherName { get; set; }
        public string Address { get; set; }
        public int blockId { get; set; }
        public virtual Block blockName { get; set; }
        public int panchayatId { get; set; }
        public virtual Panchayat panchayatName { get; set; }
        public int villageId { get; set; }
        public virtual Village villageName { get; set; }
    }
}