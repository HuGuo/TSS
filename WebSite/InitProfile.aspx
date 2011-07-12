<%@ Page Language="C#" AutoEventWireup="true" CodeFile="InitProfile.aspx.cs" Inherits="InitProfile" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>初始化用户信息</title>
    <link href="Styles/_base.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <div class="success">
        <h4>
            登录成功！第一次登录本系统，需要设置以下信息</h4>
    </div>
    专业:<asp:DropDownList ID="ddlSpecialty" runat="server">
    </asp:DropDownList>
    <asp:LinkButton ID="lbtnOk" runat="server" class="big button" 
        onclick="lbtnOk_Click"><span class="icon check"></span>确定</asp:LinkButton>
    </form>
</body>
</html>
