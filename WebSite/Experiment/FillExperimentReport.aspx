<%@ Page Language="C#" ValidateRequest="false" EnableViewState="false" AutoEventWireup="true" CodeFile="FillExperimentReport.aspx.cs" Inherits="Experiment_FillExperimentReport" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>试验报告数据</title>
    <link href="experiment.css" rel="stylesheet" type="text/css" />
    <script src="../scripts/jquery-1.6.1.min.js" type="text/javascript"></script>
    <script src="../scripts/My97DatePicker/WdatePicker.js" type="text/javascript"></script>
    <script src="../scripts/jquery-easyui/easyloader.js" type="text/javascript"></script>
    <style type="text/css">
    .text,.number,.time{ border:none;}
    .error{ color:#FF4500; font-weight:700;}
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div id="toolbar">
        试验报告名称<input type="text" id="txt_tmpName" runat="server" style="width: 380px;" />
        <input type="button" id="btnSave" value="保存报告" />
        试验时间
        <asp:TextBox ID="txt_expdate" runat="server" class="Wdate" onclick="WdatePicker()"></asp:TextBox>
        试验结果
        <asp:DropDownList ID="ddlResult" runat="server">
            <asp:ListItem Selected="True" Value="1">合格</asp:ListItem>
            <asp:ListItem Value="0">不合格</asp:ListItem>
        </asp:DropDownList>
    </div>
    <div id="dtb" style="margin-top: 31px;">
        <asp:Literal ID="ltHTML" runat="server"></asp:Literal>
    </div>
    <div style="position: fixed; top: 31px; right: 0; width: 250px; height: 500px">
        <div id="panel_equipmentInfo" title="设备信息" collapsible="true" style="height: 160px;
            overflow: auto;">
            <table cellpadding="0" cellspacing="0" style="width: 100%; border: 0;">
                <asp:Repeater ID="rptEquipment" runat="server">
                    <ItemTemplate>
                        <tr>
                            <th style="widht: 50px;">
                                <%#Eval("Lable")%>
                            </th>
                            <td>
                                <%#Eval("Value")%>
                            </td>
                        </tr>
                    </ItemTemplate>
                </asp:Repeater>
            </table>
        </div>
        <div id="panel_attachment" title="试验相关附件" collapsible="true" style="height: 160px;
            overflow: auto;">
            <table id="athlist" style="width: 100%; border: 0; border-collapse: collapse;">
                <asp:Repeater runat="server" ID="rptlistAttachment">
                    <ItemTemplate>
                        <tr id="tr_<%#Eval("Id") %>">
                            <td>
                            <a href="../exp.ashx?op=download&id=<%#Eval("Id") %>&filename=<%# Server.UrlEncode(Eval("FileName").ToString())%>"><%# Eval("FileName") %></a>
                            </td>
                            <td style="width:15px;"><a href="#" onclick="javascript:removeFile(this);" id="<%#Eval("Id") %>" filename="<%#Eval("FileName") %>">X</a></td>
                        </tr>
                    </ItemTemplate>
                </asp:Repeater>                
            </table>
            <div id="up"></div>
        </div>
        <div id="panel_remark" title="备注" collapsible="true" style="height: 160px; overflow: auto;">
            <textarea runat="server" id="txt_remark" style="width: 238px; height: 126px; overflow: auto;"></textarea>
        </div>
    </div>
    </form>
</body>
</html>
<script type="text/javascript">
    $.extend({ IsNumeric: function (input) {
        return (input - 0) == input || ($.trim(input).length == 0);}
});

var _id = '<%=Request.QueryString["id"] %>';
var _tid = '<%=Request.QueryString["tid"] %>';
var _eqmId = '<%=Request.QueryString["eqmId"] %>';
var handlerUrl="../exp.ashx";
$(window).load(function () {

    easyloader.load("panel", function () {
        $("#panel_equipmentInfo,#panel_attachment,#panel_remark").panel({ style: { marginBottom: "3px"} });
    })
    if (_id != "") {
        //edit model，init uploadify
        var file = $('<input type="file" id="upload">').appendTo("#up");
        $.getScript("../uploadify/swfobject.js", function () {
            $.getScript("../uploadify/jquery.uploadify.v2.1.4.min.js", function () {
                file.uploadify({
                    'uploader': '../uploadify/uploadify.swf',
                    'cancelImg': '../uploadify/cancel.png',
                    'script': handlerUrl,
                    'scriptData': { op: "upload", expid: _id },
                    'auto': true,
                    'onComplete': function (event, ID, fileObj, response, data) {
                        var json = eval('(' + response + ')');
                        $("#athlist").append('<tr><td><a href="../exp.ashx?op=download&id=' + json.id + '&filename=' + escape(json.name) + '">' + json.name + '</a></td><td><a href="#" onclick="javascript:removeFile(this);" id="' + json.id + '" filename="' + json.name + '">X</a></td></tr>');
                    }
                });
            });
        });
    } else {
        $("#simpleExcel").find(":input,textarea").each(function () {
            var $$ = $(this);
            var $p = $$.parent("td");
            var w = $p.width() - 5;
            var h = $p.height() - 5;
            $$.width(w).height(h).attr("pid", $p.attr("id")).css("backgroundColor", "#FFFFE0");
            if ($$.hasClass("number")) {
                $$.blur(function () {
                    if (!$.IsNumeric($$.val())) {
                        $$.addClass("error");
                        $$.focus();
                    } else {
                        $$.removeClass("error");
                    }
                });
            }
        });
    }
    $("#btnSave").click(function () {
        $("textarea").each(function () { $(this).html($(this).val()); });
        var q = { op: "savedata", id: "", tid: "", eqmId: "", result: 0, title: "", date: "", remark: "", html: "", data: "" };
        q.id = _id;
        q.tid = _tid;
        q.eqmId = _eqmId;
        q.result = $("#ddlResult option:selected").val();
        q.date = $("#txt_expdate").val();
        q.remark = $("#txt_remark").val();
        q.title = $("#txt_tmpName").val();
        q.data = $("#dtb :text").map(function () {
            var $$ = $(this);
            $$.attr("value", $$.val());
            return $$.attr("pid") + "<=>" + $$.val();
        }).get().join("<|>");
        q.html = $("#dtb").html();
        $.post(handlerUrl, q, function (data) {
            var json = eval('(' + data + ')');
            if (json.msg) {
                alert(json.msg);
            } else {
                if (_id == "") {
                    document.location.href = "success.aspx?id=" + json.id;
                } else {
                    alert("操作成功");
                    window.close();
                }
            }
        });
    });
});
function removeFile(o) {
    if (confirm("确定删除附件："+o.getAttribute("filename"))) {
        var query = { op: "del-attachment", id: _id, att: o.id };
        $.get(handlerUrl, query, function (res) {
            if (res == "") {
                $("#tr_" + query.att).remove();
            } else { alert(res); }
        });
    }
}
</script>