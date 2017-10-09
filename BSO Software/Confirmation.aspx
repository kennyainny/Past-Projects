<%@ Page Title="Successful Form Submission" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="Confirmation.aspx.cs" Inherits="Confirmation" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
    <p>
        &nbsp;</p>
    <p>
        &nbsp;</p>
    <p>
        &nbsp;</p>
    <p>
        <br />
    </p>
    <p>
        <asp:Label ID="Label32" runat="server" Font-Bold="True" Font-Italic="True" 
            Font-Names="Bodoni MT" Font-Size="Large" ForeColor="#000066" 
            
            Text="Your form has been successfully submitted, click 'ok' to return home." 
            style="font-family: Calibri; font-size: x-large"></asp:Label>
    </p>
    <p>
        &nbsp;</p>
    <p>
        <asp:Button ID="Button1" runat="server" onclick="Button1_Click" Text="OK" />
        &nbsp;</p>
</asp:Content>

