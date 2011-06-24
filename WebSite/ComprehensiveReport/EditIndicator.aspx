<%@ Page Title="" Language="C#" MasterPageFile="~/ValidateAndUi.master" AutoEventWireup="true"
    CodeFile="EditIndicator.aspx.cs" Inherits="ComprehensiveReport_EditIndicator" %>

<%@ Register Src="../UserControl/SpecialtyControl.ascx" TagName="SpecialtyControl"
    TagPrefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="Server">
    <script type="text/javascript">
        $().ready(function () {
            $("#<%=Page.Form.UniqueID %>").validate();
        });
    </script>
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
                <uc2:SpecialtyControl ID="SpecialtyControl1" runat="server" />
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <asp:Button ID="btnEdit" runat="server" Text="编辑" CssClass="btn" OnClick="btnEdit_Click" />
            </td>
        </tr>
    </table>
</asp:Content>
