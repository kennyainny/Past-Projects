using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace CTracker.Models
{
    public class ModelsDbContext : DbContext
    {
        public ModelsDbContext() : base("CertTrackerDbConection") { }
        public DbSet<ProjectCategory> ProjectCategories { get; set; }
        public DbSet<ProjectClient> ProjectClients { get; set; }
        public DbSet<ProjectOwner> ProjectOwners { get; set; }
        public DbSet<ProjectStatus> ProjectStatus { get; set; }
        public DbSet<Project> Projects { get; set; }
    }

    public class DatabaseContextInitializer : DropCreateDatabaseIfModelChanges<ModelsDbContext>
    {
        protected override void Seed(ModelsDbContext context)
        {
            ProjectCategory mainCategory = new ProjectCategory() { CategoryName = "CPV/CNS" };
            var category = new List<ProjectCategory>()
            {
                new ProjectCategory() { CategoryName = "Category A"}, 
                new ProjectCategory() { CategoryName = "Category B"}, 
                new ProjectCategory() { CategoryName = "Category C"}, 
                new ProjectCategory() { CategoryName = "Category D"} 
            };
            category.ForEach(p => context.ProjectCategories.Add(p));
            context.ProjectCategories.Add(mainCategory);
            context.SaveChanges();

            ProjectClient mainClient = new ProjectClient() { Client = "Wema Bank Nigeria Plc", IssuerCode = "WMA" };
            var client = new List<ProjectClient>()
            {
                new ProjectClient() { Client =  "Client 1", IssuerCode = "Code 1A"},
                new ProjectClient() { Client =  "Client 2", IssuerCode = "Code 2B"},
                new ProjectClient() { Client =  "Client 3", IssuerCode = "Code 3C"},
                new ProjectClient() { Client =  "Client 4", IssuerCode = "Code 4D"},
            };
            client.ForEach(p => context.ProjectClients.Add(p));
            context.ProjectClients.Add(mainClient);
            context.SaveChanges();

            ProjectOwner mainOwner = new ProjectOwner() { Owner = "Kehinde Aina", EmployeeCode = "200" };
            var owner = new List<ProjectOwner>()
            {
                new ProjectOwner() { Owner="Owner 1", EmployeeCode = "Code A1"},
                new ProjectOwner() { Owner="Owner 2", EmployeeCode = "Code B2"},
                new ProjectOwner() { Owner="Owner 3", EmployeeCode = "Code C3"},
                new ProjectOwner() { Owner="Owner 4", EmployeeCode = "Code D4"}
            };
            owner.ForEach(p => context.ProjectOwners.Add(p));
            context.ProjectOwners.Add(mainOwner);
            context.SaveChanges();

            ProjectStatus mainStatus = new ProjectStatus() { Status = "Active" };
            var status = new List<ProjectStatus>()
            {
                new ProjectStatus() { Status="Stalled"},
                new ProjectStatus() { Status="Finished"},
                new ProjectStatus() { Status="Cancel"}
            };
            status.ForEach(p => context.ProjectStatus.Add(p));
            context.ProjectStatus.Add(mainStatus);
            context.SaveChanges();

            Project mainProject = new Project()
            {
                CategoryID = mainCategory.CategoryID,
                ClientID = mainClient.ClientID,
                OwnerID = mainOwner.OwnerID,
                Description = "A test project entry",
                StartDate = DateTime.Now,
                StatusID = mainStatus.StatusID,
                SubmissionDate = DateTime.Now,
                FinishDate = DateTime.Now,
                RequirementGatheredDate = DateTime.Now,
                Remarks = "No comments"
            };
            context.Projects.Add(mainProject);
            context.SaveChanges();
        }
    }
}