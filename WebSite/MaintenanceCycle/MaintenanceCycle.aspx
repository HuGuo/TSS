<%@ Page Language="C#" AutoEventWireup="true" CodeFile="MaintenanceCycle.aspx.cs"
    Inherits="MaintenanceCycle_MaintenanceCycle" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:Button runat="server" ID="btnClass" Text="设备分类" onclick="btnClass_Click" />
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
                    <%# Container.ItemIndex %>
                </td>
                <td>
                   <%-- <%# ((TSS.Models.MaintenanceCycle)Container.DataItem)
                    .MaintenanceCalss.equipmentClassName%>--%>
                </td>
                <td>
                    <%# ((TSS.Models.MaintenanceCycle)Container.DataItem)
                    .EquipmentId.ToString()%>
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
                    <%# ((TSS.Models.MaintenanceCycle)Container.DataItem)
                    .InstallTime.ToString()%>
                </td>
                <td>
                    <%# ((TSS.Models.MaintenanceCycle)Container.DataItem).Cycle%>
                </td>
                <td>
                <%--    <%# ((TSS.Models.MaintenanceCycle)Container.DataItem)
                    .MaintenanceExperiments.Last().ExperimentTime.ToString()%>--%>
                </td>
                <td>
               <%--     <%# ((TSS.Models.MaintenanceCycle)Container.DataItem)
                    .MaintenanceExperiments.Last().ExperimentTime.AddYears(
                     int.Parse(((TSS.Models.MaintenanceCycle)Container.DataItem).Cycle))%>--%>
                </td>                
                <td>
                 <a href="EditMaintenanceCycle.aspx?id=<%# ((TSS.Models.MaintenanceCycle)Container.DataItem).Id.ToString()%>">编辑</a>
                  <a href="">删除</a>
                    <a href="AddMaintenanceCycle.aspx">添加</a>
                    <a href="MaintenanceExperiment.aspx?maintenanceExperimentId=<%# ((TSS.Models.MaintenanceCycle)Container.DataItem).Id.ToString()%>">历史记录</a>
                </td></tr>
            </ItemTemplate>
            <FooterTemplate>
                </table>
            </FooterTemplate>
        </asp:Repeater>
    </div>
    </form>
</body>
</html>
