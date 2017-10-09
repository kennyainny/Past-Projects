using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Application.Web.helpers;
using System.Data.Entity;
using Application.Web.Models;
using System.Web.Mvc;
using Application.Web.Controllers;

namespace Application.Web.Models
{
    public class AccountSystemModels
    {
        private acct db = new acct();
        custom_helper customHelp = new custom_helper();
        public int get_ledger_balance(int ledger_id)
	    {
                ledger pds =  get_op_balance(ledger_id);
                int op_bal = Convert.ToInt32(pds.op_balance);
                string op_bal_type = pds.type.ToString();

            double dr_total = get_dr_total(ledger_id);
            int cr_total = get_cr_total(ledger_id);
            int dr_total2 = Convert.ToInt32(dr_total);
            int total = customHelp.float_ops(dr_total2, cr_total, "-");
            if (op_bal_type == "D")
                total = customHelp.float_ops(total, op_bal, "+");
            else
                total = customHelp.float_ops(total, op_bal, "-"); 

            return total;
	    }

        public ledger get_op_balance(int ledger_id)
        {
            var opBal = db.ledgers.Where(a => a.id == ledger_id).First();
            return opBal;
        }

        public int get_dr_total(int ledger_id)
        {
            decimal? drTotal = new decimal();
            //var drTotal = db.Database.ExecuteSqlCommand("SELECT dr_total + dbo.entry_items.amount AS 'asas' FROM dbo.entry_items JOIN dbo.entries ON entries.id = entry_items.entry_id WHERE ledger_id = @p0 AND dc = @p1", ledger_id, "D");
            //ViewBag.total = db.entries.Include(i => i.entry_items.Where(e => e.ledger_id == ledger_id && e.dc == "D")).Sum(d => d.dr_total);
            try
                {
                    drTotal = db.entry_items.Where(q => q.ledger_id == ledger_id && q.dc == "D").Sum((d => d.amount));
                     int drTotal2 = Convert.ToInt32(drTotal);
                      return drTotal2;
                    
                }
            catch
            {
                return 0;
            }
            
        }


        public int get_cr_total(int ledger_id)
        {
            try
            {
                var crTotal = db.entry_items.Where(q => q.ledger_id == ledger_id && q.dc == "C").Sum((d => d.amount));
                int crTotal2 = Convert.ToInt32(crTotal);

                return crTotal2;
            }
            catch
            {
                return 0;
            }
                
            //var crTotal = db.entries.Include(i => i.entry_items.Where(e => e.ledger_id == ledger_id && e.dc == "C")).Sum(d => d.dr_total);
            
        }

    }
}