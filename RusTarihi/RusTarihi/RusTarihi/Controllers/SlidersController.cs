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
    public class SlidersController : Controller
    {
        private RusTarihiDBEntities db = new RusTarihiDBEntities();

        // GET: Sliders
        public ActionResult SliderList()
        {
            return View(db.Sliders.ToList());
        }

        // GET: Sliders/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Sliders sliders = db.Sliders.Find(id);
            if (sliders == null)
            {
                return HttpNotFound();
            }
            return View(sliders);
        }

        // GET: Sliders/Create
        public ActionResult SliderCreate()
        {
            return View();
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SliderCreate(/*[Bind(Include = "ID,SliderSitePath,Title,Summary,Detail")]*/ SliderVM model,HttpPostedFileBase SliderSitePath)
        {
            if (ModelState.IsValid)
            {
                Sliders sliders = new Sliders();

                if (SliderSitePath!=null)
                {
                    WebImage img = new WebImage(SliderSitePath.InputStream);
                    FileInfo imginfo = new FileInfo(SliderSitePath.FileName);

                    string sliderimgname = Guid.NewGuid().ToString() + imginfo.Extension;

                    img.Resize(100, 100);

                    img.Save("~/Uploads/sliders/" + sliderimgname);

                    model.SliderSitePath = "/Uploads/sliders/" + sliderimgname;

                    
                }

                sliders.SliderSitePath = model.SliderSitePath;
                sliders.Title = model.Title;
                sliders.Summary = model.Summary;
                sliders.Detail = model.Detail;

                db.Sliders.Add(sliders);
                db.SaveChanges();
                return RedirectToAction("SliderList");
            }

            return View(model);
        }

        // GET: Sliders/Edit/5
        public ActionResult SliderEdit(int? id)
        {
            //if (id == null)
            //{
            //    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            //}
            //Sliders sliders = db.Sliders.Find(id);

            Sliders sliders = db.Sliders.Where(x => x.ID == id).SingleOrDefault();

            SliderVM model = new SliderVM()
            {
                ID=sliders.ID,
                SliderSitePath=sliders.SliderSitePath,
                Title=sliders.Title,
                Summary=sliders.Summary,
                Detail=sliders.Detail
            };

            if (sliders == null)
            {
                return HttpNotFound();
            }
            return View(model);
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SliderEdit(/*[Bind(Include = "ID,SliderSitePath,Title,Summary,Detail")] */int id,SliderVM model,HttpPostedFileBase SliderSitePath)
        {
            if (ModelState.IsValid)
            {
                var s = db.Sliders.Where(x => x.ID == model.ID).SingleOrDefault();

                if (SliderSitePath!=null)
                {
                    if (System.IO.File.Exists(Server.MapPath(s.SliderSitePath)))
                    {
                        System.IO.File.Delete(Server.MapPath(s.SliderSitePath));
                    }

                    WebImage img = new WebImage(SliderSitePath.InputStream);
                    FileInfo imginfo = new FileInfo(SliderSitePath.FileName);

                    string sliderimgname = Guid.NewGuid().ToString() + imginfo.Extension;

                    img.Resize(100, 100);

                    img.Save("~/Uploads/sliders/" + sliderimgname);

                    model.SliderSitePath = "/Uploads/sliders/" + sliderimgname;
                }

                s.ID = model.ID;
                s.SliderSitePath = model.SliderSitePath;
                s.Title = model.Title;
                s.Summary = model.Summary;
                s.Detail = model.Detail;

                //db.Entry(sliders).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("SliderList");
            }
            return View();
        }

        // GET: Sliders/Delete/5
        public ActionResult SliderDelete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Sliders sliders = db.Sliders.Find(id);
            if (sliders == null)
            {
                return HttpNotFound();
            }
            return View(sliders);
        }

        // POST: Sliders/Delete/5
        [HttpPost, ActionName("SliderDelete")]
        [ValidateAntiForgeryToken]
        public ActionResult SliderDeleteConfirmed(int id)
        {
            Sliders sliders = db.Sliders.Find(id);
            db.Sliders.Remove(sliders);
            db.SaveChanges();
            return RedirectToAction("SliderList");
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
