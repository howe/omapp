﻿<%@ Page MaintainScrollPositionOnPostback="true" MasterPageFile="~/Site.Master" Language="C#"  AutoEventWireup="true" CodeBehind="DMJHEdit.aspx.cs" Inherits="OperatingManagement.Web.Views.PlanManage.DMJHEdit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
<style type="text/css">
        .style1
        {
            width: 157px;
        }
        .style2
        {
            width: 236px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="NavigatorContent" runat="server">
    <om:PageNavigator ID="navMain" runat="server" CssName="menu-top" SelectedId="index" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MenuContent" runat="server">
    <om:PageMenu ID="PageMenu1" runat="Server" XmlFileName="menuPlan" />
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="MapPathContent" runat="server">
    地面站工作计划
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="BodyContent" runat="server">
    <div>
        <table cellpadding="0" class="edit1" style="width: 750px;">
            <tr>
                <th class="style1">
                    计划开始时间</th>
                <td class="style2">
                    <asp:TextBox ID="txtPlanStartTime" runat="server" CssClass="text" 
                            MaxLength="10"  ClientIDMode="Static"  ></asp:TextBox>
                </td>
                <th>
                    计划结束时间</th>
                <td>
                    <asp:TextBox ID="txtPlanEndTime" runat="server" CssClass="text" 
                            MaxLength="10"    ClientIDMode="Static"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <th class="style1">
                    编号
                </th>
                <td class="style2">
                    <asp:TextBox ID="txtSequence" CssClass="text"  runat="server"></asp:TextBox>
                </td>
                <th>
                    时间
                </th>
                <td>
                    <asp:TextBox ID="txtDatetime" CssClass="text"  runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <th class="style1">
                    工作单位
                </th>
                <td class="style2">
                    <asp:TextBox ID="txtStationName" CssClass="text"  runat="server" style="margin-left: 0px"></asp:TextBox>
                </td>
                <th>
                    设备代号
                </th>
                <td>
                    <asp:TextBox ID="txtEquipmentID" CssClass="text"  runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <th class="style1">
                    任务个数
                </th>
                <td class="style2">
                    <asp:TextBox ID="txtTaskCount" CssClass="text"  runat="server" ReadOnly="True"></asp:TextBox>
                </td>
                <th>
                     <asp:HiddenField ID="HfID" runat="server" />
                     <asp:HiddenField ID="HfFileIndex" runat="server" />
                </th>
                <td>
                    <asp:HiddenField ID="hfTaskID" runat="server" />
                <asp:HiddenField ID="hfSatID" runat="server" />
                <asp:HiddenField ID="hfOverDate" runat="server" />
                </td>
            </tr>
        </table>
        <table class="edit" style="width: 750px;">
            <tr>
                <td>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Repeater ID="Repeater1" runat="server" OnItemDataBound="Repeater1_ItemDataBound"
                        OnItemCommand="Repeater1_ItemCommand">
                        <ItemTemplate>
                            <table class="edit" style="width: 800px;">
                                <tr>
                                    <th>
                                        任务标志
                                    </th>
                                    <td>
                                        <asp:TextBox ID="txtTaskFlag" CssClass="text" runat="server" Text='<%# Eval("TaskFlag")%>'></asp:TextBox>
                                    </td>
                                    <th>
                                        工作方式
                                    </th>
                                    <td>
                                    <asp:DropDownList ID="ddlFS" runat="server" DataTextField="Text" 
                                        DataValueField="Value"  Width="150px">
                                    </asp:DropDownList>
                                        <%--<asp:TextBox ID="txtWorkWay" CssClass="text" runat="server" Text='<%# Eval("WorkWay")%>'></asp:TextBox>--%>
                                    </td>
                                </tr>
                                <tr>
                                    <th>
                                        计划性质
                                    </th>
                                    <td>
                                    <asp:DropDownList ID="ddlJXZ" runat="server" DataTextField="Text" 
                                        DataValueField="Value"  Width="150px">
                                    </asp:DropDownList>
                                       <%-- <asp:TextBox ID="txtPlanPropertiy" CssClass="text" runat="server" Text='<%# Eval("PlanPropertiy")%>'></asp:TextBox>--%>
                                    </td>
                                    <th>
                                        工作模式
                                    </th>
                                    <td>
                                    <asp:DropDownList ID="ddlMS" runat="server" DataTextField="Text" 
                                        DataValueField="Value"  Width="150px">
                                    </asp:DropDownList>
                                        <%--<asp:TextBox ID="txtWorkMode" CssClass="text" runat="server" Text='<%# Eval("WorkMode")%>'></asp:TextBox>--%>
                                    </td>
                                </tr>
                                <tr>
                                    <th>
                                        任务准备开始时间
                                    </th>
                                    <td>
                                        <asp:TextBox ID="txtPreStartTime" CssClass="text" runat="server" Text='<%# Eval("PreStartTime")%>'></asp:TextBox>
                                    </td>
                                    <th>
                                        任务开始时间
                                    </th>
                                    <td>
                                        <asp:TextBox ID="txtStartTime" CssClass="text" runat="server" Text='<%# Eval("StartTime")%>'></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <th>
                                        跟踪开始时间
                                    </th>
                                    <td>
                                        <asp:TextBox ID="txtTrackStartTime" CssClass="text" runat="server" Text='<%# Eval("TrackStartTime")%>'></asp:TextBox>
                                    </td>
                                    <th>
                                        开上行载波时间
                                    </th>
                                    <td>
                                        <asp:TextBox ID="txtWaveOnStartTime" CssClass="text" runat="server" Text='<%# Eval("WaveOnStartTime")%>'></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <th>
                                        关上行载波时间
                                    </th>
                                    <td>
                                        <asp:TextBox ID="txtWaveOffStartTime" CssClass="text" runat="server" Text='<%# Eval("WaveOffStartTime")%>'></asp:TextBox>
                                    </td>
                                    <th>
                                        跟踪结束时间
                                    </th>
                                    <td>
                                        <asp:TextBox ID="txtTrackEndTime" CssClass="text" runat="server" Text='<%# Eval("TrackEndTime")%>'></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <th>
                                        任务结束时间
                                    </th>
                                    <td>
                                        <asp:TextBox ID="txtEndTime" CssClass="text" runat="server" Text='<%# Eval("EndTime")%>'></asp:TextBox>
                                    </td>
                                    <th>
                                        
                                    </th>
                                    <td>
                                        
                                    </td>
                                </tr>
                                <tr>
                                    <th colspan="4" style="text-align:center">
                                        实时传输
                                    </th>
                                </tr>
                                <tr>
                                    <td colspan="4">
                                        <asp:Repeater ID="rpReakTimeTransfor" runat="server" OnItemCommand="Repeater2_ItemCommand">
                                            <ItemTemplate>
                                                <table>
                                                    <tr>
                                                        <th>
                                                            格式标志
                                                        </th>
                                                        <td>
                                                            <asp:TextBox ID="txtFormatFlag" CssClass="text" runat="server" Text='<%# Eval("FormatFlag")%>'></asp:TextBox>
                                                        </td>
                                                        <th>
                                                            信息流标志
                                                        </th>
                                                        <td>
                                                            <asp:TextBox ID="txtInfoFlowFlag" CssClass="text" runat="server" Text='<%# Eval("InfoFlowFlag")%>'></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <th>
                                                            数据传输开始时间
                                                        </th>
                                                        <td>
                                                            <asp:TextBox ID="txtTransStartTime" CssClass="text" runat="server" Text='<%# Eval("TransStartTime")%>'></asp:TextBox>
                                                        </td>
                                                        <th>
                                                            数据传输结束时间
                                                        </th>
                                                        <td>
                                                            <asp:TextBox ID="txtTransEndTime" CssClass="text" runat="server" Text='<%# Eval("TransEndTime")%>'></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <th>
                                                            数据传输速率(BPS)
                                                        </th>
                                                        <td>
                                                            <asp:TextBox ID="txtTransSpeedRate" CssClass="text" runat="server" Text='<%# Eval("TransSpeedRate")%>'></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:Button ID="Button2" CssClass="button" CommandName="Add" runat="server" Text="添加" />
                                                        </td>
                                                        <td>
                                                            <asp:Button ID="Button3" CssClass="button" CommandName="Del" runat="server" Text="删除" />
                                                        </td>
                                                    </tr>
                                                </table>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                    </td>
                                </tr>
                                <tr>
                                    <th colspan="4" style="text-align:center">
                                        事后回放
                                    </th>
                                </tr>
                                <tr>
                                    <td colspan="4">
                                        <asp:Repeater ID="rpAfterFeedBack" runat="server" OnItemCommand="Repeater3_ItemCommand">
                                            <ItemTemplate>
                                                <table>
                                                    <tr>
                                                        <th>
                                                            格式标志
                                                        </th>
                                                        <td>
                                                            <asp:TextBox ID="FormatFlag" CssClass="text" runat="server" Text='<%# Eval("FormatFlag")%>'></asp:TextBox>
                                                        </td>
                                                        <th>
                                                            信息流标志
                                                        </th>
                                                        <td>
                                                            <asp:TextBox ID="InfoFlowFlag" CssClass="text" runat="server" Text='<%# Eval("InfoFlowFlag")%>'></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <th>
                                                            数据起始时间
                                                        </th>
                                                        <td>
                                                            <asp:TextBox ID="DataStartTime" CssClass="text" runat="server" Text='<%# Eval("DataStartTime")%>'></asp:TextBox>
                                                        </td>
                                                        <th>
                                                            数据结束时间
                                                        </th>
                                                        <td>
                                                            <asp:TextBox ID="DataEndTime" CssClass="text" runat="server" Text='<%# Eval("DataEndTime")%>'></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <th>
                                                            数据传输开始时间
                                                        </th>
                                                        <td>
                                                            <asp:TextBox ID="TransStartTime" CssClass="text" runat="server" Text='<%# Eval("TransStartTime")%>'></asp:TextBox>
                                                        </td>
                                                        <th>
                                                            数据传输速率
                                                        </th>
                                                        <td>
                                                            <asp:TextBox ID="TransSpeedRate" CssClass="text" runat="server" Text='<%# Eval("TransSpeedRate")%>'></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <th>
                                                            
                                                        </th>
                                                        <td>
                                                            
                                                        </td>
                                                        <td>
                                                           <asp:Button ID="Button5" CssClass="button" CommandName="Add" runat="server" Text="添加" />
                                                        </td>
                                                        <td>
                                                            <asp:Button ID="Button6" CssClass="button" CommandName="Del" runat="server" Text="删除" />
                                                        </td>
                                                    </tr>
                                                </table>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Button ID="Button1" CssClass="button" CommandName="Add" runat="server" Text="添加任务" />
                                    </td>
                                    <td>
                                    <asp:Button ID="Button4" CssClass="button" CommandName="Del" runat="server" Text="删除任务" />
                                    </td>
                                    <td>
                                    </td>
                                    <td>
                                    </td>
                                </tr>
                            </table>
                        </ItemTemplate>
                    </asp:Repeater>
                </td>
            </tr>
        </table>
    </div>
    <br />
    <div style="width:750px; text-align:center;">
        <asp:Button ID="btnSubmit" runat="server" CssClass="button" Text="保存计划" onclick="btnSubmit_Click" />
        &nbsp;&nbsp;&nbsp;
                    <asp:Button ID="btnSaveTo" runat="server" CssClass="button" Text="另存计划" 
                    onclick="btnSubmit_Click" />
    </div>
</asp:Content>
