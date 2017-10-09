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
using System.Threading;

public partial class Account_Register : System.Web.UI.Page
{

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["DBLogStatus"].ToString() != "LoIn")
        {
            Server.Transfer("~/Account/Login.aspx");
        }

        // RegisterUser.ContinueDestinationPageUrl = Request.QueryString["ReturnUrl"];
    }


    protected void Button1_Click(object sender, EventArgs e)
    {
        SqlDataReader userReader;
        string userReaderStr = "SELECT  Username FROM Full_Department_Table";
        SqlConnection userReaderCon = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ToString());
        SqlCommand userReaderCmd = new SqlCommand(userReaderStr, userReaderCon);

        userReaderCmd.CommandType = CommandType.Text;

        string tempstr = null;
        bool IsExist = false;

        //SqlDataSource myDataSource = new SqlDataSource();

        try
        {
            userReaderCon.Open();
            userReader = userReaderCmd.ExecuteReader();

            while (userReader.Read())
            {
                tempstr = Convert.ToString(userReader["Username"]);
                if (tempstr == TextBox1.Text)
                {
                    IsExist = true;
                }
            }

            if (IsExist)
            {
                Label8.Text = "Username has been taken, choose another one!";
            }
            else
            {
                SqlConnection UpdateConn = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ToString());
                string UpdateStr = "UPDATE Full_Department_Table SET Username = @Username, Password = @Password, Email = @Email, LoginCount = @LoginCount, DateUpdated = @DateUpdated WHERE (NameID = @NameID)";
                SqlCommand UpdateCmd = new SqlCommand(UpdateStr, UpdateConn);

                UpdateCmd.CommandType = CommandType.Text;
                UpdateCmd.Parameters.AddWithValue("Username", TextBox1.Text);
                UpdateCmd.Parameters.AddWithValue("Password", TextBox2.Text);
                UpdateCmd.Parameters.AddWithValue("Email", TextBox4.Text);
                UpdateCmd.Parameters.AddWithValue("LoginCount", 1);
                UpdateCmd.Parameters.AddWithValue("DateUpdated", DateTime.Today);
                UpdateCmd.Parameters.AddWithValue("NameID", Convert.ToInt32(DropDownList3.SelectedValue));

                try
                {
                    UpdateConn.Open();
                    int tte = UpdateCmd.ExecuteNonQuery();
                    UpdateConn.Close();

                    if (tte != 1)
                    {
                        Label8.Text = "Error! Value could not be parsed!";
                    }
                    else
                    {
                        Label8.Text = "Profile successfully updated. Redirecting.....";

                        Session.Add("displayN", Convert.ToString(DropDownList3.SelectedItem));
                        Session.Add("nameID", Convert.ToInt32(DropDownList3.SelectedValue));
                        Session.Add("deptID", Convert.ToString(DropDownList1.SelectedItem));
                        Session.Add("DBLogStatus", "LoggedIn");
                        Session.Add("location", Convert.ToString(DropDownList2.SelectedItem));

                        //Thread.Sleep(5000);
                        Server.Transfer("~/Default.aspx");
                    }
                }
                catch
                {
                    Label8.Text = "Value could not be parsed, try again later!";
                }

                userReader.Close();
                userReaderCon.Close();
            }
        }
        catch
        {
            Label8.Text = "Server Error, please try again later!";
        }
        finally
        {
            TextBox2.Text = "";
            TextBox3.Text = "";
            TextBox4.Text = "";
            TextBox1.Text = "";
        }
        //}
    }

}