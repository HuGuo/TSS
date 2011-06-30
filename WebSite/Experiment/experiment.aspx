<%@ Page Language="C#" AutoEventWireup="true" EnableViewState="false" CodeFile="experiment.aspx.cs"
    Inherits="Experiment_experiment" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>试验报告</title>
    <link href="experiment.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        ul{list-style:none;margin:0;padding:0;}
        ul li{margin-left:20px;}
        .expresult{font-weight:700;margin:0 5px;padding:3px 15px;}
        .res0{background-color:#d11;}
        .res1{background-color:#cf7;}
        .remark{position:fixed;right:0;bottom:0;width:300px;height:150px;border:5px solid #efefef;background-color:#FFF;opacity:0.8;}
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <table style="margin: 5px auto; border: 0;" border="0" cellpadding="0" cellspacing="0">
        <tr>
            <th style="background-color: #e6e6e6; height: 35px; line-height: 35px; font-size: 18px;">
                <asp:Literal ID="ltTitle" runat="server"></asp:Literal>
            </th>
        </tr>
        <tr>
            <td align="right" style="background-color: #e6e6e6;">
                试验结果:<span class="expresult <%=res %>"><asp:Literal ID="ltResult" runat="server"></asp:Literal></span>
            </td>
        </tr>
        <tr>
            <td valign="top">
                <div id="dtb">
                    <asp:Literal ID="ltHTML" runat="server"></asp:Literal>
                </div>
            </td>
        </tr>
    </table>
    <p style="height: 150px;">
    </p>
    <div class="remark">
        <div style="height: 25px; font-weight: 700; padding-left: 20px; width: 280px; background-color: #e2e2e2;">
            试验相关附件及备注信息</div>
        <ul id="attachmentlist">
            <asp:Literal ID="ltAttachment" runat="server" /></ul>
        <div id="remark">
            <asp:Literal ID="ltRemark" runat="server" /></div>
    </div>
    </form>
</body>
</html>
<script src="../scripts/jquery-1.6.1.min.js" type="text/javascript"></script>
<script type="text/javascript">
    $(function () {
        $("#dtb tr:eq(1) td").each(function () {
            var $$ = $(this);
            $$.width($$.width());
        });
        $("#dtb th").remove();
        $("#dtb :input,#dtb textarea").each(function () {
            var $$ = $(this);
            if (this.tagName = "INPUT") {
                $$.parent("td").html($$.val());
            } else {
                $$.parent("td").html($$.text());
            }
        });
        if ($.trim(document.getElementById("remark").innerText) == "" && document.getElementById("attachmentlist").childNodes.length <2) {
            $("div.remark").hide();
        }
    });
</script>
