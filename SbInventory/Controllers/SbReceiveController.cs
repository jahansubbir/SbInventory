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
    public class SbReceiveController : Controller
    {
        private SbDBEntities db = new SbDBEntities();
        SbDBViewEntities viewdb=new SbDBViewEntities();
        TransectionManager transectionManager=new TransectionManager();

        // GET: /SbReceive/
         private HttpContext context = System.Web.HttpContext.Current;
        private HttpCookie cookie;
        private int loggedIn;
        private string userId;

        // GET: /SdDispatch/
        public SbReceiveController()
        {
            cookie = context.Request.Cookies["loginCookie"];
            if (cookie!=null)
            {
                loggedIn = Convert.ToInt32(cookie["loggedIn"]);
            }
        }
        public ActionResult Index()
        {
            if (loggedIn!=0)
            {
                List<ReceiveView> receives = viewdb.ReceiveViews.ToList();
                return View(receives);
            }
            return RedirectToAction("Login", "RPOLogin");

        }

        // GET: /SbReceive/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SBReceive sbreceive = transectionManager.GetReceives().Find(a => a.Id == id);
            if (sbreceive == null)
            {
                return HttpNotFound();
            }
            return View(sbreceive);
        }

        // GET: /SbReceive/Login
        public ActionResult ReceivePositive()
        {
            if (loggedIn!=0)
            {
                ViewBag.SBDSBId = new SelectList(db.SBDSBs, "Id", "Name");
                return View();
            }
            return RedirectToAction("Login", "RPOLogin");

        }

        // POST: /SbReceive/Login
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ReceivePositive([Bind(Include="Id,DRDate,DRTime,EID,Remarks,SBDSBId,Status,DIDId")] SBReceive sbreceive)
        {
            sbreceive.DRDate = DateTime.Today;
            sbreceive.DRTime=DateTime.Now;
            sbreceive.Remarks = "Receive";
            if (ModelState.IsValid)
            {
                
                ViewBag.Message=transectionManager.Receive(sbreceive);
                if (ViewBag.Message == "File with Enrollment Id " + sbreceive.EID + "has been received " + sbreceive.Status +
                               "ly")
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    ViewBag.SBDSBId = new SelectList(db.SBDSBs, "Id", "Name", sbreceive.SBDSBId);
                    return View();
                }
                
            }

            ViewBag.SBDSBId = new SelectList(db.SBDSBs, "Id", "Name", sbreceive.SBDSBId);
            return View(sbreceive);
        }

        public ActionResult ReceiveNegative()
        {
            if (loggedIn!=0)
            {
                ViewBag.SBDSBId = new SelectList(db.SBDSBs, "Id", "Name");
                return View();
            }
            TempData["message"] = "Please Login to continue";
            return RedirectToAction("Login", "RPOLogin");

        }

        // POST: /SbReceive/Login
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ReceiveNegative([Bind(Include = "Id,DRDate,DRTime,EID,Remarks,SBDSBId,Status,DIDId")] SBReceive sbreceive)
        {
            sbreceive.DRDate = DateTime.Today;
            sbreceive.DRTime = DateTime.Now;
            sbreceive.Remarks = "Receive";
            if (ModelState.IsValid)
            {

                ViewBag.Message = transectionManager.Receive(sbreceive);
                if (ViewBag.Message == "File with Enrollment Id " + sbreceive.EID + "has been received " + sbreceive.Status +
                               "ly")
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    ViewBag.SBDSBId = new SelectList(db.SBDSBs, "Id", "Name", sbreceive.SBDSBId);
                    return View();
                }

            }

            ViewBag.SBDSBId = new SelectList(db.SBDSBs, "Id", "Name", sbreceive.SBDSBId);
            return View(sbreceive);
        }

        // GET: /SbReceive/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SBReceive sbreceive = db.SBReceives.Find(id);
            if (sbreceive == null)
            {
                return HttpNotFound();
            }
            ViewBag.SBDSBId = new SelectList(db.SBDSBs, "Id", "Name", sbreceive.SBDSBId);
            return View(sbreceive);
        }

        // POST: /SbReceive/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="Id,DRDate,DRTime,EID,Remarks,SBDSBId,Status,DIDId")] SBReceive sbreceive)
        {
            if (ModelState.IsValid)
            {
                
                return RedirectToAction("Index");
            }
            ViewBag.SBDSBId = new SelectList(db.SBDSBs, "Id", "Name", sbreceive.SBDSBId);
            return View(sbreceive);
        }

        // GET: /SbReceive/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SBReceive sbreceive = db.SBReceives.Find(id);
            if (sbreceive == null)
            {
                return HttpNotFound();
            }
            return View(sbreceive);
        }

        // POST: /SbReceive/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            SBReceive sbreceive = db.SBReceives.Find(id);
            db.SBReceives.Remove(sbreceive);
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
