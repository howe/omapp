﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MBXQEdit.aspx.cs" Inherits="OperatingManagement.Web.Views.PlanManage.MBXQEdit" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
            <style type="text/css">
        .style1
        {
            width: 147px;
        }
        .text
        {}
    </style>
    <script src="../../Scripts/calendar.js" type="text/javascript"></script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <table class="edit" style="width:800px;">
        <tr>
            <th class="style1">计划开始时间</th>
            <td>
                    <asp:TextBox ID="txtPlanStartTime" runat="server" CssClass="text" 
                            MaxLength="10"   onclick="setday(this);" Width="300px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <th class="style1">计划结束时间</th>
            <td>
                    <asp:TextBox ID="txtPlanEndTime" runat="server" CssClass="text" 
                            MaxLength="10"   onclick="setday(this);" Width="300px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <th class="style1">用户名称</th>
            <td>
                <asp:TextBox ID="txtMBUser" runat="server" Width="300px" CssClass="text" 
                    MaxLength="20"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <th class="style1">需求制定时间</th>
            <td>
                <asp:TextBox ID="txtMBTime" runat="server" Width="300px" CssClass="text" 
                    MaxLength="20"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <th class="style1">目标信息标志</th>
            <td>
                <asp:TextBox ID="txtMBTargetInfo" runat="server" Width="300px" CssClass="text" 
                    MaxLength="20"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <th class="style1">开始时间</th>
            <td>
                <asp:TextBox ID="txtMBTimeSection1" runat="server" Width="300px" CssClass="text" 
                    MaxLength="20"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <th class="style1">结束时间</th>
            <td>
                <asp:TextBox ID="txtMBTimeSection2" runat="server" Width="300px" CssClass="text" 
                    MaxLength="20"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <th class="style1">信息条数</th>
            <td>
                <asp:TextBox ID="txtMBSum" runat="server" Width="300px"  CssClass="text" 
                    MaxLength="50"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <th class="style1">&nbsp;</th>
            <td>
                    <asp:Repeater ID="rpMB" runat="server" 
                        onitemcommand="rpMB_ItemCommand">
                        <ItemTemplate>
                            <table>
                                <tr>
                                    <td>
                                       卫星名称</td>
                                    <td>
                                        <asp:TextBox ID="txtMBSatName" runat="server" Text='<%# Eval("SatName")%>'></asp:TextBox>
                                    </td>
                                    <td>
                                        产品名称</td>
                                    <td>
                                        <asp:TextBox ID="txtMBInfoName" runat="server" Text='<%# Eval("InfoName")%>'></asp:TextBox>
                                    </td>
                                </tr>
                                 <tr>
                                    <td>
                                      提供时间</td>
                                    <td>
                                        <asp:TextBox ID="txtMBInfoTime" runat="server" Text='<%# Eval("InfoTime")%>'></asp:TextBox>
                                    </td>
                                    <td>
                                         <asp:Button ID="btn9" CssClass="button"  runat="server" CommandName="Add" Text="添加" />
                                    </td>
                                    <td>
                                        <asp:Button ID="btn10" CssClass="button"  runat="server" CommandName="Del" Text="删除" />
                                    </td>
                                </tr>
                            </table>
                        </ItemTemplate>
                    </asp:Repeater>
                </td>
        </tr>
        <tr>
            <th class="style1">&nbsp;</th>
            <td>
                <asp:Button ID="btnSubmit" runat="server" CssClass="button" Text="提交" 
                    onclick="btnSubmit_Click" />
                    &nbsp;&nbsp;&nbsp;
                    <asp:Button ID="btnSaveTo" runat="server" CssClass="button" Text="另存计划" 
                    onclick="btnSubmit_Click" />
                     <asp:HiddenField ID="HfID" runat="server" />
                    <asp:HiddenField ID="HfFileIndex" runat="server" />
                    <asp:HiddenField ID="hfTaskID" runat="server" />
                <asp:HiddenField ID="hfSatID" runat="server" />
                <asp:HiddenField ID="hfOverDate" runat="server" />
                </td>
        </tr>
    </table>

    </div>
    </form>
</body>
</html>
