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

public partial class AdminPanel_EditRecord : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["adminLog"].ToString() != "LoggedIn")
        {
            Server.Transfer("~/Account/adminLogin.aspx");
        }

        if (!Page.IsPostBack)
        {
            Label7.Text = "";
        }
    }
    protected void Button2_Click(object sender, EventArgs e)
    {
        Label7.Text = "";
        int vals = 0;
        int vals2 = 0;
        bool res = int.TryParse(TextBox1.Text, out vals);
        bool res2 = int.TryParse(TextBox2.Text, out vals2);
        if (DropDownList2.SelectedItem == null)
        {
            Label7.Text = "A Sub-Department does not exist, create one first";

        }
        else if (DropDownList3.SelectedItem == null)
        {
            Label7.Text = "An Observer does not exist, create one first";
        }
        else if (res&&res2)
        {
            SqlConnection UpdateConn = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ToString());
            string UpdateStr = "UPDATE Full_Department_Table SET Actual = @Actual, Planned = @Planned, Completed = @Completed WHERE (NameID = @NameID)";

            SqlCommand UpdateCmd = new SqlCommand(UpdateStr, UpdateConn);
            UpdateCmd.CommandType = CommandType.Text;
            UpdateCmd.Parameters.AddWithValue("Actual", vals);
            UpdateCmd.Parameters.AddWithValue("Planned", vals2);
            UpdateCmd.Parameters.AddWithValue("Completed", (((float)vals / (float)vals2) * 100).ToString("0.0") + "%");
            UpdateCmd.Parameters.AddWithValue("NameID", Convert.ToInt32(DropDownList3.SelectedValue));
            try
            {
                UpdateConn.Open();
                int tte = UpdateCmd.ExecuteNonQuery();
                UpdateConn.Close();

                if (tte != 1)
                {
                    Label7.Text = "Error, value could not be parsed!";
                }
                //Label71.ForeColor
                Label7.Text = "Record was updated successfully!";


                SqlDataSource TrackerSource = new SqlDataSource();
                TrackerSource.ConnectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ToString();
                TrackerSource.InsertCommandType = SqlDataSourceCommandType.Text;
                TrackerSource.InsertCommand = "INSERT INTO Admin_Tracker (Name, TimeDate, TaskPerformed, IPAddy)";
                TrackerSource.InsertCommand += " VALUES (@Name, @TimeDate, @TaskPerformed, @IPAddy)";
                TrackerSource.InsertParameters.Add("Name", Session["adminName"].ToString());
                TrackerSource.InsertParameters.Add("TimeDate", DateTime.Now.ToString());
                TrackerSource.InsertParameters.Add("TaskPerformed", "Modified record successfully, Observer: " + DropDownList3.SelectedItem.ToString() + " - Location: " + DropDownList2.SelectedItem.ToString());
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
                TextBox1.Text = "";
                TextBox2.Text = "";
            }

        }
        else
        {
            Label7.Text = "Invalid input, Check 'Actual' and 'Planned'";
            //error msg
        }

    }
}