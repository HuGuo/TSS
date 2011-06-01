using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using TSS.BLL;
using TSS.Models;

public partial class Experiment_data : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Response.ContentType = "text/xml;";
        Response.ContentEncoding = System.Text.Encoding.GetEncoding("GB2312");
        DateTime? stime = null;
        DateTime? etime = null;
        string qStime = Request.QueryString["stime"];
        string qEtime = Request.QueryString["etime"];
        if (!string.IsNullOrWhiteSpace(qStime)) {
            stime = DateTime.Parse(qStime);
        }
        if (!string.IsNullOrWhiteSpace(qEtime)) {
            etime = DateTime.Parse(qEtime);
        }
        string coord = Request.QueryString["coord"];
        IList<ChartData> list = ExperimentRepository.Repository.GetChartData(coord, stime, etime);
        string caption = Request.QueryString["t"];
        string subCaption = "";
        int numVisiblePlot = 12;
        int showValues = 1;
        string clickURL = "";
        string xAxisName = "实验日期";
        string yAxisName = "";
        string tooltip = "{0}<BR>result:{1} <BR>value:{2}";
        string[] labelDisplay = { "WRAP", "STAGGER", "ROTATE", "NONE" };
        
        XElement chart =new XElement("chart",
            new XAttribute("numVisiblePlot", numVisiblePlot),
            //new XAttribute("connectNullData",1),
            new XAttribute("xAxisName", xAxisName),
            new XAttribute("yAxisName", yAxisName),
            new XAttribute("labelDisplay", labelDisplay[2]),
            new XAttribute("anchorRadius", 5),
            new XAttribute("showAlternateVGridColor", 1),
            //new XAttribute("slantLabels", 1),
            new XAttribute("showValues", showValues),
            new XAttribute("caption", caption),
            new XAttribute("subCaption", subCaption),
            new XElement("categories", from p in list
                                       select new XElement("category",
                                           new XAttribute("label", p.ExpDate.ToString("yyyy/MM/dd"))
                                           )),
            new XElement("dataset",
                new XAttribute("color", "FF0080"),
                new XAttribute("anchorBorderColor", "FF0080"), from p in list
                                                               select new XElement("set",
                                                                   p.CoordValue.HasValue ? new XAttribute("value", p.CoordValue) : new XAttribute("nodata", ""),
                                                                   new XAttribute("anchorBgColor", p.ExpResult == 1 ? "008000" : "ff0000"),
                                                                   new XAttribute("link",Server.UrlEncode("n-experiment.aspx?id="+p.ExperimentId.ToString())),
                                                                   new XAttribute("toolText", string.Format(tooltip, p.ExpDate.ToString("yyyy-MM-dd"), p.ExpResult == 1 ? "合格" : "不合格", p.CoordValue))
                                                                   )
                )
            );

        string style = "<styles><definition><style name='myHTMLFont' type='font' isHTML='1' /></definition><application><apply toObject='TOOLTIP' styles='myHTMLFont' /></application></styles>";
        XElement styleElement = XElement.Parse(style);
        chart.Add(styleElement);
        Response.Write("<?xml version=\"1.0\" encoding=\"gb2312\"?>");
        Response.Write(chart.ToString().Replace("\"","'"));
        Response.End();
    }
}