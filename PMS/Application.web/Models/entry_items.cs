//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Application.Web.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class entry_items
    {
        public int id { get; set; }
        public int entry_id { get; set; }
        public int ledger_id { get; set; }
        public Nullable<decimal> amount { get; set; }
        public string dc { get; set; }
        public Nullable<System.DateTime> reconciliation_date { get; set; }
    
        public virtual entry entry { get; set; }
        public virtual ledger ledger { get; set; }
    }
}
