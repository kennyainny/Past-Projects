<%@ Page Title="Observers' Login Credentials" Language="C#" MasterPageFile="~/AdminPanel/Site.master" AutoEventWireup="true" CodeFile="ObseverCreds.aspx.cs" Inherits="AdminPanel_ObseverCreds" %>

<%@ Register assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a" namespace="Microsoft.Reporting.WebForms" tagprefix="rsweb" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
    <p>
        <asp:Label ID="Label4" runat="server" Font-Bold="True" Font-Size="Large" 
            ForeColor="#000099" style="font-size: large; text-align: left;" 
        Text="Observers' Login Credentials"></asp:Label>
    </p>
    <rsweb:ReportViewer ID="ReportViewer1" runat="server" 
        DocumentMapCollapsed="True" Font-Names="Verdana" Font-Size="8pt" 
        InteractiveDeviceInfos="(Collection)" PageCountMode="Actual" 
        PromptAreaCollapsed="True" SizeToReportContent="True" 
        WaitMessageFont-Names="Verdana" WaitMessageFont-Size="14pt" 
    Width="628px" ZoomMode="PageWidth">
        <LocalReport ReportPath="AdminPanel\ObseversCreds.rdlc">
            <DataSources>
                <rsweb:ReportDataSource DataSourceId="ObserverSqlDataSource" 
                    Name="ObserverCredS" />
            </DataSources>
        </LocalReport>
    </rsweb:ReportViewer>
    <br />
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:SqlDataSource ID="ObserverSqlDataSource" runat="server" 
        ConnectionString="<%$ ConnectionStrings:ConnectionString %>" 
        SelectCommand="SELECT * FROM [Full_Department_Table]"></asp:SqlDataSource>
    <p>
    </p>
</asp:Content>

