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
    
    public partial class log
    {
        public int id { get; set; }
        public Nullable<System.DateTime> date { get; set; }
        public Nullable<int> level { get; set; }
        public string host_ip { get; set; }
        public string user { get; set; }
        public string url { get; set; }
        public string user_agent { get; set; }
        public string message_title { get; set; }
        public string message_desc { get; set; }
    }
}