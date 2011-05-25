<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Add.aspx.cs" Inherits="Certificate_Add" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>人员资质</title>
</head>
<body>
    <form id="form1" runat="server">
    <table>
    <tr>
    <td>证书编号</td>
    <td>
        <asp:TextBox ID="txtNumber" runat="server"></asp:TextBox>
        </td>
    </tr>
    <tr>
    <td>证书效果图</td>
    <td>
        <asp:FileUpload ID="fileUp" runat="server" />
        </td>
    </tr>
    <tr>
    <td>姓名</td>
    <td>
        <asp:TextBox ID="txtName" runat="server"></asp:TextBox>
        </td>
    </tr>
    <tr>
    <td>性别</td>
    <td>
        <asp:DropDownList ID="ddlGender" runat="server">
            <asp:ListItem Selected="True">男</asp:ListItem>
            <asp:ListItem>女</asp:ListItem>
        </asp:DropDownList>
        </td>
    </tr>
    <tr>
    <td>作业种类</td>
    <td>
        <asp:TextBox ID="txtType" runat="server"></asp:TextBox>
        </td>
    </tr>
    <tr>
    <td>资格项目</td>
    <td>
        <asp:TextBox ID="txtProject" runat="server"></asp:TextBox>
        </td>
    </tr>
    <tr>
    <td>培训单位</td>
    <td>
        <asp:TextBox ID="txtAuthor" runat="server"></asp:TextBox>
        </td>
    </tr>
    <tr>
    <td>取证时间</td>
    <td>
        <asp:TextBox ID="txtReceiveDate" runat="server"></asp:TextBox>
        </td>
    </tr>
    <tr>
    <td>证件有效期</td>
    <td>
        <asp:TextBox ID="txtExpireDate" runat="server"></asp:TextBox>
        </td>
    </tr>
    <tr>
    <td>备注</td>
    <td>
        <asp:TextBox ID="txtRemark" runat="server"></asp:TextBox>
        </td>
    </tr>
    <tr>
    <td>&nbsp;</td>
    <td>
        <asp:Button ID="btnSave" runat="server" Text="保存" onclick="btnSave_Click" />
        </td>
    </tr>
    </table>
    </form>
</body>
</html>
