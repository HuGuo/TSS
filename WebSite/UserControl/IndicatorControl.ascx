<%@ Control Language="C#" AutoEventWireup="true" CodeFile="IndicatorControl.ascx.cs" Inherits="UserControl_IndicatorControl" %>
 <%@ Register src="SpecialtyControl.ascx" tagname="SpecialtyControl" tagprefix="uc1" %>
 <table>
        <tr>
            <td>
                指标名称
            </td>
            <td>
                <asp:TextBox runat="server" validate="{required:true}" ID="tbName"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                指标单位
            </td>
            <td>
                <asp:TextBox runat="server" validate="{required:true}" ID="tbUnit"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                专业
            </td>
            <td>
                <uc1:SpecialtyControl ID="SpecialtyControl1" runat="server" />
            </td>
        </tr>
    </table>