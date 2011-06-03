<%@ Page Language="C#" AutoEventWireup="true" EnableViewState="false" CodeFile="experiment.aspx.cs" Inherits="Experiment_experiment" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>实验报告</title>
    <link href="experiment.css" rel="stylesheet" type="text/css" />
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
                实验结果:<asp:Literal ID="ltResult" runat="server"></asp:Literal>
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
    });
</script>
