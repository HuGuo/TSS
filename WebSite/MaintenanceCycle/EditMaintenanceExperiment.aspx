<%@ Page Language="C#" AutoEventWireup="true" CodeFile="EditMaintenanceExperiment.aspx.cs"
    MasterPageFile="~/Default.master" Inherits="MaintenanceCycle_EditMaintenanceExperiment" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="Server">
    <script type="text/javascript">
        $().ready(function () {
            $("#<%=Page.Form.UniqueID %>").validate();
        });
    </script>
    <div>
        <label>
            实际试验时间</label><asp:TextBox runat="server" onclick="WdatePicker()" validate="{required:true}" ID="tbExperimentTime"></asp:TextBox>
        <label>
            试验周期</label><asp:TextBox runat="server" validate="{required:true,number:true,min:0}" ID="tbCycle"></asp:TextBox>
        <label>
            关联试验报告</label>
        <asp:Button runat="server" ID="btnEdit" Text="修改" OnClick="btnEdit_Click" />
        <input type="button" value="取消" onclick="window.locatino.href='maintenanceCycle.aspx'" />
    </div>
</asp:Content>
