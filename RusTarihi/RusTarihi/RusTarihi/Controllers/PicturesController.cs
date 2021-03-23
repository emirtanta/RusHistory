using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using RusTarihi.Models;
using RusTarihi.Models.Others;

namespace RusTarihi.Controllers
{
    public class PicturesController : Controller
    {
        private RusTarihiDBEntities db = new RusTarihiDBEntities();

        // GET: Pictures
        public ActionResult PictureList()
        {
            var pictures = db.Pictures.Include(p => p.Tsars);
            return View(pictures.ToList());
        }

        // GET: Pictures/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Pictures pictures = db.Pictures.Find(id);
            if (pictures == null)
            {
                return HttpNotFound();
            }
            return View(pictures);
        }

        // GET: Pictures/Create
        public ActionResult PictureCreate()
        {
            ViewBag.TsarID = new SelectList(db.Tsars, "ID", "FullName");
            return View();
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult PictureCreate(/*[Bind(Include = "ID,PictureSitePath,TsarID")]*/ PictureVM model,HttpPostedFileBase PictureSitePath)
        {
            if (ModelState.IsValid)
            {
                Pictures pictures = new Pictures();

                if (PictureSitePath!=null)
                {
                    WebImage img = new WebImage(PictureSitePath.InputStream);
                    FileInfo imginfo = new FileInfo(PictureSitePath.FileName);

                    string imgname = Guid.NewGuid().ToString() + imginfo.Extension;

                    img.Resize(225, 255);

                    img.Save("~/Uploads/carResimler/" + imgname);

                    model.PictureSitePath = "/Uploads/carResimler/" + imgname;
                }

                pictures.PictureSitePath = model.PictureSitePath;
                pictures.TsarID = model.TsarID;


                db.Pictures.Add(pictures);
                db.SaveChanges();
                return RedirectToAction("PictureList");
            }

            ViewBag.TsarID = new SelectList(db.Tsars, "ID", "FullName", model.TsarID);
            return View(model);
        }

        // GET: Pictures/Edit/5
        public ActionResult PictureEdit(int? id)
        {
            //if (id == null)
            //{
            //    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            //}
            Pictures pictures = db.Pictures.Where(x=>x.ID==id).SingleOrDefault();

            PictureVM model = new PictureVM()
            {

                PictureSitePath = pictures.PictureSitePath,
                TsarID =Convert.ToInt32(pictures.TsarID)
            };
            if (pictures == null)
            {
                return HttpNotFound();
            }
            ViewBag.TsarID = new SelectList(db.Tsars, "ID", "FullName", pictures.TsarID);
            return View(model);
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult PictureEdit(/*[Bind(Include = "ID,PictureSitePath,TsarID")]*/ int id,PictureVM model,HttpPostedFileBase PictureSitePath)
        {

            var p = db.Pictures.Where(x => x.ID == model.ID).SingleOrDefault();

            if (ModelState.IsValid)
            {

                if (PictureSitePath!=null)
                {
                    WebImage img = new WebImage(PictureSitePath.InputStream);
                    FileInfo imginfo = new FileInfo(PictureSitePath.FileName);

                    string imgname = Guid.NewGuid().ToString() + imginfo.Extension;

                    img.Resize(255, 255);

                    img.Save("~/Uploads/carResimler/" + imgname);

                    model.PictureSitePath = "/Uploads/carResimler/" + imgname;
                }

                p.PictureSitePath = model.PictureSitePath;
                p.TsarID = model.TsarID;
                
                db.SaveChanges();
                return RedirectToAction("PictureList");
            }
            ViewBag.TsarID = new SelectList(db.Tsars, "ID", "FullName", p.TsarID);
            return View(model);
        }

        // GET: Pictures/Delete/5
        public ActionResult PictureDelete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Pictures pictures = db.Pictures.Find(id);
            if (pictures == null)
            {
                return HttpNotFound();
            }
            return View(pictures);
        }

        // POST: Pictures/Delete/5
        [HttpPost, ActionName("PictureDelete")]
        [ValidateAntiForgeryToken]
        public ActionResult PictureDeleteConfirmed(int id)
        {
            Pictures pictures = db.Pictures.Find(id);
            db.Pictures.Remove(pictures);
            db.SaveChanges();
            return RedirectToAction("PictureList");
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
