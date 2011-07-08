using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Linq;
using workflow;
public class WorkflowHandler:IHttpHandler
{
	public WorkflowHandler(){}

    #region IHttpHandler Members

    public bool IsReusable {
        get { return false; }
    }

    public void ProcessRequest(HttpContext context) {
        string op =context.Request["op"];
        if (!string.IsNullOrWhiteSpace(op)) {
            switch (op.ToLower()) {
                case "create":
                    CreateWorkflow(context);
                    break;
                default:
                    break;
            }
        }
    }

    #endregion

    void CreateWorkflow(HttpContext context) {
        string name = context.Request["name"];
        string id = context.Request["id"];
        string wf = context.Request["wf"];
        try {            
            if (string.IsNullOrWhiteSpace(id)) {
                id = System.Guid.NewGuid().ToString();
            }
            XElement doc = XElement.Parse(wf);
            var obj = from p in doc.Elements("a")
                      select new Active {
                          Id =  System.Guid.NewGuid().ToString(),
                          IntervalHours = int.Parse(p.Attribute("hours").Value),
                          Signers=BuildSigners(p.Elements())
                      };
            Workflow workflow = new Workflow {  
                Id=id,
                Name=context.Server.UrlDecode(name),
                Actives=obj.ToList()
            };
            using (WFContext db = new WFContext()) {
                WFRepository.Save(workflow , db);
            }
        } catch (Exception ex) {
            context.Response.Write(ex.Message);
        }
    }

    SignerList BuildSigners(IEnumerable<XElement> nodes) {
        var query = from p in nodes
                    select new Signer {  
                        Id=p.Attribute("sid").Value,
                        NameCN=p.Value,
                        IsWeight=p.Attribute("isweight").Value=="true",
                        signResult= SignResult.Unaudit
                    };
        SignerList ss = new SignerList();
        foreach (var item in query) {
            ss.Add(item);
        }

        return ss;
    }
}