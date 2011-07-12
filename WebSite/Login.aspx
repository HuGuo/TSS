<%@ Page Language="C#" EnableViewState="false" AutoEventWireup="true" CodeFile="Login.aspx.cs"
    Inherits="_Login" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>湖北清江隔河岩水力发电厂—技术监督管理系统</title>
    <link href="~/Styles/login.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <div style="height: 50px;">
    </div>
    <div class="content">
        <div class="column_left">
            <div id="logo">
            </div>
            <div class="column_left_h80_w160">
            </div>
            <div class="column_left_h279">
            </div>
        </div>
        <div class="column_center">
            <div class="column_center_h80">
            </div>
            <form id="form1" runat="server" defaultfocus="txtLoginName" defaultbutton="btnLogin">
            <div class="column_center_h162">
                <div class="placeholder" style="height: 50px;">
                <div id="emsg" style=" margin:20px 30px; color:#d11; font-size:90%; letter-spacing:1px;">
                    <asp:Literal ID="ltmsg" runat="server"></asp:Literal></div>
                </div>
                <p>
                    <label for="txtLoginName">
                        帐 号</label><asp:TextBox ID="txtLoginName" runat="server" class="txt"></asp:TextBox>
                </p>
                <p>
                    <label for="txtPassword">
                        密 码</label><asp:TextBox 
                        ID="txtPassword" runat="server" class="txt" TextMode="Password"></asp:TextBox></p>
            </div>
            <div class="column_center_h45_w91">
            </div>
            <div>
                <asp:Button ID="btnLogin" runat="server" Text="" class="loginbtn" OnClick="btnLogin_Click" />
            </div>
            <div class="column_center_h45_w65">
            </div>
            </form>
            <div class="column_center_h142">
                <div class="placeholder">
                </div>
                <p>
                    <b>浏览器要求</b>：IE8.0及以上版本，否则 部分功能或效果将无法使用</p>
                <p>
                    <b>推荐浏览器</b>：<a href="http://www.google.com/chrome?brand=CHKZ&hl=zh-CN" target="_blank">Chrome</a></p>
            </div>
        </div>
        <div class="column_right">
            <div class="column_right_h80">
            </div>
            <div class="column_right_h279">
            </div>
        </div>
        <div class="clear">
        </div>
        <div class="footline" style="color:#777;">
            © 2011 深圳天道数字工程有限公司
        </div>
    </div>
</body>
</html>
<script src="scripts/jquery-1.6.1.min.js" type="text/javascript"></script>
<script src="scripts/jquery-easyui/easyloader.js" type="text/javascript"></script>
<script type="text/javascript">
    var handlerUrl = "login.ashx";
    $(document).ready(function () {
        //check browser version
        if ($.browser.version == "6.0") {
            easyloader.load("themes/gray/easyui.css", function () {
                easyloader.load("messager", function () {
                    $.messager.alert('系统提示', '您的浏览器（IE 6.0）版本过低，部分功能将无法使用！<br/>建议您使用IE 8.0及以上版本', 'warning');
                });
            });
        }

        $("#btnLogin").click(function () {
            if ($("#txtLoginName").val() == "") {
                $("#emsg").text("帐号不能为空")
                return false;
            }
        });
    });
</script>
