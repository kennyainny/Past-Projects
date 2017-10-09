<%@ Page Title="Administrative Home Panel" Language="C#" MasterPageFile="~/AdminPanel/Site.master" AutoEventWireup="true" CodeFile="AdminPanel.aspx.cs" Inherits="AdminPanel_AdminPanel" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
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
            Text="Validate Records" />
    </p>
<p>
        <asp:Button ID="Button4" runat="server" onclick="Button4_Click" 
            Text="View Daily Tracking Sheet" />
    </p>
    <p>
        <asp:Button ID="Button2" runat="server" onclick="Button2_Click" 
            Text="View Weekly Tracking Sheet" />
        &nbsp;</p>
<p>
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
</p>
</asp:Content>

