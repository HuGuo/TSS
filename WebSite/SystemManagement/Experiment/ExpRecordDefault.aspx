<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ExpRecordDefault.aspx.cs" Inherits="SystemManagement_Experiment_ExpRecordDefault" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>试验台帐</title>
    <link href="../../Styles/_base.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server" style=" padding-top:32px;">
    <div id="toolbar" class="fixed">
    <a href="Default.aspx">实验报告</a>
    </div>
    <table>
    <thead>
    <tr>
    <th></th>
        <th>专业
        </th>
        <th>名称
        </th>
        <th>
        </th>
    </tr>
        <asp:Repeater runat="server" ID="rptlist">
            <ItemTemplate>
            <tr>
            <td><%#Container.ItemIndex+1 %></td>
            <td><%# ((TSS.Models.Specialty)(Eval("Specialty"))).Name %></td>
            <td><%#Eval("name") %></td>
            <td><a href="SetExpRecord.aspx?id=<%#Eval("Id") %>">编辑</a></td>
            </tr>
            </ItemTemplate>
        </asp:Repeater>
    </thead>
    </table>
    </form>
</body>
</html>
