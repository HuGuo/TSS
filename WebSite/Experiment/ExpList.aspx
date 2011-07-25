<%@ Page Language="C#" AutoEventWireup="true" EnableViewState="false" CodeFile="ExpList.aspx.cs"
    Inherits="Experiment_ExpList" %>

<%@ Register src="../UserControl/Pager.ascx" tagname="Pager" tagprefix="uc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>试验报告</title>
    <link href="~/Styles/_base.css" rel="stylesheet" type="text/css" />
    <link href="../Styles/datasheet.css" rel="stylesheet" type="text/css" />
    <script src="../scripts/jquery-1.6.1.min.js" type="text/javascript"></script>
    <!--easyui-->
    <link href="../scripts/jquery-easyui/themes/gray/easyui.css" rel="stylesheet" type="text/css" />
    <script src="../scripts/jquery-easyui/jquery.easyui.min.js" type="text/javascript"></script>
    <style type="text/css">
    </style>
</head>
<body>
    <form id="form1" runat="server" style="padding-top: 32px;">
    <div id="toolbar" class="fixed">
        <%--<a href="RecordDefault.aspx?s=<%=Request.QueryString[Helper.queryParam_specialty] %>">试验台帐</a>--%>
        <a runat="server" id="goback" href="#">返回</a>
        <a runat="server" id="linkAdd" target="_blank">填写试验报告</a>
        <a href="ChartStep1.aspx?tid=<%=Request.QueryString["id"] %>" target="_blank">数据分析</a>
        
    </div>
    <div class="wrap">
        <div class="wrap_inner">
    <table id="tblist" class="datasheet" cellpadding="0" cellspacing="0">
        <thead>
            <tr>
                <th style="width:20px;"></th>
                <th style="width:80px;">
                    试验时间
                </th>
                <th style="width:50px;">
                    试验结果
                </th>
                <th>
                    试验报告
                </th>
                <th style="width: 80px;">
                </th>
            </tr>
        </thead>
        <tbody>
        <asp:Repeater ID="rptList" runat="server">
            <ItemTemplate>
                <tr id="tr_<%#Eval("Id") %>">
                    <th>
                        <%# (PageIndex-1)*PageSize+Container.ItemIndex+1 %>
                    </th>
                    <td>
                        <%#Eval("ExpDate", "{0:yyyy-MM-dd}")%>
                    </td>
                    <td>
                        <%#Eval("Result").ToString()=="1"?"合格":"不合格" %>
                    </td>
                    <td>
                        <a href="experiment.aspx?id=<%#Eval("Id") %>" target="_blank">
                            <%#Eval("Title") %></a>
                    </td>
                    <td class="center_cell">
                        <asp:HyperLink ID="linkEdit" href='<%# string.Format("FillExperimentReport.aspx?id={0}&tid={1}&eqmId={2}" , Eval("Id") , Eval("ExpTemplateID") , Eval("EquipmentID"))%>'
                            runat="server" Text="编辑" Target="_blank" />
                        <asp:HyperLink ID="linkDel" NavigateUrl="javascript:void(0)" runat="server" class="delete"
                            key='<%#Eval("id") %>' Text="删除" />
                    </td>
                </tr>
            </ItemTemplate>
        </asp:Repeater>
        </tbody>
    </table>
    <uc1:Pager ID="Pagination" runat="server" />
        </div>
    </div>
    <!--dialog-->
    <%--<div id="dg_win1" class="easyui-dialog" title="可选设备" modal="true" style="width:400px; height:200px;">
        <table border="0" cellpadding="0" cellspacing="5" style=" width:365px;">
            <asp:Repeater runat="server" ID="rptEqList">
                <ItemTemplate>
                    <tr>
                        <td style="padding-left: 10px; border:1px dotted #efefef;">
                            <a href="FillExperimentReport.aspx?s=<%=Request.QueryString["s"] %>&tid=<%=Request.QueryString["id"] %>&eqmId=<%#Eval("Id") %>"
                                target="_blank">
                                <%#Eval("name") %></a>
                        </td>
                    </tr>
                </ItemTemplate>
            </asp:Repeater>
        </table>
    </div>--%>
    </form>
</body>
</html>
<script src="../scripts/jquery.tableStyle.js" type="text/javascript"></script>
<script type="text/javascript">
    var handlerUrl = "../exp.ashx";
    $(document).ready(function () {
        //$("#dg_win1").dialog("close");
        //bind delete
        $("a.delete").bindDelete({
            handler: handlerUrl,
            op: "del-d",
            onSuccess: function (k) {
                $("#tr_" + k).remove();
            }
        });

        $("#tblist tr:gt(0)").alternateColor();

        //linkAdd
//        $("#linkAdd").click(function () {
//            $("#dg_win1").dialog("open");
//        });
    });
    var sp = '<%=Request.QueryString[Helper.queryParam_specialty] %>';

    function goFillIn() {
        var tmpId = $("#tmplist :radio:checked").val();
        var emp = $("#eqmlist :radio:checked").val();
        if (tmpId == null) {
            alert("试验模报告板未选择"); return false;
        }
        if (emp == null) {
            alert("设备未选择"); return false;
        }
        window.open("FillExperimentReport.aspx?s=" + sp + "&tid=" + tmpId + "&eqmId=" + emp);
        $('#dg_win').dialog('close');
    }

    function goChart() {
        var tid = $("#tmplist2 :radio:checked").val();
        if (tid == null) {
            alert("选择试验报告模板"); return false;
        }
        window.open("chartstep1.aspx?tid=" + tid);
    }
</script>
