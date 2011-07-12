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
            var opt = { color: "#F0E68C" };
            $.extend(opt, o);
            $("<style type=\"text/css\">.hover td,.checked td{background-Color:" + opt.color + ";}</style>").appendTo("head");
            return this.hover(
            function () { $(this).addClass("hover"); },
            function () { $(this).removeClass("hover"); }
            ).toggle(
            function () { $(this).addClass("checked"); },
            function () { $(this).removeClass("checked"); }
            );
        },
        "alternateColor": function (o) {
            var colors = { "color1": "#FFF", "color2": "#FFFACD" };
            $.extend(colors, o);
            $("<style type=\"text/css\">.c1 td{background-Color:" + colors.color1 + ";} .c2 td{background-Color:" + colors.color2 + ";}</style>").appendTo("head");
            //            this.filter(":even").addClass("c1");
            //            this.filter(":odd").addClass("c2");
            //            return this;
            return this.each(function (i) {
                $(this).addClass((i % 2 == 0) ? "c1" : "c2");
            });
        }
    });
})(jQuery)