<%@ Page Title="Add a New Sub-Department" Language="C#" MasterPageFile="~/AdminPanel/Site.master" AutoEventWireup="true" CodeFile="AddSubDept.aspx.cs" Inherits="AdminPanel_AddSubDept" %>

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
            Text="Add a Sub-Department to an existing department:"></asp:Label>
    </p>
    <p>
        <asp:Label ID="Label2" runat="server" ForeColor="#CC3300" 
            style="font-size: medium"></asp:Label>
    </p>
    <asp:ValidationSummary ID="ValidationSummary1" runat="server" ForeColor="Red" 
        HeaderText="Please make sure the following field(s) are filled:" />
    <p>
        <table __designer:mapid="f3" cellpadding="5" cellspacing="5" class="style10" 
            style="font-family: Calibri; font-size: large; color: #000080;" 
            align="center">
            <tr __designer:mapid="f4">
                <td __designer:mapid="f5" class="style12">
                    <asp:Label ID="Label4" runat="server" Text="Department: "></asp:Label>
                </td>
                <td __designer:mapid="f7" class="style13">
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
            <tr __designer:mapid="fa">
                <td __designer:mapid="fb" class="style12">
                    <asp:Label ID="Label3" runat="server" Text="Sub-Department:"></asp:Label>
                </td>
                <td __designer:mapid="fd" class="style13">
                    <asp:TextBox ID="TextBox1" runat="server" Height="22px"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                        ControlToValidate="TextBox1" ErrorMessage="Type in the Sub-Department" 
                        ForeColor="Red">*</asp:RequiredFieldValidator>
                </td>
            </tr>
        </table>
    </p>
    <p>
        <asp:Button ID="Button4" runat="server" onclick="Button4_Click" Text="Add" />
    </p>
    <p>
    </p>
</asp:Content>

