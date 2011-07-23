<%@ Page Title="" Language="C#" MasterPageFile="~/Default.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="SystemManagement_Module_Default" %>

<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="Server">
  <asp:ScriptManager ID="ScriptManager1" runat="server" />
  <div id="toolbar">
  </div>
  <div id="content">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
      <ContentTemplate>
        <asp:ListView ID="ListView1" runat="server" DataSourceID="ObjectDataSource1">
          <LayoutTemplate>
            <ul>
              <asp:PlaceHolder ID="ItemPlaceHolder" runat="server" />
            </ul>
          </LayoutTemplate>
          <ItemTemplate>
            <li>
              <ul>
                <li>
                  <%# modules.GetFullName(Convert.ToInt32(Eval("Id")))  %>
                </li>
                <li>
                  <%# modules.GetFullPath(Convert.ToInt32(Eval("Id"))) %>
                </li>
              </ul>
            </li>
          </ItemTemplate>
        </asp:ListView>
      </ContentTemplate>
    </asp:UpdatePanel>
    <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" SelectMethod="GetAll" TypeName="TSS.BLL.Modules"></asp:ObjectDataSource>
  </div>
</asp:Content>
