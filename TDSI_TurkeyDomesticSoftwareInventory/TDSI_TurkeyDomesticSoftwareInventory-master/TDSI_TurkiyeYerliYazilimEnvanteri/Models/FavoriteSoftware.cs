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
    
    public partial class FavoriteSoftware
    {
        public int accountID { get; set; }
        public int favsoftwareID { get; set; }
        public int fsID { get; set; }
    
        public virtual Account Account { get; set; }
        public virtual Software Software { get; set; }
    }
}