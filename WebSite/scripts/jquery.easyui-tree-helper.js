/**
* 肖宏飞 
* 2011-6-11
*/
(function ($) {
    var $$ = null;
    var opt = {
        enableContextMenu: true,
        remoteData: true,
        handlerUrl: '',
        opStr: { "data": null, "add": "add", "del": "delete", "edit": "edit" },
        onclick: null,
        onDblClick: null,
        onAfterRemove: null,
        onAfterEdit: null,
        appendItem: null,
        onAfterAppend: null,
        onBeforeEdit: null,
        onCancelEdit: null
    };
    var d = $("<p></p>");
    $.fn.treeHelper = function (o) {
        $.extend(opt, o);
        if (opt.remoteData && !opt.opStr.data) {
            $.messager.alert('错误提示', 'easyui-tree 加载数据出错,' + opt.opStr.data, 'error'); return false;
        }
        $$ = this;
        //bind contextmenu
        if (opt.enableContextMenu) {
            var ctmenu = $('<div id="tree_contextmenu" style="width:120px;"><div id="addChild" onclick="javascript:$.fn.treeHelper.addChildItem();">添加子节点</div><div id="delNode" onclick="javascript:$.fn.treeHelper.removeItem();">删除节点</div><div class="menu-sep"></div><div id="editNode" onclick="javascript:$.fn.treeHelper.editItem();">修改</div></div>');
            //var ctmenu = $("#tree_contextmenu");
            //$("body").after(ctmenu);
            ctmenu.menu();
        }
        //bidn tree
        var currentText = '';
        $$.tree({
            url: opt.remoteData ? opt.handlerUrl + "?op=" + opt.opStr.data : null,
            onContextMenu: function (e, node) {
                if (opt.enableContextMenu) {
                    e.preventDefault();
                    $$.tree('select', node.target);
                    ctmenu.menu("show", {
                        left: e.pageX,
                        top: e.pageY
                    });
                }
            },
            onClick: function (node) {
                if (node.text != currentText) {
                    $$.tree("select", node.target);
                    return false;
                }
            },
            onSelect: function (node) {
                currentText = node.text;
                if (opt.onclick) {
                    opt.onclick(node);
                }
            },
            onDblClick: function (node) {
                if (node.id == "") {
                    return false;
                }
                currentText = node.text;
                if (opt.onDblClick) {
                    opt.onDblClick(node);
                }
                $$.tree("beginEdit", node.target);
            },
            onBeforeEdit: function (node) {
                if (opt.onBeforeEdit) {
                    opt.onBeforeEdit(node);
                    return false;
                }
                var aTag = $(node.target).find("a");
                if (aTag.size() > 0) {
                    d.data("t", aTag).data("nd", node);
                    node.text = aTag.text();
                    $$.tree("update", node);
                }
            },
            onCancelEdit: function (node) {
                if (opt.onCancelEdit) {
                    opt.onCancelEdit(node);
                    return false;
                }
                if (d.data("t") != null) {
                    node.text = d.data("t");
                    $$.tree("update", node);
                    d.removeData("t").removeData("nd");
                }
            },
            onAfterEdit: function (node) {
                if (node.text != currentText) {
                    var query = { op: opt.opStr.edit, id: node.id, text: node.text };
                    $.get(opt.handlerUrl, query, function (data) {
                        if (data == "") {
                            if (d.data("t") != null) {
                                node.text = d.data("t").text(node.text);
                                $$.tree("update", node);
                            }
                            if (opt.onAfterEdit) {
                                opt.onAfterEdit(node);
                            }
                        } else {
                            node.text = currentText;
                            $$.tree("update", node);
                            alert(data);
                        }
                    });
                } else {
                    if (d.data("t") != null) {
                        node.text = d.data("t");
                        $$.tree("update", node);
                    }
                }
                d.removeData("t");
            },
            onLoadError: function () {
                alert("数据源错误,未能提供正确的数据");
            }
        });

        return $$;
    };
    $.fn.treeHelper.addChildItem = function (p) {
        if (opt.appendItem) {
            opt.appendItem();
            return false;
        }

        var node = p ? p : $$.tree("getSelected");
        if (!node) {
            node = $$.tree("getRoot");
        }
        $.messager.prompt("添加子节点", "节点名称", function (r) {
            if (r) {
                var query = { op: opt.opStr.add, text: r, rand: Math.random() };
                $.getJSON(opt.handlerUrl, query, function (data) {
                    if (data.msg) {
                        alert(data.msg);
                    } else {
                        var newNode = { id: data.id, text: r };
                        $$.tree("append", {
                            parent: node.target,
                            data: [newNode]
                        });
                        if (opt.onAfterAppend) {
                            opt.onAfterAppend(newNode);
                        }
                    }
                });
            }
        });
    };
    $.fn.treeHelper.removeItem = function (o) {
        var node = o ? o : $$.tree("getSelected");
        if (node) {
            $.messager.confirm("系统提示", "确定删除", function (r) {
                if (r) {
                    var query = { op: opt.opStr.del, id: node.id };
                    $.get(opt.handlerUrl, query, function (data) {
                        if (data == "") {
                            $$.tree("remove", node.target);
                            if (opt.onAfterRemove) {
                                opt.onAfterRemove(node);
                            }
                        } else {
                            alert(data);
                        }
                    });
                }
            });
        }
    };
    $.fn.treeHelper.editItem = function (o) {
        var node = o ? o : $$.tree("getSelected");
        $$.tree("beginEdit", node.target);
    };
})(jQuery);