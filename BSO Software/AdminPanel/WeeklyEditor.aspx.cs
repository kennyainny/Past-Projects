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

public partial class AdminPanel_WeeklyEditor : System.Web.UI.Page
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
    protected void Button2_Click(object sender, EventArgs e)
    {
        Label2.Text = "";

        if (DropDownList8.SelectedItem == null)
        {
            Label2.Text = "An Observer does not exist, create one first";
        }
        else
        {
            // = @Actual, Planned = @Planned, Completed = @Completed WHERE (NameID = @NameID)";
            SqlConnection UpdateConn = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ToString());
            string UpdateStr = "UPDATE Full_Department_Table SET Week" + DropDownList4.SelectedValue + "= @Week" + DropDownList4.SelectedValue + " WHERE (NameID = @NameID)";

            SqlCommand UpdateCmd = new SqlCommand(UpdateStr, UpdateConn);
            UpdateCmd.CommandType = CommandType.Text;
            UpdateCmd.Parameters.AddWithValue("Week" + DropDownList4.SelectedValue, DropDownList5.SelectedValue);
            UpdateCmd.Parameters.AddWithValue("NameID", Convert.ToInt32(DropDownList8.SelectedValue));
            try
            {
                UpdateConn.Open();
                int tte = UpdateCmd.ExecuteNonQuery();
                UpdateConn.Close();

                if (tte != 1)
                {
                    Label2.Text = "Error, value could not be parsed!";
                }

                Label2.Text = "Status was successfully changed!";


                SqlDataSource TrackerSource = new SqlDataSource();
                TrackerSource.ConnectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ToString();
                TrackerSource.InsertCommandType = SqlDataSourceCommandType.Text;
                TrackerSource.InsertCommand = "INSERT INTO Admin_Tracker (Name, TimeDate, TaskPerformed, IPAddy)";
                TrackerSource.InsertCommand += " VALUES (@Name, @TimeDate, @TaskPerformed, @IPAddy)";
                TrackerSource.InsertParameters.Add("Name", Session["adminName"].ToString());
                TrackerSource.InsertParameters.Add("TimeDate", DateTime.Now.ToString());
                TrackerSource.InsertParameters.Add("TaskPerformed", "Changed an Observer status: " + DropDownList8.SelectedItem.ToString() + " - Location: " + DropDownList7.SelectedItem.ToString() + ", to '" + DropDownList5.SelectedValue +"'");
                TrackerSource.InsertParameters.Add("IPAddy", Request.UserHostAddress.ToString());

                try
                {
                    int rowsAffected = TrackerSource.Insert();
                }
                catch
                {
                    //Server.Transfer("error.aspx");
                }
                finally
                {
                    TrackerSource.Dispose();
                }


            }
            catch
            {
                Label7.Text = "Error, value could not be parsed!";
            }
            finally
            {
                UpdateConn.Close();

            }

        }
    }
}