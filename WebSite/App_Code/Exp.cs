using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TSS.Models;
using TSS.BLL;

public class Exp : IHttpHandler
{
    #region IHttpHandler Members

    public bool IsReusable
    {
        get { return false; }
    }

    public void ProcessRequest(HttpContext context)
    {
        string op = context.Request["op"];
        if (!string.IsNullOrWhiteSpace(op)) {
            switch (op.ToLower()) {
                case "save":
                    SaveTemplate(context);
                    break;
                case "savedata":
                    SaveData(context);
                    break;
                case "del-t":
                    DeleteTemplate(context);
                    break;
                case "del-d":
                    DeleteExperiment(context);
                    break;
                default:
                    break;
            }
        } else {
            context.Response.Write("参数错误");
        }
    }

    void SaveTemplate(HttpContext context)
    {
        try {
            string _cid = context.Request["tid"];
            string _title = context.Server.UrlDecode(context.Request["title"]);
            string _html = context.Server.UrlDecode(context.Request["html"]);
            string sp = context.Request["sp"];
            Guid guid;
            if (!string.IsNullOrWhiteSpace(_cid)) {
                guid= new Guid(_cid);
            } else {
                guid = System.Guid.NewGuid();
            }
            ExpTemplate template = new ExpTemplate {
                Id = guid,
                SpecialtyId = sp,
                HTML = _html,
                Title = _title
            };
            ExpTemplateRepository repository = new ExpTemplateRepository();
            bool exists = repository.Exists(guid);
            if (exists) {
                repository.Update(template);
            } else {
                repository.Add(template);
            }
            context.Response.Write("1");
        } catch (Exception ex) {
            context.Response.Write("错误：" + ex.Message);
        }
    }

    void SaveData(HttpContext context)
    {
        try {
            string _id = context.Request["id"];
            string _tid = context.Request["tid"];
            string date = context.Request["date"];
            string emID = context.Request["eqmId"];
            string result = context.Request["result"];
            string title = context.Server.UrlDecode(context.Request["title"]);
            string html = context.Server.UrlDecode(context.Request["html"]);
            string data = context.Server.UrlDecode(context.Request["data"]);
            Guid templateID = new Guid(_tid);
            Guid newID = string.IsNullOrWhiteSpace(_id) ? System.Guid.NewGuid() : new Guid(_id);

            ICollection<ExpData> datas = null;
            if (!string.IsNullOrWhiteSpace(data)) {
                string[] arry1 = data.Split(new string[] { "<|>" }, StringSplitOptions.None);
                datas = (from p in arry1
                         let s = p.Split(new string[] { "<=>" }, StringSplitOptions.None)
                         select new ExpData {
                             GUID = s[0],
                             Value = s[1] == "" ? null : (decimal?)decimal.Parse(s[1]),
                             ExperimentId = newID
                         }).ToList();
            }

            Experiment experiment = new Experiment {
                Id = newID,
                Title = title,
                Result=int.Parse(result),
                ExpDate = DateTime.Parse(date),
                ExpTemplateID = templateID,
                HTML = html,
                EquipmentID = Guid.Parse(emID),
                Expdatas = datas
            };
            ExperimentRepository repository = new ExperimentRepository();
            if (!string.IsNullOrWhiteSpace(_id)) {
                repository.Update(experiment);
            } else {
                repository.Add(experiment);
            }
            context.Response.Write(experiment.Id);
        } catch (Exception ex) {
            context.Response.Write("错误：" + ex.Message);
        }
    }

    void DeleteTemplate(HttpContext context) {
        ExpTemplateRepository repository = new ExpTemplateRepository();
        try {
            Guid id = new Guid(context.Request["id"]);
            repository.Delete(id);            
        } catch (Exception ex) {
            context.Response.Write(ex.Message);
        }
        context.Response.End();
    }
    void DeleteExperiment(HttpContext context) {
        ExperimentRepository repository = new ExperimentRepository();
        try {
            Guid id = new Guid(context.Request["id"]);
            repository.Delete(id);
        } catch (Exception ex) {
            context.Response.Write(ex.Message);
        }
        context.Response.End();
    }

    #endregion
}