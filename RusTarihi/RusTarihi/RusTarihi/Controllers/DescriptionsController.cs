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
    public class DescriptionsController : Controller
    {
        private RusTarihiDBEntities db = new RusTarihiDBEntities();

        // GET: Descriptions
        public ActionResult DescriptionList()
        {
            var descriptions = db.Descriptions.Include(d => d.Categories).Include(d => d.Tsars);
            return View(descriptions.ToList());
        }
        

        // GET: Descriptions/Create
        public ActionResult DescriptionCreate()
        {
            ViewBag.CategoryID = new SelectList(db.Categories, "ID", "CategoryName");

            ViewBag.TsarID = new SelectList(db.Tsars, "ID", "FullName");

            return View();
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult DescriptionCreate( DescriptionVM model)
        {
            if (ModelState.IsValid)
            {
                Descriptions descriptions = new Descriptions();
                descriptions.Title = model.Title;
                descriptions.Detail = model.Detail;
                descriptions.TsarID = model.TsarID;
                descriptions.CategoryID = model.CategoryID;

                db.Descriptions.Add(descriptions);
                db.SaveChanges();
                return RedirectToAction("DescriptionList");
            }

            ViewBag.CategoryID = new SelectList(db.Categories, "ID", "CategoryName", model.CategoryID);

            ViewBag.TsarID = new SelectList(db.Tsars, "ID", "FullName", model.TsarID);

            return View(model);
        }

        // GET: Descriptions/Edit/5
        public ActionResult DescriptionEdit(int? id)
        {
            
            Descriptions descriptions = db.Descriptions.Where(x=>x.ID==id).SingleOrDefault();

            DescriptionVM model = new DescriptionVM()
            {
                ID = descriptions.ID,
                Title = descriptions.Title,
                Detail = descriptions.Detail,
                TsarID = Convert.ToInt32(descriptions.TsarID),
                CategoryID = Convert.ToInt32(descriptions.CategoryID),
            };
            
            ViewBag.CategoryID = new SelectList(db.Categories, "ID", "CategoryName", descriptions.CategoryID);

            ViewBag.TsarID = new SelectList(db.Tsars, "ID", "FullName", descriptions.TsarID);

            return View(descriptions);
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DescriptionEdit( DescriptionVM model)
        {
            if (ModelState.IsValid)
            {
                var d = db.Descriptions.Where(x => x.ID == model.ID).SingleOrDefault();

                d.ID = model.ID;
                d.Title = model.Title;
                d.Detail = model.Detail;
                d.TsarID = Convert.ToInt32(model.TsarID);
                d.CategoryID = Convert.ToInt32(model.CategoryID);

                
                db.SaveChanges();
                return RedirectToAction("DescriptionList");
            }
            ViewBag.CategoryID = new SelectList(db.Categories, "ID", "CategoryName", model.CategoryID);

            ViewBag.TsarID = new SelectList(db.Tsars, "ID", "FullName", model.TsarID);

            return View();
        }

        // GET: Descriptions/Delete/5
        public ActionResult DescriptionDelete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Descriptions descriptions = db.Descriptions.Find(id);
            if (descriptions == null)
            {
                return HttpNotFound();
            }
            return View(descriptions);
        }

        // POST: Descriptions/Delete/5
        [HttpPost, ActionName("DescriptionDelete")]
        [ValidateAntiForgeryToken]
        public ActionResult DescriptionDeleteConfirmed(int id)
        {
            Descriptions descriptions = db.Descriptions.Find(id);
            db.Descriptions.Remove(descriptions);
            db.SaveChanges();
            return RedirectToAction("DescriptionList");
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
