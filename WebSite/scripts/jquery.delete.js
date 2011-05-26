$.fn.bindDelete = function (o) {
    var opt = { handler: "delete.ashx", op: "", onSuccess: null };
    $.extend(opt, o);
    this.bind("click", function () {
        if (confirm("确定删除")) {
            var query = { op: opt.op, id: $(this).attr("key") };
            $.get(opt.handler, query, function (data) {
                if (data != "") {
                    alert(data);
                } else {
                    if (opt.onSuccess) {
                        opt.onSuccess(query.id);
                    } else {
                        alert("删除成功");
                    }
                }
            });
        }
    });
};