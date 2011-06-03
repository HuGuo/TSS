<%@ Page Language="C#" AutoEventWireup="true" Title="�豸����" MasterPageFile="~/Default.master" %>

<asp:Content ContentPlaceHolderID="body" runat="server">
  <a href="Add.aspx">����豸</a> <a href="Category/Add.aspx">������</a>
  <asp:ListView ID="ListView1" runat="server" ItemPlaceholderID="Placeholder1" DataSourceID="ObjectDataSource1" DataKeyNames="Id, EquipmentCategoryId">
    <LayoutTemplate>
      <table>
        <thead>
          <tr>
            <th>
              ���
            </th>
            <th>
              �豸����
            </th>
            <th>
              �豸���
            </th>
            <th>
              ����רҵ
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
          <a href="<%# Eval("Id", "Detail/?id={0}") %>">����</a>
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
          <asp:Button ID="update" runat="server" CommandName="Update" Text="����" />
        </td>
      </tr>
    </EditItemTemplate>
  </asp:ListView>
  <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" SelectMethod="GetAllWithCategoryAndSpecialty" TypeName="TSS.BLL.Equipments" DataObjectTypeName="TSS.Models.Equipment" UpdateMethod="Update"></asp:ObjectDataSource>
  <asp:ObjectDataSource ID="ObjectDataSource2" runat="server" SelectMethod="GetAll" TypeName="TSS.BLL.Specialties"></asp:ObjectDataSource>
</asp:Content>
