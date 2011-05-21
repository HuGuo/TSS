using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml;
using System.Xml.Xsl;
using System.IO;
public class EquipmentCategory : IHttpHandler
{
    public bool IsReusable
    {
        get { return false; }
    }

    public void ProcessRequest(HttpContext context)
    {
        string op = context.Request.QueryString["xslt"];
        if (null==op) {

            context.Response.ContentType = "text/xml; charset=UTF-8";

            context.Response.Write(TSS.BLL.EquipmentCategory.GetXml().ToString());
            context.Response.End();
        } else {
            ResponseHtml(context);
        }
    }

    void ResponseHtml(HttpContext context) 
    {
        XslCompiledTransform xsl = new XslCompiledTransform();
        xsl.Load(context.Server.MapPath("~/equipmentcategory.xslt"));

        XsltArgumentList args = new XsltArgumentList();
        //src 参数值带有?，&时需要在客户端编码
        string src = context.Request.QueryString["src"];
        args.AddParam("src", string.Empty, string.IsNullOrEmpty(src) ? "" : context.Server.UrlDecode(src));
        args.AddParam("target", string.Empty, context.Request.QueryString["target"]??"");

        args.AddParam("expendDeep", string.Empty, context.Request.QueryString["dp"] ?? "1");

        XmlTextReader reader = new XmlTextReader(new StringReader(TSS.BLL.EquipmentCategory.GetXml().ToString()));
        xsl.Transform(reader, args, context.Response.OutputStream);
        context.Response.End();
    }
}