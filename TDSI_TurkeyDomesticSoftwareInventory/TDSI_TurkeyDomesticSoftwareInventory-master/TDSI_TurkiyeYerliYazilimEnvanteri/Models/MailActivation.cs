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
    
    public partial class MailActivation
    {
        public int maID { get; set; }
        public int accountID { get; set; }
        public bool isVerified { get; set; }
        public string activationCode { get; set; }
        public System.DateTime validationDate { get; set; }
        public Nullable<System.DateTime> verifiedDate { get; set; }
    
        public virtual Account Account { get; set; }
    }
}
