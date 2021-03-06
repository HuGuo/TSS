﻿<%@ Page Title="" Language="C#" MasterPageFile="~/ValidateAndUi.master" AutoEventWireup="true"
    CodeFile="ComprehensiveReport.aspx.cs" Inherits="ComprehensiveReport_ComprehensiveReport" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript">
        function Del(id) {
            $.messager.confirm("删除", "是否删除！", function (result) {
                if (result) {
                    window.location.href = "ComprehensiveReport.aspx?id=" + id;
                } else {
                    return result;
                }
            });
        }
    </script> 
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="Server">
    <div id="toolbar" class="fixed">
    
    </div>
        <asp:Repeater runat="server" ID="rptReport">
            <HeaderTemplate>
            <table>
                <tr>
                    <td>
                        序号
                    </td>
                    <td>
                        名称
                    </td>
                    <td>
                        审核
                    </td>
                    <td>
                        操作
                    </td>
                </tr>
            </HeaderTemplate>
            <ItemTemplate>
                <tr>
                    <td>
                        <%# Container.ItemIndex+1 %>
                    </td>
                    <td>
                    <%# string.Format("{0}年{1}月技术监督月报", Eval("ReportYear"), Eval("ReportMonth"))%>
                    </td>
                    <td>
                    </td>
                    <td>
                    <a href="#">综合评价</a>
                    <a href="#" onclick="Del(<%# Eval("id")  %>)">删除</a>
                    <a href="ComprehensiveReportDetail.aspx?id=<%# Eval("id")  %>">详细</a>
                    </td>
                </tr>
            </ItemTemplate>
            <FooterTemplate>
                <tr style="visibility: <%#  rptReport.Items.Count==0?"visible":"hidden" %>">
                    <td colspan="4" align="center">
                        无指标
                    </td>
                </tr>
                </table>
            </FooterTemplate>
        </asp:Repeater>
</asp:Content>
