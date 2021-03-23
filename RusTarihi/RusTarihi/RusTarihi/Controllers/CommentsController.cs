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
    public class CommentsController : Controller
    {
        private RusTarihiDBEntities db = new RusTarihiDBEntities();

        // GET: Comments
        public ActionResult CommentList()
        {
            var comments = db.Comments.Include(c => c.Categories).Include(c => c.Members);
            return View(comments.ToList());
        }

        // GET: Comments/Details/5
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

        // GET: Comments/Create
        public ActionResult CommentCreate()
        {
            ViewBag.CategoryID = new SelectList(db.Categories, "ID", "CategoryName");
            ViewBag.MemberID = new SelectList(db.Members, "ID", "UserName");
            ViewBag.TsarID = new SelectList(db.Tsars, "ID", "TsarName");
            return View();
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CommentCreate(CommentVM model)
        {
            if (ModelState.IsValid)
            {
                Comments comments = new Comments();
                comments.FullName = model.FullName;
                comments.Detail = model.Detail;
                comments.AddedDate = Convert.ToDateTime(model.AddedDate);
                comments.ModifiedDate = DateTime.Now;
                comments.Email = model.Email;
                comments.IsConfirm = model.IsConfirm;
                comments.CategoryID = model.CategoryID;
                //comments.MemberID = model.MemberID;
                comments.TsarID = model.TsarID;

                db.Comments.Add(comments);
                db.SaveChanges();
                return RedirectToAction("CommentList");
            }

            ViewBag.CategoryID = new SelectList(db.Categories, "ID", "CategoryName", model.CategoryID);
            ViewBag.MemberID = new SelectList(db.Members, "ID", "UserName", model.MemberID);

            ViewBag.TsarID = new SelectList(db.Tsars, "ID", "TsarName",model.TsarID);

            return View(model);
        }

        // GET: Comments/Edit/5
        public ActionResult CommentEdit(int? id)
        {
            
            Comments comments = db.Comments.Where(x=>x.ID==id).SingleOrDefault();

            CommentVM model = new CommentVM()
            {
                ID = comments.ID,
                FullName = comments.FullName,
                Email = comments.Email,
                Detail = comments.Detail,
                AddedDate = DateTime.Now/*Convert.ToDateTime(comments.AddedDate)*/,
                ModifiedDate = DateTime.Now,
                IsConfirm=Convert.ToBoolean(comments.IsConfirm),
                CategoryID=Convert.ToInt32(comments.CategoryID),
                //MemberID=Convert.ToInt32(comments.MemberID),
                TsarID = Convert.ToInt32(comments.TsarID)
            };
            
            ViewBag.CategoryID = new SelectList(db.Categories, "ID", "CategoryName", comments.CategoryID);

            ViewBag.MemberID = new SelectList(db.Members, "ID", "UserName", comments.MemberID);

            ViewBag.TsarID = new SelectList(db.Tsars, "ID", "TsarName",comments.TsarID);

            return View(model);
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CommentEdit(CommentVM model)
        {
            if (ModelState.IsValid)
            {
                var c = db.Comments.Where(x => x.ID == model.ID).SingleOrDefault();

                c.FullName = model.FullName;
                c.Email = model.Email;
                c.Detail = model.Detail;
                c.AddedDate = model.AddedDate;
                c.ModifiedDate = DateTime.Now;
                c.CategoryID = model.CategoryID;
                c.MemberID = model.MemberID;
                c.TsarID = model.TsarID;
                c.IsConfirm = model.IsConfirm;
                
                db.SaveChanges();
                return RedirectToAction("CommentList");
            }
            ViewBag.CategoryID = new SelectList(db.Categories, "ID", "CategoryName", model.CategoryID);
            ViewBag.MemberID = new SelectList(db.Members, "ID", "UserName", model.MemberID);

            ViewBag.TsarID = new SelectList(db.Members, "ID", "TsarName", model.TsarID);

            return View();
        }

        // GET: Comments/Delete/5
        public ActionResult CommentDelete(int? id)
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

        // POST: Comments/Delete/5
        [HttpPost, ActionName("CommentDelete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Comments comments = db.Comments.Find(id);
            db.Comments.Remove(comments);
            db.SaveChanges();
            return RedirectToAction("CommentList");
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
