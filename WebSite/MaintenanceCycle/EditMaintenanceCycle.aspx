<%@ Page Language="C#" AutoEventWireup="true" CodeFile="EditMaintenanceCycle.aspx.cs"
    MasterPageFile="~/Default.master" Inherits="MaintenanceCycle_EditMaintenanceCycle" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="Server">
    <script language="javascript" type="text/javascript">
        $().ready(function () {
            $("#<%=Page.Form.UniqueID %>").validate();
        });
    </script>
    <div>
        <fieldset>
            <legend>设备预示周期 </legend>
            <p>
                <asp:DropDownList runat="server" validate="{required:true}" ID="ddlEquipment">
                    <asp:ListItem Value="">请选择设备</asp:ListItem>
                </asp:DropDownList>
            </p>
            <p>
                <label>
                    设备类型</label>
                <asp:DropDownList runat="server" validate="{required:true}" ID="ddlClass">
                    <asp:ListItem Value="">请选择设备类型</asp:ListItem>
                </asp:DropDownList>
            </p>
            <p>
                <label>
                    设备型号</label><asp:TextBox runat="server" ID="tbModel"></asp:TextBox>
            </p>
            <p>
                <label>
                    检修类别</label><asp:TextBox runat="server" ID="tbType"></asp:TextBox>
            </p>
            <p>
                <label>
                    安装日期</label><asp:TextBox runat="server" onclick="WdatePicker()" validate="{date:true}"
                        ID="tbInstallTime"></asp:TextBox>
            </p>
            <p>
                <label>
                    周期</label><asp:TextBox runat="server" validate="{required:true,number:true,min:0}" ID="tbCycle"></asp:TextBox>
            </p>
            <p>
                <asp:Button runat="server" ID="btnEdit" Text="添加" OnClick="btnEdit_Click" />
                <input type="button" value="取消" onclick="window.document.location.href='MaintenanceCycle.aspx'" />
        </fieldset>
    </div>
</asp:Content>
