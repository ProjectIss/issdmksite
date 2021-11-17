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
    public class DailyUpdatesController : Controller
    {
        private issModel db = new issModel();

        // GET: DailyUpdates
        public ActionResult Index()
        {
            return View(db.DailyUpdates.ToList());
        }

        // GET: DailyUpdates/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DailyUpdate dailyUpdate = db.DailyUpdates.Find(id);
            if (dailyUpdate == null)
            {
                return HttpNotFound();
            }
            return View(dailyUpdate);
        }

        // GET: DailyUpdates/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: DailyUpdates/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,dailyDetail")] DailyUpdate dailyUpdate)
        {
            if (ModelState.IsValid)
            {
                db.DailyUpdates.Add(dailyUpdate);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(dailyUpdate);
        }

        // GET: DailyUpdates/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DailyUpdate dailyUpdate = db.DailyUpdates.Find(id);
            if (dailyUpdate == null)
            {
                return HttpNotFound();
            }
            return View(dailyUpdate);
        }

        // POST: DailyUpdates/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,dailyDetail")] DailyUpdate dailyUpdate)
        {
            if (ModelState.IsValid)
            {
                db.Entry(dailyUpdate).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(dailyUpdate);
        }

        // GET: DailyUpdates/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DailyUpdate dailyUpdate = db.DailyUpdates.Find(id);
            if (dailyUpdate == null)
            {
                return HttpNotFound();
            }
            return View(dailyUpdate);
        }

        // POST: DailyUpdates/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            DailyUpdate dailyUpdate = db.DailyUpdates.Find(id);
            db.DailyUpdates.Remove(dailyUpdate);
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
