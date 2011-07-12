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
        if ($("#simpleExcel").size() < 1) {
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
        }
        //inint head
        $("#simpleExcel th").live("click", function (e) {
            var _x = this.getAttribute("xx");
            var _y = this.getAttribute("yy");
            methods.getSelectedCells().removeClass(cg.selectedCellClass);
            if (_x == "0" && _y != "0") {
                excel.find("td[yy='" + _y + "']").addClass(cg.selectedCellClass);
            } else if (_x != "0" && _y == "0") {
                $(this).nextAll("td").addClass(cg.selectedCellClass);
            }
            e.preventDefault();
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
                var $tr = $(_tr);
                _preTR.after($tr);
                init_Cell($tr.find("td"));
                cg.rows++;
            },
            addColumn: function () {
                var _y = cg.columns - 1;
                var _lastCell = methods.getSelectedCells().last();
                if (_lastCell.size() > 0) {
                    _y = parseInt(_lastCell.attr("yy"));
                }
                excel.find("tr").each(function (i) {
                    $(this).find("th[yy='" + _y + "'],td[yy='" + _y + "']").each(function () {
                        var $$ = $(this)
                        $$.nextAll().each(function () {
                            var _$$ = $(this);
                            var _yy = parseInt(_$$.attr("yy")) + 1;
                            _$$.attr("yy", _yy);
                            if (this.tagName == "TH") {
                                _$$.text(_yy);
                            }

                        });
                        if (this.tagName == "TH") {
                            $$.after('<th id="' + $.newGuid() + '"  xx="0" yy="' + (_y + 1) + '" width="' + cg.cellWidth + 'px">' + (_y + 1) + '</th>');
                        } else {
                            $$.after(init_Cell($($.format(_tdStr, $.newGuid(), i, (_y + 1)))));
                        }
                    });
                });
                cg.columns++;
            },
            removeRow: function () {
                if (cg.rows < 3) { alert("最少保留一行"); return false; }
                if (confirm("确认删除行？")) {
                    var _removeTR = excel.find("tr:last");
                    var _lastCell = methods.getSelectedCells().last();
                    if (_lastCell.size() > 0) {
                        _removeTR = _lastCell.parent("tr");
                    }
                    _removeTR.nextAll("tr").find("td,th").each(function () {
                        var $$ = $(this);
                        var _x = parseInt($$.attr("xx")) - 1;
                        if (this.tagName == "TH") {
                            $$.text(_x);
                        }
                        $$.attr("xx", _x);
                    });
                    _removeTR.remove();
                    cg.rows--;
                }
            },
            removeColumn: function () {
                if (cg.columns < 3) {
                    alert("最少保留1列");
                    return false;
                }
                if (confirm("确认删除列？")) {
                    var _y = cg.columns - 1;
                    var _lastCell = methods.getSelectedCells().last();
                    if (_lastCell.size() > 0) {
                        _y = parseInt(_lastCell.attr("yy"));
                    }
                    excel.find("tr").each(function (i) {
                        var $$ = $(this).find("th[yy='" + _y + "'],td[yy='" + _y + "']");
                        $$.nextAll().each(function () {
                            var _$$ = $(this);
                            var _yy = parseInt(_$$.attr("yy")) - 1;
                            _$$.attr("yy", _yy);
                            if (this.tagName = "TH") {
                                _$$.text(_yy);
                            }
                        });
                        $$.remove();
                    });
                    cg.columns--;
                }
            },
            clearCell: function () {
                this.getSelectedCells().text("");
            },
            mergeCell: function () { 
            
            },
            splitCell: function () { },
            getSelectedCells: function () {
                return excel.find("td." + cg.selectedCellClass);
            }
        };
        var init_Cell = function (tds) {
            return tds.bind("contextmenu", function (e) {
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