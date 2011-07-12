﻿using System;
using System.Web;
using TSS.BLL;
using TSS.Models;
using System.Linq;

/// <summary>
/// Summary description for ExpTemplateHandler
/// </summary>
public class ExpTemplateHandler:IHttpHandler
{
    #region IHttpHandler Members

    public bool IsReusable {
        get { return false; }
    }

    public void ProcessRequest(HttpContext context) {
        string op = context.Request["op"];
        if (!string.IsNullOrWhiteSpace(op)) {
            switch (op.ToLower()) {
                case "save":
                    SaveTemplate(context);
                    break;
                case "del-t":
                    DeleteTemplate(context);
                    break;
                case"bindedequipmentjson":
                    BindedEquipmentJSON(context);
                    break;
                case "bindequipment":
                    BindEquipment(context,true);
                    break;
                case"unbindequipment":
                    BindEquipment(context , false);
                    break;
            }
        } else {
            context.Response.Write("参数错误");
        }
    }

    #endregion

    #region 实验模板
    //保存试验报告模板
    void SaveTemplate(HttpContext context) {
        try {
            string _cid = context.Request["tid"];
            string _title = context.Server.UrlDecode(context.Request["title"]);
            string _html = context.Server.UrlDecode(context.Request["html"]);
            string sp = context.Request["sp"];
            Guid guid;
            if (!string.IsNullOrWhiteSpace(_cid)) {
                guid = new Guid(_cid);
            } else {
                guid = System.Guid.NewGuid();
            }
            ExpTemplate template = new ExpTemplate {
                Id = guid ,
                SpecialtyId = sp ,
                HTML = _html ,
                Title = _title
            };
            bool exists = RepositoryFactory<ExpTemplateRepository>.Get().IsExists(guid);
            if (exists) {
                RepositoryFactory<ExpTemplateRepository>.Get().Update(template.Id , template);
            } else {
                RepositoryFactory<ExpTemplateRepository>.Get().Add(template);
            }
            context.Response.Write("{\"id\":\"" + template.Id + "\"}");
        } catch (Exception ex) {
            context.Response.Write("{\"msg\":\"错误：" + ex.Message + "\"");
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

    void BindEquipment(HttpContext context,bool bind) {
        string etId = context.Request["etid"];
        string eqId = context.Request["eqid"];
        try {
            var guids = from p in eqId.Split(';')
                        select new Guid(p);
            if (bind) {
                RepositoryFactory<ExpTemplateRepository>.Get().BindEquipment(new Guid(etId) , guids.ToArray());
            } else {
                RepositoryFactory<ExpTemplateRepository>.Get().UnBindEquipment(new Guid(etId) , guids.ToArray());
            }
        } catch (Exception ex) {
            context.Response.Write(ex.Message);
        }
    }

    void BindedEquipmentJSON(HttpContext context) {
        context.Response.ContentType = "application/json; charset=UTF-8";
        string etid = context.Request["etid"];
        ExpTemplate obj= RepositoryFactory<ExpTemplateRepository>.Get().Get(new Guid(etid));
        if (null != obj) {

            var query = from p in obj.Equipments
                        select new { id = p.Id.ToString() , text = p.Name };
            System.Web.Script.Serialization.JavaScriptSerializer jss = new System.Web.Script.Serialization.JavaScriptSerializer();
            context.Response.Write(jss.Serialize(query));
        } else {
            context.Response.Write("[]");
        }

    }
    #endregion
}