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
    
    public partial class SocialMedia
    {
        public int accountID { get; set; }
        public string instagramLink { get; set; }
        public string twitterLink { get; set; }
        public string linkedinLink { get; set; }
        public string facebookLink { get; set; }
        public string youtubeLink { get; set; }
        public int smID { get; set; }
    
        public virtual Account Account { get; set; }
    }
}