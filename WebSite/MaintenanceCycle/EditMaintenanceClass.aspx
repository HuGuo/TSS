<%@ Page Language="C#" AutoEventWireup="true" CodeFile="EditMaintenanceClass.aspx.cs"
    MasterPageFile="~/ValidateAndUi.master" Inherits="MaintenanceCycle_EditMaintenanceClass" %>

<%@ Register src="../UserControl/SpecialtyControl.ascx" tagname="SpecialtyControl" tagprefix="uc1" %>

<%@ Register src="../UserControl/MaintenanceClassControl.ascx" tagname="MaintenanceClassControl" tagprefix="uc2" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script language="javascript" type="text/javascript">
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
 <asp:Button ID="btnEdit" runat="server" Text="修改" CssClass="btn" OnClick="btnEdit_Click" />
</asp:Content>
