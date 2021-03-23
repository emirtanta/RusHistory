using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace RusTarihi.Models.Others
{
    public class CommentVM
    {
        public int ID { get; set; }

        [Required(ErrorMessage ="ad soyad zorunludur")]
        [Display(Name ="Ad Soyad")]
        [StringLength(50,ErrorMessage ="50 karakteri geçemez")]
        public string FullName { get; set; }

        public string Email { get; set; }

        [Required(ErrorMessage = "Detay alanı zorunludur")]
        [Display(Name = "Detay")]
        public string Detail { get; set; }

        [Display(Name = "Eklenme Tarihi")]
        [DisplayFormat(DataFormatString = "{0:d}", ApplyFormatInEditMode = true)]
        public DateTime AddedDate { get; set; }

        [Display(Name = "Düzenleme Tarihi")]
        [DisplayFormat(DataFormatString = "{0:d}", ApplyFormatInEditMode = true)]
        public DateTime ModifiedDate { get; set; }

        [Required(ErrorMessage ="Onay kısmı zorunludur")]
        [Display(Name ="Onaylandı mı?")]
        public bool IsConfirm { get; set; }

        public int CategoryID { get; set; }

        public int MemberID { get; set; }

        public int TsarID { get; set; }
    }
}