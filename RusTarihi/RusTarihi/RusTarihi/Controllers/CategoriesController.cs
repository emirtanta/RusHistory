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
    public class CategoriesController : Controller
    {
        private RusTarihiDBEntities db = new RusTarihiDBEntities();

        // GET: Categories
        public ActionResult CategoryList()
        {
            return View(db.Categories.ToList());
        }
        

        // GET: Categories/Create
        public ActionResult CategoryCreate()
        {
            return View();
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CategoryCreate(/*[Bind(Include = "ID,CategoryName,Aralık,Start,Finish")]*/ CategoryVM model)
        {
            if (ModelState.IsValid)
            {
                Categories categories = new Categories();
                categories.CategoryName = model.CategoryName;
                categories.Aralık = model.Frequency;
                categories.Start = model.StartDate;
                categories.Finish = model.FinishDate;

                db.Categories.Add(categories);
                db.SaveChanges();
                return RedirectToAction("CategoryList");
            }

            return View(model);
        }

        // GET: Categories/Edit/5
        public ActionResult CategoryEdit(int? id)
        {
           
            Categories categories = db.Categories.Where(x=>x.ID==id).SingleOrDefault();

            CategoryVM model = new CategoryVM()
            {
                ID=categories.ID,
                CategoryName=categories.CategoryName,
                Frequency=categories.Aralık,
                StartDate=Convert.ToDateTime(categories.Start),
                FinishDate=Convert.ToDateTime(categories.Finish)
            };
            
            return View(model);
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CategoryEdit( CategoryVM model)
        {
            if (ModelState.IsValid)
            {
                var c = db.Categories.Where(x => x.ID == model.ID).SingleOrDefault();

                c.ID = model.ID;
                c.CategoryName = model.CategoryName;
                c.Aralık = model.Frequency;
                c.Start = model.StartDate;
                c.Finish = model.FinishDate;
                
                db.SaveChanges();
                return RedirectToAction("CategoryList");
            }
            return View();
        }

        // GET: Categories/Delete/5
        public ActionResult CategoryDelete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Categories categories = db.Categories.Find(id);
            if (categories == null)
            {
                return HttpNotFound();
            }
            return View(categories);
        }

        // POST: Categories/Delete/5
        [HttpPost, ActionName("CategoryDelete")]
        [ValidateAntiForgeryToken]
        public ActionResult CategoryDeleteConfirmed(int id)
        {
            Categories categories = db.Categories.Find(id);
            db.Categories.Remove(categories);
            db.SaveChanges();
            return RedirectToAction("CategoryList");
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
