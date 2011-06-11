<%@ Page Language="C#" EnableViewState="false" AutoEventWireup="true" CodeFile="Login.aspx.cs" Inherits="_Login" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>系统登录</title>
    <link href="~/Styles/login.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <div id="form_wrapper" class="form_wrapper">
        <form id="loginForm" class="login active">
        <h3>
            用户登录</h3>
        <div>
            <label>
                用户名:</label>
            <input type="text" id="txt_name" />
            <span class="error">This is an error</span>
        </div>
        <div>
            <label>
                密 码:
            </label>
            <input type="password" id="txt_pwd" />
            <span class="error">This is an error</span>
        </div>
        <div class="bottom">
            <div class="remember">
                <input type="checkbox" /><span>记住用户名密码</span></div>
            <%--<asp:Button ID="btnLogin" runat="server" Text="Login" 
                onclick="btnLogin_Click" />--%>
                <input type="button" id="btnLogin" value="Login" />
            <div class="clear">
            </div>
        </div>
        </form>
        <form id="InitForm" runat="server" class="register">
        <h3>
            第一次登陆，设置信息</h3>
        <div>
        <label>
                专业:<asp:DropDownList ID="ddlSpecialty" runat="server">
        </asp:DropDownList>
            </label>
        
        </div>
        <div class="bottom">
        <input type="button" id="btnSet" value="OK" />
        <%--<div class="clear"/>--%>
        </div>
        </form>
    </div>
</body>
</html>
<script src="scripts/jquery-1.6.1.min.js" type="text/javascript"></script>
<script src="scripts/jquery-easyui/easyloader.js" type="text/javascript"></script>
<script type="text/javascript">
    $(document).ready(function () {
        //check browser version
        if ($.browser.version == "6.0") {
            easyloader.load("themes/gray/easyui.css", function () {
                easyloader.load("messager", function () {
                    $.messager.alert('系统提示', '您的浏览器（IE 6.0）版本过低，部分功能将无法使用！<br/>建议您使用IE 8.0及以上版本', 'warning');
                });
            });
        }
        // set width and height
        var loginResult = ["success", "口令错误", "用户不存在", "用户帐号被禁用"];
        var $form_wrapper = $('#form_wrapper')
        var $currentForm = $form_wrapper.children('form.active');
        $form_wrapper.children('form').each(function (i) {
            var $theForm = $(this);
            if (!$theForm.hasClass('active'))
                $theForm.hide();
            $theForm.data({
                width: $theForm.width(),
                height: $theForm.height()
            });
        });

        $("#btnLogin").click(function (e) {
            var query = { ajax: "", name: "", pwd: "" };
            query.name = $("#txt_name").val();
            query.pw = $("#txt_pwd").val();
            if (query.name == "") {
                alert("用户名不能为空"); return false;
            }
            $.get("login.aspx", query, function (data) {
                if (data == "1") {
                    //第一次登陆系统
                    //var target=$(this).attr("rel")
                    $currentForm.fadeOut(400, function () {
                        $currentForm.removeClass('active');
                        $currentForm = $("#InitForm");
                        $form_wrapper.stop()
									 .animate({
									     width: $currentForm.data('width') + 'px',
									     height: $currentForm.data('height') + 'px'
									 }, 500, function () {
									     $currentForm.addClass('active');
									     $currentForm.fadeIn(400);
									 });
                    });
                    e.preventDefault();
                } else {
                    var r = parseInt(data);
                    if (r > 0) {
                        alert(loginResult[r]);
                    }
                }
            });
        }).ajaxStart(function () {
            $(this).val("验证中...");
        }).ajaxComplete(function () { $(this).val("Login"); });

        $("#btnSet").click(function () {
            document.location.href = "default.htm";
        });
    });
</script>