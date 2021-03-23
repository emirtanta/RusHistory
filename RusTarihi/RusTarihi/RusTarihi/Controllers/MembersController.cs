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
using RusTarihi.Models.Account;

namespace RusTarihi.Controllers
{
    public class MembersController : Controller
    {
        private RusTarihiDBEntities db = new RusTarihiDBEntities();

        #region Üyeler Listesi Bölgesi

        public ActionResult MemberList()
        {
            var members = db.Members.Include(m => m.Rols);
            return View(members.ToList());
        }

        #endregion

        #region Üye Rol Düzenleme Bölgesi

        // GET: Members1/Edit/5
        public ActionResult MemberEdit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Members members = db.Members.Find(id);
            if (members == null)
            {
                return HttpNotFound();
            }
            ViewBag.RolID = new SelectList(db.Rols, "ID", "Name", members.RolID);
            return View(members);
        }

        // POST: Members1/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult MemberEdit([Bind(Include = "ID,MemberPicturePath,UserName,Name,Surname,Email,Password,AddedDate,ModifiedDate,RolID")] Members members)
        {
            if (ModelState.IsValid)
            {
                db.Entry(members).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.RolID = new SelectList(db.Rols, "ID", "Name", members.RolID);
            return View(members);
        }

        #endregion

        // GET: Members/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Members members = db.Members.Find(id);
            if (members == null)
            {
                return HttpNotFound();
            }
            return View(members);
        }

        #region Register Bölgesi

        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(Members members, string sifre, string eposta)
        {
            if (ModelState.IsValid)
            {
                //Şifrenin şifreli şekilde olmasını sağladık
                //members.Password = Crypto.Hash(sifre, "MD5");

                db.Members.Add(members);

                db.SaveChanges();

                return RedirectToAction("MemberList");
            }

            return View(members);

        }


        #endregion


        #region Üye Silme Bölgesi

        // GET: Members/Delete/5
        public ActionResult MemberDelete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Members members = db.Members.Find(id);
            if (members == null)
            {
                return HttpNotFound();
            }
            return View(members);
        }

        // POST: Members/Delete/5
        [HttpPost, ActionName("MemberDelete")]
        [ValidateAntiForgeryToken]
        public ActionResult MemberDeleteConfirmed(int id)
        {
            Members members = db.Members.Find(id);
            db.Members.Remove(members);
            db.SaveChanges();
            return RedirectToAction("MemberList");
        }


        #endregion

        #region Deneme Register Bölgesi

        //public ActionResult Register()
        //{
        //    ViewBag.RolID = new SelectList(db.Rols, "ID", "Name");

        //    return View();
        //}

        //[HttpPost]
        //public ActionResult Register(Models.Account.RegisterVM user)
        //{
        //    //kayıt başarılı ise
        //    try
        //    {
        //        if (user.rePassword!=user.Member.Password)
        //        {
        //            throw new Exception("Şifreler Aynı Değil");
        //        }

        //        //aynı emailden kayıt olamam durumu
        //        if (db.Members.Any(x=>x.Email==user.Member.Email))
        //        {
        //            throw new Exception("Bu email zaten var");
        //        }

        //        context.Members.Add(user.Member);

        //        context.SaveChanges();

        //        return RedirectToAction("Login", "Members");
        //    }
        //    catch (Exception ex)
        //    {
        //        ViewBag.RegError = ex.Message;

        //        return View();
        //    }
        //}

        #endregion

        #region Login Bölgesi

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(Members model,string sifre)
        {
            //var md5pass = Crypto.Hash(sifre, "MD5");

            var login = db.Members.Where(x => x.Email == model.Email).SingleOrDefault();

            if (login.Email==model.Email && login.Password==model.Password)
            {
                Session["memberid"] = login.ID;
                Session["eposta"] = login.Email;

                //belli bölgeilere belirli kullanıcılar için yetki verme işlemi
                Session["yetki"] = login.RolID;

                return RedirectToAction("Index", "Home");
            }

            ViewBag.Uyari = "Kullanıcı adı ya da şifre yanlış";

            return View(model);
        }

        #endregion

        #region Logout Bölgesi

        public ActionResult Logout()
        {
            Session["memberid"] = null;
            Session["eposta"] = null;

            //sessionları sildik
            Session.Abandon();

            return RedirectToAction("Login", "Members");
        }

        #endregion

        #region Profil Bölgesi

        public ActionResult Profil(int id = 0, string ad = "")
        {

            var user = db.Members.FirstOrDefault(x => x.ID == id);

            if (user == null)
            {
                return RedirectToAction("Index", "Home");
            }

            ProfilVM model = new ProfilVM()
            {
                Members = user,
            };

            return View(model);
        }

        #endregion

        #region Profil Düzenleme Bölgesi

        public ActionResult ProfilEdit(int id)
        {
             

            var user = db.Members.FirstOrDefault(x => x.ID == id);

            if (user == null)
            {
                return RedirectToAction("Index", "Home");
            }

            ProfilVM model = new ProfilVM()
            {
                Members = user,
            };

            return View(model);
        }

        [HttpPost]
        public ActionResult ProfilEdit(ProfilVM model, int id)
        {
            try
            {

                var updateMember = db.Members.FirstOrDefault(x => x.ID == id);

                updateMember.Name = model.Members.Name;
                updateMember.Surname = model.Members.Surname;

                if (string.IsNullOrEmpty(model.Members.Password) == false)
                {
                    updateMember.Password = model.Members.Password;
                }

                //resim yükleme işlemi
                if (Request.Files != null && Request.Files.Count > 0)
                {
                    var file = Request.Files[0];

                    if (file.ContentLength > 0)
                    {
                        //resmin fiziksel klasörü
                        var folder = Server.MapPath("~/Uploads/uyeResimler");

                        //resmi jpg uzantılı  guid ile
                        var fileName = Guid.NewGuid() + ".jpg";

                        //resim kaydedildi
                        file.SaveAs(Path.Combine(folder, fileName));

                        //veritabanına resmi gösterme işlemi
                        var filePath = "Uploads/uyeResimler/" + fileName;

                        updateMember.MemberPicturePath = filePath;
                    }
                }

                db.SaveChanges();

                return RedirectToAction("Profil", "Members");
            }
            catch (Exception ex)
            {
                ViewBag.MyError = ex.Message;
                

                var viewModel = new Models.Account.ProfilVM()
                {
                    Members = db.Members.FirstOrDefault(x => x.ID == id)
                };

                return View(viewModel);
            }
        }

        #endregion

        #region Şifremi Unuttum Bölgesi

        public ActionResult ForgotPassword(string email)
        {
            //mail adresinden ilgili kişiyi bulduk
            var member = db.Members.FirstOrDefault(x => x.Email == email);

            if (member == null)
            {
                ViewBag.MyError = "Böyle bir kullanıcı adı yok";

                return View();
            }

            //kullanıcı hesabı varsa
            else
            {
                //mail gönderme işlemi
                var body = "Şifreniz : " + member.Password;

                MyMail mail = new MyMail(member.Email, "Şifremi Unuttum", body);

                mail.SendMail();

                //mesaj gönderimi
                TempData["Info"] = email + " mail adresinize şifreniz gönderildi";

                return RedirectToAction("Login", "Members");
            }
        }

        //public ActionResult ForgotPassword(string eposta)
        //{
        //    var mail = db.Members.Where(x => x.Email == eposta).SingleOrDefault();

        //    //şifreyi mail adresine gönderme işlemi
        //    if (mail!=null)
        //    {
        //        Random rnd = new Random();
        //        int newPassword = rnd.Next();

        //        Members sifre = new Members();
        //        mail.Password = Convert.ToString(newPassword); //Crypto.Hash(Convert.ToString(newPassword), "MD5");

        //        db.SaveChanges();

        //        WebMail.SmtpServer = "smtp.gmail.com";
        //        WebMail.EnableSsl = true;
        //        WebMail.UserName = "emirdeneme41@gmail.com"; //ana mail adresi
        //        WebMail.Password = "Et19901990";
        //        WebMail.SmtpPort = 587;
        //        WebMail.Send(eposta, "Admin Panel Giriş Şifreniz", "Şifreniz: " + newPassword);
        //        //WebMail.Send("emirtanta41@gmail.com", konu, email + "-" + mesaj);

        //        ViewBag.Uyari = "Şifrenin Mail Adresinize Gönderildi";
        //    }

        //    else
        //    {
        //        ViewBag.Uyari = "şifre mail adresinize gönderildi";
        //    }

        //    return View();
        //}

        #endregion

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
