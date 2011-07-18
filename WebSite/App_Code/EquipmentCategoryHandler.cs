using System.IO;
using System.Web;
using System.Xml;
using System.Xml.Xsl;
using TSS.BLL;
using System.Linq;

public class EquipmentCategoryHandler : IHttpHandler
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
        XmlTextReader reader = new XmlTextReader(new StringReader(category.GetXml()));
        Helper.ResponseEasyuiTreeHTML(reader , context);
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