<%@ Page Language="C#" ValidateRequest="false" EnableViewState="false" AutoEventWireup="true" CodeFile="SetTemplate.aspx.cs" Inherits="Experiment_SetTemplate" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>设置模版</title>
        
    <link href="experiment.css" rel="stylesheet" type="text/css" />

    <link href="../scripts/jquery-easyui/thems/gray/menu.css" rel="stylesheet" type="text/css" />
    <script src="../scripts/jquery-1.6.1.min.js" type="text/javascript"></script>
    <script src="../scripts/jquery-easyui/plugins/jquery.menu.js" type="text/javascript"></script>
    <script src="../scripts/jquery.excel.js" type="text/javascript"></script>
</head>
<body>
    <form id="form1" runat="server">
    <div id="toolbar">
        行数<input type="text" id="txt_row" value="10" style="width: 50px;" />列数<input type="text"
            id="txt_column" style="width: 50px;" value="10" />
        <input type="button" id="btnDrawTable" value="绘制表格" />
        模板名称<input type="text" id="txt_tmpName" runat="server" />
        <input type="button" id="btnSave" value="保存模板" />
    </div>
    <div id="dtb" style=" margin-top:31px;"><asp:Literal ID="ltHTML" runat="server"></asp:Literal></div>
        <input id="txt_hidden_cid" type="hidden" value="<%=Request.QueryString["cid"] %>" />
        <input id="txt_hidden_sp" type="hidden" value="<%=Request.QueryString["sp"] %>" />
    
    </form>
    <div id="ct_menu" style="width: 120px;">
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
    $(function () {
        var excel = $("#dtb").simpleExcel({ rows: 10, columns: 10, menu: $("#ct_menu") });        
    });
</script>
