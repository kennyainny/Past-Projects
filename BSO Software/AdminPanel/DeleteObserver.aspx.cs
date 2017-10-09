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

public partial class AdminPanel_DeleteObserver : System.Web.UI.Page
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
    protected void Button1_Click(object sender, EventArgs e)
    {
        Label2.Text = "";

        if (DropDownList2.SelectedItem == null)
        {
            Label2.Text = "A Sub-Department does not exist, create one first";

        }
        else if (DropDownList3.SelectedItem == null)
        {
            Label2.Text = "An Observer does not exist, create one first";
        }
        else
        {
            SqlDataSource myDataSource4 = new SqlDataSource();
            myDataSource4.ConnectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ToString();
            myDataSource4.DeleteCommandType = SqlDataSourceCommandType.Text;

            myDataSource4.DeleteCommand = "DELETE FROM Full_Department_Table WHERE (NameID = @NameID)";

            myDataSource4.DeleteParameters.Add("NameID", DropDownList3.SelectedValue);

            int rowsAff = 0;
            try
            {
                rowsAff = myDataSource4.Delete();
            }
            catch
            {
                Label2.Text = "Server Error, value could not be parsed!!!";
            }
            finally
            {
                myDataSource4 = null;
            }
            if (rowsAff != 1)
            {
                Label2.Text = "Server Error, value could not be parsed!!!";
            }
            else
            {
                Label2.Text = DropDownList3.SelectedItem + " has been successfully deleted!!!";

                SqlDataSource TrackerSource = new SqlDataSource();
                TrackerSource.ConnectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ToString();
                TrackerSource.InsertCommandType = SqlDataSourceCommandType.Text;
                TrackerSource.InsertCommand = "INSERT INTO Admin_Tracker (Name, TimeDate, TaskPerformed, IPAddy)";
                TrackerSource.InsertCommand += " VALUES (@Name, @TimeDate, @TaskPerformed, @IPAddy)";
                TrackerSource.InsertParameters.Add("Name", Session["adminName"].ToString());
                TrackerSource.InsertParameters.Add("TimeDate", DateTime.Now.ToString());
                TrackerSource.InsertParameters.Add("TaskPerformed", "An Observer was successfully deleted, Name: " + DropDownList3.SelectedItem.ToString() + " - Location: " + DropDownList2.SelectedItem.ToString());
                TrackerSource.InsertParameters.Add("IPAddy", Request.UserHostAddress.ToString());
                try
                {
                    int rowsAffected = TrackerSource.Insert();
                }
                catch
                {
                    Label3.Text = "Server Error, value could not be parsed!";
                }
                finally
                {
                    TrackerSource.Dispose();
                }
            }
        }
    }
}