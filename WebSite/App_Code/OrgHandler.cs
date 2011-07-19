using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Linq;
using wssmax;

public class OrgHandler:IHttpHandler
{
    static readonly string orgxml = HttpContext.Current.Server.MapPath("~/orgmap.xml");
	public OrgHandler(){}

    #region IHttpHandler Members

    public bool IsReusable {
        get { return false; }
    }

    public void ProcessRequest(HttpContext context) {
        string id = context.Request["id"];
        string b1 = context.Request["b1"];
        string b2 = context.Request["b2"];
        string c = context.Request["c"];
        try {
            XElement xe = XElement.Load(orgxml);
            XElement currentNode;
            if (xe.Attribute("id").Value == id) {
                currentNode = xe;
            } else {
                currentNode = (from p in xe.Element("d").Elements("a")
                               where p.Attribute("id").Value.Equals(id)
                               select p).FirstOrDefault();
            }
            if (null !=currentNode) {
                currentNode.Element("b1").Value = context.Server.UrlDecode(b1);
                currentNode.Element("b2").Value = context.Server.UrlDecode(b2);
                currentNode.Element("c").ReplaceNodes(new XCData(context.Server.UrlDecode(c).HtmlEncode()));
                xe.Save(orgxml);
            }
        } catch (Exception ex) {
            context.Response.Write(ex.Message);
        }        
    }

    #endregion
}