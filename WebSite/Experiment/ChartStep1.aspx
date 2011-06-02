<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ChartStep1.aspx.cs" Inherits="Experiment_ChartStep1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>实验数据曲线</title>
    <script src="../scripts/jquery-1.6.1.min.js" type="text/javascript"></script>
    <link href="experiment.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server"><input type="checkbox" id="selEQ" /> 选择设备
    <div id="eqlist">
        <asp:CheckBoxList ID="ckblist" runat="server" RepeatColumns="8" 
            RepeatDirection="Horizontal" RepeatLayout="Flow">
            <asp:ListItem>11</asp:ListItem>
            <asp:ListItem>22</asp:ListItem>
            <asp:ListItem>33</asp:ListItem>
            <asp:ListItem>44</asp:ListItem>
        </asp:CheckBoxList>
    </div>
    step1：从模板中选择单元格：
    <div id="dtb">
    <p id="tmpTitle"><asp:Literal ID="ltTitle" runat="server"></asp:Literal></p>
    <asp:Literal ID="ltHtml" runat="server"></asp:Literal>
    </div>
    step2：<input id="step2" type="button" value="下一步" />
    </form>
</body>
</html>
<script type="text/javascript">
    $(document).ready(function () {
        disableCheckbox(true);
        $("#selEQ").click(function () {
            disableCheckbox(!this.checked);
        });

        //
        var enableCells = $("#simpleExcel td:contains('{d}')");
        enableCells.click(function () {
            enableCells.find("td.selectedCell").removeClass("selectedCell");
            $(this).addClass("selectedCell");
        });

        //
        $("#step2").click(function () {
            var sel = $("#simpleExcel td.selectedCell");
            if (sel.size() > 0) {
                var query = { coord: sel.attr("id"), eqs: "", t: $("#tmpTitle").text() };
                if ($("#selEQ").attr("checked")) {
                    query.eqs = $("#eqlist :checkbox:checked").map(function () {
                        return this.value;
                    }).get().join(",");
                }
                document.location.href = "chartstep2.aspx?" + $.param(query);
            } else {
                alert("坐标未选择");
            }
        });
    });
    function disableCheckbox(o) {
        if (o) {
            $("#eqlist :checkbox").attr("disabled", "disabled");
        } else {
            $("#eqlist :checkbox").removeAttr("disabled");
        }
    }
</script>
