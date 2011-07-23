<%@ Page Language="C#" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
  <head runat="server">
    <title></title>
    <link href="~/Styles/Nav.css" rel="Stylesheet" type="text/css" />
    <script type="text/javascript" src="Scripts/jquery-1.6.1.min.js"></script>
    <script type="text/javascript">
      $(function () {
        $("a").bind("click", function (e) {
          $(this).parent().siblings().find("a").each(function () {
            if (this != e.target) {
              $(this).next("ul").hide();
              $(this).removeClass("hover");
            }
          });

          $(this).next("ul").toggle();
          $(this).toggleClass("hover");

          if ($(this).attr("href") == "#") {
            return false;
          }
        });
      });
    </script>
    <base target="content" />
  </head>
  <body id="nav">
    <form id="form1" runat="server">
    <div id="nav_menu">
      <asp:ListView ID="ListView1" runat="server" DataSourceID="ObjectDataSource1" ItemPlaceholderID="Placeholder1">
        <LayoutTemplate>
          <ul>
            <li><a href="#">专业监督</a>
              <ul>
                <asp:PlaceHolder ID="Placeholder1" runat="server" />
              </ul>
            </li>
            <asp:ListView ID="ListView3" runat="server" DataSourceID="ObjectDataSource2" ItemPlaceholderID="PlaceHolder3">
              <LayoutTemplate>
                <asp:PlaceHolder ID="PlaceHolder3" runat="server" />
              </LayoutTemplate>
              <ItemTemplate>
                <li><a href="<%# ((HashSet<TSS.Models.Module>)Eval("Submodules")).Count == 0 && Eval("Path") != null ? ResolveUrl(string.Format("~/{0}/", Eval("Path"))) : "#" %>">
                  <%# Eval("Name") %></a>
                  <asp:ListView ID="ListView4" runat="server" DataSource='<%# Eval("Submodules") %>' ItemPlaceholderID="PlaceHolder4">
                    <LayoutTemplate>
                      <ul>
                        <asp:PlaceHolder ID="PlaceHolder4" runat="server" />
                      </ul>
                    </LayoutTemplate>
                    <ItemTemplate>
                      <li><a href="<%# ((HashSet<TSS.Models.Module>)Eval("Submodules")).Count == 0 ? ResolveUrl(string.Format("~/{0}/{1}/", DataBinder.Eval(((ListViewDataItem)Container.Parent.DataItemContainer).DataItem, "Path"), Eval("Path"))) : "#" %>">
                        <%# Eval("Name") %></a>
                        <asp:ListView ID="ListView5" runat="server" DataSource='<%# Eval("Submodules") %>' ItemPlaceholderID="PlaceHolder5">
                          <LayoutTemplate>
                            <ul>
                              <asp:PlaceHolder ID="PlaceHolder5" runat="server" />
                            </ul>
                          </LayoutTemplate>
                          <ItemTemplate>
                            <li><a href="<%# ResolveUrl(string.Format("~/{0}/{1}/{2}/", DataBinder.Eval(((ListViewDataItem)Container.Parent.DataItemContainer.Parent.DataItemContainer).DataItem, "Path"), DataBinder.Eval(((ListViewDataItem)Container.Parent.DataItemContainer).DataItem, "Path"), Eval("Path"))) %>">
                              <%# Eval("Name") %></a></li>
                          </ItemTemplate>
                        </asp:ListView>
                      </li>
                    </ItemTemplate>
                  </asp:ListView>
                </li>
              </ItemTemplate>
            </asp:ListView>
          </ul>
        </LayoutTemplate>
        <ItemTemplate>
          <li><a href="#">
            <%# Eval("Name") %></a>
            <asp:ListView ID="ListView2" runat="server" DataSource='<%# Eval("Submodules") %>' ItemPlaceholderID="Placeholder2">
              <LayoutTemplate>
                <ul>
                  <asp:PlaceHolder ID="Placeholder2" runat="server" />
                </ul>
              </LayoutTemplate>
              <ItemTemplate>
                <li><a href="<%# ResolveUrl(string.Format("~/{0}/?s={1}", Eval("Path"), DataBinder.Eval(((ListViewDataItem)Container.Parent.DataItemContainer).DataItem, "Path"))) %>">
                  <%# Eval("Name") %></a></li>
              </ItemTemplate>
            </asp:ListView>
          </li>
        </ItemTemplate>
      </asp:ListView>
      <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" SelectMethod="GetSpecialtyModules" TypeName="TSS.BLL.Modules"></asp:ObjectDataSource>
      <asp:ObjectDataSource ID="ObjectDataSource2" runat="server" SelectMethod="GetRootModulesWithoutSpecialty" TypeName="TSS.BLL.Modules" />
    </div>
    </form>
  </body>
</html>
