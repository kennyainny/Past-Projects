<%@ Page Title="Adminnistrators' Login" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeFile="adminLogin.aspx.cs" Inherits="Account_Login" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
    <style type="text/css">

        .style1
        {
            width: 28%;
            margin-left: 0px;
        }
        .style2
        {            text-align: center;
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
            text-align: center;
        }
    </style>
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <h2 class="style2">
        &nbsp;</h2>
    <h2 class="style2">
        Administrators Only&nbsp;</h2>
    <asp:ValidationSummary ID="ValidationSummary1" runat="server" ForeColor="Red" 
                HeaderText="Fill in the following fields:" 
        style="text-align: center" />
        <p>
        <table align="center" cellpadding="5" cellspacing="5" class="style3" 
                style="background-color: #C0C0C0">
            <tr>
                <td colspan="2" class="style4">
                        <asp:Label ID="Label40" runat="server" Font-Bold="True" 
                            Font-Names="Microsoft Sans Serif" ForeColor="Maroon" Text="Administrator Login" 
                            style="font-size: large"></asp:Label>
                    </td>
            </tr>
            <tr>
                <td colspan="2" class="style4">
                    <asp:Label ID="Label2" runat="server" ForeColor="Red" style="font-size: medium"></asp:Label>
                    </td>
            </tr>
            <tr>
                <td class="style4">
                    <asp:Label ID="Label38" runat="server" Text="Username:" ForeColor="Maroon"></asp:Label>
                </td>
                <td style="text-align: left">
                    <asp:TextBox ID="TextBox1" runat="server" MaxLength="15"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                        ControlToValidate="TextBox1" ErrorMessage="Username" ForeColor="Red">*</asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td class="style4">
                    <asp:Label ID="Label39" runat="server" Text="Password:" ForeColor="Maroon"></asp:Label>
                </td>
                <td style="text-align: left">
                    <asp:TextBox ID="TextBox2" runat="server" MaxLength="15" TextMode="Password"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" 
                        ControlToValidate="TextBox2" ErrorMessage="Password" ForeColor="Red">*</asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td class="style4">
                    &nbsp;</td>
                <td style="text-align: left">
                    <asp:Button ID="Button1" runat="server" onclick="Button1_Click" Text="Login" />
                </td>
            </tr>
            <tr>
                <td colspan="2" class="style4">
                    &nbsp;</td>
            </tr>
    </table>
    <p>
        &nbsp;<p>
    &nbsp;</asp:Content>