<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="SystemManagement_Experiment_Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>实验报告模板</title>
</head>
<body>
    <form id="form1" runat="server">
    <a href="setTemplate.aspx" target="_blank">添加模板</a>
    <table>
        <tr>
            <th>
                序号</tdth>
                <th>
                    模板名称
                </th>
                <th>
                    操作
                </th>
        </tr>
        <asp:Repeater ID="rptList" runat="server" DataSourceID="ExpTemplateDataSources">
            <ItemTemplate>
                <tr id="tr_<%#Eval("id") %>">
                    <td>
                        <%# Container.ItemIndex+1 %>
                    </td>
                    <td>
                        <%#Eval("Title") %>
                    </td>
                    <td>
                        <a href="setTemplate.aspx?tid=<%#Eval("id") %>" target="_blank">编辑</a>
                        <a href="javascript:void(0);" class="delete" tid="<%#Eval("id") %>">删除</a>
                    </td>
                </tr>
            </ItemTemplate>
        </asp:Repeater>
    </table>
    <asp:ObjectDataSource ID="ExpTemplateDataSources" runat="server" SelectMethod="GetAll"
        TypeName="TSS.BLL.ExpTemplateRepository" 
        OldValuesParameterFormatString="original_{0}"></asp:ObjectDataSource>
    </form>
</body>
</html>
<script src="../../scripts/jquery-1.6.1.min.js" type="text/javascript"></script>
<script type="text/javascript">
    $(function () {
        $("a.delete").click(function () {
            if (confirm("确定删除")) {
                var query = { op: "del-t", tid: "" };
                query.tid = $(this).attr("tid");
                $.get("../../exp.ashx", query, function (data) {
                    if (data != "") {
                        $("#tr_" + query.tid).remove();
                    } else {
                        alert(data);
                    }
                });
            }
        });
    });
</script>