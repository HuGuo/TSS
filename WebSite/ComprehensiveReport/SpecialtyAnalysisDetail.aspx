<%@ Page Title="" Language="C#" MasterPageFile="~/ValidateAndUi.master" AutoEventWireup="true"
    CodeFile="SpecialtyAnalysisDetail.aspx.cs" Inherits="ComprehensiveReport_SpecialtyAnalysisDetail" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="Server">
    <div id="toolbar" class="fixed">
        <a href="Default.aspx?s=<%= Request.QueryString["specialtyId"] %>">
            返回</a>
    </div>
    <table>
        <tr>
            <td colspan="6">
                <asp:Label runat="server" ID="lbTitle"></asp:Label>
            </td>
        </tr>
        <asp:Repeater runat="server" ID="rptIndicator">
            <HeaderTemplate>
                <tr>
                    <td>
                        序号
                    </td>
                    <td>
                        指标名称
                    </td>
                    <td>
                        指标单位
                    </td>
                    <td>
                        标准值
                    </td>
                    <td>
                        实际值
                    </td>
                    <td>
                        分析
                    </td>
                </tr>
            </HeaderTemplate>
            <ItemTemplate>
                <tr>
                    <td>
                        <%# Container.ItemIndex+1 %>
                        <asp:HiddenField runat="server" ID="hdfId" Value='<%# Eval("Id") %>' />
                    </td>
                    <td>
                        <%# Eval("Indicator.IndicatorName") %>
                    </td>
                    <td>
                        <%# Eval("Indicator.IndivatorUnit")%>
                    </td>
                    <td>
                        <%# Eval("StandardValue")%>
                    </td>
                    <td>
                        <%# Eval("ActualValue")%>
                    </td>
                    <td>
                        <%# Eval("Analysis")%>
                    </td>
                </tr>
            </ItemTemplate>
        </asp:Repeater>
        <tr>
            <td colspan="6">
                <table>
                    <tr>
                        <td>
                            综合分析
                        </td>
                        <td>
                            <asp:Label runat="server" ID="lbSpecialtyAnalysis"></asp:Label>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</asp:Content>
