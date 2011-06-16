<%@ Page Language="C#" AutoEventWireup="true" CodeFile="MaintenanceExperiment.aspx.cs"
    MasterPageFile="~/Default.master" Inherits="MaintenanceCycle_MaintenanceExperiment" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="Server">
    <script type="text/javascript">
        function Del(id) {
            $.messager.confirm("删除", "是否删除！", function (result) {
                if (result) {
                    window.location.href = "MaintenanceExperiment.aspx?id=" + id;
                } else {
                    return result;
                }
            });
        }
    </script>
    <div>
        <a href="AddMaintenanceExperiment.aspx?maintenanceCycleId=><%= Request.QueryString["maintenanceExperimentId"] %>">
            添加</a>
        <asp:Repeater runat="server" ID="rptExperiment">
            <HeaderTemplate>
                <table>
                    <tr>
                        <td>
                            预期试验时间
                        </td>
                        <td>
                            实际试验时间
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
                        <%# ((TSS.Models.MaintenanceExperiment)Container.DataItem).CurrentCycle.ToString() %>
                    </td>
                    <td>
                        <%# ((TSS.Models.MaintenanceExperiment)Container.DataItem).ExperimentTime.ToString() %>
                    </td>
                    <td>
                        关联试验报告
                    </td>
                    <td>
                        <a href="EditMaintenanceExperiment.aspx?id=<%# ((TSS.Models.MaintenanceExperiment)Container.DataItem).Id%>">
                            修改</a> <a href="#" onclick="Del(<%# ((TSS.Models.MaintenanceExperiment)Container.DataItem).Id.ToString() %>);">
                                删除</a>
                    </td>
                </tr>
            </ItemTemplate>
            <FooterTemplate>
                </table>
            </FooterTemplate>
        </asp:Repeater>
    </div>
</asp:Content>
