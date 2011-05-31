<%@ Page Language="C#" AutoEventWireup="true" CodeFile="MaintenanceClass.aspx.cs"
    Inherits="MaintenanceCycle_MaintenanceClass" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>设备分类</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
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
                        <asp:LinkButton runat="server" CommandArgument="<%# ((TSS.Models.MaintenanceClass)Container.DataItem).Id %>"
                         OnClick="lbtnDel_Click" ID="lbtnDel">删除</asp:LinkButton>
                        <a href="AddMaintenanceClass.aspx">添加</a>
                        <a href="AddMaintenanceClass.aspx">录入</a>
                    </td>
                </tr>
            </ItemTemplate>
            <FooterTemplate>
                </table>
            </FooterTemplate>
        </asp:Repeater>
    </div>
    </form>
</body>
</html>
