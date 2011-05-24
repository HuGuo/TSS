<%@ WebHandler Language="C#" Class="UploadHandler" %>

using System;
using System.Web;
public class UploadHandler : IHttpHandler {
    
    public void ProcessRequest (HttpContext context) {
        string folder = context.Server.MapPath(@context.Request["folder"])+"\\";
        HttpFileCollection fileCollection=context.Request.Files;
        int count = fileCollection.Count;
        HttpPostedFile upload;
        for (int i = 0; i < count; i++) {
            upload = fileCollection[i];
            upload.SaveAs(folder + System.IO.Path.GetFileName(upload.FileName));
        }
        context.Response.Write("1");
    }
 
    public bool IsReusable {
        get {
            return false;
        }
    }

}