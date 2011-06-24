<%@ Page Language="C#" AutoEventWireup="true" CodeFile="MaintenanceClass.aspx.cs"
    MasterPageFile="~/ValidateAndUi.master" Inherits="MaintenanceCycle_MaintenanceClass" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="Server">
    <script type="text/javascript">
        function Del(id) {
            $.messager.confirm("删除", "是否删除！", function (result) {
                if (result) {
                    window.location.href = "MaintenanceClass.aspx?id=" + id;
                } else {
                    return result;
                }
            });
        }
    </script>
   <div id="toolbar" class="fixed">
        <a href="AddMaintenanceClass.aspx?specialtyId=<%= Request.QueryString["specialtyId"] %>">添加</a>
        <a href="Default.aspx?s=<%= Request.QueryString["specialtyId"] %>">返回</a>
    </div>
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
                        <%# ((TSS.Models.MaintenanceClass)Container.DataItem).equipmentClassName %>
                    </td>
                    <td>
                        <a href="EditMaintenanceClass.aspx?id=<%# ((TSS.Models.MaintenanceClass)Container.DataItem).Id %>">
                            编辑</a> <a onclick="Del(<%# ((TSS.Models.MaintenanceClass)Container.DataItem).Id.ToString() %>)"
                                href="#">删除</a>
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
</asp:Content>
