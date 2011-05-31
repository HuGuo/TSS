<%@ Page Language="C#" AutoEventWireup="true" CodeFile="EditMaintenanceExperiment.aspx.cs"
    Inherits="MaintenanceCycle_EditMaintenanceExperiment" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <label>
            实际试验时间</label><asp:TextBox runat="server" ID="tbExperimentTime"></asp:TextBox>
        <label>
            试验周期</label><asp:TextBox runat="server" ID="tbCycle"></asp:TextBox>
        <label>
            关联试验报告</label>
        <asp:Button runat="server" ID="btnEdit" Text="修改" OnClick="btnEdit_Click" />
        <asp:Button runat="server" ID="btnCancle" Text="取消" OnClick="btnCancle_Click" />
    </div>
    </form>
</body>
</html>
