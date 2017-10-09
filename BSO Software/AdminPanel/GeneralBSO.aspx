<%@ Page Title="Daily Tracking Indicator" Language="C#" AutoEventWireup="true" CodeFile="GeneralBSO.aspx.cs" Inherits="AdminPanel_GeneralBSO" %>

<%@ Register assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a" namespace="Microsoft.Reporting.WebForms" tagprefix="rsweb" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">

<link id="Link1" runat="server" rel="shortcut icon" href="~/images/favicon.ico" type="image/x-icon" /> 
    <link id="Link2" runat="server" rel="icon" href="~/images/favicon.ico" type="image/ico" /> 

    <title></title>
</head>
<body style="text-align: center">
    <form id="form1" runat="server">
    <div>
    
        <h2 style="color: #800080; font-family: calibri; font-size: xx-large;">
            Welcome to the Unilever SHEQS&#39; Daily Tracking 
            Indicator</h2>
        <p>
        </p>
        <rsweb:ReportViewer ID="ReportViewer1" runat="server" 
            DocumentMapCollapsed="True" Font-Names="Verdana" Font-Size="8pt" 
            InteractiveDeviceInfos="(Collection)" PageCountMode="Actual" 
            PromptAreaCollapsed="True" SizeToReportContent="True" 
            WaitMessageFont-Names="Verdana" WaitMessageFont-Size="14pt" Width="809px" 
            ZoomMode="PageWidth">
            <LocalReport ReportPath="AdminPanel\GeneralBSO.rdlc">
                <DataSources>
                    <rsweb:ReportDataSource DataSourceId="GeneralSqlDataSource" 
                        Name="DailyTracker" />
                </DataSources>
            </LocalReport>
        </rsweb:ReportViewer>
        <asp:SqlDataSource ID="GeneralSqlDataSource" runat="server" 
            ConnectionString="<%$ ConnectionStrings:ConnectionString %>" 
            SelectCommand="SELECT * FROM [General_Table]"></asp:SqlDataSource>
        <p>
            <asp:ScriptManager ID="ScriptManager1" runat="server">
            </asp:ScriptManager>
        </p>
    
    </div>
    </form>
</body>
</html>
