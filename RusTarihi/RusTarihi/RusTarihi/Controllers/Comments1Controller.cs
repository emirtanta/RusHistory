using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using RusTarihi.Models;

namespace RusTarihi.Controllers
{
    public class Comments1Controller : Controller
    {
        private RusTarihiDBEntities db = new RusTarihiDBEntities();

        // GET: Comments1
        public ActionResult Index()
        {
            var comments = db.Comments.Include(c => c.Categories).Include(c => c.Members).Include(c => c.Tsars);
            return View(comments.ToList());
        }

        // GET: Comments1/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Comments comments = db.Comments.Find(id);
            if (comments == null)
            {
                return HttpNotFound();
            }
            return View(comments);
        }

        // GET: Comments1/Create
        public ActionResult Create()
        {
            ViewBag.CategoryID = new SelectList(db.Categories, "ID", "CategoryName");
            ViewBag.MemberID = new SelectList(db.Members, "ID", "MemberPicturePath");
            ViewBag.TsarID = new SelectList(db.Tsars, "ID", "FullName");
            return View();
        }

        // POST: Comments1/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,FullName,Email,Detail,AddedDate,ModifiedDate,IsConfirm,CategoryID,MemberID,TsarID")] Comments comments)
        {
            if (ModelState.IsValid)
            {
                db.Comments.Add(comments);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CategoryID = new SelectList(db.Categories, "ID", "CategoryName", comments.CategoryID);
            ViewBag.MemberID = new SelectList(db.Members, "ID", "MemberPicturePath", comments.MemberID);
            ViewBag.TsarID = new SelectList(db.Tsars, "ID", "FullName", comments.TsarID);
            return View(comments);
        }

        // GET: Comments1/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Comments comments = db.Comments.Find(id);
            if (comments == null)
            {
                return HttpNotFound();
            }
            ViewBag.CategoryID = new SelectList(db.Categories, "ID", "CategoryName", comments.CategoryID);
            ViewBag.MemberID = new SelectList(db.Members, "ID", "MemberPicturePath", comments.MemberID);
            ViewBag.TsarID = new SelectList(db.Tsars, "ID", "FullName", comments.TsarID);
            return View(comments);
        }

        // POST: Comments1/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,FullName,Email,Detail,AddedDate,ModifiedDate,IsConfirm,CategoryID,MemberID,TsarID")] Comments comments)
        {
            if (ModelState.IsValid)
            {
                db.Entry(comments).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CategoryID = new SelectList(db.Categories, "ID", "CategoryName", comments.CategoryID);
            ViewBag.MemberID = new SelectList(db.Members, "ID", "MemberPicturePath", comments.MemberID);
            ViewBag.TsarID = new SelectList(db.Tsars, "ID", "FullName", comments.TsarID);
            return View(comments);
        }

        // GET: Comments1/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Comments comments = db.Comments.Find(id);
            if (comments == null)
            {
                return HttpNotFound();
            }
            return View(comments);
        }

        // POST: Comments1/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Comments comments = db.Comments.Find(id);
            db.Comments.Remove(comments);
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
