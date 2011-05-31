<%@ Page Language="C#" AutoEventWireup="true" CodeFile="MaintenanceExperiment.aspx.cs"
    Inherits="MaintenanceCycle_MaintenanceExperiment" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <a href="AddMaintenanceExperiment.aspx?maintenanceCycleId=><%= Request.QueryString["maintenanceExperimentId"] %>">添加</a>
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
                            修改</a>
                        <asp:LinkButton runat="server" ID="lbtnDel" CommandArgument="<%# ((TSS.Models.MaintenanceExperiment)Container.DataItem).Id%>"
                            OnClick="lbtnDel_Click">删除</asp:LinkButton>
                    </td>
                </tr>
            </ItemTemplate>
            <FooterTemplate>
                </table>
            </FooterTemplate>
        </asp:Repeater>
    </div>
    </form>
</body>
</html>
