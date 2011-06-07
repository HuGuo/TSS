<%@ Page Language="C#" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
  <head runat="server">
    <title></title>
    <script type="text/javascript" src="Scripts/jquery-1.6.1.min.js"></script>
    <script type="text/javascript">
      $(function () {
        $("a.expand").bind("click", function (e) {
          $(e.target).parent().siblings().find("a.expand").each(function () {
            if (this != e.target) {
              $(this).next("ul").hide();
            }
          });

          $(this).next("ul").toggle();

          return false;
        });
      });
    </script>
    <base target="content" />
  </head>
  <body id="nav">
    <form id="form1" runat="server">
    <div id="nav_top_top">
    </div>
    <div id="nav_top_bottom">
    </div>
    <div id="nav_menu">
      <ul>
        <li><a href="#" class="expand">监督动态</a></li>
        <li><a href="#" class="expand">监督体系</a></li>
        <li><a href="#" class="expand">监督管理</a></li>
        <li><a href="#" class="expand">专业监督</a>
          <asp:ListView ID="ListView1" runat="server" DataSourceID="ObjectDataSource1" ItemPlaceholderID="Placeholder1">
            <LayoutTemplate>
              <ul>
                <asp:PlaceHolder ID="Placeholder1" runat="server" />
              </ul>
            </LayoutTemplate>
            <ItemTemplate>
              <li><a href="#" class="expand">
                <%# Eval("Name") %></a>
                <asp:ListView ID="ListView2" runat="server" DataSource='<%# Eval("Modules") %>' ItemPlaceholderID="Placeholder2">
                  <LayoutTemplate>
                    <ul>
                      <asp:PlaceHolder ID="Placeholder2" runat="server" />
                    </ul>
                  </LayoutTemplate>
                  <ItemTemplate>
                    <li><a href="<%# DataBinder.Eval(((ListViewDataItem)Container.Parent.DataItemContainer).DataItem, "Id", Eval("Url") + "/?s={0}") %>">
                      <%# Eval("Name") %></a></li>
                  </ItemTemplate>
                </asp:ListView>
              </li>
            </ItemTemplate>
          </asp:ListView>
        </li>
        <li><a href="#" class="expand">系统管理</a>
          <ul>
            <li><a href="SystemManagement/Employee/">人员管理</a></li>
            <li><a href="SystemManagement/Equipment/">设备管理</a></li>
            <li><a href="SystemManagement/Experiment/">实验管理</a></li>
            <li><a href="SystemManagement/Workflow/">工作流管理</a></li>
            <li><a href="SystemManagement/Module/">模块管理</a></li>
          </ul>
        </li>
      </ul>
    </div>
    <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" SelectMethod="GetAll" TypeName="TSS.BLL.Specialties"></asp:ObjectDataSource>
    </form>
  </body>
</html>
