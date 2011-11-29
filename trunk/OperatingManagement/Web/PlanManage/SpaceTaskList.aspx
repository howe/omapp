﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="SpaceTaskList.aspx.cs" Inherits="OperatingManagement.Web.PlanManage.SpaceTaskList" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
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
        .style7
        {
            width: 100%;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="NavigatorContent" runat="server">
    <om:PageNavigator ID="navMain" runat="server" CssName="menu-top" SelectedId="usernrole" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MenuContent" runat="server">
    <om:PageMenu ID="PageMenu1" runat="Server" XmlFileName="menuPlan" />
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="MapPathContent" runat="server">
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="BodyContent" runat="server">
    <%--    <asp:Panel ID="pnlData" runat="server">--%>
    <div id="divData">
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
                        Text="查询" Width="69px" />
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
                    <table class="listTitle">
                        <tr>
                            <td class="listTitle-c1">
                                <button class="button" onclick="return selectAll();">
                                    全选</button>&nbsp;&nbsp;
                                <button class="button" onclick="return sendYDSJ1();">
                                    发送引导数据</button>
                            </td>
                            <td class="listTitle-c2">
                                <div class="load" id="submitIndicator" style="display: none">
                                    提交中，请稍候。。。</div>
                            </td>
                        </tr>
                    </table>
                    <asp:Repeater ID="rpDatas" runat="server">
                        <HeaderTemplate>
                            <table class="list">
                                <tr>
                                    <th style="width: 20px;">
                                        <input type="checkbox" onclick="checkAll(this)" />
                                    </th>
                                    <th style="width: 100px;">
                                        信源
                                    </th>
                                    <th style="width: 100px;">
                                        信宿
                                    </th>
                                    <th style="width: 100px;">
                                        任务代码
                                    </th>
                                    <th style="width: 100px;">
                                        卫星编号
                                    </th>
                                    <th>
                                        创建时间
                                    </th>
                                    <th style="width: 70px;">
                                        明细
                                    </th>
                                </tr>
                                <tbody id="tbYDSJs">
                        </HeaderTemplate>
                        <ItemTemplate>
                            <tr>
                                <td>
                                    <input type="checkbox" name="chkDelete" value="<%# Eval("Id") %>" />
                                </td>
                                <td>
                                    <%# Eval("SOURCEADDRESS")%>
                                </td>
                                <td>
                                    <%# Eval("DESTINATIONADDRESS")%>
                                </td>
                                <td>
                                    <%# Eval("MISSIONCODEV")%>
                                </td>
                                <td>
                                    <%# Eval("SATELLITECODE")%>
                                </td>
                                <td>
                                    <%# Eval("CreatedTime","{0:"+this.SiteSetting.DateTimeFormat+"}") %>
                                </td>
                                <td>
                                    <button class="button" onclick="return showDetail('<%# Eval("Id") %>')">
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
                                <button class="button" onclick="return selectAll();">
                                    全选</button>&nbsp;&nbsp;
                                <button class="button" onclick="return sendYDSJ1();">
                                    发送引导数据</button>
                            </td>
                            <td class="listTitle-c2">
                                <om:CollectionPager ID="cpPager" runat="server">
                                </om:CollectionPager>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td colspan="4" align="center">
                    <%-- <asp:Button ID="btnSend" runat="server" Text="发送轨道数据" onclick="btnSend_Click" />--%>
                </td>
            </tr>
        </table>
    </div>
    <%--    </asp:Panel>--%>
    <%--    <asp:Panel ID="pnlDestination" runat="server">--%>
    <div id="tartgetPanel" style="display: none">
        <table class="style7">
            <tr>
                <td align="center">
                    <asp:RadioButtonList ID="rbtDestination" runat="server">
                    </asp:RadioButtonList>
                </td>
            </tr>
            <tr>
                <td style="text-align: center">
                    <asp:Button ID="btnSubmit" runat="server" OnClick="btnSubmit_Click" Text="发送" />
                    &nbsp;&nbsp;
                    <asp:Button ID="btnCancel" runat="server" OnClick="btnCancel_Click" Text="取消" />
                </td>
            </tr>
        </table>
    </div>
    <%--    </asp:Panel>--%>
    
</asp:Content>