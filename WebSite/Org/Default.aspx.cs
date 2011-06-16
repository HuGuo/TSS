using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml.Linq;
using wssmax;

public partial class Org_Default : BasePage
{
    int n = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack) {
            RegScript("scripts/jquery.org.js");
            XElement xe = XElement.Load(Server.MapPath("~/orgmap.xml"));
            OrgNode root = new OrgNode {
                Text = BuildText(xe) ,
                Id = xe.Attribute("id").Value ,
                Description = xe.Element("c").Value
            };
            var childs = from p in xe.Element("d").Elements("a")
                         select new OrgNode {
                             Id = p.Attribute("id").Value ,
                             Text = BuildText(p) ,
                             Description = p.Element("c").Value
                         };
            foreach (var item in childs) {
                root.Nodes.Add(item);
            }

            OrgChart1.Node = root;
        }
    }

    string BuildText(XElement node) {
        string build = "<b>{0}</b><p><label for='span_" + (++n) + "'>{1}</label><span id='span_" + n + "'>{2}</span></p><p><label for='span_" + (++n) + "'>{3}</label><span id='span_" + n + "'>{4}</span></p>";
        XElement b1 = node.Element("b1");
        XElement b2 = node.Element("b2");
        return string.Format(build , node.Attribute("name").Value , b1.Attribute("name").Value , b1.Value , b2.Attribute("name").Value , b2.Value);
    }
}