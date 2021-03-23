using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Web;

namespace RusTarihi.Models.Account
{
    public class MyMail
    {
        public string ToMail { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }

        public MyMail(string _tomail, string _subject, string _body)
        {
            this.ToMail = _tomail;
            this.Subject = _subject;
            this.Body = _body;
        }

        public void SendMail()
        {
            MailMessage mail = new MailMessage()
            {
                //mail göndereceğimiz mail adresimizi yazıyoruz,ikinci kısım istediğimiz bir cümleyi yazabiliriz
                From = new MailAddress("emirdeneme41@gmail.com", "Noktasal Yazılım Sitesi")
            };

            mail.To.Add(this.ToMail);
            mail.Subject = this.Subject;
            mail.Body = this.Body;

            //smtp ayarlamaları gmail için
            SmtpClient client = new SmtpClient()
            {
                Port = 587,
                Host = "smtp.gmail.com",
                EnableSsl = true
            };

            //açtığımız gmail hesabından ilgili adreslere mail gönderebilmek için gmail hesabımızı ve şifremizi girdik
            client.Credentials = new System.Net.NetworkCredential("emirdeneme41@gmail.com", "Et19901990");

            client.Send(mail);
        }
    }
}