<%@ Page Language="C#" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
  <head runat="server">
    <title></title>
    <link href="~/Styles/Nav.css" rel="Stylesheet" type="text/css" />
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
      <asp:ListView ID="ListView1" runat="server" DataSourceID="ObjectDataSource1" ItemPlaceholderID="Placeholder1">
        <LayoutTemplate>
          <ul>
            <li><a href="#" class="expand">专业监督</a>
              <ul>
                <asp:PlaceHolder ID="Placeholder1" runat="server" />
              </ul>
            </li>
            <asp:ListView ID="ListView3" runat="server" DataSourceID="ObjectDataSource2" ItemPlaceholderID="PlaceHolder3">
              <LayoutTemplate>
                <asp:PlaceHolder ID="PlaceHolder3" runat="server" />
              </LayoutTemplate>
              <ItemTemplate>
                <li><a href="#" class="expand">
                  <%# Eval("Name") %></a>
                  <asp:ListView ID="ListView4" runat="server" DataSource='<%# Eval("Submodules") %>' ItemPlaceholderID="PlaceHolder4">
                    <LayoutTemplate>
                      <ul>
                        <asp:PlaceHolder ID="PlaceHolder4" runat="server" />
                      </ul>
                    </LayoutTemplate>
                    <ItemTemplate>
                      <li><a href="<%# string.Format("{0}/{1}/", DataBinder.Eval(((ListViewDataItem)Container.Parent.DataItemContainer).DataItem, "Url"), Eval("Url")) %>">
                        <%# Eval("Name") %></a></li>
                    </ItemTemplate>
                  </asp:ListView>
                </li>
              </ItemTemplate>
            </asp:ListView>
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
                <li><a href="<%# string.Format("{0}/?s={1}", Eval("Url"), DataBinder.Eval(((ListViewDataItem)Container.Parent.DataItemContainer).DataItem, "Id")) %>">
                  <%# Eval("Name") %></a></li>
              </ItemTemplate>
            </asp:ListView>
          </li>
        </ItemTemplate>
      </asp:ListView>
      <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" SelectMethod="GetAllWithModules" TypeName="TSS.BLL.Specialties"></asp:ObjectDataSource>
      <asp:ObjectDataSource ID="ObjectDataSource2" runat="server" SelectMethod="GetRootModules" TypeName="TSS.BLL.Modules" />
    </div>
    </form>
  </body>
</html>
