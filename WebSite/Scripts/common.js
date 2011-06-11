var common = {};

common.initLayout = function (layoutId, contentId, navId, navTitle) {
    $("#" + navId).attr({ region: "west", split: "true", title: navTitle });
    $("#" + contentId).attr({ region: "center" });
    $("#" + layoutId).height($(document).height()).layout();
}

common.initCategoryTree = function (id, url, selectedId) {
    var e = $("#" + id);
    e.load(url, function (o) {
        e.tree({
            onLoadSuccess: function (data) {
                $(this).tree("select", $(this).find('[node-id="' + selectedId + '"]'));
            }
        });
    });
}