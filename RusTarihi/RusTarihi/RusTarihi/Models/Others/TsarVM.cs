using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace RusTarihi.Models.Others
{
    public class TsarVM
    {
        public int ID { get; set; }

        [Required(ErrorMessage = "Ad alanı gereklidir")]
        [Display(Name = "Tam Adı")]
        [StringLength(60, ErrorMessage = "60 karakteri aşamaz")]
        public string FullName { get; set; }

        [Required(ErrorMessage = "Ad alanı gereklidir")]
        [Display(Name = "Çar Adı")]
        [StringLength(50, ErrorMessage = "50 karakteri aşamaz")]
        public string Name { get; set; }

        [Display(Name ="Selef")]
        [StringLength(50, ErrorMessage = "50 karakteri aşamaz")]
        public string Predecessor { get; set; }

        [Display(Name ="Halef")]
        [StringLength(50, ErrorMessage = "50 karakteri aşamaz")]
        public string Successor { get; set; }

        [Display(Name ="Hüküm Süresi")]
        public string Reign { get; set; }

        [Display(Name = "Doğum Tarihi")]
        //[DisplayFormat(DataFormatString = "{0:d}", ApplyFormatInEditMode = true)]
        [DisplayFormat(DataFormatString = "{0:d}", NullDisplayText = "Dogum Tarihi Girilmemiş")]
        public DateTime BirhtDate { get; set; }

        [Display(Name ="Ölüm Tarihi")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        //[DisplayFormat(DataFormatString = "{0:d}", ApplyFormatInEditMode = true)]
        public DateTime DiedDate { get; set; }

        [Display(Name ="Yaş")]
        [Range(1,85,ErrorMessage ="1 ile 85 sayısı arasında bir değer girin")]
        public int Age { get; set; }

        [Display(Name ="Hanedan Adı")]
        [StringLength(50, ErrorMessage = "50 karakteri aşamaz")]
        public string Dynasty { get; set; }

        public int CategoryID { get; set; }
    }
}