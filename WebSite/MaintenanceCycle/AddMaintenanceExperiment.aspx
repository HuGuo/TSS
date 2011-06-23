<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AddMaintenanceExperiment.aspx.cs"
    MasterPageFile="~/Default.master" Inherits="MaintenanceCycle_AddMaintenanceExperiment" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="Server">
    <script type="text/javascript">
        $(function () {
            $("#<%=Page.Form.UniqueID %>").validate();
        });
    </script>
    <div>
        <fieldset>
            <legend>关联试验报告 </legend>
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <p>
                        <label>
                            上次试验时间</label><asp:TextBox runat="server" AutoPostBack="true" onclick="WdatePicker()"
                                validate="{date:true}" ID="tbActualTime" OnTextChanged="tbActualTime_TextChanged"></asp:TextBox>
                    </p>
                    <p>
                        <label>
                            下次试验时间</label><asp:TextBox runat="server" onclick="WdatePicker()" validate="{date:true,required:true}"
                                ID="tbExpectantTime"></asp:TextBox></p>
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="tbActualTime" />
                </Triggers>
            </asp:UpdatePanel>
            <p>
                <label>
                    关联试验报告</label>
                <asp:DropDownList runat="server" ID="ddlExperiment">
                    <asp:ListItem Value="">请选择</asp:ListItem>
                </asp:DropDownList>
            </p>
            <p>
                <asp:Button runat="server" ID="btnAdd" Text="修改" OnClick="btnAdd_Click" />
                <input type="button" value="取消" onclick="window.location.href='MaintenanceExperiment.aspx?MaintenanceCycleId=<%= Request.QueryString["MaintenanceCycleId"] %>'" /></p>
            <asp:HiddenField runat="server" ID="hfCycle" />
        </fieldset>
    </div>
</asp:Content>
