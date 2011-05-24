<%@ Page Language="C#" AutoEventWireup="true" CodeFile="uploadfyDemo.aspx.cs" Inherits="Experiment_uploadfyDemo" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../uploadify/uploadify.css" rel="stylesheet" type="text/css" />
    <script src="../scripts/jquery-1.6.1.min.js" type="text/javascript"></script>
    <script src="../uploadify/swfobject.js" type="text/javascript"></script>
    <script src="../uploadify/jquery.uploadify.v2.1.4.min.js" type="text/javascript"></script>
</head>
<body>
    <form id="form1" runat="server">
    <input type="file" id="upDemo" />
    <a id="upload" href="javascript:$('#upDemo').uploadifyUpload()">上传</a>
    <a id="cancelUpload" href="javascript:$('#upDemo').uploadifyClearQueue()">取消</a>
    </form>
</body>
</html>
<script type="text/javascript">
    $(document).ready(function () {
        $("#upDemo").uploadify({
            'uploader': '../uploadify/uploadify.swf',
            'cancelImg':'../uploadify/cancel.png',
            'script': 'uploadhandler.ashx',
            'folder': '../attachment',
            'multi':true,
            'auto':false,
            'onComplete': function (event, ID, fileObj, response, data) {
            },
            'onAllComplete': function (event, data) {
                alert(data.filesUploaded + ' 个文件上传成功!');
            }
        });
        
    });
</script>