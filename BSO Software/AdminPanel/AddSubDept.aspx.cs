using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.Security;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;

public partial class AdminPanel_AddSubDept : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["adminLog"].ToString() != "LoggedIn")
        {
            Server.Transfer("~/Account/adminLogin.aspx");
        }

        if (!Page.IsPostBack)
        {
            Label2.Text = "";
        }
    }
    protected void Button4_Click(object sender, EventArgs e)
    {
        Label2.Text = "";

        SqlDataSource myDataSource2 = new SqlDataSource();
        myDataSource2.ConnectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ToString();
        myDataSource2.InsertCommandType = SqlDataSourceCommandType.Text;

        myDataSource2.InsertCommand = "INSERT INTO SubDepartment_Tables (DeptID, SubDept) VALUES (@DeptID, @SubDept)";

        myDataSource2.InsertParameters.Add("DeptID", DropDownList1.SelectedValue);
        myDataSource2.InsertParameters.Add("SubDept", TextBox1.Text);

        int rowsAff = 0;
        try
        {
            rowsAff = myDataSource2.Insert();
        }
        catch
        {
            Label2.Text = "Server Error, value could not be parsed!!!";
        }
        finally
        {
            myDataSource2 = null;
        }
        if (rowsAff != 1)
        {
            Label2.Text = "Server Error, value could not be parsed!";
        }
        else
        {
            Label2.Text = TextBox1.Text + " has been successfully added to the " + DropDownList1.SelectedItem.ToString() + " Department!";

            SqlDataSource TrackerSource = new SqlDataSource();
            TrackerSource.ConnectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ToString();
            TrackerSource.InsertCommandType = SqlDataSourceCommandType.Text;
            TrackerSource.InsertCommand = "INSERT INTO Admin_Tracker (Name, TimeDate, TaskPerformed, IPAddy)";
            TrackerSource.InsertCommand += " VALUES (@Name, @TimeDate, @TaskPerformed, @IPAddy)";
            TrackerSource.InsertParameters.Add("Name", Session["adminName"].ToString());
            TrackerSource.InsertParameters.Add("TimeDate", DateTime.Now.ToString());
            TrackerSource.InsertParameters.Add("TaskPerformed", "A sub-department was added successfully, Sub-Dept: " + TextBox1.Text + " - Dept: " + DropDownList1.SelectedItem.ToString());
            TrackerSource.InsertParameters.Add("IPAddy", Request.UserHostAddress.ToString());
            try
            {
                int rowsAffected = TrackerSource.Insert();
            }
            catch
            {
                Label2.Text = "Server Error, value could not be parsed!";
            }
            finally
            {
                TrackerSource.Dispose();
            }

            TextBox1.Text = "";
        }

    }
}