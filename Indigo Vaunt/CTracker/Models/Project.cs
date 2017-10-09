using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace CTracker.Models
{
    public class Project
    {
        //public Project()
        //    {
        //ProjectCategory = new List<ProjectCategory>();
        //ProjectClient = new List<ProjectClient>();
        //ProjectOwner = new List<ProjectOwner>();
        //ProjectStatus = new List<ProjectStatus>();
        //}
        [Display(Name = "S/N")]
        public int ProjectID { get; set; }
        [Display(Name = "Descriptn")]
        public string Description { get; set; }
        public int OwnerID { get; set; }
        public int ClientID { get; set; }
        public int CategoryID { get; set; }
        public int StatusID { get; set; }
        [Display(Name = "Start Date")]
        public System.DateTime StartDate { get; set; }
        [Display(Name = "Req. Gathd.")]
        public System.DateTime RequirementGatheredDate { get; set; }
        [Display(Name = "Submissn Date")]
        public System.DateTime SubmissionDate { get; set; }
        [Display(Name = "Finish Date")]
        public System.DateTime FinishDate { get; set; }
        public string Remarks { get; set; }
        [ForeignKey("OwnerID")]
        public virtual ProjectOwner Owner { get; set; }
        [ForeignKey("ClientID")]
        public virtual ProjectClient Client { get; set; }
        [ForeignKey("CategoryID")]
        public virtual ProjectCategory Category { get; set; }
        [ForeignKey("StatusID")]
        public virtual ProjectStatus Status { get; set; }

        //public virtual ICollection<ProjectCategory> ProjectCategory { get; set; }
        //public virtual ICollection<ProjectClient> ProjectClient { get; set; }
        //public virtual ICollection<ProjectOwner> ProjectOwner { get; set; }
        //public virtual ICollection<ProjectStatus> ProjectStatus { get; set; }
    }
}