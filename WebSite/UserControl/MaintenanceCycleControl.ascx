<%@ Control Language="C#" AutoEventWireup="true" CodeFile="MaintenanceCycleControl.ascx.cs"
    Inherits="UserControl_MaintenanceCycleControl" %>
<script type="text/javascript">
    $().ready(function () {
        $("#<%=Page.Form.UniqueID %>").validate();
    });
</script>
<table>
    <tr>
        <td>
            设备
        </td>
        <td>
            <asp:DropDownList runat="server" ID="ddlEquipment">
                <asp:ListItem Value="">请选择设备</asp:ListItem>
            </asp:DropDownList>
        </td>
    </tr>
    <tr>
        <td>
            设备类型
        </td>
        <td>
            <asp:DropDownList validate="{required:true}" runat="server" ID="ddlClass">
                <asp:ListItem Value="">请选择设备类型</asp:ListItem>
            </asp:DropDownList>
        </td>
    </tr>
    <tr>
        <td>
            设备型号
        </td>
        <td>
            <asp:TextBox runat="server" ID="tbModel"></asp:TextBox>
        </td>
    </tr>
    <tr>
        <td>
            检修类别
        </td>
        <td>
            <asp:TextBox runat="server" ID="tbType"></asp:TextBox>
        </td>
    </tr>
    <tr>
        <td>
            安装日期
        </td>
        <td>
            <asp:TextBox runat="server" onclick="WdatePicker()" validate="{date:true}" ID="tbInstallTime"></asp:TextBox>
        </td>
    </tr>
    <tr>
        <td>
            周期
        </td>
        <td>
            <asp:TextBox runat="server" class="ignore" validate="{required:true,number:true,min:0}"
                ID="tbCycle"></asp:TextBox>
        </td>
    </tr>
</table>
