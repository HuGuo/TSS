<%@ Page Language="C#" AutoEventWireup="true" CodeFile="MaintenanceClass.aspx.cs"
    MasterPageFile="~/ValidateAndUi.master" Inherits="MaintenanceCycle_MaintenanceClass" %>

<%@ Import Namespace="TSS.Models" %>
<%@ Register Src="../UserControl/MaintenanceClassControl.ascx" TagName="MaintenanceClassControl"
    TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript">
        $(function () {
            Validate();
            InitWindow("wAdd");
            InitWindow("wEdit");
        });
        function Validate() {
            $("#<%=Page.Form.UniqueID %>").validate();
        }
        function InitWindow(id) {
            var dlg = $('#'+id).window({
                title: '设备类型',
                width: 300,
                height: 150,
                modal: true,
                shadow: false,
                closed: true,
                closable: false,
                collapsible: false,
                minimizable: false,
                maximizable: false  
            });
            dlg.parent().appendTo($("form:first"));
        }
        function Confirm(msg, control) {
            $.messager.confirm('确认', msg, function (r) {
                if (r) {
                    eval(control.toString().slice(11)); //截掉 javascript: 并执行
                }
            });
            return false;
        }

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="Server">
    <div id="toolbar" class="fixed">
        <a href="#" onclick="Open('wAdd')">添加</a><a href="Default.aspx?s=<%= Request.QueryString["specialtyId"] %>">返回</a>
    </div>
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:UpdatePanel runat="server" UpdateMode="Conditional" ID="upClass">
        <ContentTemplate>
            <asp:Repeater runat="server" ID="rptClass">
                <HeaderTemplate>
                    <table>
                        <tr>
                            <td>
                                类型名称
                            </td>
                            <td>
                                操作
                            </td>
                        </tr>
                </HeaderTemplate>
                <ItemTemplate>
                    <tr>
                        <td>
                            <%# Eval("equipmentClassName")%>
                        </td>
                        <td>
                        <asp:LinkButton runat="server" ID="lbtn" CommandArgument='<%# Eval("id") %>' OnClick="lbtnDel_Click" OnClientClick="return Confirm('是否删除？',this)">删除</asp:LinkButton>
                            <asp:LinkButton ID="lbtnEdit" Text="编辑" runat="server" OnClick="lbtnEdit_Click" CommandArgument='<%# Eval("id") %>'
                                OnClientClick="Open('wEdit')" ></asp:LinkButton>
                        </td>
                    </tr>
                </ItemTemplate>
                <FooterTemplate>
                    <tr style="visibility: <%# rptClass.Items.Count == 0 ? "visible" : "hidden"%>">
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
                <uc1:MaintenanceClassControl ID="MaintenanceClassControl1" runat="server" />
                <asp:Button runat="server" ID="btnAdd" Text="添加" OnClientClick="Close('wAdd')"   />
                <asp:Button runat="server" ID="btnAddClose" Text="关闭" OnClientClick="Close('wAdd')" 
                    OnClick="btnAddClose_Click" />
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
    <div id="wEdit">
        <asp:UpdatePanel ID="upEdit" runat="server">
            <ContentTemplate>
                <uc1:MaintenanceClassControl ID="MaintenanceClassControl2" runat="server" />
                <asp:Button runat="server" ID="btnEdit" Text="编辑" OnClientClick="Close('wEdit')" 
                    OnClick="btnEdit_Click" />
                <asp:Button runat="server" ID="btnEditClose" Text="关闭" OnClientClick="Close('wEdit')" 
                    OnClick="btnEditClose_Click" />
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</asp:Content>
