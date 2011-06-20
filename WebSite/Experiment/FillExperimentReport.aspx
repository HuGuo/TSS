﻿<%@ Page Language="C#" ValidateRequest="false" EnableViewState="false" AutoEventWireup="true" CodeFile="FillExperimentReport.aspx.cs" Inherits="Experiment_FillExperimentReport" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>实验报告数据</title>
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
    <div style="position: fixed; top: 31px; right: 0; width: 250px; height: 500px">
        <div id="commField" title="设备信息" fit="true" collapsed="true" collapsible="true" style="overflow: auto;">
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
    </div>
    </form>
</body>
</html>
<script type="text/javascript">
    $.extend({ IsNumeric: function (input) {
        return (input - 0) == input || ($.trim(input).length == 0);}
    });

    $(window).load(function () {
        var _id = '<%=Request.QueryString["id"] %>';
        var _tid = '<%=Request.QueryString["tid"] %>';
        var _eqmId = '<%=Request.QueryString["eqmId"] %>';
        easyloader.load("panel", function () {
            $("#commField").panel();
        })
        if (_id != "") {
            //edit model

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
                return $$.attr("pid") + "<=>" + $$.val();
            }).get().join("<|>");
            q.html = $("#dtb").html();
            $.post("../exp.ashx", q, function (data) {
                alert(data);
            });
        });
    });
</script>