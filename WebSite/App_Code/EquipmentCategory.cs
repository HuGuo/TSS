using System.IO;
using System.Web;
using System.Xml;
using System.Xml.Xsl;
using TSS.BLL;

public class EquipmentCategory : IHttpHandler
{
    private EquipmentCategories category = RepositoryFactory<EquipmentCategories>.Get();

    public bool IsReusable
    {
        get { return false; }
    }

    public void ProcessRequest(HttpContext context)
    {
        string type = context.Request.QueryString["type"];
        switch (type) {
            case "json":
                ResponseJson(context);
                break;
            case "xml":
                ResponseXml(context);
                break;
            default:
                context.Response.StatusCode = 404;
                break;
        }

        context.Response.End();

    }

    private void ResponseJson(HttpContext context)
    {
        context.Response.ContentType = "application/json; charset=UTF-8";

        context.Response.Write(category.GetJson());
    }

    private void ResponseXml(HttpContext context)
    {
        context.Response.ContentType = "text/xml; charset=UTF-8";

        XslCompiledTransform xsl = new XslCompiledTransform();
        xsl.Load(context.Server.MapPath("~/equipmentcategory.xslt"));

        XsltArgumentList args = new XsltArgumentList();
        //src 参数值带有?，&时需要在客户端编码
        string src = context.Request.QueryString["src"];
        args.AddParam("src", string.Empty, string.IsNullOrEmpty(src) ? "" : context.Server.UrlDecode(src));
        args.AddParam("target", string.Empty, context.Request.QueryString["target"] ?? "");

        args.AddParam("expendDeep", string.Empty, context.Request.QueryString["dp"] ?? "1");

        XmlTextReader reader = new XmlTextReader(new StringReader(category.GetXml()));
        xsl.Transform(reader, args, context.Response.OutputStream);
    }
}