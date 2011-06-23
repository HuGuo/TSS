<%@ Page Title="" Language="C#" MasterPageFile="~/Default.master" AutoEventWireup="true"
    CodeFile="AddSpecialtyAnalysis.aspx.cs" Inherits="ComprehensiveReport_AddSpecialtyAnalysis" %>

<%@ Register Src="../UserControl/YearAndMonControl.ascx" TagName="YearAndMonControl"
    TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="Server">
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
                        <asp:HiddenField ID="tbIndicatorId" Value="<%# ((TSS.Models.Indicator)Container.DataItem).Id %>"
                            runat="server" />
                    </td>
                    <td>
                        <%# ((TSS.Models.Indicator)Container.DataItem).IndicatorName %>
                    </td>
                    <td>
                        <%# ((TSS.Models.Indicator)Container.DataItem).IndivatorUnit %>
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
                <table>
                    <tr>
                        <td>
                            综合分析
                        </td>
                        <td>
                            <asp:TextBox runat="server" ID="tbSpecialtyAnalysis" TextMode="MultiLine"></asp:TextBox>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <p>
        <asp:Button runat="server" ID="btnAdd" Text="添加" OnClick="btnAdd_Click" />
        <input value="返回" type="button" onclick="window.location.href='SpecialtyAnalysis.aspx?specialtyId=<%= Request.QueryString["specialtyId"] %>'" />
    </p>
</asp:Content>
