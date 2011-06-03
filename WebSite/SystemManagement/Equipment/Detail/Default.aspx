<%@ Page Title="" Language="C#" MasterPageFile="~/Default.master" %>

<script runat="server">

</script>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="Server">
  <asp:ListView ID="ListView1" runat="server" InsertItemPosition="LastItem" DataSourceID="ObjectDataSource1" ItemPlaceholderID="PlaceHolder1" DataKeyNames="EquipmentId">
    <LayoutTemplate>
      <table>
        <thead>
          <tr>
            <th>
              名称
            </th>
            <th>
              值
            </th>
            <th>
            </th>
        </thead>
        <tbody>
          <asp:PlaceHolder ID="PlaceHolder1" runat="server" />
        </tbody>
      </table>
    </LayoutTemplate>
    <ItemTemplate>
      <tr>
        <td>
          <%# Eval("Lable") %>
        </td>
        <td>
          <%# Eval("Value") %>
        </td>
        <td>
        </td>
      </tr>
    </ItemTemplate>
    <InsertItemTemplate>
      <tr>
        <td>
          <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("Lable") %>' />
        </td>
        <td>
          <asp:TextBox ID="TextBox2" runat="server" Text='<%# Bind("Value") %>' />
        </td>
        <td>
          <asp:Button ID="Button1" runat="server" CommandName="Insert" Text="添加" />
        </td>
      </tr>
    </InsertItemTemplate>
    <EmptyDataTemplate>
      <%= Helper.EmptyData %>
    </EmptyDataTemplate>
  </asp:ListView>
  <asp:HyperLink ID="HyperLink" runat="server" NavigateUrl="~/SystemManagement/Equipment/">返回</asp:HyperLink>
  <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" SelectMethod="GetAllByEquipment" TypeName="TSS.BLL.EquipmentDetails" InsertMethod="Add">
    <InsertParameters>
      <asp:QueryStringParameter Name="equipmentId" QueryStringField="id" Type="String" />
      <asp:Parameter Name="lable" Type="String" />
      <asp:Parameter Name="value" Type="String" />
    </InsertParameters>
    <SelectParameters>
      <asp:QueryStringParameter Name="equipmentId" QueryStringField="id" Type="String" />
    </SelectParameters>
  </asp:ObjectDataSource>
</asp:Content>
