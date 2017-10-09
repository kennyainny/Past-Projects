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

public partial class AdminPanel_CreateAdminaspx : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["adminLog"].ToString() != "LoggedIn")
        {
            Server.Transfer("~/Account/adminLogin.aspx");
        }
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        if (TextBox3.Text != TextBox2.Text)
        {
            Label7.Text = "Password and Confirmation do not match!!!";
        }
        else
        {
            SqlDataReader userReader;
            string userReaderStr = "SELECT Username FROM Admins_Login";
            SqlConnection userReaderCon = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ToString());
            SqlCommand userReaderCmd = new SqlCommand(userReaderStr, userReaderCon);
            userReaderCmd.CommandType = CommandType.Text;

            string tempstr = null;
            bool existx = false;
            int rowsAff = 0;

            SqlDataSource myDataSource = new SqlDataSource();

            try
            {
                userReaderCon.Open();
                userReader = userReaderCmd.ExecuteReader();
                while (userReader.Read())
                {
                    tempstr = Convert.ToString(userReader["Username"]);
                    if (tempstr == TextBox1.Text)
                    {
                        //tempstr = "exist";
                        existx = true;
                        //redirect
                    }
                }
                userReader.Close();
                userReaderCon.Close();

                if (existx)
                {
                    Label7.Text = "User already exist!";
                }
                else
                {
                    myDataSource.ConnectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ToString();
                    myDataSource.InsertCommandType = SqlDataSourceCommandType.Text;
                    myDataSource.InsertCommand = " INSERT INTO Admins_Login (Username, Password, Sec_Ques, Sec_Ans, LoginCount)";
                    myDataSource.InsertCommand += " VALUES (@Username, @Password, @Sec_Ques, @Sec_Ans, @LoginCount)";
                    myDataSource.InsertParameters.Add("Username", TextBox1.Text);
                    myDataSource.InsertParameters.Add("Password", TextBox2.Text);
                    myDataSource.InsertParameters.Add("Sec_Ques", TextBox4.Text);
                    myDataSource.InsertParameters.Add("Sec_Ans", TextBox5.Text);
                    myDataSource.InsertParameters.Add("LoginCount", "0");

                    rowsAff = myDataSource.Insert();

                }
            }
            catch
            {
                Label7.Text = "Server Error, please try again later!!!";
            }
            finally
            {
                myDataSource = null;
            }
            if (rowsAff != 1)
            {
                Label7.Text = "Server Error, please try again later!!!";
            }
            else
            {
                Label7.Text = "Administrator successfully created!";


                SqlDataSource TrackerSource = new SqlDataSource();
                TrackerSource.ConnectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ToString();
                TrackerSource.InsertCommandType = SqlDataSourceCommandType.Text;
                TrackerSource.InsertCommand = "INSERT INTO Admin_Tracker (Name, TimeDate, TaskPerformed, IPAddy)";
                TrackerSource.InsertCommand += " VALUES (@Name, @TimeDate, @TaskPerformed, @IPAddy)";
                TrackerSource.InsertParameters.Add("Name", Session["adminName"].ToString());
                TrackerSource.InsertParameters.Add("TimeDate", DateTime.Now.ToString());
                TrackerSource.InsertParameters.Add("TaskPerformed", "An administrator was successfully created, Name: " + TextBox1.Text);
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

                    TextBox2.Text = "";
                    TextBox3.Text = "";
                    TextBox4.Text = "";
                    TextBox5.Text = "";
                }
            }

        }
    }
}