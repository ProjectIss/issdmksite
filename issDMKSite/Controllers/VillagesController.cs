using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using issDMKSite.Models;

namespace issDMKSite.Controllers
{
    public class VillagesController : Controller
    {
        private issModel db = new issModel();

        // GET: Villages
        public ActionResult Index()
        {
            var villages = db.Villages.Include(v => v.blockName).Include(v => v.panchayatName);
            return View(villages.ToList());
        }

        // GET: Villages/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Village village = db.Villages.Find(id);
            if (village == null)
            {
                return HttpNotFound();
            }
            return View(village);
        }

        // GET: Villages/Create
        public ActionResult Create()
        {
            ViewBag.blockId = new SelectList(db.Blocks, "Id", "blockName");
            ViewBag.panchayatId = new SelectList(db.Panchayats, "Id", "panchayatName");
            return View();
        }

        // POST: Villages/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,villageName,villageSecretaryName,villageSecretaryAddress,mobileNo,email,blockId,panchayatId")] Village village)
        {
            if (ModelState.IsValid)
            {
                db.Villages.Add(village);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.blockId = new SelectList(db.Blocks, "Id", "blockName", village.blockId);
            ViewBag.panchayatId = new SelectList(db.Panchayats, "Id", "panchayatName", village.panchayatId);
            return View(village);
        }

        // GET: Villages/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Village village = db.Villages.Find(id);
            if (village == null)
            {
                return HttpNotFound();
            }
            ViewBag.blockId = new SelectList(db.Blocks, "Id", "blockName", village.blockId);
            ViewBag.panchayatId = new SelectList(db.Panchayats, "Id", "panchayatName", village.panchayatId);
            return View(village);
        }

        // POST: Villages/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,villageName,villageSecretaryName,villageSecretaryAddress,mobileNo,email,blockId,panchayatId")] Village village)
        {
            if (ModelState.IsValid)
            {
                db.Entry(village).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.blockId = new SelectList(db.Blocks, "Id", "blockName", village.blockId);
            ViewBag.panchayatId = new SelectList(db.Panchayats, "Id", "panchayatName", village.panchayatId);
            return View(village);
        }

        // GET: Villages/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Village village = db.Villages.Find(id);
            if (village == null)
            {
                return HttpNotFound();
            }
            return View(village);
        }

        // POST: Villages/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Village village = db.Villages.Find(id);
            db.Villages.Remove(village);
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
