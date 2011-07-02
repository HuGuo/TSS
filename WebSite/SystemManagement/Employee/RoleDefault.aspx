<%@ Page Language="C#" AutoEventWireup="true" CodeFile="RoleDefault.aspx.cs" Inherits="SystemManagement_Employee_RoleDefault" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>角色</title>
    <link href="~/Styles/_base.css" rel="stylesheet" type="text/css" />
    <link href="~/Scripts/jquery-easyui/themes/icon.css" rel="stylesheet" type="text/css" />
    <link href="~/Scripts/jquery-easyui/themes/gray/easyui.css" rel="stylesheet" type="text/css" />
    <script src="../../scripts/jquery-1.6.1.min.js" type="text/javascript"></script>
    <script src="../../scripts/jquery-easyui/jquery.easyui.min.js" type="text/javascript"></script>
    <style type="text/css">
        td, th
        {
            border: 1px solid #e2e2e2;
            line-height: 22px;
        }
        td{ cursor:pointer;}
        .checked{ background:url(../../images/ok.png) no-repeat center;}
        li{ display:block; margin:3px 5px;}
    </style>
</head>
<body class="easyui-layout">
    <div region="west" split="true" title="角色列表" closable="false" style="width: 220px;
        max-width: 300px; padding: 5px;">
        <ul id="roleTree">
        </ul>
    </div>
    <div region="east" title="角色用户列表" split="true" style="width: 200px">
            <ul id="employees">
            </ul>
            </div>
    <div region="center">
    <div id="toolbar">
    <a href="Default.aspx">用户管理</a>
    <input type="button" id="btnSave" class="btn" value="保存" style=" float:right; margin-top:3px;" />
    </div>
        <table id="tbrights" cellpadding="0" cellspacing="0" style="width: 100%; border-collapse: collapse;">
            <thead>
                <tr>
                    <th rowspan="2" style="width: 120px;">
                        系统模块
                    </th>
                    <th colspan="3">
                        操作权限
                    </th>
                </tr>
                <tr>
                    <th p="1">
                        浏览
                    </th>
                    <th p="2">
                        操作
                    </th>
                    <th p="4">
                        审核
                    </th>
                </tr>
            </thead>
            <form id="form1" runat="server">
            <asp:Repeater ID="rptlist" runat="server">
                <ItemTemplate>
                    <tr id="<%#Eval("id") %>">
                        <th>
                            <%#Eval("Name") %>
                        </th>
                        <td p="1">
                        </td>
                        <td p="2">
                        </td>
                        <td p="4">
                        </td>
                    </tr>
                </ItemTemplate>
            </asp:Repeater>
            </form>
        </table>
    </div>
</body>
</html>
<script src="../../scripts/jquery.easyui-tree-helper.js" type="text/javascript"></script>
<script type="text/javascript">
    $(function () {
        var $$$ = {
            url: "../../role.ashx",
            $treeRole: $("#roleTree"),
            clearRight: function () {
                $("#employees").removeData("rid").empty();
                $("#tbrights td.checked").removeClass("checked");
            }
        };

        $$$.$treeRole.treeHelper({
            handlerUrl: $$$.url,
            opStr: { "data": "list", "add": "add", "del": "delete", "edit": "update-n" },
            enableContextMenu: false,
            onclick: function (o) {
                if (o.id == "" || o.id == $("#employees").data("rid")) {
                    return false;
                }
                $$$.clearRight();
                $("#employees").data("rid", o.id);
                var query = { op: "right", id: o.id, rand: Math.random() };
                $.getJSON(this.handlerUrl, query, function (data) {
                    if (!data.msg) {
                        var items = [];
                        for (var i = 0; i < data.employees.length; i++) {
                            items.push('<li><input type="checkbox" value="' + data.employees[i].id + '"/>' + data.employees[i].name + '</li>');
                        }
                        $("#employees").append(items.join(""));
                        for (var j = 0; j < data.mvs.length; j++) {
                            var $tr = $("#" + data.mvs[j].id);
                            var k = data.mvs[j].name.length;
                            var m = 0;
                            for (var i = (k - 1); i >= 0; i--) {
                                if (data.mvs[j].name[i] == "1") {
                                    $tr.find("td:eq(" + m + ")").addClass("checked");
                                    m++;
                                }
                            }
                        }
                    } else {
                        alert(data.msg);
                    }
                });
            },
            onAfterRemove: function (node) {
                $$$.clearRight();
            }
        });

        $("body").layout("panel", "west").panel({
            tools: [{
                iconCls: 'icon-add',
                handler: function (e) { $$$.$treeRole.treeHelper.addChildItem($$$.$treeRole.tree("getRoot")); }
            },
        {
            iconCls: 'icon-remove',
            handler: function (e) { $$$.$treeRole.treeHelper.removeItem(); }
        }]
        });
        $("body").layout("panel", "east").panel({ tools: [
    {
        iconCls: 'icon-remove',
        handler: function () {
            var query = { op: "del-employee", id: $("#employees").data("rid"), emps: "" };
            var sels = $("#employees :checkbox:checked");
            if (sels.size() < 1) {
                return false;
            }
            query.emps = sels.map(function () {
                return this.value;
            }).get().join(",");
            $.messager.confirm("系统提示", "确定移除用户", function (r) {
                if (r) {
                    $.post($$$.url, query, function (response) {
                        if (response == "") {
                            sels.parent("li").remove();
                        } else {
                            alert(response);
                        }
                    });
                }
            });
        }
    }]
        });

        $("#tbrights td").click(function (e) {
            if ($("#employees").data("rid") == null) {
                return;
            }
            $(this).toggleClass("checked");
        });
        $("#tbrights th[p]").click(function () {
            if ($("#employees").data("rid") == null) {
                return;
            }
            var $this = $(this);
            if ($this.hasClass("aa")) {
                $("#tbrights td[p='" + this.getAttribute("p") + "']").removeClass("checked");
            } else {
                $("#tbrights td[p='" + this.getAttribute("p") + "']").addClass("checked");
            }
            $this.toggleClass("aa");
        });
        $("#btnSave").click(function (e) {
            var query = { op: "update-r", id: "", mvs: "" };
            if ($("#employees").data("rid") == null) {
                return false;
            }
            query.id = $("#employees").data("rid");
            query.mvs = $("#tbrights tr:gt(1)").map(function () {
                var mid = this.id;
                var v = 0;
                $(this).find("td.checked").each(function (i, td) {
                    v = v + parseInt(td.getAttribute("p"));
                });
                return mid + "=" + v;
            }).get().join(",");

            $.post($$$.url, query, function (res) {
                if (res != "") {
                    $.messager.show({
                        title: '系统提示',
                        msg: '操作权限设置成功',
                        timeout: 3000,
                        showType: 'slide'
                    });
                } else {
                    alert(res);
                }
            });
        });
    });
</script>
