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
                case "bind":
                    BindReport(context);
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
                AppLog.Write("创建流程："+workflow.Name , AppLog.LogMessageType.Info , "id="+workflow.Id , this.GetType());
            }
        } catch (Exception ex) {
            AppLog.Write("创建流程 出错", AppLog.LogMessageType.Error,"",ex,this.GetType());
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

    void BindReport(HttpContext context) {
        string rptId = context.Request["rpt"];
        string wfId = context.Request["wf"];
        try {
            using (WFContext db=new WFContext()) {
                ReportDetail obj= db.ReportDetails.Find(rptId);
                if (null !=obj) {
                    obj.WorkflowId = wfId;
                    ReportDetailRepository.Update(obj , db);
                }
            }
        } catch (Exception ex) {
            context.Response.Write(ex.Message);
        }
    }

    //audit
    void Audit(HttpContext context) {
        if (!context.User.Identity.IsAuthenticated) {
            context.Response.Write("无操作权限");
            return;
        }
        string signerId = context.User.Identity.Name;
        string doId = context.Request["todoId"];
        string tag = context.Request["tag"];
        string result = context.Request["result"];
        try {
            using (WFContext db=new WFContext()) {
                Signer s = new Signer {
                    Id = signerId ,
                    signResult = (SignResult)int.Parse(result) ,
                    SignTime = DateTime.Now ,
                    Tag = context.Server.UrlDecode(tag)
                };
                RepositoryFactory<WFInstanceRepository>.Get().Sign(doId , s , db);
            }
        } catch (Exception ex) {
            context.Response.Write(ex.Message);
        }
    }
}