<%@ Page Title="Create a New Administrator" Language="C#" MasterPageFile="~/AdminPanel/Site.master" AutoEventWireup="true" CodeFile="CreateAdmin.aspx.cs" Inherits="AdminPanel_CreateAdminaspx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
    <style type="text/css">
        .style5
        {
            text-align: right;
        }
        .style6
        {
            text-align: left;
            margin-left: 280px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
    <p>
        <asp:Label ID="Label1" runat="server" Font-Bold="True" 
            Font-Names="Microsoft Sans Serif" ForeColor="#000099" 
            Text="Create a new Administrator" style="font-size: large"></asp:Label>
    </p>
    <asp:ValidationSummary ID="ValidationSummary1" runat="server" ForeColor="Red" 
        HeaderText="Please make sure the following field(s) are filled:" />
    <p>
        <asp:Label ID="Label7" runat="server" Font-Size="Small" ForeColor="Red" 
            style="font-size: medium" Text="Password and Confirmation must match"></asp:Label>
    </p>
    <table cellpadding="5" cellspacing="3" class="style2" 
        style="font-family: Calibri; font-size: large; color: #000080" 
        align="center">
        <tr>
            <td class="style5">
                <asp:Label ID="Label2" runat="server" Text="Username:"></asp:Label>
            </td>
            <td class="style6">
                <asp:TextBox ID="TextBox1" runat="server" MaxLength="20"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                    ControlToValidate="TextBox1" ErrorMessage="Username" ForeColor="Red">*</asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td class="style5">
                <asp:Label ID="Label4" runat="server" Text="Password:"></asp:Label>
            </td>
            <td class="style6">
                <asp:TextBox ID="TextBox2" runat="server" MaxLength="20" TextMode="Password"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" 
                    ControlToValidate="TextBox2" ErrorMessage="Password" ForeColor="Red">*</asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td class="style5">
                <asp:Label ID="Label5" runat="server" Text="Confirm Password:"></asp:Label>
            </td>
            <td class="style6">
                <asp:TextBox ID="TextBox3" runat="server" MaxLength="20" TextMode="Password"></asp:TextBox>
                <asp:CompareValidator ID="CompareValidator1" runat="server" 
                    ControlToCompare="TextBox2" ControlToValidate="TextBox3" 
                    ErrorMessage="Confirm is not equal to password" ForeColor="Red">*</asp:CompareValidator>
            </td>
        </tr>
        <tr>
            <td class="style5">
                <asp:Label ID="Label6" runat="server" Text="Security Question:"></asp:Label>
            </td>
            <td class="style6">
                <asp:TextBox ID="TextBox4" runat="server" MaxLength="20"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" 
                    ControlToValidate="TextBox4" ErrorMessage="Security question" 
                    ForeColor="Red">*</asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td class="style5">
                <asp:Label ID="Label3" runat="server" Text="Security Answer:"></asp:Label>
            </td>
            <td class="style6">
                <asp:TextBox ID="TextBox5" runat="server" MaxLength="20"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" 
                    ControlToValidate="TextBox5" ErrorMessage="Security question" 
                    ForeColor="Red">*</asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td class="style5">
                &nbsp;</td>
            <td class="style6">
                &nbsp;</td>
        </tr>
        <tr>
            <td colspan="2" class="style6">
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                <asp:Button ID="Button1" runat="server" onclick="Button1_Click" 
                    Text="Create Admin" />
            </td>
        </tr>
        <tr>
            <td colspan="2" class="style6">
            </td>
        </tr>
    </table>
    <p>
        &nbsp;</p>
</asp:Content>

