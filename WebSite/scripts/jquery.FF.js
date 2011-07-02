/**
* 肖宏飞 
* 2011-5-20
*/
var cfg = { s: '', pid: '',
        $ul: $("#mylist"),
        $li: '<li><a href="#" pid="{0}" isfolder="{1}" title="{2}"><div class="ico {3}"></div><div class="filename">{2}</div></a></li>'
    };
$(document).ready(function () {
    $.extend({ format: function (src) {
        if (arguments.length == 0) return null;
        var args = Array.prototype.slice.call(arguments, 1);
        return src.replace(/\{(\d+)\}/g, function (m, i) {
            return args[i];
        });
    }
    });
    $('#dg_upfile,#dg_newfolder').dialog('close');
    $("#upload,#new_folder").click(function () { $($(this).attr("href")).dialog('open'); });
    $("#cancelUpload").click(function () {
        $('#upDemo').uploadifyClearQueue();
        $('#dg_upfile').dialog('close');
    });
    //init uploadify
    $("#upload").one("click", function () {
        $("#upDemo").uploadify({
            'uploader': '../uploadify/uploadify.swf',
            'cancelImg': '../uploadify/cancel.png',
            'script': 'upload.ashx',
            'scriptData': { op: "file", s: cfg.s, pid: cfg.pid },
            'multi': true,
            'auto': false,
            'onComplete': function (event, ID, fileObj, response, data) {
                var json = eval('(' + response + ')');
                if (json.errorMsg == '') {
                    cfg.$ul.append($.format(cfg.$li, json.id, "0", json.name, json.extension));
                } else {
                    alert(json.errorMsg);
                }
            },
            'onAllComplete': function (event, data) {
                $.messager.show({
                    title: '系统提示',
                    msg: data.filesUploaded + ' 个文件上传成功!',
                    timeout: 3000,
                    showType: 'slide'
                });
                $('#dg_upfile').dialog('close');
            }
        });
    });
    $("#mylist a").live("click", function () {
        cfg.$ul.find("a.selected").removeClass("selected");
        $(this).addClass("selected");
    }).live("dblclick", function () {
        var $$ = $(this);
        var $id = $$.attr("pid");
        if ($$.attr("isfolder") == "0") {
            //下载文件
            window.open("../upload.ashx?op=download&id=" + $id);
        } else {
            //打开文件夹
            document.location.href = "default.aspx?s=" + cfg.s + "&pid=" + $id;
        }
    });
    $("#goSearch").click(function () {
        var q = { s: cfg.s, key: $.trim($("#txtKey").val()) };
        if (q.key != "") {
            document.location.href = "search.aspx?" + $.param(q);
        }
    });
});

function newFolder() {
    var q = { op: "folder", s:cfg.s, pid: cfg.pid, name: "" };
    q.name = $.trim($("#NN").val());
    if (q.n != "") {
        $.get("../upload.ashx", q, function (data) {
            var json = eval('(' + data + ')');
            if (json.errorMsg == "") {
                cfg.$ul.append($.format(cfg.$li, json.id, "1", json.name, json.extension));
                $('#dg_newfolder').dialog('close');
            } else {
                alert(json.errorMsg);
            }
        });
    }
}

function delItem() {
    var $selitems = cfg.$ul.find("a.selected");
    if ($selitems.size() < 1) {
        return false;
    }
    $.messager.confirm('删除提示', '确定删除所选项？', function (r) {
        if (r) {
            var q = { op: "delete", id: "" };
            $selitems.each(function () {
                var $$ = $(this);
                q.id = $$.attr("pid");
                $.get("../upload.ashx", q, function (data) {
                    if (data == "") {
                        $$.parent("li").remove();
                    } else {
                        alert(data);
                    }
                });
            });
        }
    });
}