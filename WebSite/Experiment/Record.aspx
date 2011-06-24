<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Record.aspx.cs" Inherits="Experiment_Record" %>

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
        td
        {
            border: 1px solid #e2e2e2;
            text-align: center;
            height:26px;
        }
        .noborder{ border:0;}
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <asp:Literal ID="ltHTML" runat="server"></asp:Literal>
    </form>
</body>
</html>
<script type="text/javascript">
    $(function () {
        $(document).ajaxError(function (event, request, settings) {
            alert("请求地址发生错误：" + settings.url);
        });
        var handlerUrl = "../exp.ashx";
        var query = { op: "recordjson", equipment: "", exptemplate: "", fields: "" };
        query.equipment = '<%=Request.QueryString["equipment"] %>';
        query.exptemplate = '<%=Request.QueryString["tid"] %>';
        query.fields = $("#form1 table:first").find("td[ds]").map(function () {
            return this.getAttribute("ds");
        }).get().join(",");

        $.post(handlerUrl, query, function (res) { 
                
        });
    });
</script>