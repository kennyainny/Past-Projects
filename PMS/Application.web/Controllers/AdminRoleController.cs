using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Microsoft.Reporting.WebForms;
using Application.Web.RDLC_DataSet;
using Application.Web.Models;
using System.Web.UI.WebControls;
using System.IO;
using System.Web.UI;
using System.Configuration;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Threading;
using NPOI.HSSF.UserModel;
using LinqToExcel;
using System.Net.Http;
using System.Text;
using WebMatrix.WebData;
using System.Threading.Tasks;

namespace Application.Web.Controllers
{
    public class AdminRoleController : Controller
    {
        #region Global Variables.
        
        private ApplicationDatabaseContext db2 = new ApplicationDatabaseContext();
        private UsersContext db1 = new UsersContext();
        //PensionDBEntitiesConnection db0 = new PensionDBEntitiesConnection();

        #endregion

        #region Landing Page for Admin.

        public ActionResult Index()
        {
            //var penDB = new PensionDBEntitiesConnection()
            //try
            //{
            //    string fileName = "Ministers1.xlsx"; // Request.Files["file"].FileName;
            //    string extension = System.IO.Path.GetExtension(fileName);
            //    string path1 = string.Format("{0}/{1}", Server.MapPath("~/Content/UploadedFolder"), fileName);

            //    //if (System.IO.File.Exists(path1)) System.IO.File.Delete(path1);

            //    //Request.Files["file"].SaveAs(path1);

            //    //var databaseDistrict = db2.Districts.Select(c => new { c.Id, c.DistrictName }).OrderBy(p => p.DistrictName);

            //    //HttpPostedFileBase hpf = this.Request.Files[0];

            //    var excel = new ExcelQueryFactory(path1);//FileName);
            //    var excelModelList = new List<MinisterProfile>();

            //    //var excel = new ExcelQueryFactory("C:\\Documents and Settings\\rk.prabakar\\Desktop\\test.xlsx");
            //    //foreach (var district in databaseDistrict)
            //    //{
            //    //  string xx = district.DistrictName.ToString();
            //    //int zz = district.Id;
            //    var dataContent = from c in excel.Worksheet<MinisterProfile>(1) select c;

            //    //var exsistingRecord = db2.ContributionScheduleTables.Where(c => c.DistrictId.Equals(zz)).ToList();

            //    //Checking the datacontent before accessing
            //    if (dataContent != null)
            //    {
            //        //reading content from the excel comes here
            //        foreach (var a in dataContent)
            //        {
            //            var excelModel = new MinisterProfile();

            //            //excelModel.Id = a.Id;
            //            excelModel.MinisterCode = a.MinisterCode;
            //            excelModel.Surname = a.Surname;
            //            excelModel.OtherName = a.OtherName;
            //            excelModel.DistrictId = a.DistrictId;
            //            excelModel.Salary = a.Salary;
            //            excelModel.DateOfBirth = a.DateOfBirth;//DateTime.Parse(a.DateOfBirth.ToShortDateString());
            //            excelModel.Retired = a.Retired;
            //            excelModel.Both = a.Both;
            //            excelModel.Stopped = a.Stopped;
            //            excelModel.NextOfKin = a.NextOfKin;
            //            excelModel.EmployDate = a.EmployDate;//DateTime.Parse(a.EmployDate.ToShortDateString());
            //            excelModel.RetiredDate = a.RetiredDate;// DateTime.Parse(a.RetiredDate.ToShortDateString());
            //            excelModel.MergeDate = DateTime.Parse("01/02/1990"); //a.MergeDate;
            //            excelModel.DateJoined = a.DateJoined;//DateTime.Parse(a.DateJoined.ToShortDateString());
            //            excelModel.NextRelation = a.NextRelation;
            //            excelModel.ExitReason = a.ExitReason;
            //            excelModel.nExist = a.nExist;
            //            excelModel.nBatch = a.nBatch;//DateTime.Parse(a.nBatch.ToShortDateString());
            //            excelModel.nCheque = a.nCheque;
            //            excelModel.nUserID = a.nUserID;
            //            excelModel.PhotoString = a.PhotoString;
            //            excelModel.YearsServed = a.YearsServed;
            //            excelModel.RegNumber = a.RegNumber;
            //            excelModel.PermanentAddress = a.PermanentAddress;

            //            if (ModelState.IsValid)
            //            {                            
            //                excelModelList.Add(excelModel);
            //            }
            //            else
            //            {
            //                var errors = ModelState.Where(x => x.Value.Errors.Count > 0).Select(x => new { x.Key, x.Value.Errors }).ToArray();
            //                TempData["status"] = errors;
            //            }
            //        }
            //    }

            //    db2.MinisterProfiles.AddRange(excelModelList);
            //    db2.SaveChanges();
            //    TempData["status"] = " Database Updated!";
            //}
            //catch (AggregateException e)
            //{
            //    TempData["status"] = " Error: " + e.Message;
            //}

            //TempData["status"] = " Import successful! ContributionSchedule has been updated with " + fileName;

            //return RedirectToAction("ContributionSchedule");

            if (TempData["status"] != null)
                ViewBag.info = TempData["status"];
            else ViewBag.info = " This is Admin panel, only Administrators are allowed!";

            //ViewBag.totalDistricts = db2.ContributionCheckListTables.Select(c => c.TotalContribution).Sum() ?? 0D;

            ViewBag.totalDistricts = db2.Districts.Count();
            ViewBag.totalMinisters = db2.MinisterProfiles.Count();
            ViewBag.totalChecklist = db2.ContributionCheckListTables.Count();
            ViewBag.totalSchedule = db2.ContributionScheduleTables.Count();

            ViewBag.lastPost = DateTime.Now.ToShortDateString();

            //if database is empty, redirect to anoda controller to populate
            //use asynchronous processing to populate several fields
            //Asynchronous Processing to display statistics

            return View(db1.UserProfiles.ToList());
        }
        
        #endregion

        #region Manage Ministers.
        
        public ActionResult RegisterMinister(string SelectedDistrictId = "0")
        {
            if (TempData["status"] != null)
                ViewBag.info = TempData["status"];
            else ViewBag.info = " Please fill in the details of the new Minister";

            //Build a DDL for list of Districts
            var districtList = db2.Districts.Select(c => new { c.DistrictCode, c.DistrictName }).OrderBy(p => p.DistrictName);

            //Cache the District DDL selected index and display it in the view
            int districtSelected;

            if (SelectedDistrictId.Equals("0") || SelectedDistrictId == null)
                districtSelected = Int32.Parse(districtList.First().DistrictCode);
            else districtSelected = Convert.ToInt32(SelectedDistrictId);

            string swaer = districtSelected.ToString();

            ViewBag.districtSelected = districtList.Where(m => m.DistrictCode == swaer).First().DistrictName;

            ViewBag.DistrictId = districtSelected;

            TempData["DistrictSelectList"] =
                new SelectList(districtList.AsEnumerable(), "DistrictCode", "DistrictName", districtSelected);

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult RegisterMinister(MinisterProfile ministerProf)
            //[Bind(Include ="Id,DistrictId,District,MinisterCode,Surname,OtherName,PermanentAddress,Salary,NextOfKin,DateOfBirth,EmployDate,DateJoined,Status")]            
        {
            if (ModelState.IsValid)
            {
                bool exists = false;
                foreach (var ministr in db2.MinisterProfiles)
                {
                    //check for duplicate entry
                    if (ministr.MinisterCode.Equals(ministerProf.MinisterCode) && ministr.Surname.Equals(ministerProf.Surname)
                         && ministr.DistrictId.Equals(ministerProf.DistrictId)) exists = true;
                }

                if (!exists)
                {
                    // Attempt to register the user
                    try
                    {
                        db2.MinisterProfiles.Add(ministerProf);
                        db2.SaveChanges();
                        TempData["status"] = " " + ministerProf.MinisterCode + " - " + ministerProf.Surname + " was successfully registered!";                        
                        return RedirectToAction("RegisterMinister");
                    }
                    catch (AggregateException e)
                    {
                        TempData["status"] = e.Message.ToString();
                        return RedirectToAction("RegisterMinister");
                    }
                }
            }

            TempData["status"] = " Minister already exists, please register a new minister";
            return RedirectToAction("RegisterMinister");
        }

        public ActionResult ManageMinister(string SelectedDistrictID = "0")
        {
            if (TempData["status"] != null)
                ViewBag.info = TempData["status"];
            else  ViewBag.info = " Please select a Minister you want to modify and click 'Edit' or 'Exit'";

            var districtList = db2.Districts.Select(c => new { c.DistrictCode, c.DistrictName }).OrderBy(p => p.DistrictCode);

            int SelectedDistrict;

            if (SelectedDistrictID.Equals("0") && Session["selected"] == null)
                SelectedDistrict = Int32.Parse(districtList.First().DistrictCode);
            else if (SelectedDistrictID.Equals("0") && Session["selected"] != null)
                SelectedDistrict = Convert.ToInt32(Session["selected"]);
            else
                SelectedDistrict = Convert.ToInt32(SelectedDistrictID);

            Session["selected"] = SelectedDistrict;

            TempData["DistrictSelectList"] =
               new SelectList(districtList.AsEnumerable(), "DistrictCode", "DistrictName", SelectedDistrict);

            return View(db2.MinisterProfiles.Where(m => m.Stopped.Equals(0) && m.DistrictId.Equals(SelectedDistrict))
                .OrderBy(c => c.MinisterCode).ThenBy(p => p.Surname).ToList());
        }
        
        public ActionResult EditMinister(double? id, string SelectedDistrictId = "0")
        {
            if (TempData["status"] != null)
                ViewBag.info = TempData["status"];
            else ViewBag.info = " Here you can Edit, Move, Merge or Exit a Minister!";

            if (id == null)
            {
                TempData["status"] = " Error! Minister no longer exist.";
                return Redirect(Session["ReturnUrl"].ToString());                
            }

            MinisterProfile minister = db2.MinisterProfiles.Find(id);
                //.Where(c => c.Id.Equals((int)id)).FirstOrDefault();

            if (minister == null)
            {
                TempData["status"] = " Error! Minister no longer exist.";
                return Redirect(Session["ReturnUrl"].ToString());            
            }

            //Build a DDL for list of Districts
            var districtList = db2.Districts
               .Select(c => new { c.DistrictCode, c.DistrictName })
               .OrderBy(p => p.DistrictName);
            
            //Cache the District DDL selected index and display it in the view
            int districtSelected;

            if (SelectedDistrictId.Equals("0") || SelectedDistrictId == null)
                districtSelected = minister.DistrictId;
            else districtSelected = Convert.ToInt32(SelectedDistrictId);

            string cdf =  districtSelected.ToString();
            ViewBag.districtSelected = districtList.Where(m => m.DistrictCode ==cdf).First().DistrictName;

            ViewBag.DistrictId = districtSelected;

            TempData["DistrictSelectList"] =
                new SelectList(districtList.AsEnumerable(), "DistrictCode", "DistrictName", districtSelected);

            return View(minister);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditMinister(MinisterProfile ministerProf)
            //[Bind(Include ="Id,DistrictId,District,MinisterCode,Surname,OtherName,PermanentAddress,ExitReason,Salary,NextOfKin,DateOfBirth,EmployDate,DateJoined,Status")]            
        {
            if (ModelState.IsValid)
            {
                db2.Entry(ministerProf).State = EntityState.Modified;
                db2.SaveChanges();

                TempData["status"] = " " + ministerProf.Surname.ToUpper() + ", " + ministerProf.OtherName + " successfully updated!";

                return Redirect(Session["ReturnUrl"].ToString());
            }

            TempData["status"] = " An error has occured! Please check your inputs and try again";
            return View(ministerProf);
        }

        public ActionResult PaidOffMinisters()
        {

            if (TempData["status"] != null)
                ViewBag.info = TempData["status"];
            else ViewBag.info = " Below is the list of 'Paid Ministers' in AGMBS";


            return View(db2.MinisterProfiles.Where(m => m.nCheque != null && m.nCheque != "" && m.Stopped > 0)
                .OrderBy(c => c.MinisterCode).ThenBy(p => p.Surname).ToList());

        }
        public ActionResult ExitedMinisters()
        {
            if (TempData["status"] != null)
                ViewBag.info = TempData["status"];
            else ViewBag.info = " Below is the list of 'Exited Ministers' in AGMBS";


            ExitedMinistersModel _objchequemodel = new ExitedMinistersModel();
            
            _objchequemodel.SelectedMinistersList = new List<MinisterProfile>();
            _objchequemodel.SelectedMinistersList = db2.MinisterProfiles.Where(m => m.Stopped > 0)
                .OrderBy(c => c.MinisterCode).ThenBy(p => p.Surname).ToList();

            return View(db2.MinisterProfiles.Where(m => m.Stopped > 0)
                .OrderBy(c => c.MinisterCode).ThenBy(p => p.Surname).ToList());
        }

        public ActionResult AssignChequeNumber()
        {
            if (TempData["status"] != null)
                ViewBag.info = TempData["status"];
            else ViewBag.info = " Assign Pay Off Cheque to Exited Ministers";

                return RedirectToAction("ExitedMinisters");
        }

        [HttpPost]
        public async Task<ActionResult> AssignChequeNumber(List<string> chkBox)
        {

            var selectedCode = new HashSet<string>(chkBox);

            if (TempData["status"] != null)
                ViewBag.info = TempData["status"];
            else ViewBag.info = " Assign Pay Off Cheque to Exited Ministers";
            if (chkBox == null)
            {
                return RedirectToAction("ExitedMinisters");
            }

            ////if(ModelState.IsValid)
            ////{
            ////    db2.Entry(_objchequemodel).State = EntityState.Modified;
            ////    db2.SaveChanges();

            ////    TempData["status"] = " " + _objchequemodel.Surname.ToUpper() + ", " + _objchequemodel.OtherName + " successfully updated!";

            ////    return Redirect(Session["ReturnUrl"].ToString());
            ////}

            //StringBuilder sb = new StringBuilder();
            //sb.Append("SELECTED COUNTRY :- ").AppendLine();

            //Array itemSelected;
            //itemSelected = "1232".ToArray();
            //foreach (string item in Request["chkBox"].Split(','))
            //{
            //    if (item != null)
            //    {
            //        itemSelected = item.ToArray();
            //        sb.Append(item + ", ").AppendLine();
                    
            //    }
            //}
            //ViewBag.Selected = sb.ToString();
            //var ddd = chkBox.ToString();

            //ViewBag.Amount = db2.AccountRecords.Where(s => selectedCode.Contains(s.MinisterCode)).Max(a => a.PostDate);
            TempData["status"] = " Cheque Details Saved Successfuly";
            return View(db2.MinisterProfiles.Where(s => selectedCode.Contains(s.MinisterCode)));

            //return View(db2.MinisterProfiles.Where(s => chkBox.All(w => s.MinisterCode.Contains(w))).ToList());
        }
        [HttpPost]
        public ActionResult PayoffReport(List<string> selc, FormCollection c)
        {

            var selectedCode = new HashSet<string>(selc);

            //string query2 = "SELECT NetBal FROM AccountRecords WHERE MinisterCode = @p0";
            //AccountRecord NetBal = await db2.AccountRecords.SqlQuery(query2, selectedCode.ToString()).SingleOrDefaultAsync();


            //string query = "SELECT EnrollmentDate, COUNT(*) AS StudentCount "
            //    + "FROM Person "
            //    + "WHERE Discriminator = 'Student' "
            //    + "GROUP BY EnrollmentDate";
            //IEnumerable<AccountRecord> data = db2.Database.SqlQuery<AccountRecord>(query);

            //return View(data.ToList());



            ////ViewBag.Amount = db2.AccountRecords.Where(s => selectedCode.Contains(s.MinisterCode)).OrderBy(vw => vw.NetBal).FirstOrDefault();
            //try
            //{

            //    ViewBag.Amount = db2.AccountRecords.Where(s => selectedCode.Contains(s.MinisterCode)).OrderBy(vw => vw.NetBal).Max(l => l.NetBal);
            //    //ViewBag.Amount = NetBal;
            //}
            //catch (Exception e)
            //{
            //    ViewBag.Amount = "error  " + e;

            //}



            try
            {
                ViewBag.Amount = db2.AccountRecords.Where(s => selectedCode.Contains(s.MinisterCode) && s.Id.Equals(db2.AccountRecords.Max(o => o.Id))).ToList();
            }
            catch (Exception e)
            {
                ViewBag.Amount = "Error  " + e;

            }
            if (TempData["status"] != null)
                ViewBag.info = TempData["status"];
            else ViewBag.info = " Paid Off Ministers Report"; 



            int i = 0;
            if (ModelState.IsValid)
            {
                var ChequeIDArray = c.GetValues("c.Id");
                var ChequeArray = c.GetValues("c.nCheque");

                for (i = 0; i < ChequeIDArray.Count(); i++)
                {
                    MinisterProfile usr = db2.MinisterProfiles.Find(Convert.ToInt32(ChequeIDArray[i]));
                    usr.nCheque = ChequeArray[i];
                    db2.Entry(usr).State = EntityState.Modified;
                db2.SaveChanges();
                }
            }

            else
            {

                TempData["status"] = "Failed ! Please try again.";

                return RedirectToAction("AssignChequeNumber");

            }




            //chkBox = Request["selc"];
            // from t1 in dt1.Rows.Cast<DataRow>()
                //        join t2 in dt2.Rows.Cast<DataRow>() on t1["ID"] equals t2["ID"]
               //       select t1;
            var v = db2.MinisterProfiles.Where(s => selectedCode.Contains(s.MinisterCode));
            //var v = from t in db2.MinisterProfiles.Cast<MinisterProfile>() join t2 in db2.AccountRecords.Cast<AccountRecord>() on t.MinisterCode equals t2.MinisterCode where selectedCode.Contains(t.MinisterCode) select t;
            return View(v);
        }

        public ActionResult ExitMinister(double? id, string MinStatus = "Active")
        {
            if (TempData["status"] != null)
                ViewBag.info = TempData["status"];
            else ViewBag.info = " Here you can Exit a Minister and give reasons for doing so.";

            if (id == null)
            {
                TempData["status"] = " Error! Minister no longer exist.";
                return Redirect(Session["ReturnUrl"].ToString()); 
            }

            MinisterProfile minister = db2.MinisterProfiles.Find(id);                

            if (minister == null)
            {
                TempData["status"] = " Error! Minister no longer exist.";
                return Redirect(Session["ReturnUrl"].ToString()); 
            }

            //string ministerStatus;
            //if (MinStatus.Equals("0") || MinStatus == null)
            //    ministerStatus = "Active";
            //else
            
            //    ministerStatus = MinStatus;

            //var exitArray = new string[,] { {"0","Active"},{"1","Death"}, {"2","Retirement"},{"3","Dismissal"}, {"4","Resignation"}};

            //List<string> exitList = exitArray.Cast<string>().ToList();
            //TempData["MinisterStatus"] =  new SelectList(exitList.AsEnumerable(), "MinStatId", "ExitType", MinStatusId);

            TempData["MinisterStatus"] = new SelectList(new[]
            { "Active", "Death", "Retirement", "Dismissal", "Resignation", "Left The Ministry", "Others" }
               .Select(x => new { Value = x, Text = x }), "Value", "Text", MinStatus);

            //var statusList =enumList.Select(c=> new {"Text", "Value"});
            //IEnumerable<string> enumList = exitArray.Cast<string>();
            //.Select(c => new { c.Id, c.DistrictName }).OrderBy(p => p.DistrictName);
         
            //TempData["MinisterStatus"] = new SelectList(enumList.Select(x =>x. new { Value = x, Text = y }), "Value", "Text", ministerStatus);            
            if (MinStatus.Equals("Active")) ViewBag.minStatusId = 0;
            if (MinStatus.Equals("Death")) ViewBag.minStatusId = 1;
            if (MinStatus.Equals("Retirement"))
            {
                ViewBag.minStatusId = 2;
                ViewBag.retired = 1;
            }
            if (MinStatus.Equals("Dismissal")) ViewBag.minStatusId = 3;
            if (MinStatus.Equals("Resignation")) ViewBag.minStatusId = 4;
            if (MinStatus.Equals("Left The Ministry")) ViewBag.minStatusId = 5;
            if (MinStatus.Equals("Others")) ViewBag.minStatusId = 6;
                        
            ViewBag.minStatus = MinStatus;

            return View(minister);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ExitMinister(MinisterProfile ministerProf)
            //[Bind(Include ="Id,DistrictId,District,MinisterCode,Surname,OtherName,PermanentAddress,ExitReason,Salary,NextOfKin,DateOfBirth,EmployDate,DateJoined,Status")]             
        {
            if (ModelState.IsValid)
            {
                db2.Entry(ministerProf).State = EntityState.Modified;
                db2.SaveChanges();

                TempData["status"] = " " + ministerProf.Surname.ToUpper() + ", " + ministerProf.OtherName + " successfully exited!";

                return Redirect(Session["ReturnUrl"].ToString());
            }

            TempData["status"] = " An error has occured! Please check your inputs and try again";
            return View(ministerProf);
        }

        #region Merger Minister
        //public ActionResult MergeMinister(double? id, string MinStatus = "0")
        //{
        //    if (TempData["status"] != null)
        //        ViewBag.info = TempData["status"];
        //    else ViewBag.info = " Here you can Exit a Minister and give reasons for doing so.";

        //    if (id == null)
        //    {
        //        TempData["status"] = " Error! Minister no longer exist.";
        //        return Redirect(Session["ReturnUrl"].ToString());
        //    }

        //    MinisterProfile minister = db2.MinisterProfiles.Find(id);

        //    if (minister == null)
        //    {
        //        TempData["status"] = " Error! Minister no longer exist.";
        //        return Redirect(Session["ReturnUrl"].ToString());
        //    }
            
        //    var exitArray = new string[,] { { "0", "Active" }, { "1", "Death" }, { "2", "Retirement" }, { "3", "Dismissal" }, { "4", "Resignation" } };

        //    List<string> exitList = exitArray.Cast<string>().ToList();
        //    TempData["MinisterStatus"] = new SelectList(exitList.AsEnumerable(), "Id", "ExitType", MinStatus);

        //    ViewBag.minStatus = MinStatus;

        //    return View(minister);
        //}

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult MergeMinister(MinisterProfile ministerProf)
        ////[Bind(Include ="Id,DistrictId,District,MinisterCode,Surname,OtherName,PermanentAddress,ExitReason,Salary,NextOfKin,DateOfBirth,EmployDate,DateJoined,Status")]             
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db2.Entry(ministerProf).State = EntityState.Modified;
        //        db2.SaveChanges();

        //        TempData["status"] = " " + ministerProf.Surname.ToUpper() + ", " + ministerProf.OtherName + " successfully exited!";

        //        return Redirect(Session["ReturnUrl"].ToString());
        //    }

        //    TempData["status"] = " An error has occured! Please check your inputs and try again";
        //    return View(ministerProf);
        //}

        #endregion

        #endregion

        #region Manage Districts.

        public ActionResult RegisterDistrict()
        {
            if (TempData["status"] != null)
                ViewBag.info = TempData["status"];
            else ViewBag.info = " Please fill in the details of the new District you wish to register";

            return View();
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult RegisterDistrict(District newDistrict)
           // [Bind(Include = "Id,DistrictCode,DistrictName,Manager,Address1,City,State,PhoneNo,Email")]            
        {
            if (ModelState.IsValid)
            {
                bool exists = false;
                foreach (var district in db2.Districts)
                {                    
                    if (district.DistrictCode.Equals(newDistrict.DistrictCode) && district.DistrictName.Equals(newDistrict.DistrictName)
                        && district.City.Equals(newDistrict.City))  exists = true;
                }

                if (!exists)
                {
                    try
                    {
                        db2.Districts.Add(newDistrict);
                        db2.SaveChanges();
                        TempData["status"] = " " + newDistrict.DistrictName + " was successfully registered!";                        
                        return RedirectToAction("RegisterDistrict");
                    }
                    catch (AggregateException e)
                    {
                        TempData["status"] = e.Message.ToString();
                        return RedirectToAction("RegisterDistrict");
                    }
                }
            }

            TempData["status"] = " District already exist, please enter a new district";
            return RedirectToAction("RegisterDistrict");
        }

        public ActionResult ManageDistrict()
        {
            if (TempData["status"] != null)
                ViewBag.info = TempData["status"];
            else ViewBag.info = " Please select a District you want to modify and click 'Edit'.";

            return View(db2.Districts.OrderBy(c => c.DistrictCode).ThenBy(p => p.DistrictName).ToList());
        }

        public ActionResult EditDistrict(double? id)
        {
            if (TempData["status"] != null)
                ViewBag.info = TempData["status"];
            else ViewBag.info = " Please enter new values for the District!";

            if (id == null)
            {
                TempData["status"] = " Error! District no longer exist.";
                return Redirect(Session["ReturnUrl"].ToString());
            }

            District district1 = db2.Districts.Where(c => c.Id.Equals((int)id)).First();

            if (district1 == null)
            {
                TempData["status"] = " Error! District no longer exist.";
                return Redirect(Session["ReturnUrl"].ToString());
            }

            return View(district1);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditDistrict(District districtProf)
            //[Bind(Include ="Id,DistrictCode,DistrictName,Address1,City,State,PhoneNo,Email,Manager")]          
        {
            if (ModelState.IsValid)
            {
                db2.Entry(districtProf).State = EntityState.Modified;
                db2.SaveChanges();

                TempData["status"] = " " + districtProf.DistrictName + " successfully updated!";

                return Redirect(Session["ReturnUrl"].ToString());
            }

            TempData["status"] = " Error! Please check the values entered and try again.";
            return View(districtProf);
        }

        #endregion

        #region Manage Contributions Schedule.

        public ActionResult ContributionSchedule(string SelectedDistrictId = "0")
        {
            if (TempData["status"] == null)
                @ViewBag.info = " Here you can modify existing records or add a new record";
            else
                @ViewBag.info = TempData["status"];
            
            string yearValue = db2.ContributionScheduleTables.Select(p => p.ScheduleYear).FirstOrDefault();
            string monthValue = db2.ContributionScheduleTables.Select(p => p.ScheduleMonth).FirstOrDefault();
            
            if (yearValue == null || monthValue == null)
            {
                //Direct to add new Schedule
                TempData["status"] = " Contribution Schedule record is empty! Please start a new Schedule for this month.";
                return RedirectToAction("CreateTempTableRecord");
            }
            ViewBag.year = yearValue;
            ViewBag.month = monthValue;

            int yearId = db2.Years.Where(c => c.YearValue.Equals(yearValue)).Select(c => c.Id).First();
            Session["SelectedYear"] = yearId;

            int monthId = db2.Months.Where(c => c.MonthName.Equals(monthValue)).Select(c => c.Id).First();
            Session["SelectedMonth"] = monthId;

            //Cache and retrieve last selected District
            var districtList = db2.Districts.Select(c => new { c.DistrictCode, c.DistrictName }).OrderBy(p => p.DistrictName);

            int SelectedDistrict;

            if (SelectedDistrictId.Equals("0") && Session["selected"] == null)
                SelectedDistrict = Int32.Parse(districtList.First().DistrictCode);
            else if (SelectedDistrictId.Equals("0") && Session["selected"] != null)
                SelectedDistrict = Convert.ToInt32(Session["selected"]);
            else
                SelectedDistrict = Convert.ToInt32(SelectedDistrictId);

            Session["selected"] = SelectedDistrict;

            string cdf = SelectedDistrict.ToString();
            //Query DB to fetch out Contribution Schedule list
            ViewBag.district = districtList.Where(m => m.DistrictCode == cdf).First().DistrictName.ToString();

            TempData["DistrictSelectList"] =
                new SelectList(districtList.AsEnumerable(), "DistrictCode", "DistrictName", SelectedDistrict);

            //Intialize and populate DTO
            var contSchedDTOList = new List<ContributionScheduleTableDTO>();
            var tempTable = db2.ContributionScheduleTables.Where(c => c.DistrictId.Equals(SelectedDistrict)).ToList();

            //int i = 0;
            foreach (ContributionScheduleTable item in tempTable)
            {
                string ccdf1 = item.MinisterId.ToString();
                MinisterProfile minProf = db2.MinisterProfiles.Where(c => c.MinisterCode == ccdf1).FirstOrDefault();

                var contSchedDTO = new ContributionScheduleTableDTO();
                contSchedDTO.ChurchContribution = item.ChurchContribution;
                contSchedDTO.DistrictId = item.DistrictId;
                contSchedDTO.Id = item.Id;
                contSchedDTO.MinisterId = item.MinisterId;
                contSchedDTO.MinisterCode = minProf.MinisterCode;
                contSchedDTO.MinisterContribution = item.MinisterContribution;
                contSchedDTO.Month = item.ScheduleMonth;
                contSchedDTO.OtherName = minProf.OtherName;
                contSchedDTO.Surname = minProf.Surname;
                contSchedDTO.TotalContribution = item.TotalContribution;
                contSchedDTO.Year = item.ScheduleYear;

                contSchedDTOList.Add(contSchedDTO);
            }

            if (contSchedDTOList.Count == 0) ViewBag.IsEmpty = true;
            else ViewBag.IsEmpty = false;

            return View(contSchedDTOList.OrderBy(c => c.MinisterCode));
        }
                
        public ActionResult EditTempTableRecord(double? id)
        {
            if (TempData["status"] != null)
                ViewBag.info = TempData["status"];
            else  ViewBag.info = " Here you can modify Church and Minister Contribution.";

            if (id == null)
            {
                TempData["status"] = " Error! Minister record no longer exist.";
                return Redirect(Session["ReturnUrl"].ToString());
            }

            ContributionScheduleTable tempTableRecord = db2.ContributionScheduleTables.Find(id);
             
            if (tempTableRecord == null)
            {
                TempData["status"] = " Error! Minister record no longer exist.";
                return Redirect(Session["ReturnUrl"].ToString());
            }

            string cdrf = tempTableRecord.MinisterId.ToString();

            MinisterProfile tempRecord = db2.MinisterProfiles
                .Where(c => c.MinisterCode == cdrf).FirstOrDefault();

            string cdse = tempTableRecord.DistrictId.ToString();
            ViewBag.district = db2.Districts
                .Where(c => c.DistrictCode == cdse).First().DistrictName;
            ViewBag.ministerCode = tempRecord.MinisterCode.ToString();
            ViewBag.surname = tempRecord.Surname.ToString();
            ViewBag.otherName = tempRecord.OtherName.ToString();

            return View(tempTableRecord);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditTempTableRecord(ContributionScheduleTable contributionScheduleTab)
            //[Bind(Include ="Id,DistrictId,MinisterId,ChurchContribution,MinisterContribution,TotalContribution,ScheduleMonth,ScheduleYear")]            
        {
            if (ModelState.IsValid)
            {
                db2.Entry(contributionScheduleTab).State = EntityState.Modified;
                db2.SaveChanges();

                TempData["status"] = " Entry successfully updated!";
                return Redirect(Session["ReturnUrl"].ToString());
            }

            TempData["status"] = " Error! Please check the values entered and try again.";
            return View(contributionScheduleTab);
        }
                
        public ActionResult DeleteTempTableRecord(double? id)
        {            
            ViewBag.info = " Are you sure you want to delete this entry?";

            if (id == null)
            {
                TempData["status"] = " Error! Minister record no longer exist.";
                return Redirect(Session["ReturnUrl"].ToString());
            }

            ContributionScheduleTable tempTableRecord = db2.ContributionScheduleTables
                .Where(c => c.Id.Equals((int)id)).First();

            if (tempTableRecord == null)
            {
                TempData["status"] = " Error! Minister record nolonger exist.";
                return Redirect(Session["ReturnUrl"].ToString());
            }

            string cvfr = tempTableRecord.MinisterId.ToString();

            MinisterProfile tempRecord = db2.MinisterProfiles
                .Where(c => c.MinisterCode == cvfr).FirstOrDefault();

            string cdert = tempTableRecord.DistrictId.ToString();

            @ViewBag.district = db2.Districts
                .Where(c => c.DistrictCode == cdert).First().DistrictName;
            @ViewBag.ministerCode = tempRecord.MinisterCode.ToString();
            @ViewBag.surname = tempRecord.Surname.ToString();
            @ViewBag.otherName = tempRecord.OtherName.ToString();

            return View(tempTableRecord);
        }

        [HttpPost, ActionName("DeleteTempTableRecord")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteTempTableRecordConfirmed(double id)
        {
            ContributionScheduleTable tempTableRecord = db2.ContributionScheduleTables.Find(id);

            try
            {
                db2.ContributionScheduleTables.Remove(tempTableRecord);
                db2.SaveChanges();
                TempData["status"] = " Entry deleted successfully!";
                return RedirectToAction("ContributionSchedule");
            }
            catch (AggregateException ex)
            {
                TempData["status"] = " " + ex.Message.ToString();
                return RedirectToAction("ContributionSchedule");
            }
        }
                
        public ActionResult CreateTempTableRecord(string SelectedDistrictId = "0",
            string SelectedMinisterId = "0", string year = "0", string month = "0")
        {
            if (TempData["status"] != null)
                ViewBag.info = TempData["status"];
            else ViewBag.info = " Here you can add a new Contribution Schedule after setting values";

            int SelectedMonthVal = 0;
            int SelectedYearVal = 0;

            if (year == "0" || year == "")
            {
                SelectedYearVal = 0;
            }
            if (month == "0" || month == "")
            {
                SelectedMonthVal = 0;
            }
            if (year != "0" && year != "")
            {
                SelectedYearVal = Convert.ToInt32(year);
            }
            if (month != "0" && month != "")
            {
                SelectedMonthVal = Convert.ToInt32(month);
            }

            //Check if year is already set
            if (SelectedYearVal == 0)
            {
                string yearValue;

                yearValue = db2.ContributionScheduleTables.Select(p => p.ScheduleYear).FirstOrDefault();

                if (yearValue == null)
                {
                    ViewBag.yearNull = true;

                    var yearList = db2.Years.Select(c => new { c.Id, c.YearValue }).OrderByDescending(p => p.Id);

                    string yy = yearList.First().YearValue;
                    ViewBag.year = yy;

                    int defaultYear = yearList.Where(c => c.YearValue.Equals(yy)).Select(c => c.Id).First();

                    Session["SelectedYear"] = defaultYear;

                    TempData["SelectYear"] = new SelectList(yearList.AsEnumerable(), "Id", "YearValue", defaultYear);
                }
                else
                {
                    ViewBag.year = yearValue;
                    Session["SelectedYear"] = db2.Years.Where(c => c.YearValue.Equals(yearValue)).Select(c => c.Id).First();
                    ViewBag.yearNull = false;
                }
            }

            //Check if month are already set
            if (SelectedMonthVal == 0)
            {
                string monthValue;

                monthValue = db2.ContributionScheduleTables.Select(p => p.ScheduleMonth).FirstOrDefault();

                if (monthValue == null)
                {
                    ViewBag.monthNull = true;

                    var monthList = db2.Months.Select(c => new { c.Id, c.MonthName }).OrderBy(p => p.Id);

                    int xx = DateTime.Now.Month;
                    
                    ViewBag.month = db2.Months.Where(p => p.Id.Equals(xx)).Select(p => p.MonthName).First().ToString();
                    
                    Session["SelectedMonth"] = xx;

                    TempData["SelectMonth"] = new SelectList(monthList.AsEnumerable(), "Id", "MonthName", xx);
                }
                else
                {
                    ViewBag.monthNull = false;
                    ViewBag.month = monthValue;
                    Session["SelectedMonth"] = db2.Months.Where(c => c.MonthName.Equals(monthValue)).Select(c => c.Id).First();
                }
            }

            if (SelectedYearVal > 0)
            {
                ViewBag.yearNull = true;

                var yearList = db2.Years.Select(c => new { c.Id, c.YearValue }).OrderByDescending(p => p.Id);

                ViewBag.year = yearList.Where(c => c.Id.Equals(SelectedYearVal)).Select(c => c.YearValue).First().ToString();

                Session["SelectedYear"] = SelectedYearVal;

                TempData["SelectYear"] = new SelectList(yearList.AsEnumerable(), "Id", "YearValue", SelectedYearVal);
            }

            if (SelectedMonthVal > 0)
            {
                ViewBag.monthNull = true;

                var monthList = db2.Months.Select(c => new { c.Id, c.MonthName }).OrderBy(p => p.Id);

                ViewBag.month = monthList.Where(p => p.Id.Equals(SelectedMonthVal)).Select(p => p.MonthName).First().ToString();

                Session["SelectedMonth"] = SelectedMonthVal;

                TempData["SelectMonth"] = new SelectList(monthList.AsEnumerable(), "Id", "MonthName", SelectedMonthVal);
            }

            //Cache the District DDL selected index and display it in the view
            int districtSelected;

            if (SelectedDistrictId.Equals("0") && Session["selected"] != null)
                districtSelected = Convert.ToInt32(Session["selected"]);
            else if (!SelectedDistrictId.Equals("0"))
                districtSelected = Convert.ToInt32(SelectedDistrictId);
            else
            {
                string cdr = db2.Districts.OrderBy(p => p.DistrictName).First().DistrictCode;
                districtSelected = Int32.Parse(cdr);
            }
            Session["selected"] = districtSelected;
            
            //Fetch the list of updated record and filter it from list of ministers
            var existingRecord = db2.ContributionScheduleTables.Where(m => m.DistrictId.Equals(districtSelected))
                .Select(c => new { c.MinisterId }).ToList();

            var newRecord = db2.MinisterProfiles.Where(m => m.DistrictId.Equals(districtSelected)).ToList();

            var newMinisterList = new List<MinisterProfile>();

            foreach (MinisterProfile item in newRecord)
            {
                bool exist = false;

                for (int i = 0; i < existingRecord.Count; i++)
                {
                    if (Int32.Parse(item.MinisterCode) == existingRecord[i].MinisterId) exist = true;
                }

                if (!exist)  newMinisterList.Add(item);
            }

            //Build a DDL for the new records
            var nameList = new List<MinisterNameDropDownDTO>();

            if (newMinisterList.Count.Equals(0)) ViewBag.IsEmpty = true;
            else ViewBag.IsEmpty = false;

            foreach (var item in newMinisterList)
            {
                var tempVal = new MinisterNameDropDownDTO();
                tempVal.Id = Int32.Parse(item.MinisterCode);
                tempVal.Name = item.Surname.ToUpper() + ", " + item.OtherName;
                nameList.Add(tempVal);
            }

            var ministerNameList = nameList.OrderBy(c => c.Name);

            //Cache the minister DDL selected index and display it in the view
            int ministerSelected;

            if (SelectedMinisterId.Equals("0"))// && 
            // (Session["selectedMinister"] == null || Session["selectedMinister"].Equals(0)))
            {
                if (nameList.Count == 0) ministerSelected = 0;
                else ministerSelected = ministerNameList.First().Id;
            }
            //else if (SelectedMinisterId.Equals("0") && Session["selectedMinister"] != null)
            //    ministerSelected = Convert.ToInt32(Session["selectedMinister"]);
            else   ministerSelected = Convert.ToInt32(SelectedMinisterId);

            // Session["selectedMinister"] = ministerSelected;

            TempData["MinisterNameSelectList"] = new SelectList(ministerNameList.AsEnumerable(), "Id", "Name", ministerSelected);

            ViewBag.DistrictId = districtSelected;
            ViewBag.MinisterId = ministerSelected;

            //Build a DDL for list of Districts
            var districtList = db2.Districts.Select(c => new { c.DistrictCode, c.DistrictName }).OrderBy(p => p.DistrictName);

            string cdf = districtSelected.ToString();

            ViewBag.districtSelect = districtList.Where(m => m.DistrictCode == cdf).First().DistrictName.ToString();

            TempData["DistrictSelectList"] = new SelectList(districtList.AsEnumerable(), "DistrictCode", "DistrictName", districtSelected);

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateTempTableRecord(ContributionScheduleTable contributionScheduleTab)
            //[Bind(Include = "Id,DistrictId,MinisterId,ChurchContribution,MinisterContribution,TotalContribution,ScheduleMonth,ScheduleYear")]            
        {
            if (ModelState.IsValid)
            {
                try
                {
                    db2.ContributionScheduleTables.Add(contributionScheduleTab);
                    db2.SaveChanges();
                    TempData["status"] = " New entry successfully added, add more entires below!";
                    return RedirectToAction("CreateTempTableRecord");
                }
                catch (AggregateException e)
                {
                    TempData["status"] = e.Message.ToString();        
                    return RedirectToAction("CreateTempTableRecord");
                }
            }

            TempData["status"] = " An error has occurred, please enter valid values and try again";
            return RedirectToAction("CreateTempTableRecord");
        }

        public ActionResult DeleteAllTableRecord()
        {
            //check if checklist has been posted first before confirming delete
            var currentMonth = db2.ContributionScheduleTables.Select(p => p.ScheduleMonth).FirstOrDefault();
            var currentYear = db2.ContributionScheduleTables.Select(p => p.ScheduleYear).FirstOrDefault();

            var isPosted = db2.ReconciliationSummarys.Where(p => p.PayMonth.Equals(currentMonth) && p.PayYear.Equals(currentYear)).FirstOrDefault();
            if (isPosted == null)
            {
                TempData["status"] = " Contribution Schedule cannot be deleted because Checklist has not been posted!";
                return RedirectToAction("ContributionSchedule");
            }

            var isDeleted = db2.ContributionScheduleTables.FirstOrDefault();
            if (isDeleted == null)
            {
                TempData["status"] = " Contributions Schedule already emptied! Please select Year and Month to start new Schedule!";
                return RedirectToAction("CreateTempTableRecord");
            }

            var allData = db2.ContributionScheduleTables.ToList();
            db2.ContributionScheduleTables.RemoveRange(allData);
            db2.SaveChanges();

            TempData["status"] = " Contributions Schedule emptied! Please select Year and Month to start new Schedule.";
            return RedirectToAction("CreateTempTableRecord");
        }
        
        public ActionResult PostSchedule()
        {
            //cache the session month and year
            var currentMonth = db2.ContributionScheduleTables.Select(p => p.ScheduleMonth).FirstOrDefault();
            var currentYear = db2.ContributionScheduleTables.Select(p => p.ScheduleYear).FirstOrDefault();

            //check whether you can post
            var isPosted = db2.ReconciliationSummarys.Where(p => p.PayMonth.Equals(currentMonth) && p.PayYear.Equals(currentYear)).FirstOrDefault();
            if (isPosted != null)
            {
                TempData["status"] = " Checklist has been posted and cannot be re-posted! Please empty Contribution Schedule and start a new one";
                return RedirectToAction("ContributionSchedule");
            }
                        
            var isChecklistPosted = db2.ContributionCheckListTables.Where(p => p.PayMonth.Equals(currentMonth) && p.PayYear.Equals(currentYear)).FirstOrDefault();
            if (isChecklistPosted != null)
            {
                var allData = db2.ContributionCheckListTables.Where(c => c.PayMonth.Equals(currentMonth) && c.PayYear.Equals(currentYear)).ToList();
                db2.ContributionCheckListTables.RemoveRange(allData);
                db2.SaveChanges();
            }

            var contribChecklist = new List<ContributionCheckListTable>();

            foreach (var contribSchedule in db2.ContributionScheduleTables)
            {
                string sxee = contribSchedule.DistrictId.ToString();

                string aswq = contribSchedule.MinisterId.ToString();

                var ministerProf = db2.MinisterProfiles.Where(c => c.MinisterCode == aswq).First();
                var distProf = db2.Districts.Where(c => c.DistrictCode == sxee).First();

                var checklist = new ContributionCheckListTable();

                checklist.DistrictId = contribSchedule.DistrictId;
                checklist.MinisterId = contribSchedule.MinisterId;
                checklist.DistrictName = distProf.DistrictName;
                checklist.MinisterCode = ministerProf.MinisterCode;
                checklist.MinisterName = ministerProf.Surname + ", " + ministerProf.OtherName;
                checklist.ChurchContribution = contribSchedule.ChurchContribution;
                checklist.MinisterContribution = contribSchedule.MinisterContribution;
                checklist.ServiceContribution = Math.Round(contribSchedule.ChurchContribution * 0.02, 2);
                checklist.TotalContribution = Math.Round(contribSchedule.ChurchContribution + contribSchedule.MinisterContribution, 2);
                checklist.NetContribution = Math.Round((contribSchedule.ChurchContribution + contribSchedule.MinisterContribution)
                    - (contribSchedule.ChurchContribution * 0.02), 2);
                checklist.PayMonth = contribSchedule.ScheduleMonth;
                checklist.PayYear = contribSchedule.ScheduleYear;

                contribChecklist.Add(checklist);
            }

            db2.ContributionCheckListTables.AddRange(contribChecklist);
            db2.SaveChanges();
            TempData["status"] = " Contribution Schedule successfully posted!";

            var newYear = contribChecklist.FirstOrDefault().PayYear;
            Session["SelectedYear"] = db2.Years.Where(c => c.YearValue.Equals(newYear)).Select(c => c.Id).First();

            var newMonth = contribChecklist.FirstOrDefault().PayMonth;
            Session["SelectedMonth"] = db2.Months.Where(c => c.MonthName.Equals(newMonth)).Select(c => c.Id).First();

            return RedirectToAction("ContributionChecklist");
        }

        #endregion
                            
        #region Manage Contribution Checklist.

        public ActionResult ContributionChecklist(
            string SelectedDistrictID = "0", string YearID = "0", string MonthID = "0")
        {
            if (TempData["status"] == null)
                @ViewBag.info = " Here you can download reports based on District selected.";
            else
                @ViewBag.info = TempData["status"];

            int yearValue;
            string YearValue;
            var yearList = db2.Years.Select(c => new { c.Id, c.YearValue }).OrderByDescending(p => p.Id);

            if (YearID.Equals("0") && Session["SelectedYear"] == null)            
                yearValue = yearList.Select(c => c.Id).First();            
            else if (YearID.Equals("0") && Session["SelectedYear"] != null)
                yearValue = Convert.ToInt32(Session["SelectedYear"]);
            else
                yearValue = Convert.ToInt32(YearID);

            Session["SelectedYear"] = yearValue;

            YearValue = yearList.Where(c => c.Id.Equals(yearValue)).Select(c => c.YearValue).First().ToString();

            TempData["SelectYearDDL"] = new SelectList(yearList.AsEnumerable(), "Id", "YearValue", yearValue);
            
            int monthValue;
            string MonthValue;
            if (MonthID.Equals("0") && Session["SelectedMonth"] == null)
                monthValue = DateTime.Now.Month;
            else if (MonthID.Equals("0") && Session["SelectedMonth"] != null)
                monthValue = Convert.ToInt32(Session["SelectedMonth"]);
            else
                monthValue = Convert.ToInt32(MonthID);

            Session["SelectedMonth"] = monthValue;

            var monthList = db2.Months.Select(c => new { c.Id, c.MonthName }).OrderBy(p => p.Id).ToList();
            MonthValue = monthList.Where(c => c.Id.Equals(monthValue)).Select(p => p.MonthName).First().ToString();

            TempData["SelectMonthDDL"] = new SelectList(monthList.AsEnumerable(), "Id", "MonthName", monthValue);
            
            //Cache and retrieve last selected District
            var districtList = db2.Districts.Select(c => new { c.DistrictCode, c.DistrictName }).OrderBy(p => p.DistrictName);
            int SelectedDistrict;

            if (SelectedDistrictID.Equals("0") && Session["SelectedDistrict"] == null)
                SelectedDistrict = Int32.Parse(districtList.First().DistrictCode);
            else if (SelectedDistrictID.Equals("0") && Session["SelectedDistrict"] != null)
                SelectedDistrict = Convert.ToInt32(Session["SelectedDistrict"]);
            else
                SelectedDistrict = Convert.ToInt32(SelectedDistrictID);

            Session["SelectedDistrict"] = SelectedDistrict;

            TempData["SelectDistrictDDL"] = new SelectList(districtList.AsEnumerable(), "DistrictCode", "DistrictName", SelectedDistrict);
            
            var tempDbTable = db2.ContributionCheckListTables
                .Where(c => c.DistrictId.Equals(SelectedDistrict) && c.PayYear.Equals(YearValue)
                    && c.PayMonth.Equals(MonthValue)).ToList();          

            if (tempDbTable.Count == 0) ViewBag.IsEmpty = true;
            else ViewBag.IsEmpty = false;

            return View(tempDbTable.OrderBy(c => c.MinisterCode));
        }
        
        public ActionResult EditChecklist(double? id)
        {
            ViewBag.info = " Here you can modify the Church and Minister Contributions before final posting";

            if (id == null)
            {
                TempData["status"] = " Error! Checklist record nolonger exist.";
                return Redirect(Session["ReturnUrl"].ToString());
            }

            var tempChecklist = db2.ContributionCheckListTables.Find(id);

            if (tempChecklist == null)
            {
                TempData["status"] = " Error! Checklist record nolonger exist.";
                return Redirect(Session["ReturnUrl"].ToString());
            }

            ViewBag.districtName = tempChecklist.DistrictName;
            ViewBag.ministerCode = tempChecklist.MinisterCode;
            ViewBag.ministerName = tempChecklist.MinisterName;

            return View(tempChecklist);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditChecklist(ContributionCheckListTable contributionChecklistTab)
            //[Bind(Include ="Id,DistrictId,MinisterId,DistrictName,MinisterCode,MinisterName,ChurchContribution,MinisterContribution,ServiceContribution,TotalContribution,NetContribution,PayMonth,PayYear")]            
        {
            if (ModelState.IsValid)
            {
                db2.Entry(contributionChecklistTab).State = EntityState.Modified;
                db2.SaveChanges();

                TempData["status"] = " Entry successfully updated!";
                return Redirect(Session["ReturnUrl"].ToString());
            }
            TempData["status"] = " Error! Please check your values and try again.";
            return View(contributionChecklistTab);
        }

        public ActionResult PostChecklist()
        {
            var currentMonth = db2.ContributionScheduleTables.Select(p => p.ScheduleMonth).FirstOrDefault();
            var currentYear = db2.ContributionScheduleTables.Select(p => p.ScheduleYear).FirstOrDefault();

            //check whether selected schedule has been posted already
            var isPosted = db2.ReconciliationSummarys
                .Where(p => p.PayMonth.Equals(currentMonth) && p.PayYear.Equals(currentYear)).FirstOrDefault();
            if (isPosted != null)
            {
                TempData["status"] = " Checklist has been posted and cannot be modified! Please start a new Schedule";
                return RedirectToAction("ContributionChecklist");
            }

            var reconcil = new List<ReconciliationSummary>();
            var distListID = db2.Districts.Select(c => c.DistrictCode).ToList();
            string prevMonth;

            //Get Previous Month and Year Value
            int currentMonthId = db2.Months.Where(c => c.MonthName.Equals(currentMonth)).Select(c => c.Id).First();
            int prevMonthId = currentMonthId - 1;
            string yearValue;

            if (prevMonthId == 0)
            {
                prevMonth = db2.Months.Where(c => c.Id.Equals(12)).Select(c => c.MonthName).First();
                int currentYearId2 = Int32.Parse(currentYear);
                yearValue = (currentYearId2 - 1).ToString();
            }
            else
            {
                prevMonth = db2.Months.Where(c => c.Id.Equals(prevMonthId)).Select(c => c.MonthName).First();
                yearValue = currentYear;
            }
            //Get Reconcile cummulative for previous month/year
            ReconciliationSummary preMonBal;
            
            //Compute Reconciliation Summary first
            try
            {
                foreach (var districtId in distListID)
                {
                    int sder = Int32.Parse(districtId);

                    preMonBal = db2.ReconciliationSummarys
                       .Where(c => c.DistrictId == sder && c.PayMonth.Equals(prevMonth) && c.PayYear.Equals(yearValue))
                       .FirstOrDefault();

                    if (preMonBal == null)
                    {
                        // create a recursive  formular that gets the last reconcilliation that was posted
                        preMonBal = new ReconciliationSummary();
                    }

                    var allData = db2.ContributionCheckListTables
                        .Where(c => c.DistrictId == sder && c.PayMonth.Equals(currentMonth)
                            && c.PayYear.Equals(currentYear)).ToList();

                    var reconcilInstance = new ReconciliationSummary();

                    string seww = districtId.ToString();

                    var districtInstance = db2.Districts.Where(m => m.DistrictCode == seww).FirstOrDefault();

                    //for case of empty districts: no contribution submitted for the month
                    if (allData.Count == 0)
                    {
                        reconcilInstance.DistrictId = Int32.Parse(districtId);
                        reconcilInstance.DistrictCode = districtInstance.DistrictCode;
                        reconcilInstance.DistrictName = districtInstance.DistrictName;
                        reconcilInstance.Remittance = 0.0;
                        reconcilInstance.Schedule = 0.0;
                        reconcilInstance.ServiceCharge = 0.0;
                        reconcilInstance.NetSchedule = 0.0;
                        reconcilInstance.Debit = preMonBal.Debit;// 0.0;
                        reconcilInstance.Credit = preMonBal.Credit;// 0.0;
                        reconcilInstance.Balance = preMonBal.Balance;// 0.0;

                        reconcilInstance.UserPosted = "Admin";
                        reconcilInstance.PayMonth = currentMonth;
                        reconcilInstance.PayYear = currentYear;
                        reconcilInstance.DatePosted = DateTime.Today.ToLongDateString();
                    }
                    else
                    {
                        double scheduleValue = 0.0;
                        double servCharValue = 0.0;

                        reconcilInstance.DistrictId = Int32.Parse(districtId);
                        reconcilInstance.DistrictCode = districtInstance.DistrictCode;
                        reconcilInstance.DistrictName = districtInstance.DistrictName;

                        reconcilInstance.Remittance = 0.0; //check remittance existence

                        var totalSched = allData.Select(m => m.TotalContribution).FirstOrDefault();
                        if (totalSched != null && totalSched != 0) 
                            scheduleValue = Math.Round(allData.Select(m => m.TotalContribution).Sum(), 2);
                        reconcilInstance.Schedule = scheduleValue;
                        
                        var servCharge = allData.Select(m => m.ServiceContribution).FirstOrDefault();
                        if (servCharge != null && servCharge != 0)
                            servCharValue = Math.Round(allData.Select(m => m.ServiceContribution).Sum(), 2);
                        reconcilInstance.ServiceCharge = servCharValue;
                        
                        reconcilInstance.NetSchedule = Math.Round((scheduleValue-servCharValue), 2);

                        double bal = 0 - scheduleValue;

                        double lastMonthDeb = preMonBal.Debit;                                                
                        double lastMonthCred = preMonBal.Credit;
                        double lastMonthBal = preMonBal.Balance;

                        if(lastMonthDeb<0 || lastMonthBal==0)
                        {
                            double variation =bal + lastMonthDeb;
                          reconcilInstance.Debit = variation;
                            reconcilInstance.Credit = 0.0;
                            reconcilInstance.Balance = variation;
                        }
                        if (lastMonthCred > 0)
                        {
                            double variation = bal + lastMonthCred;
                            if (variation > 0)
                            {
                                reconcilInstance.Debit = 0.0;
                                reconcilInstance.Credit = variation;
                                reconcilInstance.Balance = variation;
                            }
                            else if (variation < 0)
                            {
                                reconcilInstance.Debit = variation;
                                reconcilInstance.Credit = 0.0;
                                reconcilInstance.Balance = variation;
                            }
                            else
                            {
                                reconcilInstance.Debit = 0.0;
                                reconcilInstance.Credit = 0.0;
                                reconcilInstance.Balance = 0.0;
                            }
                        }

                        reconcilInstance.UserPosted = "Admin";
                        reconcilInstance.PayMonth = currentMonth;
                        reconcilInstance.PayYear = currentYear;
                        reconcilInstance.DatePosted = DateTime.Today.ToLongDateString();
                    }
                    reconcil.Add(reconcilInstance);
                }
                db2.ReconciliationSummarys.AddRange(reconcil);
                db2.SaveChanges();
            }
            catch (AggregateException e)
            {
                TempData["status"] = " The system encountered an error!\n" + e.Message;

                Session["SelectedYear"] = db2.Years.Where(c => c.YearValue.Equals(currentYear)).Select(c => c.Id).First();
                Session["SelectedMonth"] = db2.Months.Where(c => c.MonthName.Equals(currentMonth)).Select(c => c.Id).First();

                return RedirectToAction("ContributionChecklist");
            }

            //Update Account Statement
            var acctState = new List<AccountRecord>();
            var allMinisters = db2.ContributionCheckListTables
                .Where(c => c.PayYear.Equals(currentYear) && c.PayMonth.Equals(currentMonth)).ToList();

            //****
            //check if there is record for previous months and nullify if none before logging for present month
            try
            {
                foreach (var minInst in allMinisters)
                {
                    var acctInstance = new AccountRecord();

                    acctInstance.DistrictId = minInst.DistrictId;
                    acctInstance.MinisterId = minInst.MinisterId;
                    acctInstance.District = minInst.DistrictName;
                    acctInstance.MinisterCode = minInst.MinisterCode;
                    acctInstance.FullName = minInst.MinisterName;
                    acctInstance.AccountNo = minInst.MinisterCode; //Account no
                    acctInstance.Debit = 0.00;//Debit
                    acctInstance.Minister = minInst.MinisterContribution;
                    acctInstance.Church = minInst.ChurchContribution;
                    acctInstance.Total = minInst.TotalContribution;

                    var prevMonMinister = db2.AccountRecords
                .Where(c => c.Year.Equals(currentYear) && c.MonthId.Equals(prevMonthId)
                    && c.MinisterCode.Equals(minInst.MinisterCode)).FirstOrDefault();

                    //if he submits 3 months ago nko?
                    if (prevMonMinister == null) acctInstance.NetBal = minInst.TotalContribution;
                    else acctInstance.NetBal = minInst.TotalContribution + prevMonMinister.NetBal;
                    //acctInstance.NetBal = minInst.NetContribution;

                    acctInstance.Month = minInst.PayMonth;
                    acctInstance.MonthId = currentMonthId;// db2.Months.Where(c => c.MonthName.Equals(minInst.PayMonth)).Select(c => c.Id).First();
                    acctInstance.Year = minInst.PayYear;
                    acctInstance.PostDate = DateTime.Now;

                    acctState.Add(acctInstance);
                }
                db2.AccountRecords.AddRange(acctState);
                db2.SaveChanges();
            }
            catch (AggregateException e)
            {
                TempData["status"] = " The system encountered an error!\n" + e.Message;

                Session["SelectedYear"] = db2.Years.Where(c => c.YearValue.Equals(currentYear)).Select(c => c.Id).First();
                Session["SelectedMonth"] = db2.Months.Where(c => c.MonthName.Equals(currentMonth)).Select(c => c.Id).First();

                return RedirectToAction("ContributionChecklist");
            }

            //Opening Balance for Next Year
            if (currentMonthId == 12)
            {
                int currentYearId = Int32.Parse(currentYear);
                int currYearId = db2.Years.Where(c => c.YearValue.Equals(currentYear)).First().Id;
                string nextYearValue = (currentYearId + 1).ToString();

                var newYear = new Year();
                newYear.Id = currYearId + 1;
                newYear.YearValue = nextYearValue;

                if (ModelState.IsValid)
                {
                    db2.Years.Add(newYear);
                    db2.SaveChanges();
                }

                var openingAcctStates = new List<AccountRecord>();
                var decMinistersAcct = db2.AccountRecords.Where(c => c.Year.Equals(currentYear) && c.Month.Equals(currentMonth)).ToList();

                try
                {
                    foreach (var newAcctInst in decMinistersAcct)
                    {
                        var acctInstance = new AccountRecord();

                        acctInstance.DistrictId = newAcctInst.DistrictId;
                        acctInstance.MinisterId = newAcctInst.MinisterId;
                        acctInstance.District = newAcctInst.District;
                        acctInstance.MinisterCode = newAcctInst.MinisterCode;
                        acctInstance.FullName = newAcctInst.FullName;
                        acctInstance.AccountNo = newAcctInst.MinisterCode; //Account no
                        acctInstance.Debit = 0.00;//Debit
                        acctInstance.Minister = newAcctInst.Minister;
                        acctInstance.Church = newAcctInst.Church;
                        acctInstance.Total = newAcctInst.Total;

                        //    var prevMonMinister = db2.ContributionCheckListTables
                        //.Where(c => c.PayYear.Equals(currentYear) && c.PayMonth.Equals(prevMonth)
                        //    && c.MinisterCode.Equals(minInst.MinisterCode)).FirstOrDefault();

                        //if he submits 3 months ago nko?
                        if (newAcctInst.NetBal == null || newAcctInst.NetBal == 0)
                            acctInstance.NetBal = newAcctInst.Total;
                        else acctInstance.NetBal = newAcctInst.NetBal;
                        //acctInstance.NetBal = minInst.NetContribution;

                        acctInstance.Month = "Opening Balance";
                        acctInstance.MonthId = 0;
                        acctInstance.Year = nextYearValue;
                        acctInstance.PostDate = DateTime.Now;

                        openingAcctStates.Add(acctInstance);
                    }
                    db2.AccountRecords.AddRange(openingAcctStates);
                    db2.SaveChanges();
                }
                catch (AggregateException e)
                {
                    TempData["status"] = " The system encountered an error!\n" + e.Message;

                    Session["SelectedYear"] = db2.Years.Where(c => c.YearValue.Equals(currentYear)).Select(c => c.Id).First();
                    Session["SelectedMonth"] = db2.Months.Where(c => c.MonthName.Equals(currentMonth)).Select(c => c.Id).First();

                    return RedirectToAction("ContributionChecklist");
                }
            }

            TempData["status"] = " Contribution Checklist successfully posted and cannot be modified anymore!";

            Session["SelectedYear"] = db2.Years.Where(c => c.YearValue.Equals(currentYear)).Select(c => c.Id).First();
            Session["SelectedMonth"] = db2.Months.Where(c => c.MonthName.Equals(currentMonth)).Select(c => c.Id).First();

            return RedirectToAction("ReconciliationReport"); //reconciliation action
        }

        #endregion
        
        #region Reports.


        [HttpPost]
        public ActionResult Report(string id, List<string> selc)
        {
            LocalReport lr = new LocalReport();
            string path = Path.Combine(Server.MapPath("~/RDLC_Reports"), "PaidOffMinisters.rdlc");
            if (System.IO.File.Exists(path))
            {
                lr.ReportPath = path;
            }
            else
            {
                return View("Index");
            }
            List<MinisterProfile> cm = new List<MinisterProfile>();

            List<AccountRecord> cm2 = new List<AccountRecord>();
            

            //chkBox = Request["selc"];
            var selectedCode = new HashSet<string>(selc);

                //cm = db2.MinisterProfiles.ToList();
                cm = db2.MinisterProfiles.Where(s => selectedCode.Contains(s.MinisterCode)).ToList();

                cm2 = db2.AccountRecords.Where(s => selectedCode.Contains(s.MinisterCode)).ToList();
                //var v = from t in db2.MinisterProfiles.Cast<MinisterProfile>() join t2 in db2.AccountRecords.Cast<AccountRecord>() on t.MinisterCode equals t2.MinisterCode where selectedCode.Contains(t.MinisterCode) select t;


                ReportDataSource rd = new ReportDataSource("PaidOffMinisters", cm);
                ReportDataSource rd2 = new ReportDataSource("DataSet1", cm2);
            lr.DataSources.Add(rd);
            
            lr.DataSources.Add(rd2);
            string reportType = id;
            string mimeType;
            string encoding;
            string fileNameExtension;



            string deviceInfo = "<DeviceInfo>" +
                "  <OutputFormat>PDF</OutputFormat>" +
                "  <PageWidth>29.7cm</PageWidth>" +
                "  <PageHeight>21cm</PageHeight>" +
                "  <MarginTop>0.05cm</MarginTop>" +
                "  <MarginLeft>0.1cm</MarginLeft>" +
                "  <MarginRight>0.1cm</MarginRight>" +
                "  <MarginBottom>0.05cm</MarginBottom>" +
                "</DeviceInfo>";

            Warning[] warnings;
            string[] streams;
            byte[] renderedBytes;

            renderedBytes = lr.Render(
                reportType,
                deviceInfo,
                out mimeType,
                out encoding,
                out fileNameExtension,
                out streams,
                out warnings);


            return File(renderedBytes, mimeType);
        }


            



        public ActionResult ReconciliationReport(string YearID = "0", string MonthID = "0")
        {
            if (TempData["status"] == null)
                @ViewBag.info = " Here you can view the latest Reconciliation Summary Report generated.";
            else
                @ViewBag.info = TempData["status"];

            int yearValue;
            string YearValue;
            var yearList = db2.Years.Select(c => new { c.Id, c.YearValue }).OrderByDescending(p => p.Id);
            if (YearID.Equals("0") && Session["SelectedYear"] == null)
                yearValue = yearList.Select(c => c.Id).First();
            else if (YearID.Equals("0") && Session["SelectedYear"] != null)
                yearValue = Convert.ToInt32(Session["SelectedYear"]);
            else
                yearValue = Convert.ToInt32(YearID);

            Session["SelectedYear"] = yearValue;

            YearValue = yearList.Where(c => c.Id.Equals(yearValue)).Select(c => c.YearValue).First().ToString();

            TempData["SelectYearDDL"] = new SelectList(yearList.AsEnumerable(), "Id", "YearValue", yearValue);

            int monthValue;
            string MonthValue;
            if (MonthID.Equals("0") && Session["SelectedMonth"] == null)
                monthValue = DateTime.Now.Month;
            else if (MonthID.Equals("0") && Session["SelectedMonth"] != null)
                monthValue = Convert.ToInt32(Session["SelectedMonth"]);
            else
                monthValue = Convert.ToInt32(MonthID);

            Session["SelectedMonth"] = monthValue;

            var monthList = db2.Months.Select(c => new { c.Id, c.MonthName }).OrderBy(p => p.Id).ToList();
            MonthValue = monthList.Where(c => c.Id.Equals(monthValue)).Select(p => p.MonthName).First().ToString();

            TempData["SelectMonthDDL"] =  new SelectList(monthList.AsEnumerable(), "Id", "MonthName", monthValue);
                    
            var tempDbTable = db2.ReconciliationSummarys.Where(c => c.PayYear.Equals(YearValue) && c.PayMonth.Equals(MonthValue)).ToList();

            //check wheda later month has been posted
            int currentMonthId = db2.Months.Where(c => c.MonthName.Equals(MonthValue)).Select(c => c.Id).First();
            ReconciliationSummary newRecord;
            string nextMonth;// = db2.Months.Where(c => c.Id.Equals(nextMonthId)).Select(c => c.MonthName).First();

            if (currentMonthId == 12)
            {
                nextMonth = db2.Months.Where(c => c.Id.Equals(1)).Select(c => c.MonthName).First();

                int nextYearId = Int32.Parse(YearValue);
                string nextYearStr = nextYearId.ToString();
                int currYearId2 = db2.Years.Where(c => c.YearValue == nextYearStr).First().Id;
                string nextYearValue = (currYearId2 + 1).ToString();

                newRecord = db2.ReconciliationSummarys
                   .Where(c => c.PayYear.Equals(nextYearValue) && c.PayMonth.Equals(nextMonth))
                   .FirstOrDefault();
            }
            else
            {
                nextMonth = db2.Months.Where(c => c.Id.Equals(currentMonthId + 1)).Select(c => c.MonthName).First();

                newRecord = db2.ReconciliationSummarys
                    .Where(c => c.PayYear.Equals(YearValue) && c.PayMonth.Equals(nextMonth))
                    .FirstOrDefault();
            }

            if (newRecord == null)
                ViewBag.OldRecon = "FALSE";
            else
                ViewBag.OldRecon = "TRUE";

            if (tempDbTable.Count == 0)
            {
                ViewBag.IsEmpty = true;
                ViewBag.UserPosted = "N/A";
                ViewBag.DatePosted = "N/A";
            }
            else
            {
                ViewBag.IsEmpty = false;
                ViewBag.UserPosted = tempDbTable.Select(v => v.UserPosted).FirstOrDefault();
                ViewBag.DatePosted = tempDbTable.Select(v => v.DatePosted).FirstOrDefault();
            }

            return View(tempDbTable.OrderBy(c => c.DistrictName));
        }
                
        //Select year range for reconciliation
        public ActionResult EditReconciliation(double? id)
        {
            ViewBag.info = " Here you can only modify the Remittance value.";

            if (id == null)
            {
                TempData["status"] = " Error! District record no longer exist.";
                return Redirect(Session["ReturnUrl"].ToString());
            }

            var tempRecon = db2.ReconciliationSummarys.Find(id);

            if (tempRecon == null)
            {
                TempData["status"] = " Error! District record no longer exist.";
                return Redirect(Session["ReturnUrl"].ToString());
            }
            
            return View(tempRecon);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditReconciliation(ReconciliationSummary reconciliationInstance)
            //[Bind(Include ="Id,DistrictId,DistrictCode,DistrictName,Remittance,Schedule,ServiceCharge,NetSchedule,Debit,Credit,Balance,PayMonth,PayYear,DatePosted,UserPosted")]            
        {
            if (ModelState.IsValid)
            {
                db2.Entry(reconciliationInstance).State = EntityState.Modified;
                db2.SaveChanges();

                TempData["status"] = " Entry successfully updated!";
                return Redirect(Session["ReturnUrl"].ToString());
            }
            TempData["status"] = " Error! Please check values and try again";
            return View(reconciliationInstance);
        }

        public ActionResult AccountStatement(string SelectedDistrictId = "0",
            string SelectedMinisterId = "0", string SelectedYear = "0")
        {
            if (TempData["status"] == null)
                @ViewBag.info = " Here you can export and print an Account Statement of a selected Minister.";
            else @ViewBag.info = TempData["status"];

            int SelectedYearVal = 0;
            int SelectedDistrictVal = 0;

            if (SelectedYear == "0" || SelectedYear == "") SelectedYearVal = 0;

            if (SelectedYear != "0" && SelectedYear != "") SelectedYearVal = Convert.ToInt32(SelectedYear);

            if (SelectedDistrictId == "0" || SelectedDistrictId == "") 
                SelectedDistrictVal = 0;
            if (SelectedDistrictId != "0" && SelectedDistrictId != "")
                SelectedDistrictVal = Convert.ToInt32(SelectedDistrictId);

            int choseYear = 0, choseDistrict = 0;

            if (SelectedYearVal == 0)
            {
                var yearList = db2.Years.Select(c => new { c.Id, c.YearValue }).OrderByDescending(p => p.Id);
                choseYear = yearList.First().Id;

                TempData["SelectYear"] = new SelectList(yearList.AsEnumerable(), "Id", "YearValue", choseYear);
            }

            if (SelectedDistrictVal == 0)
            {
                var districtList = db2.Districts.Select(c => new { c.DistrictCode, c.DistrictName }).OrderBy(p => p.DistrictName);
                choseDistrict = Int32.Parse(districtList.First().DistrictCode);

                TempData["DistrictSelectList"] = new SelectList(districtList.AsEnumerable(), "DistrictCode", "DistrictName", choseDistrict);
            }

            if (SelectedYearVal > 0)
            {
                choseYear = SelectedYearVal;
                var yearList = db2.Years.Select(c => new { c.Id, c.YearValue }).OrderByDescending(p => p.Id);
                TempData["SelectYear"] = new SelectList(yearList.AsEnumerable(), "Id", "YearValue", choseYear);
            }

            if (SelectedDistrictVal > 0)
            {
                choseDistrict = SelectedDistrictVal;
                var districtList = db2.Districts.Select(c => new { c.DistrictCode, c.DistrictName }).OrderBy(p => p.DistrictName);
                TempData["DistrictSelectList"] = new SelectList(districtList.AsEnumerable(), "DistrictCode", "DistrictName", choseDistrict);
            }

            var ministerList = db2.MinisterProfiles.Where(c => c.DistrictId.Equals(choseDistrict)).ToList();
            var nameList = new List<MinisterNameDropDownDTO>();

            foreach (var item in ministerList)
            {
                var tempVal = new MinisterNameDropDownDTO();
                tempVal.Id = Int32.Parse(item.MinisterCode);
                tempVal.Name = item.Surname.ToUpper() + ", " + item.OtherName;
                nameList.Add(tempVal);
            }
            var ministerNameList = nameList.OrderBy(c => c.Name);

            int ministerSelected;
            if (SelectedMinisterId == "0" || SelectedMinisterId == "")
                ministerSelected = ministerNameList.First().Id;

            else ministerSelected = Convert.ToInt32(SelectedMinisterId);

            TempData["MinisterNameSelectList"] = new SelectList(ministerNameList.AsEnumerable(), "Id", "Name", ministerSelected);

            var yearName = db2.Years.Where(c => c.Id.Equals(choseYear)).Select(c => c.YearValue).First();
            var accountState = db2.AccountRecords
                .Where(c => c.MinisterId.Equals(ministerSelected) && c.Year.Equals(yearName) && c.DistrictId.Equals(choseDistrict))
                .ToList();

            Session["YearString"] = yearName;
            Session["MinisterId"] = ministerSelected;
            Session["DistrictId"] = choseDistrict;

            if (accountState.Count == 0 || accountState.Count == null)
                ViewBag.IsEmpty = "True";
            else
                ViewBag.IsEmpty = "False";

            var dividend = accountState.Where(c => c.MonthId == 13).FirstOrDefault();
            if (dividend == null)            
                ViewBag.IsDiv = "False";
                //TempData["YearV"] = choseYear;
            
            else
                ViewBag.IsDiv = "True";

            return View(accountState.OrderBy(c => c.MonthId));
        }

        public ActionResult EnterDividend()
        {
            if (TempData["status"] == null)
                @ViewBag.info = " Here you can enter the Dividend value you want to share among the Ministers.";
            else @ViewBag.info = TempData["status"];
                        
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EnterDividend(string txtDiv)
        {
            double divdendVal;
            bool success = double.TryParse(txtDiv, out divdendVal);

            if (success && divdendVal > 0)
            {
                string divYear = Session["YearString"].ToString();

                var ministersAcct1 = db2.AccountRecords.Where(c => c.Year == divYear && c.MonthId==12).FirstOrDefault();//.ToList();

                if(ministersAcct1 == null)
                {
                    TempData["status"] = " Checklist has not been posted for December, " + divYear +"! Please make sure the Year is correct and Post Checklist for December before entering dividend";
                    return RedirectToAction("EnterDividend");
                }

                var ministersAcct = db2.AccountRecords.Where(c => c.Year == divYear && c.MonthId == 12).ToList();
                double totalMinister = ministersAcct.Select(m => m.Minister).Sum();

                double dividendMinister = divdendVal / totalMinister;

                var allAcctState = new List<AccountRecord>();
                try
                {
                    foreach (var ministerInstance in ministersAcct)
                    {
                        var acctInstance = new AccountRecord();
                        double minDiv = Math.Round((dividendMinister * ministerInstance.Minister), 2);

                        acctInstance.DistrictId = ministerInstance.DistrictId;
                        acctInstance.MinisterId = ministerInstance.MinisterId;
                        acctInstance.District = ministerInstance.District;
                        acctInstance.MinisterCode = ministerInstance.MinisterCode;
                        acctInstance.FullName = ministerInstance.FullName;
                        acctInstance.AccountNo = ministerInstance.MinisterCode; //Account no
                        acctInstance.Debit = 0.00;//Debit
                        acctInstance.Minister = minDiv;
                        acctInstance.Church = 0.00;
                        acctInstance.Total = minDiv;
                        acctInstance.NetBal = ministerInstance.NetBal + minDiv;

                        acctInstance.Month = "Dividend";
                        acctInstance.MonthId = 13;
                        acctInstance.Year = divYear;
                        acctInstance.PostDate = DateTime.Now;

                        allAcctState.Add(acctInstance);
                    }
                    db2.AccountRecords.AddRange(allAcctState);
                    db2.SaveChanges();
                }
                catch (AggregateException e)
                {
                    TempData["status"] = " The system encountered an error!\n" + e.Message;
                    return RedirectToAction("EnterDividend");
                }

                TempData["status"] = " Dividend value of =N=" + txtDiv + " was successfully shared among Ministers!";
                return RedirectToAction("AccountStatement");
            }
            else
            {
                TempData["status"] = "Incorrect input! Please enter figures only and try again.";
                return RedirectToAction("EnterDividend");
            }
        }

        public ActionResult OpeningBalance(double? id)
        {
            if (id == null)
            {
                TempData["status"] = " Error! Minister Account no longer exist.";
                return Redirect(Session["ReturnUrl"].ToString());
            }

            var openingAcct = db2.AccountRecords
                .Where(c => c.MinisterId.Equals((int)id) && c.MonthId.Equals(0)).FirstOrDefault();

            if (openingAcct != null)
            {
                TempData["status"] = " You have entered an Opening Balance for " + openingAcct.FullName + " already! Please select another Minister.";
                return RedirectToAction("AccountStatement");
            }

            var accountInstance = db2.AccountRecords.Where(c => c.MinisterId.Equals((int)id)).First();

            if (accountInstance == null)
            {
                TempData["status"] = " Error! Minister Account no longer exist.";
                return Redirect(Session["ReturnUrl"].ToString());
            }

            if (TempData["status"] == null)
                @ViewBag.info = " Here you can enter the Opening Balance for " + accountInstance.FullName + ".";
            else @ViewBag.info = TempData["status"];

            @ViewBag.Fullname = accountInstance.FullName;
            @ViewBag.MinisterCode = accountInstance.MinisterCode;
            @ViewBag.AccountNo = accountInstance.AccountNo;

            return View(accountInstance);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult OpeningBalance(AccountRecord acctRecordInstance)
            //[Bind(Include ="Id,DistrictId,MinisterId,District,MinisterCode,FullName,AccountNo,Debit,Minister,Church,Total,NetBal,Month,MonthId,Year,PostDate")]            
        {
            if (ModelState.IsValid)
            {
                try
                {
                    db2.AccountRecords.Add(acctRecordInstance);
                    db2.SaveChanges();
                    TempData["status"] = " Opening Account for " + acctRecordInstance.FullName + " was successfully entered!";
                    return RedirectToAction("AccountStatement");
                }
                catch (AggregateException e)
                {
                    TempData["status"] = e.Message.ToString();
                    return RedirectToAction("OpeningBalance");
                }
            }
            //else
            //{
                //TempData["status"] = ModelState.Values.SelectMany(v => v.Errors);//" Error! Invalid inputs. Please check your values and try again";
                //var errors = ModelState.SelectMany(x => x.Value.Errors.Select(z => z.Exception));
                //var errors = ModelState.Where(x => x.Value.Errors.Count > 0).Select(x => new { x.Key, x.Value.Errors }).ToArray();
                //TempData["status"] = errors;
            //}

            var errors = ModelState.Where(x => x.Value.Errors.Count > 0).Select(x => new { x.Key, x.Value.Errors }).ToArray();
            TempData["status"] = errors;
            return RedirectToAction("AccountStatement");
        }

        //Select year range for Account
        
        #endregion

        #region File Operations.

        public ActionResult ExportData()
        {
            var databaseDistrict = db2.Districts.Select(c => new { c.DistrictCode, c.DistrictName }).OrderBy(p => p.DistrictName);

            var workbook = new HSSFWorkbook();

            foreach (var districtSheet in databaseDistrict)
            {
                var sheet = workbook.CreateSheet(districtSheet.DistrictCode.ToString());

                // Add header labels
                var rowIndex = 0;
                var row = sheet.CreateRow(rowIndex);
                row.CreateCell(0).SetCellValue("Id");
                row.CreateCell(1).SetCellValue("DistrictId");
                row.CreateCell(2).SetCellValue("MinisterId");
                row.CreateCell(3).SetCellValue("ChurchContribution");
                row.CreateCell(4).SetCellValue("MinisterContribution");
                row.CreateCell(5).SetCellValue("TotalContribution");
                row.CreateCell(6).SetCellValue("ScheduleMonth");
                row.CreateCell(7).SetCellValue("ScheduleYear");
                rowIndex++;

                //fiter the records in the tables
                int xx = Int32.Parse(districtSheet.DistrictCode);
                var filteredRecord = db2.ContributionScheduleTables.Where(c => c.DistrictId.Equals(xx)).OrderBy(c => c.Id).ToList();

                // Add data rows
                foreach (var record in filteredRecord)
                {
                    row = sheet.CreateRow(rowIndex);
                    //row.CreateCell(0).SetCellValue(rowIndex);
                    row.CreateCell(0).SetCellValue(record.Id);
                    row.CreateCell(1).SetCellValue(record.DistrictId);
                    row.CreateCell(2).SetCellValue(record.MinisterId);
                    row.CreateCell(3).SetCellValue(record.ChurchContribution);
                    row.CreateCell(4).SetCellValue(record.MinisterContribution);
                    row.CreateCell(5).SetCellValue(record.TotalContribution);
                    row.CreateCell(6).SetCellValue(record.ScheduleMonth);
                    row.CreateCell(7).SetCellValue(Convert.ToInt32(record.ScheduleYear));
                    rowIndex++; //can be used to index the rows
                }
            }

            // Save the Excel spreadsheet to a MemoryStream and return it to the client
            string saveAsFileName = string.Format("ContributionSchedule_{0:d}.xls", DateTime.Now.Date).Replace("/", "-");

            using (var exportData = new MemoryStream())
            {
                var cookie = new HttpCookie("Downloaded", "True");
                Response.Cookies.Add(cookie);
                workbook.Write(exportData);

                Response.ContentType = "application/vnd.ms-excel";
                Response.AddHeader("Content-Disposition", string.Format("attachment;filename={0}", saveAsFileName));

                Response.Clear();
                Response.BinaryWrite(exportData.GetBuffer());
                Response.End();

                TempData["status"] = " Export successful! Please download and save the updated Schedule: " + saveAsFileName;
            }
            return RedirectToAction("ContributionSchedule");
        }

        public ActionResult ImportData()
        {
            string fileName = Request.Files["file"].FileName;
            string extension = System.IO.Path.GetExtension(fileName);
            string path1 = string.Format("{0}/{1}", Server.MapPath("~/Content/UploadedFolder"), fileName);

            if (System.IO.File.Exists(path1)) System.IO.File.Delete(path1);

            Request.Files["file"].SaveAs(path1);

            var databaseDistrict = db2.Districts.Select(c => new { c.DistrictCode, c.DistrictName }).OrderBy(p => p.DistrictName);

            //HttpPostedFileBase hpf = this.Request.Files[0];

            var excel = new ExcelQueryFactory(path1);//FileName);
            //var excelFirstEntry;// = null;
            bool excelNotEmpty = false;
            int sheetNo = 0;
            try
            {
                var workSheetNames = excel.GetWorksheetNames();
                int counter = 0;
                while (counter < workSheetNames.Count() && !excelNotEmpty)
                {
                  var excelFirstEntry1 = excel.Worksheet<ContributionScheduleTable>(workSheetNames.ElementAt(counter)).FirstOrDefault();
                  if (excelFirstEntry1 != null)
                  {
                      excelNotEmpty = true;
                      sheetNo = Int32.Parse(workSheetNames.ElementAt(counter));                      
                  }
                  counter++;
                }
            }
            catch (AggregateException ex)
            {
                TempData["status"] = "Error!!! - " + ex.Message;
                return RedirectToAction("CreateTempTableRecord");
            }

            //Add a recursive formular that checks all sheets
            if (!excelNotEmpty)
             {
                 TempData["status"] = " Imported file is corrupt or empty! Please get the original exported file and try again.";
                 return RedirectToAction("CreateTempTableRecord");
             }
             else
             {
                 var excelFirstEntry = excel.Worksheet<ContributionScheduleTable>(sheetNo.ToString()).FirstOrDefault();

                 //try individual comparison of year and month for data integrity-later
                 var excelYear = excelFirstEntry.ScheduleYear;
                 var excelMonth = excelFirstEntry.ScheduleMonth;

                 //check if its a new record or old one
                 var isSheduleEmpty = db2.ContributionScheduleTables.FirstOrDefault();
                 if (isSheduleEmpty == null)
                 {
                     //check if it has been posted earlier
                     var isSheduleExist = db2.ContributionCheckListTables
                         .Where(c=>c.PayYear==excelYear && c.PayMonth==excelMonth).FirstOrDefault();

                     if (isSheduleExist != null)
                     {
                         TempData["status"] = " Imported file has been uploaded and processed earlier! Please get the latest Schedule and try again.";
                         return RedirectToAction("CreateTempTableRecord");
                     }
                 }
                 else
                 {
                     var dbYear = isSheduleEmpty.ScheduleYear;
                     var dbMonth = isSheduleEmpty.ScheduleMonth;

                     if (excelYear != dbYear || excelMonth != dbMonth)
                     {
                         TempData["status"] = " Imported file is old or corrupt! Please get the latest exported file and try again.";
                         return RedirectToAction("CreateTempTableRecord");
                     }
                 }
             }
                
            
            //var excel = new ExcelQueryFactory("C:\\Documents and Settings\\rk.prabakar\\Desktop\\test.xlsx");
            var excelModelList = new List<ContributionScheduleTable>();
                        
            foreach (var district in databaseDistrict.ToList())
            {
                //string xx = district.DistrictName.ToString();
                int zz = Int32.Parse(district.DistrictCode);
                var dataContent = from c in excel.Worksheet<ContributionScheduleTable>(zz.ToString()) select c;

                var exsistingRecord = db2.ContributionScheduleTables.Where(c => c.DistrictId.Equals(zz)).ToList();
                
                //Checking the datacontent before accessing
                if (dataContent != null)
                {
                    //reading content from the excel comes here
                    foreach (var a in dataContent)
                    {
                        var excelModel = new ContributionScheduleTable();

                        excelModel.Id = a.Id;
                        excelModel.DistrictId = a.DistrictId;
                        excelModel.MinisterId = a.MinisterId;
                        excelModel.ChurchContribution = a.ChurchContribution;
                        excelModel.MinisterContribution = a.MinisterContribution;
                        excelModel.TotalContribution = a.TotalContribution;
                        excelModel.ScheduleMonth = a.ScheduleMonth;
                        excelModel.ScheduleYear = a.ScheduleYear;

                        if (ModelState.IsValid)
                        {
                            bool isExist = false;
                            int minId=0;
                            foreach (var yy in exsistingRecord)
                            {
                                if (excelModel.MinisterId == yy.MinisterId)
                                {
                                    isExist = true;
                                    minId = yy.MinisterId;
                                }
                            }

                            if (isExist)
                            {
                                //Thread td = new Thread();
                                var del = db2.ContributionScheduleTables.Where(c => c.MinisterId == minId).First();
                                db2.ContributionScheduleTables.Remove(del);
                                db2.SaveChanges();
                            }                            
                                excelModelList.Add(excelModel);
                        }
                    }
                }
            }

            db2.ContributionScheduleTables.AddRange(excelModelList);
            db2.SaveChanges();

            TempData["status"] = " Import successful! ContributionSchedule has been updated with " + fileName;

            return RedirectToAction("ContributionSchedule");
        }

        //public FileContentResult ChecklistReport()
        //Export Checklist for all Districts

        public ActionResult ChecklistReport()
        {
            LocalReport localReport = new LocalReport();
            localReport.ReportPath = Server.MapPath("~/RDLC_Reports/ChecklistReport.rdlc");

            int yearValue = Convert.ToInt32(Session["SelectedYear"]);
            int monthValue = Convert.ToInt32(Session["SelectedMonth"]);
            int SelectedDistrict = Convert.ToInt32(Session["SelectedDistrict"]);

            var yearList = db2.Years.Select(c => new { c.Id, c.YearValue }).OrderByDescending(p => p.Id);
            var monthList = db2.Months.Select(c => new { c.Id, c.MonthName }).OrderBy(p => p.Id).ToList();

            string YearValue = yearList.Where(c => c.Id.Equals(yearValue)).Select(c => c.YearValue).First().ToString();
            string MonthValue = monthList.Where(c => c.Id.Equals(monthValue)).Select(p => p.MonthName).First().ToString();

            var rprt = db2.ContributionCheckListTables
                .Where(c => c.DistrictId.Equals(SelectedDistrict) && c.PayYear.Equals(YearValue)
                    && c.PayMonth.Equals(MonthValue)).ToList();

            string sedss = SelectedDistrict.ToString();

            string districtName = db2.Districts.Where(m => m.DistrictCode == sedss).First().DistrictName.ToString();
            string downloadFileName = "ContributionChecklist-" + MonthValue + "_" + YearValue;
            //+ districtName.Remove(districtName.IndexOf(' '), 1) + "_" 
            ReportDataSource reportDataSource = new ReportDataSource();
            reportDataSource.Name = "ChecklistDataSet";

            reportDataSource.Value = rprt;

            localReport.DataSources.Add(reportDataSource);
            string reportType = "PDF";
            string mimeType;
            string encoding;
            string fileNameExtension;

            string deviceInfo = "<DeviceInfo>" +
                "  <OutputFormat>PDF</OutputFormat>" +
                "  <PageWidth>21cm</PageWidth>" +
                "  <PageHeight>29.7cm</PageHeight>" +
                "  <MarginTop>0.05cm</MarginTop>" +
                "  <MarginLeft>0.1cm</MarginLeft>" +
                "  <MarginRight>0.1cm</MarginRight>" +
                "  <MarginBottom>0.05cm</MarginBottom>" +
                "</DeviceInfo>";

            Warning[] warnings;
            string[] streams;
            byte[] renderedBytes;

            renderedBytes = localReport.Render(reportType, deviceInfo, out mimeType, out encoding, out fileNameExtension, out streams, out warnings);

            Response.AddHeader("Content-Type: application/pdf; content-disposition:inline;", "filename=" + downloadFileName + "." + fileNameExtension);
            return File(renderedBytes, mimeType);
        }
        
        public ActionResult AccountStatementReport()
        {
            LocalReport localReport = new LocalReport();
            localReport.ReportPath = Server.MapPath("~/RDLC_Reports/AccountStatementReport.rdlc");

            string yearName = Session["YearString"].ToString();
            int ministerSelected = Convert.ToInt32(Session["MinisterId"]);
            int districtSelected = Convert.ToInt32(Session["DistrictId"]);

            string kiiii = ministerSelected.ToString();
            var rprt = db2.AccountRecords.Where(c => c.MinisterCode == kiiii 
                    && c.Year.Equals(yearName) && c.DistrictId.Equals(districtSelected)).OrderBy(c => c.MonthId).ToList();

            string reportName = rprt.First().FullName + "_" + rprt.First().AccountNo;
            string downloadFileName = "AccountStatement-" + reportName.Remove(reportName.IndexOf(','), 1);

            ReportDataSource reportDataSource = new ReportDataSource();
            reportDataSource.Name = "AccountStatementDataSet";

            reportDataSource.Value = rprt;

            localReport.DataSources.Add(reportDataSource);
            string reportType = "PDF";
            string mimeType;
            string encoding;
            string fileNameExtension;

            string deviceInfo = "<DeviceInfo>" +
                "  <OutputFormat>PDF</OutputFormat>" +
                "  <PageWidth>21cm</PageWidth>" +
                "  <PageHeight>29.7cm</PageHeight>" +
                "  <MarginTop>0.05cm</MarginTop>" +
                "  <MarginLeft>0.1cm</MarginLeft>" +
                "  <MarginRight>0.1cm</MarginRight>" +
                "  <MarginBottom>0.05cm</MarginBottom>" +
                "</DeviceInfo>";

            Warning[] warnings;
            string[] streams;
            byte[] renderedBytes;

            renderedBytes = localReport.Render(reportType, deviceInfo, out mimeType, out encoding, out fileNameExtension, out streams, out warnings);

            Response.AddHeader("Content-Type: application/pdf; content-disposition:inline;", "filename=" + downloadFileName + "." + fileNameExtension);
            return File(renderedBytes, mimeType);
        }


        public ActionResult AccountStatementPayoff()
        {
            LocalReport localReport = new LocalReport();
            localReport.ReportPath = Server.MapPath("~/RDLC_Reports/AccountStatementPayoff.rdlc");

            //string yearName = Session["YearString"].ToString();
            int ministerSelected = Convert.ToInt32(Session["MinisterId"]);
            int districtSelected = Convert.ToInt32(Session["DistrictId"]);


            string kiiii = ministerSelected.ToString();
            var rprt = db2.AccountRecords.Where(c => c.MinisterCode == kiiii
                //&& c.Year.Equals(yearName)
                    && c.DistrictId.Equals(districtSelected)).OrderBy(c => c.MonthId).ToList();

            string[] yearPresent = rprt.Select(c => c.Year).ToArray();

            ReportParameter YearListParameter = new ReportParameter("ReportParameter1", yearPresent);
            localReport.SetParameters(YearListParameter);

            string reportName = rprt.First().FullName + "_" + rprt.First().AccountNo;
            string downloadFileName = "PayoffAccountStatement-" + reportName.Remove(reportName.IndexOf(','), 1);

            ReportDataSource reportDataSource = new ReportDataSource();
            reportDataSource.Name = "AccountStatementDataSet";

            reportDataSource.Value = rprt;

            localReport.DataSources.Add(reportDataSource);
            string reportType = "PDF";
            string mimeType;
            string encoding;
            string fileNameExtension;

            string deviceInfo = "<DeviceInfo>" +
                "  <OutputFormat>PDF</OutputFormat>" +
                "  <PageWidth>21cm</PageWidth>" +
                "  <PageHeight>29.7cm</PageHeight>" +
                "  <MarginTop>0.05cm</MarginTop>" +
                "  <MarginLeft>0.1cm</MarginLeft>" +
                "  <MarginRight>0.1cm</MarginRight>" +
                "  <MarginBottom>0.05cm</MarginBottom>" +
                "</DeviceInfo>";

            Warning[] warnings;
            string[] streams;
            byte[] renderedBytes;

            renderedBytes = localReport.Render(reportType, deviceInfo, out mimeType, out encoding, out fileNameExtension, out streams, out warnings);

            Response.AddHeader("Content-Type: application/pdf; content-disposition:inline;", "filename=" + downloadFileName + "." + fileNameExtension);
            return File(renderedBytes, mimeType);

        }

        public ActionResult AccountStatementRange()
        {
            LocalReport localReport = new LocalReport();
            localReport.ReportPath = Server.MapPath("~/RDLC_Reports/AccountStatementRange.rdlc");

            //string yearName = Session["YearString"].ToString();
           int ministerSelected = Convert.ToInt32(Session["MinisterId"]);
            int districtSelected = Convert.ToInt32(Session["DistrictId"]);

            string kiiii = ministerSelected.ToString();
            var rprt = db2.AccountRecords.Where(c => c.MinisterCode == kiiii
                    //&& c.Year.Equals(yearName)
                    && c.DistrictId.Equals(districtSelected)).OrderBy(c => c.MonthId).ToList();

            string[] yearPresent = rprt.Select(c=>c.Year).ToArray();

            ReportParameter YearListParameter = new ReportParameter("ReportParameter1", yearPresent);
            localReport.SetParameters(YearListParameter);

            string reportName = rprt.First().FullName + "_" + rprt.First().AccountNo;
            string downloadFileName = "GeneralAccountStatement-" + reportName.Remove(reportName.IndexOf(','), 1);

            ReportDataSource reportDataSource = new ReportDataSource();
            reportDataSource.Name = "AccountStatementRangeDataSet";

            reportDataSource.Value = rprt;

            localReport.DataSources.Add(reportDataSource);
            string reportType = "PDF";
            string mimeType;
            string encoding;
            string fileNameExtension;

            string deviceInfo = "<DeviceInfo>" +
                "  <OutputFormat>PDF</OutputFormat>" +
                "  <PageWidth>21cm</PageWidth>" +
                "  <PageHeight>29.7cm</PageHeight>" +
                "  <MarginTop>0.05cm</MarginTop>" +
                "  <MarginLeft>0.1cm</MarginLeft>" +
                "  <MarginRight>0.1cm</MarginRight>" +
                "  <MarginBottom>0.05cm</MarginBottom>" +
                "</DeviceInfo>";

            Warning[] warnings;
            string[] streams;
            byte[] renderedBytes;

            renderedBytes = localReport.Render(reportType, deviceInfo, out mimeType, out encoding, out fileNameExtension, out streams, out warnings);

            Response.AddHeader("Content-Type: application/pdf; content-disposition:inline;", "filename=" + downloadFileName + "." + fileNameExtension);
            return File(renderedBytes, mimeType);


            //LocalReport localReport = new LocalReport();
            //localReport.ReportPath = Server.MapPath("~/RDLC_Reports/AccountStatementRange.rdlc");

            //string yearName = Session["YearString"].ToString();
            //int ministerSelected = Convert.ToInt32(Session["MinisterId"]);
            //int districtSelected = Convert.ToInt32(Session["DistrictId"]);

            //string hudhu = ministerSelected.ToString();
            //var rprt = db2.AccountRecords.Where(c => c.MinisterCode == hudhu
            //        && c.DistrictId.Equals(districtSelected)).OrderBy(c => c.MonthId).ToList();
            ////&& c.Year.Equals(yearName) 
            //string reportName = rprt.First().FullName + "_" + rprt.First().AccountNo;
            //string downloadFileName = "AccountStatement-" + reportName.Remove(reportName.IndexOf(','), 1);

            //ReportDataSource reportDataSource = new ReportDataSource();
            //reportDataSource.Name = "AccountStatementDataSet";

            //reportDataSource.Value = rprt;

            //localReport.DataSources.Add(reportDataSource);
            //string reportType = "PDF";
            //string mimeType;
            //string encoding;
            //string fileNameExtension;

            //string deviceInfo = "<DeviceInfo>" +
            //    "  <OutputFormat>PDF</OutputFormat>" +
            //    "  <PageWidth>21cm</PageWidth>" +
            //    "  <PageHeight>29.7cm</PageHeight>" +
            //    "  <MarginTop>0.05cm</MarginTop>" +
            //    "  <MarginLeft>0.1cm</MarginLeft>" +
            //    "  <MarginRight>0.1cm</MarginRight>" +
            //    "  <MarginBottom>0.05cm</MarginBottom>" +
            //    "</DeviceInfo>";

            //Warning[] warnings;
            //string[] streams;
            //byte[] renderedBytes;

            //renderedBytes = localReport.Render(reportType, deviceInfo, out mimeType, out encoding, out fileNameExtension, out streams, out warnings);

            //Response.AddHeader("Content-Type: application/pdf; content-disposition:inline;", "filename=" + downloadFileName + "." + fileNameExtension);
            //return File(renderedBytes, mimeType);
        }

        public ActionResult ReconciliationSummaryReport()
        {
            LocalReport localReport = new LocalReport();
            localReport.ReportPath = Server.MapPath("~/RDLC_Reports/ReconciliationReport.rdlc");

            int yearValue = Convert.ToInt32(Session["SelectedYear"]);
            int monthValue = Convert.ToInt32(Session["SelectedMonth"]);

            string YearValue = db2.Years.Where(c => c.Id.Equals(yearValue)).Select(c => c.YearValue).First().ToString();
            string MonthValue = db2.Months.Where(c => c.Id.Equals(monthValue)).Select(p => p.MonthName).First().ToString();

            var rprt = db2.ReconciliationSummarys
                .Where(c => c.PayYear.Equals(YearValue) && c.PayMonth.Equals(MonthValue))
                .OrderBy(c=>c.DistrictCode).ToList();

            string downloadFileName = "ReconciliationSummaryReport-" + MonthValue + "_" + YearValue;

            ReportDataSource reportDataSource = new ReportDataSource();
            reportDataSource.Name = "ReconciliationDataSet";

            reportDataSource.Value = rprt;

            localReport.DataSources.Add(reportDataSource);
            string reportType = "PDF";
            string mimeType;
            string encoding;
            string fileNameExtension;

            string deviceInfo = "<DeviceInfo>" +
                "  <OutputFormat>PDF</OutputFormat>" +
                "  <PageWidth>29.7cm</PageWidth>" +
                "  <PageHeight>21cm</PageHeight>" +
                "  <MarginTop>0.05cm</MarginTop>" +
                "  <MarginLeft>0.1cm</MarginLeft>" +
                "  <MarginRight>0.1cm</MarginRight>" +
                "  <MarginBottom>0.05cm</MarginBottom>" +
                "</DeviceInfo>";

            Warning[] warnings;
            string[] streams;
            byte[] renderedBytes;

            //Render the report            
            renderedBytes = localReport.Render(reportType, deviceInfo, out mimeType, out encoding, out fileNameExtension, out streams, out warnings);

            Response.AddHeader("Content-Type:application/pdf; content-disposition:inline;", "filename=" + downloadFileName + "." + fileNameExtension);
            //Response.AppendHeader();
            //Response.AddHeader("content-disposition", "filename=" + downloadFileName + "." + fileNameExtension);
            return File(renderedBytes, mimeType);
        }

        #endregion

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db2.Dispose();
            }
            base.Dispose(disposing);
        }
    
    }
}