<%@ Page Title="" Language="C#" MasterPageFile="~/Default.master" AutoEventWireup="true" CodeFile="List.aspx.cs" Inherits="Equipment_List" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="Server">
  <asp:ListView ID="ListView1" runat="server" ItemPlaceholderID="PlaceHolder1" DataSourceID="ObjectDataSource1">
    <LayoutTemplate>
      <table>
        <thead>
          <tr>
            <th>
              设备名称
            </th>
          </tr>
        </thead>
        <tbody>
          <asp:PlaceHolder ID="PlaceHolder1" runat="server" />
        </tbody>
      </table>
    </LayoutTemplate>
    <ItemTemplate>
      <tr>
        <td>
          <%# Eval("Name") %>
        </td>
      </tr>
      <tr>
        <td>
          <asp:ListView ID="ListBox2" runat="server" ItemPlaceholderID="PlaceHolder2" DataSource='<%# Eval("EquipmentDetails") %>'>
            <LayoutTemplate>

              <ul>
                <asp:PlaceHolder ID="PlaceHolder2" runat="server" />
              </ul>
            </LayoutTemplate>
            <ItemTemplate>
              <li>
                <%# Eval("Lable") %>:
                <%# Eval("Value") %></li>
            </ItemTemplate>
          </asp:ListView>
        </td>
      </tr>
    </ItemTemplate>
    <EmptyDataTemplate>
      <%= Helper.EmptyData %>
    </EmptyDataTemplate>
  </asp:ListView>
  <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" SelectMethod="GetAllByCategoryAndSpecialtyWithDetails" TypeName="TSS.BLL.Equipments">
    <SelectParameters>
      <asp:QueryStringParameter Name="categoryId" QueryStringField="category" Type="String" />
      <asp:QueryStringParameter Name="specialtyId" QueryStringField="s" Type="String" />
    </SelectParameters>
  </asp:ObjectDataSource>
</asp:Content>
