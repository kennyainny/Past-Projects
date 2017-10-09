<%@ Page Title="Delete an Observer" Language="C#" MasterPageFile="~/AdminPanel/Site.master" AutoEventWireup="true" CodeFile="DeleteObserver.aspx.cs" Inherits="AdminPanel_DeleteObserver" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
    <style type="text/css">

        .style10
    {
        width: 88%;
            margin-left: 95px;
        }
    
    .style12
    {
            width: 219px;
            text-align: right;
        }
    .style13
    {
            width: 436px;
            text-align: left;
        }
        </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
    <p>
        <asp:Label ID="Label1" runat="server" Font-Bold="True" Font-Size="Large" 
            ForeColor="#000066" style="font-size: large" Text="Delete an Observer:"></asp:Label>
    </p>
    <p>
        <asp:Label ID="Label3" runat="server" Font-Bold="True" Font-Size="X-Large" 
            ForeColor="Red" style="font-size: medium" Text="WARNING!!!"></asp:Label>
    </p>
    <p __designer:mapid="116" 
        style="font-family: Calibri; font-size: medium; color: #FF0000">
        ANY ACTION PERFORMED HERE HAS AN IRREVERSIBLE CONSEQUENCE ON THE DATABASE e.g 
        PERMANENT LOSS OF RECORDS. YOU WILL BE HELD RESPONSIBLE FOR ANY LOSS IN DATA 
        INTEGRITY!!!</p>
    <p>
        <asp:Label ID="Label2" runat="server" ForeColor="#CC3300" 
            style="font-size: medium"></asp:Label>
    </p>
    <p>
        <table __designer:mapid="11a" cellpadding="5" cellspacing="5" class="style10" 
            style="font-family: Calibri; font-size: large; color: #000080;" 
            align="center">
            <tr __designer:mapid="11b">
                <td __designer:mapid="11c" class="style12">
                    <asp:Label ID="Label4" runat="server" Text="Department: "></asp:Label>
                </td>
                <td __designer:mapid="11e" class="style13">
                    <asp:DropDownList ID="DropDownList1" runat="server" AutoPostBack="True" 
                        DataSourceID="SqlDataSource1" DataTextField="Department" 
                        DataValueField="DeptID">
                    </asp:DropDownList>
                    <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
                        ConnectionString="<%$ ConnectionStrings:ConnectionString %>" 
                        SelectCommand="SELECT [DeptID], [Department] FROM [Departmental_Table] ORDER BY [Department]">
                    </asp:SqlDataSource>
                </td>
            </tr>
            <tr __designer:mapid="121">
                <td __designer:mapid="122" class="style12">
                    <asp:Label ID="Label5" runat="server" Text="Sub-Department:"></asp:Label>
                </td>
                <td __designer:mapid="124" class="style13">
                    <asp:DropDownList ID="DropDownList2" runat="server" AutoPostBack="True" 
                        DataSourceID="SqlDataSource2" DataTextField="SubDept" 
                        DataValueField="SubDeptID">
                    </asp:DropDownList>
                    <asp:SqlDataSource ID="SqlDataSource2" runat="server" 
                        ConnectionString="<%$ ConnectionStrings:ConnectionString %>" 
                        SelectCommand="SELECT [SubDeptID], [DeptID], [SubDept] FROM [SubDepartment_Tables] WHERE ([DeptID] = @DeptID) ORDER BY [SubDept]">
                        <SelectParameters>
                            <asp:ControlParameter ControlID="DropDownList1" Name="DeptID" 
                                PropertyName="SelectedValue" Type="Int32" />
                        </SelectParameters>
                    </asp:SqlDataSource>
                </td>
            </tr>
            <tr __designer:mapid="129">
                <td __designer:mapid="12a" class="style12">
                    <asp:Label ID="Label6" runat="server" Text="Name of Observer:"></asp:Label>
                </td>
                <td __designer:mapid="12c" class="style13">
                    <asp:DropDownList ID="DropDownList3" runat="server" AutoPostBack="True" 
                        DataSourceID="SqlDataSource3" DataTextField="Observer" DataValueField="NameID">
                    </asp:DropDownList>
                    <asp:SqlDataSource ID="SqlDataSource3" runat="server" 
                        ConnectionString="<%$ ConnectionStrings:ConnectionString %>" 
                        SelectCommand="SELECT [NameID], [DeptID], [SubDeptID], [Observer] FROM [Full_Department_Table] WHERE (([DeptID] = @DeptID) AND ([SubDeptID] = @SubDeptID)) ORDER BY [Observer]">
                        <SelectParameters>
                            <asp:ControlParameter ControlID="DropDownList1" Name="DeptID" 
                                PropertyName="SelectedValue" Type="Int32" />
                            <asp:ControlParameter ControlID="DropDownList2" Name="SubDeptID" 
                                PropertyName="SelectedValue" Type="Int32" />
                        </SelectParameters>
                    </asp:SqlDataSource>
                </td>
            </tr>
        </table>
    </p>
    <p>
        <asp:Button ID="Button1" runat="server" ForeColor="Red" onclick="Button1_Click" 
            Text="Delete" />
    </p>
    <p>
    </p>
</asp:Content>

