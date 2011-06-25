<%@ Page Title="" Language="C#" MasterPageFile="~/ValidateAndUi.master" AutoEventWireup="true"
    CodeFile="SpecialtyAnalysis.aspx.cs" Inherits="ComprehensiveReport_SpecialtyAnalysis" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript">
        function Del(id, specialtyId) {
            $.messager.confirm("删除", "是否删除！", function (result) {
                if (result) {
                    window.location.href = "SpecialtyAnalysis.aspx?id=" + id + "&specialtyId=" + specialtyId;
                } else {
                    return result;
                }
            });
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="Server">
    <div id="toolbar" class="fixed">
        <a href="AddSpecialtyAnalysis.aspx?specialtyId=<%= Request.QueryString["s"] %>">添加</a>
        <a href="Indicator.aspx?specialtyId=GHY-JY">指标管理</a>
    </div>
    <asp:Repeater runat="server" ID="rptSpecialtyAnalysis">
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
                        流程流转状态
                    </td>
                    <td>
                        启动流程
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
                    <%= string.Format("{0}年{1}月{2}报表", Eval("ComprehensiveReport.ReportYear"),
                    Eval("ComprehensiveReport.ReportMonth"), Eval("Specialty.Name"))%>
                </td>
                <td>
                </td>
                <td>
                </td>
                <td>
                    <a href="EditSpecialtyAnalysis.aspx?specialtyId=<%# Request.QueryString["specialtyId"] %>&id=<%# Eval("Id")%>">
                        编辑</a> <a onclick="Del(<%# Eval("Id")%>,'<%# Request.QueryString["specialtyId"] %>')"
                            href="#">删除</a> <a href="SpecialtyAnalysisDetail.aspx?id=<%# Eval("Id")%>&specialtyId=<%# Request.QueryString["specialtyId"] %>">
                                详细</a>
                </td>
            </tr>
        </ItemTemplate>
        <FooterTemplate>
            <tr style="visibility: <%# rptSpecialtyAnalysis.Items.Count==0?"visible":"hidden" %>">
                <td colspan="6" align="center">
                    无数据
                </td>
            </tr>
            </table>
        </FooterTemplate>
    </asp:Repeater>
</asp:Content>
