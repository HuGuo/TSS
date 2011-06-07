<%@ Page Language="C#" AutoEventWireup="true" CodeFile="MaintenanceCycle.aspx.cs"
    Inherits="MaintenanceCycle_MaintenanceCycle" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>设备周期</title>
    <link rel="Stylesheet" type="text/css" href="../scripts/jquery-easyui/thems/default/easyui.css" />
    <link rel="Stylesheet" type="text/css" href="../scripts/jquery-easyui/thems/icon.css" />
    <script src="../scripts/jquery-1.6.1.min.js" type="text/javascript"></script>
    <script src="../scripts/jquery-validation/lib/jquery.js" type="text/javascript"></script>
    <script src="../scripts/jquery-validation/jquery.validate.js" type="text/javascript"></script>
    <script src="../scripts/jquery-validation/messages_cn.js" type="text/javascript"></script>
    <script src="../scripts/jquery-easyui/jquery.easyui.min.js" type="text/javascript"></script>
    <script src="../scripts/My97DatePicker/WdatePicker.js" type="text/javascript"></script>
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
</head>
<body>
    <form id="form1" runat="server">
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
                        <%# ((TSS.Models.MaintenanceCycle)Container.DataItem)
                    .InstallTime.Value.ToShortDateString()%>
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
    </form>
</body>
</html>
