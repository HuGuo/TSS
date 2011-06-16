<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AddMaintenanceExperiment.aspx.cs" MasterPageFile="~/Default.master"
    Inherits="MaintenanceCycle_AddMaintenanceExperiment" %>

<asp:content id="Content1" contentplaceholderid="head" runat="Server">
</asp:content>
<asp:content id="Content2" contentplaceholderid="body" runat="Server">
    <script type="text/javascript">
        $().ready(function () {
            $("#<%=Page.Form.UniqueID %>").validate();
        });
    </script>
    <div>
        <label>
            实际试验时间</label><asp:TextBox runat="server"  onclick="WdatePicker()" validate="{date:true}" ID="tbExperimentTime"></asp:TextBox>
        <label>
            试验周期</label><asp:TextBox runat="server" validate="{required:true}"  ID="tbCycle"></asp:TextBox>
        <label>
            关联试验报告</label>
        <asp:Button runat="server" ID="btnAdd" Text="修改" OnClick="btnAdd_Click" />
        <input type="button" value="取消" onclick="window.locatino.href='maintenanceCycle.aspx'" />
    </div>
</asp:content>
