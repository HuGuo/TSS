<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AddMaintenanceExperiment.aspx.cs"
    MasterPageFile="~/ValidateAndUi.master" Inherits="MaintenanceCycle_AddMaintenanceExperiment" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript">
        $(function () {
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
                <asp:TextBox runat="server" AutoPostBack="true" onclick="WdatePicker()" validate="{date:true}"
                    ID="tbActualTime" OnTextChanged="tbActualTime_TextChanged"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                下次试验时间
            </td>
            <td>
                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <ContentTemplate>
                        <asp:TextBox runat="server" onclick="WdatePicker()" validate="{date:true,required:true}"
                            ID="tbExpectantTime"></asp:TextBox>
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="tbActualTime" />
                    </Triggers>
                </asp:UpdatePanel>
            </td>
        </tr>
        <tr>
            <td>
                关联试验报告
            </td>
            <td>
                <asp:DropDownList runat="server" ID="ddlExperiment">
                    <asp:ListItem Value="">请选择</asp:ListItem>
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <asp:Button runat="server" ID="btnAdd" Text="修改" CssClass="btn" OnClick="btnAdd_Click" />
            </td>
        </tr>
    </table>
    <asp:HiddenField runat="server" ID="hfCycle" />
</asp:Content>
