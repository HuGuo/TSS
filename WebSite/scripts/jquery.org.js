$(function () {
    $.ajaxSetup({
        gloab: false,
        async: false
    });
    $.extend({
        toHTML: function (o) {
            return ((o.replace(/<(.+?)>/gi, "&lt;$1&gt;")).replace(/ /gi, "&nbsp;")).replace(/\n/gi, "<BR>");
        },
        toText: function (o) {
            return (((o.replace(/&nbsp;/gi, " ")).replace(/<[Bb][Rr] \/>/gi, "\n")).replace(/&lt;/gi, "<")).replace(/&gt;/gi, ">");
        }
    });
    $.getScript("../scripts/jquery-easyui/easyloader.js", function () {
        easyloader.base = '../scripts/jquery-easyui/';
        easyloader.load(['menu', 'dialog'], function () {
            var ctmenu = '<div id="ctm" style="width: 120px;"><div>编辑</div></div>';
            $("body").append(ctmenu);
            var g = { handlerUrl: "../org.ashx", dg: null, t1: null, t2: null, t3: null };

            $("#ctm").menu({ onClick: function (item) {
                var $td = $("#ctm").data("node");
                if (!g.dg) {
                    g.dg = $('<div id="dg_win1" title="编辑" style="width: 400px; height: 300px; top: 150px; margin-left: auto;margin-right: auto; padding: 0;" buttons="#dlg_btn1"><table cellpadding="0" cellspacing="0" style="width:100%; height:229px ;border:0;"><tr><th colspan="2" id="gname"></th></tr><tr><th align="center" style="width:100px;">网长/组长</th><td><input type="text" id="txt_v1" style="width:96%;" /></td></tr><tr><th align="center">副网长/组员</th><td><input type="text" id="txt_v2" style="width: 96%;" /></td></tr><tr><th colspan="2" align="left" style="padding-left:20px;">职责</th></tr><tr><td colspan="2"><textarea id="txt_v3" style="width:96%; height:100px;"></textarea></td></table></div>');
                    $("body").append(g.dg);
                    g.t1 = $("#txt_v1");
                    g.t2 = $("#txt_v2");
                    g.t3 = $("#txt_v3");
                    g.dg.dialog({ buttons: [
                        {
                            text: "确定",
                            handler: function () {
                                var _d = $("#ctm").data("node");
                                var query = { id: _d.attr("id"), b1: g.t1.val(), b2: g.t2.val(), c: g.t3.val() };

                                $.post(g.handlerUrl, query, function (res) {
                                    if (res == "") {
                                        _d.find("span:eq(0)").text(query.b1);
                                        _d.find("span:eq(1)").text(query.b2);
                                        _d.attr("org_title", query.c);
                                        _d.tipTip();
                                        g.dg.dialog("close");
                                    } else {
                                        alert(res);
                                    }
                                });
                            }
                        }, {
                            text: "关闭",
                            handler: function () {
                                g.dg.dialog("close");
                            }
                        }
                        ]
                    });
                }

                g.dg.find("#gname").text($td.find("b").text());
                g.t1.val($td.find("span:eq(0)").text());
                g.t2.val($td.find("span:eq(1)").text());
                g.t3.val($.toText($td.attr("org_title")));
                g.dg.dialog("open");
            }
            });

            $("td.OrgNode").bind("contextmenu", function (e) {
                e.preventDefault();
                $("#ctm").data("node", $(this)).menu("show", {
                    left: e.pageX,
                    top: e.pageY
                });
            });
        });
    });
})