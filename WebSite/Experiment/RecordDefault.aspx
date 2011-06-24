<%@ Page Language="C#" AutoEventWireup="true" CodeFile="RecordDefault.aspx.cs" Inherits="Experiment_RecordDefault" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>试验台帐</title>
    <link href="../Styles/_base.css" rel="stylesheet" type="text/css" />
    <script src="../scripts/jquery-1.6.1.min.js" type="text/javascript"></script>
    <style type="text/css">
    li{ margin:3px 0px;  border:1px solid #e2e2e2; padding:5px 10px;}
    li a{ display:block;} 
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    </div>
    <table>
        <thead>
            <tr>
                <th>
                </th>
                <th>
                    名称
                </th>
            </tr>
        </thead>
        <asp:Repeater ID="rptlist" runat="server">
            <ItemTemplate>
                <tr>
                    <td>
                        <%#Container.ItemIndex+1 %>
                    </td>
                    <td>
                        <a href="javascript:;" id="<%#Eval("Id") %>" class="c" tid="<%#Eval("ExpTemplateId") %>">
                            <%#Eval("Name") %></a>
                    </td>
                </tr>
            </ItemTemplate>
        </asp:Repeater>
    </table>
    <div id="dg_win" title="选择设备" closable="true" style="width: 300px; height: 200px;padding: 0;">
        <ul id="equipmentList">
        </ul>
    </div>
    </form>
</body>
</html>
<script src="../scripts/jquery-easyui/easyloader.js" type="text/javascript"></script>
<script type="text/javascript">
    $(function () {
        $(document).ajaxError(function (event, request, settings) {
            alert("请求地址发生错误：" + settings.url);
        });
        var handlerUrl = "../exp.ashx";
        easyloader.load("panel", function () {
            var p = $("#dg_win");
            p.panel({ top: 100, left: 200, style: { position: "fixed"} }).panel("close");
            $("a.c").click(function (e) {
                p.panel("setTitle", this.innerText).panel("open");
                var query = { op: "recordequipments", id: "", tid: "" };
                query.tid = this.getAttribute("tid");
                query.id = this.id;
                $.getJSON(handlerUrl, query, function (res) {
                    var lis = '';
                    for (var i = 0; i < res.length; i++) {
                        lis += '<li><a target="_parent" href="Record.aspx?id=' + query.id + '&tid=' + query.tid + '&equipment=' + res[i].id + '">' + res[i].name + '</a></li>';
                    }
                    $("#equipmentList").empty().append(lis);
                });
            });
        });
    });
</script>
