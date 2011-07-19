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
        //试验曲线图数据源
        Response.ContentType = "text/xml;";
        Response.ContentEncoding = System.Text.Encoding.GetEncoding("GB2312");
        DateTime? stime = null;
        DateTime? etime = null;
        string qStime = Request.QueryString["stime"];//
        string qEtime = Request.QueryString["etime"];
        if (!string.IsNullOrWhiteSpace(qStime)) {
            stime = DateTime.Parse(qStime);
        }
        if (!string.IsNullOrWhiteSpace(qEtime)) {
            etime = DateTime.Parse(qEtime);
        }
        //指定设备
        string eqs = Request.QueryString["eqs"];
        string[] arry = { };
        if (!string.IsNullOrWhiteSpace(eqs)) {
            string _split = Request.QueryString["split"];
            char split = string.IsNullOrWhiteSpace(_split) ? ';' : _split[0];
            arry = eqs.Split(split);
        }
        
        string coord = Request.QueryString["coord"];//试验报告模板 坐标ID
        IList<ChartData> list = RepositoryFactory<ExperimentRepository>.Get().GetChartData(coord, stime, etime,arry);
        //合格次数
        int passed = list.Count(p => p.ExpResult == 1);
        int listCount = list.Count;
        //string caption = Server.UrlEncode(Server.UrlDecode(Request.QueryString["t"]));
        //string subCaption = "";
        int numVisiblePlot = 12;//可见点数，超过12显示滚动条
        int showValues = 1;
        string clickURL = Server.UrlEncode("n-experiment.aspx?id=");
        string xAxisName = "试验日期";
        string yAxisName = "";
        string tooltip = "{0}<BR>result:{1} <BR>value:{2}";
        string[] labelDisplay = { "WRAP", "STAGGER", "ROTATE", "NONE" };//新坐标 时间显示方式
        
        XElement chart =new XElement("chart",
            new XAttribute("baseFont" , "Verdana") ,
            new XAttribute("baseFontSize" , "12") ,
            new XAttribute("numVisiblePlot", numVisiblePlot),
            //new XAttribute("imageSave" , 1) ,
            //new XAttribute("imageSaveURL" , "../FusionCharts/FusionChartsSave.aspx") ,
            //new XAttribute("connectNullData",1),
            new XAttribute("xAxisName", xAxisName),
            new XAttribute("yAxisName", yAxisName),
            new XAttribute("labelDisplay", labelDisplay[2]),
            new XAttribute("anchorRadius", 5),
            new XAttribute("showAlternateVGridColor", 1),
            //new XAttribute("slantLabels", 1),
            new XAttribute("showValues", showValues),
            //new XAttribute("caption", caption),
            //new XAttribute("subCaption", subCaption),
            new XElement("categories", from p in list
                                       select new XElement("category",
                                           new XAttribute("label", p.ExpDate.ToString("yyyy/MM/dd"))
                                           )),
            new XElement("dataset",
                new XAttribute("seriesName" , string.Format("合格：{0}，不合格：{1}，试验次数：{2}",passed,listCount-passed,listCount)) ,
                new XAttribute("color", "FF0080"),
                new XAttribute("anchorBorderColor", "FF0080"), from p in list
                                                               select new XElement("set",
                                                                   !string.IsNullOrWhiteSpace(p.CoordValue) ? new XAttribute("value" , p.CoordValue) : new XAttribute("nodata" , "") ,
                                                                   new XAttribute("anchorBgColor", p.ExpResult == 1 ? "008000" : "ff0000"),
                                                                   new XAttribute("link",clickURL+p.ExperimentId.ToString()),
                                                                   new XAttribute("toolText", string.Format(tooltip, p.ExpDate.ToString("yyyy-MM-dd"), p.ExpResult == 1 ? "合格" : "不合格", p.CoordValue))
                                                                   )
                )
            );

        string style = "<styles><definition><style name='myHTMLFont' type='font' isHTML='1' /></definition><application><apply toObject='TOOLTIP' styles='myHTMLFont' /></application></styles>";
        XElement styleElement = XElement.Parse(style);
        chart.Add(styleElement);
        Response.Write("<?xml version=\"1.0\" encoding=\"gb2312\"?>");
        Response.Write(chart.ToString().Replace("\"","'"));//数据源xml 使用单引号
        Response.End();
    }
}