<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Search.aspx.cs" Inherits="document_Search" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>文档搜索</title>
    <link href="listStyle.css" rel="stylesheet" type="text/css" />
    <link href="~/Styles/_base.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <div id="toolbar">
    <a href="Default.aspx?s=<%=Request.QueryString["s"] %>">返回</a>
        <div class="search">
            <asp:TextBox ID="txtKey" runat="server" class="textbox"></asp:TextBox>
            <asp:Button ID="btnSearch" runat="server" Text="" class="searchbtn" 
                onclick="btnSearch_Click" />
        </div>    
    </div>
    <ul id="mylist" style="margin-top: 32px;">
        <asp:Repeater ID="rptlist" runat="server">
            <ItemTemplate>
                <li><a href="javascript:void(0);" ondblclick="javascript:download('<%#Eval("Id") %>')" pid="<%#Eval("id") %>" isfolder="<%#Eval("isFolder") %>"
                    title="<%#Eval("name") %>">
                    <div class="ico" style="background: url(<%# getIcon(Eval("Path").ToString()) %>) no-repeat center center;">
                    </div>
                    <div class="filename">
                        <%#Eval("name") %>
                    </div>
                </a></li>
            </ItemTemplate>
        </asp:Repeater>
    </ul>
<asp:Literal runat="server" ID="ltmsg"></asp:Literal>
    </form>
</body>
</html>
<script type="text/javascript">
    function download(o) {
        document.location.href = "../upload.ashx?op=download&id=" + o;
    }
</script>