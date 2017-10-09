<%@ Page Title="Administrative Home Panel" Language="C#" MasterPageFile="~/AdminPanel/Site.master" AutoEventWireup="true" CodeFile="AdminPanelModified.aspx.cs" Inherits="AdminPanel_AdminPanel" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
    <style type="text/css">
        .style10
    {
        width: 88%;
            margin-left: 1px;
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
        .style14
        {
            font-size: small;
            color: #006600;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
    <p>
        <asp:Label ID="Label2" runat="server" Font-Bold="True" Font-Size="Large" 
            ForeColor="#000099" style="font-size: large" 
            Text="Update &amp; View Trends"></asp:Label>
    </p>
    <p>
        <asp:Label ID="Label1" runat="server" ForeColor="#990000"></asp:Label>
    </p>
    <p>
        <asp:Button ID="Button1" runat="server" onclick="Button1_Click" 
            Text="Validate Records" Width="120px" />
        <br __designer:mapid="247" />
        <br __designer:mapid="248" />
        <asp:Button ID="Button4" runat="server" onclick="Button4_Click" 
            Text="View Daily Tracking Sheet" Width="163px" />
        <br __designer:mapid="24a" />
        <br __designer:mapid="24b" />
        <asp:Button ID="Button2" runat="server" onclick="Button2_Click" 
            Text="View Weekly Tracking Sheet" Width="176px" />
    </p>
    <p>
        &nbsp;</p>
    <p>
        <asp:Label ID="Label41" runat="server" ForeColor="#990000" 
            style="font-weight: 700; color: #000066; font-size: large">Planned Auto-Update</asp:Label>
    </p>
    <p>
        <asp:Label ID="Label42" runat="server" ForeColor="#990000"></asp:Label>
        &nbsp;</p>
        <table __designer:mapid="8e" cellpadding="5" cellspacing="5" class="style10" 
            style="font-family: Calibri; font-size: large; color: #000080;" 
            align="center">
            <tr>
                <td __designer:mapid="a4" class="style12">
                    <asp:Label ID="Label7" runat="server" Text="Increase planned by:"></asp:Label>
                </td>
                <td __designer:mapid="a6" class="style13">
                    <asp:TextBox ID="TextBox2" runat="server" MaxLength="3"></asp:TextBox>
                    <span class="style14">(number only)</span></td>
            </tr>
            <tr __designer:mapid="8f">
                <td __designer:mapid="90" class="style12">
                    <asp:Label ID="Label40" runat="server" Text="Department: "></asp:Label>
                </td>
                <td __designer:mapid="92" class="style13">
                    <asp:DropDownList ID="DropDownList1" runat="server" AutoPostBack="True" 
                        DataSourceID="SqlDataSource1" DataTextField="Department" 
                        DataValueField="DeptID">
                    </asp:DropDownList>
                    &nbsp;<span class="style14">(Update planned based on department)</span><asp:SqlDataSource 
                        ID="SqlDataSource1" runat="server" 
                        ConnectionString="<%$ ConnectionStrings:ConnectionString %>" 
                        
                        SelectCommand="SELECT [DeptID], [Department], [Dept] FROM [Departmental_Table] ORDER BY [Department]">
                    </asp:SqlDataSource>
                </td>
            </tr>
            <tr __designer:mapid="a9">
                <td __designer:mapid="aa" class="style12">
                    &nbsp;</td>
                <td __designer:mapid="ac" class="style13">
                    <asp:Button ID="Button5" runat="server" onclick="Button5_Click" 
                        Text="Update Planned" Width="114px" />
                </td>
            </tr>
            </table>
    <p>
        &nbsp;</p>
<p>
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
</p>
</asp:Content>

