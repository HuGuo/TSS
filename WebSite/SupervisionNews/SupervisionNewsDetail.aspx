<%@ Page Title="" Language="C#" MasterPageFile="~/ValidateAndUI.master" AutoEventWireup="true" CodeFile="SupervisionNewsDetail.aspx.cs" Inherits="SupervisionNews_SupervisionNewsDetail" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="body" Runat="Server">
<table style="text-align:center;width:100%">
<tr><td><asp:Label runat="server" ID="lbTitle"></asp:Label></td></tr>
<tr><td style="text-align:right;">
作者：<asp:Label runat="server" ID="lbAuthor"></asp:Label>&nbsp;&nbsp;
发布时间：<asp:Label runat="server" ID="lbReleaseTime"></asp:Label></td></tr>
<tr><td>
<div runat="server" style="text-align:left;" ID="plContent"></div>
</td></tr>
</table>
</asp:Content>

