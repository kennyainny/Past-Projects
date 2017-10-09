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

public partial class AdminPanel_AdminPanel : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["adminLog"].ToString() != "LoggedIn")
        {
            Server.Transfer("~/Account/adminLogin.aspx");
        }

        if (!Page.IsPostBack)
        {
            Label1.Text = "";
        }
    }
    protected void Button1_Click(object sender, EventArgs e)
    {        
        try
        {
            for (int i = 1; i < 11; i++)
            {
                SqlDataReader readPlanned;
                SqlConnection readPlannedCon = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ToString());
                string readPlannedStr = "SELECT SUM(Planned) AS totalPlanned, SUM(Actual) AS totalActual FROM Full_Department_Table WHERE (DeptID = @DeptID)";

                SqlCommand readPlanCmd = new SqlCommand(readPlannedStr, readPlannedCon);

                readPlanCmd.CommandType = CommandType.Text;

                readPlanCmd.Parameters.AddWithValue("DeptID", i);

                readPlannedCon.Open();

                readPlanned = readPlanCmd.ExecuteReader();

                readPlanned.Read();
    
                int planned = Convert.ToInt32(readPlanned["totalPlanned"]);
                int actual = Convert.ToInt32(readPlanned["totalActual"]);
                string complete;
                if (planned != 0)
                {
                    complete = (((float)actual / (float)planned) * 100).ToString("0.0");
                }
                else
                {
                    complete = "0";
                }
                readPlanned.Close();
                readPlannedCon.Close();
                               
                                
                SqlConnection UpdateConn = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ToString());
                string UpdateStr = "UPDATE Departmental_Table SET Planned = @Planned, Actual = @Actual, Complete = @Complete WHERE (DeptID = @DeptID)";

                SqlCommand UpdateCmd = new SqlCommand(UpdateStr, UpdateConn);
                UpdateCmd.CommandType = CommandType.Text;

                //UpdateCmd.Parameters.AddWithValue("Dept", regionDept);
                UpdateCmd.Parameters.AddWithValue("Planned", planned);
                UpdateCmd.Parameters.AddWithValue("Actual", actual);
                UpdateCmd.Parameters.AddWithValue("Complete", complete);
                UpdateCmd.Parameters.AddWithValue("DeptID", i);

                UpdateConn.Open();
                int tte = UpdateCmd.ExecuteNonQuery();
                UpdateConn.Close();

                if (tte != 1)
                {
                    Label1.Text = "Error, value could not be parsed!";
                }

            }


            SqlDataSource TrackerSource = new SqlDataSource();
            TrackerSource.ConnectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ToString();
            TrackerSource.InsertCommandType = SqlDataSourceCommandType.Text;
            TrackerSource.InsertCommand = "INSERT INTO Admin_Tracker (Name, TimeDate, TaskPerformed, IPAddy)";
            TrackerSource.InsertCommand += " VALUES (@Name, @TimeDate, @TaskPerformed, @IPAddy)";
            TrackerSource.InsertParameters.Add("Name", Session["adminName"].ToString());
            TrackerSource.InsertParameters.Add("TimeDate", DateTime.Now.ToString());
            TrackerSource.InsertParameters.Add("TaskPerformed", "Modified and updated the Audit Trend in the database successfully");
            TrackerSource.InsertParameters.Add("IPAddy", Request.UserHostAddress.ToString());


            try
            {
                int rowsAffected = TrackerSource.Insert();
                Label1.Text = "Database successfully updated!";
            }
            catch
            {
                Label1.Text = "Error, value could not be parsed!";
            }
            finally
            {
                TrackerSource.Dispose();
            }

        }
        catch
        {
            Label1.Text = "Error, value could not be parsed!";
        }
        finally
        {

        }

    }
    protected void Button2_Click(object sender, EventArgs e)
    {
        Label1.Text = "Audit Trend was successfully opened in a new window!";

        string url = "AuditWeeklyIndicator.aspx";
        string fullURL = "window.open('" + url + "', '_blank', 'height=500,width=800,status=yes,toolbar=yes,menubar=yes,location=yes,scrollbars=yes,resizable=yes,titlebar=yes' );";
        ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", fullURL, true);
    }
    
    protected void Button4_Click(object sender, EventArgs e)
    {
        Label1.Text = "Daily Track record was successfully opened in a new window!";

        string url = "GeneralBSO.aspx";
        string fullURL = "window.open('" + url + "', '_blank', 'height=500,width=800,status=yes,toolbar=yes,menubar=yes,location=yes,scrollbars=yes,resizable=yes,titlebar=yes' );";
        ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", fullURL, true);
    }
    protected void Button5_Click(object sender, EventArgs e)
    {
        int plannedIncrement=0;
        if (!(int.TryParse(TextBox2.Text, out plannedIncrement)))
        {
            Label42.Text = "Increament value could not be parsed, enter a valid integer!";
        }
        else if (DropDownList1.SelectedIndex == null)
        {
            Label42.Text = "Select a department first!";
        }
        else
        {
            int totalNoOfDays = DateTime.Now.DayOfYear;// -2;
            int noOfWeeks = totalNoOfDays / 7;
            int topWeeks = totalNoOfDays % 7;
            if (topWeeks > 0) noOfWeeks += 1;

            int updatedWeekz = 0;
            int nameId = 0;
            int actValue = 0;
            int planValue = 0;

            SqlDataReader readPlan;
            SqlConnection readPlanCon = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ToString());

            string readPlanStr = "SELECT NameID, updateWeek, Planned, Actual FROM Full_Department_Table";
            SqlCommand readPlannedCmd = new SqlCommand(readPlanStr, readPlanCon);
            readPlannedCmd.CommandType = CommandType.Text;

            int writeSuccess = 0;

            try
            {
                readPlanCon.Open();

                readPlan = readPlannedCmd.ExecuteReader();
                readPlan.Read();
                do
                {
                    string writeFullStr = "UPDATE Full_Department_Table SET updateWeek = @updateWeek, Planned = @Planned, Actual = @Actual, Completed = @Completed WHERE (NameID = @NameID)";

                    SqlConnection writeFullCon = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ToString());

                    SqlCommand writeFullCmd = new SqlCommand(writeFullStr, writeFullCon);
                    writeFullCmd.CommandType = CommandType.Text;

                    updatedWeekz = Convert.ToInt32(readPlan["updateWeek"]);
                    nameId = Convert.ToInt32(readPlan["NameID"]);
                    actValue = Convert.ToInt32(readPlan["Actual"]);
                    planValue = Convert.ToInt32(readPlan["Planned"]);

                    if (updatedWeekz < noOfWeeks)
                    {
                        planValue += 1;
                        updatedWeekz = noOfWeeks;
                        string complete = (((float)actValue / planValue) * 100).ToString("0.0") + "%";
                        writeFullCmd.Parameters.AddWithValue("updateWeek", updatedWeekz);
                        writeFullCmd.Parameters.AddWithValue("Planned", planValue);
                        writeFullCmd.Parameters.AddWithValue("Actual", actValue);
                        writeFullCmd.Parameters.AddWithValue("Completed", complete);
                        writeFullCmd.Parameters.AddWithValue("NameID", nameId);

                        writeFullCon.Open();
                        writeSuccess = writeFullCmd.ExecuteNonQuery();
                        writeFullCon.Close();
                    }

                }
                while (readPlan.Read());

                readPlan.Close();
                readPlanCon.Close();
            }
            catch
            {
                Label42.Text = "Server error, please try again later!";
            }
            finally
            {

            }

        }

    }
}