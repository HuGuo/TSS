<%@ Page Language="C#" AutoEventWireup="true" CodeFile="EditMaintenanceCycle.aspx.cs"
    MasterPageFile="~/ValidateAndUi.master" Inherits="MaintenanceCycle_EditMaintenanceCycle" %>

<%@ Register src="../UserControl/MaintenanceCycleControl.ascx" tagname="MaintenanceCycleControl" tagprefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script language="javascript" type="text/javascript">
        $().ready(function () {
            $("#<%=Page.Form.UniqueID %>").validate();
        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="Server">
    <div id="toolbar" class="fixed">
        <a href="Default.aspx?s=<%= Request.QueryString["specialtyId"] %>">返回</a>
    </div>
       <uc1:MaintenanceCycleControl ID="MaintenanceCycleControl1" runat="server" />
                <asp:Button runat="server" ID="btnEdit" Text="编辑" CssClass="btn" OnClick="btnEdit_Click" />
</asp:Content>
