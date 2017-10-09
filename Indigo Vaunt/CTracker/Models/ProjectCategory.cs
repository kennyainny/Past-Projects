using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace CTracker.Models
{
    public class ProjectCategory
    {
        //public ProjectCategory() { }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CategoryID { get; set; }
        [Display(Name = "Category")]
        public string CategoryName { get; set; }
        public virtual ICollection<Project> Projects { get; set; }
    }
}