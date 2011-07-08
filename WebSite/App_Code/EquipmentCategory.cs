using System.IO;
using System.Web;
using System.Xml;
using System.Xml.Xsl;
using TSS.BLL;
using System.Linq;

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
            case "equipments":
                GetEquipmentsJson(context);
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

    public void GetEquipmentsJson(HttpContext context) {
        context.Response.ContentType="application/json; charset=UTF-8";
        string id=context.Request["id"];
        string specialtyId=context.Request["spid"];
        System.Text.StringBuilder result = new System.Text.StringBuilder();

        using (var equipmentCategorys = TSS.BLL.RepositoryFactory<TSS.BLL.EquipmentCategories>.Get()) {
            var equipmentCategory = equipmentCategorys.Get(new System.Guid(id));
            if (equipmentCategory != null && equipmentCategory.Equipments != null) {
                foreach (var equipment in equipmentCategory.Equipments.Where(p=>p.SpecialtyId.Equals(specialtyId))) {
                    result.Append(string.Format("{{\"id\":\"{0}\",\"text\":\"{1}\"}}," , equipment.Id , equipment.Name));
                }
            }
        }

        context.Response.Write(string.Format("[{0}]" , result.ToString().TrimEnd(',')));
    }

}