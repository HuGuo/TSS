<%@ Page Language="C#" AutoEventWireup="true" EnableViewState="false" CodeFile="ChartStep1.aspx.cs" Inherits="Experiment_ChartStep1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>试验数据曲线</title>
    <script src="../scripts/jquery-1.6.1.min.js" type="text/javascript"></script>
    <!--easyui-->
    <script src="../scripts/jquery-easyui/jquery.easyui.min.js" type="text/javascript"></script>
    <link href="../scripts/jquery-easyui/themes/gray/easyui.css" rel="stylesheet" type="text/css" />
    <link href="../scripts/jquery-easyui/themes/icon.css" rel="stylesheet" type="text/css" />
    <!--日历-->
    <script src="../scripts/My97DatePicker/WdatePicker.js" type="text/javascript"></script>
    <!--画图-->
    <script src="../FusionCharts/FusionCharts.js" type="text/javascript"></script>

    <link href="experiment.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
    ul{ list-style:none; margin:0; padding:0;}
    ul li{ display:block; margin:2px 10px; line-height:25px;}
    .tool{ padding:5px; background-color:#efefef;}
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div class="layout" style="width:100%;">
    <div region="east" split="true" title="设备(<b style='color:red'>可 选</b>)" style="width: 200px;
        overflow: auto;">
        <ul>
            <asp:Repeater ID="rptlist" runat="server">
                <ItemTemplate>
                <li>
                    <input type="checkbox" name="ckeq" value="<%#Eval("id") %>" /><%#Eval("Name") %></li>
                </ItemTemplate>
            </asp:Repeater>
        </ul>
    </div>
    <div region="center" style="margin: 0; padding: 0; overflow: auto;" title="<%=templateTitle %>">
        <div class="tool" style=" text-align:right;">
            <a class="easyui-linkbutton" id="step2">画图</a>
        </div>
        <div id="dtb">
            <asp:Literal ID="ltHtml" runat="server"></asp:Literal>
        </div>
    </div>
    </div>
    </form>
    <div id="dg_chart" title="实验数据折线图" modal="true"
        toolbar="#dg_chart_toolbar" style="width: 750px; height: 500px; left: 200px;
        top: 30px">
        <div id='scrollChartDiv' align='center'>
        </div>
    </div>
    <div id="dg_chart_toolbar">
    时间范围 <input type="text" id="txtStime" class="Wdata" onclick="WdatePicker()" />到
        <input type="text" id="txtEtime" class="Wdata" onclick="WdatePicker()" />
        <a id="btnshow" class="easyui-linkbutton" iconCls="icon-search">显示图表</a>
    </div>
</body>
</html>
<script type="text/javascript">
    var t = '<%=templateTitle %>';
    $(window).resize(function () {
        $("div.layout").layout("resize");
    });
    $(document).ready(function () {
        $("div.layout").height($(window).height()).layout();
        $("#dg_chart").dialog().dialog("close");
        //
        var enableCells = $("#simpleExcel td:contains('{d}')");
        enableCells.click(function () {
            enableCells.filter(".selectedCell").removeClass("selectedCell");
            $(this).addClass("selectedCell");
        });

        //
        $("#step2").click(function () {
            var cell = $("#simpleExcel td.selectedCell");
            if (cell.size() < 1) {
                $.messager.alert("系统提示", "未从试验报告模板中选择要进行分析的单元格");
                return false;
            }
            //clear chart
            var _coord = $("#btnshow").data("coord");
            if (_coord != null && _coord != cell[0].id) {
                $("#scrollChartDiv").empty();
            }
            $("#btnshow").data("coord", cell[0].id);
            $("#dg_chart").dialog("open");
        });
        $("#btnshow").click(function () {
            draw($(this).data("coord"));
        });
    });

    function draw(coord) {
        var stime = document.getElementById("txtStime").value;
        var etime = document.getElementById("txtEtime").value;
        var dataurl = "data.aspx?coord=" + coord + "&stime=" + stime + "&etime=" + etime + "&t=" + escape(t) + "&eqs=";

        var chart_scrollChart = new FusionCharts("../FusionCharts/ScrollLine2D.swf", "scrollChart", "720", "430", "0", "0");
        chart_scrollChart.setDataURL(escape(dataurl));
        chart_scrollChart.render("scrollChartDiv");
    }
</script>
