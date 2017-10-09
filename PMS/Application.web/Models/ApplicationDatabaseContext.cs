using System;
using System.Collections.Generic;
using System.Data.Entity;

namespace Application.Web.Models
{
    public class ApplicationDatabaseContext : DbContext
    {
        // You can add custom code to this file. Changes will not be overwritten.
        // 
        // If you want Entity Framework to drop and regenerate your database
        // automatically whenever you change your model schema, add the following
        // code to the Application_Start method in your Global.asax file.
        // Note: this will destroy and re-create your database with every model change.
        // 
        // System.Data.Entity.Database.SetInitializer(new System.Data.Entity.DropCreateDatabaseIfModelChanges<Application.Web.Models.ApplicationDatabaseContext>());

        public ApplicationDatabaseContext()
            : base("name=ApplicationDatabaseContext")
        {
        }

        public DbSet<Month> Months { get; set; }
        public DbSet<Year> Years { get; set; }
        public DbSet<UserModel> UserModels { get; set; }
        public DbSet<History> Historys { get; set; }
        public DbSet<District> Districts { get; set; }
        public DbSet<MinisterProfile> MinisterProfiles { get; set; }
        public DbSet<AccountRecord> AccountRecords { get; set; }
        public DbSet<ReconciliationSummary> ReconciliationSummarys { get; set; }
        public DbSet<ContributionScheduleTable> ContributionScheduleTables { get; set; }
        public DbSet<ContributionCheckListTable> ContributionCheckListTables { get; set; }

    }

    #region DatabaseContextInitializer

    public class DatabaseContextInitializer : DropCreateDatabaseIfModelChanges<ApplicationDatabaseContext>
    {
        protected override void Seed(ApplicationDatabaseContext context)
        {

            #region User Model

            var userModel = new List<UserModel>()
            {
                new UserModel() { FullName = "Full Name 1", Email = "Email1@gmail.com", UserName = "Username1", 
                    UserRole="User", LastLogin = DateTime.Now, RegisterDate = DateTime.Now},   
               new UserModel() { FullName = "Full Name 2", Email = "Email2@gmail.com", UserName = "Username2", 
                    UserRole="Admin", LastLogin = DateTime.Now, RegisterDate = DateTime.Now},  
               new UserModel() { FullName = "Full Name 3", Email = "Email3@gmail.com", UserName = "Username3", 
                    UserRole="User", LastLogin = DateTime.Now, RegisterDate = DateTime.Now},  
               new UserModel() { FullName = "Full Name 4", Email = "Email4@gmail.com", UserName = "Username4", 
                    UserRole="Admin", LastLogin = DateTime.Now, RegisterDate = DateTime.Now}
            };
            userModel.ForEach(p => context.UserModels.Add(p));
            context.SaveChanges();

            #endregion


            #region District List

            var district = new List<District>()
            {
                new District() { DistrictCode =  "77", DistrictName = "KEBBI MISSIONARY AREA", Address1 = "", City = "", State = "", PhoneNo = "", Fax = "", Email = "", Contact = "", Manager = "IBEWUIKE"}, 
                new District() { DistrictCode =  "7", DistrictName = "ONITSHA", Address1 = "", City = "", State = "", PhoneNo = "", Fax = "", Email = "", Contact = "", Manager = "ADEBAYO"}, 
                new District() { DistrictCode =  "3", DistrictName = "ABUJA", Address1 = "", City = "", State = "", PhoneNo = "", Fax = "", Email = "", Contact = "", Manager = "IBEWUIKE"}, 
                new District() { DistrictCode =  "4", DistrictName = "AFENMAI", Address1 = "", City = "", State = "", PhoneNo = "", Fax = "", Email = "", Contact = "", Manager = "ADEBAYO"}, 
                new District() { DistrictCode =  "65", DistrictName = "GENERAL COUNCIL", Address1 = "", City = "", State = "", PhoneNo = "", Fax = "", Email = "", Contact = "", Manager = "ADEBAYO"}, 
                new District() { DistrictCode =  "88", DistrictName = "IKONO", Address1 = "", City = "", State = "", PhoneNo = "", Fax = "", Email = "", Contact = "", Manager = "IBEWUIKE"}, 
                new District() { DistrictCode =  "8", DistrictName = "APAPA", Address1 = "", City = "", State = "", PhoneNo = "", Fax = "", Email = "", Contact = "", Manager = "ADEBAYO"}, 
                new District() { DistrictCode =  "69", DistrictName = "FOREIGN MISSIONARIES", Address1 = "", City = "", State = "", PhoneNo = "", Fax = "", Email = "", Contact = "", Manager = "ADEBAYO"}, 
                new District() { DistrictCode =  "14", DistrictName = "ANIOMA", Address1 = "", City = "", State = "", PhoneNo = "", Fax = "", Email = "", Contact = "", Manager = "IBEWUIKE"}, 
                new District() { DistrictCode =  "80", DistrictName = "UGHELI", Address1 = "DELTA STATE", City = "", State = "", PhoneNo = "", Fax = "", Email = "", Contact = "",  Manager = "IBEWUIKE"}, 
                new District() { DistrictCode =  "84", DistrictName = "NSUKKA", Address1 = "", City = "", State = "", PhoneNo = "", Fax = "", Email = "", Contact = "", Manager = "IBEWUIKE"}, 
                new District() { DistrictCode =  "20", DistrictName = "IKOM", Address1 = "", City = "", State = "", PhoneNo = "", Fax = "", Email = "", Contact = "", Manager = "ADEBAYO"}, 
                new District() { DistrictCode =  "118", DistrictName = "SHENDAM AREA", Address1 = "", City = "", State = "", PhoneNo = "", Fax = "", Email = "", Contact = "", Manager = "EWELIKE"}, 
                new District() { DistrictCode =  "16", DistrictName = "EDO", Address1 = "", City = "", State = "", PhoneNo = "", Fax = "", Email = "", Contact = "", Manager = "EWELIKE"}, 
                new District() { DistrictCode =  "54", DistrictName = "NEBS - YOLA", Address1 = "", City = "", State = "", PhoneNo = "", Fax = "", Email = "", Contact = "", Manager = "ADEBAYO"}, 
                new District() { DistrictCode =  "55", DistrictName = "AGBC - UYO", Address1 = "", City = "", State = "", PhoneNo = "", Fax = "", Email = "", Contact = "", Manager = "ADEBAYO"}, 
                new District() { DistrictCode =  "56", DistrictName = "NAST- EWU", Address1 = "", City = "", State = "", PhoneNo = "", Fax = "", Email = "", Contact = "", Manager = "ADEBAYO"}, 
                new District() { DistrictCode =  "28", DistrictName = "KOGI", Address1 = "", City = "", State = "", PhoneNo = "", Fax = "", Email = "", Contact = "", Manager = "ADEBAYO"}, 
                new District() { DistrictCode =  "64", DistrictName = "INT. CORRESPONDENT INSTITUTE, JOS  - ICI", Address1 = "", City = "", State = "", PhoneNo = "", Fax = "", Email = "", Contact = "", Manager = "ADEBAYO"}, 
                new District() { DistrictCode =  "82", DistrictName = "BONNY", Address1 = "", City = "", State = "", PhoneNo = "", Fax = "", Email = "", Contact = "", Manager = "IBEWUIKE"}, 
                new District() { DistrictCode =  "35", DistrictName = "OGOJA", Address1 = "", City = "", State = "", PhoneNo = "", Fax = "", Email = "", Contact = "", Manager = "IBEWUIKE"}, 
                new District() { DistrictCode =  "22", DistrictName = "IKOT EKPENE", Address1 = "", City = "", State = "", PhoneNo = "", Fax = "", Email = "", Contact = "", Manager = "IBEWUIKE"}, 
                new District() { DistrictCode =  "90", DistrictName = "KADUNA SOUTH", Address1 = "", City = "", State = "", PhoneNo = "", Fax = "", Email = "", Contact = "", Manager ="IBEWUIKE"}, 
                new District() { DistrictCode =  "111", DistrictName = "OYO EAST", Address1 = "", City = "", State = "", PhoneNo = "", Fax = "", Email = "", Contact = "", Manager = "EWELIKE"}, 
                new District() { DistrictCode =  "1", DistrictName = "ABA", Address1 = "1", City = "", State = "", PhoneNo = "", Fax = "", Email = "", Contact = "", Manager = "EWELIKE"}, 
                new District() { DistrictCode =  "91", DistrictName = "WUKARI MISSIONARY AREA", Address1 = "", City = "", State = "", PhoneNo = "", Fax = "", Email = "", Contact = "", Manager = "EWELIKE"}, 
                new District() { DistrictCode =  "61", DistrictName = "BBC IGEDE", Address1 = "", City = "", State = "", PhoneNo = "", Fax = "", Email = "", Contact = "", Manager = "ADEBAYO"}, 
                new District() { DistrictCode =  "5", DistrictName = "AFIKPO", Address1 = "", City = "", State = "", PhoneNo = "", Fax = "", Email = "", Contact = "", Manager = "IBEWUIKE"}, 
                new District() { DistrictCode =  "119", DistrictName = "KEFFI AREA", Address1 = "", City = "", State = "", PhoneNo = "", Fax = "", Email = "", Contact = "", Manager = "IBEWUIKE"}, 
                new District() { DistrictCode =  "31", DistrictName = "MAKURDI", Address1 = "", City = "", State = "", PhoneNo = "", Fax = "", Email = "", Contact = "", Manager = "EWELIKE"}, 
                new District() { DistrictCode =  "52", DistrictName = "IKWERRE", Address1 = "", City = "", State = "", PhoneNo = "", Fax = "", Email = "", Contact = "", Manager = "ADEBAYO"}, 
                new District() { DistrictCode =  "38", DistrictName = "ONDO", Address1 = "", City = "", State = "", PhoneNo = "", Fax = "", Email = "", Contact = "", Manager = "IBEWUIKE"}, 
                new District() { DistrictCode =  "74", DistrictName = "OYO NORTH", Address1 = "OYO", City = "", State = "", PhoneNo = "", Fax = "", Email = "", Contact = "", Manager = "IBEWUIKE"}, 
                new District() { DistrictCode =  "44", DistrictName = "PANKSHIN", Address1 = "", City = "", State = "", PhoneNo = "", Fax = "", Email = "", Contact = "", Manager = "IBEWUIKE"}, 
                new District() { DistrictCode =  "6", DistrictName = "ALI- UDO", Address1 = "", City = "", State = "", PhoneNo = "", Fax = "", Email = "", Contact = "", Manager = "IBEWUIKE"}, 
                new District() { DistrictCode =  "43", DistrictName = "OYO", Address1 = "", City = "", State = "", PhoneNo = "", Fax = "", Email = "", Contact = "", Manager = "EWELIKE"}, 
                new District() { DistrictCode =  "53", DistrictName = "YOLA", Address1 = "", City = "", State = "", PhoneNo = "", Fax = "", Email = "", Contact = "", Manager = "IBEWUIKE"}, 
                new District() { DistrictCode =  "59", DistrictName = "TSNN, SAMINAKA", Address1 = "", City = "", State = "", PhoneNo = "", Fax = "", Email = "", Contact = "", Manager = "ADEBAYO"}, 
                new District() { DistrictCode =  "27", DistrictName = "KANO", Address1 = "", City = "", State = "", PhoneNo = "", Fax = "", Email = "", Contact = "", Manager = "EWELIKE"}, 
                new District() { DistrictCode =  "93", DistrictName = "BALI MISSIONARY AREA", Address1 = "", City = "", State = "", PhoneNo = "", Fax = "", Email = "", Contact = "", Manager = "EWELIKE"}, 
                new District() { DistrictCode =  "68", DistrictName = "GOMBE", Address1 = "", City = "", State = "", PhoneNo = "", Fax = "", Email = "", Contact = "", Manager = "EWELIKE"}, 
                new District() { DistrictCode =  "11", DistrictName = "BORNO", Address1 = "", City = "", State = "", PhoneNo = "", Fax = "", Email = "", Contact = "", Manager = "ADEBAYO  "}, 
                new District() { DistrictCode =  "967", DistrictName = "GENERAL LEDGER", Address1 = "", City = "", State = "", PhoneNo = "", Fax = "", Email = "", Contact = "", Manager = "NULL"}, 
                new District() { DistrictCode =  "18", DistrictName = "ENUGU", Address1 = "", City = "", State = "", PhoneNo = "", Fax = "", Email = "", Contact = "", Manager = "ADEBAYO"}, 
                new District() { DistrictCode =  "58", DistrictName = "SAST - IPERU", Address1 = "", City = "", State = "", PhoneNo = "", Fax = "", Email = "", Contact = "", Manager = "ADEBAYO"}, 
                new District() { DistrictCode =  "10", DistrictName = "BAYELSA", Address1 = "", City = "", State = "", PhoneNo = "", Fax = "", Email = "", Contact = "", Manager = "EWELIKE"}, 
                new District() { DistrictCode =  "95", DistrictName = "UBA MISSIONARY AREA", Address1 = "", City = "", State = "", PhoneNo = "", Fax = "", Email = "", Contact = "", Manager = "IBEWUIKE"}, 
                new District() { DistrictCode =  "12", DistrictName = "CALABAR", Address1 = "", City = "", State = "", PhoneNo = "", Fax = "", Email = "", Contact = "", Manager = "IBEWUIKE"}, 
                new District() { DistrictCode =  "999", DistrictName = "DORMANT DISTRICT", Address1 = "NULL", City = "NULL", State = "NULL", PhoneNo = "NULL", Fax = "NULL", Email = "NULL", Contact = "NULL", Manager = "NULL"}, 
                new District() { DistrictCode =  "15", DistrictName = "EASTERN RIVERS", Address1 = "", City = "", State = "", PhoneNo = "", Fax = "", Email = "", Contact = "", Manager = "ADEBAYO"}, 
                new District() { DistrictCode =  "120", DistrictName = "NDOKWA", Address1 = "", City = "", State = "", PhoneNo = "", Fax = "", Email = "", Contact = "", Manager = "IBEWUIKE"}, 
                new District() { DistrictCode =  "19", DistrictName = "ESAN", Address1 = "", City = "", State = "", PhoneNo = "", Fax = "", Email = "", Contact = "", Manager = "IBEWUIKE"}, 
                new District() { DistrictCode =  "122", DistrictName = "ETCHE", Address1 = "", City = "", State = "", PhoneNo = "", Fax = "", Email = "", Contact = "", Manager = "NULL"}, 
                new District() { DistrictCode =  "25", DistrictName = "KADUNA", Address1 = "", City = "", State = "", PhoneNo = "", Fax = "", Email = "", Contact = "", Manager = "IBEWUIKE"}, 
                new District() { DistrictCode =  "103", DistrictName = "ABA NORTH", Address1 = "", City = "", State = "", PhoneNo = "", Fax = "", Email = "", Contact = "", Manager = "EWELIKE"}, 
                new District() { DistrictCode =  "9", DistrictName = "BAUCHI", Address1 = "", City = "", State = "", PhoneNo = "", Fax = "", Email = "", Contact = "", Manager = "IBEWUIKE"}, 
                new District() { DistrictCode =  "29", DistrictName = "LAFIA", Address1 = "", City = "", State = "", PhoneNo = "", Fax = "", Email = "", Contact = "", Manager = "ADEBAYO"}, 
                new District() { DistrictCode =  "30", DistrictName = "LAGOS", Address1 = "", City = "", State = "", PhoneNo = "", Fax = "", Email = "", Contact = "", Manager = "ADEBAYO"}, 
                new District() { DistrictCode =  "45", DistrictName = "RIVERS", Address1 = "", City = "", State = "", PhoneNo = "", Fax = "", Email = "", Contact = "", Manager = "ADEBAYO"}, 
                new District() { DistrictCode =  "32", DistrictName = "NGWA", Address1 = "", City = "", State = "", PhoneNo = "", Fax = "", Email = "", Contact = "", Manager = "IBEWUIKE"},
                new District() { DistrictCode =  "17", DistrictName = "EKET", Address1 = "", City = "", State = "", PhoneNo = "", Fax = "", Email = "", Contact = "", Manager = "ADEBAYO"}, 
                new District() { DistrictCode =  "33", DistrictName = "NIGER", Address1 = "", City = "", State = "", PhoneNo = "", Fax = "", Email = "", Contact = "", Manager = "IBEWUIKE"}, 
                new District() { DistrictCode =  "34", DistrictName = "MBAISE", Address1 = "", City = "", State = "", PhoneNo = "", Fax = "", Email = "", Contact = "", Manager = "ADEBAYO"}, 
                new District() { DistrictCode =  "62", DistrictName = "PTS-PH", Address1 = "", City = "", State = "", PhoneNo = "", Fax = "", Email = "", Contact = "", Manager = "ADEBAYO"}, 
                new District() { DistrictCode =  "39", DistrictName = "ORLU", Address1 = "", City = "", State = "", PhoneNo = "", Fax = "", Email = "", Contact = "", Manager = "IBEWUIKE"}, 
                new District() { DistrictCode =  "40", DistrictName = "OSUN", Address1 = "", City = "", State = "", PhoneNo = "", Fax = "", Email = "", Contact = "", Manager = "EWELIKE"}, 
                new District() { DistrictCode =  "49", DistrictName = "UKWA", Address1 = "", City = "", State = "", PhoneNo = "", Fax = "", Email = "", Contact = "", Manager = "EWELIKE"}, 
                new District() { DistrictCode =  "96", DistrictName = "ORON", Address1 = "", City = "", State = "", PhoneNo = "", Fax = "", Email = "", Contact = "", Manager = "IBEWUIKE"}, 
                new District() { DistrictCode =  "46", DistrictName = "SAMINAKA", Address1 = "", City = "", State = "", PhoneNo = "", Fax = "", Email = "", Contact = "", Manager = "IBEWUIKE"}, 
                new District() { DistrictCode =  "47", DistrictName = "SOKOTO", Address1 = "", City = "", State = "", PhoneNo = "", Fax = "", Email = "", Contact = "", Manager = "ADEBAYO"}, 
                new District() { DistrictCode =  "57", DistrictName = "ETS JOS", Address1 = "", City = "", State = "", PhoneNo = "", Fax = "", Email = "", Contact = "", Manager = "ADEBAYO"}, 
                new District() { DistrictCode =  "48", DistrictName = "TARABA", Address1 = "", City = "", State = "", PhoneNo = "", Fax = "", Email = "", Contact = "", Manager = "IBEWUIKE"}, 
                new District() { DistrictCode =  "60", DistrictName = "AGS - OGOJA", Address1 = "", City = "", State = "", PhoneNo = "", Fax = "", Email = "", Contact = "", Manager = "ADEBAYO"}, 
                new District() { DistrictCode =  "67", DistrictName = "IGEDE", Address1 = "", City = "", State = "", PhoneNo = "", Fax = "", Email = "", Contact = "", Manager = "EWELIKE"}, 
                new District() { DistrictCode =  "63", DistrictName = "AGDSN, UMUAHIA", Address1 = "", City = "", State = "", PhoneNo = "", Fax = "", Email = "", Contact = "", Manager = "ADEBAYO"}, 
                new District() { DistrictCode =  "73", DistrictName = "GBOKO", Address1 = "", City = "", State = "", PhoneNo = "", Fax = "", Email = "", Contact = "", Manager = "EWELIKE"}, 
                new District() { DistrictCode =  "21", DistrictName = "IKORODU", Address1 = "", City = "", State = "", PhoneNo = "", Fax = "", Email = "", Contact = "", Manager = "EWELIKE"}, 
                new District() { DistrictCode =  "24", DistrictName = "JOS", Address1 = "", City = "", State = "", PhoneNo = "", Fax = "", Email = "", Contact = "", Manager = "EWELIKE"}, 
                new District() { DistrictCode =  "78", DistrictName = "ZAMFARA", Address1 = "", City = "", State = "", PhoneNo = "", Fax = "", Email = "", Contact = "", Manager = "ADEBAYO"}, 
                new District() { DistrictCode =  "72", DistrictName = "AHOADA", Address1 = "", City = "", State = "", PhoneNo = "", Fax = "", Email = "", Contact = "", Manager = "IBEWUIKE"}, 
                new District() { DistrictCode =  "13", DistrictName = "WARRI", Address1 = "", City = "", State = "", PhoneNo = "", Fax = "", Email = "", Contact = "", Manager = "EWELIKE"}, 
                new District() { DistrictCode =  "50", DistrictName = "UMUAHIA", Address1 = "", City = "", State = "", PhoneNo = "", Fax = "", Email = "", Contact = "", Manager = "IBEWUIKE"}, 
                new District() { DistrictCode =  "79", DistrictName = "SAPELE", Address1 = "DELTA STATE", City = "", State = "", PhoneNo = "", Fax = "", Email = "", Contact = "", Manager = "IBEWUIKE"}, 
                new District() { DistrictCode =  "37", DistrictName = "OKIGWE", Address1 = "", City = "", State = "", PhoneNo = "", Fax = "", Email = "", Contact = "", Manager = "IBEWUIKE"}, 
                new District() { DistrictCode =  "51", DistrictName = "UYO", Address1 = "", City = "", State = "", PhoneNo = "", Fax = "", Email = "", Contact = "", Manager = "EWELIKE"}, 
                new District() { DistrictCode =  "81", DistrictName = "AGMBS", Address1 = "PLOT 108 ISHERI RD OJODU", City = "BERGER", State = "LAGOS", PhoneNo = "01-4715138", Fax = "", Email = "", Contact = "", Manager = "ADEBAYO"}, 
                new District() { DistrictCode =  "83", DistrictName = "EDO EAST", Address1 = "", City = "", State = "", PhoneNo = "", Fax = "", Email = "", Contact = "", Manager = "ADEBAYO"}, 
                new District() { DistrictCode =  "85", DistrictName = "AKAMKPA", Address1 = "", City = "", State = "", PhoneNo = "", Fax = "", Email = "", Contact = "", Manager = "IBEWUIKE"}, 
                new District() { DistrictCode =  "26", DistrictName = "KAFANCHAN", Address1 = "", City = "", State = "", PhoneNo = "", Fax = "", Email = "", Contact = "", Manager = "EWELIKE"}, 
                new District() { DistrictCode =  "86", DistrictName = "NNEWI", Address1 = "", City = "", State = "", PhoneNo = "", Fax = "", Email = "", Contact = "", Manager = "ADEBAYO"}, 
                new District() { DistrictCode =  "108", DistrictName = "UYO EAST", Address1 = "", City = "", State = "", PhoneNo = "", Fax = "", Email = "", Contact = "", Manager = "EWELIKE"}, 
                new District() { DistrictCode =  "41", DistrictName = "OTURKPO", Address1 = "", City = "", State = "", PhoneNo = "", Fax = "", Email = "", Contact = "", Manager = "EWELIKE"}, 
                new District() { DistrictCode =  "92", DistrictName = "JALINGO MISSIONARY AREA", Address1 = "", City = "", State = "", PhoneNo = "", Fax = "", Email = "", Contact = "", Manager = "EWELIKE"}, 
                new District() { DistrictCode =  "94", DistrictName = "YOBE MISSIONARY AREA", Address1 = "", City = "", State = "", PhoneNo = "", Fax = "", Email = "", Contact = "", Manager = "ADEBAYO"}, 
                new District() { DistrictCode =  "70", DistrictName = "HOME  MISSIONARIES", Address1 = "", City = "", State = "", PhoneNo = "", Fax = "", Email = "", Contact = "", Manager = "ADEBAYO"}, 
                new District() { DistrictCode =  "76", DistrictName = "EKITI", Address1 = "", City = "", State = "", PhoneNo = "", Fax = "", Email = "", Contact = "", Manager = "IBEWUIKE"}, 
                new District() { DistrictCode =  "87", DistrictName = "AWKA", Address1 = "", City = "", State = "", PhoneNo = "", Fax = "", Email = "", Contact = "", Manager = "ADEBAYO"}, 
                new District() { DistrictCode =  "42", DistrictName = "OWERRI", Address1 = "", City = "", State = "", PhoneNo = "", Fax = "", Email = "", Contact = "", Manager = "IBEWUIKE"}, 
                new District() { DistrictCode =  "99", DistrictName = "EBONYI", Address1 = "", City = "", State = "", PhoneNo = "", Fax = "", Email = "", Contact = "", Manager = "ADEBAYO"}, 
                new District() { DistrictCode =  "100", DistrictName = "OBUDU", Address1 = "", City = "", State = "", PhoneNo = "", Fax = "", Email = "", Contact = "", Manager = "IBEWUIKE"}, 
                new District() { DistrictCode =  "66", DistrictName = "NATIONAL PIONEER PASTORS", Address1 = "", City = "", State = "", PhoneNo = "", Fax = "", Email = "", Contact = "", Manager = "ADEBAYO"}, 
                new District() { DistrictCode =  "102", DistrictName = "OKIGWE SOUTH", Address1 = "", City = "", State = "", PhoneNo = "", Fax = "", Email = "", Contact = "", Manager = "IBEWUIKE"}, 
                new District() { DistrictCode =  "89", DistrictName = "OBUBRA ", Address1 = "", City = "", State = "", PhoneNo = "", Fax = "", Email = "", Contact = "", Manager = "IBEWUIKE"}, 
                new District() { DistrictCode =  "75", DistrictName = "ELEME TAI", Address1 = "", City = "", State = "", PhoneNo = "", Fax = "", Email = "", Contact = "", Manager = "IBEWUIKE"}, 
                new District() { DistrictCode =  "104", DistrictName = "ABEOKUTA", Address1 = "", City = "", State = "", PhoneNo = "", Fax = "", Email = "", Contact = "", Manager = "ADEBAYO"}, 
                new District() { DistrictCode =  "105", DistrictName = "SANGO", Address1 = "", City = "", State = "", PhoneNo = "", Fax = "", Email = "", Contact = "", Manager = "ADEBAYO"}, 
                new District() { DistrictCode =  "106", DistrictName = "LAGOS MAINLAND", Address1 = "", City = "", State = "", PhoneNo = "", Fax = "", Email = "", Contact = "", Manager = "ADEBAYO"}, 
                new District() { DistrictCode =  "107", DistrictName = "IJEBU", Address1 = "", City = "", State = "", PhoneNo = "", Fax = "", Email = "", Contact = "", Manager = "ADEBAYO"}, 
                new District() { DistrictCode =  "97", DistrictName = "ABUJA EAST", Address1 = "", City = "", State = "", PhoneNo = "", Fax = "", Email = "", Contact = "", Manager = "IBEWUIKE"}, 
                new District() { DistrictCode =  "113", DistrictName = "OWAN  AREA", Address1 = "", City = "", State = "", PhoneNo = "", Fax = "", Email = "", Contact = "", Manager = "EWELIKE"}, 
                new District() { DistrictCode =  "114", DistrictName = "ENUGU SOUTH", Address1 = "", City = "", State = "", PhoneNo = "", Fax = "", Email = "", Contact = "", Manager = "ADEBAYO"}, 
                new District() { DistrictCode =  "109", DistrictName = "UKANAFUN AREA", Address1 = "", City = "", State = "", PhoneNo = "", Fax = "", Email = "", Contact = "", Manager = "EWELIKE"}, 
                new District() { DistrictCode =  "110", DistrictName = "IKWERRE SOUTH", Address1 = "", City = "", State = "", PhoneNo = "", Fax = "", Email = "", Contact = "", Manager = "ADEBAYO"}, 
                new District() { DistrictCode =  "23", DistrictName = "KWARA", Address1 = "", City = "", State = "", PhoneNo = "", Fax = "", Email = "", Contact = "", Manager = "ADEBAYO"}, 
                new District() { DistrictCode =  "115", DistrictName = "GWOL", Address1 = "", City = "", State = "", PhoneNo = "", Fax = "", Email = "", Contact = "", Manager = "EWELIKE"}, 
                new District() { DistrictCode =  "112", DistrictName = "BADAGRY", Address1 = "", City = "", State = "", PhoneNo = "", Fax = "", Email = "", Contact = "", Manager = "EWELIKE"}, 
                new District() { DistrictCode =  "116", DistrictName = "JOS MAINLAND", Address1 = "", City = "", State = "", PhoneNo = "", Fax = "", Email = "", Contact = "", Manager = "EWELIKE"}, 
                new District() { DistrictCode =  "117", DistrictName = "MANGU", Address1 = "", City = "", State = "", PhoneNo = "", Fax = "", Email = "", Contact = "", Manager = "IBEWUIKE"}, 
                new District() { DistrictCode =  "121", DistrictName = "ESAN EAST", Address1 = "", City = "", State = "", PhoneNo = "", Fax = "", Email = "", Contact = "", Manager = "IBEWUIKE"}, 
                new District() { DistrictCode =  "2", DistrictName = "ABAKALIKI", Address1 = "", City = "", State = "", PhoneNo = "", Fax = "", Email = "", Contact = "", Manager = "EWELIKE"}, 
                new District() { DistrictCode =  "98", DistrictName = "IKWO", Address1 = "", City = "", State = "", PhoneNo = "", Fax = "", Email = "", Contact = "", Manager = "EWELIKE"}, 
                new District() { DistrictCode =  "71", DistrictName = "KATSINA", Address1 = "", City = "", State = "", PhoneNo = "", Fax = "", Email = "", Contact = "", Manager = "EWELIKE"}, 
                new District() { DistrictCode =  "101", DistrictName = "OWERRI EAST", Address1 = "", City = "", State = "", PhoneNo = "", Fax = "", Email = "", Contact = "", Manager = "IBEWUIKE"}
                            };

            district.ForEach(p => context.Districts.Add(p));
            context.SaveChanges();

            #endregion


            #region Contribution Schedule

            //        //var contributionSched = new List<ContributionScheduleTable>()
            //        //{
            //        //    new ContributionScheduleTable() { DistrictId = 1, MinisterId = 1, ChurchContribution = 87.23,
            //        //        MinisterContribution = 43, TotalContribution = 2001, ScheduleMonth = "June", ScheduleYear = "2009"},
            //        //    new ContributionScheduleTable() { DistrictId = 1, MinisterId = 2, ChurchContribution = 817.23,
            //        //        MinisterContribution = 44, TotalContribution = 422, ScheduleMonth = "June", ScheduleYear = "2009"},
            //        //    new ContributionScheduleTable() { DistrictId = 1, MinisterId = 3, ChurchContribution = 33.3,
            //        //        MinisterContribution = 32, TotalContribution = 311, ScheduleMonth = "June", ScheduleYear = "2009"},
            //        //    new ContributionScheduleTable() { DistrictId = 1, MinisterId = 4, ChurchContribution = 77,
            //        //        MinisterContribution = 79, TotalContribution = 544, ScheduleMonth = "June", ScheduleYear = "2009"},
            //        //    new ContributionScheduleTable() { DistrictId = 1, MinisterId = 5, ChurchContribution = 39,
            //        //        MinisterContribution = 21, TotalContribution = 342, ScheduleMonth = "June", ScheduleYear = "2009"},
            //        //    new ContributionScheduleTable() { DistrictId = 1, MinisterId = 6, ChurchContribution = 30,
            //        //        MinisterContribution = 10, TotalContribution = 80, ScheduleMonth = "June", ScheduleYear = "2009"},
            //        //    new ContributionScheduleTable() { DistrictId = 1, MinisterId = 7, ChurchContribution = 65,
            //        //        MinisterContribution = 32, TotalContribution = 98, ScheduleMonth = "June", ScheduleYear = "2009"},
            //        //    new ContributionScheduleTable() { DistrictId = 1, MinisterId = 8, ChurchContribution = 79.9,
            //        //        MinisterContribution = 76, TotalContribution = 189, ScheduleMonth = "June", ScheduleYear = "2009"},
            //        //    new ContributionScheduleTable() { DistrictId = 1, MinisterId = 9, ChurchContribution = 27,
            //        //        MinisterContribution = 9, TotalContribution = 49, ScheduleMonth = "June", ScheduleYear = "2009"},
            //        //    new ContributionScheduleTable() { DistrictId = 1, MinisterId = 10, ChurchContribution = 11.23,
            //        //        MinisterContribution = 22, TotalContribution = 322, ScheduleMonth = "June", ScheduleYear = "2009"},
            //        //    new ContributionScheduleTable() { DistrictId = 1, MinisterId = 11, ChurchContribution = 81.03,
            //        //        MinisterContribution = 92, TotalContribution = 522, ScheduleMonth = "June", ScheduleYear = "2009"},
            //        //    new ContributionScheduleTable() { DistrictId = 1, MinisterId = 12, ChurchContribution = 51.71,
            //        //        MinisterContribution = 65, TotalContribution = 392, ScheduleMonth = "June", ScheduleYear = "2009"},
            //        //    new ContributionScheduleTable() { DistrictId = 2, MinisterId = 13, ChurchContribution = 87.23,
            //        //        MinisterContribution = 43, TotalContribution = 2001, ScheduleMonth = "June", ScheduleYear = "2009"},
            //        //    new ContributionScheduleTable() { DistrictId = 2, MinisterId = 14, ChurchContribution = 817.23,
            //        //        MinisterContribution = 44, TotalContribution = 422, ScheduleMonth = "June", ScheduleYear = "2009"},
            //        //    new ContributionScheduleTable() { DistrictId = 2, MinisterId = 15, ChurchContribution = 33.3,
            //        //        MinisterContribution = 32, TotalContribution = 311, ScheduleMonth = "June", ScheduleYear = "2009"},
            //        //    new ContributionScheduleTable() { DistrictId = 2, MinisterId = 16, ChurchContribution = 77,
            //        //        MinisterContribution = 79, TotalContribution = 544, ScheduleMonth = "June", ScheduleYear = "2009"},
            //        //    new ContributionScheduleTable() { DistrictId = 2, MinisterId = 17, ChurchContribution = 39,
            //        //        MinisterContribution = 21, TotalContribution = 342, ScheduleMonth = "June", ScheduleYear = "2009"},
            //        //    new ContributionScheduleTable() { DistrictId = 2, MinisterId = 18, ChurchContribution = 30,
            //        //        MinisterContribution = 10, TotalContribution = 80, ScheduleMonth = "June", ScheduleYear = "2009"},
            //        //    new ContributionScheduleTable() { DistrictId = 2, MinisterId = 19, ChurchContribution = 65,
            //        //        MinisterContribution = 32, TotalContribution = 98, ScheduleMonth = "June", ScheduleYear = "2009"},
            //        //    new ContributionScheduleTable() { DistrictId = 2, MinisterId = 20, ChurchContribution = 79.9,
            //        //        MinisterContribution = 76, TotalContribution = 189, ScheduleMonth = "June", ScheduleYear = "2009"},
            //        //    new ContributionScheduleTable() { DistrictId = 2, MinisterId = 21, ChurchContribution = 27,
            //        //        MinisterContribution = 9, TotalContribution = 49, ScheduleMonth = "June", ScheduleYear = "2009"},
            //        //    new ContributionScheduleTable() { DistrictId = 2, MinisterId = 22, ChurchContribution = 11.23,
            //        //        MinisterContribution = 22, TotalContribution = 322, ScheduleMonth = "June", ScheduleYear = "2009"},
            //        //    new ContributionScheduleTable() { DistrictId = 2, MinisterId = 23, ChurchContribution = 81.03,
            //        //        MinisterContribution = 92, TotalContribution = 522, ScheduleMonth = "June", ScheduleYear = "2009"},
            //        //    new ContributionScheduleTable() { DistrictId = 2, MinisterId = 24, ChurchContribution = 51.71,
            //        //        MinisterContribution = 65, TotalContribution = 392, ScheduleMonth = "June", ScheduleYear = "2009"}
            //        //};
            //        //contributionSched.ForEach(p => context.ContributionScheduleTables.Add(p));
            //        //context.SaveChanges();

            #endregion


            #region Year

            var year = new List<Year>()
            {
                new Year() { YearValue = "2003" }, new Year() { YearValue = "2004" },
                new Year() { YearValue = "2005" }, new Year() { YearValue = "2006" },
                new Year() { YearValue = "2007" }, new Year() { YearValue = "2008" },
                new Year() { YearValue = "2009" }, new Year() { YearValue = "2010" },
                new Year() { YearValue = "2011" }, new Year() { YearValue = "2012" },
                new Year() { YearValue = "2013" }, new Year() { YearValue = "2014" }
            };
            year.ForEach(p => context.Years.Add(p));
            context.SaveChanges();

            #endregion

            #region Month

            var month = new List<Month>()
            {
                new Month() { MonthName = "January"}, new Month() { MonthName = "February"},
                new Month() { MonthName = "March"}, new Month() { MonthName = "April"},
                new Month() { MonthName = "May"}, new Month() { MonthName = "June"},
                new Month() { MonthName = "July"}, new Month() { MonthName = "August"},
                new Month() { MonthName = "September"}, new Month() { MonthName = "October"},
                new Month() { MonthName = "November"}, new Month() { MonthName = "December"}
            };
            month.ForEach(p => context.Months.Add(p));
            context.SaveChanges();

            #endregion


            #region Ministers

            //        //var ministerprofile = new List<MinisterProfile>()
            //        //{
            //        //    new MinisterProfile() {  DistrictId = 1, DateJoined = DateTime.Now, EmployDate = DateTime.Today, Status="Active",
            //        //         Surname = "Minister 1", OtherName= "Minister Othername 1", MinisterCode="76451", District="Abuja District"},
            //        //    new MinisterProfile() { DistrictId = 1, DateJoined = DateTime.Now, EmployDate = DateTime.Today, Status="Active",
            //        //         Surname = "Minister 2", OtherName= "Minister Othername 2", MinisterCode="76452", District="Abuja District"},
            //        //    new MinisterProfile() { DistrictId = 1, DateJoined = DateTime.Now, EmployDate = DateTime.Today, Status="Active",
            //        //         Surname = "Minister 3", OtherName= "Minister Othername 3", MinisterCode="76453", District="Abuja District"},
            //        //    new MinisterProfile() { DistrictId = 1, DateJoined = DateTime.Now, EmployDate = DateTime.Today, Status="Active",
            //        //         Surname = "Minister 4", OtherName= "Minister Othername 4", MinisterCode="76454", District="Abuja District"},
            //        //    new MinisterProfile() {  DistrictId = 1, DateJoined = DateTime.Now, EmployDate = DateTime.Today, Status="Active",
            //        //         Surname = "Minister 5", OtherName= "Minister Othername 5", MinisterCode="76451", District="Abuja District"},
            //        //    new MinisterProfile() { DistrictId = 1, DateJoined = DateTime.Now, EmployDate = DateTime.Today, Status="Active",
            //        //         Surname = "Minister 6", OtherName= "Minister Othername 6", MinisterCode="76452", District="Abuja District"},
            //        //    new MinisterProfile() { DistrictId = 1, DateJoined = DateTime.Now, EmployDate = DateTime.Today, Status="Active",
            //        //         Surname = "Minister 7", OtherName= "Minister Othername 7", MinisterCode="76453", District="Abuja District"},
            //        //    new MinisterProfile() { DistrictId = 1, DateJoined = DateTime.Now, EmployDate = DateTime.Today, Status="Active",
            //        //         Surname = "Minister 8", OtherName= "Minister Othername 8", MinisterCode="76454", District="Abuja District"},
            //        //    new MinisterProfile() {  DistrictId = 1, DateJoined = DateTime.Now, EmployDate = DateTime.Today, Status="Active",
            //        //         Surname = "Minister 9", OtherName= "Minister Othername 9", MinisterCode="76451", District="Abuja District"},
            //        //    new MinisterProfile() { DistrictId = 1, DateJoined = DateTime.Now, EmployDate = DateTime.Today, Status="Active",
            //        //         Surname = "Minister 10", OtherName= "Minister Othername 10", MinisterCode="76452", District="Abuja District"},
            //        //    new MinisterProfile() { DistrictId = 1, DateJoined = DateTime.Now, EmployDate = DateTime.Today, Status="Active",
            //        //         Surname = "Minister 11", OtherName= "Minister Othername 11", MinisterCode="76453", District="Abuja District"},
            //        //    new MinisterProfile() { DistrictId = 1, DateJoined = DateTime.Now, EmployDate = DateTime.Today, Status="Active",
            //        //         Surname = "Minister 12", OtherName= "Minister Othername 12", MinisterCode="76454", District="Abuja District"},
            //        //    new MinisterProfile() {  DistrictId = 2, DateJoined = DateTime.Now, EmployDate = DateTime.Today, Status="Active",
            //        //         Surname = "Minister 1", OtherName= "Minister Othername 13", MinisterCode="76451", District="Adamawa District"},
            //        //    new MinisterProfile() { DistrictId = 2, DateJoined = DateTime.Now, EmployDate = DateTime.Today, Status="Active",
            //        //         Surname = "Minister 2", OtherName= "Minister Othername 14", MinisterCode="76452", District="Adamawa District"},
            //        //    new MinisterProfile() { DistrictId = 2, DateJoined = DateTime.Now, EmployDate = DateTime.Today, Status="Active",
            //        //         Surname = "Minister 3", OtherName= "Minister Othername 15", MinisterCode="76453", District="Adamawa District"},
            //        //    new MinisterProfile() { DistrictId = 2, DateJoined = DateTime.Now, EmployDate = DateTime.Today, Status="Active",
            //        //         Surname = "Minister 4", OtherName= "Minister Othername 16", MinisterCode="76454", District="Adamawa District"},
            //        //    new MinisterProfile() {  DistrictId = 2, DateJoined = DateTime.Now, EmployDate = DateTime.Today, Status="Active",
            //        //         Surname = "Minister 5", OtherName= "Minister Othername 17", MinisterCode="76451", District="Adamawa District"},
            //        //    new MinisterProfile() { DistrictId = 2, DateJoined = DateTime.Now, EmployDate = DateTime.Today, Status="Active",
            //        //         Surname = "Minister 6", OtherName= "Minister Othername 18", MinisterCode="76452", District="Adamawa District"},
            //        //    new MinisterProfile() { DistrictId = 2, DateJoined = DateTime.Now, EmployDate = DateTime.Today, Status="Active",
            //        //         Surname = "Minister 7", OtherName= "Minister Othername 19", MinisterCode="76453", District="Adamawa District"},
            //        //    new MinisterProfile() { DistrictId = 2, DateJoined = DateTime.Now, EmployDate = DateTime.Today, Status="Active",
            //        //         Surname = "Minister 8", OtherName= "Minister Othername 20", MinisterCode="76454", District="Adamawa District"},
            //        //    new MinisterProfile() {  DistrictId = 2, DateJoined = DateTime.Now, EmployDate = DateTime.Today, Status="Active",
            //        //         Surname = "Minister 9", OtherName= "Minister Othername 21", MinisterCode="76451", District="Adamawa District"},
            //        //    new MinisterProfile() { DistrictId = 2, DateJoined = DateTime.Now, EmployDate = DateTime.Today, Status="Active",
            //        //         Surname = "Minister 10", OtherName= "Minister Othername 22", MinisterCode="76452", District="Adamawa District"},
            //        //    new MinisterProfile() { DistrictId = 2, DateJoined = DateTime.Now, EmployDate = DateTime.Today, Status="Active",
            //        //         Surname = "Minister 11", OtherName= "Minister Othername 23", MinisterCode="76453", District="Adamawa District"},
            //        //    new MinisterProfile() { DistrictId = 2, DateJoined = DateTime.Now, EmployDate = DateTime.Today, Status="Active",
            //        //         Surname = "Minister 12", OtherName= "Minister Othername 24", MinisterCode="76454", District="Adamawa District"}
            //        //};
            //        //ministerprofile.ForEach(p => context.MinisterProfiles.Add(p));
            //        //context.SaveChanges();

            #endregion


            #region Checklist

            //        //var contributionchecklist = new List<ContributionCheckListTable>()
            //        //{
            //        //    new ContributionCheckListTable() { DistrictName="Abuja District", DistrictId = 1, MinisterId = 1, ChurchContribution = 87.23,
            //        //        MinisterContribution = 43, ServiceContribution = 21, NetContribution = 90, MinisterCode = "541",
            //        //         MinisterName = "Minister Name 1", ser=1, TotalContribution = 2001, PayMonth = "May", PayYear  = "2014"},
            //        //    new ContributionCheckListTable() { DistrictName="Abuja District", DistrictId = 1, MinisterId = 2, ChurchContribution = 87.23,
            //        //        MinisterContribution = 43, ServiceContribution = 21, NetContribution = 90,  MinisterCode = "542",
            //        //         MinisterName = "Minister Name 2", ser=2, TotalContribution = 2001, PayMonth = "May", PayYear  = "2014"},
            //        //    new ContributionCheckListTable() { DistrictName="Abuja District", DistrictId = 1, MinisterId = 3, ChurchContribution = 87.23,
            //        //        MinisterContribution = 43, ServiceContribution = 21, NetContribution = 90,  MinisterCode = "543",
            //        //         MinisterName = "Minister Name 3", ser=3, TotalContribution = 2001, PayMonth = "May", PayYear  = "2014"},
            //        //    new ContributionCheckListTable() { DistrictName="Abuja District", DistrictId = 1, MinisterId = 4, ChurchContribution = 87.23,
            //        //        MinisterContribution = 43, ServiceContribution = 21, NetContribution = 90,  MinisterCode = "544",
            //        //         MinisterName = "Minister Name 4", ser=4, TotalContribution = 2001, PayMonth = "May", PayYear  = "2014"},
            //        //    new ContributionCheckListTable() { DistrictName="Abuja District", DistrictId = 1, MinisterId = 5, ChurchContribution = 87.23,
            //        //        MinisterContribution = 43, ServiceContribution = 21, NetContribution = 90,  MinisterCode = "545",
            //        //         MinisterName = "Minister Name 5", ser=5, TotalContribution = 2001, PayMonth = "May", PayYear  = "2014"},
            //        //    new ContributionCheckListTable() { DistrictName="Abuja District", DistrictId = 1, MinisterId = 6, ChurchContribution = 87.23,
            //        //        MinisterContribution = 43, ServiceContribution = 21, NetContribution = 90,  MinisterCode = "546",
            //        //         MinisterName = "Minister Name 6", ser=6, TotalContribution = 2001, PayMonth = "May", PayYear  = "2014"},
            //        //    new ContributionCheckListTable() { DistrictName="Abuja District", DistrictId = 1, MinisterId = 7, ChurchContribution = 87.23,
            //        //        MinisterContribution = 43, ServiceContribution = 21, NetContribution = 90,  MinisterCode = "547",
            //        //         MinisterName = "Minister Name 7", ser=7, TotalContribution = 2001, PayMonth = "May", PayYear  = "2014"},
            //        //    new ContributionCheckListTable() { DistrictName="Abuja District", DistrictId = 1, MinisterId = 8, ChurchContribution = 87.23,
            //        //        MinisterContribution = 43, ServiceContribution = 21, NetContribution = 90,  MinisterCode = "548",
            //        //         MinisterName = "Minister Name 8", ser=8, TotalContribution = 2001, PayMonth = "May", PayYear  = "2014"},
            //        //     new ContributionCheckListTable() { DistrictName="Abuja District", DistrictId = 1, MinisterId = 9, ChurchContribution = 87.23,
            //        //        MinisterContribution = 43, ServiceContribution = 21, NetContribution = 90,  MinisterCode = "549",
            //        //         MinisterName = "Minister Name 9", ser=9, TotalContribution = 2001, PayMonth = "May", PayYear  = "2014"},
            //        //     new ContributionCheckListTable() { DistrictName="Abuja District", DistrictId = 1, MinisterId = 10, ChurchContribution = 87.23,
            //        //        MinisterContribution = 43, ServiceContribution = 21, NetContribution = 90,  MinisterCode = "54A",
            //        //         MinisterName = "Minister Name 10", ser=10, TotalContribution = 2001, PayMonth = "May", PayYear  = "2014"},
            //        //    new ContributionCheckListTable() { DistrictName="Adamawa District", DistrictId = 2, MinisterId = 1, ChurchContribution = 87.23,
            //        //        MinisterContribution = 43, ServiceContribution = 21, NetContribution = 90, MinisterCode = "541",
            //        //         MinisterName = "Minister Name 1", ser=1, TotalContribution = 2001, PayMonth = "May", PayYear  = "2014"},
            //        //    new ContributionCheckListTable() { DistrictName="Adamawa District", DistrictId = 2, MinisterId = 2, ChurchContribution = 87.23,
            //        //        MinisterContribution = 43, ServiceContribution = 21, NetContribution = 90,  MinisterCode = "542",
            //        //         MinisterName = "Minister Name 2", ser=2, TotalContribution = 2001, PayMonth = "May", PayYear  = "2014"},
            //        //    new ContributionCheckListTable() { DistrictName="Adamawa District", DistrictId = 2, MinisterId = 3, ChurchContribution = 87.23,
            //        //        MinisterContribution = 43, ServiceContribution = 21, NetContribution = 90,  MinisterCode = "543",
            //        //         MinisterName = "Minister Name 3", ser=3, TotalContribution = 2001, PayMonth = "May", PayYear  = "2014"},
            //        //    new ContributionCheckListTable() { DistrictName="Adamawa District", DistrictId = 2, MinisterId = 4, ChurchContribution = 87.23,
            //        //        MinisterContribution = 43, ServiceContribution = 21, NetContribution = 90,  MinisterCode = "544",
            //        //         MinisterName = "Minister Name 4", ser=4, TotalContribution = 2001, PayMonth = "May", PayYear  = "2014"},
            //        //    new ContributionCheckListTable() { DistrictName="Adamawa District", DistrictId = 2, MinisterId = 5, ChurchContribution = 87.23,
            //        //        MinisterContribution = 43, ServiceContribution = 21, NetContribution = 90,  MinisterCode = "545",
            //        //         MinisterName = "Minister Name 5", ser=5, TotalContribution = 2001, PayMonth = "May", PayYear  = "2014"},
            //        //    new ContributionCheckListTable() { DistrictName="Adamawa District", DistrictId = 2, MinisterId = 6, ChurchContribution = 87.23,
            //        //        MinisterContribution = 43, ServiceContribution = 21, NetContribution = 90,  MinisterCode = "546",
            //        //         MinisterName = "Minister Name 6", ser=6, TotalContribution = 2001, PayMonth = "May", PayYear  = "2014"},
            //        //    new ContributionCheckListTable() { DistrictName="Adamawa District", DistrictId = 2, MinisterId = 7, ChurchContribution = 87.23,
            //        //        MinisterContribution = 43, ServiceContribution = 21, NetContribution = 90,  MinisterCode = "547",
            //        //         MinisterName = "Minister Name 7", ser=7, TotalContribution = 2001, PayMonth = "May", PayYear  = "2014"},
            //        //    new ContributionCheckListTable() { DistrictName="Adamawa District", DistrictId = 2, MinisterId = 8, ChurchContribution = 87.23,
            //        //        MinisterContribution = 43, ServiceContribution = 21, NetContribution = 90,  MinisterCode = "548",
            //        //         MinisterName = "Minister Name 8", ser=8, TotalContribution = 2001, PayMonth = "May", PayYear  = "2014"},
            //        //     new ContributionCheckListTable() { DistrictName="Adamawa District", DistrictId = 2, MinisterId = 9, ChurchContribution = 87.23,
            //        //        MinisterContribution = 43, ServiceContribution = 21, NetContribution = 90,  MinisterCode = "549",
            //        //         MinisterName = "Minister Name 9", ser=9, TotalContribution = 2001, PayMonth = "May", PayYear  = "2014"},
            //        //     new ContributionCheckListTable() { DistrictName="Adamawa District", DistrictId = 2, MinisterId = 10, ChurchContribution = 87.23,
            //        //        MinisterContribution = 43, ServiceContribution = 21, NetContribution = 90,  MinisterCode = "54A",
            //        //         MinisterName = "Minister Name 10", ser=10, TotalContribution = 2001, PayMonth = "May", PayYear  = "2014"},

            //        //     new ContributionCheckListTable() { DistrictName="Abuja District", DistrictId = 2, MinisterId = 1, ChurchContribution = 87.23,
            //        //        MinisterContribution = 43, ServiceContribution = 21, NetContribution = 90, MinisterCode = "541",
            //        //         MinisterName = "Minister Name 1", ser=1, TotalContribution = 2001, PayMonth = "April", PayYear  = "2014"},
            //        //    new ContributionCheckListTable() { DistrictName="Abuja District", DistrictId = 2, MinisterId = 2, ChurchContribution = 87.23,
            //        //        MinisterContribution = 43, ServiceContribution = 21, NetContribution = 90,  MinisterCode = "542",
            //        //         MinisterName = "Minister Name 2", ser=2, TotalContribution = 2001, PayMonth = "April", PayYear  = "2014"},
            //        //    new ContributionCheckListTable() { DistrictName="Abuja District", DistrictId = 2, MinisterId = 3, ChurchContribution = 87.23,
            //        //        MinisterContribution = 43, ServiceContribution = 21, NetContribution = 90,  MinisterCode = "543",
            //        //         MinisterName = "Minister Name 3", ser=3, TotalContribution = 2001, PayMonth = "April", PayYear  = "2014"},
            //        //    new ContributionCheckListTable() { DistrictName="Abuja District", DistrictId = 2, MinisterId = 4, ChurchContribution = 87.23,
            //        //        MinisterContribution = 43, ServiceContribution = 21, NetContribution = 90,  MinisterCode = "544",
            //        //         MinisterName = "Minister Name 4", ser=4, TotalContribution = 2001, PayMonth = "April", PayYear  = "2014"},
            //        //    new ContributionCheckListTable() { DistrictName="Abuja District", DistrictId = 2, MinisterId = 5, ChurchContribution = 87.23,
            //        //        MinisterContribution = 43, ServiceContribution = 21, NetContribution = 90,  MinisterCode = "545",
            //        //         MinisterName = "Minister Name 5", ser=5, TotalContribution = 2001, PayMonth = "April", PayYear  = "2014"},
            //        //    new ContributionCheckListTable() { DistrictName="Abuja District", DistrictId = 2, MinisterId = 6, ChurchContribution = 87.23,
            //        //        MinisterContribution = 43, ServiceContribution = 21, NetContribution = 90,  MinisterCode = "546",
            //        //         MinisterName = "Minister Name 6", ser=6, TotalContribution = 2001, PayMonth = "April", PayYear  = "2014"},
            //        //    new ContributionCheckListTable() { DistrictName="Abuja District", DistrictId = 2, MinisterId = 7, ChurchContribution = 87.23,
            //        //        MinisterContribution = 43, ServiceContribution = 21, NetContribution = 90,  MinisterCode = "547",
            //        //         MinisterName = "Minister Name 7", ser=7, TotalContribution = 2001, PayMonth = "April", PayYear  = "2014"},
            //        //    new ContributionCheckListTable() { DistrictName="Abuja District", DistrictId = 2, MinisterId = 8, ChurchContribution = 87.23,
            //        //        MinisterContribution = 43, ServiceContribution = 21, NetContribution = 90,  MinisterCode = "548",
            //        //         MinisterName = "Minister Name 8", ser=8, TotalContribution = 2001, PayMonth = "April", PayYear  = "2014"},
            //        //     new ContributionCheckListTable() { DistrictName="Abuja District", DistrictId = 2, MinisterId = 9, ChurchContribution = 87.23,
            //        //        MinisterContribution = 43, ServiceContribution = 21, NetContribution = 90,  MinisterCode = "549",
            //        //         MinisterName = "Minister Name 9", ser=9, TotalContribution = 2001, PayMonth = "April", PayYear  = "2014"},
            //        //     new ContributionCheckListTable() { DistrictName="Abuja District", DistrictId = 2, MinisterId = 10, ChurchContribution = 87.23,
            //        //        MinisterContribution = 43, ServiceContribution = 21, NetContribution = 90,  MinisterCode = "54A",
            //        //         MinisterName = "Minister Name 10", ser=10, TotalContribution = 2001, PayMonth = "April", PayYear  = "2014"},
            //        //    new ContributionCheckListTable() { DistrictName="Adamawa District", DistrictId = 2, MinisterId = 1, ChurchContribution = 87.23,
            //        //        MinisterContribution = 43, ServiceContribution = 21, NetContribution = 90, MinisterCode = "541",
            //        //         MinisterName = "Minister Name 1", ser=1, TotalContribution = 2001, PayMonth = "April", PayYear  = "2014"},
            //        //    new ContributionCheckListTable() { DistrictName="Adamawa District", DistrictId = 2, MinisterId = 2, ChurchContribution = 87.23,
            //        //        MinisterContribution = 43, ServiceContribution = 21, NetContribution = 90,  MinisterCode = "542",
            //        //         MinisterName = "Minister Name 2", ser=2, TotalContribution = 2001, PayMonth = "April", PayYear  = "2014"},
            //        //    new ContributionCheckListTable() { DistrictName="Adamawa District", DistrictId = 2, MinisterId = 3, ChurchContribution = 87.23,
            //        //        MinisterContribution = 43, ServiceContribution = 21, NetContribution = 90,  MinisterCode = "543",
            //        //         MinisterName = "Minister Name 3", ser=3, TotalContribution = 2001, PayMonth = "April", PayYear  = "2014"},
            //        //    new ContributionCheckListTable() { DistrictName="Adamawa District", DistrictId = 2, MinisterId = 4, ChurchContribution = 87.23,
            //        //        MinisterContribution = 43, ServiceContribution = 21, NetContribution = 90,  MinisterCode = "544",
            //        //         MinisterName = "Minister Name 4", ser=4, TotalContribution = 2001, PayMonth = "April", PayYear  = "2014"},
            //        //    new ContributionCheckListTable() { DistrictName="Adamawa District", DistrictId = 2, MinisterId = 5, ChurchContribution = 87.23,
            //        //        MinisterContribution = 43, ServiceContribution = 21, NetContribution = 90,  MinisterCode = "545",
            //        //         MinisterName = "Minister Name 5", ser=5, TotalContribution = 2001, PayMonth = "April", PayYear  = "2014"},
            //        //    new ContributionCheckListTable() { DistrictName="Adamawa District", DistrictId = 2, MinisterId = 6, ChurchContribution = 87.23,
            //        //        MinisterContribution = 43, ServiceContribution = 21, NetContribution = 90,  MinisterCode = "546",
            //        //         MinisterName = "Minister Name 6", ser=6, TotalContribution = 2001, PayMonth = "April", PayYear  = "2014"},
            //        //    new ContributionCheckListTable() { DistrictName="Adamawa District", DistrictId = 2, MinisterId = 7, ChurchContribution = 87.23,
            //        //        MinisterContribution = 43, ServiceContribution = 21, NetContribution = 90,  MinisterCode = "547",
            //        //         MinisterName = "Minister Name 7", ser=7, TotalContribution = 2001, PayMonth = "April", PayYear  = "2014"},
            //        //    new ContributionCheckListTable() { DistrictName="Adamawa District", DistrictId = 2, MinisterId = 8, ChurchContribution = 87.23,
            //        //        MinisterContribution = 43, ServiceContribution = 21, NetContribution = 90,  MinisterCode = "548",
            //        //         MinisterName = "Minister Name 8", ser=8, TotalContribution = 2001, PayMonth = "April", PayYear  = "2014"},
            //        //     new ContributionCheckListTable() { DistrictName="Adamawa District", DistrictId = 2, MinisterId = 9, ChurchContribution = 87.23,
            //        //        MinisterContribution = 43, ServiceContribution = 21, NetContribution = 90,  MinisterCode = "549",
            //        //         MinisterName = "Minister Name 9", ser=9, TotalContribution = 2001, PayMonth = "April", PayYear  = "2014"},
            //        //     new ContributionCheckListTable() { DistrictName="Adamawa District", DistrictId = 2, MinisterId = 10, ChurchContribution = 87.23,
            //        //        MinisterContribution = 43, ServiceContribution = 21, NetContribution = 90,  MinisterCode = "54A",
            //        //         MinisterName = "Minister Name 10", ser=10, TotalContribution = 2001, PayMonth = "April", PayYear  = "2014"}
            //        //};
            //        //contributionchecklist.ForEach(p => context.ContributionCheckListTables.Add(p));
            //        //context.SaveChanges();

            #endregion

        }
    }

    #endregion
}