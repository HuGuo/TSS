using System;
using System.Web;
using TSS.BLL;
using TSS.Models;

public class UploadHandler : IHttpHandler
{
    private static string configPath = HttpContext.Current.Server.MapPath(System.Configuration.ConfigurationManager.AppSettings["Document"])+"\\";
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
                case "file":
                    UpFile(context);
                    break;
                case "folder":
                    NewFolder(context);
                    break;
                case "download":
                    Download(context);
                    break;
                case "delete":
                    DeleteItem(context);
                    break;
                default:
                    context.Response.Write("参数错误");
                    break;
            }
        } else {
            context.Response.Write("参数错误");
        }
    }

    void UpFile(HttpContext context)
    {
        string res = "id:'{0}',name:'{1}',extension:'{2}',errorMsg:'{3}'";
        try {
            Document obj = Build(context);
            HttpFileCollection fileCollection = context.Request.Files;
            int count = fileCollection.Count;
            HttpPostedFile upload;
            for (int i = 0; i < count; i++) {
                upload = fileCollection[i];
                string fileExtension = System.IO.Path.GetExtension(upload.FileName);
                Document doc = new Document {
                    IsFolder = 0,
                    Id = System.Guid.NewGuid(),
                    Name = System.IO.Path.GetFileName(upload.FileName),
                    UploadDate = obj.UploadDate,
                    SpecialtyId = obj.SpecialtyId,
                    ParentId = obj.ParentId
                };
                doc.Path = obj.Path+ doc.Id + fileExtension;
                upload.SaveAs(configPath + doc.Path);
                RepositoryFactory<DocumentRepository>.Get().Add(doc);
                context.Response.Write("{" + string.Format(res, doc.Id.ToString(), doc.Name, fileExtension.Replace(".",""), "") + "}");
            }
        } catch(Exception ex) {
            context.Response.Write("{" + string.Format(res, "", "", "", ex.Message) + "}");
        }
        context.Response.End();
    }

    void NewFolder(HttpContext context)
    {
        string res = "id:'{0}',name:'{1}',extension:'{2}',errorMsg:'{3}'";
        try {
            string name = context.Server.UrlDecode(context.Request["name"]);
            Document doc = Build(context);
            doc.Name = name;
            doc.IsFolder = 1;
            doc.Path = doc.Path + doc.Id;
            if (!System.IO.Directory.Exists(configPath+doc.Path)) {
                System.IO.Directory.CreateDirectory(configPath + doc.Path);
                RepositoryFactory<DocumentRepository>.Get().Add(doc);
                context.Response.Write("{" + string.Format(res, doc.Id.ToString(), doc.Name, "folder", "") + "}");
            }            
        } catch (Exception ex) {
            context.Response.Write("{" + string.Format(res, "", "", "", ex.Message) + "}");
        }
    }

    Document Build(HttpContext context) {
        string specialtyId = context.Request["s"];
        string pid = context.Request["pid"];

        Document doc = new Document {
            IsFolder = 0,
            Id = System.Guid.NewGuid(),
            UploadDate = DateTime.Now,
            SpecialtyId = specialtyId
        };

        Guid? parentId = string.IsNullOrWhiteSpace(pid) ? null : (Guid?)new Guid(pid);
        if (parentId.HasValue) {
            Document parentFolder = RepositoryFactory<DocumentRepository>.Get().Get(parentId.Value);
            if (null!= parentFolder) {
                doc.ParentId = parentId;
                doc.Path = parentFolder.Path+"/";
            }
        } else {
            doc.Path = string.Empty;
        }
        return doc;
    }

    void DeleteItem(HttpContext context) {
        try {
            string id = context.Request["id"];
            
            if (!string.IsNullOrWhiteSpace(id)) {
                RepositoryFactory<DocumentRepository>.Get().Delete(new Guid(id) , onDelete);                
            }
        } catch (Exception ex) {
            context.Response.Write(ex.Message);
        }
    }
    void onDelete(string path) {
        string absPath = configPath + path;
        if (System.IO.File.Exists(absPath)) {
            System.IO.File.Delete(absPath);
        } 
        else if(System.IO.Directory.Exists(absPath)) {
            System.IO.Directory.Delete(absPath,true);
        }
    }
    void Download(HttpContext context) {
        try {
            string id = context.Request["id"];
            Document doc = RepositoryFactory<DocumentRepository>.Get().Get(new Guid(id));
            if (null !=doc) {
                DownloadFile.ResponseFile(configPath + doc.Path,doc.Name, context);
            }
        } catch (Exception ex) {
            context.Response.Write(ex.Message);
        }
    }
    #endregion
}