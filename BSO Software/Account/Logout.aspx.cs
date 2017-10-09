using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Account_Logout : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Session.Add("DBname", "");
        Session.Add("displayN", "");
        Session.Add("nameID", "");
        Session.Add("DBpass", "");
        Session.Add("DBLogStatus", "");
        Session.Add("location", "");

        Session.Add("adminLog", "");
        Session.Add("adminName", "");
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        Server.Transfer("~/Account/Login.aspx");
    }
}