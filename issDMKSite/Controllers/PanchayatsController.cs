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
    public class PanchayatsController : Controller
    {
        private issModel db = new issModel();

        // GET: Panchayats
        public ActionResult Index()
        {
            var panchayats = db.Panchayats.Include(p => p.blockName);
            return View(panchayats.ToList());
        }

        // GET: Panchayats/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Panchayat panchayat = db.Panchayats.Find(id);
            if (panchayat == null)
            {
                return HttpNotFound();
            }
            return View(panchayat);
        }

        // GET: Panchayats/Create
        public ActionResult Create()
        {
            ViewBag.blockId = new SelectList(db.Blocks, "Id", "blockName");
            return View();
        }

        // POST: Panchayats/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,panchayatName,panchayatSecretaryName,panchayatSecretaryAddress,mobileNo,email,blockId")] Panchayat panchayat)
        {
            if (ModelState.IsValid)
            {
                db.Panchayats.Add(panchayat);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.blockId = new SelectList(db.Blocks, "Id", "blockName", panchayat.blockId);
            return View(panchayat);
        }

        // GET: Panchayats/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Panchayat panchayat = db.Panchayats.Find(id);
            if (panchayat == null)
            {
                return HttpNotFound();
            }
            ViewBag.blockId = new SelectList(db.Blocks, "Id", "blockName", panchayat.blockId);
            return View(panchayat);
        }

        // POST: Panchayats/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,panchayatName,panchayatSecretaryName,panchayatSecretaryAddress,mobileNo,email,blockId")] Panchayat panchayat)
        {
            if (ModelState.IsValid)
            {
                db.Entry(panchayat).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.blockId = new SelectList(db.Blocks, "Id", "blockName", panchayat.blockId);
            return View(panchayat);
        }

        // GET: Panchayats/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Panchayat panchayat = db.Panchayats.Find(id);
            if (panchayat == null)
            {
                return HttpNotFound();
            }
            return View(panchayat);
        }

        // POST: Panchayats/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Panchayat panchayat = db.Panchayats.Find(id);
            db.Panchayats.Remove(panchayat);
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
