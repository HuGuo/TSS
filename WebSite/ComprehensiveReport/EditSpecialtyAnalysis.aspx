<%@ Page Title="" Language="C#" MasterPageFile="~/Default.master" AutoEventWireup="true"
    CodeFile="EditSpecialtyAnalysis.aspx.cs" Inherits="ComprehensiveReport_EditSpecialtyAnalysis" %>

<%@ Register Src="../UserControl/YearAndMonControl.ascx" TagName="YearAndMonControl"
    TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="Server">
    <table>
        <tr>
            <td align="center">
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
                        <asp:HiddenField runat="server" ID="hdfId" Value="<%# ((TSS.Models.IndicatorAnalysis)Container.DataItem).Id %>" />
                    </td>
                    <td>
                        <%# ((TSS.Models.IndicatorAnalysis)Container.DataItem).Indicator.IndicatorName %>
                         <asp:HiddenField runat="server" ID="hdfIndicatorId" Value="<%# ((TSS.Models.IndicatorAnalysis)Container.DataItem).IndicatorId %>"/>
                    </td>
                    <td>
                        <%# ((TSS.Models.IndicatorAnalysis)Container.DataItem).Indicator.IndivatorUnit%>
                    </td>
                    <td>
                        <asp:TextBox runat="server" ID="tbStanderdValue" Text="<%# ((TSS.Models.IndicatorAnalysis)Container.DataItem).StandardValue%>"></asp:TextBox>
                    </td>
                    <td>
                        <asp:TextBox runat="server" ID="tbActualValue" Text="<%# ((TSS.Models.IndicatorAnalysis)Container.DataItem).ActualValue%>"></asp:TextBox>
                    </td>
                    <td>
                        <asp:TextBox runat="server" TextMode="MultiLine" Text="<%# ((TSS.Models.IndicatorAnalysis)Container.DataItem).Analysis%>"
                            ID="tbIndicatorAnalysis"></asp:TextBox>
                    </td>
                </tr>
            </ItemTemplate>
            <FooterTemplate>
            </FooterTemplate>
        </asp:Repeater>
        <tr>
            <td>
                <table>
                    <tr>
                        <td>
                            综合分析
                        </td>
                        <td>
                            <asp:TextBox runat="server" ID="tbSpecialtyAnalysis" Text="" TextMode="MultiLine"></asp:TextBox>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <asp:HiddenField runat="server" ID="HfcomprehensiveReportId" Value="" />
    <p>
        <asp:Button runat="server" ID="btnAdd" Text="修改" OnClick="btnAdd_Click" />
        <input type="button" value="返回" onclick="window.location.href='SpecialtyAnalysis.aspx?specialtyId=<%= Request.QueryString["specialtyId"] %>'" />
    </p>
</asp:Content>
