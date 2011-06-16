/**
*/
(function ($) {
    $.extend({
        format: function (src) {
            if (arguments.length == 0) return null;
            var args = Array.prototype.slice.call(arguments, 1);
            return src.replace(/\{(\d+)\}/g, function (m, i) {
                return args[i];
            });
        },
        subtraction: function (a) {
            var _max = a[0];
            var _min = a[0];
            for (var i = 1; i < a.length; i++) {
                _max = Math.max(_max, a[i]);
                _min = Math.min(_min, a[i]);
            }
            return _max - _min;
        },
        newGuid: function () {
            var s = 'xxxxxxxx-xxxx-4xxx-yxxx-xxxxxxxxxxxx'.replace(/[xy]/g, function (c) {
                var r = Math.random() * 16 | 0; var v = c == 'x' ? r : (r & 0x3 | 0x8);
                return v.toString(16);
            });
            return s;
        }
    });

    $.fn.simpleExcel = function (o) {
        var cg = $.extend($.fn.simpleExcel.defaultSettings, o);
        if (cg.rows < 1 || cg.columns < 1) {
            alert("输入行数和列数必须为大于0的正整数");
            return;
        }
        //create table
        var _str = '<table id="simpleExcel" class="myTB">';
        var _tr = "";
        for (var i = 0; i < cg.rows; i++) {
            _tr = '<tr>';
            for (var j = 0; j < cg.columns; j++) {
                if (i == 0) {
                    _tr = _tr + $.format('<th id="{0}" width="{1}px"  xx="0" yy="{2}">{2}</th>', $.newGuid(), j == 0 ? 40 : cg.cellWidth, j);
                } else {
                    if (j == 0) {
                        _tr = _tr + $.format('<th id="{0}"  xx="{1}" yy="0">{1}</th>', $.newGuid(), i);
                    } else {
                        _tr = _tr + $.format('<td id="{0}" xx="{1}" yy="{2}" width="{3}px"></td>', $.newGuid(), i, j, cg.cellWidth);
                    }
                }
            }
            _tr += "</tr>";
            _str += _tr;
        }
        _str += "</table>";

        this.empty().append(_str);
        //inint head
        $("#simpleExcel th").live("click", function () {
            var _x = this.getAttribute("xx");
            var _y = this.getAttribute("yy");
            methods.getSelectedCells().removeClass(cg.selectedCellClass);
            if (_x == "0" && _y != "0") {
                excel.find("td[yy='" + _y + "']").addClass(cg.selectedCellClass);
            } else if (_x != "0" && _y == "0") {
                $(this).nextAll("td").addClass(cg.selectedCellClass);
            }
        });

        //
        cg.menu.menu({
            onClick: function (item) {
                cg.menu.removeData("srcElement");
            }
        });

        var _trStr = '<tr><th  xx="{0}" yy="0">{0}</th>';
        var _tdStr = '<td id="{0}"   xx="{1}" yy="{2}"></td>';
        var excel = $("#simpleExcel");
        var methods = {
            initTD: function (o) {
            },
            addRow: function () {
                var _x = cg.rows;
                var _lastCell = methods.getSelectedCells().last();
                var _preTR = excel.find("tr:last");
                if (_lastCell.size() > 0) {
                    _x = parseInt(_lastCell.attr("xx"));
                    _preTR = _lastCell.parent("tr");
                }
                var _tr = $.format(_trStr, _x);
                for (var i = 1; i < cg.columns; i++) {
                    _tr = _tr + $.format(_tdStr, $.newGuid(), _x, i);
                }
                _preTR.nextAll("tr").find("th,td").each(function () {
                    var $$ = $(this);
                    var _xx = parseInt($$.attr("xx")) + 1;
                    if (this.tagName == "TH") {
                        $$.text(_xx);
                    }
                    $$.attr("xx", _xx);
                });
                _preTR.after(_tr);
                this.initTD($(_tr).find("td"));
                cg.rows++;
            },
            addColumn: function () {
                if (cg.rows < 3) {
                    alert("最少保留一行");
                    return false;
                }
                if (confirm("确认删除行？")) {

                }
                cg.rows--;
            },
            removeRow: function () { },
            removeColumn: function () { },
            clearCell: function () { },
            mergeCell: function () { },
            splitCell: function () { },
            getSelectedCells: function () {
                return excel.find("td." + cg.selectedCellClass);
            },
            test: function () {
                return excel.find("tr").size();
            }
        };

        var init_Cell = function (tds) {            
            tds.bind("contextmenu", function (e) {
                cg.menu.data("srcElement", this).menu("show", {
                    left: e.pageX,
                    top: e.pageY
                });
                if (!$(this).hasClass(cg.selectedCellClass)) {
                    excel.find("td." + cg.selectedCellClass).removeClass(cg.selectedCellClass);
                    $(this).addClass(cg.selectedCellClass);
                }
                e.preventDefault();
            })
            .bind("click", function (e) {
                var $$ = $(this);
                if (!e.ctrlKey) {
                    excel.find("td." + cg.selectedCellClass).removeClass(cg.selectedCellClass);
                    $$.addClass(cg.selectedCellClass);
                } else {
                    $$.hasClass(cg.selectedCellClass) ? $$.removeClass(cg.selectedCellClass) : $$.addClass(cg.selectedCellClass);
                }
            })
            .bind("dblclick", function (e) {
                var $$ = $(this);
                if ($$.find("textarea").size() > 1) {
                    return false;
                }
                var _textArea = $($.format('<textarea style="width:{0}px;height:{1}px">{2}</textarea>', ($$.width() - 5), ($$.height() - 5), $$.text()));
                $$.empty().append(_textArea);
                _textArea.focus().bind("blur", function () {
                    $$.html(_textArea.val());
                });
            })
        }

        init_Cell(excel.find("td"));
        return methods;
    };

    $.fn.simpleExcel.defaultSettings = { cellWidth: 85, cellHeight: 25, rows: 0, columns: 0, selectedCellClass: "selectedCell", menu: null };
})(jQuery)