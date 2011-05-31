<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AddMaintenanceClass.aspx.cs" Inherits="MaintenanceCycle_AddMaintenanceClass" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>设备类型添加</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
     <label>设备类名</label>
     <asp:TextBox runat="server" ID="tbClassNames">
     
     </asp:TextBox>
     <label>专业名称</label>
     <asp:DropDownList runat="server" ID="ddlSpecialty">
     <asp:ListItem Text="">请选择专业</asp:ListItem>
     </asp:DropDownList>
        <asp:Button ID="btnAdd" runat="server" Text="添加" onclick="btnAdd_Click" />
        <asp:Button ID="btnCancle" runat="server" Text="取消" onclick="btnCancle_Click" />
    </div>   
    </form>
</body>
</html>
