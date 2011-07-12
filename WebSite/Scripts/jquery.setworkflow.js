$.extend({ IsNumeric: function (input) {
    return (input - 0) == input && ($.trim(input).length > 0);
}
});
var queryId = '';
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
        var liStr = '<div hours="' + $("#time").val() + '"><em>Step ' + steps + ': [' + $("#time").val() + '小时]</em>',
            len = $opts.size();
        $opts.each(function () {
            var $$ = $(this);
            if (this.selected || len == 1) {
                liStr += '<a class="primary negative button" sid="' + $$.val() + '"><span class="user icon"></span>' + $$.text() + '</a> ';
            } else {
                liStr += '<a class="button" sid="' + $$.val() + '"><span class="user icon"></span>' + $$.text() + '</a> ';
            }
        });
        //$ul.find("li:last").removeClass("current").addClass("done");
        $ul.append(liStr + '</div>');
        if ($opts.size() == 1) {
            $ul.find("span:last").addClass("weight");
        }
        $opts.remove();
        $("#dg_win").dialog("close");
    });
    //
    $("#btnDel").click(function () {
        if (steps < 1) {
            return false;
        }
        $ul.find("div:last").remove();
        steps--;
    });
    //
    $("#btnSave").click(function () {
        var query = { op: "create", id: "", name: $("#txtName").val(), wf: "" };
        query.id = queryId;
        if (query.name == "") {
            alert("流程名称未设置");
            return false;
        }
        var xmlstr = "<w>";
        if (steps == 0) {
            alert("流程不能为空");
            return false;
        }
        $ul.find(">div").each(function () {
            var $$ = $(this);
            xmlstr += '<a hours="' + $$.attr("hours") + '">';
            $$.find("a").each(function () {
                xmlstr += '<s sid="' + this.getAttribute("sid") + '" isweight="' + $(this).hasClass("primary") + '">' + $(this).text() + '</s>';
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