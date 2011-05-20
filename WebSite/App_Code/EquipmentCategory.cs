using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using System.Web;

public class EquipmentCategory : IHttpHandler
{
    public bool IsReusable
    {
        get { return false; }
    }

    public void ProcessRequest(HttpContext context)
    {
        context.Response.ContentType = "text/xml; charset=UTF-8";

        context.Response.Write(TSS.BLL.EquipmentCategory.GetXml().ToString());
        context.Response.End();
    }
}