﻿<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Add.aspx.cs" Inherits="SystemManagement_Equipment_Category_Add" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:TreeView ID="TreeView1" runat="server" DataSourceID="XmlDataSource1">
        <DataBindings>
        <asp:TreeNodeBinding DataMember="Category" TextField="Name" ValueField="Id" />
        </DataBindings>
        </asp:TreeView>
        <asp:XmlDataSource ID="XmlDataSource1" runat="server" EnableCaching="false"></asp:XmlDataSource>
        <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
        <asp:Button ID="Button1" runat="server" onclick="Button1_Click" Text="Button" />
    </div>
    </form>
</body>
</html>
