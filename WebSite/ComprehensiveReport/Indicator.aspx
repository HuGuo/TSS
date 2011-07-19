<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Indicator.aspx.cs" MasterPageFile="~/ValidateAndUi.master"
    Inherits="ComprehensiveReport_Indicator" %>

<%@ Register Src="../UserControl/IndicatorControl.ascx" TagName="IndicatorControl"
    TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript" src="../Scripts/jquery.validatewindow.js"></script>
    <script type="text/javascript">
        $(function () {
            $.Validate("<%=Page.Form.UniqueID %>");
            $.InitWindow("wAdd", 300, 150);
            $.InitWindow("wEdit", 300, 150);
        });       
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="Server">
    <asp:ScriptManager runat="server" ID="ScriptManager1">
    </asp:ScriptManager>
    <div id="toolbar" class="fixed">
        <asp:UpdatePanel runat="server" ID="upLbtnAdd">
            <ContentTemplate>
                <asp:LinkButton runat="server" ID="lbtnAdd" OnClientClick="$.Open('wAdd')" OnClick="lbtnAdd_Click">添加</asp:LinkButton>
                <a href="Default.aspx?s=<%= Request.QueryString["specialtyId"] %>">返回</a>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
    <asp:UpdatePanel runat="server" UpdateMode="Conditional" ID="upClass">
        <ContentTemplate>
            <asp:Repeater ID="rptIndicator" runat="server">
                <HeaderTemplate>
                    <table>
                        <tr>
                            <td>
                                序号
                            </td>
                            <td>
                                指标
                            </td>
                            <td>
                                单位
                            </td>
                            <td>
                                专业
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
                            <%# Eval("IndicatorName")%>
                        </td>
                        <td>
                            <%# Eval("IndivatorUnit")%>
                        </td>
                        <td>
                            <%# Eval("Specialty.Name")%>
                        </td>
                        <td>
                            <asp:LinkButton runat="server" ID="lbtnDel" OnClick="lbtnDel_Click" CommandArgument='<%# Eval("Id")%>'
                                OnClientClick="return $.Confirm('是否删除？',this)">删除</asp:LinkButton>
                            <asp:LinkButton runat="server" ID="lbtnEdit" OnClick="lbtnEdit_Click" CommandArgument='<%# Eval("Id")%>'
                                OnClientClick="$.Open('wEdit')">编辑</asp:LinkButton>
                        </td>
                    </tr>
                </ItemTemplate>
                <FooterTemplate>
                    <tr style="visibility: <%#  rptIndicator.Items.Count==0?"visible":"hidden" %>">
                        <td colspan="5" align="center">
                            无指标
                        </td>
                    </tr>
                    </table>
                </FooterTemplate>
            </asp:Repeater>
        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="btnAdd" />
            <asp:AsyncPostBackTrigger ControlID="btnEdit" />
        </Triggers>
    </asp:UpdatePanel>
    <div id="wAdd">
        <asp:UpdatePanel ID="upAdd" runat="server">
            <ContentTemplate>
                <uc1:IndicatorControl ID="IndicatorControlAdd" runat="server" />
                <asp:Button runat="server" ID="btnAdd" Text="添加" OnClick="btnAdd_Click" OnClientClick="return $.Add('wAdd','upAdd')" />
                <asp:Button runat="server" ID="btnAddClose" Text="关闭" OnClientClick="$.Close('wAdd')"
                    OnClick="btnAddClose_Click" />
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
    <div id="wEdit">
        <asp:UpdatePanel ID="upEdit" runat="server">
            <ContentTemplate>
                <uc1:IndicatorControl ID="IndicatorControlEdit" runat="server" />
                <asp:Button runat="server" ID="btnEdit" Text="编辑" OnClientClick="return $.Edit('wEdit','upEdit')"
                    OnClick="btnEdit_Click" />
                <asp:Button runat="server" ID="btnEditClose" Text="关闭" OnClientClick="$.Close('wEdit')"
                    OnClick="btnEditClose_Click" />
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</asp:Content>
