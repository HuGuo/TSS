<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" EnableViewState="false"
    Inherits="Certificate_Default" %>

<%@ Register Src="../UserControl/Pager.ascx" TagName="Pager" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>持证管理</title>
    <link href="~/Styles/_base.css" rel="stylesheet" type="text/css" />
    <link href="../Styles/datasheet.css" rel="stylesheet" type="text/css" />
    <link href="../scripts/popImg/jquery.fancybox-1.3.4.css" rel="stylesheet" type="text/css" />
    <script src="../scripts/jquery-1.6.1.min.js" type="text/javascript"></script>
    <!--easyui-->
    <link href="../Scripts/jquery-easyui/themes/gray/easyui.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/jquery-easyui/jquery.easyui.min.js" type="text/javascript"></script>
    <style type="text/css">
        #tblist tr.color1 td
        {
            background-color: #FFB6C1;
            color: inherit;
        }
        #tblist tr.color2 td
        {
            background-color: #FFEFBB;
            color: inherit;
        }
        .example span
        {
            padding: 1px 5px;
            border: 1px solid #ccc;
        }
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
    <div id="rowfilter" style="margin: 5px 15px;">
        <a class="button left status0" href="Default.aspx?s=<%=Request.QueryString[Helper.queryParam_specialty] %>&status=0">
            全部</a><a class="button middle status1" href="Default.aspx?s=<%=Request.QueryString[Helper.queryParam_specialty] %>&status=1">未到期</a><a
                class="button middle status2" href="Default.aspx?s=<%=Request.QueryString[Helper.queryParam_specialty] %>&status=2">30天内到期</a><a
                    class="button right status3" href="Default.aspx?s=<%=Request.QueryString[Helper.queryParam_specialty] %>&status=3">已过期</a>
        <div class="example" style="float: right; margin-right: 10px;">
            <span style="background-color: #FFB6C1">已过期</span> <span style="background-color: #FFEFBB">
                30天内到期</span>
        </div>
    </div>
    <div class="wrap">
        <div class="wrap_inner">
            <table id="tblist" class="datasheet" cellpadding="0" cellspacing="0">
                <thead>
                    <tr>
                        <th style="width: 20px;">
                        </th>
                        <th style="width: 40px;">
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
                        <th style="width: 100px;">
                            证书编号
                        </th>
                        <th style="width: 40px;">
                            效果图
                        </th>
                        <th>
                            备注
                        </th>
                        <th style="width:60px;">
                        </th>
                    </tr>
                </thead>
                <tbody>
                    <asp:Repeater ID="rptList" runat="server" OnItemDataBound="rptList_ItemDataBound">
                        <ItemTemplate>
                            <tr id="tr_<%#Eval("id") %>" class="<%# SetColor((DateTime)Eval("ExpireDateTime")) %>">
                                <th align="center">
                                    <%# PageSize * (PageIndex-1) + Container.ItemIndex + 1%>
                                </th>
                                <td style="padding: 0; text-align: center;">
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
                                <td style="width: 70px;">
                                    <%# Eval("ReceiveDateTime", "{0:yyyy-MM-dd}")%>
                                </td>
                                <td style="width: 70px;">
                                    <%# Eval("ExpireDateTime", "{0:yyyy-MM-dd}")%>
                                </td>
                                <td>
                                    <%# Eval("Number")%>
                                </td>
                                <td>
                                    <%# string.IsNullOrWhiteSpace(Eval("ScanFilePath").ToString()) ? "" : string.Format("<a class='preview' href='{0}'><img style='display:none' />预览</a>" , ResolveUrl(Eval("ScanFilePath").ToString()))%>
                                </td>
                                <td style="width: 80px;">
                                    <%# Eval("Remark")%>
                                </td>
                                <td align="center">
                                    <asp:HyperLink ID="linkEdit" href='<%# "Add.aspx?id="+Eval("id").ToString()+"&s="+Eval("SpecialtyId").ToString() %>'
                                        runat="server" Text="编辑" />
                                    <asp:HyperLink ID="linkDel" NavigateUrl="javascript:void(0)" runat="server" class="delete"
                                        key='<%#Eval("id") %>' Text="删除" />
                                </td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                </tbody>
            </table>
            <uc1:Pager ID="Pager1" runat="server" />
        </div>
    </div>
    </form>
</body>
</html>
<script src="../Scripts/jquery.tableStyle.js" type="text/javascript"></script>
<script src="../scripts/popImg/jquery.fancybox-1.3.4.pack.js" type="text/javascript"></script>
<script type="text/javascript">
    /**
    * 肖宏飞
    * 2011-7-20
    */
    
    $(function () {
        var rows = $("#tblist tr:gt(0)");
        var ss = '<%=Request.QueryString["status"] %>';
        if (ss == "") {
            ss = "0";
        }
        
        $("a.status"+ss).addClass("active");
        //bind delete
        $("a.delete").bindDelete({
            handler: "../delete.ashx",
            op: "del-certificate",
            onSuccess: function (k) { $("#tr_" + k).remove(); }
        });

        //preview images
        $("a.preview").fancybox({
            'overlayColor': '#777',
            'overlayOpacity': 0.3
        });

        //filter rows
        //        filters.click(function () {
        //            var cls = this.getAttribute("filter");
        //            filters.removeClass("active");
        //            $(this).addClass("active");

        //            if (cls == "ALL") {
        //                rows.show();
        //            } else {
        //                rows.hide().filter(cls).show();
        //            }
        //        });
    });
</script>
