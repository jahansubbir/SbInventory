using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SbInventory.Models;

namespace SbInventory.Controllers
{
    public class SBDSBController : Controller
    {
        private SbDBEntities db = new SbDBEntities();

        // GET: /SBDSB/
        public ActionResult Index()
        {
            return View(db.SBDSBs.ToList());
        }

        // GET: /SBDSB/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SBDSB sbdsb = db.SBDSBs.Find(id);
            if (sbdsb == null)
            {
                return HttpNotFound();
            }
            return View(sbdsb);
        }

        // GET: /SBDSB/Login
        public ActionResult Create()
        {
            ViewBag.RPOId = new SelectList(db.RPOes, "Id", "Name");
            return View();
        }

        // POST: /SBDSB/Login
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="Id,RPOId,Name")] SBDSB sbdsb)
        {
            if (ModelState.IsValid)
            {
                db.SBDSBs.Add(sbdsb);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.RPOId = new SelectList(db.RPOes, "Id", "Name");
            return View(sbdsb);
        }

        // GET: /SBDSB/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SBDSB sbdsb = db.SBDSBs.Find(id);
            if (sbdsb == null)
            {
                return HttpNotFound();
            }
            return View(sbdsb);
        }

        // POST: /SBDSB/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="Id,RPOId,Name")] SBDSB sbdsb)
        {
            if (ModelState.IsValid)
            {
                db.Entry(sbdsb).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(sbdsb);
        }

        // GET: /SBDSB/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SBDSB sbdsb = db.SBDSBs.Find(id);
            if (sbdsb == null)
            {
                return HttpNotFound();
            }
            return View(sbdsb);
        }

        // POST: /SBDSB/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            SBDSB sbdsb = db.SBDSBs.Find(id);
            db.SBDSBs.Remove(sbdsb);
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
