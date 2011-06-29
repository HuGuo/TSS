<%@ Page Title="" Language="C#" MasterPageFile="~/ValidateAndUi.master" AutoEventWireup="true"
    CodeFile="ComprehensiveReportDetail.aspx.cs" Inherits="ComprehensiveReport_ComprehensiveReportDetail" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="Server">
<div id="toolbar" class="fixed">
<a href="SpecialtyAnalysis.aspx?specialtyId=<%= Request.QueryString["specialtyId"] %>">返回</a>
</div>
     <table>
        <tr>
            <td colspan="7">
                <asp:Label runat="server" ID="lbTitle"></asp:Label>
            </td>
        </tr>
        <asp:Repeater runat="server" ID="rptComprehensiveReport" OnItemDataBound="rptComprehensiveReport_ItemDataBound">
            <HeaderTemplate>
                <tr>
                    <td>
                        序号
                    </td>
                    <td>
                        专业
                    </td>
                    <td>
                        指标名称
                    </td>
                    <td>
                        单位
                    </td>
                    <td>
                        标准值
                    </td>
                    <td>
                        实际值
                    </td>
                    <td>
                        指标分析
                    </td>
                </tr>
            </HeaderTemplate>
            <ItemTemplate>
                <asp:Repeater runat="server" ID="rptSpecialty">
                    <ItemTemplate>
                        <tr>
                            <td>
                                <%# Container.ItemIndex+1  %>
                            </td>
                            <td>
                                <%# Eval("SpecialtyAnalysis.Specialty.Name") %>
                            </td>
                            <td>
                                <%# Eval("Indicator.IndicatorName")%>
                            </td>
                            <td>
                                <%# Eval("Indicator.IndivatorUnit") %>
                            </td>
                            <td>
                                <%#Eval("StandardValue") %>
                            </td>
                            <td>
                                <%# Eval("ActualValue") %>
                            </td>
                            <td>
                                <%# Eval("Analysis") %>
                            </td>
                        </tr>
                    </ItemTemplate>
                </asp:Repeater>
                <tr>
                    <td colspan="7">
                        综合分析
                    </td>
                </tr>
                <tr>
                    <td colspan="7">
                        <%# ((TSS.Models.SpecialtyAnalysis)Container.DataItem).Analysis %>
                    </td>
                </tr>
            </ItemTemplate>
        </asp:Repeater>
        <tr>
            <td colspan="7">
            综合评价
            </td>
        </tr>
        <tr>
            <td colspan="7">
            <asp:Label runat="server" ID="lbComment"></asp:Label>
            </td>
        </tr>
    </table>
</asp:Content>
