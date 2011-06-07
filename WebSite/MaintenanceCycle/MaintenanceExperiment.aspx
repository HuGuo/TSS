<%@ Page Language="C#" AutoEventWireup="true" CodeFile="MaintenanceExperiment.aspx.cs"
    Inherits="MaintenanceCycle_MaintenanceExperiment" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
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
                            修改</a>
                          <a href="#" onclick="Del(<%# ((TSS.Models.MaintenanceExperiment)Container.DataItem).Id.ToString() %>);">删除</a>
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
