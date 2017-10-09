using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class AdminPanel_ActualPlannedChartt : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["adminLog"].ToString() != "LoggedIn")
        {
            Server.Transfer("~/Account/adminLogin.aspx");
        }
    }
}