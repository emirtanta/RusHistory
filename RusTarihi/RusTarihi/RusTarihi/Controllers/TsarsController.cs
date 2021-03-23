using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using RusTarihi.Models;
using RusTarihi.Models.Others;

namespace RusTarihi.Controllers
{
    public class TsarsController : Controller
    {
        private RusTarihiDBEntities db = new RusTarihiDBEntities();

        // GET: Tsars
        public ActionResult TsarList()
        {
            var tsars = db.Tsars.Include(t => t.Categories);
            return View(tsars.ToList().OrderByDescending(x=>x.ID));
        }

        // GET: Tsars/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tsars tsars = db.Tsars.Find(id);
            if (tsars == null)
            {
                return HttpNotFound();
            }
            return View(tsars);
        }

        // GET: Tsars/Create
        public ActionResult TsarCreate()
        {
            ViewBag.CategoryID = new SelectList(db.Categories, "ID", "CategoryName");
            return View();
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult TsarCreate( TsarVM model)
        {
            if (ModelState.IsValid)
            {
                Tsars tsar = new Tsars();
                tsar.FullName = model.FullName;
                tsar.TsarName = model.Name;
                tsar.Predecessor = model.Predecessor;
                tsar.Successor = model.Successor;
                tsar.Reign = model.Reign;
                tsar.BirthDate = model.BirhtDate;
                tsar.DiedDate = model.DiedDate;
                tsar.Age = Convert.ToByte(model.Age);
                tsar.Dynasty = model.Dynasty;
                tsar.CategoryID = model.CategoryID;

                db.Tsars.Add(tsar);
                db.SaveChanges();
                return RedirectToAction("TsarList");
            }

            ViewBag.CategoryID = new SelectList(db.Categories, "ID", "CategoryName", model.CategoryID);
            return View(model);
        }

        // GET: Tsars/Edit/5
        public ActionResult TsarEdit(int? id)
        {
            Tsars tsars = db.Tsars.Where(x => x.ID == id).SingleOrDefault();

            TsarVM model = new TsarVM()
            {
                ID=tsars.ID,
                FullName=tsars.FullName,
                Name=tsars.TsarName,
                Predecessor=tsars.Predecessor,
                Successor=tsars.Successor,
                Reign=tsars.Reign,
                BirhtDate=Convert.ToDateTime(tsars.BirthDate),
                DiedDate=Convert.ToDateTime(tsars.DiedDate),
                Age=Convert.ToByte(tsars.Age),
                Dynasty=tsars.Dynasty,
                CategoryID=Convert.ToInt32(tsars.CategoryID),

            };

            ViewBag.CategoryID = new SelectList(db.Categories, "ID", "CategoryName", tsars.CategoryID);
            return View(model);
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult TsarEdit(/*[Bind(Include = "ID,FullName,TsarName,Predecessor,Successor,Reign,BirthDate,DiedDate,Age,Dynasty,CategoryID")]*/ TsarVM model)
        {
            if (ModelState.IsValid)
            {
                //db.Entry(tsars).State = EntityState.Modified;

                var t = db.Tsars.Where(x => x.ID == model.ID).SingleOrDefault();

                t.ID = model.ID;
                t.FullName = model.FullName;
                t.TsarName = model.Name;
                t.Predecessor = model.Predecessor;
                t.Successor = model.Successor;
                t.Reign = model.Reign;
                t.Dynasty = model.Dynasty;
                t.BirthDate = model.BirhtDate;
                t.DiedDate = model.DiedDate;
                t.Age = Convert.ToByte(model.Age);
                t.CategoryID = Convert.ToInt32(model.CategoryID);

                db.SaveChanges();
                return RedirectToAction("TsarList");
            }
            ViewBag.CategoryID = new SelectList(db.Categories, "ID", "CategoryName", model.CategoryID);
            return View();
        }

        // GET: Tsars/Delete/5
        public ActionResult TsarDelete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tsars tsars = db.Tsars.Find(id);
            if (tsars == null)
            {
                return HttpNotFound();
            }
            return View(tsars);
        }

        // POST: Tsars/Delete/5
        [HttpPost, ActionName("TsarDelete")]
        [ValidateAntiForgeryToken]
        public ActionResult TsarDeleteConfirmed(int id)
        {
            Tsars tsars = db.Tsars.Find(id);
            db.Tsars.Remove(tsars);
            db.SaveChanges();
            return RedirectToAction("TsarList");
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
