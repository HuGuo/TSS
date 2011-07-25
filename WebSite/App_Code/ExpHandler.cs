using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TSS.BLL;
using TSS.Models;

public class ExpHandler : IHttpHandler
{
    static readonly string attachmentPath = HttpContext.Current.Server.MapPath(System.Configuration.ConfigurationManager.AppSettings["Experiment"]) + "\\";

    #region IHttpHandler Members

    public bool IsReusable {
        get { return false; }
    }

    public void ProcessRequest(HttpContext context) {
        string op = context.Request["op"];
        if (!string.IsNullOrWhiteSpace(op)) {
            switch (op.ToLower()) {
                case "savedata":
                    SaveData(context);
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
                case "recordequipments":
                    ResponseEquipmentJSON(context);
                    break;
                case "upload":
                    Upload(context);
                    break;
                case "del-attachment":
                    DeleteAttachment(context);
                    break;
                case "download":
                    DownloadAttachment(context);
                    break;
                default:
                    break;
            }
        } else {
            context.Response.Write("参数错误");
        }
    }
    #endregion

    #region 实验报告，模板，附件

    //试验报告数据
    void SaveData(HttpContext context) {
        string _id = context.Request["id"];
        string _tid = context.Request["tid"];
        string date = context.Request["date"];
        string emID = context.Request["eqmId"];
        string result = context.Request["result"];
        Guid newID = string.IsNullOrWhiteSpace(_id) ? System.Guid.NewGuid() : new Guid(_id);
        try {
            
            string remark = context.Server.UrlDecode(context.Request["remark"]);
            string title = context.Server.UrlDecode(context.Request["title"]);
            string html = context.Server.UrlDecode(context.Request["html"]);
            string data = context.Server.UrlDecode(context.Request["data"]);
            Guid templateID = new Guid(_tid);

            ICollection<ExpData> datas = null;
            if (!string.IsNullOrWhiteSpace(data)) {
                string[] arry1 = data.Split(new string[] { "<|>" } , StringSplitOptions.None);
                datas = (from p in arry1
                         let s = p.Split(new string[] { "<=>" } , StringSplitOptions.None)
                         select new ExpData {
                             GUID = s[0] ,
                             Value = s[1] ,
                             ExperimentId = newID
                         }).ToList();
            }

            Experiment experiment = new Experiment {
                Id = newID ,
                Title = title ,
                Result = int.Parse(result) ,
                ExpDate = DateTime.Parse(date) ,
                ExpTemplateID = templateID ,
                HTML = html ,
                EquipmentID = Guid.Parse(emID) ,
                Remark = remark.HtmlEncode() ,
                Expdatas = datas
            };
            if (!string.IsNullOrWhiteSpace(_id)) {
                RepositoryFactory<ExperimentRepository>.Get().Update(experiment.Id , experiment);
                AppLog.Write("修改试验报告" , AppLog.LogMessageType.Info , "id=" + experiment.Id.ToString() , this.GetType());
            } else {
                RepositoryFactory<ExperimentRepository>.Get().Add(experiment);
                AppLog.Write("填写试验报告" , AppLog.LogMessageType.Info , "id=" + experiment.Id.ToString() , this.GetType());
            }
            context.Response.Write("{\"id\":\"" + experiment.Id + "\"}");
        } catch (Exception ex) {
            //log
            AppLog.Write("保存实验报告出错" , AppLog.LogMessageType.Error , "id=" + newID , ex , this.GetType());

            context.Response.Write("{\"msg\":\"错误：" + ex.Message + "\"");
        }
    }

    //上传试验报告附件
    void Upload(HttpContext context) {
        try {
            string id = context.Request["expid"];

            HttpFileCollection fileCollection = context.Request.Files;
            int count = fileCollection.Count;
            HttpPostedFile upload;
            ICollection<ExpAttachment> items = new List<ExpAttachment>();
            for (int i = 0; i < count; i++) {
                upload = fileCollection[i];
                string fileExtension = System.IO.Path.GetExtension(upload.FileName);
                Guid guid = System.Guid.NewGuid();
                upload.SaveAs(attachmentPath + guid.ToString() + fileExtension);
                ExpAttachment item = new ExpAttachment {
                    Id = guid ,
                    FileName = upload.FileName
                };

                items.Add(item);
                context.Response.Write("{" + string.Format("\"id\":\"{0}\",\"name\":\"{1}\"" , item.Id , item.FileName.Replace("\"" , "“")) + "}");
            }
            RepositoryFactory<ExperimentRepository>.Get().AddAttachment(new Guid(id) , items , onUploadError);
        } finally { }
    }
    void onUploadError(ICollection<ExpAttachment> uploadedFiles) {
        foreach (var item in uploadedFiles) {
            string path = attachmentPath + item.Id.ToString() + System.IO.Path.GetExtension(item.FileName);
            if (System.IO.File.Exists(path)) {
                System.IO.File.Delete(path);
            }
        }
    }

    //删除试验报告附件
    void DeleteAttachment(HttpContext context) {
        string id = context.Request["id"];
        string attachment = context.Request["att"];
        try {
            RepositoryFactory<ExperimentRepository>.Get().RemoveAttachment(new Guid(id) , new Guid(attachment) , onRemove);
            //log
            AppLog.Write("删除试验报告附件" , AppLog.LogMessageType.Info , string.Format("id={0},attachment={1}",id,attachment) , this.GetType());
        } catch (Exception ex) {
            AppLog.Write("删除试验报告附件 出错" , AppLog.LogMessageType.Error , string.Format("id={0},attachment={1}" , id , attachment) , ex , this.GetType());

            context.Response.Write(ex.Message);
        }

    }
    void onRemove(string fileName) {
        if (System.IO.File.Exists(attachmentPath + fileName)) {
            System.IO.File.Delete(attachmentPath + fileName);
        }
    }

    void DownloadAttachment(HttpContext context) {
        string id = context.Request.QueryString["id"];
        string filename = context.Server.UrlDecode(context.Request.QueryString["filename"]);
        DownloadFile.ResponseFile(attachmentPath + id + System.IO.Path.GetExtension(filename) , filename , context);
    }
    
    void DeleteExperiment(HttpContext context) {
        try {
            RepositoryFactory<ExperimentRepository>.Get().Delete(new Guid(context.Request["id"]));
            //log
            AppLog.Write("删除试验报告" , AppLog.LogMessageType.Info , "id=" + context.Request["id"] , this.GetType());
        } catch (Exception ex) {
            AppLog.Write("删除试验报告 出错" , AppLog.LogMessageType.Error , "id=" + context.Request["id"] , ex , this.GetType());            

            context.Response.Write(ex.Message);
        }
        context.Response.End();
    }
    #endregion
    
    #region 实验台帐
    //试验台帐 数据
    void ResponseExpRecordJSON(HttpContext context) {
        //context.Response.ContentType = "application/json";
        string eqId = context.Request["equipment"];
        string tmpId = context.Request["exptemplate"];
        string columns = context.Request["fields"];
        try {
            string cacheKey = "category_" + eqId;
            object json = CacheManager.GetCache(cacheKey);
            string[] fields = columns.Split(',');
            //if (null != json) {
            System.Web.Script.Serialization.JavaScriptSerializer jss = new System.Web.Script.Serialization.JavaScriptSerializer();
            IList<Experiment> list = RepositoryFactory<ExperimentRepository>.Get().GetList(new Guid(eqId) , new Guid(tmpId));
            Equipment obj = RepositoryFactory<Equipments>.Get().Get(new Guid(eqId));
            var datas = from p in list
                        select new {
                            expdate = p.ExpDate.ToString("yyyy-MM-dd") ,
                            id = p.Id ,
                            result = p.Result ,
                            remark = p.Remark ,
                            dts = from m in p.Expdatas
                                  where fields.Contains(m.GUID)
                                  select new { label = m.GUID , val = m.Value }
                        };
            obj.EquipmentDetails.Add(new EquipmentDetail { Lable = "id" , Value = obj.Id.ToString() });
            obj.EquipmentDetails.Add(new EquipmentDetail { Lable = "code" , Value = obj.Code });
            obj.EquipmentDetails.Add(new EquipmentDetail { Lable = "name" , Value = obj.Name });
            var result = new {
                rows = datas ,
                nameplates = from m in obj.EquipmentDetails
                             select new { label = m.Lable , val = m.Value }
            };
            json = jss.Serialize(result);
            //CacheManager.SetCache(cacheKey , json,30);
            //}
            context.Response.Write(json);

        } catch (Exception ex) {
            context.Response.Write("{\"msg\":\"" + ex.Message + "\"}");
        }
    }

    //试验台帐设备列表 
    void ResponseEquipmentJSON(HttpContext context) {
        context.Response.ContentType = "application/json";
        string tmpId = context.Request.QueryString["tid"];
        IEnumerable<Equipment> list = RepositoryFactory<ExpReocrdRepository>.Get().GetEquipmentsByExpTemplate(new Guid(tmpId));
        var quer = from p in list
                   select new { id = p.Id , name = p.Name };

        System.Web.Script.Serialization.JavaScriptSerializer jss = new System.Web.Script.Serialization.JavaScriptSerializer();
        context.Response.Write(jss.Serialize(quer));
    }

    //保存试验台帐模板
    void SaveExpRecordTemplate(HttpContext context) {
        string id = context.Request["id"];
        string tmpId = context.Request["tmpId"];
        string html = context.Request["html"];
        try {
            ExpRecord obj = RepositoryFactory<ExpReocrdRepository>.Get().Get(new Guid(id));
            if (null != obj) {
                //obj.DataSourcesTemplate = RepositoryFactory<ExpTemplateRepository>.Get().Get(new Guid(tmpId));
                //RepositoryFactory<ExpReocrdRepository>.Get().Update(obj.Id , obj);

                string path = context.Server.MapPath(obj.Path);
                if (System.IO.File.Exists(path)) {
                    using (System.IO.StreamWriter write = new System.IO.StreamWriter(path , false)) {
                        //write.WriteLine("<!--"+obj.Name+"-->");
                        write.Write(html);
                        write.Flush();
                        write.Close();
                    }
                }

            }
        } catch (Exception ex) {
            context.Response.Write(ex.Message);
        }
    }
    #endregion
}