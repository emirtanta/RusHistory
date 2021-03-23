using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace RusTarihi.Models.Others
{
    public class DescriptionVM
    {
        public int ID { get; set; }

        [Required(ErrorMessage ="Başlık alanı zorunludur")]
        [Display(Name ="Başlık")]
        [StringLength(50,ErrorMessage ="50 karakteri geçemez")]
        public string Title { get; set; }

        [Required(ErrorMessage ="Detay girilmesi zorunludur")]
        [Display(Name ="Detay")]
        [StringLength(50,ErrorMessage ="50 karakteri aşamaz")]
        public string Detail { get; set; }

        public int TsarID { get; set; }

        public int CategoryID { get; set; }
    }
}