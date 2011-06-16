<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="SystemManagement_Experiment_Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>实验报告模板</title>
    <link href="../../scripts/base.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server" style="padding-top: 32px;">
    <div id="toolbar" class="fixed">
        <a href="setTemplate.aspx" target="_blank">添加模板</a></div>
    <table style="width:90%;">
        <tr>
            <th style="width:50px;">
                序号</tdth>
                <th>
                    模板名称
                </th>
                <th style="width:120px;">
                    操作
                </th>
        </tr>
        <asp:Repeater ID="rptList" runat="server">
            <ItemTemplate>
                <tr id="tr_<%#Eval("id") %>">
                    <td>
                        <%# Container.ItemIndex+1 %>
                    </td>
                    <td>
                        <a href="PreView.aspx?id=<%#Eval("Id") %>" target="_blank">
                            <%#Eval("Title") %></a>
                    </td>
                    <td>
                        <a href="setTemplate.aspx?tid=<%#Eval("id") %>" target="_blank">编辑</a> <a href="javascript:void(0);"
                            class="delete" key="<%#Eval("id") %>">删除</a>
                    </td>
                </tr>
            </ItemTemplate>
        </asp:Repeater>
    </table>
    </form>
</body>
</html>
<script src="../../scripts/jquery-1.6.1.min.js" type="text/javascript"></script>
<script src="../../scripts/jquery.delete.js" type="text/javascript"></script>
<script type="text/javascript">
    $(function () {
        $("a.delete").bindDelete({
            handler: "../../exp.ashx",
            op: "del-t",
            onSuccess: function (k) {
                $("#tr_" + k).remove();
            }
        });
    });
</script>
