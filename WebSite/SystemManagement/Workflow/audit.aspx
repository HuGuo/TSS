<%@ Page Language="C#" AutoEventWireup="true" CodeFile="audit.aspx.cs" Inherits="SystemManagement_Workflow_audit" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>流程审批</title>
    <link href="../../Styles/_base.css" rel="stylesheet" type="text/css" />
    <script src="../../Scripts/jquery-1.6.1.min.js" type="text/javascript"></script>
    <!--easyui-->
    <link href="../../Scripts/jquery-easyui/themes/gray/easyui.css" rel="stylesheet"
        type="text/css" />
    <script src="../../Scripts/jquery-easyui/jquery.easyui.min.js" type="text/javascript"></script>
</head>
<body class="easyui-layout">
    <div region="center" title="审批内容">
        <iframe name="frm" id="frm" frameborder="0" border="0" marginwidth="0" marginheight="0"
            scrolling="auto" allowtransparency="true" src="<%=Request.QueryString["url"] %>"
            style="width: 100%; height: 100%;"></iframe>
    </div>
    <div region="south" title="审批意见" split="true" style=" height:150px; margin:0; padding:0; vertical-align:middle;">
        <textarea id="txtTag" rows="5" cols="80" style="width: 70%; height: 100px;"></textarea>
        <a class="big button primary"><span class="icon check"></span>同意</a> <a class="big button negative">
            <span class="icon leftarrow"></span>打回上一步</a>
    </div>
    <div region="east" title="其他用户审批意见" split="true" style="width:300px;">
    </div>
</body>
</html>
