<%@ Page Title="" Language="C#" MasterPageFile="~/ValidateAndUi.master" AutoEventWireup="true"
    CodeFile="Default.aspx.cs" Inherits="ComprehensiveReport_Default" %>

<asp:Content ID="Content3" ContentPlaceHolderID="head" runat="Server">
  <script type="text/javascript" src="../Scripts/jquery.validatewindow.js"></script>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="body" runat="Server">
    <div id="toolbar" class="fixed">
        <a href="AddSpecialtyAnalysis.aspx?specialtyId=<%= Request.QueryString["s"] %>">添加</a>
        <a href="Indicator.aspx?specialtyId=<%= Request.QueryString["s"] %>">指标管理</a>
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
                    <%# string.Format("{0}年{1}月{2}报表", Eval("ComprehensiveReport.ReportYear"), 
                    Eval("ComprehensiveReport.ReportMonth"), Eval("Specialty.Name"))%>
                </td>
                <td>
                </td>
                <td>
                </td>
                <td>
                    <a href="EditSpecialtyAnalysis.aspx?specialtyId=<%# Request.QueryString["s"] %>&id=<%# Eval("id")%>">
                        编辑</a> 
                     <asp:LinkButton runat="server"  OnClientClick="return $.Confirm('是否删除？',this)" ID="lbtnDel" 
                     CommandArgument='<%# Eval("id")%>' onclick="lbtnDel_Click">删除</asp:LinkButton>
                   <a href="SpecialtyAnalysisDetail.aspx?id=<%# Eval("id")%>&specialtyId=<%# Request.QueryString["s"] %>">
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
