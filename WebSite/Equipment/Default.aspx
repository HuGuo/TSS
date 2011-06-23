<%@ Page Title="设备台帐" Language="C#" MasterPageFile="~/Default.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Equipment_Default" %>

<asp:Content ContentPlaceHolderID="head" runat="server">
  <link href="<%= ResolveUrl("~/Scripts/jquery-easyui/themes/gray/easyui.css") %>" rel="Stylesheet" type="text/css" />
  <script type="text/javascript" src="<%= ResolveUrl("~/Scripts/jquery-easyui/jquery.easyui.min.js") %>"></script>
  <script type="text/javascript" src="<%= ResolveUrl("~/Scripts/common.js") %>"></script>
  <script type="text/javascript">
    $(function () {
      common.initLayout("main", "left", "right", "设备类别");

      var url = '<%= ResolveUrl("~/EquipmentCategory.ashx") %>' + "?type=xml&src=Default.aspx" +
        escape('?s=<%= Request.QueryString["s"] %>&category=');
      common.initCategoryTree("categoryTree", url, '<%= Request.QueryString["category"] %>');
    });
  </script>
  <style type="text/css">
    #left {
      width: 200px;
      max-width: 300px;
      padding: 5px;
    }
    
    #right {
      width: 100%;
      padding: 0;
    }
  </style>
</asp:Content>
<asp:Content ContentPlaceHolderID="body" runat="Server">
  <div id="main">
    <div id="left">
      <ul id="categoryTree">
      </ul>
    </div>
    <div id="right">
      <asp:ListView ID="ListView1" runat="server" ItemPlaceholderID="PlaceHolder1" DataSourceID="ObjectDataSource1">
        <LayoutTemplate>
          <table>
            <thead>
              <tr>
                <th>
                  设备名称
                </th>
                <th>
                  设备编号
                </th>
                <th>
                  生产厂家
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
            <td>
            <%# Eval("Code") %>
            </td>
            <td>
            <%# Helper.GetEquipmentField(Eval("Id"), "生产厂家") %>
            </td>
          </tr>
        </ItemTemplate>
        <EmptyDataTemplate>
          <%= Helper.EmptyData %>
        </EmptyDataTemplate>
      </asp:ListView>
    </div>
  </div>
  <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" SelectMethod="GetList" TypeName="TSS.BLL.Equipments">
    <SelectParameters>
      <asp:QueryStringParameter Name="categoryId" QueryStringField="category" Type="String" />
      <asp:QueryStringParameter Name="specialtyId" QueryStringField="s" Type="String" />
    </SelectParameters>
  </asp:ObjectDataSource>
</asp:Content>
