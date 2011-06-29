﻿<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AddMaintenanceCycle.aspx.cs"
    MasterPageFile="~/ValidateAndUi.master" Inherits="MaintenanceCycle_AddMaintenanceCycle" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript">
        $().ready(function () {
            $("#<%=Page.Form.UniqueID %>").validate();
        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="Server">

    <div id="toolbar" class="fixed">
        <a href="Default.aspx?s=<%= Request.QueryString["specialtyId"] %>">返回</a>
    </div>
    <table>
        <tr>
            <td>
                设备
            </td>
            <td>
                <asp:DropDownList runat="server" ID="ddlEquipment" OnSelectedIndexChanged="ddlEquipment_SelectedIndexChanged">
                    <asp:ListItem Value="">请选择设备</asp:ListItem>
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td>
                设备类型
            </td>
            <td>
                <asp:DropDownList validate="{required:true}" runat="server" ID="ddlClass">
                    <asp:ListItem Value="">请选择设备类型</asp:ListItem>
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td>
                设备型号
            </td>
            <td>
                <asp:TextBox runat="server" ID="tbModel"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                检修类别
            </td>
            <td>
                <asp:TextBox runat="server" ID="tbType"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                安装日期
            </td>
            <td>
                <asp:TextBox runat="server" onclick="WdatePicker()" validate="{date:true}" ID="tbInstallTime"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                周期
            </td>
            <td>
                <asp:TextBox runat="server" class="ignore" validate="{required:true,number:true,min:0}"
                    ID="tbCycle"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <asp:Button runat="server" ID="btnAdd" Text="添加" CssClass="btn" OnClick="btnAdd_Click" />
            </td>
        </tr>
    </table>
</asp:Content>
