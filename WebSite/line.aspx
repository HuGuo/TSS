<%@ Page Language="C#" AutoEventWireup="true" CodeFile="line.aspx.cs" Inherits="line" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body class="body_bg">
<div class="line">
	<a onclick='HideList("arrow")' id="arrow" class="arrow1"></a>
</div>
</body>
</html>
<script type="text/javascript" language="javascript">
    var view_flag = 1;
    var frame_cols = parent.parent.parent.document.getElementById("frame2").cols;
    function HideList(id) {
        var el = document.getElementById(id);
        if (view_flag == 1) {
            parent.parent.document.getElementById("frame2").cols = "0" + frame_cols.substr(frame_cols.indexOf(","));
            el.className = "arrow2";
        }
        else {
            parent.parent.document.getElementById("frame2").cols = frame_cols;
            el.className = "arrow1";
        }

        view_flag = 1 - view_flag;
    }

    var t = true;
    function ChangeImg(id) {
        var obj = document.getElementById(id)
        if (t == true) {
            obj.className = "th_p1"
            t = false;
        }
        else {
            obj.className = "th_p2"
            t = true;
        }
    }
</script>