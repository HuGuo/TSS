<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Certificate_Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>持证管理</title>
</head>
<body>
    <form id="form1" runat="server">
    <a href="Add.aspx?s=<%=Request.QueryString["s"] %>">添加持证信息</a>
    <table>
        <tr>
            <td>
                序号
            </td>
            <td>
                姓名
            </td>
            <td>
                性别
            </td>
            <td>
                作业种类
            </td>
            <td>
                资格项目
            </td>
            <td>
                培训单位
            </td>
            <td>
                取证时间
            </td>
            <td>
                证件有效期
            </td>
            <td>
                证书编号
            </td>
            <td>
                备注
            </td>
        </tr>
        <asp:Repeater ID="rptList" runat="server" DataSourceID="CeretificateDataSources">
            <ItemTemplate>
                <tr>
                    <td>
                        <%# Container.ItemIndex+1 %>
                    </td>
                    <td>
                        <%# Eval("EpmloyeeName")%>
                    </td>
                    <td>
                        <%# Eval("Gender") %>
                    </td>
                    <td>
                        <%# Eval("Type") %>
                    </td>
                    <td>
                        <%# Eval("Project") %>
                    </td>
                    <td>
                        <%# Eval("CretificationAuthority")%>
                    </td>
                    <td>
                        <%# Eval("ReceiveDateTime")%>
                    </td>
                    <td>
                        <%# Eval("ExpireDateTime")%>
                    </td>
                    <td>
                        <%# Eval("Number")%>
                    </td>
                    <td>
                        <%# Eval("Remark")%>
                    </td>
                </tr>
            </ItemTemplate>
        </asp:Repeater>
    </table>
    <asp:ObjectDataSource ID="CeretificateDataSources" runat="server" DataObjectTypeName="System.Guid"
        DeleteMethod="Delete" SelectMethod="GetBySpecialty" TypeName="TSS.BLL.CertificateRepository">
        <SelectParameters>
            <asp:QueryStringParameter Name="id" QueryStringField="s" Type="String" />
        </SelectParameters>
    </asp:ObjectDataSource>
    </form>
</body>
</html>
