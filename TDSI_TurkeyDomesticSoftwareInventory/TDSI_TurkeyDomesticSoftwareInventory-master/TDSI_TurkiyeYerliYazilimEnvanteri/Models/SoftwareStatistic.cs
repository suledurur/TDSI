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
    
    public partial class SoftwareStatistic
    {
        public int softwareID { get; set; }
        public int softwareViewsTotal { get; set; }
        public int softwareViewsMonthly { get; set; }
        public int softwareViewsDaily { get; set; }
        public System.DateTime softwareViewDate { get; set; }
        public int ssID { get; set; }
    
        public virtual Software Software { get; set; }
    }
}