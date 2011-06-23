<%@ Page Language="C#" AutoEventWireup="true" ValidateRequest="false" EnableViewState="false" CodeFile="SetWorkflow.aspx.cs" Inherits="SystemManagement_Workflow_SetWorkflow" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>设置流程</title>
    <link href="../../scripts/base.css" rel="stylesheet" type="text/css" />
    <link href="../../scripts/cssstep.css" rel="stylesheet" type="text/css" />
    <link href="../../scripts/jquery-easyui/themes/gray/easyui.css" rel="stylesheet" type="text/css" />
    <script src="../../scripts/jquery-1.6.1.min.js" type="text/javascript"></script>
    <script src="../../scripts/jquery-easyui/jquery.easyui.min.js" type="text/javascript"></script>
    <style type="text/css">
        p
        {
            margin-top: 5px;
        }
        .weight{ font-weight:bold; color:#FFFF99;}
    </style>
</head>
<body>
    <form id="form1" runat="server" style="padding-top:32px;">
    <div id="toolbar" class="fixed" style="padding:3px 5px;">
    <a href="Default.aspx">返回列表</a>
        流程名称：        
        <asp:TextBox runat="server" ID="txtName" Style="width: 300px;" />
        <input type="button" id="btnNext" value="添加流程节点" class="btnlong" />
        <input type="button" id="btnDel" value="删除流程节点" class="btnlong" />
        <input type="button" id="btnSave" value="保存流程" class="btnlong" />
    </div>
    <ul id="mainNav" class="fourStep">
        <asp:Repeater runat="server" ID="rptlist">
            <ItemTemplate>
            <li class="done" hours="<%#Eval("hours") %>"><em>Step <%#Container.ItemIndex+1 %>: </em>
            <%#Eval("t") %>
            </li>
            </ItemTemplate>
        </asp:Repeater>
    </ul>
    <div class="clearfloat">
        &nbsp;</div>
    <div id="dg_win" class="easyui-dialog" title="创建流程节点" style="width: 400px; height: 400px;
        top: 80px; margin-left: auto; margin-right: auto; padding: 0;" buttons="#dlg_buttons">
        <div class="easyui-layout" style="width: 100%; height: 100%;">
            <div region="west" border="true" split="false" style="width: 200px; overflow: auto;">
                <ul id="tree_users" class="easyui-tree">
                    <asp:Literal ID="ltLI" runat="server"></asp:Literal>
                </ul>
            </div>
            <div region="center" border="false" style="overflow: auto;">
                <select id="selUsers" size="10" style="width: 150px; height: 180px;">
                </select>
                <p>
                    设置完成时限：<input type="text" id="time" value="0" style="width: 50px;" />小时</p>
                <p style="color: Red; font-size: 9pt;">
                    1 双击左边用户名即可选择,双击已选用户取消选择</p>
                <p style="color: Red; font-size: 9pt;">
                    2一个节点中多个用户参与时，需要选中权重大者</p>
            </div>
        </div>
    </div>
    <div id="dlg_buttons">
        <a href="#" id="createNode" class="easyui-linkbutton">创 建</a> <a href="#" class="easyui-linkbutton"
            onclick="javascript:$('#dg_win').dialog('close')">关闭</a>
    </div>
    <asp:HiddenField runat="server" ID="stepsCount" Value="0" />
    </form>
</body>
</html>
<script type="text/javascript">
    $.extend({ IsNumeric: function (input) {
        return (input - 0) == input && ($.trim(input).length > 0);
    }
});

$(function () {
    $("#dg_win").dialog("close");
    var $sel = $("#selUsers");
    var $ul = $("#mainNav");
    var handlerUrl = "workflow.ashx";
    var steps = parseInt($("#stepsCount").val());
    //
    $("#time").blur(function () {
        var t = this.value;
        if (!$.IsNumeric(t)) {
            this.value = "0";
        }
    });
    //
    $("#tree_users").tree({ "onDblClick": function (n) {
        if (!$("#tree_users").tree("isLeaf", n.target)) {
            $("#tree_users").tree("toggle", n.target);
            return false;
        }
        var opt = $sel.find("option[value='" + n.id + "']");
        if (opt.size() == 0) {
            $sel.append("<option value='" + n.id + "'>" + n.text + "</option>");
        }
    }
    });
    //
    $("#btnNext").click(function () {
        $("#dg_win").dialog("open");
    });
    //
    $sel.dblclick(function () {
        $sel.find("option:selected").remove();
    });
    //
    $("#createNode").click(function () {
        var $opts = $sel.find("option");
        if ($opts.size() < 1) {
            alert("未选择活动参与人员"); return false;
        } else if ($opts.size() > 1) {
            if ($sel.find("option:selected").size() == 0) {
                alert("未选中权重者"); return false;
            }
        }
        steps++;
        var liStr = '<li class="done" hours="' + $("#time").val() + '"><em>Step ' + steps + ': </em>';
        $opts.each(function () {
            var $$ = $(this);
            if (this.selected) {
                liStr += '<span class="weight" sid="' + $$.val() + '">' + $$.text() + '</span>';
            } else {
                liStr += '<span sid="' + $$.val() + '">' + $$.text() + '</span>';
            }
        });
        //$ul.find("li:last").removeClass("current").addClass("done");
        $ul.append(liStr + '</li>');
        if ($opts.size() == 1) {
            $ul.find("span:last").addClass("weight");
        }
        $opts.remove();
    });
    //
    $("#btnDel").click(function () {
        if (steps < 1) {
            return false;
        }
        $ul.find("li:last").remove();
        steps--;
    });
    //
    $("#btnSave").click(function () {
        var query = { op: "create", id: "", name: $("#txtName").val(), wf: "" };
        query.id = '<%=Request.QueryString["id"] %>';
        if (query.name == "") {
            alert("流程名称未设置");
            return false;
        }
        var xmlstr = "<w>";
        $ul.find("li").each(function () {
            var $$ = $(this);
            xmlstr += '<a hours="' + $$.attr("hours") + '">';
            $$.find("span").each(function () {
                xmlstr += '<s sid="' + this.getAttribute("sid") + '" isweight="' + $(this).hasClass("weight") + '">' + $(this).text() + '</s>';
            });
            xmlstr += "</a>"
        });
        xmlstr += "</w>"
        query.wf = xmlstr;
        $.post(handlerUrl, query, function (res) {
            if (res != "") {
                alert(res);
            } else {
                alert("保存成功");
                document.location.href = "default.aspx";
            }
        });
    });

});
</script>
