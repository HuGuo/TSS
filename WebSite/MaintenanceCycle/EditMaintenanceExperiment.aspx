<%@ Page Language="C#" AutoEventWireup="true" CodeFile="EditMaintenanceExperiment.aspx.cs"
    MasterPageFile="~/Default.master" Inherits="MaintenanceCycle_EditMaintenanceExperiment" %>

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
            <legend>修改试验报告</legend>
            <asp:UpdatePanel runat="server">
                <ContentTemplate>
                    <p>
                        <label>
                            上次试验时间</label><asp:TextBox runat="server" onclick="WdatePicker()" AutoPostBack="true" OnTextChanged="tbActualTime_TextChanged"
                                validate="{date:true}" ID="tbActualTime"></asp:TextBox></p>
                    <p>
                        <label>
                            下次试验时间</label><asp:TextBox runat="server" onclick="WdatePicker()" validate="{date:true,required:true}"
                                ID="tbExpectantTime"></asp:TextBox>
                    </p>
                    <p>
                        <label>
                            关联试验报告</label>
                        <asp:DropDownList runat="server" ID="ddlExperiment">
                            <asp:ListItem Value="">请选择</asp:ListItem>
                        </asp:DropDownList>
                    </p>
                </ContentTemplate>
                <Triggers>
                <asp:AsyncPostBackTrigger ControlID="tbActualTime" />
                </Triggers>
            </asp:UpdatePanel>
            <p>
                <asp:Button runat="server" ID="btnEdit" Text="修改" OnClick="btnEdit_Click" />
                <input type="button" value="取消" onclick="window.location.href='MaintenanceExperiment.aspx?MaintenanceCycleId=<%= Request.QueryString["MaintenanceCycleId"] %>'" />
            </p>
            <asp:HiddenField runat="server" ID="hfCycle" />
            <asp:HiddenField runat="server" ID="hfMaintenanceCycleId" />
        </fieldset>
    </div>
</asp:Content>
