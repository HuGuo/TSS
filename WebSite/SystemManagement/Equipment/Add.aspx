<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Add.aspx.cs" Inherits="SystemManagement_Equipment_Add" MasterPageFile="~/Default.master" %>

<asp:Content ContentPlaceHolderID="body" runat="server">
  <table>
    <tr>
      <td>
        <asp:TreeView ID="TreeView1" runat="server" DataSourceID="XmlDataSource1">
          <DataBindings>
            <asp:TreeNodeBinding DataMember="Category" TextField="Name" ValueField="Id" />
          </DataBindings>
        </asp:TreeView>
      </td>
      <td style="vertical-align: top">
        设备类别：<%= TreeView1.SelectedNode != null ? TreeView1.SelectedNode.Text : string.Empty %>
        <br />
        归属专业：<asp:DropDownList ID="DropDownList1" runat="server" DataSourceID="ObjectDataSource1" DataTextField="Name" DataValueField="Id">
        </asp:DropDownList>
        <br />
        设备名称：<asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
        <br />
        设备编号：<asp:TextBox ID="TextBox2" runat="server"></asp:TextBox>
      </td>
    </tr>
    <tr>
      <td colspan="2" style="text-align: center">
        <asp:Button ID="addButton" runat="server" OnClick="addButton_Click" Text="添加" />
        <asp:HyperLink ID="HyperLink" runat="server" NavigateUrl="~/SystemManagement/Equipment/">返回</asp:HyperLink>
      </td>
    </tr>
  </table>
  <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" SelectMethod="GetAll" TypeName="TSS.BLL.Specialties"></asp:ObjectDataSource>
  <asp:XmlDataSource ID="XmlDataSource1" runat="server" EnableCaching="False" XPath="Categories/*"></asp:XmlDataSource>
</asp:Content>
