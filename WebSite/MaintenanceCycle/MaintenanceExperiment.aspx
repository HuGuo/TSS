<%@ Page Language="C#" AutoEventWireup="true" CodeFile="MaintenanceExperiment.aspx.cs"
    MasterPageFile="~/ValidateAndUi.master" Inherits="MaintenanceCycle_MaintenanceExperiment" %>

<%@ Import Namespace="TSS.Models" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="Server">
    <script type="text/javascript">
        function Del(maintenanceExperimentId, id) {
            $.messager.confirm("删除", "是否删除！", function (result) {
                if (result) {
                    window.location.href = "MaintenanceExperiment.aspx?maintenanceExperimentId=" + maintenanceExperimentId + "&id=" + id;
                } else {
                    return result;
                }
            });
        }
    </script>
    <div id="toolbar" class="fixed">
        <a href="AddMaintenanceExperiment.aspx?maintenanceCycleId=<%= Request.QueryString["maintenanceExperimentId"] %>">添加</a>
        <a href="Default.aspx?s=<%= Request.QueryString["specialtyId"] %>">返回</a>
    </div>
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
                    <%# ((MaintenanceExperiment)Container.DataItem).ActualTime.ToShortDateString() %>
                </td>
                <td>
                    <%# ((MaintenanceExperiment)Container.DataItem).ExpectantTime.Value.ToShortDateString() %>
                </td>
                <td>
                    关联试验报告
                </td>
                <td>
                    <a href="EditMaintenanceExperiment.aspx?maintenanceCycleId=<%# ((TSS.Models.MaintenanceExperiment)Container.DataItem).MaintenanceCycleId%>&id=<%# ((TSS.Models.MaintenanceExperiment)Container.DataItem).Id%>">
                        修改</a> <a href="#" onclick="Del(<%# ((MaintenanceExperiment)Container.DataItem).MaintenanceCycleId.ToString() %>,<%# ((MaintenanceExperiment)Container.DataItem).Id.ToString() %>);">
                            删除</a>
                </td>
            </tr>
        </ItemTemplate>
        <FooterTemplate>
            </table>
        </FooterTemplate>
    </asp:Repeater>
</asp:Content>
