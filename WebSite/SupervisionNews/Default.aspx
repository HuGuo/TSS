<%@ Page Title="" Language="C#" MasterPageFile="~/ValidateAndUI.master" AutoEventWireup="true"
    CodeFile="Default.aspx.cs" Inherits="SupervisionNews_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
<script type="text/javascript" src="../Scripts/jquery.validatewindow.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="Server">
    <div id="toolbar" class="fixed">
    <a href="SupervisionNewsAdd.aspx?s=<%= Request.QueryString["s"] %>">添加</a>
    </div>
    <asp:Repeater ID="rptNews" runat="server">
        <HeaderTemplate>
            <table>
                <tr>
                    <td>
                        序号
                    </td>
                    <td>
                        标题
                    </td>
                    <td>
                        发布人
                    </td>
                    <td>
                        发布时间
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
                    <%# Eval("Title") %>
                </td>
                <td>
                    <%# Eval("Author")%>
                </td>
                <td>
                    <%# Eval("ReleaseTime")%>
                </td>
                <td>
                    <asp:LinkButton runat="server" ID="lbtnDel" OnClick="lbtnDel_Click" CommandArgument='<%# Eval("Id")%>'
                        OnClientClick="return $.Confirm('是否删除？',this)">删除</asp:LinkButton>
                    <a href="SupervisionNewsEdit.aspx?s=<%= Request.QueryString["s"] %>&id=<%# Eval("id") %>">编辑</a>
                    <a href="SupervisionNewsDetail.aspx?s=<%= Request.QueryString["s"] %>&id=<%# Eval("id") %>">详细</a>
                </td>
            </tr>
        </ItemTemplate>
        <FooterTemplate>
            <tr style="visibility: <%# rptNews.Items.Count==0?"visible":"hidden" %>">
                <td colspan="5" align="center">
                    无数据
                </td>
            </tr>
            </table>
        </FooterTemplate>
    </asp:Repeater>
</asp:Content>
