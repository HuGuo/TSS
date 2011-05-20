<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Add.aspx.cs" Inherits="SystemManagement_Equipment_Add" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        设备目录<asp:TreeView ID="TreeView1" runat="server" DataSourceID="XmlDataSource1">
            <DataBindings>
                <asp:TreeNodeBinding DataMember="Category" TextField="Name" ValueField="Id" />
            </DataBindings>
        </asp:TreeView>
        专业<asp:DropDownList ID="DropDownList1" runat="server" DataSourceID="ObjectDataSource1"
            DataTextField="Name" DataValueField="Id">
        </asp:DropDownList>
        <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
        <asp:Button ID="addButton" runat="server" OnClick="addButton_Click" Text="添加" />
        <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" SelectMethod="GetAll"
            TypeName="TSS.BLL.Specialty"></asp:ObjectDataSource>
        <asp:XmlDataSource ID="XmlDataSource1" runat="server" EnableCaching="False"></asp:XmlDataSource>
    </div>
    </form>
</body>
</html>
