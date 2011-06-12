var common = {};

common.initLayout = function (layoutId, navId, contentId, navTitle, contentTitle) {
    $("#" + navId).attr({ region: "west", split: true, title: navTitle || "" });
    $("#" + contentId).attr({ region: "center", title: contentTitle || "" });
    $("#" + layoutId).height($(window).height()).layout();
}

common.initCategoryTree = function (id, url, selectedId, dblClickHandler) {
    var e = $("#" + id);
    e.load(url, function () {
        e.tree({
            onLoadSuccess: function () {
                var selected = $(this).find('[node-id="' + selectedId + '"]')
                if (selected.size() == 1) {
                    $(this).tree("collapseAll").tree("expandTo", selected).tree("select", selected);
                }
            },
            onDblClick: dblClickHandler
        });
    });
}