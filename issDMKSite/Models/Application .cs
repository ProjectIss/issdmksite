using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace issDMKSite.Models
{
    public class Application
    {
        public int Id { get; set; }
        public string ComplainNo { get; set; }
        public DateTime? Dateofapplied { get; set; }
        public string Detailofcomplain { get; set; }
        public string DetailProof { get; set; }
        public string Status { get; set; }
        public string Feedback { get; set; }
        public string ReviewComments { get; set; }
        public string AdminName { get; set; }
        public DateTime? DateandTimeofReact { get; set; }
        public string MobilenNo { get; set; }
        public int blockId { get; set; }
        public virtual Block Block { get; set; }
        public int departmentId { get; set; }
        public virtual Department Department { get; set; }
        public int villageId { get; set; }
        public virtual Village Village { get; set; }



    }
}