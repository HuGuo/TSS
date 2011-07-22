<%@ Page Title="设备管理" Language="C#" MasterPageFile="~/Default.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="SystemManagement_Equipment_Default" %>

<asp:Content ContentPlaceHolderID="head" runat="Server">
  <link href="<%= ResolveUrl("~/Scripts/jquery-easyui/themes/gray/easyui.css") %>" rel="Stylesheet" type="text/css" />
  <link href="<%= ResolveUrl("~/Scripts/jquery-easyui/themes/icon.css") %>" rel="Stylesheet" type="text/css" />
  <script type="text/javascript" src="<%= ResolveUrl("~/Scripts/jquery-easyui/jquery.easyui.min.js") %>"></script>
  <script type="text/javascript" src="<%= ResolveUrl("~/Scripts/common.js") %>"></script>
  <script type="text/javascript">
    var toJson;
    $(function () {
      var toJson = Sys.Serialization.JavaScriptSerializer.serialize;

      common.initLayout("main", "left", "right", "设备类别", "设备列表");

      initToolbars();

      var url = '<%= ResolveUrl("~/EquipmentCategory.ashx?type=xml&src=Default.aspx?category=") %>';
      common.initCategoryTree("categoryTree", url, '<%= Request.QueryString["category"] %>', renameCategory);

      Sys.Application.add_load(function (sender, args) {
        initDialogs();
        attachComboTrees();
      });
    });

    function initToolbars() {
      var nav = $("#left");
      if (nav.data("panel")) {
        nav.panel({
          tools: [
          { iconCls: 'icon-add', handler: addCategory },
          { iconCls: 'icon-remove', handler: removeCategory }
          ]
        });
      }

      var content = $("#right");
      if (content.data("panel")) {
        content.panel({
          tools: [
          { iconCls: "icon-add", handler: function () {
            openDialog("AddEquipmentDialog", "添加设备");
          }
          }]
        });
      }
    }

    function initDialogs() {
      var dialogs = $("#AddEquipmentDialog, #AddCategoryDialog, #DetailDialog");
      dialogs.each(function () {
        if (!$(this).data("dialog")) {
          $(this).hide();
        }
      });
    }

    function attachComboTrees() {
      var url = '<%= ResolveUrl("~/EquipmentCategory.ashx?type=json") %>';

      $(".combo").remove();
      $(".combo-panel").parent().remove()
      $("#EquipmentCategoryTextBox, #ParentCategoryTextBox, #EditCategoryTextBox").removeData("combo").removeData("combotree")
        .combotree({ url: url });
    }

    function addCategory() {
      openDialog("AddCategoryDialog", "添加类别");
    }

    function removeCategory() {
      var categoryTree = $("#categoryTree");
      if (categoryTree.data("tree")) {
        var selected = categoryTree.tree("getSelected");
        if (selected) {
          ajax("RemoveCategory", toJson({ id: selected.id }), function (msg) {
            switch (msg.d.toUpperCase()) {
              case "COMPLETED":
                location.href = "Default.aspx";
                break;
              case "NOT_EMPTY":
                $.messager.alert(null, "类别下存在内容", "error");
                break;
            }
          });
        } else {
          $.messager.alert(null, "请选择要删除的类别", "warning");
        }
      }
    }

    function renameCategory(category) {
      var categoryTreeData = $("#categoryTree").data("tree");
      if (categoryTreeData) {
        var a;
        categoryTreeData.options.onBeforeEdit = function (node) {
          a = node.text;
          node.text = $(a).text();
        };
        categoryTreeData.options.onAfterEdit = function (node) {
          ajax("RenameCategory", toJson({ id: node.id, name: node.text }), function (msg) {
            if (msg.d.toUpperCase() == "COMPLETED") {
              var text = node.text;
              node.text = $(a).text(text)[0];
            } else {
              node.text = a;
            }
            $("#categoryTree").tree("update", node);
          });
        }

        $("#categoryTree").tree("beginEdit", category.target);
      }
    }

    function openDialog(id, title) {
      $(".window-mask").remove();
      var e = $("#" + id);
      if (e.data("dialog")) {
        e.dialog("open");
      }
      else {
        var parent = e.parent();
        e.show().dialog({ title: title, modal: true, shadow: false, width: 320 }).parent().appendTo(parent);
      }
    }

    function ajax(method, params, callback) {
      $.ajax({
        type: "POST",
        url: "Default.aspx/" + method,
        data: params,
        dataType: "json",
        contentType: "application/json; charset=UTF-8",
        success: callback,
        error: function (xhr) {
          $.messager.alert(method, xhr.status, "error");
        }
      });
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
  </style>
</asp:Content>
<asp:Content ContentPlaceHolderID="body" runat="Server">
  <asp:ScriptManager ID="ScriptManager1" runat="server">
  </asp:ScriptManager>
  <div id="main">
    <div id="left">
      <a href="Default.aspx">全部</a>
      <ul id="categoryTree">
      </ul>
    </div>
    <div id="right">
      <div id="content">
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
          <ContentTemplate>
            <asp:ListView ID="EquipmentListView" runat="server" DataSourceID="EquipmentDataSource" DataKeyNames="Id, EquipmentCategoryId" OnSelectedIndexChanged="EquipmentListView_SelectedIndexChanged">
              <LayoutTemplate>
                <table class="list">
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
                        操作
                      </th>
                    </tr>
                  </thead>
                  <tbody>
                    <asp:PlaceHolder ID="ItemPlaceHolder" runat="server"></asp:PlaceHolder>
                  </tbody>
                </table>
              </LayoutTemplate>
              <ItemTemplate>
                <tr>
                  <td>
                    <%# Container.DataItemIndex + 1 %>
                  </td>
                  <td>
                    <asp:LinkButton ID="LinkButton1" runat="server" CommandName="Edit" Text='<%# Eval("Name") %>'></asp:LinkButton>
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
                    <asp:LinkButton ID="LinkButton2" runat="server" CommandName="Select">详情</asp:LinkButton>
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
                    <asp:LinkButton ID="LinkButton1" runat="server" CommandName="Update" Text="保存" />
                  </td>
                </tr>
              </EditItemTemplate>
            </asp:ListView>
            <div id="DetailDialog" class="dialog">
              <asp:ListView ID="DetailListView" runat="server" InsertItemPosition="LastItem" DataSourceID="DetailDataSource" DataKeyNames="Id" OnItemDeleting="DetailListView_ItemDeleting" OnItemEditing="DetailListView_ItemEditing" OnItemUpdating="DetailListView_ItemUpdating" OnItemInserting="DetailListView_ItemInserting">
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
                      <asp:PlaceHolder ID="ItemPlaceHolder" runat="server" />
                    </tbody>
                  </table>
                </LayoutTemplate>
                <ItemTemplate>
                  <tr>
                    <td>
                      <asp:LinkButton ID="LinkButton1" runat="server" CommandName="Edit" Text='<%# Eval("Lable") %>' />
                    </td>
                    <td>
                      <%# Eval("Value") %>
                    </td>
                    <td>
                      <asp:LinkButton ID="LinkButton2" runat="server" CommandName="Delete" Text="删除" />
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
                      <asp:LinkButton ID="LinkButton1" runat="server" CommandName="Insert" Text="添加" />
                    </td>
                  </tr>
                </InsertItemTemplate>
                <EditItemTemplate>
                  <tr>
                    <td>
                      <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("Lable") %>' />
                    </td>
                    <td>
                      <asp:TextBox ID="TextBox2" runat="server" Text='<%# Bind("Value") %>' />
                    </td>
                    <td>
                      <asp:LinkButton ID="LinkButton1" runat="server" CommandName="Update" Text="保存" />
                    </td>
                  </tr>
                </EditItemTemplate>
                <EmptyDataTemplate>
                  <%= Helper.EMPTY_DATA %>
                </EmptyDataTemplate>
              </asp:ListView>
            </div>
          </ContentTemplate>
        </asp:UpdatePanel>
        <div id="AddEquipmentDialog" class="dialog">
          <table>
            <tbody>
              <tr>
                <th>
                  设备类别
                </th>
                <td>
                  <asp:TextBox ID="EquipmentCategoryTextBox" runat="server" ClientIDMode="Static" />
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
                  <asp:TextBox ID="EquipmentNameTextBox" runat="server" />
                </td>
              </tr>
              <tr>
                <th>
                  设备编号
                </th>
                <td>
                  <asp:TextBox ID="EquipmentCodeTextBox" runat="server" />
                </td>
              </tr>
            </tbody>
            <tfoot>
              <tr>
                <td colspan="2">
                  <asp:Button ID="AddEquipmentButton" runat="server" Text="添加" OnClick="AddEquipmentButton_Click" />
                </td>
              </tr>
            </tfoot>
          </table>
        </div>
        <div id="AddCategoryDialog" class="dialog">
          <table>
            <tbody>
              <tr>
                <th>
                  父类别
                </th>
                <td>
                  <asp:TextBox ID="ParentCategoryTextBox" runat="server" ClientIDMode="Static" />
                </td>
              </tr>
              <tr>
                <th>
                  类别名称
                </th>
                <td>
                  <asp:TextBox ID="CategoryNameTextBox" runat="server" ClientIDMode="Static" />
                </td>
              </tr>
            </tbody>
            <tfoot>
              <tr>
                <td colspan="2">
                  <asp:Button ID="AddCategoryButton" runat="server" Text="添加" OnClick="AddCategoryButton_Click" />
                </td>
              </tr>
            </tfoot>
          </table>
        </div>
      </div>
    </div>
  </div>
  <asp:ObjectDataSource ID="EquipmentDataSource" runat="server" SelectMethod="GetList" UpdateMethod="Update" TypeName="TSS.BLL.Equipments" DataObjectTypeName="TSS.Models.Equipment">
    <SelectParameters>
      <asp:QueryStringParameter Name="categoryId" QueryStringField="category" Type="String" />
      <asp:Parameter Name="specialtyId" />
    </SelectParameters>
  </asp:ObjectDataSource>
  <asp:ObjectDataSource ID="DetailDataSource" runat="server" SelectMethod="GetDetails" InsertMethod="AddDetail" DeleteMethod="RemoveDetail" UpdateMethod="UpdateDetail" TypeName="TSS.BLL.Equipments">
    <SelectParameters>
      <asp:ControlParameter Name="equipmentId" ControlID="EquipmentListView" PropertyName='SelectedDataKey["Id"]' />
    </SelectParameters>
    <InsertParameters>
      <asp:ControlParameter Name="equipmentId" ControlID="EquipmentListView" PropertyName='SelectedDataKey["Id"]' />
    </InsertParameters>
    <DeleteParameters>
      <asp:ControlParameter Name="equipmentId" ControlID="EquipmentListView" PropertyName='SelectedDataKey["Id"]' />
    </DeleteParameters>
    <UpdateParameters>
      <asp:ControlParameter Name="equipmentId" ControlID="EquipmentListView" PropertyName='SelectedDataKey["Id"]' />
    </UpdateParameters>
  </asp:ObjectDataSource>
  <asp:ObjectDataSource ID="SpecialtyDataSource" runat="server" SelectMethod="GetAll" TypeName="TSS.BLL.Specialties"></asp:ObjectDataSource>
</asp:Content>
