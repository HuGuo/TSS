<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ChartStep2.aspx.cs" Inherits="Experiment_ChartStep2" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script src="../scripts/My97DatePicker/WdatePicker.js" type="text/javascript"></script>
    <script src="../FusionCharts/FusionCharts.js" type="text/javascript"></script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    选择时间段：<asp:TextBox ID="txtStime" CssClass="Wdate" runat="server" onclick="WdatePicker()" ></asp:TextBox>到<asp:TextBox ID="txtEtime" runat="server" CssClass="Wdate" onclick="WdatePicker()"  ></asp:TextBox>
    <input type="button" value="显示图表" onclick="javascript:draw()" />
    </div>
    <div id='scrollChartDiv' align='center'></div>
    </form>
</body>
</html>
<!-- START Script Block for Chart scrollChart -->
<script type="text/javascript">
    
    var coord = '<%= Request.QueryString["coord"]%>';
    var eqs = '<%= Request.QueryString["eqs"]%>';
    var title = '<%= Request.QueryString["t"]%>';
    function draw() {
        var stime = document.getElementById("txtStime").value;
        var etime = document.getElementById("txtEtime").value;
        var dataurl = "data.aspx?coord=" + coord + "&stime=" + stime + "&etime=" + etime + "&t=" + escape(title) + "&eqs=" + eqs;

        var chart_scrollChart = new FusionCharts("../FusionCharts/ScrollLine2D.swf", "scrollChart", "700", "450", "0", "0");
        chart_scrollChart.setDataURL(escape(dataurl));
        chart_scrollChart.render("scrollChartDiv");
    }
</script>
<!-- END Script Block for Chart scrollChart -->