<%@ Page Language="C#" AutoEventWireup="true" CodeFile="MaintenanceExperiment.aspx.cs"
    MasterPageFile="~/ValidateAndUi.master" Inherits="MaintenanceCycle_MaintenanceExperiment" %>

<%@ Import Namespace="TSS.Models" %>
<%@ Register Src="../UserControl/MaintenanceExpControl.ascx" TagName="MaintenanceExpControl"
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
    <div id="toolbar" class="fixed">
        <a href="#" onclick="$.Open('wAdd')">添加</a> <a href="Default.aspx?s=<%= Request.QueryString["specialtyId"] %>">
            返回</a>
    </div>
    <asp:ScriptManager runat="server" ID="ScriptManager1">
    </asp:ScriptManager>
    <asp:UpdatePanel runat="server" UpdateMode="Conditional" ID="upClass">
        <ContentTemplate>
            <asp:Repeater runat="server" ID="rptExperiment">
                <HeaderTemplate>
                    <table>
                        <tr>
                            <td>
                                序号
                            </td>
                            <td>
                                上次试验时间
                            </td>
                            <td>
                                这次试验时间
                            </td>
                            <td>
                                调整前试验周期
                            </td>
                            <td>
                                调整后试验周期
                            </td>
                            <td>
                                关联试验报告
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
                            <%# Eval("LastExpTime", "{0:yyyy-MM-dd}")%>
                        </td>
                        <td>
                            <%# Eval("NextExpTime", "{0:yyyy-MM-dd}")%>
                        </td>
                        <td>
                            <%# Eval("CurrentCycle")%>
                        </td>
                        <td>
                            <%# Eval("MaintenanceCycle.Cycle")%>
                        </td>
                        <td>
                            关联试验报告
                        </td>
                        <td>
                            <asp:LinkButton runat="server" ID="lbtnDel" OnClick="lbtnDel_Click" CommandArgument='<%# Eval("id") %>'
                                OnClientClick="return $.Confirm('是否删除？',this)">删除</asp:LinkButton>
                            <asp:LinkButton runat="server" ID="lbtnEdit" OnClick="lbtnEdit_Click" CommandArgument='<%# Eval("id") %>'
                                OnClientClick="$.Open('wEdit')">编辑</asp:LinkButton>
                        </td>
                    </tr>
                </ItemTemplate>
                <FooterTemplate>
                    <tr style="visibility: <%# rptExperiment.Items.Count == 0 ? "visible" : "hidden"%>">
                        <td colspan="2" align="center">
                            无数据
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
                <uc1:MaintenanceExpControl ID="MaintenanceExpControl1" runat="server" />
                <asp:Button runat="server" ID="btnAdd" Text="添加" OnClick="btnAdd_Click" OnClientClick="return $.Add('wAdd','upAdd')" />
                <asp:Button runat="server" ID="btnAddClose" Text="关闭" OnClientClick="$.Close('wAdd')"
                    OnClick="btnAddClose_Click" />
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
    <div id="wEdit">
        <asp:UpdatePanel ID="upEdit" runat="server">
            <ContentTemplate>
                <uc1:MaintenanceExpControl ID="MaintenanceExpControl2" runat="server" />
                <asp:Button runat="server" ID="btnEdit" Text="编辑" OnClientClick="return $.Edit('wEdit','upEdit')"
                    OnClick="btnEdit_Click" />
                <asp:Button runat="server" ID="btnEditClose" Text="关闭" OnClientClick="$.Close('wEdit')"
                    OnClick="btnEditClose_Click" />
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</asp:Content>
