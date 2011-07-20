<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CategoryList.aspx.cs" Inherits="Experiment_CategoryList" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>试验报告模板列表</title>
    <link href="../Styles/_base.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/jquery-1.6.1.min.js" type="text/javascript"></script>
</head>
<body>
    <form id="form1" runat="server">
    <div id="toolbar">
    <a runat="server" id="linkAdd" target="_blank">新建试验报告模板</a>
    </div>
    <table id="tblist" cellpadding="0" cellspacing="0" style="width: 100%;">
        <asp:Repeater runat="server" ID="rptlist">
            <ItemTemplate>
                <tr>
                    <td align="center" style="width: 30px;">
                        <%#Container.ItemIndex+1 %>
                    </td>
                    <td>
                        <a href="explist.aspx?id=<%#Eval("Id") %>&s=<%#Eval("specialtyId") %>">
                            <%#Eval("Title") %></a>
                            <span style=" color:#e74;">(<%#((TSS.Models.ExpTemplate)(Container.DataItem)).Experiments.Count %>)</span>
                    </td>
                    <td align="center">
                        <a href="ChartStep1.aspx?tid=<%#Eval("Id") %>" target="_blank" class="button">数据分析</a>
                    </td>
                </tr>
            </ItemTemplate>
        </asp:Repeater>
    </table>
    </form>
</body>
</html>
<script src="../Scripts/jquery.delete.js" type="text/javascript"></script>
<script type="text/javascript">
    $(function () {
        $("#tblist tr").alternateColor();
    });
</script>
