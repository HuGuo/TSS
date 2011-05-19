<%@ Page Language="C#" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<script runat="server">
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack) {
            //laod tree
            string sp_code = Request.QueryString["sp"];
            if (!string.IsNullOrWhiteSpace(sp_code)) {
                
            }
        }
    }
</script>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>实验报告</title>
    <link href="../scripts/jquery-easyui/thems/default/easyui.css" rel="stylesheet" type="text/css" />
    <script src="../scripts/jquery-1.6.min.js" type="text/javascript"></script>
    <script src="../scripts/jquery-easyui/jquery.easyui.min.js" type="text/javascript"></script>
</head>
<body class="easyui-layout">
	<div region="west" split="true" title="实验报告分类" style="width:200px;padding:5px;"></div>
	<div region="center" style="padding:0">
    <iframe name="frm_explist" scrolling="auto" frameborder="0"  src="explist.aspx" style="width:100%;height:100%;"></iframe>
    </div>
</body>
</html>
