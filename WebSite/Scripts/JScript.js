$(function () {
    var $$$ = {
        url: "../../role.ashx",
        $treeRole: $("#roleTree"),
        addRole: function () {
            $.messager.prompt("新角色", "角色名称", function (r) {
                if (r) {
                    var query = { op: "add", text: r };
                    $.getJSON($$$.url, query, function (data) {
                        if (data.msg) {
                            alert(data.msg);
                        } else {
                            var root = $$$.$treeRole.tree("getRoot");
                            $$$.$treeRole.tree("append", {
                                parent: root ? root.target : null,
                                data: [{ id: data.id, text: r}]
                            })
                        }
                    });
                }
            });
        },
        removeRole: function () {
            var o = $$$.$treeRole.tree("getSelected");
            if (!o) {
                return false;
            }
            $.messager.confirm("系统提示", "确定删除", function (r) {
                if (r) {
                    var query = { op: "delete", id: o.id };
                    $.getJSON($$$.url, query, function (data) {
                        if (!data.msg) {
                            $$$.$treeRole.tree("remove", o.target);
                        } else {
                            alert(data.msg);
                        }
                    });
                }
            });
        },
        initRight: function (o) {
            this.clearRight();
            var query = { op: "right", id: o };
            $.getJSON(this.url, query, function (data) {
                if (!data.msg) {
                    $("#roleName").text(data.name);
                    var items = [];
                    for (var i = 0; i < data.employees.length; i++) {
                        items.push('<li><input type="checkbox" value="' + data.employees[i].id + '"/>' + data.employees[i].name + '</li>');
                    }
                    $("#employees").append(items.join(""));
                    for (var j = 0; j < data.mvs.length; j++) {
                        var $tr = $("#" + data.mvs[j].id);
                        if (data.mvs[j].name[0] == "1") { $tr.find("td:eq(2)").addClass("checked"); }
                        if (data.mvs[j].name[1] == "1") { $tr.find("td:eq(1)").addClass("checked"); }
                        if (data.mvs[j].name[2] == "1") { $tr.find("td:eq(0)").addClass("checked"); }
                    }
                } else {
                    alert(data.msg);
                }
            });
        },
        clearRight: function () {
            $("#employees,#roleName").empty();
            $("#tbrights td.checked").removeClass("checked");
        }
    };

    $("body").layout("panel", "west").panel({
        tools: [{
            iconCls: 'icon-add',
            handler: $$$.addRole
        },
        {
            iconCls: 'icon-remove',
            handler: $$$.removeRole
        }]
    });

    $("#ct").layout("panel", "east").panel({ tools: [
    {
        iconCls: 'icon-remove',
        handler: function () { alert("remove"); }
    }]
    });
    $("#tbrights td").click(function () {
        $(this).toggleClass("checked");
    });

    var currentText = "";
    $$$.$treeRole.tree({
        onClick: function (node) {
            $$$.initRight(node.id);
        },
        onDblClick: function (node) {
            $$$.$treeRole.tree("beginEdit", node.target);
            currentText = node.text;
        },
        onAfterEdit: function (node) {
            if (node.text == currentText) {
                return false;
            }
            var query = { op: "update-n", id: node.id, text: node.text };
            $.get($$$.url, query, function (data) {
                if (data != "") {
                    alert(data);
                } else {
                    $("#roleName").text(query.text);
                }
            });
        }
    });
});