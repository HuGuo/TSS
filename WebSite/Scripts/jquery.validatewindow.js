(function ($) {
    $.Validate = function (formId) {
        $("#" + formId).validate();
    }
    $.InitWindow = function (id, width, height) {
        var dlg = $("#" + id).window({
            title: '设备类型',
            width: width,
            height: height,
            modal: true,
            shadow: false,
            closed: true,
            closable: false,
            collapsible: false,
            minimizable: false,
            maximizable: false
        });
        //默认将window窗口附加到body之后，无法响服务器端应按钮事件，所以将其附加到form
        dlg.parent().appendTo($("form:first"));
    }

    $.Open = function (id) {
        $("#" + id).window("open");
    }

    $.Close = function (id) {
        Close(id);
    }

    function Close(id) {
        $("#" + id).window("close");
    }

    $.Add = function (windowId, updatePanelId) {
        var result = valid(updatePanelId);
        if (result) { Close(windowId); }
        return result;
    }

    $.Edit = function (windowId, updatePanelId) {
        var result = valid(updatePanelId);
        if (result) { Close(windowId); }
        return result;
    }

    function valid(containId) {
        var valid = true;
        $("#" + containId).find("input[validate],select[validate]").each(function () {
            if (!$(this).valid()) {
                valid = false;
            }
        });
        return valid;
    }

    $.Confirm = function (msg, control) {
        $.messager.confirm('确认', msg, function (r) {
            if (r) {
                eval(control.toString().slice(11)); //截掉 javascript: 并执行
            }
        });
        return false;
    }
})(jQuery)