﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ResourceStatusManage.aspx.cs" Inherits="OperatingManagement.Web.Views.BusinessManage.ResourceStatusManage" %>
<%@ Import Namespace="OperatingManagement.DataAccessLayer.BusinessManage" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
<style type="text/css">
        .norText
        {
            width: 155px;
            margin: 0px;
            padding: 0px;
        }
        .norDpl
        {
            width: 160px;
            margin: 0px;
            padding: 0px;
        }
        .index_content_search
        {
            margin-top: 10px;
        }
        
        .index_content_search table
        {
            border: 1px solid #eeeeee;
            border-collapse: collapse;
            width: 100%;
        }
        
        .index_content_search table td
        {
            border: 1px solid #eeeeee;
            line-height: 26px;
            color: #333333;
            text-align: left;
            height: 26px;
        }
        .index_content_search table th
        {
            border: 1px solid #eeeeee;
            font-weight: bold;
            line-height: 26px;
            color: #333333;
            text-align: right;
            height: 26px;
        }
        .index_content_view
        {
            margin-top: 10px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="NavigatorContent" runat="server">
<om:PageNavigator ID="navMain" runat="server" CssName="menu-top" SelectedId="index" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MenuContent" runat="server">
<om:PageMenu ID="PageMenu1" runat="Server" XmlFileName="menuBusiness" />
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="MapPathContent" runat="server">
业务管理&gt;资源状态管理
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="BodyContent" runat="server">
<script type="text/javascript">
    $(function () {
        $("#txtBeginTime").datepicker();
        $("#txtEndTime").datepicker();
    });
    </script>
 <div class="index_content_search">
        <table cellspacing="0" cellpadding="0" class="searchTable">
            <tr>
                <th width="15%">
                    资源类型：
                </th>
                <td width="20%">
                    <asp:DropDownList ID="dplResourceType" runat="server" CssClass="norDpl">
                    </asp:DropDownList>
                </td>
                <th width="15%">
                    资源编号：
                </th>
                <td width="20%">
                    <asp:TextBox ID="txtResourceCode" runat="server" CssClass="norText"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" Display="Dynamic"
                    ForeColor="Red" ControlToValidate="txtResourceCode" ErrorMessage="（必填）" ValidationGroup="SearchStatus"></asp:RequiredFieldValidator>
                </td>
                <td width="30%">
                </td>
            </tr>
            <tr>
               <th>
                  起始时间：
               </th>
               <td>
                  <asp:TextBox ID="txtBeginTime" runat="server" CssClass="norText" ClientIDMode="Static"></asp:TextBox>
                  <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" Display="Dynamic"
                    ForeColor="Red" ControlToValidate="txtBeginTime" ErrorMessage="（必填）" ValidationGroup="SearchStatus"></asp:RequiredFieldValidator>
               </td>
               <th>
                  结束时间：
               </th>
               <td>
                 <asp:TextBox ID="txtEndTime" runat="server" CssClass="norText" ClientIDMode="Static"></asp:TextBox>
                 <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" Display="Dynamic"
                    ForeColor="Red" ControlToValidate="txtEndTime" ErrorMessage="（必填）" ValidationGroup="SearchStatus"></asp:RequiredFieldValidator>
               </td>
               <td>
                    <asp:Button ID="btnSearch" runat="server" OnClick="btnSearch_Click" CssClass="button" ValidationGroup="SearchStatus" Text="查 询"/>
                    <asp:Button ID="btnViewChart" runat="server" OnClick="btnViewChart_Click" CssClass="button" Text="图 示"/>
                    <asp:Button ID="btnAdd" runat="server" OnClick="btnAdd_Click" CssClass="button" Text="添 加"/>
               </td>
            </tr>
        </table>
    </div>
     <div id="divResourceStatus" class="index_content_view">
        <asp:Repeater ID="rpResourceHealthStatusList" runat="server">
            <HeaderTemplate>
                <table class="list">
                    <tr>
                        <th style="width: 10%;">
                            资源类型
                        </th>
                        <th style="width: 10%;">
                            资源名称
                        </th>
                        <th style="width: 10%;">
                            资源编号
                        </th>
                        <th style="width: 10%;">
                            功能类型
                        </th>
                        <th style="width: 10%;">
                            健康状态
                        </th>
                        <th style="width: 10%;">
                            起始时间
                        </th>
                        <th style="width: 10%;">
                            结束时间
                        </th>
                    </tr>
                    <tbody id="tbResourceHealthStatusList">
            </HeaderTemplate>
            <ItemTemplate>
                <tr>
                    <td>
                        <%# SystemParameters.GetSystemParameterText(SystemParametersType.ResourceType,Eval("ResourceType").ToString())%>
                    </td>
                    <td>
                        <%# Eval("ResourceName")%>
                    </td>
                    <td>
                        <%# Eval("ResourceCode")%>
                    </td>
                    <td>
                        <%# SystemParameters.GetSystemParameterText(SystemParametersType.HealthStatusFunctionType,Eval("FunctionType").ToString())%>
                    </td>
                    <td>
                        <%# SystemParameters.GetSystemParameterText(SystemParametersType.HealthStatus, Eval("Status").ToString())%>
                    </td>
                    <td>
                         <%# Eval("BeginTime", "{0:" + this.SiteSetting.DateFormat + "}")%>
                    </td>
                    <td>
                        <%# Eval("EndTime", "{0:" + this.SiteSetting.DateFormat + "}")%>
                    </td>
                </tr>
            </ItemTemplate>
            <FooterTemplate>
                </tbody></table>
            </FooterTemplate>
        </asp:Repeater>
        <table class="listTitle">
            <tr>
                <td class="listTitle-c1">
                </td>
                <td class="listTitle-c2">
                    <om:CollectionPager ID="cpResourceHealthStatusPager" runat="server">
                    </om:CollectionPager>
                </td>
            </tr>
        </table>
        <br />
         <asp:Repeater ID="rpResourceUseStatusList" runat="server">
            <HeaderTemplate>
                <table class="list">
                    <tr>
                         <th style="width: 10%;">
                            资源类型
                        </th>
                        <th style="width: 10%;">
                            资源名称
                        </th>
                        <th style="width: 10%;">
                            资源编号
                        </th>
                        <th style="width: 10%;">
                            占用类型
                        </th>
                        <th style="width: 10%;">
                            起始时间
                        </th>
                        <th style="width: 10%;">
                            结束时间
                        </th>
                          <th style="width: 10%;">
                            服务对象
                        </th>
                         <th style="width: 10%;">
                            服务种类
                        </th>
                         <th style="width: 10%;">
                            占用原因
                        </th>
                         <th style="width: 10%;">
                            是否可执行任务
                        </th>
                    </tr>
                    <tbody id="tbResourceHealthStatusList">
            </HeaderTemplate>
            <ItemTemplate>
                <tr>
                   <td>
                        <%# SystemParameters.GetSystemParameterText(SystemParametersType.ResourceType,Eval("ResourceType").ToString())%>
                    </td>
                    <td>
                        <%# Eval("ResourceName")%>
                    </td>
                    <td>
                        <%# Eval("ResourceCode")%>
                    </td>
                    <td>
                        <%# SystemParameters.GetSystemParameterText(SystemParametersType.UseStatusUsedType, Eval("UsedType").ToString())%>
                    </td>
                    <td>
                         <%# Eval("BeginTime", "{0:" + this.SiteSetting.DateFormat + "}")%>
                    </td>
                    <td>
                        <%# Eval("EndTime", "{0:" + this.SiteSetting.DateFormat + "}")%>
                    </td>
                     <td>
                        <%# Eval("UsedBy")%>
                    </td>
                      <td>
                        <%# Eval("UsedCategory")%>
                    </td>
                     </td>
                      <td>
                        <%# Eval("UsedFor")%>
                    </td>
                     </td>
                      <td>
                        <%#SystemParameters.GetSystemParameterText(SystemParametersType.UseStatusCanBeUsed, Eval("CanBeUsed").ToString())%>
                    </td>
                </tr>
            </ItemTemplate>
            <FooterTemplate>
                </tbody></table>
            </FooterTemplate>
        </asp:Repeater>
        <table class="listTitle">
            <tr>
                <td class="listTitle-c1">
                </td>
                <td class="listTitle-c2">
                    <om:CollectionPager ID="cpResourceUseStatusPager" runat="server">
                    </om:CollectionPager>
                </td>
            </tr>
        </table>
    </div>
</asp:Content>