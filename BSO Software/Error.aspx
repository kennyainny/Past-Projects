<%@ Page Title="Error Page" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="Error.aspx.cs" Inherits="Error" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
    <style type="text/css">
    .style2
    {
        text-align: center;
    }
</style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
    <p class="style2">
        &nbsp;</p>
    <p class="style2">
        &nbsp;</p>
    <p class="style2">
        <br />
    </p>
    <center style="font-size: small">
        <p style="font-size: medium; color: #FF0000; margin-left: 0px; font-family: Arial, Helvetica, sans-serif; font-weight: bold; font-style: oblique;" 
            class="style2">
            Sorry you saw this, our server has encountered a problem!</p>
        <p style="font-size: medium; color: #FF0000; margin-left: 0px; font-weight: bold; font-style: oblique; font-family: Arial, Helvetica, sans-serif;" 
            class="style2">
            All changes have been discarded.</p>
        <p style="font-size: medium; color: #FF0000; margin-left: 0px; font-weight: bold; font-style: oblique; font-family: Arial, Helvetica, sans-serif;" 
            class="style2">
            Click &#39;ok&#39; to return Home.</p>
    </center>
    <p>
        <asp:Button ID="Button1" runat="server" onclick="Button1_Click" Text="OK" />
    </p>
</asp:Content>

