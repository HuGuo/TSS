/**
* 肖宏飞 
* 2011-5-20
*/
(function ($) {
    $.fn.extend({
        "bindDelete": function (o) {
            var opt = { handler: "delete.ashx", op: "", onSuccess: function (k) { $("#tr_" + k).remove(); } };
            $.extend(opt, o);
            return this.bind("click", function () {
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
        }
    });
})(jQuery)