<%@ Page Language="C#" EnableViewState="false" ValidateRequest="false" AutoEventWireup="true"
    CodeFile="setTemplate.aspx.cs" Inherits="SystemManagement_Experiment_setTemplate" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>设置模板</title>
    <link href="../../experiment/experiment.css" rel="stylesheet" type="text/css" />
    <!--jQuery-->
    <script src="../../scripts/jquery-1.6.1.min.js" type="text/javascript"></script>
    <!--easyui 1.2.3-->
    <script src="../../scripts/jquery-easyui/jquery.easyui.min.js" type="text/javascript"></script>
    <link href="../../scripts/jquery-easyui/themes/gray/menu.css" rel="stylesheet" type="text/css" />
    <link href="../../scripts/jquery-easyui/themes/icon.css" rel="stylesheet" type="text/css" />
    <!--easyui-menu 1.2.4-->
    <script src="../../scripts/jquery-easyui/plugins/jquery.menu.js" type="text/javascript"></script>

    <script src="../../scripts/jquery.simpleExcel.js" type="text/javascript"></script>    
</head>
<body>
    <form id="form1" runat="server">
    <div id="toolbar">
        行数<input type="text" id="txt_row" value="10" style="width: 50px;" />列数<input type="text"
            id="txt_column" style="width: 50px;" value="10" />
        <input type="button" id="btnDrawTable" value="绘制表格" />
        模板名称<input type="text" id="txt_tmpName" runat="server" style="width: 250px;" />
        <asp:DropDownList ID="ddlSpecialty" runat="server">
        </asp:DropDownList>
        <input type="button" id="btnSave" value="保存模板" />
    </div>
    <!--excel content-->
    <div id="dtb" style="margin-top: 33px;">
        <asp:Literal ID="ltHTML" runat="server"></asp:Literal></div>
    <input id="txt_hidden_cid" type="hidden" value="<%=Request.QueryString["tid"] %>" />
    <input id="txt_hidden_sp" type="hidden" value="<%=Request.QueryString["s"] %>" />
    </form>
    <!--context menu-->
    <div id="ct_menu" class="easyui-menu" style="width: 120px;">
        <div onclick="javascript:$.simpleExcel.clearCell()">
            清空</div>
        <div class="menu-sep">
        </div>
        <div onclick="javascript:$.simpleExcel.mergeCell()">
            合并单元格</div>
        <div onclick="javascript:$.simpleExcel.splitCell()">
            拆分单元格</div>
        <div class="menu-sep">
        </div>
        <div>
            <span>设置格式</span>
            <div style="width: 100px;">
                <div onclick="javascript:$.simpleExcel.setStyle({textAlign:'left'});">
                    左对齐</div>
                <div onclick="javascript:$.simpleExcel.setStyle({textAlign:'center'});">
                    居 中</div>
                <div onclick="javascript:$.simpleExcel.setStyle({textAlign:'right'});">
                    右对齐</div>
                <div onclick="javascript:$.simpleExcel.setStyle({fontWeight:'700'});">
                    加 粗</div>
            </div>
        </div>
        <div>
            <span>添加/删除</span>
            <div style="width: 100px;">
                <div onclick="javascript:$.simpleExcel.appendRow();">
                    插入 下一行</div>
                <div onclick="javascript:$.simpleExcel.appendColumn();">
                    插入 右边列</div>
                <div onclick="javascript:$.simpleExcel.removeRow();">
                    删除 行</div>
                <div onclick="javascript:$.simpleExcel.removeColumn();">
                    删除 列</div>
            </div>
        </div>
    </div>
</body>
</html>
<script type="text/javascript">
    $(document).ready(function () {
        var handlerUrl = "../../Exptemplate.ashx";
        $("#ct_menu").menu();
        $.simpleExcel._op.container = $("#dtb");
        //edit model
        $.simpleExcel._op.excell = $("#simpleExcel");
        if ($.simpleExcel._op.excell.size() > 0) {
            $.simpleExcel._init($.simpleExcel._op.excell.find("td"));
            $.simpleExcel._initHead();
            $.simpleExcel._op.rows = $.simpleExcel._op.excell.find("tr").size();
            $.simpleExcel._op.columns = $.simpleExcel._op.excell.find("tr:first th").size();
        }
        $("#btnDrawTable").click(function () {
            var r = $("#txt_row").val();
            var c = $("#txt_column").val();
            if ($.simpleExcel._op.excell.size() > 0) {
                if (!confirm("重新创建，将会覆盖已经存在的格式，确定")) {
                    return false;
                }
            }
            $.simpleExcel.create(r, c);
        });

        $("#btnSave").click(function () {
            var q = { op: "save", tid: 0, sp: "", title: "", html: "" };
            q.title = $.trim($("#txt_tmpName").val());
            if (q.title == "") {
                alert("模板名称未设置");
                return false;
            }
            q.tid = $("#txt_hidden_cid").val();
            q.sp = $("#ddlSpecialty option:selected").val();
            $.simpleExcel._op.excell.find("td." + $.simpleExcel._op.selectedCellClass).removeClass($.simpleExcel._op.selectedCellClass);
            q.html = $("#dtb").html();
            $.post(handlerUrl, q, function (data) {
                var json = eval('(' + data + ')');
                if (json.msg) {
                    alert(json.msg);
                } else {
                    document.location.href = "default.aspx";
                    //                    document.location.href = "success.aspx?id=" + json.id;
                    //                    alert("操作成功");
                    //                    window.close();
                }
            });
        });
    });
</script>
