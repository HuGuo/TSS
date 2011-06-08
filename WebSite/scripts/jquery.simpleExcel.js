(function ($) {
    $.simpleExcel = {
        _op: { container: $("body"), cellW: 75, cellH: 25, excell: null, rows: 0, columns: 0, selectedCellClass: "selectedCell" },
        create: function (x, y) {
            if (x < 1 || y < 1) { alert("指定的行数和列数必须大于1"); return false; }
            x++;
            y++;
            var _str = '<table id="simpleExcel" class="myTB">';
            var _tr = "";
            for (var i = 0; i < x; i++) {
                _tr = '<tr>';
                for (var j = 0; j < y; j++) {
                    if (i == 0) {
                        _tr = _tr + String.format('<th id="{0}" width="{1}px"  xx="0" yy="{2}">{2}</th>', guid(), j == 0 ? 40 : $.simpleExcel._op.cellW, j);
                    } else {
                        if (j == 0) {
                            _tr = _tr + String.format('<th id="{0}"  xx="{1}" yy="0">{1}</th>', guid(), i);
                        } else {
                            _tr = _tr + String.format('<td id="{0}" xx="{1}" yy="{2}"></td>', guid(), i, j);
                        }
                    }
                }
                _tr += "</tr>";
                _str += _tr;
            }
            _str += "</table>";
            if ($.simpleExcel._op.container) {
                $.simpleExcel._op.excell = $(_str);
                $.simpleExcel._op.container.empty().append($.simpleExcel._op.excell);
                $.simpleExcel._init($.simpleExcel._op.excell.find("td"));
                $.simpleExcel._initHead();
                $.simpleExcel._op.rows = x;
                $.simpleExcel._op.columns = y;
            }
        },
        _init: function (tds) {
            if (!$.simpleExcel._op.excell) { return false; }
            tds.bind("contextmenu", function (e) {
                  var $m = $('#ct_menu');
//                var mW = $m.width();
//                var mH = $m.height();
//                var winW = $(document).width();
//                var winH = $(document).height();
//                var _x = 0; var _y = 0;
//                _x = (e.pageX + mW) > winW ? (e.pageX - mW - 5) : e.pageX;
//                _y = (e.pageY + mH) > winH ? (e.pageY - mH - 5) : e.pageY;
                $m.menu('show', {
                    left: e.pageX,
                    top: e.pageY
                });
                var $this = $(this);
                if (!$this.hasClass($.simpleExcel._op.selectedCellClass)) {
                    $.simpleExcel._op.excell.find("td." + $.simpleExcel._op.selectedCellClass).removeClass($.simpleExcel._op.selectedCellClass);
                    $this.addClass($.simpleExcel._op.selectedCellClass);
                }
                return false;
            }).bind("click", function (e) {
                var $this = $(this);
                if (!e.ctrlKey) {
                    $("#simpleExcel td." + $.simpleExcel._op.selectedCellClass).removeClass($.simpleExcel._op.selectedCellClass);
                } else {
                    if ($this.hasClass($.simpleExcel._op.selectedCellClass)) {
                        $this.removeClass($.simpleExcel._op.selectedCellClass);
                        return false;
                    }
                }
                $this.addClass($.simpleExcel._op.selectedCellClass);
            }).bind("dblclick", function () {
                var $this = $(this);
                if ($this.find("textarea").size() > 0) {
                    return false;
                }
                var _textArea = $(String.format('<textarea style="width:{0}px;height:{1}px">{2}</textarea>', ($this.width() - 5), ($this.height() - 5), $this.text()));
                $this.empty().append(_textArea);
                _textArea.focus().bind("blur", function () {
                    $this.html(_textArea.val());
                });
            });
        },
        _initHead: function () {
            $("#simpleExcel th").live("click", function () {
                var $this = $(this);
                var _xx = parseInt($this.attr("xx"));
                var _yy = parseInt($this.attr("yy"));
                $.simpleExcel._op.excell.find("td." + $.simpleExcel._op.selectedCellClass).removeClass($.simpleExcel._op.selectedCellClass);
                if (_xx == 0 && _yy == 0) {
                } else if (_xx == 0) {
                    $.simpleExcel._op.excell.find("td[yy='" + _yy + "']").addClass($.simpleExcel._op.selectedCellClass);
                } else if (_yy == 0) {
                    $this.parent("tr").find("td").addClass($.simpleExcel._op.selectedCellClass);
                }
            });
        },
        appendRow: function () {
            if (!$.simpleExcel._op.excell) { return false; }
            var _selectedCellLast = $.simpleExcel._op.excell.find("td." + $.simpleExcel._op.selectedCellClass + ":last");
            var _xx = $.simpleExcel._op.rows;
            var _p = null;

            if (_selectedCellLast.size() > 0) {
                _xx = parseInt(_selectedCellLast.attr("xx")) + 1;
                _p = _selectedCellLast.parent("tr");
            } else { _p = $.simpleExcel._op.excell.find("tr:last"); }
            var _trStr = '<tr><th  xx="{0}" yy="0">{0}</th>';
            var _tdStr = '<td id="{0}"   xx="{1}" yy="{2}"></td>';

            var _cs = $.simpleExcel._op.columns;
            var _tr = String.format(_trStr, _xx);
            for (var i = 1; i < _cs; i++) {
                _tr = _tr + String.format(_tdStr, guid(), _xx, i);
            }
            _tr += "</tr>";
            var _jqTR = $(_tr);
            _p.after(_jqTR);
            $.simpleExcel._init(_jqTR.find("td"));
            //change xx yy
            _jqTR.nextAll("tr").find("th,td").each(function () {
                var _$this = $(this);
                if (this.tagName == "TH") {
                    _$this.text(parseInt(_$this.attr("xx")) + 1);
                }
                _$this.attr("xx", parseInt(_$this.attr("xx")) + 1);
            });

            $.simpleExcel._op.rows++;
        },
        removeRow: function () {
            if (!$.simpleExcel._op.excell) { return false; }
            if ($.simpleExcel._op.rows < 3) {
                alert("最少保留1行");
                return false;
            }
            if (confirm("确认删除行？")) {
                var _selectedCellLast = $.simpleExcel._op.excell.find("td." + $.simpleExcel._op.selectedCellClass + ":last");
                var _removeTR = null;
                if (_selectedCellLast.size() > 0) {
                    _removeTR = _selectedCellLast.parent("tr");
                } else {
                    _removeTR = $.simpleExcel._op.excell.find("tr:last");
                }
                _removeTR.nextAll("tr").find("th,td").each(function () {
                    var $this = $(this);
                    if (this.tagName == "TH") {
                        $this.text(parseInt($this.attr("xx")) - 1);
                    }
                    $this.attr("xx", parseInt($this.attr("xx")) - 1);
                });
                _removeTR.remove();
                $.simpleExcel._op.rows--;
            }
        },
        appendColumn: function () {
            if (!$.simpleExcel._op.excell) { return false; }
            var _selectedCellLast = $.simpleExcel._op.excell.find("td." + $.simpleExcel._op.selectedCellClass + ":last");
            var _yy = $.simpleExcel._op.rows;
            if (_selectedCellLast.size() > 0) {
                _yy = parseInt(_selectedCellLast.attr("yy"));
            }
            var _tdStr = '<td id="{0}"  xx="{1}" yy="{2}"></td>';
            $.simpleExcel._op.excell.find("tr").each(function (i) {
                var $this = $(this).find("th[yy='" + _yy + "'],td[yy='" + _yy + "']");
                if (i == 0) {
                    var $th = $('<th id="' + guid() + '"  xx="0" yy="' + (_yy + 1) + '" width="' + $.simpleExcel._op.cellW + 'px">' + (_yy + 1) + '</th>');
                    $this.after($th);
                    $th.nextAll("th").each(function () {
                        var _$this = $(this);
                        var __yy = parseInt(_$this.attr("yy")) + 1;
                        $(this).text(__yy).attr("yy", __yy);
                    });
                } else {
                    var $td = $(String.format(_tdStr, guid(), i, _yy + 1));
                    $this.after($td);
                    $.simpleExcel._init($td);
                    $td.nextAll("td").each(function () {
                        var _$this = $(this);
                        _$this.attr("yy", parseInt(_$this.attr("yy")) + 1);
                    });
                }
            });
            $.simpleExcel._op.columns++;
        },
        removeColumn: function () {
            if (!$.simpleExcel._op.excell) { return false; }
            if ($.simpleExcel._op.columns < 3) {
                alert("最少保留1列");
                return false;
            }
            if (confirm("确认删除列？")) {
                var _selectedCellLast = $.simpleExcel._op.excell.find("td." + $.simpleExcel._op.selectedCellClass + ":last");
                var _yy = $.simpleExcel._op.rows;
                if (_selectedCellLast.size() > 0) {
                    _yy = parseInt(_selectedCellLast.attr("yy"));
                }
                $("tr", $.simpleExcel._op.excell).each(function (i) {
                    if (i == 0) {
                        var $th = $(this).find("th[yy='" + _yy + "']");
                        $th.nextAll("th").each(function () {
                            var _$this = $(this);
                            var __yy = parseInt(_$this.attr("yy")) - 1;
                            $(this).text(__yy).attr("yy", __yy);
                        });
                        $th.remove();
                    } else {
                        var $td = $(this).find("td[yy='" + _yy + "']");
                        $td.nextAll("td").each(function () {
                            var _$this = $(this);
                            _$this.attr("yy", parseInt(_$this.attr("yy")) - 1);
                        });
                        $td.remove();
                    }
                });
                $.simpleExcel._op.columns--;
            }
        },
        clearCell: function () {
            $.simpleExcel._op.excell.find("td." + $.simpleExcel._op.selectedCellClass).text("");
        },
        setStyle: function (o) {
            $.extend(o, {});
            var $clickItem = $.simpleExcel._op.excell.find("td." + $.simpleExcel._op.selectedCellClass);
            if (o.fontWeight) {
                if ($clickItem.css("fontWeight") == o.fontWeight || $clickItem.css("fontWeight")=="bold") {
                    o.fontWeight = "400";}}
            $clickItem.css(o);
        },
        mergeCell: function () {
            var _selectedCells = $.simpleExcel._op.excell.find("td." + $.simpleExcel._op.selectedCellClass);
            var _n = _selectedCells.size();
            if (_n < 2) { return false; }
            var _x = []; var _y = []; var __c = 0;
            $.each(_selectedCells, function (i, q) {
                var $this = $(this);
                _x.push($this.attr("yy"));
                _x.push(parseInt($this.attr("yy")) + q.colSpan - 1);
                _y.push($this.attr("xx"));
                _y.push(parseInt($this.attr("xx")) + q.rowSpan - 1);
                if (q.rowSpan > 1 || q.colSpan > 1) {
                    __c += (q.rowSpan * q.colSpan - 1); }
            });
            var _xmax_min = maxmin(_x) + 1;
            var _ymax_min = maxmin(_y) + 1;

            if ((_n + __c) != _xmax_min * _ymax_min) { alert("所选单元格无法合并"); return false; }
            _selectedCells.first().attr({ rowspan: _ymax_min, colspan: _xmax_min });
            _selectedCells.slice(1).remove();
        },
        splitCell: function () {
            var _selectedCell = $.simpleExcel._op.excell.find("td." + $.simpleExcel._op.selectedCellClass);
            if (_selectedCell.size() > 1) { return false; }
            var _rowSpan = _selectedCell.attr("rowspan");
            var _colSpan = _selectedCell.attr("colspan");
            var _xx = parseInt(_selectedCell.attr("xx"));
            var _yy = parseInt(_selectedCell.attr("yy"));
            if (_rowSpan < 2 && _colSpan < 2) { return false; }
            for (var i = _colSpan - 1; i > 0; i--) {
                var _td = $(String.format('<td id="{0}" xx="{1}" yy="{2}"></td>', guid(), _xx, _yy + i));
                $.simpleExcel._init(_td);
                _selectedCell.after(_td);
            }
            for (var j = 1; j < _rowSpan; j++) {
                var _id = "td[xx='" + (_xx + j) + "'][yy='" + (_yy - 1) + "']";
                var nexttd = $(_id);
                if (nexttd) {
                    for (var k = _colSpan; k > 0; k--) {
                        var _td = $(String.format('<td id="{0}" xx="{1}" yy="{2}"></td>', guid(), _xx + j, _yy + k));
                        $.simpleExcel._init(_td);
                        nexttd.after(_td);
                    }
                }
            }
            _selectedCell.attr("rowSpan", "1").attr("colSpan", "1");
        }
    };

    String.format = function (src) {
        if (arguments.length == 0) return null;
        var args = Array.prototype.slice.call(arguments, 1);
        return src.replace(/\{(\d+)\}/g, function (m, i) {
            return args[i];
        });
    }

    function maxmin(a) {
        var _max = a[0];
        var _min = a[0];
        for (var i = 1; i < a.length; i++) {
            _max = Math.max(_max, a[i]);
            _min = Math.min(_min, a[i]);
        }
        return _max - _min;
    }

    function guid() {
        var s = 'xxxxxxxx-xxxx-4xxx-yxxx-xxxxxxxxxxxx'.replace(/[xy]/g, function (c) {
            var r = Math.random() * 16 | 0; var v = c == 'x' ? r : (r & 0x3 | 0x8);
            return v.toString(16);
        });
        return s;
    }
})(jQuery);