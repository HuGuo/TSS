<%@ Page Language="C#" %>
<%@ Import Namespace="TSS.Models"%>
<%@ Import Namespace="TSS.BLL"%>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<script runat="server">
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack) {
            //laod tree
            //string sp_code = Request.QueryString["sp"];
            string sp_code = "GHY-JY";
            if (!string.IsNullOrWhiteSpace(sp_code)) {
                ExpCategoryRepository repository = new ExpCategoryRepository();
                ExpCategory root = repository.GetRoot(sp_code);
                System.Text.StringBuilder build = new StringBuilder();
                build.AppendFormat("<li><span>{0}</span>",root.Name);
                ICollection<ExpCategory> childs = root.Childs;
                if (childs.Count>0) {
                    BuildTree(root.Childs, build);
                }
                build.Append("</li>");
                ltLi.Text = build.ToString();
            }
        }
    }

    void BuildTree(ICollection<ExpCategory> childs,System.Text.StringBuilder build) 
    {
        build.Append("<ul>");
        foreach (ExpCategory item in childs) {
            build.AppendFormat("<li><span><a href='explist.aspx?cid={1}&sp={2}' target='frm_explist'>{0}</a></span>", item.Name, item.Id, item.SP_Code);
            ICollection<ExpCategory> child = item.Childs;
            if (null!=child) {
                BuildTree(child, build);
            }
            build.Append("</li>");
        }
        build.Append("</ul>");
    }
</script>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>实验报告</title>
    <link href="../scripts/jquery-easyui/thems/default/easyui.css" rel="stylesheet" type="text/css" />
    <script src="../scripts/jquery-1.6.1.min.js" type="text/javascript"></script>
    <script src="../scripts/jquery-easyui/jquery.easyui.min.js" type="text/javascript"></script>
</head>
<body class="easyui-layout">
	<div region="west" split="true" title="实验报告分类" style="width:200px; max-width:300px;padding:5px;">
        <ul id="expTree" class="easyui-tree" animate="true">
            <asp:Literal ID="ltLi" Text="" runat="server" />
        </ul>
    </div>
	<div region="center" style="padding:0">
    <iframe name="frm_explist" scrolling="auto" frameborder="0"  src="explist.aspx" style="width:100%;height:100%;"></iframe>
    </div>
</body>
</html>
