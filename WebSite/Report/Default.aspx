<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Report_Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>专业报表</title>
</head>
<body>
    <form id="form1" runat="server">
    <table border="0" cellpadding="0" cellspacing="0">
        <thead>
        <tr>
        <td>序号</td>
        <td>报表名称</td>
        </tr>
        </thead>
        <asp:Repeater runat="server" ID="rptlist">
            <ItemTemplate>
            <tr>
            <td><%# Container.ItemIndex+1 %></td>
            <td><%#Eval("Name") %></td>
            </tr>
            </ItemTemplate>
        </asp:Repeater>
    </table>
    </form>
</body>
</html>
