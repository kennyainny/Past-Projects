using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace CTracker.Models
{
    public class ProjectClient
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ClientID { get; set; }
        public string Client { get; set; }
        public string IssuerCode { get; set; }

        public virtual ICollection<Project> Projects { get; set; }
    }
}