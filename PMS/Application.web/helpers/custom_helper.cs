using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Application.Web.helpers;
using System.Data.Entity;
using Application.Web.Models;
using System.Web.Mvc;
using Application.Web.Controllers;

namespace Application.Web.helpers
{
    public class custom_helper
    {
        
        private acct db = new acct();
        
	public int entry_type_name_to_id( string entry_type_name)
	{
        try
        {
            var itemID =  db.entry_types.Where(a => a.label.Contains(entry_type_name)).First();

            int ID = itemID.id;
            return ID;
        }
        catch
        {
            return 0;
            
        }
            

		
	}

        
	public int? next_entry_number(int entry_type_id)
	{
		var last_no_q = db.entries.Where(a => a.entry_type == entry_type_id).Max(a => a.number);
		
		if (last_no_q != null || last_no_q == 0)
		{
            var row = last_no_q;
			var last_no = row;
			last_no++;
			return last_no;
		} else {
			return 1;
		}
	}


        public int float_ops(int param1 = 0, int param2 = 0, string opa = "")
	{
		double result = 0;
		double paramq1 = param1 * 100;
		double paramq2 = param2 * 100;
        paramq1 = Math.Round(paramq1, 0);
		paramq2 = Math.Round(paramq2, 0);
		switch (opa)
		{
		case "+":
			result = paramq1 + paramq2;
			break;
		case "-":
			result = paramq1 - paramq2;
			break;
		case "==":
			if (paramq1 == paramq2) 
				return 1;
			else
				return 0;
			break;
		case "!=":
			if (paramq1 != paramq2)
				return 1;
			else
				return 0;
			break;
		case "<":
			if (paramq1 < paramq2)
				return 1;
			else
				return 0;
			break;
		case ">":
			if (paramq1 > paramq2)
				return 1;
			else
				return 0;
			break;

		}
		result = result/100;
        int result2 = Convert.ToInt32(result);
		return result2;
	}
    }
}