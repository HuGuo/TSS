<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>技术监督管理系统</title>
    <link href="scripts/jquery-easyui/thems/gray/easyui.css" rel="stylesheet" type="text/css" />
    <link href="scripts/jquery-easyui/thems/icon.css" rel="stylesheet" type="text/css" />
    <script src="scripts/jquery-1.6.1.min.js" type="text/javascript"></script>
    <script src="scripts/jquery-easyui/jquery.easyui.min.js" type="text/javascript"></script>
    <style>
    a{ text-decoration:none; color:#00527f}
    </style>
</head>
<body class="easyui-layout">
    <div region="west" title="系统菜单" split="true" border="true" iconCls="icon-favcenter" style="width: 220px; max-width: 350px;
        min-width: 150px;">
        <div id="menu_accordion" class="easyui-accordion" fit="true" border="false" style="background-color:#e6e6e6">        
            <div title="专业监督" selected="true" style="overflow: auto;">
                <ul class="easyui-tree" id="specialtyTree">
                    <li  state="open"><span>绝缘监督</span>
                        <ul>
                            <li><span><a href="javascript:void(0);" target="frm_main">设备台帐</a></span></li>
                            <li><span><a href="Certificate/Default.aspx?s=GHY-JY" target="frm_main">人员资质</a></span></li>
                            <li><span><a href="javascript:void(0);" target="frm_main">设备预试周期</a></span></li>
                            <li><span><a href="Experiment/Default.aspx?s=GHY-JY" target="frm_main">实验报告</a></span></li>
                            <li><span><a href="javascript:void(0);" target="frm_main">实验台帐</a></span></li>
                            <li><span><a href="javascript:void(0);" target="frm_main">监督月报</a></span></li>
                            <li><span><a href="javascript:void(0);" target="frm_main">档案资料</a></span></li>
                        </ul>
                    </li>
                    <li state="closed"><span>化学监督</span>
                        <ul>
                            <li><span><a href="javascript:void(0);" target="frm_main">设备台帐</a></span></li>
                            <li><span><a href="javascript:void(0);" target="frm_main">人员资质</a></span></li>
                            <li><span><a href="javascript:void(0);" target="frm_main">设备预试周期</a></span></li>
                            <li><span><a href="javascript:void(0);" target="frm_main">实验报告</a></span></li>
                            <li><span><a href="javascript:void(0);" target="frm_main">实验台帐</a></span></li>
                            <li><span><a href="javascript:void(0);" target="frm_main">监督月报</a></span></li>
                            <li><span><a href="javascript:void(0);" target="frm_main">档案资料</a></span></li>
                        </ul>
                    </li>
                </ul>
            </div>
            <div title="系统管理" iconcls="icon-tools" style="overflow: auto;">
                <ul class="easyui-tree" id="sysTree">
                    <li><span>系统管理</span>
                        <ul>
                            <li><span><a href="SystemManagement/Experiment/Default.aspx" target="frm_main">实验报告模板</a></span></li>
                            <li><span>设备管理</span></li>
                            <li><span><a href="SystemManagement/Workflow/Default.aspx" target="frm_main">流程设置</a></span></li>
                        </ul>
                    </li>
                </ul>
            </div>
            <div title="连接" iconCls="icon-tools"  style="overflow: auto;">
            模板下载
            </div>
        </div>
    </div>
    <div region="center" style="margin:0; padding:0;">
        <iframe name="frm_main" scrolling="auto" frameborder="0" src="about:blank" style="width: 100%;
            height: 99%;"></iframe>
    </div>
    <div region="south" style="height: 26px; background: #D2E0F2; padding: 1px 20px;
        text-align: right;">
        <input type="text" value="深 圳 天 道 数 字 工 程 有 限 公 司" style="border: 0; background-color: #D2E0F2;
            width: 240px; height: 16px;" disabled="disabled" />
    </div>
</body>
</html>
<script type="text/javascript">
    $(document).ready(function () {
        $("#menu_accordion").accordion("select", "专业监督");
    });
</script>