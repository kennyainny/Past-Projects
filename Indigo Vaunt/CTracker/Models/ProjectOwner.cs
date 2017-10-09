using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace CTracker.Models
{
    public class ProjectOwner
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int OwnerID { get; set; }
        public string Owner { get; set; }
        public string EmployeeCode { get; set; }
        
        public virtual ICollection<Project> Projects { get; set; }
    }
}