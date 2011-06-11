<%@ Page Title="" Language="C#" MasterPageFile="~/Default.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="SystemManagement_Module_Default" %>

<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="Server">
  <asp:ScriptManager ID="ScriptManager1" runat="server" />
  <asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
      <table>
        <tr>
          <td colspan="3">
            <asp:DropDownList ID="DropDownList1" runat="server" DataSourceID="ObjectDataSource1" DataTextField="Name" DataValueField="Id" AutoPostBack="True">
            </asp:DropDownList>
          </td>
        </tr>
        <tr>
          <td rowspan="2">
            <asp:ListBox ID="ListBox1" runat="server" DataSourceID="ObjectDataSource2" DataTextField="Name" DataValueField="Id"></asp:ListBox>
          </td>
          <td>
            <asp:Button ID="RemoveButton" runat="server" Text=">" OnClick="RemoveButton_Click" />
          </td>
          <td rowspan="2">
            <asp:ListBox ID="ListBox2" runat="server" DataSourceID="ObjectDataSource3" DataTextField="Name" DataValueField="Id"></asp:ListBox>
          </td>
        </tr>
        <tr>
          <td>
            <asp:Button ID="AddButton" runat="server" Text="<" OnClick="AddButton_Click" />
          </td>
        </tr>
      </table>
    </ContentTemplate>
  </asp:UpdatePanel>
  <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" SelectMethod="GetAll" TypeName="TSS.BLL.Specialties"></asp:ObjectDataSource>
  <asp:ObjectDataSource ID="ObjectDataSource2" runat="server" SelectMethod="GetModules" TypeName="TSS.BLL.Specialties">
    <SelectParameters>
      <asp:ControlParameter ControlID="DropDownList1" Name="specialtyId" PropertyName="SelectedValue" Type="String" />
    </SelectParameters>
  </asp:ObjectDataSource>
  <asp:ObjectDataSource ID="ObjectDataSource3" runat="server" SelectMethod="GetNotHasModules" TypeName="TSS.BLL.Specialties">
    <SelectParameters>
      <asp:ControlParameter ControlID="DropDownList1" Name="spcialtyId" PropertyName="SelectedValue" Type="String" />
    </SelectParameters>
  </asp:ObjectDataSource>
</asp:Content>
