<%@ Page Title="Observers' Profile Update" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeFile="Register.aspx.cs" Inherits="Account_Register" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
    <style type="text/css">

        .style1
        {
            width: 100%;
        }
        .style1
        {
            height: 23px;
        }
        .style2
        {
            text-align: center;
        }
        .style3
        {
            width: 36%;
            border-style: solid;
            border-width: 3px;
            margin-left: 0px;
        position: relative;
    }
    .style4
    {
        font-size: small;
        color: #006600;
    }
    .style6
    {
            font-size: small;
            color: #00FF00;
            width: 230px;
        }
        .style7
        {
            width: 230px;
        }
        .style8
        {
            color: #006600;
        }
    </style>
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <h2 class="style2">
        &nbsp;</h2>
    <h2 class="style2">
        PLEASE
        UPDATE YOUR PROFILE</h2>
    <center>
        <asp:ValidationSummary ID="ValidationSummary1" runat="server" 
            HeaderText="The following field(s) is/are compulsory:" ForeColor="Red" />
    </center>
    <p class="style4">
        Compulsory fields are indicated by *</p>
<p>
        <br />
        <table align="center" cellpadding="5" cellspacing="5" class="style3">
            <tr>
                <td colspan="2" class="style2">
                        <asp:Label ID="Label9" runat="server" Font-Bold="True" 
                            Font-Names="Microsoft Sans Serif" ForeColor="Lime" 
                            Text="Account Information" style="font-size: large"></asp:Label>
                    </td>
            </tr>
            <tr>
                <td colspan="2" class="style2">
                            <asp:Label ID="Label8" runat="server" Font-Size="Small" ForeColor="Red" 
                                Text="Password and Confirmation must match"></asp:Label>
                    </td>
            </tr>
            <tr>
                <td class="style2">
                        <asp:Label ID="Label1" runat="server" Text="Department: "></asp:Label>
                </td>
                <td style="text-align: left" class="style7">
                        <asp:DropDownList ID="DropDownList1" runat="server" AutoPostBack="True" 
                            DataSourceID="SqlDataSource1" DataTextField="Department" 
                            DataValueField="DeptID">
                        </asp:DropDownList>
                        <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
                            ConnectionString="<%$ ConnectionStrings:ConnectionString %>" 
                            SelectCommand="SELECT [DeptID], [Department] FROM [Departmental_Table] ORDER BY [Department]">
                        </asp:SqlDataSource>
                        <span class="style8">*</span></td>
            </tr>
            <tr>
                <td class="style2">
                        <asp:Label ID="Label2" runat="server" Text="Sub-Department:"></asp:Label>
                </td>
                    <td style="text-align: left" class="style7">
                        <asp:DropDownList ID="DropDownList2" runat="server" AutoPostBack="True" 
                            DataSourceID="SqlDataSource2" DataTextField="SubDept" 
                            DataValueField="SubDeptID">
                        </asp:DropDownList>
                        <asp:SqlDataSource ID="SqlDataSource2" runat="server" 
                            ConnectionString="<%$ ConnectionStrings:ConnectionString %>" 
                            SelectCommand="SELECT [SubDeptID], [DeptID], [SubDept] FROM [SubDepartment_Tables] WHERE ([DeptID] = @DeptID) ORDER BY [SubDept]">
                            <SelectParameters>
                                <asp:ControlParameter ControlID="DropDownList1" Name="DeptID" 
                                    PropertyName="SelectedValue" Type="Int32" />
                            </SelectParameters>
                        </asp:SqlDataSource>
                        <span class="style8">*</span></td>
            </tr>
            <tr>
                <td class="style2">
                        <asp:Label ID="Label3" runat="server" Text="Name of Observer:"></asp:Label>
                    </td>
                <td style="text-align: left" class="style7">
                        <asp:DropDownList ID="DropDownList3" runat="server" AutoPostBack="True" 
                            DataSourceID="SqlDataSource3" DataTextField="Observer" DataValueField="NameID">
                        </asp:DropDownList>
                        <asp:SqlDataSource ID="SqlDataSource3" runat="server" 
                            ConnectionString="<%$ ConnectionStrings:ConnectionString %>" 
                            
                        SelectCommand="SELECT [NameID], [DeptID], [SubDeptID], [Observer] FROM [Full_Department_Table] WHERE (([DeptID] = @DeptID) AND ([SubDeptID] = @SubDeptID) AND ([LoginCount] &lt; @LoginCount)) ORDER BY [Observer]">
                            <SelectParameters>
                                <asp:ControlParameter ControlID="DropDownList1" Name="DeptID" 
                                    PropertyName="SelectedValue" Type="Int32" />
                                <asp:ControlParameter ControlID="DropDownList2" Name="SubDeptID" 
                                    PropertyName="SelectedValue" Type="Int32" />
                                <asp:Parameter DefaultValue="1" Name="LoginCount" Type="Int32" />
                            </SelectParameters>
                        </asp:SqlDataSource>
                        <span class="style8">*</span></td>
            </tr>
            <tr>
                <td class="style2">
                        <asp:Label ID="Label4" runat="server" Text="New Username:"></asp:Label>
                </td>
                <td style="text-align: left" class="style7">
                        <asp:TextBox ID="TextBox1" runat="server" MaxLength="15"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" 
                            ControlToValidate="TextBox1" ErrorMessage="New username" ForeColor="Red">*</asp:RequiredFieldValidator>
                        <span class="style8">*</span></td>
            </tr>
            <tr>
                <td class="style2">
                        &nbsp;</td>
                <td style="text-align: left" class="style6">
                        (username could be a combination of any set of characters. e.g. frank or 
                        frank_2012)</td>
            </tr>
            <tr>
                <td class="style2">
                        <asp:Label ID="Label5" runat="server" Text="New Password:"></asp:Label>
                </td>
                <td style="text-align: left" class="style7">
                        <asp:TextBox ID="TextBox2" runat="server" MaxLength="15" TextMode="Password"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" 
                            ControlToValidate="TextBox2" ErrorMessage="New password" ForeColor="Red">*</asp:RequiredFieldValidator>
                        <span class="style8">*</span></td>
            </tr>
            <tr>
                <td class="style2">
                        &nbsp;</td>
                <td style="text-align: left" class="style6">
                        (password could be a combination of any set of characters. e.g. my_password or 
                        bird-frank02)</td>
            </tr>
            <tr>
                <td class="style2">
                        <asp:Label ID="Label6" runat="server" Text="Confirm Password:"></asp:Label>
                </td>
                <td style="text-align: left" class="style7">
                        <asp:TextBox ID="TextBox3" runat="server" MaxLength="15" TextMode="Password"></asp:TextBox>
                        <asp:CompareValidator ID="CompareValidator1" runat="server" 
                            ControlToCompare="TextBox2" ControlToValidate="TextBox3" 
                            ErrorMessage="Passwords do not match" ForeColor="Red" ValueToCompare="t">*</asp:CompareValidator>
                        <span class="style8">*</span></td>
            </tr>
            <tr>
                <td class="style2">
                        <asp:Label ID="Label7" runat="server" Text="Email:"></asp:Label>
                </td>
                <td style="text-align: left" class="style7">
                        <asp:TextBox ID="TextBox4" runat="server" MaxLength="45"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" 
                            ControlToValidate="TextBox4" ErrorMessage="Email" ForeColor="Red">*</asp:RequiredFieldValidator>
                        <span class="style8">*</span></td>
            </tr>
            <tr>
                <td class="style2">
                        &nbsp;</td>
                <td style="text-align: left" class="style6">
                        (unilever email is preferable. e.g. tessy.kings@unilever.com)</td>
            </tr>
            <tr>
                <td colspan="2" class="style2">
                    &nbsp;</td>
            </tr>
            <tr>
                <td colspan="2" class="style2">
                        <asp:Button ID="Button1" runat="server" onclick="Button1_Click" 
                            Text="Update" />
                </td>
            </tr>
    </table>
        <br />
    </p>
    <p>
        &nbsp;</p>
        </asp:Content>