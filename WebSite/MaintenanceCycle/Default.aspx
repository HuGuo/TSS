<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" MasterPageFile="~/ValidateAndUi.master"
    Inherits="MaintenanceCycle_Default" %>

<%@ Import Namespace="TSS.Models" %>
<%@ Register Src="../UserControl/MaintenanceCycleControl.ascx" TagName="MaintenanceCycleControl"
    TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
 <script type="text/javascript" src="../Scripts/jquery.validatewindow.js"></script>
    <script type="text/javascript">
        $(function () {
            $.Validate("<%=Page.Form.UniqueID %>");
            $.InitWindow("wAdd", 300, 250);
            $.InitWindow("wEdit", 300, 250);
        });       
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="Server">
    <div id="toolbar" class="fixed">
        <a href="#" onclick="$.Open('wAdd')">添加</a> <a href="MaintenanceClass.aspx?specialtyId=<%= Request.QueryString["s"] %>">
            设备分类</a>
        <div class="search">查询：
            <asp:DropDownList ID="ddlClass" AutoPostBack="true" runat="server" 
                onselectedindexchanged="ddlClass_SelectedIndexChanged">
                <asp:ListItem Value="">全选</asp:ListItem>
            </asp:DropDownList>
        </div>
    </div>
    <asp:ScriptManager runat="server" ID="ScriptManager1">
    </asp:ScriptManager>
    <asp:UpdatePanel runat="server" UpdateMode="Conditional" ID="upContent">
        <ContentTemplate>
            <asp:Repeater ID="rptCycle" runat="server">
                <HeaderTemplate>
                    <table>
                        <tr>
                            <td>
                                序号
                            </td>
                            <td>
                                设备类型
                            </td>
                            <td>
                                设备名称
                            </td>
                            <td>
                                设备型号
                            </td>
                            <td>
                                检修类别
                            </td>
                            <td>
                                安装日期
                            </td>
                            <td>
                                调整前周期
                            </td>
                             <td>
                                调整后周期
                            </td>
                            <td>
                                最近试验
                            </td>
                            <td>
                                下次试验
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
                            <%# Eval("MaintenanceCalss.equipmentClassName")%>
                        </td>
                        <td>
                        </td>
                        <td>
                            <%# Eval("EquipmentModel") %>
                        </td>
                        <td>
                            <%# Eval("MaintenanceType") %>
                        </td>
                        <td>
                            <%# Eval("InstallTime", "{0:yyyy-MM-dd}")%>
                        </td>
                        <td>
                            <%# GetLastExpCycle(Container.DataItem)%>
                        </td>
                          <td>
                            <%# Eval("Cycle")%>
                        </td>
                        <td>
                            <%# GetLastExpTime(Container.DataItem)%>
                        </td>
                        <td>
                            <%# GetNextExpTime(Container.DataItem)%>
                        </td>
                        <td>
                            <asp:LinkButton runat="server" ID="lbtnDel" OnClick="lbtnDel_Click" CommandArgument='<%# Eval("Id")%>'
                                OnClientClick="return $.Confirm('是否删除？',this)">删除</asp:LinkButton>
                            <asp:LinkButton runat="server" ID="lbtnEdit" CommandArgument='<%# Eval("Id")%>' OnClick="lbtnEdit_Click"
                                OnClientClick="$.Open('wEdit')">编辑</asp:LinkButton>
                            <a href="MaintenanceExperiment.aspx?specialtyId=<%= Request.QueryString["s"]%>&maintenanceCycleId=<%# Eval("Id")%>">
                                历史记录</a>
                        </td>
                    </tr>
                </ItemTemplate>
                <FooterTemplate>
                    <tr style="visibility: <%# rptCycle.Items.Count==0?"visible":"hidden" %>">
                        <td colspan="10" align="center">
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
                <uc1:MaintenanceCycleControl ID="MaintenanceCycleControl1" runat="server" />
                <asp:Button runat="server" ID="btnAdd" Text="添加" OnClientClick="return $.Add('wAdd','upAdd')" OnClick="btnAdd_Click" />
                <asp:Button runat="server" ID="btnAddClose" Text="关闭" OnClientClick="$.Close('wAdd')"
                    OnClick="btnAddClose_Click" />
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
    <div id="wEdit">
        <asp:UpdatePanel ID="upEdit" runat="server">
            <ContentTemplate>
                <uc1:MaintenanceCycleControl ID="MaintenanceCycleControl2" runat="server" />
                <asp:Button runat="server" ID="btnEdit" Text="编辑" OnClientClick="return $.Edit('wEdit','upEdit')"
                    OnClick="btnEdit_Click" />
                <asp:Button runat="server" ID="btnEditClose" Text="关闭" OnClientClick="$.Close('wEdit')"
                    OnClick="btnEditClose_Click" />
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</asp:Content>
