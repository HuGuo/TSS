/**
* 肖宏飞 
* 2011-5-20
*/
(function ($) {
    $.fn.extend({
        "bindDelete": function (o) {
            var opt = { handler: "delete.ashx", op: "", onSuccess: function (k) { $("#tr_" + k).remove(); } };
            $.extend(opt, o);
            return this.bind("click", function (e) {
                if (confirm("确定删除")) {
                    var query = { op: opt.op, id: $(this).attr("key") };
                    $.get(opt.handler, query, function (data) {
                        if (data != "") {
                            alert(data);
                        } else {
                            if (opt.onSuccess) {
                                opt.onSuccess(query.id);
                            }
                        }
                    });
                }
                e.preventDefault();
            });
        },
        "hoverColor": function (o) {
            var opt = { hoverColor: "#FFF", checkedColor: "#F0E68C", hoverCls: "", checkedCls: "" };
            $.extend(opt, o);
            var style = "<style type=\"text/css\">";
            if (opt.hoverCls == "") {
                style += ".hover td{background-Color:" + opt.hoverColor + ";}";
                opt.cls = "hover";
            }
            if (opt.checkedCls == "") {
                style += ".checked td{background-Color:" + opt.checkedColor + ";}";
                opt.checkedCls = "checked";
            }
            style += "</style>"
            $("head").append(style);
            return this.hover(
                function () { $(this).addClass(opt.cls); },
                function () { $(this).removeClass(opt.cls); }
            )
            .click(function () { $(this).toggleClass(opt.checkedCls); });
        },
        "alternateColor": function (o) {
            var colors = { "color1": "#F7F8F9", "color2": "#F2F2F2" };
            $.extend(colors, o);
            $("<style type=\"text/css\">.c1 td{background-Color:" + colors.color1 + "; height:27px;} .c2 td{background-Color:" + colors.color2 + "; height:27px;}</style>").appendTo("head");

            return this.each(function (i) {
                $(this).addClass((i % 2 == 0) ? "c1" : "c2");
            });
        }
    });
})(jQuery)