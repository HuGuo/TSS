<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Org_Default" %>

<%@ Register Assembly="wssmax" Namespace="wssmax" TagPrefix="cc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>监督网络</title>
    <link href="../scripts/Tip/tipTip.css" rel="stylesheet" type="text/css" />
    <link href="../scripts/jquery-easyui/themes/gray/easyui.css" rel="stylesheet" type="text/css" />
    <script src="../scripts/jquery-1.6.1.min.js" type="text/javascript"></script>
    <script src="../scripts/Tip/jquery.tipTip.js" type="text/javascript"></script>
    <style type="text/css">
        .OrgNode
        {
            font-size: 12px;
            font-family: Verdana, Arial;
            padding: 5px 5px 5px 5px;
            border: solid 1px orange;
            background-color: #ffffcc;
        }
        p{ text-align:left;padding:5px 3px; margin:0;line-height:16px;}
        label{ font-weight:700;}
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <cc1:OrgChart ID="OrgChart1" runat="server" ChartStyle="Vertical" ImageFolder="/" />
    </form>
</body>
</html>
<script type="text/javascript">
    $(function () {
        $("td.OrgNode").tipTip();
    });
</script>
