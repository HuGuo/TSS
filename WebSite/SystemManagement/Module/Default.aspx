<%@ Page Title="" Language="C#" MasterPageFile="~/Default.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="SystemManagement_Module_Default" %>

<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="Server">
  <asp:ScriptManager ID="ScriptManager1" runat="server" />
  <asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
      <div id="content">
        <asp:ListView ID="ListView1" runat="server" DataSourceID="ObjectDataSource1" DataKeyNames="Id" InsertItemPosition="LastItem">
          <LayoutTemplate>
            <table>
              <thead>
                <tr>
                  <th>
                    模块编号
                  </th>
                  <th>
                    模块名称
                  </th>
                  <th>
                    模块路径
                  </th>
                  <th>
                    父模块编号
                  </th>
                  <th>
                    操作
                  </th>
                </tr>
              </thead>
              <tbody>
                <asp:PlaceHolder ID="ItemPlaceHolder" runat="server" />
              </tbody>
            </table>
          </LayoutTemplate>
          <ItemTemplate>
            <tr>
              <td>
                <%# Eval("Id") %>
              </td>
              <td>
                <%# modules.GetFullName(Convert.ToInt32(Eval("Id"))) %>
              </td>
              <td>
                <%# modules.GetFullPath(Convert.ToInt32(Eval("Id"))) %>
              </td>
              <td>
                <%# Eval("ParentModuleId") %>
              </td>
              <td>
                <asp:LinkButton ID="LinkButton1" runat="server" CommandName="Edit" Text="编辑" />
              </td>
            </tr>
          </ItemTemplate>
          <EditItemTemplate>
            <tr>
              <td>
                <%# Eval("Id") %>
              </td>
              <td>
                <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("Name") %>' />
              </td>
              <td>
                <asp:TextBox ID="TextBox2" runat="server" Text='<%# Bind("Path") %>' />
              </td>
              <td>
                <asp:TextBox ID="EditParentModuleTextBox" runat="server" ClientIDMode="Static" Text='<%# Bind("ParentModuleId") %>' />
              </td>
              <td>
                <asp:LinkButton ID="LinkButton1" runat="server" CommandName="Update" Text="保存" />
              </td>
            </tr>
          </EditItemTemplate>
          <InsertItemTemplate>
            <td>
            </td>
            <td>
              <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("Name") %>' />
            </td>
            <td>
              <asp:TextBox ID="TextBox2" runat="server" Text='<%# Bind("Path") %>' />
            </td>
            <td>
              <asp:TextBox ID="TextBox3" runat="server" Text='<%# Bind("ParentModuleId") %>' />
            </td>
            <td>
              <asp:LinkButton ID="LinkButton1" runat="server" CommandName="Insert" Text="添加" />
            </td>
          </InsertItemTemplate>
        </asp:ListView>
      </div>
    </ContentTemplate>
  </asp:UpdatePanel>
  <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" SelectMethod="GetAllWithOrder" UpdateMethod="Update" InsertMethod="Add" TypeName="TSS.BLL.Modules" DataObjectTypeName="TSS.Models.Module"></asp:ObjectDataSource>
</asp:Content>
