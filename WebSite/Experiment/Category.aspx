<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Category.aspx.cs" Inherits="Experiment_Category" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>分类管理</title>
    <link href="../Styles/_base.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/jquery-1.6.1.min.js" type="text/javascript"></script>
    <!--easyui-->
    <link href="../Scripts/jquery-easyui/themes/gray/easyui.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/jquery-easyui/jquery.easyui.min.js" type="text/javascript"></script>

</head>
<body class="easyui-layout">
    <div region="west" split="true" title="实验报告分类" style="width: 220px; max-width: 300px;
        padding: 5px;">
        <div style="">
        <a class="button">添加分类</a> 
        <a class="button">重命名</a>
        </div>
        <ul id="expTree">
        </ul>
    </div>
    <div region="center" style="padding: 0">
        <iframe name="frm_explist" scrolling="auto" frameborder="0" src="" style="width: 100%;
            height: 100%;"></iframe>
    </div>
</body>
</html>
<script type="text/javascript">
    var s = '<%=Request.QueryString["s"] %>',
          href = "explist.aspx";
    $(function () {
        //load tree
        var query = { op: "xml", s: s, dp: 0, target: "frm_explist", src: href + "?s=" + s + "&category=" };
        var tree_dataurl = "expcategory.ashx";
        $("#expTree").load(tree_dataurl, query, function () {
            $(this).tree();
        });
    });
</script>