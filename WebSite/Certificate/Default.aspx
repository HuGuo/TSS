<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Certificate_Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>持证管理</title>
    <link href="~/Styles/_base.css" rel="stylesheet" type="text/css" />
    <link href="../scripts/popImg/jquery.fancybox-1.3.4.css" rel="stylesheet" type="text/css" />
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
        #tblist tr.color1 td
        {
            background-color: #FFA07A; color:inherit;
            border-bottom:1px solid #FFF;
        }
        #tblist tr.color2 td
        {
            background-color: #FFE4B5; color:inherit;
        }
        .example span{ padding:1px 5px; border:1px solid #ccc;}
    </style>
</head>
<body>
    <form id="form1" runat="server" style="padding-top: 32px;">
    <div id="toolbar" class="fixed">
        <asp:HyperLink ID="linkAdd" runat="server" Visible="false">添加持证信息</asp:HyperLink>
        <div class="search">
            <asp:TextBox ID="txtKey" runat="server" class="textbox"></asp:TextBox>
            <asp:Button ID="btnSearch" runat="server" class="searchbtn" OnClick="btnSearch_Click"
                Text="" />
        </div>
    </div>
    <div style="margin: 5px 15px;" id="rowfilter">
        <a class="button left active" filter="ALL">全部</a><a class="button middle" filter=".normal,.color2">未到期</a><a
            class="button middle" filter=".color2">30天内到期</a><a class="button right" filter=".color1">已过期</a>
            <div class="example" style="float:right; margin-right:10px;">
            <span style="background-color: #FFA07A">已过期</span>
            <span style="background-color: #FFE4B5">30天内到期</span>
            </div>
    </div>
    <table id="tblist" cellpadding="0" cellspacing="0" style="width: 100%;">
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
                <th>
                    效果图
                </th>
                <th>
                    备注
                </th>
                <th>
                </th>
            </tr>
        </thead>
        <asp:Repeater ID="rptList" runat="server" OnItemDataBound="rptList_ItemDataBound">
            <ItemTemplate>
                <tr id="tr_<%#Eval("id") %>" class="<%# SetColor((DateTime)Eval("ExpireDateTime")) %>">
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
                        <%# string.IsNullOrWhiteSpace(Eval("ScanFilePath").ToString()) ? "" : string.Format("<a class='preview' href='{0}'><img style='display:none' />预览</a>" , ResolveUrl(Eval("ScanFilePath").ToString()))%>
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
<script src="../scripts/popImg/jquery.fancybox-1.3.4.pack.js" type="text/javascript"></script>
<script type="text/javascript">
    $(function () {
        var rows = $("#tblist tr:gt(0)");
        var filters = $("#rowfilter a");
        //bind delete
        $("a.delete").bindDelete({
            handler: "../delete.ashx",
            op: "del-certificate",
            onSuccess: function (k) { $("#tr_" + k).remove(); }
        });

        rows.alternateColor();

        //preview images
        $("a.preview").fancybox({
            'overlayColor': '#777',
            'overlayOpacity': 0.3
        });

        //filter rows
        filters.click(function () {
            var cls = this.getAttribute("filter");
            filters.removeClass("active");
            $(this).addClass("active");

            if (cls == "ALL") {
                rows.show();
            } else {
                rows.hide().filter(cls).show();
            }
        });
    });
</script>
