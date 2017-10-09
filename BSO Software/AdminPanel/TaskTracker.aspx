<%@ Page Title="Adminnistators' Event Log" Language="C#" MasterPageFile="~/AdminPanel/Site.master" AutoEventWireup="true" CodeFile="TaskTracker.aspx.cs" Inherits="AdminPanel_TaskTracker" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
        <asp:Label ID="Label4" runat="server" Font-Bold="True" Font-Size="Large" 
            ForeColor="#000099" style="font-size: large; " 
        Text="Administrators' Event Log"></asp:Label>
    <p>
        <asp:GridView ID="GridView1" runat="server" AllowPaging="True" 
            AutoGenerateColumns="False" BackColor="White" BorderColor="#3366CC" 
            BorderStyle="None" BorderWidth="1px" CellPadding="6" CellSpacing="5" 
            DataKeyNames="ser_no" DataSourceID="SqlDataSource1" PageSize="15">
            <Columns>
                <asp:BoundField DataField="ser_no" HeaderText="S/N" InsertVisible="False" 
                    ReadOnly="True" SortExpression="ser_no" />
                <asp:BoundField DataField="Name" HeaderText="Admin's username" 
                    SortExpression="Name" />
                <asp:BoundField DataField="TimeDate" HeaderText="Time Performed" 
                    SortExpression="TimeDate" />
                <asp:BoundField DataField="TaskPerformed" HeaderText="Task Performed" 
                    SortExpression="TaskPerformed" />
                <asp:BoundField DataField="IPAddy" HeaderText="Host Address" 
                    SortExpression="IPAddy" />
            </Columns>
            <FooterStyle BackColor="#99CCCC" ForeColor="#003399" />
            <HeaderStyle BackColor="#003399" Font-Bold="True" ForeColor="#CCCCFF" />
            <PagerStyle BackColor="#99CCCC" ForeColor="#003399" HorizontalAlign="Left" />
            <RowStyle BackColor="White" ForeColor="#003399" />
            <SelectedRowStyle BackColor="#009999" Font-Bold="True" ForeColor="#CCFF99" />
            <SortedAscendingCellStyle BackColor="#EDF6F6" />
            <SortedAscendingHeaderStyle BackColor="#0D4AC4" />
            <SortedDescendingCellStyle BackColor="#D6DFDF" />
            <SortedDescendingHeaderStyle BackColor="#002876" />
        </asp:GridView>
        <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
            ConnectionString="<%$ ConnectionStrings:ConnectionString %>" 
            SelectCommand="SELECT [ser_no], [Name], [TimeDate], [TaskPerformed], [IPAddy] FROM [Admin_Tracker] ORDER BY [ser_no]">
        </asp:SqlDataSource>
    </p>
    <p>
    </p>
</asp:Content>

