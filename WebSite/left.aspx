<%@ Page Language="C#" AutoEventWireup="true" CodeFile="left.aspx.cs" Inherits="left" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script src="content/left_tree.js" type="text/javascript"></script>
</head>
<body class="leftbg" style="overflow: auto; height: 500px; width: 195px">
    <div class="lefttopbg">
    </div>
    <div class="L_top">
    </div>
    <div id="menu">
        <ul>
            <li class="L1" id="L1"><a href="javascript:c('m01');" id="m01" class="add2">监督动态</a></li>
            <ul id="m01d" style="display: none;" class="U1">
                <li class="L22" runat="server" id="li_1"><a href="jddt/bcxx/showNews.aspx" target="fmain">
                    <div class="img">
                    </div>
                    本厂信息</a></li>
                <li class="L22" runat="server" id="li_2"><a href="jddt/ghxx/showNews.aspx" target="fmain">
                    <div class="img">
                    </div>
                    国华信息</a></li>
                <li class="L22" runat="server" id="li_3"><a href="jddt/hyxx/showNews.aspx" target="fmain">
                    <div class="img">
                    </div>
                    行业信息</a></li>
            </ul>
            <li class="L1"><a href="javascript:c('m02');" id="m02" class="add2">监督体系</a></li>
            <ul id="m02d" style="display: none;" class="U1">
                <li class="L22" runat="server" id="li_5"><a href="jdtx/bczd/LawIndex.aspx" target="fmain">
                    <div class="img">
                    </div>
                    本厂制度</a></li>
                <li class="L22" runat="server" id="li_4"><a href="http://10.1.206.193:8081" title="国华制度 有问题请联系国网信息中心：李凌云010-63415431"
                    target="_blank">
                    <div class="img">
                    </div>
                    国华制度</a></li>
                <li class="L22" runat="server" id="li_6"><a href="jdtx/jdwl/SjWlIndex.aspx" target="fmain">
                    <div class="img">
                    </div>
                    监督网络</a></li>
                <li class="L22" runat="server" id="li_31"><a href="javascript:c('ul_31');" id="ul_31">
                    <div class="img">
                    </div>
                    资质管理</a></li>
                <ul id="ul_31d" runat="server" style="display: none; margin-left: 15px;">
                    <li class="L3"><a href="jdtx/zzgl/dwzz_index.aspx" target="fmain">
                        <div class="mimg3">
                        </div>
                        单位资质</a></li>
                    <li class="L3"><a href="jdtx/zzgl/czgl_Index.aspx" target="fmain">
                        <div class="mimg3">
                        </div>
                        持证管理</a></li>
                </ul>
            </ul>
            <li class="L1"><a href="javascript:c('m03');" id="m03" class="add2">监督管理</a></li>
            <ul id="m03d" style="display: none;" class="U1">
                <li class="L22" runat="server" id="li_8"><a href="javascript:c('ul_8');" id="ul_8">
                    <div class="img">
                    </div>
                    报表管理</a></li>
                <ul id="ul_8d" style="display: none; margin-left: 15px;" runat="server">
                    <li class="L3"><a href="jdgl/zhbb/BbGlIndex.aspx" target="fmain">
                        <div class="mimg3">
                        </div>
                        综合报表</a></li>
                    <li class="L3"><a href="jdgl/pdf/FdgsZcIndex.aspx?zy=<%=Server.UrlEncode("年度总结计划") %>"
                        target="fmain">
                        <div class="mimg3">
                        </div>
                        年度总结计划</a></li>
                    <li class="L3"><a href="jdgl/pdf/FdgsZcIndex.aspx?zy=<%=Server.UrlEncode("重大事件") %>"
                        target="fmain">
                        <div class="mimg3">
                        </div>
                        重大事件</a></li>
                    <li class="L3"><a href="jdgl/pdf/FdgsZcIndex.aspx?zy=<%=Server.UrlEncode("专题报告") %>"
                        target="fmain">
                        <div class="mimg3">
                        </div>
                        专题报告</a></li>
                </ul>
                <li class="L22" runat="server" id="li_9"><a href="javascript:c('ul_9');" id="ul_9">
                    <div class="img">
                    </div>
                    异常管理</a></li>
                <ul id="ul_9d" style="display: none; margin-left: 15px;" runat="server">
                    <li class="L3"><a href="jdgl/Ycsb/Ycsb.aspx?type=overtime" target="fmain">
                        <div class="mimg3">
                        </div>
                        异常识别</a></li>
                    <li class="L3"><a href="jdgl/ycgj/ycgjIndex.aspx" target="fmain">
                        <div class="mimg3">
                        </div>
                        异常告警</a></li>
                    <li class="L3"><a href="jdgl/yczg/index.aspx" target="fmain">
                        <div class="mimg3">
                        </div>
                        异常整改</a></li>
                    <li class="L3"><a href="jdgl/yczg/yccl.aspx" target="fmain">
                        <div class="mimg3">
                        </div>
                        异常处理检查评价</a></li>
                    <li class="L3"><a href="jdgl/pdf/FdgsZcIndex.aspx?zy=<%=Server.UrlEncode("发电公司自查") %>"
                        target="fmain">
                        <div class="mimg3">
                        </div>
                        发电公司自查</a> </li>
                </ul>
                <li class="L22" runat="server" id="li_55"><a href="zyjd/wwxm/Index.aspx?zy=<%#Server.UrlEncode("化学") %>&year=<%=System.DateTime.Now.Year.ToString() %>"
                    target="fmain">
                    <div class="img">
                    </div>
                    外委项目</a></li>
            </ul>
            <li class="L1"><a href="javascript:c('m04');" id="m04" class="add2">专业监督</a></li>
            <ul id="m04d" style="display: none;" class="U1">
                <li class="L22" runat="server" id="li_11"><a href="javascript:c('ul_11');" id="ul_11">
                    <div class="img">
                    </div>
                    化学监督</a></li>
                <ul id="ul_11d" runat="server" style="display: none; margin-left: 15px;">
                    
                </ul>
                <li class="L22" runat="server" id="li_12"><a href="javascript:c('ul_12');" id="ul_12">
                    <div class="img">
                    </div>
                    电能监督</a></li>
                <ul id="ul_12d" runat="server" style="display: none; margin-left: 15px;">
                    
                </ul>
                <li class="L22" runat="server" id="li_13"><a href="javascript:c('ul_13');" id="ul_13">
                    <div class="img">
                    </div>
                    金属监督</a></li>
                <ul id="ul_13d" runat="server" style="display: none; margin-left: 15px;">
                    
                </ul>
                <li class="L22" runat="server" id="li_14"><a href="javascript:c('ul_14');" id="ul_14">
                    <div class="img">
                    </div>
                    绝缘监督</a></li>
                <ul runat="server" id="ul_14d" style="display: none; margin-left: 15px;">
                    <li class="L3"><a href="javascript:void(0);" target="fmain">
                        <div class="mimg3">
                        </div>
                        设备台帐</a></li>
                    <li class="L3"><a href="javascript:void(0);" target="fmain">
                        <div class="mimg3">
                        </div>
                        人员资质</a></li>
                    <li class="L3"><a href="javascript:void(0);" target="fmain">
                        <div class="mimg3">
                        </div>
                        设备预试周期</a></li>
                    <li class="L3"><a href="javascript:void(0);" target="fmain">
                        <div class="mimg3">
                        </div>
                        实验报告</a></li>
                    <li class="L3"><a href="javascript:void(0);" target="fmain">
                        <div class="mimg3">
                        </div>
                        试验台帐</a></li>
                    <li class="L3"><a href="javascript:void(0);" target="fmain">
                        <div class="mimg3">
                        </div>
                        监督月报</a></li>
                    <li class="L3"><a href="javascript:void(0);" target="fmain">
                        <div class="mimg3">
                        </div>
                        档案资料</a></li>
                </ul>
                <li class="L22" runat="server" id="li_15"><a href="javascript:c('ul_15');" id="ul_15">
                    <div class="img">
                    </div>
                    热工监督</a></li>
                <ul id="ul_15d" runat="server" style="display: none; margin-left: 15px;">
                    
                </ul>
                <li class="L22" runat="server" id="li_16"><a href="javascript:c('ul_16');" id="ul_16">
                    <div class="img">
                    </div>
                    电测监督</a></li>
                <ul id="ul_16d" runat="server" style="display: none; margin-left: 15px;">
                    
                </ul>
                <li class="L22" runat="server" id="li_17"><a href="javascript:c('ul_17');" id="ul_17">
                    <div class="img">
                    </div>
                    环境保护监督</a></li>
                <ul id="ul_17d" runat="server" style="display: none; margin-left: 15px;">
                    
                </ul>
                <li class="L22" runat="server" id="li_18"><a href="javascript:c('ul_18');" id="ul_18">
                    <div class="img">
                    </div>
                    继电保护监督</a></li>
                <ul id="ul_18d" runat="server" style="display: none; margin-left: 15px;">
                    
                </ul>
                <li class="L22" runat="server" id="li_19"><a href="javascript:c('ul_19');" id="ul_19">
                    <div class="img">
                    </div>
                    节能监督</a></li>
                <ul id="ul_19d" runat="server" style="display: none; margin-left: 15px;">
                    
                </ul>
                <li class="L22" runat="server" id="li_21"><a href="javascript:c('ul_21');" id="ul_21">
                    <div class="img">
                    </div>
                    汽机振动监督</a></li>
                <ul id="ul_21d" runat="server" style="display: none; margin-left: 15px;">
                    
                </ul>
                <li class="L22" runat="server" id="li_24"><a href="javascript:c('ul_24');" id="ul_24">
                    <div class="img">
                    </div>
                    励磁监督</a></li>
                <ul id="ul_24d" runat="server" style="display: none; margin-left: 15px;">
                    
                </ul>
                <li class="L22" runat="server" id="li_25"><a href="javascript:c('ul_25');" id="ul_25">
                    <div class="img">
                    </div>
                    水工监督</a></li>
                <ul id="ul_25d" runat="server" style="display: none; margin-left: 15px;">
                    
                </ul>
                <li class="L22" runat="server" id="li_20"><a href="javascript:c('ul_20');" id="ul_20">
                    <div class="img">
                    </div>
                    锅炉压力容器监督</a></li>
                <ul id="ul_20d" runat="server" style="display: none; margin-left: 15px;">
                    
                </ul>
                <li class="L22" runat="server" id="li_22"><a href="javascript:c('ul_22');" id="ul_22">
                    <div class="img">
                    </div>
                    监控监督</a></li>
                <ul id="ul_22d" runat="server" style="display: none; margin-left: 15px;">
                    
                </ul>
            </ul>
            <li class="L1" runat="server" id="li_10"><a href="javascript:c('m07');" id="m07"
                class="add2">系统管理</a></li>
            <ul id="m07d" style="display: none;" class="U1">
                <li class="L22" runat="server" id="li_26"><a href="sysmange/userlist.aspx" target="fmain">
                    <div class="img">
                    </div>
                    用户管理</a></li>
                <li class="L22" runat="server" id="li_27"><a href="sysmange/DepementList.aspx" target="fmain">
                    <div class="img">
                    </div>
                    部门管理</a></li>
                <li class="L22" runat="server" id="li_28"><a href="sysmange/RoleList.aspx" target="fmain">
                    <div class="img">
                    </div>
                    角色管理</a></li>
                <li class="L22" runat="server" id="li_29"><a href="sysmange/setflow.aspx" target="fmain">
                    <div class="img">
                    </div>
                    流程设定</a></li>
                <li class="L22" runat="server" id="li_30"><a href="sysmange/loginlog.aspx" target="fmain">
                    <div class="img">
                    </div>
                    登录日志</a></li>
            </ul>
        </ul>
    </div>
</body>
</html>
<script type="text/javascript" language="JavaScript">
    var cur_id = "", cur_expand = "";
    var flag = 0, sflag = 0;

    //-------- 菜单点击事件 -------
    function c(id) {
        var targetid, targetelement;
        var strbuf;

        var el = $(id);
        if (!el)
            return;
        //-------- 如果点击了展开或收缩按钮---------
        targetid = el.id + "d";

        targetelement = document.getElementById(targetid);
        var expandUL = document.getElementById(cur_expand + "d");
        var expandLink = document.getElementById(cur_expand);
        var L1 = document.getElementById(L1);

        if (targetelement.style.display == "none") {
            if (expandUL && expandLink && el.id.substr(0, 1) == "m") {
                expandLink.className = "add";
                expandUL.style.display = 'none';
            }
            if (el.id.substr(0, 1) == "m")
                cur_expand = el.id;
            el.className = "dec";
            targetelement.style.display = '';

        }
        else {
            el.className = "add";
            targetelement.style.display = "none";

            menu_flag = 1;
            var links = document.getElementsByTagName("A");
            for (i = 0; i < links.length; i++) {
                el = links[i];
                if (el.parentNode.className.toUpperCase() == "L1" && el.className == "active" && el.id.substr(0, 1) == "m") {
                    menu_flag = 0;
                    break;
                }
            }
        }
    }

</script>

<script src="content/jquery-1.6.min.js" type="text/javascript"></script>
<script type="text/javascript">
    jQuery.noConflict();
</script>
<script type="text/javascript">

    jQuery(document).ready(function () {
        for (var i = 0; i < 3; i++) {
            jQuery("ul:empty").prev().parent("li").remove();
        }
    });
</script>
