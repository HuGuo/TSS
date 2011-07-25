<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Experiment_Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>试验报告</title>
    <link href="../Styles/_base.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/jquery-1.6.1.min.js" type="text/javascript"></script>
    <!--easyui-->
    <link href="../Scripts/jquery-easyui/themes/gray/easyui.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/jquery-easyui/jquery.easyui.min.js" type="text/javascript"></script>
</head>
<body class="easyui-layout">
    <div region="west" split="true" title="实验报告分类" style="width: 220px; max-width: 300px;
        padding: 0px;">
        <div style="padding: 3px; border-bottom:1px solid #f7f7f7; background-color:#f7f7f7;">
            <a id="linkAdd" class="button" href="javascript:addItem();">添加分类</a> <a id="linkRename"
                class="button" href="javascript:rename()">重命名</a> <a id="linkDel" class="button negative"
                    href="javascript:delItem()">删除</a>
        </div>
        <ul id="expTree">
        </ul>
    </div>
    <div region="center" style="padding: 0">
        <iframe name="frm" id="frm" scrolling="auto" frameborder="0" src="" style="width: 100%;
            height: 100%;"></iframe>
    </div>
</body>
</html>
<script type="text/javascript">
    var s = '<%=Request.QueryString[Helper.queryParam_specialty] %>';
    var $tree = $("#expTree"),
          frm_src = "categorylist.aspx?",
          handlerUrl="expcategory.ashx";
    $(function () {
        //load tree
        var query = { op: "xml", s: s, dp: 0 };
        var tree_dataurl = "expcategory.ashx";
        $tree.load(tree_dataurl, query, function () {
            $(this).tree({
                onClick: function (node) {
                    $("#linkRename").data("t", node.text);
                    document.getElementById("frm").src = frm_src + $.param({ s: s, category: node.id });
                },
                onAfterEdit: function (node) {
                    //rename
                    var t1 = $("#linkRename").data("t");
                    var t2 = $.trim(node.text);
                    if (t2 == "" || t2 == t1) {
                        node.text = t1;
                        $tree.tree("update", node);
                        return false;
                    }
                    var q = { op: "rename", id: node.id, name: node.text };
                    $.get(handlerUrl, q, function (res) {
                        if (res == "") {
                            //rename success
                        } else {
                            $.messager.alert("系统提示", res);
                        }
                    });
                }
            });
        });
    });

    function addItem() {
    var node=getSelectedNode();
    $.messager.prompt("添加分类", "分类名称", function (r) {
        if (r) {
            var q = { op: "add", pid: node ? node.id : "", name: r };
            $.getJSON(handlerUrl, q, function (res) {
                if (res.msg) {
                    $.messager.alert(res.msg);
                } else if (res.id) {
                    $tree.tree("append", { parent: (node ? node.target : null), data: [{ id: res.id, text: q.name}] });
                }
            });
        }
    });
    }

    function rename() {
        var node = getSelectedNode();
        if (node != null) {
            $tree.tree("beginEdit", node.target);
        }
    }

    function delItem() {
        var node = getSelectedNode();
        if (node!=null) {
            $.messager.confirm("删除分类", "确定删除分类：" + node.text, function (r) {
                if (r) {
                    var q = { op: "delete", id: node.id };
                    $.get(handlerUrl, q, function (res) {
                        if (res == "") {
                            $tree.tree("remove",node.target);
                        } else {
                            $.messager.alert("系统提示", res);
                        }
                    });
                }
            });
        }
    }

    function getSelectedNode() {
        return $tree.tree("getSelected");
    }
</script>
