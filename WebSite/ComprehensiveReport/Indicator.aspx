<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Indicator.aspx.cs" MasterPageFile="~/ValidateAndUi.master"
    Inherits="ComprehensiveReport_Indicator" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript">
        function Del(id, specialtyId) {
            $.messager.confirm("删除", "是否删除！", function (result) {
                if (result) {
                    window.location.href = "Indicator.aspx?id=" + id + "&specialtyId=" + specialtyId;
                } else {
                    return result;
                }
            });
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="Server">


 <div id="toolbar" class="fixed">
      <a href="AddIndicator.aspx?specialtyId=<%= Request.QueryString["specialtyId"] %>">添加</a>
      <a href="Default.aspx?s=<%= Request.QueryString["specialtyId"] %>">返回</a>
    </div>
      <asp:Repeater ID="rptIndicator" runat="server">
            <HeaderTemplate>
                <table>
                    <tr>
                        <td>
                            序号
                        </td>
                        <td>
                            指标
                        </td>
                        <td>
                            单位
                        </td>
                        <td>
                            专业
                        </td>
                        <td>
                            操作
                        </td>
                    </tr>
            </HeaderTemplate>
            <ItemTemplate>
                <tr>
                    <td>
                        <%# Container.ItemIndex+1 %>
                    </td>
                    <td>
                        <%# Eval("IndicatorName")%>
                    </td>
                    <td>
                        <%# Eval("IndivatorUnit")%>
                    </td>
                    <td>
                        <%# Eval("Specialty.Name")%>
                    </td>
                    <td>
                        <a href="EditIndicator.aspx?specialtyId=<%# Request.QueryString["specialtyId"]  %>&id=<%# Eval("Id")%>">修改</a>
                        <a href="#" onclick="Del('<%# Eval("Id")%>','<%# Request.QueryString["specialtyId"]  %>')">删除</a>
                    </td>
                </tr>
            </ItemTemplate>
            <FooterTemplate>
                <tr style="visibility:<%#  rptIndicator.Items.Count==0?"visible":"hidden" %>">
                    <td colspan="5" align="center">
                        无指标
                    </td>
                </tr>
                </table>
            </FooterTemplate>
        </asp:Repeater>
</asp:Content>
