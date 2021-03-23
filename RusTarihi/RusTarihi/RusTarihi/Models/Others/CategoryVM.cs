using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace RusTarihi.Models.Others
{
    public class CategoryVM
    {
        public int ID { get; set; }

        [Display(Name = "Kategori Adı")]
        public string CategoryName { get; set; }

        [Display(Name ="Aralık")]
        public string Frequency { get; set; }

        [Display(Name ="Başlangıç Tarihi")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        //[DisplayFormat(DataFormatString = "{0:d}", ApplyFormatInEditMode = true)]
        public DateTime StartDate { get; set; }

        [Display(Name = "Bitiş Tarihi")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        //[DisplayFormat(DataFormatString = "{0:d}", ApplyFormatInEditMode = true)]
        public DateTime FinishDate { get; set; }
    }
}