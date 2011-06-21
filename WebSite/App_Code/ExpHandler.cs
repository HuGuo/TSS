using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TSS.BLL;
using TSS.Models;

public class ExpHandler : IHttpHandler
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
                case "recordjson":
                    ResponseExpRecordJSON(context);
                    break;
                case "sert":
                    SaveExpRecordTemplate(context);
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
            bool exists = RepositoryFactory<ExpTemplateRepository>.Get().IsExists(guid);
            if (exists) {
                RepositoryFactory<ExpTemplateRepository>.Get().Update(template.Id,template);
            } else {
                RepositoryFactory<ExpTemplateRepository>.Get().Add(template);
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
                             Value = s[1] ,
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
            if (!string.IsNullOrWhiteSpace(_id)) {
                RepositoryFactory<ExperimentRepository>.Get().Update(experiment.Id,experiment);
            } else {
                RepositoryFactory<ExperimentRepository>.Get().Add(experiment);
            }
            context.Response.Write(experiment.Id);
        } catch (Exception ex) {
            context.Response.Write("错误：" + ex.Message);
        }
    }

    void DeleteTemplate(HttpContext context) {        
        try {
            RepositoryFactory<ExpTemplateRepository>.Get().Delete(new Guid(context.Request["id"]));            
        } catch (Exception ex) {
            context.Response.Write(ex.Message);
        }
        context.Response.End();
    }
    void DeleteExperiment(HttpContext context) {        
        try {
            RepositoryFactory<ExperimentRepository>.Get().Delete(new Guid(context.Request["id"]));
        } catch (Exception ex) {
            context.Response.Write(ex.Message);
        }
        context.Response.End();
    }

    //实验台帐 数据
    void ResponseExpRecordJSON(HttpContext context) {
        
        string eqId = context.Request["equipment"];
        string tmpId = context.Request["exptemplate"];
        string columns = context.Request["fields"];
        try {
            string cacheKey = "category_" + eqId;
            object json= CacheManager.GetCache(cacheKey);
            string[] fields = columns.Split(',');
            //if (null != json) {
                System.Web.Script.Serialization.JavaScriptSerializer jss = new System.Web.Script.Serialization.JavaScriptSerializer();
                IList<Experiment> list = RepositoryFactory<ExperimentRepository>.Get().GetMuch(new Guid(eqId) , new Guid(tmpId));
                Equipment obj = RepositoryFactory<Equipments>.Get().Get(new Guid(eqId));
                var datas = from p in list
                            select new {
                                expdate = p.ExpDate.ToString("yyyy-MM-dd") ,
                                id = p.Id ,
                                result = p.Result ,
                                dts = from m in p.Expdatas
                                      where fields.Contains(m.GUID)
                                      select new { label=m.GUID, val=m.Value}
                            };
                var result = new {
                    eqid = obj.Id.ToString() ,
                    name = obj.Name ,
                    code = obj.Code ,
                    rows=datas,
                    nameplates = from m in obj.EquipmentDetails
                                 select new { label = m.Lable , val = m.Value }
                };
                json = jss.Serialize(result);
                //CacheManager.SetCache(cacheKey , json,30);
            //}
            context.Response.Write(json);
            
        } catch (Exception ex) {
            context.Response.Write("{\"msg\":\""+ex.Message+"\"}");
        }
    }
    //保存实验台帐模板
    void SaveExpRecordTemplate(HttpContext context) {
        string id = context.Request["id"];
        string tmpId = context.Request["tmpId"];
        string html = context.Request["html"];
        try {
            ExpRecord obj = RepositoryFactory<ExpReocrdRepository>.Get().Get(new Guid(id));
            if (null !=obj) {
                obj.DataSourcesTemplate = RepositoryFactory<ExpTemplateRepository>.Get().Get(new Guid(tmpId));
                string path = context.Server.MapPath(obj.Path);
                if (System.IO.File.Exists(path)) {
                    using (System.IO.StreamWriter write = new System.IO.StreamWriter(path , false)) {
                        write.WriteLine("<!--"+obj.Name+"-->");
                        write.Write(html);
                        write.Flush();
                        write.Close();
                    }
                }
                RepositoryFactory<ExpReocrdRepository>.Get().Update(obj.Id , obj);
            }
        } catch (Exception ex) {
            context.Response.Write(ex.Message);
        }
    }
    #endregion
}