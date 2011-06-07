using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.HtmlControls;

public class BasePage:System.Web.UI.Page
{
    public BasePage() {
    }

    #region regist css and js
    protected void RegScripts(string basePath,params string [] scriptFileName) {
        foreach (string item in scriptFileName) {
            RegScript(System.IO.Path.Combine(basePath , item));
        }
    }
    protected void RegScript(string scriptFileName) {
        string name = scriptFileName.ToLower();
        string key = System.IO.Path.GetFileName(name);
        
        if (name.StartsWith("http://") || name.StartsWith(Request.ApplicationPath.ToLower())) {
            Page.ClientScript.RegisterClientScriptInclude(key , scriptFileName);
        } else {
            bool startRoot = name.StartsWith("/");
            Page.ClientScript.RegisterClientScriptInclude(key , Request.ApplicationPath + (startRoot ? "" : "/") + scriptFileName);
        }
    }

    protected void RegCsss(string basePath,params string[] styleFileName) {
        foreach (string item in styleFileName) {
            RegCss(System.IO.Path.Combine(basePath , item));
        }
    }
    protected void RegCss(string styleFileName) {
        string name = styleFileName.ToLower();        
        HtmlLink link = new HtmlLink();
        if (name.StartsWith("http://") || name.StartsWith(Request.ApplicationPath.ToLower())) {
            link.Href = styleFileName;
        } else {
            bool startRoot = name.StartsWith("/");
            link.Href = Request.ApplicationPath + (startRoot ? "" : "/") + styleFileName;
        }
        link.Attributes.Add("type" , "text/css");
        link.Attributes.Add("rel" , "stylesheet");
        Page.Header.Controls.Add(link);
    }

    protected void RegClientScript(string script,string key) {
        Type t = this.GetType();
        if (!this.ClientScript.IsStartupScriptRegistered(t,key)) {
            this.ClientScript.RegisterStartupScript(t ,key , script);
        }
    }
    #endregion
}

[Flags]
public enum Action
{
    View=0x00,
    Add=0x01,
    Edit=0x02,
    Delete=0x04,
    Audit=0x08
}