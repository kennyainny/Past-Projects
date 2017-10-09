using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Application.Web.Models
{
    public class District
    {
        public int Id { get; set; }
        public string DistrictCode { get; set; }
        public string DistrictName { get; set; }
        public string Address1 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string PhoneNo { get; set; }
        public string Fax { get; set; }
        public string Email { get; set; }
        public string Contact { get; set; }
        public string Manager { get; set; }
    }

    public class ReconciliationSummary
    {
        public int Id { get; set; }
        public int DistrictId { get; set; }
        public string DistrictCode { get; set; }
        public string DistrictName { get; set; }
        public double Remittance { get; set; }
        public double Schedule { get; set; }
        public double ServiceCharge { get; set; }
        public double NetSchedule { get; set; }
        public double Debit { get; set; }
        public double Credit { get; set; }
        public double Balance { get; set; }
        public string PayMonth { get; set; }
        public string PayYear { get; set; }
        public string DatePosted { get; set; }
        public string UserPosted { get; set; }
    }

    public class ExitedMinistersModel
    {
        public List<MinisterProfile> SelectedMinistersList { get; set; }
    }
    public class Getfiles
    {
        public bool chkbox { get; set; }
    }
    public class MinisterProfile
    {
        public int Id { get; set; }                
        public string MinisterCode { get; set; }
        public string Surname { get; set; }
        public string OtherName { get; set; }
        public int DistrictId { get; set; }        
        public double Salary { get; set; }
        [Column(TypeName = "datetime2")]
        public DateTime DateOfBirth { get; set; }
        public int Retired { get; set; }
        public string Both { get; set; }
        public int Stopped { get; set; }
        public string NextOfKin { get; set; }
        [Column(TypeName = "datetime2")]
        public DateTime EmployDate { get; set; }
        [Column(TypeName = "datetime2")]
        public DateTime RetiredDate { get; set; }
        [Column(TypeName = "datetime2")]
        public DateTime MergeDate { get; set; }
        [Column(TypeName = "datetime2")]
        public DateTime DateJoined { get; set; }
        public string NextRelation { get; set; }
        public string ExitReason { get; set; }
        public int nExist { get; set; }
        [Column(TypeName = "datetime2")]
        public DateTime nBatch { get; set; }
        public string nCheque { get; set; }
        public int nUserID { get; set; }
        public string PhotoString { get; set; }
        public double YearsServed { get; set; }
        public string RegNumber { get; set; }
        public string PermanentAddress { get; set; }
        //public bool IsPaidOff { get; set; }

        //public string Status { get; set; } //Exit or Active Ministers
        //public string District { get; set; }        
    }

    public class AccountRecord
    {
        public int Id { get; set; }
        public int DistrictId { get; set; }
        public int MinisterId { get; set; }
        public string District { get; set; }
        public string MinisterCode { get; set; }
        public string FullName { get; set; }        
        public string AccountNo { get; set; }
        
        public double Debit { get; set; }
        public double Minister { get; set; }
        public double Church { get; set; }
        public double Total { get; set; }
        public double NetBal { get; set; }
        public string Month { get; set; }
        public int MonthId { get; set; }
        public string Year { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime PostDate { get; set; }
    }

    public class ContributionScheduleTable
    {
        public int Id { get; set; }
        public int DistrictId { get; set; }
        public int MinisterId { get; set; }
        public double ChurchContribution { get; set; }
        public double MinisterContribution { get; set; }
        public double TotalContribution { get; set; }
        public string ScheduleMonth { get; set; }
        public string ScheduleYear { get; set; }
    }

    public class ContributionCheckListTable
    {
        public int Id { get; set; }
        public int DistrictId { get; set; }
        public int MinisterId { get; set; }
        public string DistrictName { get; set; }
        public string MinisterCode{ get; set; }
        public string MinisterName { get; set; }
        public double ChurchContribution { get; set; }
        public double MinisterContribution { get; set; }
        public double ServiceContribution { get; set; }
        public double TotalContribution { get; set; }
        public double NetContribution { get; set; }
        public string PayMonth { get; set; }
        public string PayYear { get; set; }
        //public int UserPosted { get; set; }
    }
    
    public class Month
    {
        public int Id { get; set; }
        public string MonthName { get; set; }
    }

    public class Year
    {
        public int Id { get; set; }
        public string YearValue { get; set; }
    }

    public class UserModel
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }
        public string UserRole { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime LastLogin { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime RegisterDate { get; set; }
    }

    public class History
    {
        public int Id { get; set; }
        public string NameofActor { get; set; }
        public string Role { get; set; }
        public string ActionDetail { get; set; }
        [Column(TypeName = "datetime2")]
        public DateTime TimeStamps { get; set; }
    }

}