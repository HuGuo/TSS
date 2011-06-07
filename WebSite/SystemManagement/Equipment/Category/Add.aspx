<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Add.aspx.cs" Inherits="SystemManagement_Equipment_Category_Add" MasterPageFile="~/Default.master" %>

<asp:Content ContentPlaceHolderID="body" runat="server">
  <asp:TreeView ID="TreeView1" runat="server" DataSourceID="XmlDataSource1">
    <DataBindings>
      <asp:TreeNodeBinding DataMember="Category" TextField="Name" ValueField="Id" />
    </DataBindings>
  </asp:TreeView>
  <asp:XmlDataSource ID="XmlDataSource1" runat="server" EnableCaching="false" XPath="Categories/*"></asp:XmlDataSource>
  <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
  <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="Button" />
</asp:Content>
