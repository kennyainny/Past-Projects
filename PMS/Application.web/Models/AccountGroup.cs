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
    
    public partial class AccountGroup
    {
        public AccountGroup()
        {
            this.ledgers = new HashSet<ledger>();
        }
    
        public int id { get; set; }
        public int parent_id { get; set; }
        public string name { get; set; }
        public bool affects_gross { get; set; }
    
        public virtual ICollection<ledger> ledgers { get; set; }
    }
}
