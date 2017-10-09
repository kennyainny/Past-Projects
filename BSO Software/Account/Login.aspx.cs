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
        string userReaderStr = "SELECT  NameID, Department, Username, Password, LoginCount, Observer, Location FROM  Full_Department_Table";
        SqlConnection userReaderCon = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ToString());
        SqlCommand userReaderCmd = new SqlCommand(userReaderStr, userReaderCon);
        userReaderCmd.CommandType = CommandType.Text;
        int nameid = 0;
        int logCount = -1;
        string currPassword = "";
        string currUsername = "";
        string displayName = "";
        string locatn = "";
        string dept = "";
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
                    currPassword = Convert.ToString(userReader["Password"]);
                    logCount = Convert.ToInt32(userReader["LoginCount"]);
                    nameid = Convert.ToInt32(userReader["NameID"]);
                    dept = Convert.ToString(userReader["Department"]);
                    displayName = Convert.ToString(userReader["Observer"]);
                    locatn = Convert.ToString(userReader["Location"]);
                    existx = true;
                }
            }
            userReader.Close();
            userReaderCon.Close();
        }
        catch
        {
            Label2.Text = "Please reload!";
        }
        finally
        {
            //TextBox1.Text = "";
            //TextBox2.Text = "";
        }
        if (existx)
        {
            if (currPassword == TextBox2.Text)
            {
                if (logCount < 1)
                {
                    Session.Add("DBLogStatus", "LoIn");
                    //Session.Add("displayN", " ");

                    Server.Transfer("~/Account/Register.aspx");
                }
                else
                {
                    Session.Add("displayN", displayName);
                    Session.Add("nameID", nameid);
                    Session.Add("deptID", dept);
                    Session.Add("DBLogStatus", "LoggedIn");
                    Session.Add("location", locatn);

                    SqlConnection UpdateConn = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ToString());
                    string UpdateStr = "UPDATE Full_Department_Table SET LoginCount = @LoginCount WHERE (NameID = @NameID)";

                    SqlCommand UpdateCmd = new SqlCommand(UpdateStr, UpdateConn);
                    UpdateCmd.CommandType = CommandType.Text;
                    UpdateCmd.Parameters.AddWithValue("LoginCount", logCount + 1);
                    UpdateCmd.Parameters.AddWithValue("NameID", nameid);

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
                        Server.Transfer("~/Default.aspx");
                    }
                }
            }
            else
            {
                Label2.Text = "Password is incorrect!";

                Session.Add("displayN", "");
                Session.Add("nameID", "");
                Session.Add("deptID", "");
                Session.Add("DBLogStatus", "");
                Session.Add("location", "");
            }
        }
        else
        {
            Label2.Text = "Username does not exit!";

            Session.Add("displayN", "");
            Session.Add("nameID", "");
            Session.Add("deptID", "");
            Session.Add("DBLogStatus", "");
            Session.Add("location", "");
        }
    }
    protected void Button2_Click(object sender, EventArgs e)
    {
        Server.Transfer("~/About.aspx");
    }
}
