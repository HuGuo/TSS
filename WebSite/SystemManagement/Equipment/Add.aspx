<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Add.aspx.cs" Inherits="SystemManagement_Equipment_Add" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
        <asp:DropDownList ID="DropDownList1" runat="server" 
            DataSourceID="LinqDataSource1" DataTextField="Name" DataValueField="Id">
        </asp:DropDownList>
        <asp:LinqDataSource ID="LinqDataSource1" runat="server" 
            ContextTypeName="TSS.Models.Context" EntityTypeName="" Select="new (Id, Name)" 
            TableName="Specitalties">
        </asp:LinqDataSource>
    
        <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
        <asp:Button ID="addButton" runat="server" onclick="addButton_Click" Text="添加" />
    
    </div>
    </form>
</body>
</html>
