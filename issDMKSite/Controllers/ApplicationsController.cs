using issDMKSite.Custom;
using issDMKSite.Models;
using System;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace issDMKSite.Controllers
{
    [CustomAuthorize(Roles = "Admin,User")]
    public class ApplicationsController : Controller
    {
        private issModel db = new issModel();

        // GET: Applications
        public ActionResult Index()
        {
            string mNo = String.Empty;
            if (Session["MobileNo"] != null) { mNo = Session["MobileNo"].ToString(); }
            //Session["MobileNo"] !=null?mNo= Session["MobileNo"].ToString():mNo=mNo;
            if (!string.IsNullOrEmpty(mNo))
            {
                string mobileNo = Session["MobileNo"].ToString();
                return View(db.Applications.Where(x => x.MobilenNo == mobileNo).OrderByDescending(x => x.Dateofapplied).ToList());
            }
            else if (Custom.Display.Role == "Admin")
            {
                var applications = db.Applications.Include(a => a.Department);
                return View(db.Applications.OrderByDescending(x => x.Dateofapplied).ToList());
            }
            else if (Display.Role == "User")
            {
                return View(db.Applications.Where(x => x.blockId == Display.blockId).OrderByDescending(x => x.Dateofapplied).ToList());
            }
            return View();
        }

        // GET: Applications/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Application application = db.Applications.Find(id);
            if (application == null)
            {
                return HttpNotFound();
            }
            return View(application);
        }

        // GET: Applications/Create
        public ActionResult Create()
        {
            int id = 0;
            if (db.Applications.Count() > 0)
            {
                id = db.Applications.Max(x => x.Id);
                if (id == 0) id = 1;
                else
                    id = id + 1;
            }
            else id = 1;
            ViewBag.Id = id;

            string complainNo = "RISH" + id;
            ViewBag.ComplainNo = complainNo;
            ViewBag.departmentId = new SelectList(db.Departments, "id", "departmentName");
            return View();
        }

        // POST: Applications/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,departmentId,ComplainNo,Dateofapplied,Detailofcomplain,DetailProof,Status,Feedback,ReviewComments,AdminName,DateandTimeofReact,MobilenNo,blockId")] Application application, HttpPostedFileBase DetailProof)
        {
            //application.DateandTimeofReact = DateTime.UtcNow;
            //application.Id = 1;
            if (DetailProof != null)
            {
                string ext = Path.GetExtension(DetailProof.FileName);
                string fileName = Guid.NewGuid().ToString()+ext;
                string path = Path.Combine(Server.MapPath("~/attachments/"), Path.GetFileName(fileName));
                DetailProof.SaveAs(path);
                application.DetailProof = "~/attachments/" + Path.GetFileName(fileName);
            }


            application.AdminName = Display.Name;
            string mNo = String.Empty;
            int bId = 0;
            if (Session["MobileNo"] != null) { mNo = Session["MobileNo"].ToString(); }
            if (Session["BlockId"] != null) { bId = Convert.ToInt32(Session["BlockId"].ToString()); }
            application.MobilenNo = mNo;
            application.blockId = bId;

            application.Dateofapplied = DateTime.Today;
            //application.Dateofapplied = DateTime.UtcNow;
            int id = 0;
            if (db.Applications.Count() > 0)
            {
                id = db.Applications.Max(x => x.Id);
                if (id == 0) id = 1;
                else
                    id = id + 1;
            }
            else id = 1;
            string complainNo = "RISH" + id;
            application.ComplainNo = complainNo;
            db.Applications.Add(application);
            db.SaveChanges();
            ViewBag.departmentId = new SelectList(db.Departments, "id", "departmentName", application.departmentId);
            return RedirectToAction("Details/" + id);

        }
        [HttpPost]
        public ActionResult UploadFiles(HttpPostedFileBase file)
        {
            if (ModelState.IsValid)
            {
                try
                {

                    //Method 2 Get file details from HttpPostedFileBase class    

                    if (file != null)
                    {
                        string path = Path.Combine(Server.MapPath("~/attachments"), Path.GetFileName(file.FileName));
                        file.SaveAs(path);
                    }

                    ViewBag.FileStatus = "File uploaded successfully.";
                }
                catch (Exception)
                {
                    ViewBag.FileStatus = "Error while file uploading."; ;
                }
            }
            return View("Index");
        }


        // GET: Applications/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {

                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Application application = db.Applications.Find(id);
            if (application == null)
            {
                return HttpNotFound();
            }
            ViewBag.departmentId = new SelectList(db.Departments, "id", "departmentName", application.departmentId);
            return View(application);
        }

        // POST: Applications/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,departmentId,ComplainNo,Dateofapplied,Detailofcomplain,DetailProof,Status,Feedback,ReviewComments,AdminName,DateandTimeofReact,,MobilenNo,blockId")] HttpPostedFileBase DetailProof, Application application)
        {


            application.DateandTimeofReact = DateTime.Today;
            if (ModelState.IsValid)
            {
                if (DetailProof != null)
                {
                    string path = Path.Combine(Server.MapPath("~/Content/Images"), Path.GetFileName(DetailProof.FileName));
                    DetailProof.SaveAs(path);
                    application.DetailProof = "/Content/Images/" + Path.GetFileName(DetailProof.FileName);
                }
                db.Entry(application).State = EntityState.Modified;
                db.SaveChanges();
               
                return RedirectToAction("Index");
            }
            ViewBag.departmentId = new SelectList(db.Departments, "id", "departmentName", application.departmentId);
            return View(application);
        }

        // GET: Applications/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Application application = db.Applications.Find(id);
            if (application == null)
            {
                return HttpNotFound();
            }
            return View(application);
        }

        // POST: Applications/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Application application = db.Applications.Find(id);
            db.Applications.Remove(application);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
