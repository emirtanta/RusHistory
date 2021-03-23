using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace RusTarihi.Models.Account
{
    public class RolVM
    {
        public int ID { get; set; }

        [Required(ErrorMessage ="Rol Adı Gereklidir")]
        [Display(Name ="Rol Adı")]
        [StringLength(50,ErrorMessage ="Rol adı 50 karakteri aşamaz")]
        public string RolName { get; set; }

        [Required(ErrorMessage ="Lütfen 1-10 değer aralığında sayı giriniz")]
        [Range(1,10,ErrorMessage ="Seviye en az 1 en fazla 10 olabilir")]
        public int Seviye { get; set; }

    }
}