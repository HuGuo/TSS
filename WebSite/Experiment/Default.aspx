<%@ Page Language="C#" %>
<%@ Import Namespace="TSS.Models"%>
<%@ Import Namespace="TSS.BLL"%>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<script runat="server">
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack) {
            
        }
    }
</script>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>试验报告</title>
    <link href="../scripts/jquery-easyui/themes/gray/easyui.css" rel="stylesheet" type="text/css" />
    <script src="../scripts/jquery-1.6.1.min.js" type="text/javascript"></script>
    <script src="../scripts/jquery-easyui/jquery.easyui.min.js" type="text/javascript"></script>
    <style type="text/css">
    body{ font-size:12px;}
    a{ text-decoration:none; color:#00527f}
    </style>
</head>
<body class="easyui-layout">
	<div region="west" split="true" title="设备分类" style="width:220px; max-width:300px;padding:5px;">
        <ul id="expTree"></ul>
    </div>
	<div region="center" style="padding:0">
    <iframe name="frm_explist" scrolling="auto" frameborder="0"  src="" style="width:100%;height:100%;"></iframe>
    </div>
</body>
</html>
<script type="text/javascript">
    var s = '<%=Request.QueryString[Helper.queryParam_specialty] %>';
    $(document).ready(function () {
        var query = escape("?s=" + s + "&category=");
        $("#expTree").load("../equipmentcategory.ashx?type=xml&dp=0&target=frm_explist&src=explist.aspx" + query, function (data) {
            $("#expTree").tree();
        });

    });
</script>