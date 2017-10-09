using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web.Security;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Data.SqlClient;

public partial class Account_Login : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            TextBox1.Text = "";
            TextBox2.Text = "";
        }
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        SqlDataReader userReader;
        string userReaderStr = "SELECT Username, Password, LoginCount FROM Admins_Login";
        SqlConnection userReaderCon = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ToString());
        SqlCommand userReaderCmd = new SqlCommand(userReaderStr, userReaderCon);
        userReaderCmd.CommandType = CommandType.Text;
        int logCount = 0;
        string currPassword = "";
        string currUsername = "";
        string cUsername = "";
        bool existx = false;

        try
        {
            userReaderCon.Open();
            userReader = userReaderCmd.ExecuteReader();
            while (userReader.Read())
            {
                currUsername = Convert.ToString(userReader["Username"]);

                if (currUsername == TextBox1.Text)
                {
                    cUsername = currUsername;
                    currPassword = Convert.ToString(userReader["Password"]);
                    logCount = Convert.ToInt32(userReader["LoginCount"]);
                    existx = true;
                }
            }
            userReader.Close();
            userReaderCon.Close();
        }
        catch
        {
            Label2.Text = "Error! Value could not be parsed!";
        }
        finally
        {

        }
        if (existx)
        {
            if (currPassword == TextBox2.Text)
            {                  
                    Session.Add("adminLog", "LoggedIn");
                    Session.Add("adminName", cUsername);

                    SqlConnection UpdateConn = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ToString());
                    string UpdateStr = "UPDATE Admins_Login SET LoginCount = @LoginCount WHERE (Username = @Username)";

                    SqlCommand UpdateCmd = new SqlCommand(UpdateStr, UpdateConn);
                    UpdateCmd.CommandType = CommandType.Text;
                    UpdateCmd.Parameters.AddWithValue("LoginCount", logCount + 1);
                    UpdateCmd.Parameters.AddWithValue("Username", cUsername);

                    int tte = 0;
                    try
                    {
                        UpdateConn.Open();
                        tte = UpdateCmd.ExecuteNonQuery();
                    }
                    catch
                    {
                        Label2.Text = "Error! Value could not be parsed!";
                    }
                    finally
                    {
                        UpdateConn.Close();
                    }

                    if (tte != 1)
                    {
                        Label2.Text = "Error! Value could not be parsed!";
                    }
                    else
                    {
                        Server.Transfer("~/AdminPanel/AdminPanel.aspx");
                    }
                }            
            else
            {
                Label2.Text = "Password is not correct!";
                Session.Add("adminLog", "");
                Session.Add("adminName", "");
            }
        }
        else
        {
            Label2.Text = "Administrator does not exit!";
            Session.Add("adminLog", "");
            Session.Add("adminName", "");
        }
    }
}
