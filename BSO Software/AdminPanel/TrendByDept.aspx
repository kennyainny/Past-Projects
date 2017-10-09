<%@ Page Title="Actual, Planned and Completed Values" Language="C#" MasterPageFile="~/AdminPanel/Site.master" AutoEventWireup="true" CodeFile="TrendByDept.aspx.cs" Inherits="AdminPanel_TrendByDept" %>

<%@ Register assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a" namespace="Microsoft.Reporting.WebForms" tagprefix="rsweb" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
    <p>
        <asp:Label ID="Label1" runat="server" Font-Bold="True" Font-Size="Large" 
            ForeColor="#000099" style="font-size: large; text-align: left;" 
            Text="Actual, Planned and Completed by Department"></asp:Label>
    </p>
    <p>
    </p>
    <rsweb:ReportViewer ID="ReportViewer1" runat="server" Font-Names="Verdana" 
        Font-Size="8pt" Height="465px" InteractiveDeviceInfos="(Collection)" 
        WaitMessageFont-Names="Verdana" WaitMessageFont-Size="14pt" Width="550px" 
        DocumentMapCollapsed="True" PageCountMode="Actual" 
    PromptAreaCollapsed="True">
        <LocalReport ReportPath="AdminPanel\TrendByDept.rdlc">
            <DataSources>
                <rsweb:ReportDataSource DataSourceId="SqlDataSource1" 
                    Name="PACDataSet" />
            </DataSources>
        </LocalReport>
    </rsweb:ReportViewer>
    <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
        ConnectionString="<%$ ConnectionStrings:ConnectionString %>" 
        SelectCommand="SELECT * FROM [Departmental_Table] ORDER BY [Planned] DESC, [Complete] DESC, [Department] DESC">
    </asp:SqlDataSource>
    <p>
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
    </p>
    <p>
    </p>
</asp:Content>

