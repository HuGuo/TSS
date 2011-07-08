<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Certificate_Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>持证管理</title>
    <link href="~/Styles/_base.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        thead th
        {
            background-color: #e1e5ee;
            height: 27px;
        }
        td, td a
        {
            color: #666;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server" style=" padding-top:32px;">
    <div id="toolbar" class="fixed">
        <a href="Add.aspx?s=<%=Request.QueryString["s"] %>">添加持证信息</a>
        <div class="search">
            <asp:TextBox ID="txtKey" runat="server" class="textbox"></asp:TextBox>
            <asp:Button ID="btnSearch" runat="server" class="searchbtn" OnClick="btnSearch_Click"
                Text="" />
        </div>
    </div>
    <table id="tblist" cellpadding="0" cellspacing="0" style="width:100%;">
    <thead>
        <tr>
            <th>
                序号
            </th>
            <th>
                姓名
            </th>
            <th>
                性别
            </th>
            <th>
                作业种类
            </th>
            <th>
                资格项目
            </th>
            <th>
                培训单位
            </th>
            <th>
                取证时间
            </th>
            <th>
                证件有效期
            </th>
            <th>
                证书编号
            </th>
            <th>效果图</th>
            <th>
                备注
            </th>
            <th>
            </th>
        </tr>
        </thead>
        <asp:Repeater ID="rptList" runat="server" 
            onitemdatabound="rptList_ItemDataBound">
            <ItemTemplate>
                <tr id="tr_<%#Eval("id") %>">
                    <td align="center">
                        <%# Container.ItemIndex+1 %>
                    </td>
                    <td>
                        <%# Eval("EpmloyeeName")%>
                    </td>
                    <td>
                        <%# Eval("Gender") %>
                    </td>
                    <td>
                        <%# Eval("Type") %>
                    </td>
                    <td>
                        <%# Eval("Project") %>
                    </td>
                    <td>
                        <%# Eval("CretificationAuthority")%>
                    </td>
                    <td>
                        <%# Eval("ReceiveDateTime", "{0:yyyy-MM-dd}")%>
                    </td>
                    <td>
                        <%# Eval("ExpireDateTime", "{0:yyyy-MM-dd}")%>
                    </td>
                    <td>
                        <%# Eval("Number")%>
                    </td>
                    <td>
                    <%# string.IsNullOrWhiteSpace(Eval("ScanFilePath").ToString()) ? "" : string.Format("<a class='preview' href='{0}'>预览</a>" , ResolveUrl(Eval("ScanFilePath").ToString()))%>
                    </td>
                    <td>
                        <%# Eval("Remark")%>
                    </td>
                    <td align="center">
                        <asp:HyperLink ID="linkEdit" href='<%# "Add.aspx?id="+Eval("id").ToString()+"&s="+Eval("SpecialtyId").ToString() %>'
                            runat="server" class="button" Text="编辑" />
                        <asp:HyperLink ID="linkDel" NavigateUrl="javascript:void(0)" runat="server" class="delete button negative"
                            key='<%#Eval("id") %>' Text="删除" />
                    </td>
                </tr>
            </ItemTemplate>
        </asp:Repeater>
    </table>
    </form>
</body>
</html>
<script src="../scripts/jquery-1.6.1.min.js" type="text/javascript"></script>
<script src="../scripts/jquery.delete.js" type="text/javascript"></script>
<script src="../scripts/popImg/jquery.popImage.js" type="text/javascript"></script>
<script type="text/javascript">
    $(function () {
        $("a.delete").bindDelete({
            handler: "../delete.ashx",
            op: "del-certificate",
            onSuccess: function (k) { $("#tr_" + k).remove(); }
        });
        $("#tblist tr:gt(0)").alternateColor();
        $("a.preview").popImage();
    });
</script>
