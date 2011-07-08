<%@ Page Language="C#" AutoEventWireup="true" ValidateRequest="false" EnableViewState="false"
    CodeFile="SetWorkflow.aspx.cs" Inherits="SystemManagement_Workflow_SetWorkflow" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>设置流程</title>
    <link href="../../Styles/_base.css" rel="stylesheet" type="text/css" />
    <link href="../../scripts/jquery-easyui/themes/gray/easyui.css" rel="stylesheet"
        type="text/css" />
    <script src="../../scripts/jquery-1.6.1.min.js" type="text/javascript"></script>
    <script src="../../scripts/jquery-easyui/jquery.easyui.min.js" type="text/javascript"></script>
    <style type="text/css">
        p
        {
            margin-top: 5px;
        }
        .weight
        {
            font-weight: bold;
            color: #FFFF99;
        }
        em
        {
            font-size: 120%;
            margin:10px 0px 5px 0px;
            display:block;
        }
        #mainNav div
        {
            margin: 10px 5px;
            border-top:1px solid #efefef;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div id="toolbar">
        <a href="Default.aspx">返回列表</a>
    </div>
    <div style=" width:100%;">
    <div style="width:500px; margin:10px auto;">
    <div class="easyui-panel" title="流程设置" style="width: 500px; height: 350px;">
        <div style="padding: 5px; background-color: #efefef;">
            流程名称：
            <asp:TextBox runat="server" ID="txtName" Style="width: 300px;" /> <a id="btnSave"
                class="button"><span class="check icon"></span> 保存流程</a>
            <div style=" text-align:right; margin-top:5px">
                <a id="btnNext" class="button">添加流程节点</a> 
                <a id="btnDel" class="button negative">删除流程节点</a></div>
        </div>
        <div id="mainNav">
            <asp:Repeater runat="server" ID="rptlist">
                <ItemTemplate>
                    <div hours="<%#Eval("hours") %>">
                        <em>Step
                            <%#Container.ItemIndex+1 %>: [<%#Eval("hours") %>小时]</em>
                        <%#Eval("t") %>
                    </div>
                </ItemTemplate>
            </asp:Repeater>
        </div>
    </div>
    </div>
    </div>
    <div id="dg_win" class="easyui-dialog" title="创建流程节点" modal="true" style="width: 400px;
        height: 400px; top: 80px; margin-left: auto; margin-right: auto; padding: 0;"
        buttons="#dlg_buttons">
        <div class="easyui-layout" style="width: 100%; height: 100%;">
            <div region="west" border="true" split="false" style="width: 200px; overflow: auto;">
                <ul id="tree_users" class="easyui-tree">
                    <asp:Literal ID="ltLI" runat="server"></asp:Literal>
                </ul>
            </div>
            <div region="center" border="false" style="overflow: auto;">
                <select id="selUsers" size="10" style="width: 150px; height: 180px; margin-left: 10px;">
                </select>
                <p>
                    设置完成时限：<input type="text" id="time" value="0" style="width: 50px;" />小时</p>
                <p style="color: Red; font-size: 9pt;">
                    1 双击用户名即可选择或者取消</p>
                <p style="color: Red; font-size: 9pt;">
                    2同一节点中多个用户参与时，需要<b> 选中 </b>权重大者</p>
            </div>
        </div>
    </div>
    <div id="dlg_buttons">
        <a href="#" id="createNode" class="easyui-linkbutton">创 建</a> <a href="#" class="easyui-linkbutton"
            onclick="javascript:$('#dg_win').dialog('close')">关闭</a>
    </div>
    <asp:HiddenField runat="server" ID="stepsCount" Value="0" />
    </form>
</body>
</html>
<script type="text/javascript" src="../../scripts/jquery.setworkflow.js">
</script>
<script type="text/javascript">
    queryId = '<%=Request.QueryString["id"] %>';
</script>