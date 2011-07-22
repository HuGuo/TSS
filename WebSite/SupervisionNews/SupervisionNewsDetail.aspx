<%@ Page Title="" Language="C#" MasterPageFile="~/ValidateAndUI.master" AutoEventWireup="true" CodeFile="SupervisionNewsDetail.aspx.cs" Inherits="SupervisionNews_SupervisionNewsDetail" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="body" Runat="Server">
<table>
<tr><td><asp:Label runat="server" ID="lbTitle"></asp:Label></td></tr>
<tr><td><asp:Label runat="server" ID="lbReleaseTime"></asp:Label></td></tr>
<tr><td>
<asp:Panel runat="server" ID="plContent"></asp:Panel>
</td></tr>
</table>
</asp:Content>

