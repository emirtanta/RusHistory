using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace RusTarihi.Models.Others
{
    public class PictureVM
    {
        public int ID { get; set; }

        [Required(ErrorMessage ="Resim Yüklemek zorunludur")]
        [Display(Name ="Resim Yükleyin")]
        public string PictureSitePath { get; set; }

        public int TsarID { get; set; }
    }
}