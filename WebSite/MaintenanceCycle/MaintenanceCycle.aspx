<%@ Page Language="C#" AutoEventWireup="true" CodeFile="MaintenanceCycle.aspx.cs"
    MasterPageFile="~/ValidateAndUi.master" Inherits="MaintenanceCycle_MaintenanceCycle" %>

<%@ Import Namespace="TSS.Models" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
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
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="Server">

    <div id="toolbar" class="fixed">
        <a href="MaintenanceClass.aspx">设备分类</a><a href="AddMaintenanceCycle.aspx">添加</a>
    </div>
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
                    <%# ((MaintenanceCycle)Container.DataItem).InstallTime.HasValue ?
                        ((MaintenanceCycle)Container.DataItem).InstallTime.Value.ToShortDateString() : ""%>
                </td>
                <td>
                    <%# Eval("Cycle")%>
                </td>
                <td>
                    <%# ((MaintenanceCycle)Container.DataItem).MaintenanceExperiments.Count == 0 ?
                        "" : ((MaintenanceCycle)Container.DataItem).MaintenanceExperiments.Last().ActualTime.ToShortDateString()%>
                </td>
                <td>
                    <%# ((MaintenanceCycle)Container.DataItem).MaintenanceExperiments.Count == 0 ?
                        "" : ((MaintenanceCycle)Container.DataItem).MaintenanceExperiments.Last().ExpectantTime.Value.ToShortDateString()%>
                </td>
                <td>
                    <a href="AddMaintenanceCycle.aspx">添加</a> <a href="EditMaintenanceCycle.aspx?id=<%# Eval("Id")%>">
                        编辑</a> <a href="#" onclick="Del(<%# Eval("Id")%>);">
                            删除</a> <a href="MaintenanceExperiment.aspx?maintenanceExperimentId=<%# Eval("Id")%>">
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
</asp:Content>
