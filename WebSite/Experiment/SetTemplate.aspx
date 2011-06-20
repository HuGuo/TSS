<%@ Page Language="C#" ValidateRequest="false" EnableViewState="false" AutoEventWireup="true" CodeFile="SetTemplate.aspx.cs" Inherits="Experiment_SetTemplate" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>����ģ��</title>
        
    <link href="experiment.css" rel="stylesheet" type="text/css" />
    <link href="../scripts/jquery-easyui/themes/gray/menu.css" rel="stylesheet" type="text/css" />
    <script src="../scripts/jquery-1.6.1.min.js" type="text/javascript"></script>
    <script src="../scripts/jquery-easyui/plugins/jquery.menu.js" type="text/javascript"></script>
    <script src="../scripts/jquery.excel.js" type="text/javascript"></script>
</head>
<body>
    <form id="form1" runat="server">
    <div id="toolbar">
        ����<input type="text" id="txt_row" value="10" style="width: 50px;" />����<input type="text"
            id="txt_column" style="width: 50px;" value="10" />
        <input type="button" id="btnDrawTable" value="���Ʊ��" />
        ģ������<input type="text" id="txt_tmpName" runat="server" />
        <input type="button" id="btnSave" value="����ģ��" />
    </div>
    <div id="dtb" style=" margin-top:31px;"><asp:Literal ID="ltHTML" runat="server"></asp:Literal></div>
        <input id="txt_hidden_cid" type="hidden" value="<%=Request.QueryString["cid"] %>" />
        <input id="txt_hidden_sp" type="hidden" value="<%=Request.QueryString["sp"] %>" />
    
    </form>
    <div id="ct_menu" style="width: 120px;">
        <div onclick="javascript:$.simpleExcel.clearCell()">
            ���</div>
        <div class="menu-sep">
        </div>
        <div onclick="javascript:$.simpleExcel.mergeCell()">
            �ϲ���Ԫ��</div>
        <div onclick="javascript:$.simpleExcel.splitCell()">
            ��ֵ�Ԫ��</div>
        <div class="menu-sep">
        </div>
        <div>
            <span>���ø�ʽ</span>
            <div style="width: 100px;">
                <div onclick="javascript:$.simpleExcel.setStyle({textAlign:'left'});">
                    �����</div>
                <div onclick="javascript:$.simpleExcel.setStyle({textAlign:'center'});">
                    �� ��</div>
                <div onclick="javascript:$.simpleExcel.setStyle({textAlign:'right'});">
                    �Ҷ���</div>
                <div onclick="javascript:$.simpleExcel.setStyle({fontWeight:'700'});">
                    �� ��</div>
            </div>
        </div>
        <div>
            <span>���/ɾ��</span>
            <div style="width: 100px;">
                <div onclick="javascript:a1();">
                    ���� ��һ��</div>
                <div onclick="javascript:$.simpleExcel.appendColumn();">
                    ���� �ұ���</div>
                <div onclick="javascript:$.simpleExcel.removeRow();">
                    ɾ�� ��</div>
                <div onclick="javascript:$.simpleExcel.removeColumn();">
                    ɾ�� ��</div>
            </div>
        </div>
    </div>
</body>
</html>
<script type="text/javascript">
    $(function () {
        var excel = $("#dtb").simpleExcel({ rows: 10, columns: 10, menu: $("#ct_menu") });
        function a1() {
            alert("aa");
            excel.addRow();
        }
        //excel.removeRow();
        excel.removeColumn();
        //excel.addColumn();
    });
</script>
