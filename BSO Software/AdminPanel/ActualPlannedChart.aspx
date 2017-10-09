<%@ Page Title="Actual and Planned Audit Trend" Language="C#" MasterPageFile="~/AdminPanel/Site.master" AutoEventWireup="true" CodeFile="ActualPlannedChart.aspx.cs" Inherits="AdminPanel_ActualPlannedChartt" %><%@ Register assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a" namespace="Microsoft.Reporting.WebForms" tagprefix="rsweb" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
    <p>
        &nbsp;</p>
    <rsweb:ReportViewer ID="FunctionByTrendReport" runat="server" Font-Names="Verdana" 
        Font-Size="8pt" Height="592px" InteractiveDeviceInfos="(Collection)" 
        WaitMessageFont-Names="Verdana" WaitMessageFont-Size="14pt" Width="673px" 
        ZoomMode="PageWidth" DocumentMapCollapsed="True">
        <LocalReport ReportPath="AdminPanel\ActualPlannedChart.rdlc">
            <DataSources>
                <rsweb:ReportDataSource DataSourceId="ActPlan_SqlDataSource" 
                    Name="DataSet1" />
            </DataSources>
        </LocalReport>
    </rsweb:ReportViewer>
    <asp:SqlDataSource ID="ActPlan_SqlDataSource" runat="server" 
        ConnectionString="<%$ ConnectionStrings:ConnectionString %>" 
        SelectCommand="SELECT [Dept], [Planned], [Actual] FROM [Departmental_Table] ORDER BY [Actual] DESC">
    </asp:SqlDataSource>
    <asp:SqlDataSource ID="ActualPlannedChart_SqlDataSource" runat="server" 
        ConnectionString="<%$ ConnectionStrings:ConnectionString %>" 
        SelectCommand="SELECT * FROM [Departmental_Table]"></asp:SqlDataSource>
    <asp:SqlDataSource ID="ActualPlannedSqlDataSource" runat="server" 
        ConnectionString="<%$ ConnectionStrings:ConnectionString %>" 
        SelectCommand="SELECT [Dept], [Planned], [Actual] FROM [Departmental_Table]">
    </asp:SqlDataSource>
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <p>
    </p>
</asp:Content>

