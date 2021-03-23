using RusTarihi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList; //page list kütüphanesi
using PagedList.Mvc; //page list.mvc kütüphanesi
using System.Net;

namespace RusTarihi.Controllers
{
    public class HomeController : Controller
    {
        RusTarihiDBEntities db = new RusTarihiDBEntities();

        // GET: Home
        public ActionResult Index()
        {
            return View();
        }

        //deneme veri sayfası
        public ActionResult DenemeVeri()
        {
            return View();
        }

        //deneme hükümdar tanıtım
        public ActionResult DenemeTanitim()
        {
            return View();
        }


        public ActionResult Category()
        {
            var model = db.Categories.OrderByDescending(x=>x.ID).ToList();

            return View(model);
        }

        #region hükümdar listeleme bölgesi

        public ActionResult Tsar(int Sayfa=1)
        {
            return View(db.Tsars.Include("Descriptions").Include("Categories").OrderByDescending(x => x.ID).ToPagedList(Sayfa, 5));
        }

        #endregion

        #region Kategori Çar Bölgesi

        //[Route("TsarPost/{tsarname}/{id:int}")]
        public ActionResult KategoriCar(int? id,int Sayfa=1)
        {
            //var c = db.Tsars.Include("Categories").OrderByDescending(x => x.ID).Where(x => x.Categories.ID == id).ToPagedList(Sayfa, 5);

            //return View(c);

            var category = db.Categories.OrderByDescending(x=>x.ID).ToPagedList(Sayfa, 5);

            return View(category);
        }

        #endregion

        #region Çar Detay Bölgesi

        //[Route("TsarPost/{tsarname}-{id:int}")] //Seo ile oluşturuldu
        public ActionResult TsarDetay(int? id)
        {
            var c = db.Tsars.Include("Categories").Include("Descriptions").Include("Pictures").Include("Comments").Where(x => x.ID == id).SingleOrDefault();

            return View(c);

            //var tsar = db.Tsars.Where(x => x.ID == id).ToList();
            //var description = db.Descriptions.Where(x => x.TsarID == id).ToList();
            //var picture = db.Pictures.Where(x => x.TsarID == id).ToList();
            //var comment = db.Comments.Where(x => x.TsarID == id).ToList();

            //return View(Tuple.Create(tsar, description, picture,comment));
        }

        #endregion

        #region Yorum Bölgesi


        //Yorum Bölgesi //blogdetay.cshtml'deki yorum bölgesindeki id değerleri(adsoyad,eposta,icerik) tanımlandı 
        public JsonResult YorumYap(string adsoyad, string eposta, string icerik, int tsarid)
        {
            if (icerik == null)
            {
                return Json(true, JsonRequestBehavior.AllowGet);
            }

            //Yorum veritabanına eklendi
            db.Comments.Add(new Comments { FullName = adsoyad, Email = eposta, Detail = icerik, TsarID = tsarid, IsConfirm = false });

            db.SaveChanges();

            //Response.Redirect("/Home/BlogDetay/" + blogid);

            return Json(false, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region Çar Kategori Partial Bölgesi

        public ActionResult TsarCategoryPartial()
        {
            db.Configuration.LazyLoadingEnabled = false;

            return PartialView(db.Categories.Include("Tsars").ToList().OrderByDescending(x => x.CategoryName));
        }


        #endregion

        #region Son Yüklenen Çarlar Partial Bölgesi

        public ActionResult TsarKayitPartial()
        {
            return View(db.Descriptions.ToList().OrderByDescending(x => x.ID));
        }

        #endregion
    }
}