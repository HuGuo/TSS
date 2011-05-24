<%@ Page Language="C#" ValidateRequest="false" EnableViewState="false" AutoEventWireup="true" CodeFile="FillExperimentReport.aspx.cs" Inherits="Experiment_FillExperimentReport" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>实验报告数据</title>
    <link href="experiment.css" rel="stylesheet" type="text/css" />
    <script src="../scripts/jquery-1.6.1.min.js" type="text/javascript"></script>
</head>
<body>
    <form id="form1" runat="server">
    <div id="toolbar">实验报告名称<input type="text" id="txt_tmpName" runat="server" style="width:380px;" />
    <input type="button" id="btnSave" value="保存报告" />
    实验时间<input type="text" id="txt_expdate" value="" />
    </div>
    <div id="dtb"><asp:Literal ID="ltHTML" runat="server"></asp:Literal>
    </div>
    <input id="txt_hidden_id" type="hidden" value="<%=Request.QueryString["id"] %>" />
    <input id="txt_hidden_tid" type="hidden" value="<%=Request.QueryString["tid"] %>" />
    </form>
</body>
</html>
<script type="text/javascript">
    $(window).load(function () {
        var _id = $("#txt_hidden_id").val();
        if (_id != "") {
            //edit model

        } else {
            //replace {#} to input
            $("#simpleExcel td:contains('{#}')").each(function () {
                var $this = $(this);
                var w = $this.width() - 5;
                var h = $this.height() - 5;
                if (parseInt(this.rowSpan) > 1) {
                    $this.html('<textarea pid="' + $this.attr("id") + '" style="width:' + w + 'px;height:' + h + 'px;"></textarea>');
                } else {
                    $this.html('<input type="text" pid="' + $this.attr("id") + '" style="width:' + w + 'px;height:' + h + 'px; "/>');
                }
            });
        }
        $("#btnSave").click(function () {
            var q = { op: "savedata", id: "", tid: "", title: "", date: "", html: "", data: "" };
            q.id = _id;
            q.tid = $("#txt_hidden_tid").val();
            q.date = $("#txt_expdate").val();
            q.title = $("#txt_tmpName").val();
            q.data = $("#simpleExcel :text,#simpleExcel textarea").map(function () {
                var $this = $(this);
                if (this.tagName == "INPUT") {
                    $this.attr("value", $this.val());
                    return $this.attr("pid") + "<=>" + $this.val();
                } else {
                    return $this.attr("pid") + "<=>" + $this.text();
                }
            }).get().join("<|>");
            q.html = $("#dtb").html();
            $.post("exp.ashx", q, function (data) {
                alert(data);
            });
        });
    });
</script>