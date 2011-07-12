/**
* 肖宏飞
* 2011-07-01
* 
*/
; (function ($) {
    $.fn.showdiv = function (o) {
        var opt = $.extend({}, $.fn.showdiv.config, o);
        var win = $(window), dom = $(document), bd = $("body");
        bd.data("of", bd.css("overflow"));

        //create div        
        if ($("#bgdiv").size() != 0) {
            var div = $("#bgdiv");
        } else {
            bd.css("overflow", "hidden");
            var div = $('<div id="bgdiv"></div>').appendTo("body");
            div.css({
                "backgroundColor": opt.bgcolor,
                "zIndex": 9998,
                "margin": 0,
                "padding": 0,
                "position": "absolute",
                "opacity": opt.bgopacity,
                "top": 0,
                "left": 0
            })
            .hide()
            .click(function () {
                $.closediv();
            });
            bd.css("overflow", bd.data("of"));
        }

        return this.each(function (i) {
            var $this = $(this);
            var _opt = $.metadata ? $.extend({}, opt, $this.metadata()) : opt;

            var targetDiv = $(this.getAttribute(_opt.attrName));
            if (targetDiv.hasClass("wraped")) {
                $this.bind("click", function (e) {
                    e.preventDefault();
                    show(targetDiv.parent());
                });
                return true;
            }
            targetDiv.addClass("wraped");
            var wrapDiv = $('<div id="wrap_' + i + '"></div>');
            var offset = $this.offset();
            var t = (win.height() - targetDiv.height()) / 2 - 20;
            var l = (win.width() - targetDiv.width()) / 2;
            wrapDiv.width(targetDiv.width())
            .css({
                "position": "absolute",
                "zIndex": 9999,
                "border": _opt.warpborder
            })
            .hide();
            targetDiv.wrap(wrapDiv);

            $this.bind("click", function (e) {
                e.preventDefault();
                show("#wrap_" + i);
            });
        });

        function show(d) {
            bd.css("overflow", "hidden").find("select").attr("visibility", "hidden");
            var _div = (typeof d == "string") ? $(d) : d;
            var st = win.scrollTop(), sl = win.scrollLeft(), w_w = win.width(), w_h = win.height();

            div.data("did", d)
            .css({ top: st, left: sl })
            .width(w_w).height(w_h).show();

            _div.css({
                top: st + (w_h - _div.height()) / 2 - 20,
                left: sl + (w_w - _div.width()) / 2
            }).show();
        }
    };

    $.extend({
        closediv: function () {
            $("body").css({ "overflow": "auto" }).find("select").removeAttr("visibility");
            $($("#bgdiv").hide().data("did")).hide();
        }
    });
    $.fn.showdiv.config = { bgcolor: "#ccc", attrName: "href",bgopacity:0.4,warpborder:"8px solid #ccc" };
})(jQuery);