﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="GroundStationAdd.aspx.cs" Inherits="OperatingManagement.Web.Views.BusinessManage.GroundStationAdd" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="NavigatorContent" runat="server">
<om:PageNavigator ID="navMain" runat="server" CssName="menu-top" SelectedId="resmanage" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MenuContent" runat="server">
<om:PageMenu ID="PageMenu1" runat="Server" XmlFileName="menuRes" />
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="MapPathContent" runat="server">
业务管理 &gt; 新增地面站
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="BodyContent" runat="server">
 <table class="edit" style="width: 800px;">
        <tr>
            <th style="width: 200px;">
                地面站名称(<span class="red">*</span>)
            </th>
            <td>
                <asp:TextBox ID="txtAddrName" runat="server" CssClass="norText" MaxLength="100"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" Display="Dynamic"
                    ForeColor="Red" ControlToValidate="txtAddrName" ErrorMessage="（必填）"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <th>
                地面站编号(<span class="red">*</span>)
            </th>
            <td>
                <asp:TextBox ID="txtAddrMark" runat="server" CssClass="norText" MaxLength="10"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" Display="Dynamic"
                    ForeColor="Red" ControlToValidate="txtAddrMark" ErrorMessage="（必填）"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <th>
                内部编码(<span class="red">*</span>)
            </th>
            <td>
                <asp:TextBox ID="txtInCode" runat="server" CssClass="norText"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" Display="Dynamic"
                    ForeColor="Red" ControlToValidate="txtInCode" ErrorMessage="（必填）"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <th>
                外部编码(<span class="red">*</span>)
            </th>
            <td>
                <asp:TextBox ID="txtExCode" runat="server" CssClass="norText"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" Display="Dynamic"
                    ForeColor="Red" ControlToValidate="txtExCode" ErrorMessage="（必填）"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <th>
                管理单位(<span class="red">*</span>)
            </th>
            <td>
                <asp:DropDownList ID="dplOwn" runat="server" CssClass="norDpl">
                </asp:DropDownList>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" Display="Dynamic"
                    ForeColor="Red" ControlToValidate="dplOwn" ErrorMessage="（必填）"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr id="trCoordinate" runat="server" visible="false">
            <th>
                站址坐标(<span class="red">*</span>)
            </th>
            <td>
                <asp:DropDownList ID="dplCoordinate" runat="server" CssClass="norDpl">
                </asp:DropDownList>
                 <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" Display="Dynamic"
                    ForeColor="Red" ControlToValidate="dplCoordinate" ErrorMessage="（必填）"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <th>
                经度坐标值(<span class="red">*</span>)
            </th>
            <td>
                 <asp:TextBox ID="txtLongitude" runat="server" CssClass="norText"></asp:TextBox>度（地心系BLH，东经正值西经负值）
                 <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" Display="Dynamic"
                    ForeColor="Red" ControlToValidate="txtLongitude" ErrorMessage="（必填）"></asp:RequiredFieldValidator>
                 <asp:RangeValidator ID="RangeValidator3" runat="server" Display="Dynamic" MinimumValue="-180" MaximumValue="180" 
                 ControlToValidate="txtLongitude" Type="Double" ForeColor="Red" ErrorMessage="（-180至180）"></asp:RangeValidator>
                 <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ValidationExpression="^(-?\d+)(\.\d{1,6})?$" Display="Dynamic"
                    ForeColor="Red" ControlToValidate="txtLongitude" ErrorMessage="（最多含有六位小数的数字）"></asp:RegularExpressionValidator>
            </td>
        </tr>
         <tr>
            <th>
                纬度坐标值(<span class="red">*</span>)
            </th>
            <td>
                 <asp:TextBox ID="txtLatitude" runat="server" CssClass="norText"></asp:TextBox>度（地心系BLH，北纬正值南纬负值）
                 <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" Display="Dynamic"
                    ForeColor="Red" ControlToValidate="txtLatitude" ErrorMessage="（必填）"></asp:RequiredFieldValidator>
                 <asp:RangeValidator ID="RangeValidator1" runat="server" Display="Dynamic"  MinimumValue="-90" MaximumValue="90" 
                 ControlToValidate="txtLatitude" Type="Double" ForeColor="Red" ErrorMessage="（-90至90）"></asp:RangeValidator>
                 <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ValidationExpression="^(-?\d+)(\.\d{1,6})?$" Display="Dynamic"
                    ForeColor="Red" ControlToValidate="txtLatitude" ErrorMessage="（最多含有六位小数的数字）"></asp:RegularExpressionValidator>
            </td>
        </tr>
        <tr>
            <th>
                高程坐标值(<span class="red">*</span>)
            </th>
            <td>
                 <asp:TextBox ID="txtGaoCheng" runat="server" CssClass="norText" MaxLength="12"></asp:TextBox>米（地心系BLH）
                 <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" Display="Dynamic"
                    ForeColor="Red" ControlToValidate="txtGaoCheng" ErrorMessage="（必填）"></asp:RequiredFieldValidator>
                 <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" ValidationExpression="^\d+(\.\d{1,1})?$" Display="Dynamic"
                    ForeColor="Red" ControlToValidate="txtGaoCheng" ErrorMessage="（>=0且最多含有一位小数的数字）"></asp:RegularExpressionValidator>
            </td>
        </tr>
        <tr>
            <th>
                主IP地址
            </th>
            <td>
                <asp:TextBox ID="txtMainIP" runat="server" CssClass="norText" MaxLength="15"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <th>
                TCP端口
            </th>
            <td>
                <asp:TextBox ID="txtTCPPort" runat="server" CssClass="norText" MaxLength="8"></asp:TextBox>
                <asp:RegularExpressionValidator ID="RegularExpressionValidator4" runat="server" ValidationExpression="^[1-9][0-9]{0,7}$" Display="Dynamic"
                    ForeColor="Red" ControlToValidate="txtTCPPort" ErrorMessage="（请输入8位内正整数）"></asp:RegularExpressionValidator>
            </td>
        </tr>
        <tr>
            <th>
                备用IP地址
            </th>
            <td>
                <asp:TextBox ID="txtBakIP" runat="server" CssClass="norText" MaxLength="15"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <th>
                UDP端口
            </th>
            <td>
                <asp:TextBox ID="txtUDPPort" runat="server" CssClass="norText" MaxLength="8"></asp:TextBox>
                <asp:RegularExpressionValidator ID="RegularExpressionValidator5" runat="server" ValidationExpression="^[1-9][0-9]{0,7}$" Display="Dynamic"
                    ForeColor="Red" ControlToValidate="txtUDPPort" ErrorMessage="（请输入8位内正整数）"></asp:RegularExpressionValidator>
            </td>
        </tr>
        <tr>
            <th>
                FTP路径
            </th>
            <td>
                <asp:TextBox ID="txtFTPPath" runat="server" CssClass="norText" MaxLength="50"></asp:TextBox>
                <asp:RegularExpressionValidator ID="RegularExpressionValidator6" runat="server" ValidationExpression="(ftp):\/\/[\w\-_]+(\.[\w\-_]+)+([\w\-\.,@?^=%&amp;:/~\+#]*[\w\-\@?^=%&amp;/~\+#])?" Display="Dynamic"
                    ForeColor="Red" ControlToValidate="txtFTPPath" ErrorMessage="（请正确输入FTP路径）"></asp:RegularExpressionValidator>
            </td>
        </tr>
           <tr>
            <th>
                FTP用户名
            </th>
            <td>
                <asp:TextBox ID="txtFTPUser" runat="server" CssClass="norText"></asp:TextBox>
            </td>
        </tr>
           <tr>
            <th>
                FTP用户密码
            </th>
            <td>
                <asp:TextBox ID="txtFTPPwd" runat="server" CssClass="norText"></asp:TextBox>
            </td>
        </tr>
        <tr id="trMessage" runat="server" visible="false">
            <th>
                &nbsp;
            </th>
            <td>
                <asp:Label ID="lblMessage" runat="server" CssClass="error" Text=""></asp:Label>
            </td>
        </tr>
        <tr>
            <th>
                &nbsp;
            </th>
            <td>
                <asp:Button ID="btnSubmit" runat="server" CssClass="button" Text="提 交" OnClick="btnSubmit_Click" />
                <asp:Button ID="btnReset" runat="server" CssClass="button" Text="清 除" OnClick="btnReset_Click"
                    CausesValidation="false" />
                <asp:Button ID="btnReturn" runat="server" CssClass="button" Text="返 回" OnClick="btnReturn_Click"
                    CausesValidation="false" />
            </td>
        </tr>
    </table>
</asp:Content>
