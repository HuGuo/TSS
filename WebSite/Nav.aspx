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
          <asp:ListView ID="ListView1" runat="server" DataSourceID="ObjectDataSource1">
            <LayoutTemplate>
              <ul>
                <asp:PlaceHolder ID="itemPlaceholder" runat="server" />
              </ul>
            </LayoutTemplate>
            <ItemTemplate>
              <li><a href="#" class="expand">
                <%# Eval("Name") %></a>
                <ul>
                  <li><a href="Equipment/?s=<%# Eval("Id") %>">设备台帐</a></li>
                  <li><a href="Certificate/?s=<%# Eval("Id") %>">人员资历</a></li>
                  <li><a href="MaintenanceCycle/?s=<%# Eval("Id") %>">设备预试周期</a></li>
                  <li><a href="Experiment/?s=<%# Eval("Id") %>">试验报告</a></li>
                  <li><a href="Certificate/?s=<%# Eval("Id") %>">试验台帐</a></li>
                  <li><a href="Report/?s=<%# Eval("Id") %>">监督月报</a></li>
                  <li><a href="Document/?s=<%# Eval("Id") %>">档案资料</a></li>
                </ul>
              </li>
            </ItemTemplate>
          </asp:ListView>
        </li>
        <li><a href="#" class="expand">系统管理</a>
          <ul>
            <li><a href="SystemManagement/Employee/">用户管理</a></li>
            <li><a href="SystemManagement/Equipment/">设备管理</a></li>
            <li><a href="SystemManagement/Experiment/">试验管理</a></li>
            <li><a href="SystemManagement/Workflow/">工作流管理</a></li>
          </ul>
        </li>
      </ul>
    </div>
    <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" SelectMethod="GetAll" TypeName="TSS.BLL.Specialties"></asp:ObjectDataSource>
    </form>
  </body>
</html>
