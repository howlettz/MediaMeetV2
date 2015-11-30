using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MediaMeetV2.Models;

namespace MediaMeetV2.Controllers
{
    public class DemographicsController : Controller
    {
        private MediaMeetV2DbContext db = new MediaMeetV2DbContext();

        // GET: Demographics
        public ActionResult Index()
        {
            return View(db.Demographics.ToList());
        }

        // GET: Demographics/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Demographics demographics = db.Demographics.Find(id);
            if (demographics == null)
            {
                return HttpNotFound();
            }
            return View(demographics);
        }

        // GET: Demographics/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Demographics/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,city,state,country,birthDate,gender")] Demographics demographics)
        {
            if (ModelState.IsValid)
            {
                db.Demographics.Add(demographics);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(demographics);
        }

        // GET: Demographics/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Demographics demographics = db.Demographics.Find(id);
            if (demographics == null)
            {
                return HttpNotFound();
            }
            return View(demographics);
        }

        // POST: Demographics/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,city,state,country,birthDate,gender")] Demographics demographics)
        {
            if (ModelState.IsValid)
            {
                db.Entry(demographics).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(demographics);
        }

        // GET: Demographics/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Demographics demographics = db.Demographics.Find(id);
            if (demographics == null)
            {
                return HttpNotFound();
            }
            return View(demographics);
        }

        // POST: Demographics/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Demographics demographics = db.Demographics.Find(id);
            db.Demographics.Remove(demographics);
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
