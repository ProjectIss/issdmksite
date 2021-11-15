using issDMKSite.Models;
using System.Data;
using System.Linq;
using System.Web.Mvc;

namespace issDMKSite.Controllers
{
    public class signUpsController : Controller
    {
        private issModel db = new issModel();



        // GET: signUps/Create
        public ActionResult Create()
        {
            ViewBag.blockId = new SelectList(db.Blocks, "Id", "blockName");
            ViewBag.panchayatId = new SelectList(db.Panchayats, "Id", "panchayatName");
            ViewBag.villageId = new SelectList(db.Villages, "Id", "villageName");
            return View();
        }

        // POST: signUps/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,Mobile,FatherName,Address,blockId,panchayatId,villageId")] signUp signUp)
        {
            if (ModelState.IsValid)
            {

                TempData["SignMobileNo"] = signUp.Mobile;
                db.SignUps.Add(signUp);
                db.SaveChanges();

                return RedirectToAction("UserLogin", "Reports");
            }

            ViewBag.blockId = new SelectList(db.Blocks, "Id", "blockName", signUp.blockId);
            ViewBag.panchayatId = new SelectList(db.Panchayats, "Id", "panchayatName", signUp.panchayatId);
            ViewBag.villageId = new SelectList(db.Villages, "Id", "villageName", signUp.villageId);
            return View(signUp);
        }
        [HttpPost]
        public JsonResult panchayatId(int NAME)
        {
            if (NAME > 0)
            {
                var resp = db.Panchayats.Where(x => x.blockId == NAME).ToList();
                return Json(resp, JsonRequestBehavior.AllowGet);
            }
            else return Json("NoData", JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult villageId(int LOCATION)
        {
            if (LOCATION > 0)
            {
                var resp = db.Villages.Where(x => x.panchayatId == LOCATION).ToList();
                return Json(resp, JsonRequestBehavior.AllowGet);

            }
            else return Json("NoData", JsonRequestBehavior.AllowGet);
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
