<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="document_Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>文档资料</title>
    <link href="listStyle.css" rel="stylesheet" type="text/css" />
    <link href="~/Styles/_base.css" rel="stylesheet" type="text/css" />
    <link href="../scripts/jquery-easyui/themes/gray/easyui.css" rel="stylesheet" type="text/css" />
    <link href="../uploadify/uploadify.css" rel="stylesheet" type="text/css" />
    <script src="../scripts/jquery-1.6.1.min.js" type="text/javascript"></script>
    <script src="../uploadify/swfobject.js" type="text/javascript"></script>
    <script src="../uploadify/jquery.uploadify.v2.1.4.min.js" type="text/javascript"></script>
    <script src="../scripts/jquery-easyui/jquery.easyui.min.js" type="text/javascript"></script>
</head>
<body><div id="toolbar" class="fixed">
<a runat="server" id="backto" href="javascript:void(0);">上级目录</a>
        <a href="#dg_upfile" id="upload">上传文件</a>
        <a href="#dg_newfolder" id="new_folder">新建文件夹</a>
        <a href="javascript:$.FF.delItem();" id="A1">删除</a>
        <div class="search">
            <input type="text" id="txtKey" class="textbox" maxlength="12" />
            <input type="button" id="goSearch" class="searchbtn" />
        </div>
    </div>
    <form id="form1" runat="server">
    <ul id="mylist" style="margin-top: 32px;">
        <asp:Repeater ID="rptlist" runat="server">
            <ItemTemplate>
                <li><a href="javascript:;" pid="<%#Eval("id") %>" backid="<%#Eval("ParentId") %>"
                    isfolder="<%#Eval("isFolder") %>" title="<%#Eval("name") %>">
                    <div class="ico" style="background: url(<%# getIcon(Eval("Path").ToString()) %>) no-repeat center center;">
                    </div>
                    <div class="filename">
                        <%#Eval("name") %>
                    </div>
                </a></li>
            </ItemTemplate>
        </asp:Repeater>
    </ul>
    </form>
    <div id="dg_upfile" class="easyui-dialog" title="上传文件" style="width: 500px; height: 300px; top: 150px;
        margin-left: auto; margin-right: auto; padding: 0;" buttons="#dlg_btn1">
        <input type="file" id="upDemo" />
    </div>
    <div id="dlg_btn1">
        <a href="#" class="easyui-linkbutton" onclick="javascript:$('#upDemo').uploadifyUpload()">上传</a>
        <a href="#" id="cancelUpload" class="easyui-linkbutton">关闭</a>
    </div>
    <div id="dg_newfolder" class="easyui-dialog" title="新建文件夹" style="width: 300px; height: 150px; top: 150px;
        margin-left: auto; margin-right: auto; padding: 0;" buttons="#dlg_btn2">
    <input type="text" value="新建文件夹" id="NN" style=" margin:20px 30px; width:220px; height:25px;" />
    </div>
    <div id="dlg_btn2">
        <a href="#" class="easyui-linkbutton" onclick="javascript:$.FF.newFolder('#NN');">确定</a>
        <a href="#" id="" class="easyui-linkbutton" onclick="javascript:$('#dg_newfolder').dialog('close');">关闭</a>
    </div>
</body>
</html>
<script src="../scripts/jquery.FF.js" type="text/javascript"></script>
<script type="text/javascript">
    $.FF.config({
        s: '<%=Request.QueryString["s"] %>',
        pid: '<%=Request.QueryString["pid"] %>',        
        iconPath: '<%=ICON_PATH %>'
    });
</script>
