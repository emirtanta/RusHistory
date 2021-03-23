using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using RusTarihi.Models;
using RusTarihi.Models.Account;

namespace RusTarihi.Controllers
{
    public class RolsController : Controller
    {
        private RusTarihiDBEntities db = new RusTarihiDBEntities();

        // GET: Rols
        public ActionResult RolList()
        {
            return View(db.Rols.ToList());
        }

        // GET: Rols/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Rols rols = db.Rols.Find(id);
            if (rols == null)
            {
                return HttpNotFound();
            }
            return View(rols);
        }

        // GET: Rols/Create
        public ActionResult RolCreate()
        {
            return View();
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult RolCreate(/*[Bind(Include = "ID,Seviye,Name")]*/ RolVM model)
        {
            if (ModelState.IsValid)
            {

                Rols rols = new Rols();
                rols.Name = model.RolName;
                rols.Seviye = model.Seviye;

                db.Rols.Add(rols);
                db.SaveChanges();
                return RedirectToAction("RolList");
            }

            return View(model);
        }

        // GET: Rols/Edit/5
        public ActionResult RolEdit(int? id)
        {
            Rols rols = db.Rols.Where(x=>x.ID==id).SingleOrDefault();

            RolVM model = new RolVM()
            {
                ID = rols.ID,
                RolName = rols.Name,
                Seviye = rols.Seviye.Value
            };

            return View(model);
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult RolEdit(/*[Bind(Include = "ID,Seviye,Name")]*/ RolVM model)
        {
            if (ModelState.IsValid)
            {
                var r = db.Rols.Where(x=>x.ID==model.ID).SingleOrDefault();

                r.ID = model.ID;
                r.Name = model.RolName;
                r.Seviye = model.Seviye;
                

                db.SaveChanges();

                ViewBag.sonuc = "Kayıt Güncelle";

                return RedirectToAction("RolList");
            }
            return View();
        }

        // GET: Rols/Delete/5
        public ActionResult RolDelete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Rols rols = db.Rols.Find(id);
            if (rols == null)
            {
                return HttpNotFound();
            }
            return View(rols);
        }

        // POST: Rols/Delete/5
        [HttpPost, ActionName("RolDelete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Rols rols = db.Rols.Find(id);
            

            db.Rols.Remove(rols);
            db.SaveChanges();
            return RedirectToAction("RolList");
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
