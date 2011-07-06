<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Add.aspx.cs" Inherits="Certificate_Add" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>人员资质</title>
    <link rel="stylesheet" type="text/css"  href="~/Styles/_base.css"/>
    <script src="../scripts/My97DatePicker/WdatePicker.js" type="text/javascript"></script>
    <style type="text/css">
        .rounded-img
        {
            display: inline-block;
            border: solid 1px #000;
            overflow: hidden;
            -webkit-border-radius: 10px;
            -moz-border-radius: 10px;
            border-radius: 10px;
            -webkit-box-shadow: 0 1px 3px rgba(0, 0, 0, .4);
            -moz-box-shadow: 0 1px 3px rgba(0, 0, 0, .4);
            box-shadow: 0 1px 3px rgba(0, 0, 0, .4);
        }
        .linkbutton
        {
            background: url(../images/bglinkbutton.gif) no-repeat scroll 0 -4px;
            display: block;
            width:75px;
            line-height:26px;
            padding:0 10px;
            color:#000;
            text-align:center;
        }
        .linkbutton span
        {
            background: url(../images/bglinkbutton.gif) no-repeat scroll -230px -4px;
            padding:13px 5px;
            float:right;
            margin-right:-15px;
        }
        .linkbutton:hover
        {
            background-position: 0 -32px;
        }
        .linkbutton:hover span
        {
            background-position: -230px -32px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server" style=" padding-top:32px;">
    <div id="toolbar" class="fixed">
    <a href="Default.aspx?s=<%=Request.QueryString["s"] %>">返回列表</a>
    </div>

        <asp:Literal ID="ltmsg" runat="server"></asp:Literal>
    <table>
        <tr>
            <td>
                证书编号
            </td>
            <td>
                <asp:TextBox ID="txtNumber" runat="server"></asp:TextBox>
            </td>
            <td style="width: 20px;">
                &nbsp;</td>
            <td rowspan="11" valign="middle" align="center">
                <div style="width:450px; height:300px;">
                    <asp:Literal ID="ltimg" runat="server"></asp:Literal>
                </div></td>
        </tr>
        <tr>
            <td>
                证书效果图
            </td>
            <td>
                <asp:FileUpload ID="fileUp" runat="server" />
            </td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td>
                姓名
            </td>
            <td>
                <asp:TextBox ID="txtName" runat="server"></asp:TextBox>
            </td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td>
                性别
            </td>
            <td>
                <asp:DropDownList ID="ddlGender" runat="server">
                    <asp:ListItem Selected="True">男</asp:ListItem>
                    <asp:ListItem>女</asp:ListItem>
                </asp:DropDownList>
            </td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td>
                作业种类
            </td>
            <td>
                <asp:TextBox ID="txtType" runat="server"></asp:TextBox>
            </td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td>
                资格项目
            </td>
            <td>
                <asp:TextBox ID="txtProject" runat="server"></asp:TextBox>
            </td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td>
                培训单位
            </td>
            <td>
                <asp:TextBox ID="txtAuthor" runat="server"></asp:TextBox>
            </td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td>
                取证时间
            </td>
            <td>
                <asp:TextBox ID="txtReceiveDate" runat="server" class="Wdate" onclick="WdatePicker({maxDate:'%y-%M-%d'})"></asp:TextBox>
            </td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td>
                证件有效期
            </td>
            <td>
                <asp:TextBox ID="txtExpireDate" runat="server" class="Wdate" onclick="WdatePicker({minDate:'#F{$dp.$D(\'txtReceiveDate\');}'})"></asp:TextBox>
            </td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td>
                备注
            </td>
            <td>
                <asp:TextBox ID="txtRemark" runat="server"></asp:TextBox>
            </td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td>
                &nbsp;
            </td>
            <td>
                <asp:Button ID="btnSave" runat="server" Text="保存" OnClick="btnSave_Click" class="btn" OnClientClick="javascript:return checkNull(['txtReceiveDate', 'txtExpireDate'])" />
            </td>
            <td>
                &nbsp;</td>
        </tr>
    </table>
    <a class="linkbutton" href="#" style=" width:50px; display:none;">保存<span></span></a>
    </form>
</body>
</html>
<script type="text/javascript">
    function checkNull(o) {
        for (var i = 0; i < o.length; i++) {
            var obj = document.getElementById(o[i]);
            if (obj) {
                if (obj.value == "") {
                    obj.style.borderColor = "red";
                    obj.focus();
                    alert("信息不完整");
                    return false;
                }
            }
        }
    }
        
</script>