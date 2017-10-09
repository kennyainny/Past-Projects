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


public partial class _Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["DBLogStatus"].ToString() != "LoggedIn")
        {
            Server.Transfer("~/Account/Login.aspx");
        }

        if (!Page.IsPostBack)
        {
            RadioButtonList1.SelectedIndex = -1;
            Button1.Visible = false;
        }
        else
        {
            MultiView1.ActiveViewIndex = RadioButtonList1.SelectedIndex;
            Button1.Visible = true;
        }


        SqlDataReader readActPlanComp;
        SqlConnection readActPlanCompCon = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ToString());

        string readActPlanCompStr = "SELECT Actual, Planned, Completed FROM Full_Department_Table WHERE (NameID = @NameID)";
        SqlCommand readActPlanCompCmd = new SqlCommand(readActPlanCompStr, readActPlanCompCon);
        readActPlanCompCmd.CommandType = CommandType.Text;
        readActPlanCompCmd.Parameters.AddWithValue("NameID", Convert.ToInt32(Session["nameID"].ToString()));

        try
        {
            readActPlanCompCon.Open();
            readActPlanComp = readActPlanCompCmd.ExecuteReader();
            readActPlanComp.Read();
            Label46.Text = readActPlanComp["Actual"].ToString();
            Label48.Text = readActPlanComp["Planned"].ToString();
            Label49.Text = readActPlanComp["Completed"].ToString();
            readActPlanComp.Close();
            readActPlanCompCon.Close();
        }
        catch
        {
            Server.Transfer("Error.aspx");
        }
        finally
        {

        }

        int totalNoOfDayz = DateTime.Now.DayOfYear+2;
        int noOfWeekz = totalNoOfDayz / 7;
        int topWeekz = totalNoOfDayz % 7;
        if (topWeekz > 0) noOfWeekz += 1;

        Label47.Text = noOfWeekz.ToString();

    }
    protected void RadioButtonList1_SelectedIndexChanged(object sender, EventArgs e)
    {
        MultiView1.ActiveViewIndex = RadioButtonList1.SelectedIndex;
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        //First updates the general table
        //Implementation code to write to our database
        //SHE_Forms_Database, General_Table

        int duration;

        //if (TextBox2.Text == "") duration = 0;
        //else
        //{
        if (!(int.TryParse(TextBox2.Text, out duration)))
        {
            Label39.Text = "Duration could not be parsed, please enter a valid time, mins!";
        }
        else if (RadioButtonList1.SelectedIndex == 2)
        {

            SqlDataSource NMDataSource = new SqlDataSource();
            NMDataSource.ConnectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ToString();
            NMDataSource.InsertCommandType = SqlDataSourceCommandType.Text;
            NMDataSource.InsertCommand = "INSERT INTO Near_Miss_Table (Observerz, Locatnz, datetime, area_affected, typeofpp,";
            NMDataSource.InsertCommand += " duration, injury, initiator, hazard) VALUES (@Observerz, @Locatnz, @datetime,";
            NMDataSource.InsertCommand += " @area_affected, @typeofpp, @duration, @injury, @initiator, @hazard)";

            NMDataSource.InsertParameters.Add("Observerz", Session["displayN"].ToString());
            NMDataSource.InsertParameters.Add("Locatnz", Session["location"].ToString());
            NMDataSource.InsertParameters.Add("datetime", DateTime.Now.ToString());
            NMDataSource.InsertParameters.Add("area_affected", TextBox1.Text);
            NMDataSource.InsertParameters.Add("typeofpp", DropDownList1.Text);
            NMDataSource.InsertParameters.Add("duration", TextBox2.Text);
            NMDataSource.InsertParameters.Add("injury", DropDownList2.Text);
            NMDataSource.InsertParameters.Add("initiator", TextBox8.Text);
            NMDataSource.InsertParameters.Add("hazard", TextBox9.Text);

            int rowsA = 0;

            try
            {
                rowsA = NMDataSource.Insert();
            }
            catch
            {
                Server.Transfer("Error.aspx");
            }
            finally
            {
                NMDataSource.Dispose();
            }

            if (rowsA != 1)
            {
                Server.Transfer("Error.aspx");
            }
            else Server.Transfer("Confirmation.aspx");
        
        }
        else
        {
            Label39.Text = "";

            if (TextBox2.Text == "") duration = 0;

            SqlDataSource myDataSource = new SqlDataSource();
            myDataSource.ConnectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ToString();
            myDataSource.InsertCommandType = SqlDataSourceCommandType.Text;
            myDataSource.InsertCommand = "INSERT INTO General_Table (Observer, Department, SubDept, AreaInspected, Duration,";
            myDataSource.InsertCommand += " Defaulter, Status, PPE, PofP, RofP, Orderliness, ToolsEquip, Procedures,";
            myDataSource.InsertCommand += " Others, Comment, Correction, Prevention, Injury, WeekCompleted, DateComp, HostAddress)";
            myDataSource.InsertCommand += " VALUES (@Observer, @Department, @SubDept, @AreaInspected, @Duration, @Defaulter,";
            myDataSource.InsertCommand += " @Status, @PPE, @PofP, @RofP, @Orderliness, @ToolsEquip, @Procedures,";
            myDataSource.InsertCommand += " @Others, @Comment, @Correction, @Prevention, @Injury, @WeekCompleted, @DateComp, @HostAddress)";

            myDataSource.InsertParameters.Add("Observer", Session["displayN"].ToString());
            myDataSource.InsertParameters.Add("Department", Session["deptID"].ToString());
            myDataSource.InsertParameters.Add("SubDept", Session["location"].ToString());
            myDataSource.InsertParameters.Add("AreaInspected", TextBox1.Text);
            myDataSource.InsertParameters.Add("Duration", TextBox2.Text);
            myDataSource.InsertParameters.Add("Defaulter", DropDownList1.Text);

            string ActStatus;
            if (RadioButtonList1.SelectedIndex == 0)
            {
                ActStatus = "All safe";
                myDataSource.InsertParameters.Add("Comment", TextBox7.Text);
                myDataSource.InsertParameters.Add("Status", ActStatus);
            }
            else
            {
                ActStatus = "Some unsafe";
                myDataSource.InsertParameters.Add("Comment", TextBox6.Text);
                myDataSource.InsertParameters.Add("Status", ActStatus);
            }

            myDataSource.InsertParameters.Add("PPE", DropDownList5.Text);
            myDataSource.InsertParameters.Add("PofP", DropDownList6.Text);
            myDataSource.InsertParameters.Add("RofP", DropDownList7.Text);
            myDataSource.InsertParameters.Add("Orderliness", DropDownList8.Text);
            myDataSource.InsertParameters.Add("ToolsEquip", DropDownList4.Text);
            myDataSource.InsertParameters.Add("Procedures", DropDownList3.Text);

            myDataSource.InsertParameters.Add("Others", TextBox3.Text);
            myDataSource.InsertParameters.Add("Correction", TextBox4.Text);
            myDataSource.InsertParameters.Add("Prevention", TextBox5.Text);
            myDataSource.InsertParameters.Add("Injury", DropDownList2.Text);
            myDataSource.InsertParameters.Add("DateComp", DateTime.Today.ToShortDateString());
            myDataSource.InsertParameters.Add("HostAddress", Request.UserHostName.ToString());

            int rowsAffected = 0;
            int wer = 1;

            //General table ends here.

            //Weeks table starts here

            //Full dept table starts here
            //It updates the Full_Department_Table

            //to compute no of weeks of d year

            int totalNoOfDays = DateTime.Now.DayOfYear+2;
            int noOfWeeks = totalNoOfDays / 7;
            int topWeeks = totalNoOfDays % 7;
            if (topWeeks > 0) noOfWeeks += 1;


            myDataSource.InsertParameters.Add("WeekCompleted", noOfWeeks.ToString());

            int nameID;
            nameID = Convert.ToInt32(Session["nameID"].ToString());

            string leave;
            if (CheckBox1.Checked) leave = "On leave";
            else leave = "Submitted";

            SqlDataReader readerWk, readerAct;

            string readWkSqlStr = "EXEC ReadWeeks " + nameID;

            SqlConnection readWkCon = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ToString());
            SqlCommand readWkCmd = new SqlCommand(readWkSqlStr, readWkCon);
            readWkCmd.CommandType = CommandType.Text;

            SqlConnection writeWkCon = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ToString());

            SqlConnection readActualCon = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ToString());

            SqlConnection writeFullCon = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ToString());


            try
            {
                readWkCon.Open();
                readerWk = readWkCmd.ExecuteReader();
                readerWk.Read();
                string tempstr = Convert.ToString(readerWk[noOfWeeks - 1]);
                readerWk.Close();
                readWkCon.Close();

                string writeWkStr = null;
                switch (noOfWeeks)
                {
                    case 1:
                        {
                            writeWkStr = "UPDATE Full_Department_Table SET Week1 = @Week1 WHERE (NameID = @NameID)";
                        }
                        break;

                    case 2:
                        {
                            writeWkStr = "UPDATE Full_Department_Table SET Week2 = @Week2 WHERE (NameID = @NameID)";
                        }
                        break;

                    case 3:
                        {
                            writeWkStr = "UPDATE Full_Department_Table SET Week3 = @Week3 WHERE (NameID = @NameID)";
                        }
                        break;

                    case 4:
                        {
                            writeWkStr = "UPDATE Full_Department_Table SET Week4 = @Week4 WHERE (NameID = @NameID)";
                        }
                        break;

                    case 5:
                        {
                            writeWkStr = "UPDATE Full_Department_Table SET Week5 = @Week5 WHERE (NameID = @NameID)";
                        }
                        break;

                    case 6:
                        {
                            writeWkStr = "UPDATE Full_Department_Table SET Week6 = @Week6 WHERE (NameID = @NameID)";
                        }
                        break;

                    case 7:
                        {
                            writeWkStr = "UPDATE Full_Department_Table SET Week7 = @Week7 WHERE (NameID = @NameID)";
                        }
                        break;

                    case 8:
                        {
                            writeWkStr = "UPDATE Full_Department_Table SET Week8 = @Week8 WHERE (NameID = @NameID)";
                        }
                        break;

                    case 9:
                        {
                            writeWkStr = "UPDATE Full_Department_Table SET Week9 = @Week9 WHERE (NameID = @NameID)";
                        }
                        break;

                    case 10:
                        {
                            writeWkStr = "UPDATE Full_Department_Table SET Week10 = @Week10 WHERE (NameID = @NameID)";
                        }
                        break;

                    case 11:
                        {
                            writeWkStr = "UPDATE Full_Department_Table SET Week11 = @Week11 WHERE (NameID = @NameID)";
                        }
                        break;

                    case 12:
                        {
                            writeWkStr = "UPDATE Full_Department_Table SET Week12 = @Week12 WHERE (NameID = @NameID)";
                        }
                        break;

                    case 13:
                        {
                            writeWkStr = "UPDATE Full_Department_Table SET Week13 = @Week13 WHERE (NameID = @NameID)";
                        }
                        break;

                    case 14:
                        {
                            writeWkStr = "UPDATE Full_Department_Table SET Week14 = @Week14 WHERE (NameID = @NameID)";
                        }
                        break;

                    case 15:
                        {
                            writeWkStr = "UPDATE Full_Department_Table SET Week15 = @Week15 WHERE (NameID = @NameID)";
                        }
                        break;

                    case 16:
                        {
                            writeWkStr = "UPDATE Full_Department_Table SET Week16 = @Week16 WHERE (NameID = @NameID)";
                        }
                        break;

                    case 17:
                        {
                            writeWkStr = "UPDATE Full_Department_Table SET Week17 = @Week17 WHERE (NameID = @NameID)";
                        }
                        break;

                    case 18:
                        {
                            writeWkStr = "UPDATE Full_Department_Table SET Week18 = @Week18 WHERE (NameID = @NameID)";
                        }
                        break;

                    case 19:
                        {
                            writeWkStr = "UPDATE Full_Department_Table SET Week19 = @Week19 WHERE (NameID = @NameID)";
                        }
                        break;

                    case 20:
                        {
                            writeWkStr = "UPDATE Full_Department_Table SET Week20 = @Week20 WHERE (NameID = @NameID)";
                        }
                        break;

                    case 21:
                        {
                            writeWkStr = "UPDATE Full_Department_Table SET Week21 = @Week21 WHERE (NameID = @NameID)";
                        }
                        break;

                    case 22:
                        {
                            writeWkStr = "UPDATE Full_Department_Table SET Week22 = @Week22 WHERE (NameID = @NameID)";
                        }
                        break;

                    case 23:
                        {
                            writeWkStr = "UPDATE Full_Department_Table SET Week23 = @Week23 WHERE (NameID = @NameID)";
                        }
                        break;

                    case 24:
                        {
                            writeWkStr = "UPDATE Full_Department_Table SET Week24 = @Week24 WHERE (NameID = @NameID)";
                        }
                        break;

                    case 25:
                        {
                            writeWkStr = "UPDATE Full_Department_Table SET Week25 = @Week25 WHERE (NameID = @NameID)";
                        }
                        break;

                    case 26:
                        {
                            writeWkStr = "UPDATE Full_Department_Table SET Week26 = @Week26 WHERE (NameID = @NameID)";
                        }
                        break;

                    case 27:
                        {
                            writeWkStr = "UPDATE Full_Department_Table SET Week27 = @Week27 WHERE (NameID = @NameID)";
                        }
                        break;

                    case 28:
                        {
                            writeWkStr = "UPDATE Full_Department_Table SET Week28 = @Week28 WHERE (NameID = @NameID)";
                        }
                        break;

                    case 29:
                        {
                            writeWkStr = "UPDATE Full_Department_Table SET Week29 = @Week29 WHERE (NameID = @NameID)";
                        }
                        break;

                    case 30:
                        {
                            writeWkStr = "UPDATE Full_Department_Table SET Week30 = @Week30 WHERE (NameID = @NameID)";
                        }
                        break;

                    case 31:
                        {
                            writeWkStr = "UPDATE Full_Department_Table SET Week31 = @Week31 WHERE (NameID = @NameID)";
                        }
                        break;

                    case 32:
                        {
                            writeWkStr = "UPDATE Full_Department_Table SET Week32 = @Week32 WHERE (NameID = @NameID)";
                        }
                        break;

                    case 33:
                        {
                            writeWkStr = "UPDATE Full_Department_Table SET Week33 = @Week33 WHERE (NameID = @NameID)";
                        }
                        break;

                    case 34:
                        {
                            writeWkStr = "UPDATE Full_Department_Table SET Week34 = @Week34 WHERE (NameID = @NameID)";
                        }
                        break;

                    case 35:
                        {
                            writeWkStr = "UPDATE Full_Department_Table SET Week35 = @Week35 WHERE (NameID = @NameID)";
                        }
                        break;

                    case 36:
                        {
                            writeWkStr = "UPDATE Full_Department_Table SET Week36 = @Week36 WHERE (NameID = @NameID)";
                        }
                        break;

                    case 37:
                        {
                            writeWkStr = "UPDATE Full_Department_Table SET Week37 = @Week37 WHERE (NameID = @NameID)";
                        }
                        break;

                    case 38:
                        {
                            writeWkStr = "UPDATE Full_Department_Table SET Week38 = @Week38 WHERE (NameID = @NameID)";
                        }
                        break;
                    case 39:
                        {
                            writeWkStr = "UPDATE Full_Department_Table SET Week39 = @Week39 WHERE (NameID = @NameID)";
                        }
                        break;

                    case 40:
                        {
                            writeWkStr = "UPDATE Full_Department_Table SET Week40 = @Week40 WHERE (NameID = @NameID)";
                        }
                        break;

                    case 41:
                        {
                            writeWkStr = "UPDATE Full_Department_Table SET Week41 = @Week41 WHERE (NameID = @NameID)";
                        }
                        break;

                    case 42:
                        {
                            writeWkStr = "UPDATE Full_Department_Table SET Week42 = @Week42 WHERE (NameID = @NameID)";
                        }
                        break;

                    case 43:
                        {
                            writeWkStr = "UPDATE Full_Department_Table SET Week43 = @Week43 WHERE (NameID = @NameID)";
                        }
                        break;

                    case 44:
                        {
                            writeWkStr = "UPDATE Full_Department_Table SET Week44 = @Week44 WHERE (NameID = @NameID)";
                        }
                        break;

                    case 45:
                        {
                            writeWkStr = "UPDATE Full_Department_Table SET Week45 = @Week45 WHERE (NameID = @NameID)";
                        }
                        break;

                    case 46:
                        {
                            writeWkStr = "UPDATE Full_Department_Table SET Week46 = @Week46 WHERE (NameID = @NameID)";
                        }
                        break;

                    case 47:
                        {
                            writeWkStr = "UPDATE Full_Department_Table SET Week47 = @Week47 WHERE (NameID = @NameID)";
                        }
                        break;

                    case 48:
                        {
                            writeWkStr = "UPDATE Full_Department_Table SET Week48 = @Week48 WHERE (NameID = @NameID)";
                        }
                        break;
                    case 49:
                        {
                            writeWkStr = "UPDATE Full_Department_Table SET Week49 = @Week49 WHERE (NameID = @NameID)";
                        }
                        break;

                    case 50:
                        {
                            writeWkStr = "UPDATE Full_Department_Table SET Week50 = @Week50 WHERE (NameID = @NameID)";
                        }
                        break;

                    case 51:
                        {
                            writeWkStr = "UPDATE Full_Department_Table SET Week51 = @Week51 WHERE (NameID = @NameID)";
                        }
                        break;

                    case 52:
                        {
                            writeWkStr = "UPDATE Full_Department_Table SET Week52 = @Week52 WHERE (NameID = @NameID)";
                        }
                        break;

                    default:
                        writeWkStr = "UPDATE Full_Department_Table SET Week53 = @Week53 WHERE (NameID = @NameID)";
                        break;
                }

                SqlCommand writeWkCmd = new SqlCommand(writeWkStr, writeWkCon);
                writeWkCmd.CommandType = CommandType.Text;
                writeWkCmd.Parameters.AddWithValue("Week" + noOfWeeks, leave);
                writeWkCmd.Parameters.AddWithValue("NameID", nameID);
                writeWkCon.Open();
                int tte = writeWkCmd.ExecuteNonQuery();
                writeWkCon.Close();


                string readActualStr = "SELECT Actual, Planned FROM Full_Department_Table WHERE (NameID = @NameID)";
                SqlCommand readActualCmd = new SqlCommand(readActualStr, readActualCon);
                readActualCmd.CommandType = CommandType.Text;
                readActualCmd.Parameters.AddWithValue("NameID", nameID);
                readActualCon.Open();
                readerAct = readActualCmd.ExecuteReader();
                readerAct.Read();
                int actual = Convert.ToInt32(readerAct["Actual"]);
                int plano = Convert.ToInt32(readerAct["Planned"]);
                readerAct.Close();
                readActualCon.Close();

                actual += 1;
                //int planned=0;

                // if (tempstr == "Not Submitted") planned = plano + 1;
                //else planned=plano;

                string complete = (((float)actual / plano) * 100).ToString("0.0") + "%";

                string writeFullStr = "UPDATE Full_Department_Table SET Actual = @Actual, Completed = @Completed WHERE (NameID = @NameID)";
                SqlCommand writeFullCmd = new SqlCommand(writeFullStr, writeFullCon);
                writeFullCmd.CommandType = CommandType.Text;
                //writeFullCmd.Parameters.AddWithValue("Planned", planned);
                writeFullCmd.Parameters.AddWithValue("Actual", actual);
                writeFullCmd.Parameters.AddWithValue("Completed", complete);
                writeFullCmd.Parameters.AddWithValue("NameID", nameID);
                writeFullCon.Open();
                wer = writeFullCmd.ExecuteNonQuery();
                writeFullCon.Close();

                rowsAffected = myDataSource.Insert();

            }
            catch
            {
                Server.Transfer("Error.aspx");
            }
            finally
            {
                myDataSource.Dispose();
            }

            if (rowsAffected != 1 || wer != 1)
            {
                Server.Transfer("Error.aspx");
            }

            else Server.Transfer("Confirmation.aspx");
        }
    }
}
