<%@ Page Language="C#" AutoEventWireup="true" CodeFile="EditMaintenanceExperiment.aspx.cs"
    MasterPageFile="~/ValidateAndUi.master" Inherits="MaintenanceCycle_EditMaintenanceExperiment" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript">
        $().ready(function () {
            $("#<%=Page.Form.UniqueID %>").validate();
        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="Server">

    <div id="toolbar" class="fixed">
        <a href="MaintenanceExperiment.aspx?MaintenanceCycleId=<%= Request.QueryString["MaintenanceCycleId"] %>">
            返回</a>
    </div>
    <table>
        <tr>
            <td>
                上次试验时间
            </td>
            <td>
                <asp:TextBox runat="server" onclick="WdatePicker()" AutoPostBack="true" OnTextChanged="tbActualTime_TextChanged"
                    validate="{date:true}" ID="tbActualTime"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                下次试验时间
            </td>
            <td>
                <asp:TextBox runat="server" onclick="WdatePicker()" validate="{date:true,required:true}"
                    ID="tbExpectantTime"></asp:TextBox>
            </td>
        </tr>
        <tr>
            关联试验报告<td>
            </td>
            <td>
                <asp:DropDownList runat="server" ID="ddlExperiment">
                    <asp:ListItem Value="">请选择</asp:ListItem>
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <asp:Button runat="server" ID="btnEdit" Text="修改" CssClass="btn" OnClick="btnEdit_Click" />
            </td>
        </tr>
    </table>
    <asp:HiddenField runat="server" ID="hfCycle" />
    <asp:HiddenField runat="server" ID="hfMaintenanceCycleId" />
</asp:Content>
