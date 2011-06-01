<%@ Page Language="C#" ValidateRequest="false" EnableViewState="false" AutoEventWireup="true" CodeFile="FillExperimentReport.aspx.cs" Inherits="Experiment_FillExperimentReport" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>实验报告数据</title>
    <link href="experiment.css" rel="stylesheet" type="text/css" />
    <script src="../scripts/jquery-1.6.1.min.js" type="text/javascript"></script>
    <script src="../scripts/My97DatePicker/WdatePicker.js" type="text/javascript"></script>
</head>
<body>
    <form id="form1" runat="server">
    <div id="toolbar">实验报告名称<input type="text" id="txt_tmpName" runat="server" style="width:380px;" />
    <input type="button" id="btnSave" value="保存报告" />
    实验时间
        <asp:TextBox ID="txt_expdate" runat="server" class="Wdate" onclick="WdatePicker()" ></asp:TextBox>
    实验结果
        <asp:DropDownList ID="ddlResult" runat="server">
            <asp:ListItem Selected="True" Value="1">合格</asp:ListItem>
            <asp:ListItem Value="0">不合格</asp:ListItem>
        </asp:DropDownList>
    </div>
    <div id="dtb"  style=" margin-top:31px;"><asp:Literal ID="ltHTML" runat="server"></asp:Literal>
    </div>
    </form>
</body>
</html>
<script type="text/javascript">
    $(window).load(function () {
        var _id = '<%=Request.QueryString["id"] %>';
        var _tid = '<%=Request.QueryString["tid"] %>';
        var _eqmId = '<%=Request.QueryString["eqmId"] %>';

        if (_id != "") {
            //edit model

        } else {
            $("#simpleExcel").find(":input,textarea").each(function () {
                var $$ = $(this);
                var $p = $$.parent("td");
                var w = $p.width() - 5;
                var h = $p.height() - 5;
                $$.width(w).height(h).attr("pid", $p.attr("id"));
            });
        }
        $("#btnSave").click(function () {
            var q = { op: "savedata", id: "", tid: "", eqmId: "", result: 0, title: "", date: "", html: "", data: "" };
            q.id = _id;
            q.tid = _tid;
            q.eqmId = _eqmId;
            q.result = $("#ddlResult option:selected").val();
            q.date = $("#txt_expdate").val();
            q.title = $("#txt_tmpName").val();
            q.data = $("#dtb :text").map(function () {
                var $$ = $(this);
                $$.attr("value", $$.val());
                if ($$.hasClass("number")) {
                    return $$.attr("pid") + "<=>" + $$.val();
                }
            }).get().join("<|>");
            q.html = $("#dtb").html();
            $.post("../exp.ashx", q, function (data) {
                alert(data);
            });
        });
    });
</script>