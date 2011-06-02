using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml.Linq;
using TSS.Models;
using TSS.BLL;

public partial class Experiment_ChartStep2 : System.Web.UI.Page
{
    public string ChartString = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack) {
            string coord = Request.QueryString["coord"];
            string eqs = Server.UrlDecode(Request.QueryString["eqs"]);
            if (!string.IsNullOrWhiteSpace(coord)) {

            }
        }
    }
    protected void btnChart_Click(object sender, EventArgs e)
    {
        DateTime? stime = null;
        DateTime? etime = null;
        if (txtStime.Text.Trim()!="") {
            stime = DateTime.Parse(txtStime.Text);
        }
        if (txtEtime.Text.Trim()!="") {
            etime = DateTime.Parse(txtEtime.Text.Trim());
        }
        string coord = Request.QueryString["coord"];
        IList<ChartData> list = ExperimentRepository.Repository.GetChartData(coord, stime, etime);
        string caption = Request.QueryString["t"];
        string subCaption = "";
        int numVisiblePlot = 12;
        int showValues = 1;
        string clickURL = "";
        string xAxisName = "实验日期";
        string yAxisName = "实验数据";
        string tooltip = "{0}&lt;br&gt;实验结果：{1} &lt;br&gt;值：{2}";
        string[] labelDisplay = { "WRAP", "STAGGER", "ROTATE", "NONE" };
        XElement chart = new XElement("chart",
            new XAttribute("numVisiblePlot", numVisiblePlot),
            new XAttribute("xAxisName", xAxisName),
            new XAttribute("yAxisName", yAxisName),
            new XAttribute("labelDisplay", labelDisplay[2]),
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
                                                                   new XAttribute("value", p.CoordValue),
                                                                   new XAttribute("tooltext", "&gt;")
                                                                   )
                )
            );
        string style = "<styles><definition>style name='myHTMLFont' type='font' isHTML='1' /></definition><application><apply toObject='TOOLTIP' styles='myHTMLFont' /></application></styles>";
        XElement styleElement = XElement.Parse(style);
        chart.Add(styleElement);
        
        ChartString = FusionCharts.RenderChart("../FusionCharts/ScrollLine2D.swf", "data.aspx", "","scrollChart", "600", "350", false, false);
    }
}