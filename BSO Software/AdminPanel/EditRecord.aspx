<%@ Page Title="Modify Observer Record" Language="C#" MasterPageFile="~/AdminPanel/Site.master" AutoEventWireup="true" CodeFile="EditRecord.aspx.cs" Inherits="AdminPanel_EditRecord" %>

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
        <asp:Label ID="Label4" runat="server" Font-Bold="True" Font-Size="Large" 
            ForeColor="#000099" style="font-size: large" Text="Edit database records:"></asp:Label>
    </p>
    <p>
        <asp:Label ID="Label7" runat="server" ForeColor="#990000" 
            style="font-size: medium"></asp:Label>
    </p>
    <asp:ValidationSummary ID="ValidationSummary1" runat="server" ForeColor="Red" 
        HeaderText="Please fill in the following field(s) properly:" />
    <p>
        <table __designer:mapid="2b" cellpadding="5" cellspacing="5" class="style10" 
            style="font-family: Calibri; color: #000080; font-size: large;">
            <tr __designer:mapid="2c">
                <td __designer:mapid="2d" class="style12">
                    <asp:Label ID="Label1" runat="server" Text="Department: "></asp:Label>
                </td>
                <td __designer:mapid="2f" class="style13">
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
            <tr __designer:mapid="32">
                <td __designer:mapid="33" class="style12">
                    <asp:Label ID="Label2" runat="server" Text="Sub-Department:"></asp:Label>
                </td>
                <td __designer:mapid="35" class="style13">
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
            <tr __designer:mapid="3a">
                <td __designer:mapid="3b" class="style12">
                    <asp:Label ID="Label3" runat="server" Text="Name of Observer:"></asp:Label>
                </td>
                <td __designer:mapid="3d" class="style13">
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
            <tr __designer:mapid="43">
                <td __designer:mapid="44" class="style12">
                    <asp:Label ID="Label5" runat="server" Text="New Actual value:"></asp:Label>
                </td>
                <td __designer:mapid="46" class="style13">
                    <asp:TextBox ID="TextBox1" runat="server" MaxLength="3"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                        ControlToValidate="TextBox1" ErrorMessage="Type in Actual value" 
                        ForeColor="Red">*</asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr __designer:mapid="49">
                <td __designer:mapid="4a" class="style12">
                    <asp:Label ID="Label6" runat="server" Text="New Planned value:"></asp:Label>
                </td>
                <td __designer:mapid="4c" class="style13">
                    <asp:TextBox ID="TextBox2" runat="server"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" 
                        ControlToValidate="TextBox2" ErrorMessage="Type in Planned value" 
                        ForeColor="Red">*</asp:RequiredFieldValidator>
                </td>
            </tr>
        </table>
    </p>
    <p>
        <asp:Button ID="Button2" runat="server" onclick="Button2_Click" 
            Text="Update Record" />
    </p>
    <p>
        &nbsp;</p>
</asp:Content>

