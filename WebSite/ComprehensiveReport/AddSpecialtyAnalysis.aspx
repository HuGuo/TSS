<%@ Page Title="" Language="C#" MasterPageFile="~/ValidateAndUi.master" AutoEventWireup="true"
    CodeFile="AddSpecialtyAnalysis.aspx.cs" Inherits="ComprehensiveReport_AddSpecialtyAnalysis" %>

<%@ Register Src="../UserControl/YearAndMonControl.ascx" TagName="YearAndMonControl"
    TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="Server">
    <div id="toolbar" class="fixed">
        <a href="Default.aspx?s=<%= Request.QueryString["specialtyId"] %>">
            返回</a>
    </div>
    <table>
        <tr>
            <td colspan="6" align="center">
                <uc1:YearAndMonControl ID="YearAndMonControl1" runat="server" />
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
                        <asp:HiddenField ID="tbIndicatorId" Value='<%# Eval("Id") %>'
                            runat="server" />
                    </td>
                    <td>
                        <%# Eval("IndicatorName") %>
                    </td>
                    <td>
                        <%# Eval("IndivatorUnit") %>
                    </td>
                    <td>
                        <asp:TextBox runat="server" ID="tbStanderdValue"></asp:TextBox>
                    </td>
                    <td>
                        <asp:TextBox runat="server" ID="tbActualValue"></asp:TextBox>
                    </td>
                    <td>
                        <asp:TextBox runat="server" TextMode="MultiLine" ID="tbIndicatorAnalysis"></asp:TextBox>
                    </td>
                </tr>
            </ItemTemplate>
            <FooterTemplate>
            </FooterTemplate>
        </asp:Repeater>
        <tr>
            <td colspan="6">
                综合分析
            </td>
        </tr>
        <tr>
            <td colspan="6">
                <asp:TextBox runat="server" ID="tbSpecialtyAnalysis" TextMode="MultiLine"></asp:TextBox>
            </td>
        </tr><tr>
        <td colspan="6">
            <asp:Button runat="server" ID="btnAdd" Text="添加" CssClass="btn" OnClick="btnAdd_Click" />
        </td</tr>
    </table>
</asp:Content>
