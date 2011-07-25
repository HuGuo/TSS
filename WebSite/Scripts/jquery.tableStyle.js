/**
* 肖宏飞
* 2011-7-20
*/
if (window.jQuery) {

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
            "bindHover": function () {
                var hoverCls = "hover_row";
                return this.hover(
                function () { $(this).addClass(hoverCls); },
                function () { $(this).removeClass(hoverCls); });
            }
        });
    })(jQuery)

    $(document).ready(function () {
        $("table.datasheet")
        .attr("cellpadding", "0").attr("cellspacing", "0")
        .find("tbody tr").addClass("datasheet_row")
        .bindHover()
        .find("th").click(function () {
            var ptr = $(this).parent("tr");
            if (ptr.hasClass("selected_row")) {
                ptr.removeClass("selected_row");
            }
            else {
                $("tr.selected_row").removeClass("selected_row");
                ptr.addClass("selected_row");
            }
            return false;
        });
    });
} else {
alert("缺少 jquery库文件");
}