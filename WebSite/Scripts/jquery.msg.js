if (jQuery) {
    (function (jQuery) {
        jQuery.extend({
            showMsg: function (o) {
                var opt = { msg: '', cls: 'success', showClose: true };
                jQuery.extend(opt, o);
                var jdiv = '<div style="display:block;position:fixed;z-index:222;width:100%;top:0;" class="' + opt.cls + '">' + opt.msg;
                if (opt.showClose) {
                    jdiv += '<a style="float:right;margin:5px 105px;font-size:9pt;color:inherit;" href="javascript:$(\'div.' + opt.cls + '\').remove();">关闭</a>';
                }
                jdiv += '</div>';
                $("body").after(jdiv);
            }
        });
    })(jQuery)
}