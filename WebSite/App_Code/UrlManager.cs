using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text.RegularExpressions;

/// <summary>
/// 分页
/// </summary>
public abstract class UrlManager
{
	public UrlManager(int recordcount,int pagesize)
	{
        if (pagesize<0) {
            pagesize = 1;
        }
        if (recordcount<0) {
            recordcount = 0;
        }
        PageSize = pagesize;
        RecordCount = recordcount;

        if (recordcount == 0) {
            pageIndex = 1;
            PageCount = 1;
        } else {
            //double recordCount2 = Convert.ToDouble(recordcount);
            double pageSize2 = Convert.ToDouble(pagesize);
            PageCount = Convert.ToInt32(Math.Ceiling(recordcount / pageSize2));
        }
	}

    protected int pageIndex;

    public int PageCount { get; private set; }
    public int PageSize { get; set; }
    public int RecordCount { get; set; }

    public abstract int PageIndex { get; }
    public abstract string GetPageUrl();
    public abstract string DisplayMsg();
}

public class DefaultUrlManager:UrlManager
{
    private HttpContext context;
    private Regex reg;
    /// <summary>
    /// 分页Querystring参数名称
    /// </summary>
    public string QueryParam { get; set; }

    public DefaultUrlManager(int recordcount , int pagesize , string queryparam)
        : base(recordcount,pagesize)
    {
        if (string.IsNullOrWhiteSpace(queryparam)) {
            throw new ArgumentNullException("queryParam 不能为空！");
        }
        QueryParam = queryparam;
        context = HttpContext.Current;

        string pattern = @"(?<r1>[?&]" + QueryParam + @"=)[^&]*";
        reg = new Regex(pattern , RegexOptions.IgnoreCase);

        if (recordcount!=0) {
            string queryIndex = context.Request[queryparam];
            if (string.IsNullOrWhiteSpace(queryIndex)) {
                pageIndex = 1;
            } else {
                try {
                    pageIndex = int.Parse(queryIndex);
                    if (pageIndex < 1) {
                        pageIndex = 1;
                    }
                    if (pageIndex > PageCount) {
                        pageIndex = PageCount;
                    }
                } catch {
                    pageIndex = 1;
                }
            }
        }
    }
    public DefaultUrlManager(int recordcount)
        :this(recordcount,15,"p") { }

    public override int PageIndex {
        get { return pageIndex; }
    }
    public override string GetPageUrl() {
        string pageUrl = context.Request.RawUrl;

        if (reg.IsMatch(pageUrl)) {
            pageUrl = reg.Replace(pageUrl , "${r1}" + "{0}");
        } else {

            string queryString = context.Request.Url.Query;

            if (string.IsNullOrEmpty(queryString))
                pageUrl += (pageUrl.EndsWith("?") ? "" : "?") + QueryParam + "=" + "{0}";
            else
                pageUrl += "&" + QueryParam + "=" + "{0}";
        }

        return pageUrl;
    }

    public override string DisplayMsg() {
        if (RecordCount > 0) {

            string msg = "Dispalying from <b>{0}</b> to <b>{1}</b> of <b>{2}</b> items";
            int start = PageSize * (PageIndex - 1)+1;
            int end = start + PageSize-1;
            if (end > RecordCount) {
                end = RecordCount;
            }
            return string.Format(msg , start , end , RecordCount);
        } else {
            return "No Data to Display";
        }
    }
}