<%@ Page Title="" Language="C#" MasterPageFile="~/Default.master" AutoEventWireup="true"
    CodeFile="SpecialtyAnalysis.aspx.cs" Inherits="ComprehensiveReport_SpecialtyAnalysis" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="Server">
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
    <div>
        <a href="AddSpecialtyAnalysis.aspx?specialtyId=<%= Request.QueryString["specialtyId"] %>">
            添加</a>   <a href="Indicator.aspx?specialtyId=GHY-JY">指标管理</a>
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
                        <%# ((TSS.Models.SpecialtyAnalysis)Container.DataItem).ComprehensiveReport.ReportYear.ToString()+"年"+
               ((TSS.Models.SpecialtyAnalysis)Container.DataItem).ComprehensiveReport.ReportMonth.ToString()+"月"
               + ((TSS.Models.SpecialtyAnalysis)Container.DataItem).Specialty.Name+ "报表"%>
                    </td>
                    <td>
                    </td>
                    <td>
                    </td>
                    <td>
                        <a href="EditSpecialtyAnalysis.aspx?specialtyId=<%# Request.QueryString["specialtyId"] %>&id=<%# ((TSS.Models.SpecialtyAnalysis)Container.DataItem).Id%>">
                            编辑</a> <a onclick="Del(<%# ((TSS.Models.SpecialtyAnalysis)Container.DataItem).Id%>,'<%# Request.QueryString["specialtyId"] %>')" href="#">
                                删除</a>
                        <a href="SpecialtyAnalysisDetail.aspx?id=<%# ((TSS.Models.SpecialtyAnalysis)Container.DataItem).Id%>&specialtyId=<%# Request.QueryString["specialtyId"] %>">详细</a>
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
    </div>
</asp:Content>
