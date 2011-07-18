<%@ Page Language="C#" AutoEventWireup="true" EnableViewState="false" CodeFile="BindEquipment.aspx.cs"
    Inherits="SystemManagement_Experiment_BindEquipment" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>关联设备</title>
    <link href="../../scripts/jquery-easyui/themes/gray/easyui.css" rel="stylesheet"
        type="text/css" />
    <link href="../../scripts/jquery-easyui/themes/icon.css" rel="stylesheet" type="text/css" />
    <link href="../../Styles/_base.css" rel="stylesheet" type="text/css" />
    <script src="../../scripts/jquery-1.6.1.min.js" type="text/javascript"></script>
    <script src="../../scripts/jquery-easyui/jquery.easyui.min.js" type="text/javascript"></script>
    <style type="text/css">
        #etlist li
        {
            display: block;
            line-height: 25px;
            margin: 2px 0px;
            cursor: pointer;
        }
        li.active
        {
            background-color: #eb6;
            color: inherit;
            border: 1px solid #941;
            border-collapse: collapse;
        }
        #etlist li span
        {
            margin: 0px 10px;
            color: #c71;
            font-weight: bold;
        }
        #eqlist li
        {
            float: left;
            margin: 3px;
            padding: 5px 10px;
            background-color: #ede;
            cursor: pointer;
        }
        .tool
        {
            padding: 5px;
            background-color: #efefef;
        }
    </style>
</head>
<body class="easyui-layout">
    <div region="north" border="false" style="height: 28px; overflow: hidden;">
        <div id="toolbar">
            <a href="Default.aspx">返回列表</a>
        </div>
    </div>
    <div region="west" title="1 实验报告模板" split="true" style="width: 400px; overflow: auto;">
        <form id="form1" runat="server">
        <div class="tool" id="splist">
            <asp:Repeater ID="rptlistSpecialty" runat="server">
                <ItemTemplate><a class="button" id="<%#Eval("Id") %>">
                        <%#Eval("name") %></a> </ItemTemplate>
            </asp:Repeater>
        </div>
        <ul id="etlist">
            <asp:Repeater ID="rptlistET" runat="server">
                <ItemTemplate>
                    <li id="<%#Eval("Id") %>" spid="<%#Eval("SpecialtyId") %>"><span>
                        <%#((TSS.Models.Specialty)Eval("Specialty")).Name %></span>
                        <%#Eval("Title") %></li>
                </ItemTemplate>
            </asp:Repeater>
        </ul>
        </form>
    </div>
    <div region="east" title="3 已关联设备" split="true" style="width: 200px; overflow: auto;">
        <div class="tool" style="text-align: center;">
            <a class="easyui-linkbutton" plain="true" id="unbind" iconcls="icon-remove">解除关联选定设备</a>
        </div>
        <ul id="bindedlist">
        </ul>
    </div>
    <div region="center" title="2 可选设备">
        <table cellpadding="0" cellspacing="0" width="100%">
            <tr>
                <td colspan="2" align="right" class="tool">
                    <a href="javascript:void(0)" id="bind" class="easyui-linkbutton" plain="true" iconcls="icon-add">
                        关联选定设备</a>
                </td>
            </tr>
            <tr>
                <td style="width: 220px;" valign="top">
                    <ul id="eqCategoryTree">
                    </ul>
                </td>
                <td valign="top">
                    <ul id="eqlist">
                    </ul>
                </td>
            </tr>
        </table>
    </div>
</body>
</html>
<script type="text/javascript">
    /**
    * 肖宏飞
    * 2011-06-30
    */
    var opt = { s: "", et: "", c: "" };
    var defaultSelected = '<%=Request.QueryString["id"] %>';
    var handlerUrl = "../../Exptemplate.ashx";
    var $eq = $("#eqlist");

    $(document).ajaxError(function (event, request, settings) {
        alert("Ajax请求出错:" + settings.url);
    });

    $(function () {
        $("#splist a").click(function () {
            checkedSpecialty(this.id);
        });
        $("#etlist li").each(function (i) {
            var $this = $(this);
            $this.click(function () {
                $.extend(opt, { "s": this.getAttribute("spid"), "et": this.id });
                $("#etlist li.active").removeClass("active");
                $(this).toggleClass("active");
                loadEquipments();
                loadBinded();
            });
            if ((this.id == defaultSelected) || (defaultSelected == '' && i == 0)) {
                $this.click();
                checkedSpecialty(defaultSelected == "" ? "ALL" : this.getAttribute("spid"));
            }
        });

        //load equipment category
        $("#eqCategoryTree").load("../../equipmentcategory.ashx?type=xml&dp=0", function (data) {
            $("#eqCategoryTree").tree({
                checkbox: false, //不显示checkbox
                onClick: function (node) {
                    $("#eqCategoryTree").tree("expand", node.target);
                    opt.c = node.id;
                    loadEquipments();
                }
            });
        });

        $("#bind").click(function () {
            var query = { op: "bindequipment", etid: opt.et, eqid: "" };
            var checkNodes = getChecked();
            if (checkNodes.size() < 1) {
                return false;
            }
            query.eqid = checkNodes.map(function () {
                return this.value;
            }).get().join(";");
            //post
            $.post(handlerUrl, query, function (res) {
                if (res != "") {
                    alert(res);
                } else {
                    var lis = '';
                    checkNodes.each(function (i) {
                        if ($("#item_" + this.value).size() == 0) {
                            lis += '<li><input type="checkbox" value="' + this.value + '" />' + this.parentElement.innerText + '</li>';
                        }
                    });
                    $("#bindedlist").append(lis);
                }
            });
        });

        $("#unbind").click(function () {
            var query = { op: "unbindequipment", etid: opt.et, eqid: "" };
            var checkNodes = $("#bindedlist input:checked");
            if (checkNodes.size() < 1) {
                return false;
            }
            query.eqid = checkNodes.map(function () {
                return this.value;
            }).get().join(";");
            $.post(handlerUrl, query, function (res) {
                if (res != "") {
                    alert(res);
                } else {
                    checkNodes.parent("li").remove();
                }
            });
        });
    });

    function loadEquipments() {
        if (opt.et != "" && opt.c != "") {
            var query = { type: "equipments", spid: opt.s, id: opt.c };
            $.getJSON("../../EquipmentCategory.ashx", query, function (res) {
                var lis = '';
                for (var i = 0; i < res.length; i++) {
                    var __id = 'rd_' + res[i].id;
                    lis += '<li><input id="' + __id + '" type="checkbox" value="' + res[i].id + '" /><label for="' + __id + '">' + res[i].text + '</label></li>';
                }
                $(lis).appendTo($eq.empty());

            });
        }
    }

    function loadBinded() {
        var query = { op: "bindedEquipmentJSON", etid: opt.et };
        $.getJSON(handlerUrl, query, function (res) {
            var lis = '';
            for (var i = 0; i < res.length; i++) {
                lis += '<li><input type="checkbox" id="item_' + res[i].id + '" value="' + res[i].id + '" /><label for="item_'+res[i].id+'">' + res[i].text + '</label></li>';
            }
            $("#bindedlist").empty().append(lis);
        });
    }

    function checkedSpecialty(sp) {
        if (sp == "ALL") {
            $("#etlist li").show();
        } else {
            $("#etlist li").show().not("[spid='" + sp + "']").hide();
        }
        $("#splist a").removeClass("active");
        $("#" + sp).addClass("active");
    }

    function clearSelected() {
        var checkeds = getChecked();
        for (var i = 0; i < checkeds.length; i++) {
            $("#eqCategoryTree").tree("uncheck", checkeds[i].target);
        }
    }

    function getChecked() {
        //return $("#eqCategoryTree").tree("getChecked");
        return $("#eqlist input:checked");
    }
</script>
