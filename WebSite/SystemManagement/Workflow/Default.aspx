<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="SystemManagement_Workflow_Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>系统流程</title>
    <link href="../../Styles/_base.css" rel="stylesheet" type="text/css" />
    <link href="../../Styles/datasheet.css" rel="stylesheet" type="text/css" />
    <script src="../../scripts/jquery-1.6.1.min.js" type="text/javascript"></script>
</head>
<body>
    <form id="form1" runat="server" style="padding-top: 32px;">
    <div id="toolbar" class="fixed">
        <a href="SetWorkflow.aspx">新建流程</a>
    </div>
    <div class="wrap">
        <div class="wrap_inner">
            <table class="datasheet">
                <thead>
                    <tr>
                        <th style="width: 20px;">
                        </th>
                        <th>
                            流程名称
                        </th>
                        <th style="width: 80px; text-align: center">
                            操作
                        </th>
                    </tr>
                </thead>
                <tbody>
                    <asp:Repeater ID="rptlist" runat="server">
                        <ItemTemplate>
                            <tr id="tr_<%#Eval("Id") %>">
                                <th>
                                    <%#Container.ItemIndex+1 %>
                                </th>
                                <td>
                                    <%#Eval("name") %>
                                </td>
                                <td class="center_cell">
                                    <a href="SetWorkflow.aspx?id=<%#Eval("Id") %>">编辑</a> <a href="#" class="delete"
                                        key="<%#Eval("Id") %>">删除</a>
                                </td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                </tbody>
            </table>
        </div>
    </div>
    </form>
</body>
</html>
<script src="../../Scripts/jquery.tableStyle.js" type="text/javascript"></script>
<script type="text/javascript">
    $("a.delete").bindDelete({
        op: "del-workflow"
    });
</script>
