<%@ Page Language="C#" AutoEventWireup="true" CodeFile="MaintenanceCycle.aspx.cs" MasterPageFile="~/Default.master"
    Inherits="MaintenanceCycle_MaintenanceCycle" %>

<asp:content id="Content1" contentplaceholderid="head" runat="Server">

</asp:content>
<asp:content id="Content2" contentplaceholderid="body" runat="Server">
    <script type="text/javascript">
        function Del(id) {
            $.messager.confirm("删除", "是否删除！", function (result) {
                if (result) {
                    window.location.href = "MaintenanceCycle.aspx?id=" + id;
                } else {
                    return result;
                }
            });
        }
    </script>
    <div>
        <a href="MaintenanceClass.aspx">设备分类</a>
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
                            周期
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
                        <%# ((TSS.Models.MaintenanceCycle)Container.DataItem)
                    .MaintenanceCalss.equipmentClassName%>
                    </td>
                    <td>
                    </td>
                    <td>
                        <%# ((TSS.Models.MaintenanceCycle)Container.DataItem)
                .EquipmentModel %>
                    </td>
                    <td>
                        <%# ((TSS.Models.MaintenanceCycle)Container.DataItem)
                .MaintenanceType %>
                    </td>
                    <td>
                        <%# ((TSS.Models.MaintenanceCycle)Container.DataItem).InstallTime.HasValue ?
                        ((TSS.Models.MaintenanceCycle)Container.DataItem).InstallTime.Value.ToShortDateString() : ""%>
                    </td>
                    <td>
                        <%# ((TSS.Models.MaintenanceCycle)Container.DataItem).Cycle%>
                    </td>
                    <td>
                        <%# ((TSS.Models.MaintenanceCycle)Container.DataItem).MaintenanceExperiments.Count == 0 ?
                        "" : ((TSS.Models.MaintenanceCycle)Container.DataItem).MaintenanceExperiments.Last().ExperimentTime.ToString()%>
                    </td>
                    <td>
                        <%# ((TSS.Models.MaintenanceCycle)Container.DataItem).MaintenanceExperiments.Count == 0 ?
                        "": ((TSS.Models.MaintenanceCycle)Container.DataItem).MaintenanceExperiments.Last().ExperimentTime.AddYears(
                     int.Parse(((TSS.Models.MaintenanceCycle)Container.DataItem).Cycle)).ToShortDateString()%>
                    </td>
                    <td>
                        <a href="AddMaintenanceCycle.aspx">添加</a> 
                        <a href="EditMaintenanceCycle.aspx?id=<%# ((TSS.Models.MaintenanceCycle)Container.DataItem).Id.ToString()%>">编辑</a>
                        <a href="#" onclick="Del(<%# ((TSS.Models.MaintenanceCycle)Container.DataItem).Id.ToString()%>);">删除</a>
                        <a href="MaintenanceExperiment.aspx?maintenanceExperimentId=<%# ((TSS.Models.MaintenanceCycle)Container.DataItem).Id.ToString()%>">历史记录</a>
                    </td>
                </tr>
            </ItemTemplate>
            <FooterTemplate>
                <tr style="visibility: <%# rptCycle.Items.Count==0?"visible":"hidden" %>">
                    <td colspan="9" align="center">
                        无数据
                    </td>
                    <td>
                        <a href="AddMaintenanceCycle.aspx">添加</a>
                    </td>
                </tr>
                </table>
            </FooterTemplate>
        </asp:Repeater>
    </div>
 </asp:content>
