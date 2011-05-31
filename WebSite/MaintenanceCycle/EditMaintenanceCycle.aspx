<%@ Page Language="C#" AutoEventWireup="true" CodeFile="EditMaintenanceCycle.aspx.cs" Inherits="MaintenanceCycle_EditMaintenanceCycle" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
       <asp:DropDownList runat="server" id="ddlEquipment" Enabled="false">
            <asp:ListItem Value="">请选择设备</asp:ListItem>
            </asp:DropDownList>
   <br /><label>设备类型</label> <asp:DropDownList runat="server" ID="ddlClass">
   <asp:ListItem Value="">请选择设备类型</asp:ListItem>
   </asp:DropDownList>
     <br />  <label>设备型号</label><asp:TextBox runat="server" ID="tbModel"></asp:TextBox>
     <br />   <label>检修类别</label><asp:TextBox runat="server" ID="tbType"></asp:TextBox>
      <br />   <label>安装日期</label><asp:TextBox runat="server" ID="tbInstallTime"></asp:TextBox>
       <br />   <label>周期</label><asp:TextBox runat="server" ID="tbCycle"></asp:TextBox>
 <br /><asp:Button runat="server" ID="btnEdit" Text="修改" onclick="btnEdit_Click" />
 <asp:Button runat="server" ID="btnCancle" Text="取消" onclick="btnCancle_Click" />
    </div>
    </form>
</body>
</html>
