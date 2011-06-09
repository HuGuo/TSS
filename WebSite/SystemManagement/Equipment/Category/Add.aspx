<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Add.aspx.cs" Inherits="SystemManagement_Equipment_Category_Add" MasterPageFile="~/Default.master" %>

<asp:Content ContentPlaceHolderID="body" runat="server">
  <table>
    <tr>
      <td>
        父类别：<%= TreeView1.SelectedNode == null ? string.Empty : TreeView1.SelectedNode.Text %>
        <asp:TreeView ID="TreeView1" runat="server" DataSourceID="XmlDataSource1">
          <DataBindings>
            <asp:TreeNodeBinding DataMember="Category" TextField="Name" ValueField="Id" />
          </DataBindings>
        </asp:TreeView>
      </td>
      <td style="vertical-align: top">
        <asp:Label ID="Label1" runat="server" AssociatedControlID="TextBox1">类别名称</asp:Label>
        <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
      </td>
    </tr>
    <tr>
      <td colspan="2" style="text-align: center">
        <asp:Button ID="addButton" runat="server" OnClick="addButton_Click" Text="添加" />
        <asp:HyperLink ID="HyperLink" runat="server" NavigateUrl="~/SystemManagement/Equipment/">返回</asp:HyperLink>
      </td>
    </tr>
  </table>
  <asp:XmlDataSource ID="XmlDataSource1" runat="server" EnableCaching="false" XPath="Categories/*"></asp:XmlDataSource>
</asp:Content>
