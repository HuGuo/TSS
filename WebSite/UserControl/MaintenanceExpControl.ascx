<%@ Control Language="C#" AutoEventWireup="true" CodeFile="MaintenanceExpControl.ascx.cs" Inherits="UserControl_MaintenanceExpControl" %>
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
    </table>
    <asp:HiddenField runat="server" ID="hdMaintenanceCycleId" />
