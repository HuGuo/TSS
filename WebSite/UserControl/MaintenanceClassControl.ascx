<%@ Control Language="C#" AutoEventWireup="true" CodeFile="MaintenanceClassControl.ascx.cs" Inherits="UserControl_MaintenanceClassControl" %>
   <%@ Register src="SpecialtyControl.ascx" tagname="SpecialtyControl" tagprefix="uc1" %>
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
                
                <uc1:SpecialtyControl ID="SpecialtyControl1" runat="server" />
                
            </td>
        </tr>
    </table>