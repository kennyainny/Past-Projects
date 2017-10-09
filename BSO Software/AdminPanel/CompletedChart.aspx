<%@ Page Title="Percentage Completed Chart" Language="C#" MasterPageFile="~/AdminPanel/Site.master" AutoEventWireup="true" CodeFile="CompletedChart.aspx.cs" Inherits="AdminPanel_PlannedActual" %>

<%@ Register assembly="System.Web.DataVisualization, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" namespace="System.Web.UI.DataVisualization.Charting" tagprefix="asp" %>

<%@ Register assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a" namespace="Microsoft.Reporting.WebForms" tagprefix="rsweb" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
    <p>
        &nbsp;</p>
    <rsweb:ReportViewer ID="ReportViewer1" runat="server" Font-Names="Verdana" 
        Font-Size="8pt" Height="502px" InteractiveDeviceInfos="(Collection)" 
        WaitMessageFont-Names="Verdana" WaitMessageFont-Size="14pt" Width="679px" 
        ZoomMode="PageWidth" DocumentMapCollapsed="True">
        <LocalReport ReportPath="AdminPanel\CompletedChart.rdlc">
            <DataSources>
                <rsweb:ReportDataSource DataSourceId="CompletedChartSqlDataSource1" 
                    Name="CompletedChart" />
            </DataSources>
        </LocalReport>
    </rsweb:ReportViewer>
    <asp:SqlDataSource ID="CompletedChartSqlDataSource1" runat="server" 
    ConnectionString="<%$ ConnectionStrings:ConnectionString %>" 
    SelectCommand="SELECT * FROM [Departmental_Table] ORDER BY [Complete] DESC">
</asp:SqlDataSource>
    <asp:SqlDataSource ID="CompletedSqlDataSource" runat="server" 
        ConnectionString="<%$ ConnectionStrings:ConnectionString %>" 
        SelectCommand="SELECT [Dept], [Complete] FROM [Departmental_Table]">
    </asp:SqlDataSource>
    <p>
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
    </p>
    <p>
    </p>
</asp:Content>

