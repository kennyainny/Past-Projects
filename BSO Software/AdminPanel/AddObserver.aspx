<%@ Page Title="Add a New Observer" Language="C#" MasterPageFile="~/AdminPanel/Site.master" AutoEventWireup="true" CodeFile="AddObserver.aspx.cs" Inherits="AdminPanel_AddObserver" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
    <style type="text/css">

        .style10
    {
        width: 88%;
            margin-left: 95px;
        }
    
    .style12
    {
            width: 219px;
            text-align: right;
        }
    .style13
    {
            width: 436px;
            text-align: left;
        }
        </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
    <p>
        <asp:Label ID="Label1" runat="server" Font-Bold="True" Font-Size="Large" 
            ForeColor="#000099" style="font-size: large" 
            Text="Add an Observer to an existing department:"></asp:Label>
    </p>
    <p>
        <asp:Label ID="Label8" runat="server" ForeColor="#990000" 
            style="font-size: medium"></asp:Label>
    </p>
    <asp:ValidationSummary ID="ValidationSummary1" runat="server" ForeColor="Red" 
        HeaderText="Please fill in the following field(s) properly:" />
    <p>
        <table __designer:mapid="8e" cellpadding="5" cellspacing="5" class="style10" 
            style="font-family: Calibri; font-size: large; color: #000080;" 
            align="center">
            <tr __designer:mapid="8f">
                <td __designer:mapid="90" class="style12">
                    <asp:Label ID="Label2" runat="server" Text="Department: "></asp:Label>
                </td>
                <td __designer:mapid="92" class="style13">
                    <asp:DropDownList ID="DropDownList1" runat="server" AutoPostBack="True" 
                        DataSourceID="SqlDataSource1" DataTextField="Department" 
                        DataValueField="DeptID">
                    </asp:DropDownList>
                    <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
                        ConnectionString="<%$ ConnectionStrings:ConnectionString %>" 
                        SelectCommand="SELECT [DeptID], [Department] FROM [Departmental_Table] ORDER BY [Department]">
                    </asp:SqlDataSource>
                </td>
            </tr>
            <tr __designer:mapid="95">
                <td __designer:mapid="96" class="style12">
                    <asp:Label ID="Label3" runat="server" Text="Sub-Department:"></asp:Label>
                </td>
                <td __designer:mapid="98" class="style13">
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
                </td>
            </tr>
            <tr __designer:mapid="9d">
                <td __designer:mapid="9e" class="style12">
                    <asp:Label ID="Label4" runat="server" Text="Name of new Observer:"></asp:Label>
                </td>
                <td __designer:mapid="a0" class="style13">
                    <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" 
                        ControlToValidate="TextBox1" 
                        ErrorMessage="Type in the name of the new Observer" ForeColor="Red">*</asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr __designer:mapid="a3">
                <td __designer:mapid="a4" class="style12">
                    <asp:Label ID="Label7" runat="server" Text="New Actual value:"></asp:Label>
                </td>
                <td __designer:mapid="a6" class="style13">
                    <asp:TextBox ID="TextBox2" runat="server" MaxLength="3"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" 
                        ControlToValidate="TextBox2" ErrorMessage="Type in the Actual Value" 
                        ForeColor="Red">*</asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr __designer:mapid="a9">
                <td __designer:mapid="aa" class="style12">
                    <asp:Label ID="Label5" runat="server" Text="New Planned:"></asp:Label>
                </td>
                <td __designer:mapid="ac" class="style13">
                    <asp:TextBox ID="TextBox3" runat="server" MaxLength="3"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                        ControlToValidate="TextBox3" ErrorMessage="Type in the Planned Value" 
                        ForeColor="Red">*</asp:RequiredFieldValidator>
                </td>
            </tr>
            </table>
    </p>
    <p>
        <asp:Button ID="Button1" runat="server" onclick="Button1_Click" Text="Add" />
    </p>
    <p>
    </p>
</asp:Content>

