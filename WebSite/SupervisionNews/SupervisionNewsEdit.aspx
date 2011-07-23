<%@ Page Title="" Language="C#" MasterPageFile="~/ValidateAndUI.master" AutoEventWireup="true" CodeFile="SupervisionNewsEdit.aspx.cs" Inherits="SupervisionNews_SupervisionNewsEdit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="body" Runat="Server">
<div id="toolbar" class="fixed">

</div>
<table>
<tr>
<td>标题</td>
<td><asp:TextBox runat="server" ID="tbTitle"></asp:TextBox></td>
</tr>
<tr>
<td colspan="2">内容</td>
</tr>
<tr>
<td colspan="2">

</td>
</tr>
<tr><td colspan="2"><asp:Button runat="server" ID="btnSave" Text="保存" 
        onclick="btnSave_Click" /><input type="button" value="取消" onclick="window.localtion.href='default.aspx'" /></td></tr>
</table>

</asp:Content>

