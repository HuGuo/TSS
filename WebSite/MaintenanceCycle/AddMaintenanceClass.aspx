<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AddMaintenanceClass.aspx.cs"
    MasterPageFile="~/ValidateAndUi.master" Inherits="MaintenanceCycle_AddMaintenanceClass" %>

<%@ Register src="../UserControl/SpecialtyControl.ascx" tagname="SpecialtyControl" tagprefix="uc1" %>

<%@ Register src="../UserControl/MaintenanceClassControl.ascx" tagname="MaintenanceClassControl" tagprefix="uc2" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript">
        $().ready(function () {
            $("#<%=Page.Form.UniqueID %>").validate();
        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="Server">

    <div id="toolbar" class="fixed">
    <a href="MaintenanceClass.aspx?specialtyId=<%= Request.QueryString["specialtyId"] %>">返回</a>
    </div>
        <uc2:MaintenanceClassControl ID="MaintenanceClassControl1" runat="server" />
<asp:Button ID="btnAdd" runat="server" Text="添加" CssClass="btn" OnClick="btnAdd_Click" />

</asp:Content>
