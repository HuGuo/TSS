<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="SystemManagement_Employee_Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>用户列表</title>
    <link href="../../Styles/_base.css" rel="stylesheet" type="text/css" />
    <link href="../../scripts/jquery-easyui/themes/gray/easyui.css" rel="stylesheet"
        type="text/css" />
    <script src="../../scripts/jquery-1.6.1.min.js" type="text/javascript"></script>
    <script src="../../scripts/jquery-easyui/jquery.easyui.min.js" type="text/javascript"></script>
</head>
<body>
    <form id="form1" runat="server" style="padding-top: 32px;">
    <div id="toolbar" class="fixed">
    <a href="RoleDefault.aspx">角色管理</a>
        <div class="search">
            <asp:TextBox ID="txtKey" runat="server" class="textbox" MaxLength="12"></asp:TextBox>
            <asp:Button ID="btnSearch" runat="server" class="searchbtn" OnClick="btnSearch_Click"
                Text="" />
        </div>
    </div>
    <table>
        <thead>
            <tr>
            <th></th>
            <th>登录帐号</th>
            <th>专业</th>
            <th>操作</th>
            </tr>
        </thead>
        <asp:repeater runat="server" ID="rptlist">
        <ItemTemplate>
        <tr>
        <td><%#Container.ItemIndex+1 %></td>
        <td><%#Eval("Name") %></td>
        <td><%#((TSS.Models.Specialty)(Eval("Specialty"))).Name%></td>
        <td>
        <a href="javascript:sss('<%#Eval("Id") %>');">编辑</a>
        </td>
        </tr>
        </ItemTemplate>
        </asp:repeater>
    </table>
    <div id="dg_win" class="easyui-dialog" title="设置用户信息" style="width: 500px; height: 300px;
        top: 100px; margin-left: auto; margin-right: auto; padding: 0;" buttons="#dlg_buttons">
        <table cellpadding="0" cellspacing="0" style="width: 100%">
            <tr>
                <td colspan="3" style="height: 40px; border-bottom:1px dotted #e2e2e2; padding-left:15px;">
                
                    <div id="div1"><span id="employeeName" style=" margin-right:10px; font-size:14px; font-weight:bolder;">
                </span>
                        <label for="spName">
                            专业：</label><span id="spName" style=" padding:5px 8px; border:1px solid #e2e2e2; font-weight:700;"></span>
                            <a href="#" id="changeSP" style=" margin-left:8px; text-decoration:none;">更改</a>
                            </div>
                    <div id="div2" style="display: none;">
                        <asp:DropDownList ID="ddlSpecialty" runat="server">
                        </asp:DropDownList>
                        <input type="button" id="btnOk" value="确定" />
                        <input type="button" id="btnCancel" value="取消" />
                    </div>
                </td>
            </tr>
            <tr>
                <td align="center">
                    <select id="selLeft" size="5" style="width: 200px; height: 180px;">
                    </select>
                </td>
                <td align="center" width="50px" style="width: 50px;">
                    <input type="button" id="btnadd" value="《《" />
                    <br />
                    <br />
                    <input type="button" id="btnRemove" value="》》" />
                </td>
                <td align="center">                
                    <asp:DropDownList runat="server" ID="selRight" size="5" Style="width: 200px; height: 180px;">
                    </asp:DropDownList>
                </td>
            </tr>
        </table>
    </div>
    <div id="dlg_buttons">
        <a href="#" class="easyui-linkbutton" onclick="javascript:$('#dg_win').dialog('close')">
            关闭</a>
    </div>
    </form>
</body>
</html>
<script type="text/javascript">
    $(function () {
        $(document).ajaxError(function (event, request, settings) {
            alert("出错页面:" + settings.url );
        });
        var g = {
            handlerUrl: "../../employee.ashx",
            ll: function (o) {
                var query = $.extend({ op: "ur", act: "d", id: $("#div1").data("employeeId"), rid: "", onSuccess: null }, o);                
                $.get(this.handlerUrl, query, function (res) {
                    if (res == "") {
                        query.onSuccess;
                    } else {
                        alert(res);
                    }
                });
            }
        };
        $('#dg_win').dialog('close');
        $("#changeSP,#btnCancel").click(function () {
            $("#div1,#div2").toggle("fast");
        });
        //
        var $selL = $("#selLeft");
        var $selR = $("#selRight");
        $selL.dblclick(function (e) { $("#btnRemove").click(); e.preventDefault(); });
        $selR.dblclick(function (e) { $("#btnadd").click(); e.preventDefault(); });

        $("#btnadd").click(function () {
            var $option = $selR.find("option:selected");
            if ($option.size() < 1) {
                return;
            }
            g.ll({ rid: $option.val(), onSuccess: function () {
                $selL.append('<option value="' + $option.val() + '">' + $option.text() + '</option>');
            }
            });
        });

        $("#btnRemove").click(function () {
            var $option = $selL.find("option:selected");
            if ($option.size() < 1) {
                return;
            }
            g.ll({ rid: $option.val(), onSuccess: function () {
                $option.remove();
            }
            });
        });

        //更改专业
        $("#btnOk").click(function () {
            var spid = $("#div1").data("specialtyId");
            var opt = $("#ddlSpecialty option:selected");

            var query = { op: "us", id: $("#div1").data("employeeId"), sp: "" };
            query.sp = opt.val();
            if (spid != query.sp) {
                $.get("../../employee.ashx", query, function (data) {
                    if (data != "") {
                        alert(data); return false;
                    } else {
                        $("#spName").text(opt.text());
                        $("#div1").data("specialtyId", query.sp);
                        $.messager.show({
                            title: '系统提示',
                            msg: '专业更新成功',
                            timeout: 3000,
                            showType: 'slide'
                        });
                    }
                });
            }
            $("#div1,#div2").toggle("fast");
        });
    });
    function sss(o) {
        var query = { OP: "get", id: o, rand: Math.random() };
        $('#dg_win').dialog('open');
        $.getJSON("../../employee.ashx", query, function (json) {
            if (json.msg) {
                alert(json.msg); return false;
            } else {
                $("#selLeft option").remove();
                var c = json.roles.length; 
                var items = "";
                for (var i = 1; i < c; i++) {
                    items += '<option value="' + json.roles[i].id + '">' + json.roles[i].name + '</option>';
                }
                $("#selLeft").append(items);
                $("#spName").text(json.specialtyName);
                $("#employeeName").text(json.name);

                $("#div1").data("employeeId", json.id).data("specialtyId", json.specialtyId);
            }
        });
    }
</script>
