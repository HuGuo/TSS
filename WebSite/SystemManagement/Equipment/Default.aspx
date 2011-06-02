<%@ Page Language="C#" AutoEventWireup="true" Title="设备管理" MasterPageFile="~/HTML5.master" %>

<asp:Content ContentPlaceHolderID="Main" runat="server">
abc
  <asp:ListView ID="ListView1" runat="server" DataSourceID="ObjectDataSource1" DataKeyNames="Id">
    <LayoutTemplate>
      <ul>
        <asp:PlaceHolder ID="itemPlaceholder" runat="server"></asp:PlaceHolder>
      </ul>
    </LayoutTemplate>
    <ItemTemplate>
      <li>
        <asp:LinkButton ID="edit" runat="server" CommandName="Edit" Text='<%# Eval("Name") %>'></asp:LinkButton>
      </li>
      <li>
        <%# new TSS.BLL.Specialties().Get(Eval("SpecialtyId").ToString()).Name %>
      </li>
    </ItemTemplate>
    <EditItemTemplate>
      <li>
        <asp:TextBox ID="txt" runat="server" Text='<%# Bind("Name") %>'></asp:TextBox>
        <asp:Button ID="update" runat="server" CommandName="Update" Text="保存" />
      </li>
      <li>
        <asp:DropDownList ID="ddl" runat="server" DataSourceID="ddlo" DataTextField="Name" DataValueField="Id" SelectedValue='<%# Bind("SpecialtyId") %>'>
        </asp:DropDownList>
      </li>
    </EditItemTemplate>
  </asp:ListView>
  <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" SelectMethod="GetAll" TypeName="TSS.BLL.Equipments" DataObjectTypeName="TSS.Models.Equipment" UpdateMethod="Update"></asp:ObjectDataSource>
  <asp:ObjectDataSource ID="ddlo" runat="server" SelectMethod="GetAll" TypeName="TSS.BLL.Specialty"></asp:ObjectDataSource>
</asp:Content>
