<%@ Page Title="设备管理" Language="C#" MasterPageFile="~/Default.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="SystemManagement_Equipment_Default" %>

<asp:Content ContentPlaceHolderID="head" runat="Server">
  <link href="<%= ResolveUrl("~/Scripts/jquery-easyui/themes/gray/easyui.css") %>" rel="Stylesheet" type="text/css" />
  <script type="text/javascript" src="<%= ResolveUrl("~/Scripts/jquery-easyui/jquery.easyui.min.js") %>"></script>
  <script type="text/javascript" src="<%= ResolveUrl("~/Scripts/common.js") %>"></script>
  <script type="text/javascript">
    $(function () {
      common.initLayout("main", "right", "left", "设备类别");

      var url = '<%= ResolveUrl("~/EquipmentCategory.ashx?type=xml&src=Default.aspx?category=") %>';
      common.initCategoryTree("categoryTree", url, '<%= Request.QueryString["category"] %>');

      initDialogs();

      Sys.Application.add_load(function (sender, args) {
        attachComboTrees();
      });
    });

    function attachComboTrees() {
      var jsonUrl = '<%= ResolveUrl("~/EquipmentCategory.ashx?type=json") %>';

      $(".combo").remove();
      $(".combo-panel").parent().remove()
      $("#CategoryTextBox, #EditCategoryTextBox").removeData("combo").removeData("combotree")
        .combotree({ url: jsonUrl });
    }

    function initDialogs() {
      $("#AddEquipmentDialog, #DetailDialog").hide();
    }

    function openDialog(id, title) {
      var e = $("#" + id);
      if (e.data("dialog")) {
        e.dialog("open");
      }
      else {
        $(".window-mask").remove();
        var parent = e.parent();
        e.show().dialog({ title: title, modal: true, shadow: false }).parent().appendTo(parent);
      }
    }

    function initLayout() {
      
    }

    
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
    
    .dialog {
      padding: 10px;
    }
  </style>
</asp:Content>
<asp:Content ContentPlaceHolderID="body" runat="Server">
  <asp:ScriptManager ID="ScriptManager1" runat="server">
  </asp:ScriptManager>
  <div id="main">
    <div id="left">
      <a href="Default.aspx">所有分类</a>
      <ul id="categoryTree">
      </ul>
    </div>
    <div id="right">
      <a href="#" onclick="openDialog('AddEquipmentDialog', $(this).text())">添加设备</a>
      <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
          <asp:ListView ID="EquipmentListView" runat="server" ItemPlaceholderID="Placeholder1" DataSourceID="EquipmentDataSource" DataKeyNames="Id, EquipmentCategoryId" OnSelectedIndexChanged="EquipmentListView_SelectedIndexChanged">
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
                      设备分类
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
                  <%# Eval("EquipmentCategory.Name") %>
                </td>
                <td>
                  <asp:LinkButton ID="ShowDetail" runat="server" CommandName="Select" ClientIDMode="Static">详情</asp:LinkButton>
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
                  <asp:DropDownList ID="DropDownList1" runat="server" DataSourceID="SpecialtyDataSource" DataTextField="Name" DataValueField="Id" SelectedValue='<%# Bind("SpecialtyId") %>' />
                </td>
                <td>
                  <asp:TextBox ID="EditCategoryTextBox" runat="server" ClientIDMode="Static" Text='<%# Bind("EquipmentCategoryId") %>' />
                </td>
                <td>
                  <asp:Button ID="update" runat="server" CommandName="Update" Text="保存" />
                </td>
              </tr>
            </EditItemTemplate>
          </asp:ListView>
          <div id="DetailDialog" class="dialog">
            <asp:ListView ID="DetailListView" runat="server" InsertItemPosition="LastItem" DataSourceID="DetailDataSource" ItemPlaceholderID="PlaceHolder1" DataKeyNames="EquipmentId" OnItemInserted="DetailListView_ItemInserted">
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
                    <asp:Button ID="AddDetail" runat="server" CommandName="Insert" ClientIDMode="Static" Text="添加" />
                  </td>
                </tr>
              </InsertItemTemplate>
              <EmptyDataTemplate>
                <%= Helper.EmptyData %>
              </EmptyDataTemplate>
            </asp:ListView>
          </div>
        </ContentTemplate>
      </asp:UpdatePanel>
      <div id="AddEquipmentDialog" class="dialog">
        <table>
          <tr>
            <th>
              设备类别
            </th>
            <td>
              <asp:TextBox ID="CategoryTextBox" runat="server" ClientIDMode="Static"></asp:TextBox>
            </td>
          </tr>
          <tr>
            <th>
              归属专业
            </th>
            <td>
              <asp:DropDownList ID="SpecialtyDropDownList" runat="server" DataSourceID="SpecialtyDataSource" DataTextField="Name" DataValueField="Id" />
            </td>
          </tr>
          <tr>
            <th>
              设备名称
            </th>
            <td>
              <asp:TextBox ID="NameTextBox" runat="server"></asp:TextBox>
            </td>
          </tr>
          <tr>
            <th>
              设备编号
            </th>
            <td>
              <asp:TextBox ID="CodeTextBox" runat="server"></asp:TextBox>
            </td>
          </tr>
          <tr>
            <td colspan="2" style="text-align: center">
              <asp:Button ID="AddEquipmentButton" runat="server" Text="添加" OnClick="AddEquipmentButton_Click" />
            </td>
          </tr>
        </table>
      </div>
    </div>
  </div>
  <asp:ObjectDataSource ID="EquipmentDataSource" runat="server" SelectMethod="GetList" TypeName="TSS.BLL.Equipments" DataObjectTypeName="TSS.Models.Equipment" UpdateMethod="Update" InsertMethod="Add">
    <SelectParameters>
      <asp:QueryStringParameter Name="categoryId" QueryStringField="category" Type="String" />
    </SelectParameters>
  </asp:ObjectDataSource>
  <asp:ObjectDataSource ID="DetailDataSource" runat="server" SelectMethod="GetList" TypeName="TSS.BLL.EquipmentDetails" InsertMethod="Add">
    <SelectParameters>
      <asp:ControlParameter Name="equipmentId" ControlID="EquipmentListView" PropertyName="SelectedDataKey[0]" />
    </SelectParameters>
    <InsertParameters>
      <asp:ControlParameter Name="equipmentId" ControlID="EquipmentListView" PropertyName="SelectedDataKey[0]" />
      <asp:Parameter Name="lable" Type="String" />
      <asp:Parameter Name="value" Type="String" />
    </InsertParameters>
  </asp:ObjectDataSource>
  <asp:ObjectDataSource ID="SpecialtyDataSource" runat="server" SelectMethod="GetAll" TypeName="TSS.BLL.Specialties"></asp:ObjectDataSource>
</asp:Content>
