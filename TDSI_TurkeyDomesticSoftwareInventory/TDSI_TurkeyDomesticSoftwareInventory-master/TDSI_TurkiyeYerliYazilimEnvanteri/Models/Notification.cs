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
    
    public partial class Notification
    {
        public int accountID { get; set; }
        public int notificationSourceID { get; set; }
        public short notificationTypeID { get; set; }
        public string notificationContent { get; set; }
        public bool isNotificationSeen { get; set; }
        public System.DateTime notificationDate { get; set; }
        public int notificationID { get; set; }
    
        public virtual Account Account { get; set; }
        public virtual Account Account1 { get; set; }
        public virtual NotificationType NotificationType { get; set; }
    }
}
