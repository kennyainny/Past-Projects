using System.Data.Entity;
using System.Linq;
using Application.Web.Models;
using System.Web.Mvc;
using System;
using System.Collections.Generic;
using Application.Web.Controllers;
using Application.Web.helpers;
using System.Web.Mvc.Html;
using System.Collections;

namespace Application.Web.Controllers
{

    public class AccountRoleController : Controller
    {
        #region Global Variables.

        private acct db = new acct();
        private UsersContext db1 = new UsersContext();
        custom_helper customHelp = new custom_helper();

        AccountSystemModels bal = new AccountSystemModels();
        //PensionDBEntitiesConnection db0 = new PensionDBEntitiesConnection();




        #endregion

        #region Accounts
        public ActionResult Index()
        {
            //ViewBag.FinancialYear = "";
            if (Session["AccountSet"] == null)
            {
                return RedirectToAction("Account");
            }
            return View();
        }
        public ActionResult Account()
        {
            ViewBag.AccountID = new SelectList(db.settings, "id", "name");

            return View();
        }
        [HttpPost]
        public ActionResult Account(FormCollection AccountID)
        {
            Session["AccountSet"] = "sss";
            ViewBag.AccountID = new SelectList(db.settings, "id", "name");
            return RedirectToAction("Index");

            //return View();
        }

        public ActionResult CreateAccount()
        {
            var accounts = db.settings.FirstOrDefault();
            return View(accounts);
        }
        [HttpPost]
        public ActionResult CreateAccount(setting settgs)
        {

            if (ModelState.IsValid)
            {
                bool exists = false;
                foreach (var setting in db.settings)
                {
                    //check for duplicate entry
                    if (setting.name.Equals(settgs.name) && setting.email.Equals(settgs.email)) exists = true;
                }

                if (!exists)
                {
                    // Attempt to create a new account
                    try
                    {
                        db.settings.Add(settgs);
                        db.SaveChanges();
                        TempData["status"] = " " + settgs.name + " was successfully registered!";
                        return RedirectToAction("RegisterMinister");
                    }
                    catch (AggregateException e)
                    {
                        TempData["status"] = e.Message.ToString();
                        return RedirectToAction("CreateAcccount");
                    }
                }
            }

            TempData["status"] = " Minister already exists, please register a new minister";
            return RedirectToAction("Account");
        }
        public ActionResult ChartOfAccounts()
        {
            //List<entry_items> objList = entry_items.GetUsers();
            var accounts = db.AccountGroups.Include("ledgers");
            return View(accounts);
        }

        public ActionResult AddGroup()
        {

            ViewBag.AccountID = new SelectList(db.AccountGroups, "id", "name");
            return View();
        }

        [HttpPost]
        public ActionResult AddGroup(AccountGroup NewGroup)
        {
            if (ModelState.IsValid)
            {
                bool exists = false;
                foreach (var group in db.AccountGroups)
                {
                    //check for duplicate entry
                    if (group.name.Equals(NewGroup.name) && group.parent_id.Equals(NewGroup.parent_id)) exists = true;
                }

                if (!exists)
                {
                    // Attempt to create a new account
                    try
                    {
                        db.AccountGroups.Add(NewGroup);
                        db.SaveChanges();
                        TempData["status"] = " " + NewGroup.name + " was successfully registered!";
                        return RedirectToAction("ChartOfAccounts");
                    }
                    catch (AggregateException e)
                    {
                        TempData["status"] = e.Message.ToString();
                        return RedirectToAction("ChartOfAccounts");
                    }
                }
            }

            TempData["status"] = " Group already exists, please register a new group";
            return RedirectToAction("ChartOfAccounts");
        }
        public ActionResult AddLedger()
        {
            ViewBag.AccountID = new SelectList(db.AccountGroups, "id", "name");
            ViewBag.Type = new SelectList(db.entry_types, "id", "name");
            return View();
        }
        [HttpPost]
        public ActionResult AddLedger(ledger newLedger)
        {
            ViewBag.AccountID = new SelectList(db.AccountGroups, "id", "name");

            if (ModelState.IsValid)
            {
                bool exists = false;
                foreach (var ledger in db.ledgers)
                {
                    //check for duplicate entry
                    if (ledger.name.Equals(newLedger.name) && ledger.group_id.Equals(newLedger.group_id)) exists = true;
                }

                if (!exists)
                {
                    // Attempt to create a new account
                    try
                    {
                        db.ledgers.Add(newLedger);
                        db.SaveChanges();
                        TempData["status"] = " " + newLedger.name + " was successfully registered!";
                        return RedirectToAction("ChartOfAccounts");
                    }
                    catch (AggregateException e)
                    {
                        TempData["status"] = e.Message.ToString();
                        return RedirectToAction("ChartOfAccounts");
                    }
                }
            }

            TempData["status"] = " Ledger already exists, please register a new ledger";
            return RedirectToAction("ChartOfAccounts");
        }
        #endregion

        #region Entries
        public ActionResult All()
        {
            //return View(db.entries
            //    .OrderBy(c => c.number).ToList());
            return View(db.entries);

            //var result = from a in db.entries.ToList()
            //             join b in db.entry_types on a.entry_type equals b.id
            //             select new
            //             {
            //                 c = a.cr_total,
            //                 d = a.date,
            //                 e = b.name
            //             };


            //return View(result.ToList());
        }
        public ActionResult Receipt()
        {
            return View(db.entries.Where(a => a.entry_type == 1)
                .OrderBy(c => c.number).ToList());
        }
        public ActionResult AddPayment()
        {
            //ViewBag.totall = db.entries.Where(s => s.entry_items.Any(q => q.ledger_id == 3 && q.dc == "D")).Sum((d => d.dr_total));
            ViewBag.totall = db.entry_items.Where(q => q.ledger_id == 2 && q.dc == "C").Sum((d => d.amount));

            ViewBag.Ledgers = new SelectList(db.ledgers, "id", "name");

            //List<SelectListItem> items = new List<SelectListItem>();

            ////items.Add(new SelectListItem { Text = "", Value = "0" });
            //items.Add(new SelectListItem { Text = "Dr", Value = "D" });
            //items.Add(new SelectListItem { Text = "Cr", Value = "C" });
            //ViewBag.EntryType = items;
            return View();
        }
        public ActionResult payment()
        {
            return View(db.entries.Where(a => a.entry_type == 2)
                .OrderBy(c => c.number).ToList());
        }
        public ActionResult Contra()
        {
            return View(db.entries.Where(a => a.entry_type == 3)
                .OrderBy(c => c.number).ToList());
        }

        public ActionResult Journal()
        {
            return View(db.entries.Where(a => a.entry_type == 4)
                .OrderBy(c => c.number).ToList());
        }

        [HttpGet]
        public ActionResult Balance(int id = 0)
        {
            if (id > 0)
                return Content(bal.get_ledger_balance(id).ToString());
            else
                return Content("");
        }

        public ActionResult addrow(string id = "all")
        {
            //return Content("<tr class='new-row'><td>" + "<select class='dc-dropdown'><option value='D'>Dr</option><option value='C'>Cr</option></select>" + "</td><td>" + "" + "</td><td>" + "<input type='text' name='DrAmount' class='dr-item' maxlength='15' disabled /></td><td><input type='text' name='CrAmount' class='cr-item' maxlength='15' disabled /></td><td><span id='add' class='addrow' title='Add Row'><img src='../../Images/Icons/add.png'></span></td><td><span id='delete' class='deleterow' title='Delete Row'><img src=\"../../Images/Icons/delete.png\"></span></td><td class=\"ledger-balance\"><div></div></td></tr>");

            if (id == "all")
            {

            }

            int entry_type_id = customHelp.entry_type_name_to_id(id);
            if (entry_type_id > 0)
            {
                var current_entry_type = db.entry_types.Where(a => a.id == entry_type_id).First();


                //var current_entry_type = db.entry_types.Where(a => a.id == entry_type_id).First();
                if (current_entry_type.bank_cash_ledger_restriction == 4)
                {
                    //ViewBag.Ledgers = new SelectList(db.entry_types.Include(s => s.ledger.name).Where(a => a.bank_cash_ledger_restriction == 1), "id", "name");
                    ViewBag.Ledgers = new SelectList(db.ledgers.Where(t => t.type == 1), "id", "name");
                }
                else if (current_entry_type.bank_cash_ledger_restriction == 5)
                {
                    ViewBag.Ledgers = new SelectList(db.ledgers.Where(t => t.type != 1), "id", "name");
                }
                else
                {
                    ViewBag.Ledgers = new SelectList(db.ledgers, "id", "name");
                }

            }
            else
            {
                ViewBag.Ledgers = new SelectList(db.ledgers, "id", "name");
            }


            return PartialView("_AddEntryPartial");

        }
        //public string drop()
        //{
        //    Html.DropDownListFor(model => model.id, (SelectList)ViewBag.Ledgers, string.Empty, new { @class = "ledger-dropdown" });
        //}
        [AcceptVerbs(HttpVerbs.Get | HttpVerbs.Post)]
        public ActionResult Add(string id, FormCollection c, List<entry> ent)
        {
            ViewBag.totall = db.entry_items.Where(q => q.ledger_id == 2 && q.dc == "C").Sum((d => d.amount));
            ViewBag.Message = "Here You can add entries ";

            ViewBag.Ledgers = new SelectList(db.ledgers, "id", "name");
            int entry_type_id = customHelp.entry_type_name_to_id(id);
            var current_entry_type = db.entry_types.Where(a => a.id.Equals(entry_type_id)).First();
            if (Request.HttpMethod == "GET")
            {
                if (entry_type_id < 1)
                {
                    ViewBag.Message = "Invalid Entry type.";
                    RedirectToAction("All");
                }


                //var current_entry_type = db.entry_types.Where(a => a.id == entry_type_id).First();
                if (current_entry_type.bank_cash_ledger_restriction == 4)
                {
                    //ViewBag.Ledgers = new SelectList(db.entry_types.Include(s => s.ledger.name).Where(a => a.bank_cash_ledger_restriction == 1), "id", "name");
                    ViewBag.Ledgers = new SelectList(db.ledgers.Where(t => t.type == 1), "id", "name");
                }
                else if (current_entry_type.bank_cash_ledger_restriction == 5)
                {
                    ViewBag.Ledgers = new SelectList(db.ledgers.Where(t => t.type != 1), "id", "name");
                }
                else
                {
                    ViewBag.Ledgers = new SelectList(db.ledgers, "id", "name");
                }

                ViewBag.totall = db.entry_items.Where(q => q.ledger_id == 2 && q.dc == "C").Sum((d => d.amount));

                //ViewBag.Ledgers = new SelectList(db.ledgers, "id", "name");
                return View(db.entries.ToList());

            }

           else if (Request.HttpMethod == "POST")
            {

                ////int entry_type_id = customHelp.entry_type_name_to_id(id);
                //if (entry_type_id < 1)
                //{
                //    ViewBag.Message = "Invalid Entry type.";
                //    RedirectToAction("All");
                //}
                //else
                //{
                //    //var current_entry_type = db.entry_types.Where(a => a.id == entry_type_id);

                //    ViewBag.Message = entry_type_id;
                //}

                //return Content(entry_type_id.ToString());








                /* Checking for Valid Ledgers account and Debit and Credit Total */

                var data_all_ledger_id = c.GetValues("Ledgers");
                var data_all_ledger_dc = c.GetValues("ledger_dc");
                var data_all_dr_amount = c.GetValues("DrAmount");
                var data_all_cr_amount = c.GetValues("CrAmount");
                var dr_total = 0;
                var cr_total = 0;
                var bank_cash_present = false; /* Whether atleast one Ledger account is Bank or Cash account */
                var non_bank_cash_present = false;  /* Whether atleast one Ledger account is NOT a Bank or Cash account */
                var data_number = 0;

                int ia = 0;
                while (ia < data_all_ledger_id.Count())
                {

                    if (Convert.ToInt32(data_all_ledger_id[Convert.ToInt32(ia)]) < 1)
                        //return RedirectToAction("Add/" + id);
                        continue;



                    var valid_ledger = db.ledgers.Find(Convert.ToInt32(data_all_ledger_id[ia]));
                    TempData["Info2"] = valid_ledger.type;
                    if (valid_ledger == null)
                    {
                        TempData["Info"] = "Invalid Ledger account.";
                        return RedirectToAction("Add/" + id);
                        //break;
                    }
                    else
                    {
                        if (current_entry_type.bank_cash_ledger_restriction == '2')
                        {
                            if (data_all_ledger_dc[ia].Equals("D") && valid_ledger.type == 1)
                            {
                                bank_cash_present = true;
                            }
                            if (valid_ledger.type != 1)
                                non_bank_cash_present = true;
                        }
                        else if (current_entry_type.bank_cash_ledger_restriction == '3')
                        {
                            if (data_all_ledger_dc[ia].Equals("C") && valid_ledger.type == 1)
                            {
                                bank_cash_present = true;
                            }
                            if (valid_ledger.type != 1)
                                non_bank_cash_present = true;
                        }
                        else if (current_entry_type.bank_cash_ledger_restriction == '4')
                        {
                            if (valid_ledger.type != 1)
                            {
                                TempData["Info"] = "Invalid Ledger account. " + current_entry_type.name + " Entries can have only Bank and Cash Ledgers accounts.";
                                return RedirectToAction("Add/" + id);
                            }
                        }
                        else if (current_entry_type.bank_cash_ledger_restriction == '5')
                        {
                            if (valid_ledger.type == 1)
                            {
                                TempData["Info"] = "Invalid Ledger account. " + current_entry_type.name + " Entries cannot have Bank and Cash Ledgers accounts.";
                                return RedirectToAction("Add/" + id);
                            }
                        }

                    }
                    ia++;
                }
                int i2 = 0;
                while (i2 < data_all_dr_amount.Count() || i2 < data_all_cr_amount.Count())
                {
                            dr_total = customHelp.float_ops(Convert.ToInt32(data_all_dr_amount[i2]), dr_total, "+");
                            cr_total = customHelp.float_ops(Convert.ToInt32(data_all_cr_amount[i2]), cr_total, "+");
                        //if (chk == "D")
                        //{
                        //        //dr_total = customHelp.float_ops(drTotal, dr_total, "+");
                        //        //int drTotal = Convert.ToInt32(data_all_dr_amount[0]);
                            
                        //}
                        //else if (chk == "C")
                        //{

                            

                        //}

                        //data_amount = int.Parse(data_all_dr_amount[Convert.ToInt32(idlg)]);



                        //dr_total = customHelp.float_ops(int.Parse(data_all_dr_amount[Convert.ToInt32(ia)]), dr_total, "+");
                    
                    i2++;

                }
                
                if (customHelp.float_ops(dr_total, cr_total, "!=") == 1)
                {
                    TempData["Info"] = "Debit and Credit Total does not match!";
                    return Content("Debit and Credit Total does not match!");
                }
                else if (customHelp.float_ops(dr_total, 0, "==") == 1 && customHelp.float_ops(cr_total, 0, "==") == 1)
                {
                    TempData["Info"] = "Cannot save empty Entry.";
                    return Content("Cannot save empty Entry.");

                }

                /* Check if atleast one Bank or Cash Ledger account is present */
                if (current_entry_type.bank_cash_ledger_restriction == '2')
                {
                    if (!bank_cash_present)
                    {
                        TempData["Info"] = "Need to Debit atleast one Bank or Cash account";
                        return Content("Need to Debit atleast one Bank or Cash account");
                    }
                    if (!non_bank_cash_present)
                    {
                        TempData["Info"] = "Need to Debit or Credit atleast one NON - Bank or Cash account";
                        return Content("Need to Debit or Credit atleast one NON - Bank or Cash account");
                    }
                }
                else if (current_entry_type.bank_cash_ledger_restriction == '3')
                {
                    if (!bank_cash_present)
                    {
                        TempData["Info"] = "Need to Credit atleast one Bank or Cash account.";
                        return Content("Need to Credit atleast one Bank or Cash account.");
                    }
                    if (!non_bank_cash_present)
                    {
                        TempData["Info"] = "Need to Debit or Credit atleast one NON - Bank or Cash account.";
                        return Content("Need to Debit or Credit atleast one NON - Bank or Cash account.");
                    }
                }

                /* Adding main entry */
                if (current_entry_type.numbering == '2')
                {
                    data_number = Convert.ToInt32(c.GetValues("entry_number"));
                }
                else if (current_entry_type.numbering == '3')
                {
                    data_number = Convert.ToInt32(c.GetValues("entry_number"));
                    if (data_number == null || data_number < 1)
                        data_number = 0;
                }
                else
                {
                    if (c.GetValues("entry_number") == null || c.GetValues("entry_number").Length < 1)
                        data_number = Convert.ToInt32(c.GetValues("entry_number"));
                    else
                        data_number = Convert.ToInt32(customHelp.next_entry_number(entry_type_id));
                }

                var data_date = c.GetValues("entry_date");
                var data_narration = c.GetValues("NarrationTxt");
                //var data_tag = c.GetValues("entry_tag");

                var data_tag = "";
                var data_type = entry_type_id;
                //data_date = data_date; // Converting date to MySQL
                var entry_id = 0;

                int i = 0;
                if (ModelState.IsValid)
                {


                    var number = data_number;
                    var date = data_date;
                    var narration = data_narration;
                    var entry_type = data_type;
                    var tag_id = data_tag;


                    entry en =new entry();

                    //foreach (var en in ents)
                    //{
                    en.number = number;
                    en.date = DateTime.Now;
                    en.narration = narration.ToString();
                    en.entry_type = entry_type;


                    //    db.entries.Add(en);
                    //    db.SaveChanges();
                    //}
                    db.entries.Add(en);
                    db.SaveChanges();
                    entry_id = en.id;
                    try
                    {
                        TempData["Message"] = "Created Sccessfuly ";
                        ModelState.Clear();
                    }
                    catch
                    {
                        TempData["Message"] = "Error addding Entry. ";
                        //this->logger->write_message("error", "Error adding " . current_entry_type['name'] . " Entry number " . full_entry_number(entry_type_id, data_number) . " since failed inserting entry");
                        //return Redirect(Request.UrlReferrer);
                        return Content("Error addding Entry. ");

                    }

                    /* Adding ledger accounts */
                    
                     data_all_ledger_id = c.GetValues("Ledgers");
                     data_all_ledger_dc = c.GetValues("ledger_dc");
                     data_all_dr_amount = c.GetValues("DrAmount");
                     data_all_cr_amount = c.GetValues("CrAmount");

                    dr_total = 0;
                    cr_total = 0;
                        var data_amount = 0;

                        int i2a = 0;
                        while (i2a < data_all_dr_amount.Count() || i2a < data_all_cr_amount.Count())
                        {
                        if (data_all_ledger_dc[Convert.ToInt32(i2a)] == "D")
                        {
                            data_amount = int.Parse(data_all_dr_amount[Convert.ToInt32(i2a)]);
                            dr_total = customHelp.float_ops(Convert.ToInt32(data_all_dr_amount[i2a]), dr_total, "+");
                            cr_total = customHelp.float_ops(Convert.ToInt32(data_all_cr_amount[i2a]), cr_total, "+");
                        }
                        else if (data_all_ledger_dc[Convert.ToInt32(i2a)] == "C")
                        {
                            data_amount = int.Parse(data_all_cr_amount[i2a]);
                            dr_total = customHelp.float_ops(Convert.ToInt32(data_all_dr_amount[i2a]), dr_total, "+");
                            cr_total = customHelp.float_ops(Convert.ToInt32(data_all_cr_amount[i2a]), cr_total, "+");
                        }

                            i2a++;

                        }

                    int idlg = 0;
                    for (idlg = 0; idlg < data_all_ledger_id.Count(); idlg++)
                    {
                    //    MinisterProfile usr = db2.MinisterProfiles.Find(Convert.ToInt32(ChequeIDArray[i]));
                    //    usr.nCheque = ChequeArray[i];
                    //    db2.Entry(usr).State = EntityState.Modified;
                    //}
                    //foreach (var idlg in data_all_ledger_dc)
                    //{
                        var data_ledger_dc = data_all_ledger_dc[Convert.ToInt32(idlg)];
                        int data_ledger_id = Convert.ToInt32(data_all_ledger_id[Convert.ToInt32(idlg)]);
                        if (data_ledger_id == 0)
                            continue;








                            //dr_total = customHelp.float_ops(Convert.ToInt32(data_all_dr_amount[Convert.ToInt32(idlg)]), dr_total, "+");
                       // }
                            //cr_total = customHelp.float_ops(int.Parse(data_all_cr_amount[idlg]), cr_total, "+");
                        //}
                        if (ModelState.IsValid)
                        {

                            var entry_idx = entry_id;
                            var ledger_id = data_ledger_id;
                            var amount = data_amount;
                            var dc = data_ledger_dc;

                            entry_items en2 = new entry_items();
                            //foreach (var en2 in entyIT)
                            //{
                                en2.entry_id = entry_id;
                                en2.ledger_id = data_ledger_id;
                                en2.amount = data_amount;
                                en2.dc = dc;


                                db.entry_items.Add(en2);
                            //}
                            try
                            {
                                db.SaveChanges();
                                TempData["Message"] = "Created Sccessfuly ";
                                ModelState.Clear();
                            }
                            catch
                            {
                                TempData["Message"] = "Error addding Entry. ";
                                //this->logger->write_message("error", "Error adding " . current_entry_type['name'] . " Entry number " . full_entry_number(entry_type_id, data_number) . " since failed inserting entry");
                                //return Redirect(Request.UrlReferrer);
                                //this->logger->write_message("error", "Error adding " . current_entry_type['name'] . " Entry number " . full_entry_number(entry_type_id, data_number) . " since failed inserting entry ledger item " . "[id:" . data_ledger_id . "]");

                                //return Content("");
                                break;

                            }
                        }



                    }
                    /* Updating Debit and Credit Total in entries table */

                    if (ModelState.IsValid)
                    {

                        var ide = entry_id;
                        var dr_totalx = dr_total;
                        var total = cr_total;


                        //for (var i2 = 0; i2 < ent.Count(); i2++)
                        //{
                        //    entry usr = db.entries.Find(int.Parse(ide[i2]));
                        //    usr.dr_total = dr_totalx[i2];
                        //    usr.cr_total = cr_total[i2];

                        //    //usr.dr_total = dr_totalx[i];
                        //    db.Entry(usr).State = EntityState.Modified;
                        //}

                        
                    entry en22 = db.entries.Find(entry_id);
                        //foreach (var en22 in ents2)
                        //{
                            en22.dr_total = dr_totalx;
                            en22.cr_total = total;

                            db.Entry(en22).State = EntityState.Modified;
                            db.SaveChanges();

                            //db.entries.Add(en22);
                        //}
                        try
                        {
                            TempData["Message"] = "Entries Saved! ";
                            ModelState.Clear();
                        }
                        catch
                        {
                            TempData["Message"] = "Error addding Entry. ";
                            //this->logger->write_message("error", "Error adding " . current_entry_type['name'] . " Entry number " . full_entry_number(entry_type_id, data_number) . " since failed updating debit and credit total")
                            return Content("Error addding Entry.");
                        }
                    }

                    /* Success */



                    /* Showing success message in show() method since message is too long for storing it in session */

                    //this->logger->write_message("success", "Added " . current_entry_type['name'] . " Entry number " . full_entry_number(entry_type_id, data_number) . " [id:" . entry_id . "]");
                    return RedirectToAction("add/" + id);

                }

            }

                return View(RedirectToAction("All"));
        }
        

        #endregion

        #region Reports

        public ActionResult BalanceSheet()
        {
            //var albums = db.AccountGroups.Include(a => a.ledgers);
            var albums = new List<AccountGroup>();
            for (int i = 0; i < 10; i++)
            {
                albums.Add(new AccountGroup { name = i.ToString() });
            }
            ViewBag.Assets = db.AccountGroups.Where(a => a.id == 1 || a.parent_id == 1).Include("ledgers");
            ViewBag.Amounts = db.ledgers;

            return View(db.AccountGroups.Where(a => a.id <= 4).Include("ledgers"));
        }
        public ActionResult ProfitAndLoss()
        {
            var albums = new List<AccountGroup>();
            for (int i = 0; i < 10; i++)
            {
                albums.Add(new AccountGroup { name = i.ToString() });
            }
            ViewBag.Assets = db.AccountGroups.Where(a => a.id == 1 || a.parent_id == 1).Include("ledgers");
            ViewBag.Amounts = db.ledgers;

            return View(db.AccountGroups.Where(a => a.id <= 4).Include("ledgers"));
        }
        public ActionResult TrialBalance()
        {
            var albums = new List<AccountGroup>();
            for (int i = 0; i < 10; i++)
            {
                albums.Add(new AccountGroup { name = i.ToString() });
            }
            ViewBag.Assets = db.AccountGroups.Where(a => a.id == 1 || a.parent_id == 1).Include("ledgers");
            ViewBag.Amounts = db.ledgers;

            return View(db.AccountGroups.Where(a => a.id <= 4).Include("ledgers"));
        }
        public ActionResult LedgerStatement()
        {
            return View();
        }


        #endregion

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }


    }
}