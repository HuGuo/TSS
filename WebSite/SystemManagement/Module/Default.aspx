<%@ Page Title="" Language="C#" MasterPageFile="~/Default.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="SystemManagement_Module_Default" %>

<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="Server">
  <asp:ScriptManager ID="ScriptManager1" runat="server" />
  <div id="toolbar">
  </div>
  <div id="content">
  <asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
    </ContentTemplate>
  </asp:UpdatePanel>
  </div>
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
