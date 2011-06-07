<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AddMaintenanceClass.aspx.cs"
    Inherits="MaintenanceCycle_AddMaintenanceClass" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>设备类型添加</title>
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
                    ddlSpecialty: "required",
                    tbClassNames: "required"
                }
            });
        });
    </script>
</head>
<body>
    <form id="signupForm" runat="server">
    <div>
        <fieldset>
            <legend>添加设备类型 </legend>
            <p>
                <label>
                    设备类名</label>
                <asp:TextBox runat="server" ID="tbClassNames"></asp:TextBox>
            </p>
            <p>
                <label>
                    专业名称</label>
                <asp:DropDownList runat="server" ID="ddlSpecialty">
                    <asp:ListItem Value="">请选择专业</asp:ListItem>
                </asp:DropDownList>
            </p>
            <p>
                <asp:Button ID="btnAdd" runat="server" Text="添加" OnClick="btnAdd_Click" />
                <input type="button" value="取消" onclick="window.location.href='MaintenanceClass.aspx'" />
            </p>
        </fieldset>
    </div>
    </form>
</body>
</html>
