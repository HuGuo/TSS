﻿<%@ Page Language="C#" AutoEventWireup="true" CodeFile="EditMaintenanceCycle.aspx.cs" Inherits="MaintenanceCycle_EditMaintenanceCycle" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>修改设备周期</title>
    <link rel="Stylesheet" type="text/css" href="../scripts/jquery-easyui/thems/default/easyui.css" />
    <link rel="Stylesheet" type="text/css" href="../scripts/jquery-easyui/thems/icon.css" />
    <script src="../scripts/jquery-1.6.1.min.js" type="text/javascript"></script>
    <script src="../scripts/jquery-validation/lib/jquery.js" type="text/javascript"></script>
    <script src="../scripts/jquery-validation/jquery.validate.js" type="text/javascript"></script>
    <script src="../scripts/jquery-validation/messages_cn.js" type="text/javascript"></script>
    <script src="../scripts/jquery-easyui/jquery.easyui.min.js" type="text/javascript"></script>
    <script src="../scripts/My97DatePicker/WdatePicker.js" type="text/javascript"></script>
    <script language="javascript" type="text/javascript">
        $().ready(function () {
            $("#signupForm").validate({
                rules: {
                    ddlEquipment: "required",
                    ddlClass: "required",
                    tbInstallTime: {
                        date: true
                    },
                    tbCycle: {
                        required: true,
                        number: true,
                        min: 0
                    }
                }
            });
        });
    </script>
</head>
<body>
    <form id="signupForm" runat="server">
    <div>
      <fieldset>
            <legend>设备预示周期 </legend>
            <p>
                <asp:DropDownList runat="server" ID="ddlEquipment">
                    <asp:ListItem Value="">请选择设备</asp:ListItem>
                </asp:DropDownList>
            </p>
            <p>
                <label>
                    设备类型</label>
                <asp:DropDownList runat="server" ID="ddlClass">
                    <asp:ListItem Value="">请选择设备类型</asp:ListItem>
                </asp:DropDownList>
            </p>
            <p>
                <label>
                    设备型号</label><asp:TextBox runat="server" ID="tbModel"></asp:TextBox>
            </p>
            <p>
                <label>
                    检修类别</label><asp:TextBox  runat="server" ID="tbType"></asp:TextBox>
            </p>
            <p>
                <label>
                    安装日期</label><asp:TextBox runat="server" onclick="WdatePicker()" ID="tbInstallTime"></asp:TextBox>
            </p>
            <p>
                <label>
                    周期</label><asp:TextBox runat="server" class="ignore" ID="tbCycle"></asp:TextBox>
            </p>
            <p>
                <asp:Button runat="server" ID="btnEdit" Text="添加" OnClick="btnEdit_Click" />
                <input type="button" value="取消" onclick="window.document.location.href='MaintenanceCycle.aspx'" />
        </fieldset>
    </div>
    </form>
</body>
</html>
