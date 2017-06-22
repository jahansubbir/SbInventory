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
    public class RPOLoginController : Controller
    {
        
        private SbDBEntities db = new SbDBEntities();
        private HttpContext context=System.Web.HttpContext.Current;
        private HttpCookie cookie;
        LoginManager loginManager=new LoginManager();
        private int loginStatus;
            string userId;
        

        // GET: /RPOLogin/
        //public ActionResult Index()
        //{
        //    if (cookie["loggedIn"]==1.ToString())
        //    {


        //        return RedirectToAction("Index", "Home");
        //    }
        //    else
        //    {
        //        return RedirectToAction("Login");
        //    }
        //}

        //// GET: /RPOLogin/Details/5
        //public ActionResult Details(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    RPOLogin rpologin = db.RPOLogins.Find(id);
        //    if (rpologin == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(rpologin);
        //}

        public ActionResult Register()
        {
            ViewBag.RPOId = new SelectList(db.RPOes, "Id", "Name");
            return View();
        }
        //post:RPOLogin/Register
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(RPOLogin login)
        {
            if (ModelState.IsValid)
            {
                loginManager.Register(login);
                return RedirectToAction("Index","Home");
            }
            ViewBag.RPOId = new SelectList(db.RPOes, "Id", "Name");
            return View(login);
        }
        
        // GET: /RPOLogin/Login
        public ActionResult Login()
        {
            ViewBag.RPOId = new SelectList(db.RPOes, "Id", "Name");
            return View();
        }

        // POST: /RPOLogin/Login
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login([Bind(Include = "Id,RPOId,UserId,Password")] RPOLogin rpologin)
        {
            if (ModelState.IsValid)
            {
                 cookie=new HttpCookie("loginCookie");
                if (loginManager.Login(rpologin)=="Logged In")
                {
                    cookie["loggedIn"] = 1.ToString();
                    cookie["UserId"] = rpologin.UserId;
                    cookie.Expires = DateTime.Now.AddDays(1);
                    Response.Cookies.Add(cookie);
                    loginStatus = Convert.ToInt32(cookie["loggedIn"]);
                    userId = Convert.ToString(cookie["UserId"]);
                    return RedirectToAction("Index","Home");
                }
                ViewBag.RPOId = new SelectList(db.RPOes, "Id", "Name");
                ViewBag.Message = loginManager.Login(rpologin);
                return View();

                //db.RPOLogins.Add(rpologin);
                //db.SaveChanges();
                //return RedirectToAction("Index");
            }

            ViewBag.RPOId = new SelectList(db.RPOes, "Id", "Name", rpologin.RPOId);
            return View(rpologin);
        }

        //// GET: /RPOLogin/Edit/5
        //public ActionResult Edit(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    RPOLogin rpologin = db.RPOLogins.Find(id);
        //    if (rpologin == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    ViewBag.RPOId = new SelectList(db.RPOes, "Id", "Name", rpologin.RPOId);
        //    return View(rpologin);
        //}

        //// POST: /RPOLogin/Edit/5
        //// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        //// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Edit([Bind(Include="Id,RPOId,UserId,Password")] RPOLogin rpologin)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Entry(rpologin).State = EntityState.Modified;
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }
        //    ViewBag.RPOId = new SelectList(db.RPOes, "Id", "Name", rpologin.RPOId);
        //    return View(rpologin);
        //}

        // GET: /RPOLogin/Logout/5
        public ActionResult Logout()
        {
            cookie = Request.Cookies["loginCookie"];
            if (cookie != null)
            {
                cookie["loginStatus"] = null;
                cookie["eId"] = null;
                cookie["adminId"] = null;
                cookie.Expires = DateTime.Now;
                Response.Cookies.Add(cookie);
                return RedirectToAction("Login");
            }
            else
            {
                return RedirectToAction("Index","Home");
            }
            //if (id == null)
            //{
            //    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            //}
            //RPOLogin rpologin = db.RPOLogins.Find(id);
            //if (rpologin == null)
            //{
            //    return HttpNotFound();
            //}
            //return View(rpologin);
        }

        // POST: /RPOLogin/Logout/5
        [HttpPost, ActionName("Logout")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            cookie = Request.Cookies["loginCookie"];
            if (cookie != null)
            {
                cookie["loginStatus"] = null;
                cookie["eId"] = null;
                cookie["adminId"] = null;
                cookie.Expires = DateTime.Now;
                Response.Cookies.Add(cookie);
                return RedirectToAction("Login");
            }
            else
            {
                return RedirectToAction("Index","Home");
            }
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
