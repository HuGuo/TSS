using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class UserControl_Pager : System.Web.UI.UserControl
{
    public int RecordCount { get; set; }
    public int PageSize { get; set; }

    //public UrlManager Urlmanager { get; set; }

    //private int beginPageIndex;
    //private int endPageIndex;

    //private bool showPrevious;//是否显示 上一页
    //private bool showNext; //下一页

    protected void Page_Load(object sender, EventArgs e)
    {
        System.Text.StringBuilder script = new System.Text.StringBuilder();
        UrlManager p = new DefaultUrlManager(RecordCount , PageSize , Helper.queryParam_pagination);

        if (p.PageCount > 1) {
            script.AppendLine("<script type=\"text/javascript\">");
        
            script.AppendLine("if (!window.jQuery) {\r");
            script.AppendLine("alert('缺少 jQuery库文件');");
            script.AppendLine("} else {");

            script.AppendLine("$(function(){");
            script.AppendLine(@"String.format = function (src) {if (arguments.length == 0) return null;var args = Array.prototype.slice.call(arguments, 1);return src.replace(/\{(\d+)\}/g, function (m, i) {return args[i];});}");
            script.AppendLine("$(\"div.pagination\").pagination({");
            script.AppendFormat("total:{0},pageSize:{1}," , RecordCount , PageSize);
            script.Append("showPageList:false,");
            script.AppendFormat("pageNumber:{0}," , p.PageIndex);
            script.AppendFormat("displayMsg:\"{0}\"," , p.DisplayMsg());
            script.Append("onSelectPage:function(pageNumber, pageSize){");
            script.AppendFormat("document.location.href=String.format('{0}',pageNumber);" , p.GetPageUrl());
            script.AppendLine("}");
            script.AppendLine("});");

            script.AppendLine("});}");

            script.AppendLine("</script>");
            Type t = Page.GetType();
            if (!Page.ClientScript.IsStartupScriptRegistered(t , "pagination")) {
                Page.ClientScript.RegisterStartupScript(t , "pagination" , script.ToString());
            }
        } else {
            ltHtml.Text = "<div class=\"pagination-info\">"+p.DisplayMsg()+"</div><div style=\"clear:both;\"></div>";
        }
    }

    void RenderPager() {
        
    }
}