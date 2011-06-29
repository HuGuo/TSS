<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AddIndicator.aspx.cs" MasterPageFile="~/ValidateAndUi.master"
    Inherits="ComprehensiveReport_AddIndicator" %>

<%@ Register Src="../UserControl/SpecialtyControl.ascx" TagName="SpecialtyControl"
    TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript">
        $().ready(function () {
            $("#<%=Page.Form.UniqueID %>").validate();
        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="Server">
    <div id="toolbar" class="fixed">
        <a href="Indicator.aspx?specialtyId=<%= Request.QueryString["specialtyId"] %>">返回</a>
    </div>
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
        <tr>
            <td colspan="2">
                <asp:Button ID="btnAdd" runat="server" Text="添加" CssClass="btn" OnClick="btnAdd_Click" />
            </td>
        </tr>
    </table>
</asp:Content>
