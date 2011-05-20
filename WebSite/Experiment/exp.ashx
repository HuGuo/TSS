<%@ WebHandler Language="C#" Class="exp" %>

using System;
using System.Collections.Generic;
using System.Web;
using System.Linq;
using TSS.Models;
using TSS.BLL;

public class exp : IHttpHandler
{

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
                default:
                    break;
            }
        } else {
            context.Response.Write("参数错误");
        }
    }

    public bool IsReusable
    {
        get
        {
            return false;
        }
    }

    void SaveTemplate(HttpContext context)
    {
        try {
            string _cid = context.Request["cid"];
            string _title = context.Server.UrlDecode(context.Request["title"]);
            string _html = context.Server.UrlDecode(context.Request["html"]);
            string sp = context.Request["sp"];
            int cid=0;
            int.TryParse(_cid,out cid);
            ExpTemplate template = new ExpTemplate {
                Id = cid,
                SP_CODE = sp,
                HTML = _html,
                Title = _title
            };
            Repository<ExpTemplate, int> repository = new Repository<ExpTemplate, int>();
            bool exists = repository.Exists(cid);
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
            string title = context.Server.UrlDecode(context.Request["title"]);
            string html = context.Server.UrlDecode(context.Request["html"]);
            string data = context.Server.UrlDecode(context.Request["data"]);
            string [] arry1= data.Split(new string []{"<|>"}, StringSplitOptions.None);
            int templateID = 0;            
            int.TryParse(_tid, out templateID);
            string newID = string.IsNullOrWhiteSpace(_id) ?System.Guid.NewGuid().ToString():_id;
            Experiment experiment = new Experiment {
                Id = newID,
                Title = title,
                ExpDate = DateTime.Parse(date),
                TemplateID = templateID,
                HTML = html,
                EquipmentID=string.Empty,
                Expdatas = (from p in arry1
                            let s = p.Split(new string[] { "<=>"}, StringSplitOptions.None)
                            select new ExpData {
                                GUID = s[0],
                                Value = s[1],
                                EXPId = newID
                            }).ToList()
            };
            ExperimentRepository repository = new ExperimentRepository();
            if (!string.IsNullOrWhiteSpace(_id)) {
                repository.Update(experiment);
            } else {
                //experiment.Id = System.Guid.NewGuid().ToString();
                repository.Add(experiment);
            }
            context.Response.Write(experiment.Id);
        } catch (Exception ex) {
            context.Response.Write("错误："+ex.Message);
        }
    }

}