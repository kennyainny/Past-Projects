using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class SiteMaster : System.Web.UI.MasterPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["adminLog"].ToString() == "LoggedIn")
        {
            HyperLink1.Visible = true;
            Label1.Visible = true;
            Label1.Text = Session["adminName"].ToString() + " (Administrator)";
        }
        else
        {
            HyperLink1.Visible = false;
            Label1.Visible = false;
        }

        Label2.Text = DateTime.Today.ToLongDateString();
    }
}
