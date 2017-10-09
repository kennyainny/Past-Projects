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

public partial class AdminPanel_AddObserver : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["adminLog"].ToString() != "LoggedIn")
        {
            Server.Transfer("~/Account/adminLogin.aspx");
        }

        if (!Page.IsPostBack)
        {
            Label8.Text = "";
        }
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        int totalNoOfDays1 = DateTime.Now.DayOfYear;// -2;
        int noOfWeeks1 = totalNoOfDays1 / 7;
        int topWeeks1 = totalNoOfDays1 % 7;
        if (topWeeks1 > 0) noOfWeeks1 += 1;


        Label8.Text = "";
        int vals = 0;
        int vals1 = 0;
        bool res = int.TryParse(TextBox2.Text, out vals);
        bool res1 = int.TryParse(TextBox3.Text, out vals1);
        if (DropDownList2.SelectedItem == null)
        {
            Label8.Text = "A Sub-Department does not exist, create one first";

        }
        else if (res && res1)
        {
            SqlDataSource myDataSource1 = new SqlDataSource();
            myDataSource1.ConnectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ToString();
            myDataSource1.InsertCommandType = SqlDataSourceCommandType.Text;


            myDataSource1.InsertCommand = "INSERT INTO Full_Department_Table (DeptID, SubDeptID, Department, Username, Password, Email, LoginCount, Observer, Location, updateWeek, Planned, Actual, Completed, DateUpdated, ";

            myDataSource1.InsertCommand += "Week1, Week2, Week3, Week4, Week5, Week6, Week7, Week8, Week9, Week10, Week11, Week12, Week13, Week14, Week15, Week16, Week17, Week18, ";
            myDataSource1.InsertCommand += "Week19, Week20, Week21, Week22, Week23, Week24, Week25, Week26, Week27, Week28, Week29, Week30, Week31, Week32, Week33, Week34, Week35, ";
            myDataSource1.InsertCommand += "Week36, Week37, Week38, Week39, Week40, Week41, Week42, Week43, Week44, Week45, Week46, Week47, Week48, Week49, Week50, Week51, Week52, Week53) ";


            myDataSource1.InsertCommand += "VALUES (@DeptID, @SubDeptID, @Department, @Username, @Password, @Email, @LoginCount, @Observer, @Location, @updateWeek, @Planned, @Actual, @Completed, @DateUpdated, ";

            myDataSource1.InsertCommand += "@Week1, @Week2, @Week3, @Week4, @Week5, @Week6, @Week7, @Week8, @Week9, @Week10, @Week11, @Week12, @Week13, @Week14, @Week15, @Week16, @Week17, @Week18, ";
            myDataSource1.InsertCommand += "@Week19, @Week20, @Week21, @Week22, @Week23, @Week24, @Week25, @Week26, @Week27, @Week28, @Week29, @Week30, @Week31, @Week32, @Week33, @Week34, @Week35, ";
            myDataSource1.InsertCommand += "@Week36, @Week37, @Week38, @Week39, @Week40, @Week41, @Week42, @Week43, @Week44, @Week45, @Week46, @Week47, @Week48, @Week49, @Week50, @Week51, @Week52, @Week53)";



            myDataSource1.InsertParameters.Add("updateWeek", noOfWeeks1.ToString());
            myDataSource1.InsertParameters.Add("DeptID", DropDownList1.SelectedValue);
            myDataSource1.InsertParameters.Add("SubDeptID", DropDownList2.SelectedValue);
            myDataSource1.InsertParameters.Add("Department", DropDownList1.SelectedItem.ToString());
            myDataSource1.InsertParameters.Add("Username", "unileverng");
            myDataSource1.InsertParameters.Add("Password", "she1234");
            myDataSource1.InsertParameters.Add("Email", "empty");
            myDataSource1.InsertParameters.Add("LoginCount", "0");
            myDataSource1.InsertParameters.Add("Observer", TextBox1.Text);
            myDataSource1.InsertParameters.Add("Location", (DropDownList2.SelectedItem).ToString());
            myDataSource1.InsertParameters.Add("Planned", TextBox3.Text);
            myDataSource1.InsertParameters.Add("Actual", TextBox2.Text);
            myDataSource1.InsertParameters.Add("Completed", (((float)vals / vals1) * 100).ToString("0.0") + "%");            
            myDataSource1.InsertParameters.Add("DateUpdated", "never");

            for (int i = 1; i < 54; i++)
            {
                myDataSource1.InsertParameters.Add("Week" + i, "Not Submitted");
            }

            int rowsAff = 0;
            try
            {
                rowsAff = myDataSource1.Insert();
            }
            catch
            {
                Label8.Text = "Server Error, values could not be parsed!";
            }
            finally
            {
                myDataSource1 = null;
            }
            if (rowsAff != 1)
            {
                Label8.Text = "Server Error, values could not be parsed!";
            }
            else
            {
                Label8.Text = TextBox1.Text + " has been successfully added!!!";

                TextBox3.Text = "";
                TextBox2.Text = "";


                SqlDataSource TrackerSource = new SqlDataSource();
                TrackerSource.ConnectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ToString();
                TrackerSource.InsertCommandType = SqlDataSourceCommandType.Text;
                TrackerSource.InsertCommand = "INSERT INTO Admin_Tracker (Name, TimeDate, TaskPerformed, IPAddy)";
                TrackerSource.InsertCommand += " VALUES (@Name, @TimeDate, @TaskPerformed, @IPAddy)";
                TrackerSource.InsertParameters.Add("Name", Session["adminName"].ToString());
                TrackerSource.InsertParameters.Add("TimeDate", DateTime.Now.ToString());
                TrackerSource.InsertParameters.Add("TaskPerformed", "Added an Observer successfully, Observer: " + TextBox1.Text + " - Location: " + DropDownList2.SelectedItem.ToString());
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

                TextBox1.Text = "";

            }
        }
        else
        {
            Label8.Text = "Invalid input, Check 'Actual' and 'Planned'";
        }
    
    }
}