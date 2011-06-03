<%@ Page Language="C#" AutoEventWireup="true" Title="设备管理" MasterPageFile="~/Default.master" %>

<asp:Content ContentPlaceHolderID="body" runat="server">
  <a href="Add.aspx">添加设备</a> <a href="Category/Add.aspx">添加类别</a>
  <asp:ListView ID="ListView1" runat="server" ItemPlaceholderID="Placeholder1" DataSourceID="ObjectDataSource1" DataKeyNames="Id, EquipmentCategoryId">
    <LayoutTemplate>
      <table>
        <thead>
          <tr>
            <th>
              编号
            </th>
            <th>
              设备名称
            </th>
            <th>
              设备编号
            </th>
            <th>
              所属专业
            </th>
            <th>
            </th>
          </tr>
        </thead>
        <tbody>
          <asp:PlaceHolder ID="PlaceHolder1" runat="server"></asp:PlaceHolder>
        </tbody>
      </table>
    </LayoutTemplate>
    <ItemTemplate>
      <tr>
        <td>
          <%# Container.DataItemIndex + 1 %>
        </td>
        <td>
          <asp:LinkButton ID="edit" runat="server" CommandName="Edit" Text='<%# Eval("Name") %>'></asp:LinkButton>
        </td>
        <td>
          <%# Eval("Code") %>
        </td>
        <td>
          <%# Eval("Specialty.Name") %>
        </td>
        <td>
          <a href="<%# Eval("Id", "Detail/?id={0}") %>">详情</a>
        </td>
      </tr>
    </ItemTemplate>
    <EditItemTemplate>
      <tr>
        <td>
        </td>
        <td>
          <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("Name") %>' />
        </td>
        <td>
          <asp:TextBox ID="TextBox2" runat="server" Text='<%# Bind("Code") %>' />
        </td>
        <td>
          <asp:DropDownList ID="DropDownList1" runat="server" DataSourceID="ObjectDataSource2" DataTextField="Name" DataValueField="Id" SelectedValue='<%# Bind("SpecialtyId") %>' />
        </td>
        <td>
          <asp:Button ID="update" runat="server" CommandName="Update" Text="保存" />
        </td>
      </tr>
    </EditItemTemplate>
  </asp:ListView>
  <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" SelectMethod="GetAllWithCategoryAndSpecialty" TypeName="TSS.BLL.Equipments" DataObjectTypeName="TSS.Models.Equipment" UpdateMethod="Update"></asp:ObjectDataSource>
  <asp:ObjectDataSource ID="ObjectDataSource2" runat="server" SelectMethod="GetAll" TypeName="TSS.BLL.Specialties"></asp:ObjectDataSource>
</asp:Content>
