<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AddMaintenanceClass.aspx.cs"
    MasterPageFile="~/ValidateAndUi.master" Inherits="MaintenanceCycle_AddMaintenanceClass" %>

<%@ Register src="../UserControl/SpecialtyControl.ascx" tagname="SpecialtyControl" tagprefix="uc1" %>

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
    <table>
        <tr>
            <td>
                设备类名
            </td>
            <td>
                <asp:TextBox runat="server" validate="{required:true}" ID="tbClassNames"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                专业名称
            </td>
            <td>
                <uc1:SpecialtyControl ID="ucSpecialtyControl" runat="server" />
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <asp:Button ID="btnAdd" runat="server" Text="添加" CssClass="btn" OnClick="btnAdd_Click" />
            </td>
        </tr>
    </table>
</asp:Content>
