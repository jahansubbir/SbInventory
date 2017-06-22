using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SbInventory.BLL;
using SbInventory.Models;

namespace SbInventory.Controllers
{
    public class RPOController : Controller
    {
        private SbDBEntities db = new SbDBEntities();
        RpoManager rpoManager=new RpoManager();

        // GET: /RPO/
        public ActionResult Index()
        {
            return View(db.RPOes.ToList());
        }

        // GET: /RPO/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RPO rpo = db.RPOes.Find(id);
            if (rpo == null)
            {
                return HttpNotFound();
            }
            return View(rpo);
        }

        // GET: /RPO/Login
        public ActionResult Create()
        {
            return View();
        }

        // POST: /RPO/Login
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="Id,Name,Title")] RPO rpo)
        {
            if (ModelState.IsValid)
            {
                rpoManager.SaveRpo(rpo);
                return RedirectToAction("Index");
            }

            return View(rpo);
        }

        // GET: /RPO/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RPO rpo = db.RPOes.Find(id);
            if (rpo == null)
            {
                return HttpNotFound();
            }
            return View(rpo);
        }

        // POST: /RPO/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="Id,Name,Title")] RPO rpo)
        {
            if (ModelState.IsValid)
            {
                rpoManager.EditRpo(rpo);
                return RedirectToAction("Index");
            }
            return View(rpo);
        }

        // GET: /RPO/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RPO rpo = db.RPOes.Find(id);
            if (rpo == null)
            {
                return HttpNotFound();
            }
            return View(rpo);
        }

        // POST: /RPO/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            rpoManager.DeleteRpo(id);
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
