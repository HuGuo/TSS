<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Success.aspx.cs" Inherits="Experiment_Success" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>保存成功</title>
    <link href="../Styles/_base.css" rel="stylesheet" type="text/css" />
    <link href="../uploadify/uploadify.css" rel="stylesheet" type="text/css" />
    <script src="../scripts/jquery-1.6.1.min.js" type="text/javascript"></script>
    <script src="../uploadify/swfobject.js" type="text/javascript"></script>
    <script src="../uploadify/jquery.uploadify.v2.1.4.min.js" type="text/javascript"></script>
</head>
<body>
    <form id="form1" runat="server">
    <div class="success">
    <h4>
        操作成功!</h4> 还可以<a id="upload" href="#">上传本次试验相关附件</a>
    </div>
    <div id="panel" style="width:400px; height:200px; border:5px solid #e2e2e2; display:none; background:#FFFFFF;">
    <input type="file" id="upfile" /><a href="#" onclick="javascript:$('#upfile').uploadifyUpload();">上传</a>
    </div>
    </form>
</body>
</html>
<script type="text/javascript">
    $(function () {
        $("#upload").one("click", function () {
            $("#panel").show();
            $("#upfile").uploadify({
                'uploader': '../uploadify/uploadify.swf',
                'cancelImg': '../uploadify/cancel.png',
                'script': '../exp.ashx',
                'scriptData': { op: "upload", expid: '<%=Request.QueryString["id"] %>' },
                'multi': true,
                'auto': false,
                'onAllComplete': function (event, data) {
                    $("#panel").remove();
                    $("div.success").html("<h4>试验附件上传成功</h4>");
                }
            });
        });
    });
</script>