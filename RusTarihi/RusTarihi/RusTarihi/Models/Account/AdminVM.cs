using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RusTarihi.Models.Account
{
    public class AdminVM
    {
        public int ID { get; set; }
        public string UserName { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public DateTime AddedDate { get; set; }
        public DateTime ModifiedDate { get; set; }

        public int RolID { get; set; }

    }
}