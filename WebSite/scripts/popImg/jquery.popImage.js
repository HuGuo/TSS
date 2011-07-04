(function ($) {
    var root;
    $('script').each(function (a, tag) {
        miuScript = this.src.match(/(.*)jquery\.pop[Ii]mage(\.mini)?\.js$/);
        if (miuScript !== null) {
            root = miuScript[1];
        }
    });

    var css_href = root + 'popImage.css';
    var styleTag = document.createElement("link");
    styleTag.setAttribute('type', 'text/css');
    styleTag.setAttribute('rel', 'stylesheet');
    styleTag.setAttribute('href', css_href);
    $("head")[0].appendChild(styleTag);
    $.fn.popImage = function (options) {
        var s = $.extend({}, $.fn.popImage.defaultSettings, options || {});
        if ($("#popImage_cache").length == 0) {
            $("<div id='popImage_cache'></div><div class='popImage_close'></div>").appendTo("body");
        }
        var item_num = $("#popImage_cache img").length;
        return this.each(function (index) {
            var $$ = $(this),
			iw = $$.outerWidth(),
			ih = $$.outerHeight(),
			imgUrl = this.getAttribute(s.tagName),
			index = item_num + index,
			this_id = "slide" + index;

            $('<img src="' + imgUrl + '" class="popImage_cached ' + this_id + '"/>').appendTo("#popImage_cache").hide();
            $$.click(function (e) {
                var animate_image = $('#popImage_cache .' + this_id),
				w_w = $(window).width(),
				w_h = $(window).height(),
				st = $(window).scrollTop();
                $('.popImage_close').hide();
                e.preventDefault();
                position = $$.offset(),
                o_h = animate_image.height() > w_h ? (w_h - 80) : animate_image.height() - 50;
                o_w = animate_image.width() > w_w ? (w_w - 80) : animate_image.width() - 50;

                var t = st + (w_h - o_h) / 2,
				l = (w_w - o_w) / 2;

                animate_image.css({ 'left': position.left, 'top': position.top, 'height': ih, 'width': iw });
                $('.popImage_cached').hide();
                animate_image.show().fadeTo(0, 0.9);
                animate_image.animate({ 'left': l, 'top': t, 'height': o_h, 'width': o_w, 'opacity': 1 }, s.timeOut, function () {
                    var position2 = animate_image.offset();
                    $('.popImage_close').css({ 'left': position2.left + o_w - 6, 'top': position2.top - 15 }).show();
                });
            });
            $('.popImage_close').bind('click', function (a) {
                $('.popImage_close').hide();
                $('.popImage_cached').fadeOut(400);
                a.preventDefault();
            });
        });
    };
    $.fn.popImage.defaultSettings = {
        "tagName": "href",
        "timeOut": "600"
    };
})(jQuery);