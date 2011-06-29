<%@ Page Language="C#" AutoEventWireup="true" EnableViewState="false" CodeFile="Record.aspx.cs" Inherits="Experiment_Record" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>试验台帐</title>
    <script src="../scripts/jquery-1.6.1.min.js" type="text/javascript"></script>
    <style type="text/css">
    body
        {
            font-size: 12px;
            margin: 0;
            padding: 0;
        }
        .nveltb
        {
            border: 1px solid #e2e2e2;
            border-collapse: collapse;
        }
        .caption
        {
            font-size: 18px;
            line-height: 30px;
            font-weight: 800;
        }
        table,tr,th,td
        {
            border: 1px solid #000;
            border-collapse:collapse;
            text-align: center;
            height:26px;
        }
        .noborder{ border:0;}
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <asp:Literal ID="ltHTML" runat="server"></asp:Literal>
    <input type="hidden" runat="server" id="templateId" />
    <input type="hidden" runat="server" id="equipmentId" />
    <input type="hidden" runat="server" id="customerThead" />
    </form>
</body>
</html>
<script type="text/javascript">
    /**
    * 肖宏飞
    * 2011-06-27
    */
    $(function () {
        $(document).ajaxError(function (event, request, settings) {
            alert("请求地址发生错误：" + settings.url);
        });

        var trow = $("tr.firstrow");
        var handlerUrl = "../exp.ashx";
        var query = { op: "recordjson", equipment: "", exptemplate: "", fields: "" };
        query.equipment = $("#equipmentId").val();
        query.exptemplate = $("#templateId").val();
        query.fields = trow.find("td[ds]").map(function () {
            return this.getAttribute("ds");
        }).get().join(",");

        $.post(handlerUrl, query, function (res) {
            var json = eval('(' + res + ')');
            if (json.msg) {
                trow.css("visibility", "hidden");
                alert("读取数据错误：" + json.msg);
                return false;
            }
            var rs = json.rows.length;
            if (rs == 0) {
                trow.css("visibility", "hidden");
                alert("无相关试验数据");
                return false;
            }
            //循环绑定数据
            for (var i = 0; i < rs; i++) {
                var tr = trow.clone(false);
                tr.attr("id", json.rows[i].id)
                .addClass("result_" + json.rows[i].result).removeClass("firstrow")
                .find("td").removeClass("droppable").empty()
                .css("backgroundColor", "#FFF");
                var dts = json.rows[i].dts;
                for (var j = 0; j < dts.length; j++) {
                    tr.find("td[ds='" + dts[j].label + "']").text(dts[j].val);
                }
                tr.find("td:first").wrapInner('<a target="_blank" href="experiment.aspx?id=' + json.rows[i].id + '"></a>');
                tr.appendTo("#tbExpRecord");
            }
            trow.remove();

            //表头 设备信息
            var cthead = $("#customerThead").val();
            if (cthead != "") {
                $(cthead).appendTo("#customerTheadContainer");
            }
            var eqinfoTds = $("#tbExpRecord td[lab]");
            if (eqinfoTds.size() > 0) {
                var labs = json.nameplates;
                for (var k = 0; k < labs.length; k++) {
                    eqinfoTds.filter("[lab='" + labs[k].label + "']").text(labs[k].val);
                }
            }

        });
    });
</script>