<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CategoryList.aspx.cs" Inherits="Experiment_CategoryList" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>试验报告模板列表</title>
    <link href="../Styles/_base.css" rel="stylesheet" type="text/css" />
    <link href="../Styles/datasheet.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/jquery-1.6.1.min.js" type="text/javascript"></script>
</head>
<body>
    <form id="form1" runat="server">
    <div id="toolbar">
        <a runat="server" id="linkAdd" target="_blank">新建试验报告模板</a>
    </div>
    <div class="wrap">
        <div class="wrap_inner">
            <table id="tblist" class="datasheet" cellpadding="0" cellspacing="0">
                <thead>
                    <tr>
                        <th style="width: 20px;">
                        </th>
                        <th>
                            试验报告模板名称
                        </th>
                        <th style="width: 80px;">
                        </th>
                    </tr>
                </thead>
                <tbody>
                    <asp:Repeater runat="server" ID="rptlist">
                        <ItemTemplate>
                            <tr>
                                <th>
                                    <%#Container.ItemIndex+1 %>
                                </th>
                                <td>
                                    <a href="explist.aspx?id=<%#Eval("Id") %>&s=<%#Eval("specialtyId") %>&category=<%=Request.QueryString["category"] %>">
                                        <%#Eval("Title") %></a> <span style="color: #e74;">(<%#((TSS.Models.ExpTemplate)(Container.DataItem)).Experiments.Count %>)</span>
                                </td>
                                <td align="center">
                                    <a href="ChartStep1.aspx?tid=<%#Eval("Id") %>" target="_blank">数据分析</a>
                                </td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                </tbody>
            </table>
        </div>
    </div>
    </form>
</body>
</html>
<script src="../Scripts/jquery.tableStyle.js" type="text/javascript"></script>
