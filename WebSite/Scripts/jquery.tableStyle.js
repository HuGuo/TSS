if (window.jQuery) {
    
    $(document).ready(function () {
        $("table.datasheet tbody tr").addClass("datasheet_row")
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