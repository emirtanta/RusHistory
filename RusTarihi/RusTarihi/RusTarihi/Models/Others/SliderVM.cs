using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace RusTarihi.Models.Others
{
    public class SliderVM
    {
        public int ID { get; set; }

        [Required(ErrorMessage ="Slider resmi yüklenmesi zorunludur")]
        [Display(Name ="Slider Resmi")]
        public string SliderSitePath { get; set; }

        [Required(ErrorMessage ="Başlık alanı gereklidir")]
        [Display(Name ="Başlık")]
        [StringLength(50,ErrorMessage ="Başlık alanı 50 karakterş geçemez")]
        public string Title { get; set; }

        [Required(ErrorMessage ="Özet alanı zorunludur")]
        [Display(Name ="Özet")]
        [StringLength(50,ErrorMessage ="Özet alanı 50 karakteri aşamaz")]
        public string Summary { get; set; }

        [Display(Name ="Detay")]
        public string Detail { get; set; }
    }
}