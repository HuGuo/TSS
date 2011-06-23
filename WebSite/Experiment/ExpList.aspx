<%@ Page Language="C#" AutoEventWireup="true" EnableViewState="false" CodeFile="ExpList.aspx.cs" Inherits="Experiment_ExpList" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>实验报告</title>
    <link href="../scripts/jquery-easyui/themes/gray/easyui.css" rel="stylesheet" type="text/css" />
    <link href="~/Styles/_base.css" rel="stylesheet" type="text/css" />
    <script src="../scripts/jquery-1.6.1.min.js" type="text/javascript"></script>
    <script src="../scripts/jquery-easyui/jquery.easyui.min.js" type="text/javascript"></script>
    <style type="text/css">
    ul{ list-style:none; margin:0; padding:0}
    </style>
</head>
<body>
    <form id="form1" runat="server" style="padding-top:32px;">
    <div id="toolbar" class="fixed">
    <a href="RecordDefault.aspx?s=<%=Request.QueryString["s"] %>">实验台帐</a>
    <a class="dg" href="#dg_win">填写实验报告</a>
    <a class="dg" href="#dg_win2">数据分析</a>
    </div>
    <table>
        <thead>
            <tr>
                <th>
                    序号
                </th>
                <th>
                    实验时间
                </th>
                <th>
                    实验结果
                </th>
                <th>
                    实验报告
                </th>
                <th>
                </th>
            </tr>
        </thead>
        <asp:Repeater ID="rptList" runat="server">
            <ItemTemplate>
                <tr id="tr_<%#Eval("Id") %>">
                    <td>
                        <%#Container.ItemIndex+1 %>
                    </td>
                    <td>
                        <%#Eval("ExpDate", "{0:yyyy-MM-dd}")%>
                    </td>
                    <td>
                        <%#Eval("Result").ToString()=="1"?"合格":"不合格" %>
                    </td>
                    <td>
                        <a href="experiment.aspx?id=<%#Eval("Id") %>" target="_blank"><%#Eval("Title") %></a>
                    </td>
                    <td>
                        <a href="FillExperimentReport.aspx?id=<%#Eval("Id") %>&tid=<%#Eval("ExpTemplateID") %>&eqmId=<%#Eval("EquipmentID") %>" target="_blank">
                            编辑</a> <a href="javascript:void(0);" class="delete" key="<%#Eval("Id") %>">删除</a>
                    </td>
                </tr>
            </ItemTemplate>
        </asp:Repeater>
    </table>
    <div id="dg_win" class="easyui-dialog" title="选择实验报告模板" style="width: 500px; height: 300px;
        top:100px; margin-left:auto; margin-right:auto; padding:0;" buttons="#dlg_buttons">
        <div class="easyui-layout" style="width:100%; height:100%;">
        <div region="west" border="true" split="false" style="width:150px; overflow:auto;">
        <ul id="eqmlist">
        <asp:Repeater ID="rptEqipmentList" runat="server">
                <ItemTemplate>
                <li>
                <input type="radio" name="eqm" value='<%#Eval("Id") %>'/><%#Eval("Name") %></li>
                </li>
                </ItemTemplate>
                </asp:Repeater>
        </ul>
        </div>
        <div region="center" border="false" style="overflow:auto;">
        <ul id="tmplist">
            <asp:Repeater ID="rptTmpList" runat="server">
                <ItemTemplate>
                    <li>
                        <input type="radio" name="tmp" value='<%#Eval("Id") %>'/><%#Eval("Title") %></li>
                </ItemTemplate>
            </asp:Repeater>
        </ul></div>
        </div>
    </div>
    <div id="dlg_buttons">
		<a href="#" class="easyui-linkbutton" onclick="javascript:goFillIn()">填写实验报告</a>
		<a href="#" class="easyui-linkbutton" onclick="javascript:$('#dg_win').dialog('close')">关闭</a>
	</div>
    <div id="dg_win2" class="easyui-dialog" title="选择实验报告模板" style="width: 350px; height: 300px;
        top: 100px; margin-left: auto; margin-right: auto; padding: 0; overflow:auto;" buttons="#dlg_buttons2">
        <ul id="tmplist2">
            <asp:Repeater ID="rptlist2" runat="server">
                <ItemTemplate>
                    <li>
                        <input type="radio" name="tmp" value='<%#Eval("Id") %>' /><%#Eval("Title") %></li>
                </ItemTemplate>
            </asp:Repeater>
        </ul>
        </div>
    <div id="dlg_buttons2">
        <a href="#" class="easyui-linkbutton" onclick="javascript:goChart()">确定</a>
        <a href="#" class="easyui-linkbutton" onclick="javascript:$('#dg_win2').dialog('close')">
            关闭</a>
    </div>
    </form>
</body>
</html>
<script src="../scripts/jquery.delete.js" type="text/javascript"></script>
<script type="text/javascript">
    $(document).ready(function () {
        $('#dg_win,#dg_win2').dialog('close');
        $("a.dg").click(function () { $($(this).attr("href")).dialog('open'); });
        $("a.delete").bindDelete({
            handler: "../exp.ashx",
            op: "del-d",
            onSuccess: function (k) {
                $("#tr_" + k).remove();
            }
        });
    });
    var sp = '<%=Request.QueryString["s"] %>';

    function goFillIn() {
        var tmpId = $("#tmplist :radio:checked").val();
        var emp = $("#eqmlist :radio:checked").val();
        if (tmpId == null) {
            alert("实验模报告板未选择"); return false;
        }
        if (emp == null) {
            alert("设备未选择"); return false;
        }
        window.open("FillExperimentReport.aspx?s=" + sp + "&tid=" + tmpId+"&eqmId="+emp);
        $('#dg_win').dialog('close');
    }

    function goChart() {
        var tid = $("#tmplist2 :radio:checked").val();
        if (tid==null) {
            alert("选择实验报告模板");return false;
        }
        window.open("chartstep1.aspx?tid=" + tid);
    }
</script>