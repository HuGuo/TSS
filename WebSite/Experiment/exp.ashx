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
            Guid guid;
            Guid.TryParse(_cid, out guid);
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
            string emID = context.Request["eqmID"];
            string title = context.Server.UrlDecode(context.Request["title"]);
            string html = context.Server.UrlDecode(context.Request["html"]);
            string data = context.Server.UrlDecode(context.Request["data"]);
            string [] arry1= data.Split(new string []{"<|>"}, StringSplitOptions.None);
            Guid templateID ;            
            Guid.TryParse(_tid, out templateID);
            Guid newID = string.IsNullOrWhiteSpace(_id) ? System.Guid.NewGuid() : System.Guid.Parse(_id);
            Experiment experiment = new Experiment {
                Id = newID,
                Title = title,
                ExpDate = DateTime.Parse(date),
                ExpTemplateID = templateID,
                HTML = html,
                EquipmentID=Guid.Parse(emID),
                Expdatas = (from p in arry1
                            let s = p.Split(new string[] { "<=>"}, StringSplitOptions.None)
                            select new ExpData {
                                GUID = s[0],
                                Value = s[1],
                                ExperimentId = newID
                            }).ToList()
            };
            ExperimentRepository repository = new ExperimentRepository();
            if (!string.IsNullOrWhiteSpace(_id)) {
                repository.Update(experiment);
            } else {
                repository.Add(experiment);
            }
            context.Response.Write(experiment.Id);
        } catch (Exception ex) {
            context.Response.Write("错误："+ex.Message);
        }
    }

}