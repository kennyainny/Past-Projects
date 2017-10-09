using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Application.Web.Models
{

    //public class ExitedMinistersModel
    //{
    //    public List<MinisterProfile> SelectedMinistersList { get; set; }
    //}
    public class ContributionScheduleTableDTO
    {
        //public int ser { get; set; }
        public int Id { get; set; }
        public int DistrictId { get; set; }
        public int MinisterId { get; set; }
        public string Surname { get; set; }
        public string OtherName { get; set; }
        public string MinisterCode { get; set; }
        public double ChurchContribution { get; set; }
        public double MinisterContribution { get; set; }
        public double TotalContribution { get; set; }
        public string Month { get; set; }
        public string Year { get; set; }
    }

    //public class ContributionCheckListTableDTO
    //{
    //    public int ser { get; set; }
    //    public int Id { get; set; }
    //    public int DistrictId { get; set; }
    //    public int MinisterId { get; set; }
    //    public string MinisterName { get; set; }
    //    public string MinisterCode { get; set; }
    //    public double ChurchContribution { get; set; }
    //    public double MinisterContribution { get; set; }
    //    public double ServiceContribution { get; set; }
    //    public double TotalContribution { get; set; }
    //    public double NetContribution { get; set; }
    //    public string PayMonth { get; set; }
    //    public string PayYear { get; set; }
    //}

    public class MinisterNameDropDownDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}