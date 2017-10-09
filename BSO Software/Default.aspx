<%@ Page Title="Unilever SHE Nigeria" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeFile="Default.aspx.cs" Inherits="_Default" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
    <style type="text/css">
        .style2
        {
            width: 91%;
        }
        .style3
        {
            width: 353px;
            text-align: right;
        }
        .style4
        {
            text-align: right;
        }
        .style5
        {
            text-align: left;
        }
        .style6
        {
            color: #006600;
            font-size: small;
        }
        .style7
        {
            color: #FF0066;
        }
        .style9
        {
            color: #006600;
        }
        .style10
        {
            text-align: left;
            width: 233px;
        }
        .style11
        {
            text-align: left;
            width: 166px;
        }
        .style12
        {
            font-size: small;
        }
        </style>
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <h2 class="style2">
        </h2>
    <asp:Panel ID="Panel3" runat="server" Height="40px" 
        style="text-align: left; color: #00FF00; font-size: small; font-weight: 700; margin-left: 646px; background-color: #FFFFFF" 
        Width="269px">
        &nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:Label ID="Label45" runat="server" style="text-align: left" Text="Actual:"></asp:Label>
        <asp:Label ID="Label46" runat="server" style="text-align: left"></asp:Label>
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:Label ID="Label42" runat="server" style="text-align: left" 
            Text="Week:"></asp:Label>
        <asp:Label ID="Label47" runat="server" style="text-align: left"></asp:Label>
        <br />
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:Label ID="Label43" runat="server" style="text-align: left" Text="Planned:"></asp:Label>
        <asp:Label ID="Label48" runat="server" style="text-align: left"></asp:Label>
        &nbsp;<asp:Label ID="Label44" runat="server" style="text-align: left" 
            Text="Completed:"></asp:Label>
        <asp:Label ID="Label49" runat="server" style="text-align: left"></asp:Label>
    </asp:Panel>
    <h2 class="style2">
        Please fill out our Behavioural Safety Observation Form as given below:&nbsp;</h2>
    <p>
        <asp:Label ID="Label39" runat="server" ForeColor="Maroon"></asp:Label>
    </p>
    <p class="style6">
        Compulsory fields are indicated by *</p>
    <asp:ValidationSummary ID="ValidationSummary1" runat="server" ForeColor="Red" 
        HeaderText="The following field(s) is/are compulsory:" 
        style="text-align: center" />
    <table cellpadding="5" class="style2" 
        
    style="font-family: Arial, Helvetica, sans-serif; font-size: medium; font-weight: normal; font-style: normal; font-variant: normal; color: #000080" 
    align="center">
        <tr>
            <td class="style4">
                <asp:Label ID="Label2" runat="server" Text="Area Inspected:"></asp:Label>
            </td>
            <td class="style10">
                <asp:TextBox ID="TextBox1" runat="server" MaxLength="40" Height="22px" 
                    Width="128px"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" 
                    ControlToValidate="TextBox1" ErrorMessage="Area Inspected" ForeColor="Red">*</asp:RequiredFieldValidator>
                <span class="style9">* <span class="style12">(location of inspection)</span></span></td>
            <td class="style4">
                <asp:Label ID="Label4" runat="server" Text="Type of People:"></asp:Label>
            </td>
            <td class="style11">
                <asp:DropDownList ID="DropDownList1" runat="server" Height="22px" Width="120px">
                    <asp:ListItem Value="Nill">Select One</asp:ListItem>
                    <asp:ListItem>Employee</asp:ListItem>
                    <asp:ListItem>Contractor</asp:ListItem>
                    <asp:ListItem>Visitor</asp:ListItem>
                    <asp:ListItem>Third Party</asp:ListItem>
                    <asp:ListItem>KDSR</asp:ListItem>
                    <asp:ListItem>Others</asp:ListItem>
                </asp:DropDownList>
                <asp:CompareValidator ID="CompareValidator1" runat="server" 
                    ControlToValidate="DropDownList1" ErrorMessage="Type of People" ForeColor="Red" 
                    Operator="NotEqual" ValueToCompare="Nill">*</asp:CompareValidator>
                <span class="style9">*</span></td>
        </tr>
        <tr>
            <td class="style4">
                <asp:Label ID="Label3" runat="server" Text="Duration of Observation:"></asp:Label>
            </td>
            <td class="style10">
                <asp:TextBox ID="TextBox2" runat="server" MaxLength="4" Width="128px" 
                    ToolTip="time in minutes" Height="22px"></asp:TextBox>
            &nbsp;<span class="style6">*(requires numbers only)</span></td>
            <td class="style4">
                <asp:Label ID="Label5" runat="server" Text="Injury Potential:"></asp:Label>
            </td>
            <td class="style11">
                <asp:DropDownList ID="DropDownList2" runat="server" Height="22px" Width="120px">
                    <asp:ListItem>No injury</asp:ListItem>
                    <asp:ListItem>Minor</asp:ListItem>
                    <asp:ListItem>Serious</asp:ListItem>
                    <asp:ListItem>Fatality</asp:ListItem>
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td class="style4">
                <asp:Label ID="Label6" runat="server" Text="Select Act:"></asp:Label>
            </td>
            <td class="style10">
                <br />
                <asp:RadioButtonList ID="RadioButtonList1" runat="server" AutoPostBack="True" 
                    CellPadding="5" CellSpacing="5" Height="42px" 
                    onselectedindexchanged="RadioButtonList1_SelectedIndexChanged" 
                    RepeatDirection="Horizontal" Width="333px" 
                    style="font-weight: 700; font-size: small">
                    <asp:ListItem Value="0">All safe</asp:ListItem>
                    <asp:ListItem Value="1">Some unsafe</asp:ListItem>
                    <asp:ListItem Value="2">Near-Miss</asp:ListItem>
                </asp:RadioButtonList>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" 
                    ControlToValidate="RadioButtonList1" ErrorMessage="Select Act" ForeColor="Red">*</asp:RequiredFieldValidator>
                <span class="style9">*</span></td>
            <td class="style4">
                &nbsp;</td>
            <td class="style11">
                <asp:CheckBox ID="CheckBox1" runat="server" Text="On leave?" TextAlign="Left" />
            </td>
        </tr>
        </table>
    <p>
        <asp:MultiView ID="MultiView1" runat="server">
            <asp:View ID="View1" runat="server">
                <table cellpadding="5" class="style2" style="color: #000080" align="center" 
                    cellspacing="3">
                    <tr>
                        <td class="style3">
                            <asp:Label ID="Label9" runat="server" Font-Names="Calibri" Font-Size="Large" 
                                ForeColor="Navy" Text="Comment on safe behaviour:" 
                                style="font-family: Arial, Helvetica, sans-serif; font-size: medium; "></asp:Label>
                        </td>
                        <td style="text-align: left">
                            <asp:TextBox ID="TextBox7" runat="server" MaxLength="150" TextMode="MultiLine" 
                                Width="299px"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" 
                                ControlToValidate="TextBox7" ErrorMessage="Safe Comment" 
                                SetFocusOnError="True" ForeColor="Red">*</asp:RequiredFieldValidator>
                            <span class="style9">*</span></td>
                    </tr>
                </table>
&nbsp;
            </asp:View>
            <asp:View ID="View2" runat="server">
                <h2 class="style2">
                    Identify unsafe observation categories by selecting from the lists below:</h2>
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<br />
                <table cellpadding="5" cellspacing="3" class="style7" 
                    
                    style="font-family: Arial, Helvetica, sans-serif; font-size: medium; height: 376px; color: #000080;" 
                    align="center" frame="border">
                    <tr>
                        <td class="style4">
                            <asp:Label ID="Label8" runat="server" Font-Size="Medium" ForeColor="Navy" 
                                Text="PPE:"></asp:Label>
                        </td>
                        <td class="style5">
                            <asp:DropDownList ID="DropDownList5" runat="server" Height="25px" Width="275px">
                                <asp:ListItem Value="      ">Select one....</asp:ListItem>
                                <asp:ListItem Value="Head">Head</asp:ListItem>
                                <asp:ListItem Value="Eyes and Face">Eyes and Face</asp:ListItem>
                                <asp:ListItem Value="Respiratory System">Respiratory System</asp:ListItem>
                                <asp:ListItem Value="Arms and Hands">Arms and Hands</asp:ListItem>
                                <asp:ListItem Value="Trunk">Trunk</asp:ListItem>
                                <asp:ListItem Value="Legs and Feet">Legs and Feet</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td class="style4">
                            <asp:Label ID="Label12" runat="server" Font-Size="Medium" ForeColor="Navy" 
                                Text="Positions of People:"></asp:Label>
                        </td>
                        <td class="style5">
                            <asp:DropDownList ID="DropDownList6" runat="server" Height="25px" Width="275px">
                                <asp:ListItem Value="      ">Select one....</asp:ListItem>
                                <asp:ListItem Value="Striking against or getting struck">Striking against or getting struck</asp:ListItem>
                                <asp:ListItem Value="Caught in or between objects">Caught in or between objects</asp:ListItem>
                                <asp:ListItem Value="Falling">Falling</asp:ListItem>
                                <asp:ListItem Value="Contacting extreme">Contacting extreme</asp:ListItem>
                                <asp:ListItem Value="Contacting electric current">Contacting electric current</asp:ListItem>
                                <asp:ListItem Value="Inhaling/swallowing hazardous substance">Inhaling/swallowing hazardous substance</asp:ListItem>
                                <asp:ListItem Value="Repetitive motion">Repetitive motion</asp:ListItem>
                                <asp:ListItem Value="Akward positions (ergonomics)">Akward positions (ergonomics)</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td class="style4">
                            <asp:Label ID="Label13" runat="server" Font-Size="Medium" ForeColor="Navy" 
                                Text="Reactions of People:"></asp:Label>
                        </td>
                        <td class="style5">
                            <asp:DropDownList ID="DropDownList7" runat="server" Height="25px" Width="275px">
                                <asp:ListItem Value="     ">Select one....</asp:ListItem>
                                <asp:ListItem Value="Adjusting PPE">Adjusting PPE?</asp:ListItem>
                                <asp:ListItem Value="Adjusting position">Adjusting position?</asp:ListItem>
                                <asp:ListItem Value="Rearranging job">Rearranging job?</asp:ListItem>
                                <asp:ListItem Value="Stopping activity">Stopping activity?</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td class="style4">
                            <asp:Label ID="Label14" runat="server" Font-Size="Medium" ForeColor="Navy" 
                                Text="Orderliness:"></asp:Label>
                        </td>
                        <td class="style5">
                            <asp:DropDownList ID="DropDownList8" runat="server" Height="25px" Width="275px">
                                <asp:ListItem Value="      ">Select one....</asp:ListItem>
                                <asp:ListItem Value="Housekeeping standards not known">Housekeeping standards not known</asp:ListItem>
                                <asp:ListItem Value="Housekeeping standards not understood">Housekeeping standards not understood</asp:ListItem>
                                <asp:ListItem Value="Housekeeping standards not followed">Housekeeping standards not followed</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td class="style4">
                            <asp:Label ID="Label16" runat="server" Font-Size="Medium" ForeColor="Navy" 
                                Text="Tools and Equipment:"></asp:Label>
                        </td>
                        <td class="style5">
                            <asp:DropDownList ID="DropDownList4" runat="server" Height="25px" Width="275px">
                                <asp:ListItem Value="     ">Select one....</asp:ListItem>
                                <asp:ListItem Value="Not right for the job">Not right for the job</asp:ListItem>
                                <asp:ListItem Value="Not used correctly">Not used correctly</asp:ListItem>
                                <asp:ListItem Value="Not in good, safe condition">Not in good, safe condition</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td class="style4">
                            <asp:Label ID="Label15" runat="server" Font-Size="Medium" ForeColor="Navy" 
                                Text="Procedures:"></asp:Label>
                        </td>
                        <td class="style5">
                            <asp:DropDownList ID="DropDownList3" runat="server" Height="25px" Width="275px">
                                <asp:ListItem Value="     ">Select one....</asp:ListItem>
                                <asp:ListItem Value="Procedures not available">Procedures not available</asp:ListItem>
                                <asp:ListItem Value="Procedures not adequate">Procedures not adequate</asp:ListItem>
                                <asp:ListItem Value="Procedures not known">Procedures not known</asp:ListItem>
                                <asp:ListItem Value="Procedures not understood">Procedures not understood</asp:ListItem>
                                <asp:ListItem Value="Procedures not followed">Procedures not followed</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td class="style4">
                            <asp:Label ID="Label10" runat="server" ForeColor="Navy" 
                                Text="Others, please specify:"></asp:Label>
                        </td>
                        <td class="style5">
                            <asp:TextBox ID="TextBox3" runat="server" MaxLength="50" Width="269px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="style4">
                            <asp:Label ID="Label27" runat="server" Font-Size="Large" ForeColor="Navy" 
                                Text="Unsafe act description/comment:" style="font-size: medium"></asp:Label>
                        </td>
                        <td class="style5">
                            <asp:TextBox ID="TextBox6" runat="server" MaxLength="150" TextMode="MultiLine" 
                                Width="299px"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" 
                                ControlToValidate="TextBox6" ErrorMessage="Unsafe comment." 
                                ForeColor="Red">*</asp:RequiredFieldValidator>
                            <span class="style9">*</span></td>
                    </tr>
                    <tr>
                        <td class="style4">
                            <asp:Label ID="Label11" runat="server" Font-Size="Large" ForeColor="Navy" 
                                Text="Immediate corrective action taken: " style="font-size: medium"></asp:Label>
                        </td>
                        <td class="style5">
                            <asp:TextBox ID="TextBox4" runat="server" MaxLength="150" TextMode="MultiLine" 
                                Width="299px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="style4">
                            <asp:Label ID="Label26" runat="server" Font-Size="Large" ForeColor="Navy" 
                                Text="Action to prevent recurrence:" style="font-size: medium"></asp:Label>
                        </td>
                        <td class="style5">
                            <asp:TextBox ID="TextBox5" runat="server" MaxLength="150" TextMode="MultiLine" 
                                Width="299px"></asp:TextBox>
                        </td>
                    </tr>
                </table>
            </asp:View>
            <asp:View ID="View3" runat="server">
                <br />
                <table align="center" cellpadding="5" cellspacing="3" class="style2" 
                    frame="border">
                    <tr>
                        <td class="style4">
                            <asp:Label ID="Label40" runat="server" Font-Size="Large" ForeColor="Navy" 
                                style="font-size: medium" Text="Initiator:"></asp:Label>
                        </td>
                        <td class="style5">
                            <asp:TextBox ID="TextBox8" runat="server" Height="22px" MaxLength="40" 
                                Width="128px"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" 
                                ControlToValidate="TextBox8" ErrorMessage="Name of Initiator" ForeColor="Red">*</asp:RequiredFieldValidator>
                            <span class="style9">*</span></td>
                    </tr>
                    <tr>
                        <td class="style4">
                            <asp:Label ID="Label41" runat="server" Font-Size="Large" ForeColor="Navy" 
                                style="font-size: medium" Text="Brief description of Near-Miss:"></asp:Label>
                        </td>
                        <td class="style5">
                            <asp:TextBox ID="TextBox9" runat="server" MaxLength="150" TextMode="MultiLine" 
                                Width="299px" Height="27px"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" 
                                ControlToValidate="TextBox9" ErrorMessage="Potential Hazard" ForeColor="Red">*</asp:RequiredFieldValidator>
                            <span class="style9">*</span></td>
                    </tr>
                </table>
            </asp:View>
        </asp:MultiView>
    </p>
    <p>
        &nbsp;</p>
    <p>
        <asp:Button ID="Button1" runat="server" Text="Submit" onclick="Button1_Click" />
    </p>
    <p>
    </p>
</asp:Content>
