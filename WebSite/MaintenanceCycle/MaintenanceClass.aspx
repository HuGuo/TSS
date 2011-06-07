<%@ Page Language="C#" AutoEventWireup="true" CodeFile="MaintenanceClass.aspx.cs"
    Inherits="MaintenanceCycle_MaintenanceClass" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>设备分类</title>
    <link rel="Stylesheet" type="text/css" href="../scripts/jquery-easyui/thems/default/easyui.css" />
    <link rel="Stylesheet" type="text/css" href="../scripts/jquery-easyui/thems/icon.css" />
    <script src="../scripts/jquery-1.6.1.min.js" type="text/javascript"></script>
    <script src="../scripts/jquery-validation/lib/jquery.js" type="text/javascript"></script>
    <script src="../scripts/jquery-validation/jquery.validate.js" type="text/javascript"></script>
    <script src="../scripts/jquery-validation/messages_cn.js" type="text/javascript"></script>
    <script src="../scripts/jquery-easyui/jquery.easyui.min.js" type="text/javascript"></script>
    <script src="../scripts/My97DatePicker/WdatePicker.js" type="text/javascript"></script>
        <script type="text/javascript">
            function Del(id) {
                $.messager.confirm("删除", "是否删除！", function (result) {
                    if (result) {
                        window.location.href = "MaintenanceClass.aspx?id=" + id;
                    } else {
                        return result;
                    }
                });
            }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <a href="AddMaintenanceClass.aspx">添加</a>
        <a href="MaintenanceCycle.aspx">返回</a>
        <asp:Repeater runat="server" ID="rptClass">
            <HeaderTemplate>
                <table>
                    <tr>
                        <td>
                            类型名称
                        </td>
                        <td>
                            操作
                        </td>
                    </tr>
            </HeaderTemplate>
            <ItemTemplate>
                <tr>
                    <td>
                        <%# ((TSS.Models.MaintenanceClass)Container.DataItem).equipmentClassName %>
                    </td>
                    <td>
                        <a href="EditMaintenanceClass.aspx?id=<%# ((TSS.Models.MaintenanceClass)Container.DataItem).Id %>">编辑</a>
                        <a onclick="Del(<%# ((TSS.Models.MaintenanceClass)Container.DataItem).Id.ToString() %>)" href="#">删除</a>
                    </td>
                </tr>
            </ItemTemplate>
            <FooterTemplate>            
                <tr  style="visibility:<%# rptClass.Items.Count == 0 ? "visible" : "hidden"%>">
                    <td colspan="2" align="center">
                        无数据
                    </td>
                </tr>
                </table>
            </FooterTemplate>
        </asp:Repeater>
    </div>
    </form>
</body>
</html>
