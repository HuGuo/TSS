<%@ Page Language="C#" AutoEventWireup="true" CodeFile="top.aspx.cs" Inherits="top" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
<form runat="server" id="form1">
<div id="top">
	<ul>
	<li class="top">
	  <div class="top_gif"></div>
	</li>
	<li class="nav">
		<div class="admin fl_l">欢迎：XXX</div>
		<ul><li class="fl_r">
			<a href="ToDoList.aspx" target="fmain" class="nh1">待办业务</a>
			<a href="DoList.aspx" target="fmain" class="nh1">已办业务</a>
			<a href="PerPlan/MyCares.aspx" target="fmain" class="nh1">我关心的监督</a>
			<a href="PerPlan/PlanList.aspx" target="fmain" class="nh">我的工作计划<asp:Label ForeColor="red" runat="server" ID="lbPlantCount"></asp:Label></a>
			<a href="help.htm" target="_blank" class="nh2">帮 助</a>
			<a href="login.aspx?act=out" target="_parent" class="nh2">注 销</a>
			<a href="#" onclick="show()" class="nh1">重设密码</a>
		</li></ul>
	</li></ul>
</div>

</form>
</body>
</html>
<script language="javascript" type="text/javascript">
    function show() {
        window.open("ResetPassword.aspx", "_black", "height=200,width=400,status=yes,toolbar=no,menubar=no,location=no,top=250,left = 350");
    }
    </script>