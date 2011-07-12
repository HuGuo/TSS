<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="SystemManagement_Report_Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>专业报表</title>
    <link href="../../Styles/_base.css" rel="stylesheet" type="text/css" />
    <link href="../../scripts/jquery-easyui/themes/gray/easyui.css" rel="stylesheet"
        type="text/css" />
    <script src="../../scripts/jquery-1.6.1.min.js" type="text/javascript"></script>
    <script src="../../scripts/jquery-easyui/jquery.easyui.min.js" type="text/javascript"></script>
</head>
<body>
    <form id="form1" runat="server">
    <table border="0" cellpadding="0" cellspacing="0">
        <thead>
            <tr>
                <td>
                    序号
                </td>
                <td>
                    报表名称
                </td>
                <td>
                    引用流程
                </td>
                <td>
                </td>
            </tr>
        </thead>
        <asp:Repeater runat="server" ID="rptlist">
            <ItemTemplate>
                <tr>
                    <td>
                        <%# Container.ItemIndex+1 %>
                    </td>
                    <td>
                        <%#Eval("Name") %>
                    </td>
                    <td id="td_<%#Eval("Id") %>">
                        <%#Eval("WorkflowId ")%>
                    </td>
                    <td>
                        <a href="#wflist" id="<%#Eval("Id") %>" class="setWF button">设置流程</a>
                    </td>
                </tr>
            </ItemTemplate>
        </asp:Repeater>
    </table>
    <div id="dg_win1" class="easyui-dialog" toolbar="#dg_win1_toolbar" modal="true" title="设置流程" style="width: 500px;
        height: 280px;">
        <table id="wflist" border="0" cellpadding="0" cellspacing="0" style="width: 486px;">
            <asp:Repeater runat="server" ID="workflowList">
                <ItemTemplate>
                    <tr>
                        <td align="center" style="width: 30px;">
                            <input type="radio" name="item" class="radioitem" value="<%#Eval("Id") %>" />
                        </td>
                        <td style="padding-left:10px;">
                            <%#Eval("Name") %>
                        </td>
                    </tr>
                </ItemTemplate>
            </asp:Repeater>
        </table>
    </div>
    <div id="dg_win1_toolbar">
    <a class="button" id="btnOk"> 确 定 </a>
    <a class="button" id="btnUnbind" style=" float:right;">取消已设置流程</a>
    </div>
    </form>
</body>
</html>
<script type="text/javascript">
    $(function () {
        var handUrl = "../../workflow.ashx";
        $("#dg_win1").dialog("close");

        $("a.setWF").click(function () {
            $("#btnOk").data("rpt", this.id);
            $("#dg_win1").dialog("open");
        });

        $("#btnOk").click(function () {
            var query = { op: "bind", rpt: $(this).data("rpt"), wf: "" };
            query.wf = $(".radioitem:checked").val();
            setWorkflow(query, query.wf);
        });
        $("#btnUnbind").click(function () {
            setWorkflow({ op: "bind", rpt: $("#btnOk").data("rpt") }, "");
        });

        function setWorkflow(o, txt) {
            $.get(handUrl, o, function (res) {
                if (res != "") {
                    alert(res);
                } else {
                    $("#td_" + o.rpt).text(txt);
                    $("#dg_win1").dialog("close");
                }
            });
        }
    });
</script>
