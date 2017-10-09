<%@ Page Title="Near-Miss Report" Language="C#" MasterPageFile="~/AdminPanel/Site.master" AutoEventWireup="true" CodeFile="NMReportz.aspx.cs" Inherits="AdminPanel_NMReportz" %>

<%@ Register assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a" namespace="Microsoft.Reporting.WebForms" tagprefix="rsweb" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
    <p>
</p>
<p>
            <asp:Label ID="Label39" runat="server" Font-Bold="True" 
                Font-Names="Microsoft Sans Serif" Font-Size="Large" ForeColor="Lime" 
                
                Text="Near-Miss Reports" style="text-align: left; color: #000099;"></asp:Label>
</p>
    <rsweb:ReportViewer ID="ReportViewer1" runat="server" DocumentMapWidth="100%" 
        Font-Names="Verdana" Font-Size="8pt" InteractiveDeviceInfos="(Collection)" 
        PageCountMode="Actual" PromptAreaCollapsed="True" style="text-align: center" 
        WaitMessageFont-Names="Verdana" WaitMessageFont-Size="14pt" Width="724px">
        <LocalReport ReportPath="AdminPanel\NMReport.rdlc">
            <DataSources>
                <rsweb:ReportDataSource DataSourceId="NMSqlDataSource" Name="NMDataSet" />
            </DataSources>
        </LocalReport>
    </rsweb:ReportViewer>
    <asp:SqlDataSource ID="NMSqlDataSource" runat="server" 
        ConnectionString="<%$ ConnectionStrings:ConnectionString %>" 
        SelectCommand="SELECT * FROM [Near_Miss_Table] ORDER BY [ser_no]">
    </asp:SqlDataSource>
<p>
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
</p>
<p>
</p>
<p>
</p>
</asp:Content>

