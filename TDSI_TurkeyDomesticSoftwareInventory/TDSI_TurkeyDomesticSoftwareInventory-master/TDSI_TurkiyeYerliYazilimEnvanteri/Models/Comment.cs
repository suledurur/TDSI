//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace TDSI_TurkiyeYerliYazilimEnvanteri.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Comment
    {
        public int accountID { get; set; }
        public int softwareID { get; set; }
        public short rate { get; set; }
        public int commentID { get; set; }
        public string commentContent { get; set; }
        public System.DateTime commentDate { get; set; }
    
        public virtual Account Account { get; set; }
        public virtual Software Software { get; set; }
    }
}
