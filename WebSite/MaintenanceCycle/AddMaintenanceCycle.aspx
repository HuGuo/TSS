<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AddMaintenanceCycle.aspx.cs"
    MasterPageFile="~/ValidateAndUi.master" Inherits="MaintenanceCycle_AddMaintenanceCycle" %>

<%@ Register src="../UserControl/SpecialtyControl.ascx" tagname="SpecialtyControl" tagprefix="uc1" %>

<%@ Register src="../UserControl/MaintenanceCycleControl.ascx" tagname="MaintenanceCycleControl" tagprefix="uc2" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript">
        $().ready(function () {
            $("#<%=Page.Form.UniqueID %>").validate();
        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="Server">
    <div id="toolbar" class="fixed">
        <a href="Default.aspx?s=<%= Request.QueryString["specialtyId"] %>">返回</a>
    </div>
 <uc2:MaintenanceCycleControl ID="MaintenanceCycleControl1" runat="server" />
                <asp:Button runat="server" ID="btnAdd" Text="添加" CssClass="btn" OnClick="btnAdd_Click" />
</asp:Content>
