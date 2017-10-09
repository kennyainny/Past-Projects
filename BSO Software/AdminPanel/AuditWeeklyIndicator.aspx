<%@ Page Title="Weekly Tracking Indicator" Language="C#" AutoEventWireup="true" CodeFile="AuditWeeklyIndicator.aspx.cs" Inherits="AdminPanel_AuditTrendByFunction" %>

<%@ Register assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a" namespace="Microsoft.Reporting.WebForms" tagprefix="rsweb" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">

<link id="Link1" runat="server" rel="shortcut icon" href="~/images/favicon.ico" type="image/x-icon" /> 
    <link id="Link2" runat="server" rel="icon" href="~/images/favicon.ico" type="image/ico" /> 

    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
        <p style="text-align: center">
            <asp:Label ID="Label39" runat="server" Font-Bold="True" 
                Font-Names="Microsoft Sans Serif" Font-Size="Large" ForeColor="Lime" 
                
                Text="Weekly Indicator Tracking Sheet: % Behavioural Audits Completed"></asp:Label>
        </p>
        <p style="text-align: center">
            <asp:Label ID="Label46" runat="server" Font-Bold="True" 
                Font-Names="Microsoft Sans Serif" Font-Size="Large" ForeColor="Lime" 
                
                
                Text="(4 Contacts per week for Supply Chain; 1 Contact per week for others)"></asp:Label>
        </p>
        <p style="text-align: center">
            <asp:Label ID="Label45" runat="server" Font-Bold="True" 
                Font-Names="Microsoft Sans Serif" Font-Size="Large" ForeColor="Lime">Region/Area:  </asp:Label>
            <asp:Label ID="Label44" runat="server" Font-Bold="True" 
                Font-Names="Microsoft Sans Serif" Font-Size="Large" ForeColor="Lime">All Departments</asp:Label>
        </p>
        <p style="text-align: center">
            <asp:Label ID="Label42" runat="server" Font-Bold="True" 
                Font-Names="Microsoft Sans Serif" Font-Size="Large" ForeColor="Lime" 
                Text="Year:   2014"></asp:Label>
        </p>
        <p>
            &nbsp;</p>
        <rsweb:ReportViewer ID="WeeklyReport" runat="server" 
            Font-Names="Verdana" Font-Size="8pt" Height="611px" 
            InteractiveDeviceInfos="(Collection)" PageCountMode="Actual" 
            PromptAreaCollapsed="True" SizeToReportContent="True" 
            style="margin-right: 159px; text-align: center;" WaitMessageFont-Names="Verdana" 
            WaitMessageFont-Size="14pt" Width="538px" ZoomMode="PageWidth" 
            DocumentMapCollapsed="True">
            <LocalReport ReportPath="AdminPanel\AuditWeeklyIndicator.rdlc" 
                DisplayName="Weekly Tracking Indicator Sheet">
                <DataSources>
                    <rsweb:ReportDataSource DataSourceId="AuditSqlDataSource" 
                        Name="WeeklyAudit" />
                </DataSources>
            </LocalReport>
        </rsweb:ReportViewer>
        <asp:SqlDataSource ID="AuditSqlDataSource" runat="server" 
            ConnectionString="<%$ ConnectionStrings:ConnectionString %>" 
            SelectCommand="SELECT * FROM [Full_Department_Table]"></asp:SqlDataSource>
        <p>
            <asp:ScriptManager ID="ScriptManager1" runat="server">
            </asp:ScriptManager>
        </p>
        <p>
        </p>
        <p>
        </p>
    
    </div>
    </form>
</body>
</html>
