//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace APIServerWeb.EF
{
    using System;
    using System.Collections.Generic;
    
    public partial class Status
    {
        public Status()
        {
            this.Orders = new HashSet<Order>();
        }
    
        public int StatusID { get; set; }
        public string Name { get; set; }
    
        public virtual ICollection<Order> Orders { get; set; }
        public virtual Status Status1 { get; set; }
        public virtual Status Status2 { get; set; }
    }
}
