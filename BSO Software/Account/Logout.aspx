<%@ Page Title="Logout -Session Terminated" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="Logout.aspx.cs" Inherits="Account_Logout" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
    <p>
    </p>
    <p>
    </p>
    <p>
    </p>
    <p>
    </p>
    <p>
<asp:Label ID="Label1" runat="server" Font-Names="Microsoft New Tai Lue" 
    ForeColor="Red" 
    
    Text="You have logged out or your session has expired, click login to continue......" 
    style="font-size: large; font-weight: 700;"></asp:Label>
<br />
    </p>
    <p>
<br />
<asp:Button ID="Button1" runat="server" onclick="Button1_Click" Text="Login" />
    </p>
                    </asp:Content>

