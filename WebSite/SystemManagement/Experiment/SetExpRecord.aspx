<%@ Page Language="C#" AutoEventWireup="true" EnableViewState="false" CodeFile="SetExpRecord.aspx.cs" Inherits="SystemManagement_Experiment_SetExpRecord" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>配置实验台帐模板</title>
    <link href="../../scripts/jquery-easyui/themes/gray/easyui.css" rel="stylesheet"
        type="text/css" />
    <script src="../../scripts/jquery-1.6.1.min.js" type="text/javascript"></script>
    <script src="../../scripts/jquery-easyui/jquery.easyui.min.js" type="text/javascript"></script>
    <style type="text/css">
        body
        {
            font-size: 12px;
            margin: 0;
            padding: 0;
        }
        .nveltb
        {
            border: 1px solid #e2e2e2;
            border-collapse: collapse;
        }
        .caption
        {
            font-size: 18px;
            line-height: 30px;
            font-weight: 800;
        }
        td
        {
            border: 1px solid #e2e2e2;
            text-align: center;
            height:26px;
        }
        .noborder{ border:0;}
    .dg{width:100px;
			height:25px;
			padding:10px;
			margin:5px;
			border:1px solid #ccc;
			background:#AACCFF;}
    .layout{ background-color:#fafafa; border:1px solid #cccccc; overflow:auto; width:99%;}
    .over,.set{
			background:#FBEC88;
		}
    </style>
</head>
<body>
<input type="button" id="btnSave" value="保存" style=" position:fixed; top:0; right:10px; width:85px; height:25px; z-index:98;" />
    <div id="divt1" class="layout" style="height:230px; ">
        <asp:Literal ID="ltExpReocrd" runat="server"></asp:Literal>
    </div>
    <div class="layout" style="overflow:auto; height:400px;">
    <asp:Literal ID="ltExpTemplate" runat="server"></asp:Literal>
    </div>
    <input type="hidden" runat="server" id="exptemplate" />
</body>
</html>
<script type="text/javascript">
    $(function () {
        var randomColor = function () {
            //return '#' + (Math.random() * 0xffffff << 0).toString(16);
            return '#' + ('00000' + (Math.random() * 0x1000000 << 0).toString(16)).substr(-6);
        };
        var c = '#fafafa';
        var handler = "../../exp.ashx";
        $("#simpleExcel").find("td:contains('{#}'),td:contains('{d}'),td:contains('{time}')").draggable({
            revert: true,
            deltaX: 0,
            deltaY: 0,
            proxy: function (s) {
                var n = $('<div class="proxy dg"></div>');
                n.html($(s).html()).appendTo("body");
                return n;
            },
            onStopDrag: function (e) {
                var $$ = $(this);
                $$.css("backgroundColor", c);
                c = "#fafafa";
            }
        });
        $("#tbExpRecord td.expdata").droppable({
            accept: 'td',
            onDrop: function (e, source) {
                c = randomColor();
                $(this).text("已设置").attr("ds", source.id);
                $(this).removeClass('over').css("backgroundColor", c);

            },
            onDragEnter: function (e, source) {
                $(this).addClass('over');
            },
            onDragLeave: function (e, source) {
                $(this).removeClass('over');
            }
        })
        .bind("dblclick", function () {
            c = "#fafafa"
            var ds = $("#" + $(this).attr("ds"));
            ds.css("backgroundColor", c);
            $(this).removeAttr("ds").text("").css("backgroundColor", c);
        });

        $("#btnSave").click(function () {
            var query = { op: "sert", id: "", tmpId: "", html: "" };
            query.id = '<%=Request.QueryString["id"] %>';
            query.tmpId = $("#exptemplate").val();
            query.html = $("#divt1").html();
            if (query.id == "" || query.t == "") {
                return;
            }
            $.post(handler, query, function (res) {
                if (res != "") {
                    alert(res);
                } else {
                    alert("保存成功");
                }
            });
        });
    });


</script>