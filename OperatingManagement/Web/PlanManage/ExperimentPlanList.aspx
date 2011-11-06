﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="ExperimentPlanList.aspx.cs" Inherits="OperatingManagement.Web.PlanManage.ExperimentPlanList" %>

<asp:Content ID="Content3" ContentPlaceHolderID="HeadContent" runat="server">
    <style type="text/css">
        .style1
        {
            width: 100%;
            border-collapse: collapse;
            border-style: solid;
            border-width: 1px;
        }
        .style2
        {
        }
        .style3
        {
            width: 179px;
        }
        .style4
        {
            width: 125px;
        }
        .style5
        {
            width: 131px;
            height: 18px;
        }
        .style6
        {
            height: 18px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="NavigatorContent" runat="server">
    <om:PageNavigator ID="navMain" runat="server" CssName="menu-top" SelectedId="index" />
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="MenuContent" runat="server">
    <om:PageMenu ID="PageMenu1" runat="Server" XmlFileName="menuIndex" />
</asp:Content>
<asp:Content ID="Content6" ContentPlaceHolderID="MapPathContent" runat="server">
    首页
</asp:Content>
<asp:Content ID="Content7" ContentPlaceHolderID="BodyContent" runat="server">
    <table cellpadding="0" class="style1">
        <tr>
            <td align="right" class="style2">
                开始日期：
            </td>
            <td class="style3">
                <asp:TextBox ID="txtStartDate" runat="server" onclick="new WdatePicker(this);"></asp:TextBox>
            </td>
            <td align="right" class="style4">
                结束日期：
            </td>
            <td>
                <asp:TextBox ID="txtEndDate" runat="server" onclick="new WdatePicker(this);"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="style5">
            </td>
            <td class="style6" colspan="3">
                <asp:Button class="button" ID="btnSearch" runat="server" OnClick="btnSearch_Click"
                    Text="查询" Width="65px" />
                &nbsp;&nbsp;
                <%--<asp:Button ID="btnReset" runat="server" Text="重置" Width="65px" />--%>
                <button class="button" onclick="return reset();" style="width: 65px;">
                    重置</button>
            </td>
        </tr>
        <tr>
            <td class="style2">
                &nbsp;
            </td>
            <td class="style3">
                &nbsp;
            </td>
            <td class="style4">
                &nbsp;
            </td>
            <td>
                &nbsp;
            </td>
        </tr>
        <tr>
            <td class="style2" colspan="4">
                <asp:Repeater ID="rpUsers" runat="server">
                    <HeaderTemplate>
                        <table class="list">
                            <tr>
                                <%--<th style="width:20px;"><input type="checkbox" onclick="checkAll(this)" /></th>--%>
                                <th style="width: 150px;">
                                    信源
                                </th>
                                <th style="width: 150px;">
                                    信宿
                                </th>
                                <th style="width: 150px;">
                                    任务代码
                                </th>
                                <th style="width: 150px;">
                                    生成时间
                                </th>
                                <th style="width: 70px;">
                                    明细
                                </th>
                            </tr>
                            <tbody id="tbUsers">
                    </HeaderTemplate>
                    <ItemTemplate>
                        <tr>
                            <%--<td><input type="checkbox" <%# Eval("LoginName").ToString().Equals(this.Profile.UserName,StringComparison.InvariantCultureIgnoreCase)?"disabled=\"true\"":"" %> name="chkDelete" value="<%# Eval("Id") %>" /></td>--%>
                            <td>
                                <%# Eval("source")%>
                            </td>
                            <td>
                                <%# Eval("destination")%>
                            </td>
                            <td>
                                <%# Eval("taskid")%>
                            </td>
                            <td>
                                <%# Eval("ctime", "{0:" + this.SiteSetting.DateTimeFormat + "}")%>
                            </td>
                            <td>
                                <button class="button" onclick="return showDetail('<%# Eval("JHID") %>')">
                                    明细</button>
                            </td>
                        </tr>
                    </ItemTemplate>
                    <FooterTemplate>
                        </tbody> </table>
                    </FooterTemplate>
                </asp:Repeater>
                <table class="listTitle">
                    <tr>
                        <td class="listTitle-c1">
                        </td>
                        <td class="listTitle-c2">
                            <om:CollectionPager ID="cpPager" runat="server">
                            </om:CollectionPager>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</asp:Content>
