//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace RusTarihi.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Comments
    {
        public int ID { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Detail { get; set; }
        public Nullable<System.DateTime> AddedDate { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
        public Nullable<bool> IsConfirm { get; set; }
        public Nullable<int> CategoryID { get; set; }
        public Nullable<int> MemberID { get; set; }
        public Nullable<int> TsarID { get; set; }
    
        public virtual Categories Categories { get; set; }
        public virtual Members Members { get; set; }
        public virtual Tsars Tsars { get; set; }
    }
}