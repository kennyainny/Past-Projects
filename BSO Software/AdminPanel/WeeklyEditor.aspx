<%@ Page Title="Weekly Tracking Sheet Editor" Language="C#" MasterPageFile="~/AdminPanel/Site.master" AutoEventWireup="true" CodeFile="WeeklyEditor.aspx.cs" Inherits="AdminPanel_WeeklyEditor" %>

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
            ForeColor="#000099" style="font-size: large" 
            Text="Weekly Database Editor"></asp:Label>
    </p>
    <p>
        <asp:Label ID="Label2" runat="server" ForeColor="#990000" 
            style="font-size: medium"></asp:Label>
    </p>
    <asp:ValidationSummary ID="ValidationSummary1" runat="server" ForeColor="Red" 
        HeaderText="Please fill in the following field(s) properly:" />
    <p>
        <asp:Label ID="Label4" runat="server" ForeColor="#000066" 
            style="font-size: medium">Select an Observer and a week to edit:</asp:Label>
    </p>
    <p>
        <table __designer:mapid="2b" cellpadding="5" cellspacing="5" class="style10" 
            style="font-family: Calibri; color: #000080; font-size: large;">
            <tr __designer:mapid="2c">
                <td __designer:mapid="2d" class="style12">
                    <asp:Label ID="Label7" runat="server" Text="Department: "></asp:Label>
                </td>
                <td __designer:mapid="2f" class="style13">
                    <asp:DropDownList ID="DropDownList6" runat="server" AutoPostBack="True" 
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
                    <asp:Label ID="Label8" runat="server" Text="Sub-Department:"></asp:Label>
                </td>
                <td __designer:mapid="35" class="style13">
                    <asp:DropDownList ID="DropDownList7" runat="server" AutoPostBack="True" 
                        DataSourceID="SqlDataSource2" DataTextField="SubDept" 
                        DataValueField="SubDeptID">
                    </asp:DropDownList>
                    <asp:SqlDataSource ID="SqlDataSource2" runat="server" 
                        ConnectionString="<%$ ConnectionStrings:ConnectionString %>" 
                        SelectCommand="SELECT [SubDeptID], [DeptID], [SubDept] FROM [SubDepartment_Tables] WHERE ([DeptID] = @DeptID) ORDER BY [SubDept]">
                        <SelectParameters>
                            <asp:ControlParameter ControlID="DropDownList6" Name="DeptID" 
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
                    <asp:DropDownList ID="DropDownList8" runat="server" AutoPostBack="True" 
                        DataSourceID="SqlDataSource3" DataTextField="Observer" DataValueField="NameID">
                    </asp:DropDownList>
                    <asp:SqlDataSource ID="SqlDataSource3" runat="server" 
                        ConnectionString="<%$ ConnectionStrings:ConnectionString %>" 
                        SelectCommand="SELECT [NameID], [DeptID], [SubDeptID], [Observer] FROM [Full_Department_Table] WHERE (([DeptID] = @DeptID) AND ([SubDeptID] = @SubDeptID)) ORDER BY [Observer]">
                        <SelectParameters>
                            <asp:ControlParameter ControlID="DropDownList6" Name="DeptID" 
                                PropertyName="SelectedValue" Type="Int32" />
                            <asp:ControlParameter ControlID="DropDownList7" Name="SubDeptID" 
                                PropertyName="SelectedValue" Type="Int32" />
                        </SelectParameters>
                    </asp:SqlDataSource>
                </td>
            </tr>
            <tr __designer:mapid="43">
                <td __designer:mapid="44" class="style12">
                    <asp:Label ID="Label5" runat="server" Text="Select Status:"></asp:Label>
                </td>
                <td __designer:mapid="46" class="style13">
                    <asp:DropDownList ID="DropDownList5" runat="server">
                        <asp:ListItem>Away</asp:ListItem>
                        <asp:ListItem>Not Submitted</asp:ListItem>
                        <asp:ListItem>On Leave</asp:ListItem>
                        <asp:ListItem>Submitted</asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
            <tr __designer:mapid="49">
                <td __designer:mapid="4a" class="style12">
                    <asp:Label ID="Label6" runat="server" Text="Select Week:"></asp:Label>
                </td>
                <td __designer:mapid="4c" class="style13">
                    <asp:DropDownList ID="DropDownList4" runat="server">
                        <asp:ListItem>Select one...</asp:ListItem>
                        <asp:ListItem>1</asp:ListItem>
                        <asp:ListItem>2</asp:ListItem>
                        <asp:ListItem>3</asp:ListItem>
                        <asp:ListItem>4</asp:ListItem>
                        <asp:ListItem>5</asp:ListItem>
                        <asp:ListItem>6</asp:ListItem>
                        <asp:ListItem>7</asp:ListItem>
                        <asp:ListItem>8</asp:ListItem>
                        <asp:ListItem>9</asp:ListItem>
                        <asp:ListItem>10</asp:ListItem>
                        <asp:ListItem>11</asp:ListItem>
                        <asp:ListItem>12</asp:ListItem>
                        <asp:ListItem>13</asp:ListItem>
                        <asp:ListItem>14</asp:ListItem>
                        <asp:ListItem>15</asp:ListItem>
                        <asp:ListItem>16</asp:ListItem>
                        <asp:ListItem>17</asp:ListItem>
                        <asp:ListItem>18</asp:ListItem>
                        <asp:ListItem>19</asp:ListItem>
                        <asp:ListItem>20</asp:ListItem>
                        <asp:ListItem>21</asp:ListItem>
                        <asp:ListItem>22</asp:ListItem>
                        <asp:ListItem>23</asp:ListItem>
                        <asp:ListItem>24</asp:ListItem>
                        <asp:ListItem>25</asp:ListItem>
                        <asp:ListItem>26</asp:ListItem>
                        <asp:ListItem>27</asp:ListItem>
                        <asp:ListItem>28</asp:ListItem>
                        <asp:ListItem>29</asp:ListItem>
                        <asp:ListItem>30</asp:ListItem>
                        <asp:ListItem>31</asp:ListItem>
                        <asp:ListItem>32</asp:ListItem>
                        <asp:ListItem>33</asp:ListItem>
                        <asp:ListItem>34</asp:ListItem>
                        <asp:ListItem>35</asp:ListItem>
                        <asp:ListItem>36</asp:ListItem>
                        <asp:ListItem>37</asp:ListItem>
                        <asp:ListItem>38</asp:ListItem>
                        <asp:ListItem>39</asp:ListItem>
                        <asp:ListItem>40</asp:ListItem>
                        <asp:ListItem>41</asp:ListItem>
                        <asp:ListItem>42</asp:ListItem>
                        <asp:ListItem>43</asp:ListItem>
                        <asp:ListItem>44</asp:ListItem>
                        <asp:ListItem>45</asp:ListItem>
                        <asp:ListItem>46</asp:ListItem>
                        <asp:ListItem>47</asp:ListItem>
                        <asp:ListItem>48</asp:ListItem>
                        <asp:ListItem>49</asp:ListItem>
                        <asp:ListItem>50</asp:ListItem>
                        <asp:ListItem>51</asp:ListItem>
                        <asp:ListItem>52</asp:ListItem>
                    </asp:DropDownList>
                    <asp:CompareValidator ID="CompareValidator1" runat="server" 
                        ControlToValidate="DropDownList4" ErrorMessage="Please select a week" 
                        ForeColor="Red" Operator="NotEqual" ValueToCompare="Select one...">*</asp:CompareValidator>
                </td>
            </tr>
        </table>
    </p>
    <p>
    </p>
    <p>
        <asp:Button ID="Button2" runat="server" onclick="Button2_Click" 
            Text="Change" />
    </p>
    <p>
    </p>
</asp:Content>

