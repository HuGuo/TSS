<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AddMaintenanceClass.aspx.cs"
    MasterPageFile="~/Default.master" Inherits="MaintenanceCycle_AddMaintenanceClass" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="Server">
    <script type="text/javascript">
        $().ready(function () {
            $("#<%=Page.Form.UniqueID %>").validate();
        });
    </script>
    <div>
        <fieldset>
            <legend>添加设备类型 </legend>
            <p>
                <label>
                    设备类名</label>
                <asp:TextBox runat="server" validate="{required:true}" ID="tbClassNames"></asp:TextBox>
            </p>
            <p>
                <label>
                    专业名称</label>
                <asp:DropDownList runat="server" validate="{required:true}" ID="ddlSpecialty">
                    <asp:ListItem Value="">请选择专业</asp:ListItem>
                </asp:DropDownList>
            </p>
            <p>
                <asp:Button ID="btnAdd" runat="server" Text="添加" OnClick="btnAdd_Click" />
                <input type="button" value="取消" onclick="window.location.href='MaintenanceClass.aspx'" />
            </p>
        </fieldset>
    </div>
</asp:Content>
