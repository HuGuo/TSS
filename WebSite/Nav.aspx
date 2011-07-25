<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Nav.aspx.cs" Inherits="Nav" %>

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
        });
      });
    </script>
    <base target="content" />
  </head>
  <body id="nav">
    <form id="form1" runat="server">
    <div id="nav_menu">
      <asp:ListView ID="ListView1" runat="server" ItemPlaceholderID="PlaceHolder1" OnItemCreated="ListView_ItemCreated">
        <LayoutTemplate>
          <ul>
            <asp:PlaceHolder ID="PlaceHolder1" runat="server" />
          </ul>
                                <li><a href="#">????</a>
                        <ul>
                            <li><a href="#">??????</a>
                                <ul>
                                    <li><a href="SupervisionNews/?s=1">????</a></li>
                                    <li><a href="SupervisionNews/?s=2">????</a></li>
                                    <li><a href="SupervisionNews/?s=3">????</a></li>
                                </ul>
                            </li>
                            <li><a href="#">??????</a>
                                <ul>
                                    <li><a href="SupervisionNews/?s=4">????????</a></li>
                                    <li><a href="SupervisionNews/?s=5">???????</a></li>
                                    <li><a href="SupervisionNews/?s=6">??????????</a></li>
                                </ul>
                            </li>
                            <li><a href="#">????????</a>
                                <ul>
                                    <li><a href="SupervisionNews/?s=7">??????</a></li>
                                    <li><a href="SupervisionNews/?s=8">??????????</a></li>
                                </ul>
                            </li>
                        </ul>
                    </li>
        </LayoutTemplate>
        <ItemTemplate>
          <li><a href="<%# GetUrl((int)Eval("Id")) ?? "javascript:void(0)" %>">
            <%# Eval("Name") %></a>
            <asp:ListView ID="ListView2" runat="server" DataSource='<%# Eval("Submodules") %>' ItemPlaceholderID="PlaceHolder2" OnItemCreated="ListView_ItemCreated">
              <LayoutTemplate>
                <ul>
                  <asp:PlaceHolder ID="PlaceHolder2" runat="server" />
                </ul>
              </LayoutTemplate>
              <ItemTemplate>
                <li><a href="<%# GetUrl((int)Eval("Id")) ?? "javascript:void(0)" %>">
                  <%# Eval("Name") %></a>
                  <asp:ListView ID="ListView3" runat="server" DataSource='<%# Eval("Submodules") %>' ItemPlaceholderID="PlaceHolder3" OnItemCreated="ListView_ItemCreated">
                    <LayoutTemplate>
                      <ul>
                        <asp:PlaceHolder ID="PlaceHolder3" runat="server" />
                      </ul>
                    </LayoutTemplate>
                    <ItemTemplate>
                      <li><a href="<%# GetUrl((int)Eval("Id")) %>">
                        <%# Eval("Name") %></a></li>
                    </ItemTemplate>
                  </asp:ListView>
                </li>
              </ItemTemplate>
            </asp:ListView>
          </li>
        </ItemTemplate>
      </asp:ListView>
    </div>
    </form>
  </body>
</html>
