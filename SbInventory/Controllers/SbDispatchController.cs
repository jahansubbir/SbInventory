using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CrystalDecisions.CrystalReports.Engine;
using SbInventory.BLL;
using SbInventory.Models;
using SbInventory.Report;

namespace SbInventory.Controllers
{
    public class SbDispatchController : Controller
    {
        private SbDBEntities db = new SbDBEntities();
        SbDBViewEntities viewdb=new SbDBViewEntities();
        TransectionManager transectionManager=new TransectionManager();
        private HttpContext context = System.Web.HttpContext.Current;
        private HttpCookie cookie;
        private int loggedIn;
        private string userId;

        // GET: /SdDispatch/
        public SbDispatchController()
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
                List<DispatchView> dispathces = viewdb.DispatchViews.ToList();
                return View(dispathces);
            }
            TempData["message"] = "Please Login to continue";
            return RedirectToAction("Login", "RPOLogin");

        }

        // GET: /SdDispatch/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SBDispatch sbdispatch = db.SBDispatches.Find(id);
            if (sbdispatch == null)
            {
                return HttpNotFound();
            }
            return View(sbdispatch);
        }

        // GET: /SdDispatch/Login
        public ActionResult Dispatch()
        {
            if (loggedIn!=0)
            {
                ViewBag.SBDSBId = new SelectList(db.SBDSBs, "Id", "Name");
                return View();
            }
            TempData["message"] = "Please Login to continue";
            return RedirectToAction("Login", "RPOLogin");
        }
            

        // POST: /SdDispatch/Login
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Dispatch([Bind(Include="Id,DRDate,DRTime,EID,Remarks,SBDSBId,Status")] SBDispatch sbdispatch)
        {
            sbdispatch.DRDate=DateTime.Today;
            sbdispatch.DRTime=DateTime.Now;
            sbdispatch.Remarks = "Dispatch";
            sbdispatch.Status = "New Dispatch";
            if (ModelState.IsValid)
            {
                ViewBag.Message=transectionManager.Dispatch(sbdispatch);
                if (ViewBag.Message == "Enrollment Id " + sbdispatch.EID + " has been dispatched ")
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    ViewBag.SBDSBId = new SelectList(db.SBDSBs, "Id", "Name", sbdispatch.SBDSBId);
                    return View();
                }
                
            }

            ViewBag.SBDSBId = new SelectList(db.SBDSBs, "Id", "Name", sbdispatch.SBDSBId);
            return View(sbdispatch);
        }

        // GET: /SdDispatch/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SBDispatch sbdispatch = db.SBDispatches.Find(id);
            if (sbdispatch == null)
            {
                return HttpNotFound();
            }
            ViewBag.SBDSBId = new SelectList(db.SBDSBs, "Id", "Name", sbdispatch.SBDSBId);
            return View(sbdispatch);
        }

        // POST: /SdDispatch/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="Id,DRDate,DRTime,EID,Remarks,SBDSBId,Status")] SBDispatch sbdispatch)
        {
            if (ModelState.IsValid)
            {
                
                return RedirectToAction("Index");
            }
            ViewBag.SBDSBId = new SelectList(db.SBDSBs, "Id", "Name", sbdispatch.SBDSBId);
            return View(sbdispatch);
        }

        // GET: /SdDispatch/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SBDispatch sbdispatch = db.SBDispatches.Find(id);
            if (sbdispatch == null)
            {
                return HttpNotFound();
            }
            return View(sbdispatch);
        }

        // POST: /SdDispatch/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            
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

        public ActionResult Report()
        {
            if (loggedIn!=0)
            {
                ViewBag.SbDsbs = new SelectList(db.SBDSBs.ToList(), "Id", "Name");
                return View();
            } // ViewBag.Status=
            TempData["message"] = "Please Login to continue";
            return RedirectToAction("Login", "RPOLogin");
        }
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult Report(DateTime FromDate, DateTime ToDate,DateTime? FromTime,DateTime? ToTime,string Status)
        {
            
            string period = "From " + FromDate.ToString("dd-MMM-yy") + " To " + ToDate.ToString("dd-MMM-yy");
            List<DispatchView> dispatches=null;
            List<ReceiveView> receives = null;
            receives = viewdb.ReceiveViews.ToList();
           
           
            ReportDocument reportDocument=new ReportDocument();
            if (Status == "New Dispatch" || Status == "Re-Dispatched")
            {
                dispatches = viewdb.DispatchViews.Where(a => a.Status == Status && a.DRDate >= FromDate && a.DRDate <= ToDate).ToList();
                reportDocument.Load(Path.Combine(Server.MapPath("~/Report"), "ReportDispatch.rpt"));
                reportDocument.SetDataSource(dispatches);
                reportDocument.DataDefinition.FormulaFields["UnboundString1"].Text = "'" + period + "'";
            }
            if (Status == "Positive" || Status == "Negative")
            {
                receives = receives.Where(a => a.DRDate >= FromDate && a.DRDate <= ToDate).ToList();
                receives = receives.Where(a => a.Status == Status).ToList();/*viewdb.ReceiveViews.Where(a => a.Status == Status && a.DRDate >= FromDate && a.DRDate <= ToDate).ToList();*/
                reportDocument.Load(Path.Combine(Server.MapPath("~/Report"), "ReportReceive.rpt"));
                reportDocument.SetDataSource(receives);
                reportDocument.DataDefinition.FormulaFields["UnboundString2"].Text = "'" + period + "'";
            }
           
            Response.Buffer = false;
            Response.ClearContent();
            Response.ClearHeaders();
            Stream stream = reportDocument.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
            // Stream stream1 = reportDocument.ExportToStream(CrystalDecisions.Shared.ExportFormatType.);
            stream.Seek(0, SeekOrigin.Begin);

            return File(stream, "application/pdf", "Report" + DateTime.Today.ToString("ddMMMyy-")+Status  + ".pdf");
            
        }
    }
}
