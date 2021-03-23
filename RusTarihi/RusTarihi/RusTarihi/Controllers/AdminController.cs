using RusTarihi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RusTarihi.Controllers
{
    public class AdminController : Controller
    {
        RusTarihiDBEntities db = new RusTarihiDBEntities();

        // GET: Admin
        public ActionResult Index()
        {
            ViewBag.CarSay = db.Tsars.Count();

            ViewBag.KategoriSay = db.Categories.Count();

            ViewBag.YorumSay = db.Comments.Count();

            //yorumların onay sayısını ve değerini geitirir
            ViewBag.YorumOnay = db.Comments.Where(x => x.IsConfirm == false).Count();

            var sorgu = db.Categories.ToList();

            return View(sorgu);
        }

        //denem tablo sayfası
        public ActionResult DenemeTablo()
        {
            return View();
        }

        //deneme form
        public ActionResult DenemeForm()
        {
            return View();
        }
    }
}