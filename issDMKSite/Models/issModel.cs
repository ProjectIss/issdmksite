using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace issDMKSite.Models
{
    public class issModel : DbContext
    {
        public issModel()
            : base("name=ISSModel")
        {
        }
      
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Application> Applications { get; set; }
        public virtual DbSet<signUp> SignUps { get; set; }

        public System.Data.Entity.DbSet<issDMKSite.Models.Block> Blocks { get; set; }

        public System.Data.Entity.DbSet<issDMKSite.Models.Panchayat> Panchayats { get; set; }

        public System.Data.Entity.DbSet<issDMKSite.Models.Village> Villages { get; set; }
        public System.Data.Entity.DbSet<issDMKSite.Models.Department> Departments { get; set; }
    }
}