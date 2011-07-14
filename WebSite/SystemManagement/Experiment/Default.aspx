<%@ Page Language="C#" AutoEventWireup="true" EnableViewState="false" CodeFile="Default.aspx.cs"
    Inherits="SystemManagement_Experiment_Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>试验报告模板</title>
    <link href="../../Styles/_base.css" rel="stylesheet" type="text/css" />
    <link href="../../scripts/jquery-easyui/themes/gray/panel.css" rel="stylesheet" type="text/css" />
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
    <form id="form1" runat="server" style="padding-top: 32px;">
    <div id="toolbar" class="fixed">
        <a href="ExpRecordDefault.aspx">试验台帐</a> <a href="setTemplate.aspx" target="_blank">
            添加模板</a> <a href="BindEquipment.aspx">关联设备</a>
    </div>

            <div id="splist" style=" margin:5px 10px;">
                <asp:Repeater ID="rptlistSpecialty" runat="server">
                    <ItemTemplate><a id="<%#Eval("Id") %>" class="button middle"><%#Eval("Name") %></a></ItemTemplate>
                </asp:Repeater>
            </div>
            <table id="tb" cellpadding="0" cellspacing="0" style="width: 100%; border: 0;">
                <thead>
                    <tr>
                        <th style="width: 50px;">
                            序号
                        </th>
                        <th>
                            专业
                        </th>
                        <th>
                            模板名称
                        </th>
                        <th>
                        </th>
                        <th style="width: 120px;">
                            操作
                        </th>
                    </tr>
                </thead>
                <asp:Repeater ID="rptList" runat="server" OnItemDataBound="rptList_ItemDataBound">
                    <ItemTemplate>
                        <tr id="tr_<%#Eval("id") %>" spid="<%#Eval("SpecialtyId") %>">
                            <td align="center">
                                <%# Container.ItemIndex+1 %>
                            </td>
                            <td style="color: #c71;">
                                <%#((TSS.Models.Specialty)Eval("Specialty")).Name %>
                            </td>
                            <td>
                                <a href="PreView.aspx?id=<%#Eval("Id") %>" target="_blank">
                                    <%#Eval("Title") %></a>
                            </td>
                            <td>
                                <asp:HyperLink ID="linkBindEQ" href='<%#Eval("id","BindEquipment.aspx?id={0}") %>'
                                    runat="server" Text="关联设备" Target="_blank" class="button" />
                            </td>
                            <td align="center">
                                <asp:HyperLink ID="linkEdit" href='<%#Eval("id","setTemplate.aspx?tid={0}") %>' runat="server"
                                    Text="编 辑" Target="_blank" class="button" />
                                <asp:HyperLink ID="linkDel" NavigateUrl="javascript:void(0);" key='<%#Eval("id") %>'
                                    class="delete button negative" runat="server" Text="删 除" />
                            </td>
                        </tr>
                    </ItemTemplate>
                </asp:Repeater>
            </table>
    </form>
</body>
</html>
<script src="../../scripts/jquery-1.6.1.min.js" type="text/javascript"></script>
<script src="../../scripts/jquery.delete.js" type="text/javascript"></script>
<script type="text/javascript">
    var handlerUrl = "../../Exptemplate.ashx";
    $(function () {
        $("a.delete").bindDelete({
            handler: handlerUrl,
            op: "del-t"
        });
        //$("#tb tr:gt(0)").hoverColor();
        $("#tb tr:gt(0)").alternateColor().hoverColor();

        var sps = $("#splist a");
        sps.first().addClass("left").addClass("active").removeClass("middle");
        sps.last().addClass("right").removeClass("middle");
        sps.click(function () {
            sps.removeClass("active");
            $(this).addClass("active");
            //过滤
            if (this.id == "ALL") {
                $("#tb tr:gt(0)").show();
            } else {
                $("#tb tr:gt(0)").hide().filter("[spid='" + this.id + "']").show();
            }
        });
    });
</script>
