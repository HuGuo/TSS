using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.HtmlControls;

public class BasePage : System.Web.UI.Page
{
    public BasePage()
    {
    }

    #region regist css and js
    protected void RegScripts(string basePath, params string[] scriptFileName)
    {
        foreach (string item in scriptFileName)
        {
            RegScripts(System.IO.Path.Combine(basePath, item));
        }
    }
    protected void RegScript(string scriptFileName)
    {
        string name = scriptFileName.ToLower();
        string key = System.IO.Path.GetFileName(name);

        if (name.StartsWith("http://") || name.StartsWith(Request.ApplicationPath.ToLower()))
        {
            Page.ClientScript.RegisterClientScriptInclude(key, scriptFileName);
        }
        else
        {
            bool startRoot = name.StartsWith("/");
            Page.ClientScript.RegisterClientScriptInclude(key, Request.ApplicationPath + (startRoot ? "" : "/") + scriptFileName);
        }
    }

    protected void RegCsss(string basePath, params string[] styleFileName)
    {
        foreach (string item in styleFileName)
        {
            RegCss(System.IO.Path.Combine(basePath, item));
        }
    }
    protected void RegCss(string styleFileName)
    {
        string name = styleFileName.ToLower();
        HtmlLink link = new HtmlLink();
        if (name.StartsWith("http://") || name.StartsWith(Request.ApplicationPath.ToLower()))
        {
            link.Href = styleFileName;
        }
        else
        {
            bool startRoot = name.StartsWith("/");
            link.Href = Request.ApplicationPath + (startRoot ? "" : "/") + styleFileName;
        }
        link.Attributes.Add("type", "text/css");
        link.Attributes.Add("rel", "stylesheet");
        Page.Header.Controls.Add(link);
    }

    protected void RegClientScript(string script, string key)
    {
        Type t = this.GetType();
        if (!this.ClientScript.IsStartupScriptRegistered(t, key))
        {
            this.ClientScript.RegisterStartupScript(t, key, script);
        }
    }
    #endregion

    #region scripts css
    public void Alert(string msg)
    {
        Page.ClientScript.RegisterStartupScript(this.GetType(), "Alert", "alert(\"" + msg + "\");", true);
    }
    /// <summary>
    /// 弹出提示    
    /// </summary>
    /// <param name="msg"></param>
    public void jAlert(string msg)
    {
        jAlert(msg, IconType.Infor, null);
    }
    public void jAlert(string msg, IconType ico)
    {
        jAlert(msg, ico, null);
    }
    public void jAlert(string msg, string callback)
    {
        jAlert(msg, "系统提示", IconType.Infor, callback);
    }
    public void jAlert(string msg, IconType ico, string callback)
    {
        jAlert(msg, "系统提示", ico, callback);
    }
    /// <summary>
    /// 弹出提示，点击确定后执行脚本
    /// 需要引用 jquery-1.2.6.pack.js
    /// </summary>
    /// <param name="msg">提示消息</param>
    /// <param name="title">提示标题</param>
    /// <param name="callback">回调javascript代码或者方法名称</param>
    public void jAlert(string msg, string title, IconType ico, string callback)
    {
        RegScripts("jquery-easyui/easyloader.js");
        System.Text.StringBuilder build = new System.Text.StringBuilder();
        build.Append("$(document).ready(function() {");
        //build.Append("using(['messager'], function(){");
        build.Append("$.messager.alert('" + title + "','" + msg + "','" + ico.ToString().ToLower() + "',");
        build.Append("function(r){" + callback + "});");
        //build.Append("});");
        build.Append("});");
        Page.ClientScript.RegisterStartupScript(this.GetType(), "jAlert", build.ToString().ToLower(), true);
    }
    /// <summary>
    /// 弹出提示确认
    /// 需要引用 jquery-1.2.6.pack.js
    /// </summary>
    /// <param name="msg">提示消息</param>
    /// <param name="okCallback">点击“确定”javascript代码或者javascript方法名称</param>
    /// <param name="cancelCallback">点击“取消”javascript代码或者javascript方法名称</param>
    public void jConfirm(string msg, string okCallback, string cancelCallback)
    {
        jConfirm(msg, "系统提示", okCallback, cancelCallback);
    }
    /// <summary>
    /// 效果同javascript的Confirm()
    /// 需要引用 jquery-1.2.6.pack.js
    /// </summary>
    /// <param name="msg">提示消息</param>
    /// <param name="title">提示标题</param>
    /// <param name="okCallback">点击“确定”javascript代码或者javascript方法名称</param>
    /// <param name="cancelCallback">点击“取消”javascript代码或者javascript方法名称</param>
    public void jConfirm(string msg, string title, string okCallback, string cancelCallback)
    {
        RegScripts("jquery-easyui/easyloader.js");
        System.Text.StringBuilder build = new System.Text.StringBuilder();
        build.Append("<script type=\"text/javascript\">\r");
        build.Append("$(document).ready(function() {\r");
        //build.Append("using(['messager'], function(){");
        build.Append("$.messager.confirm('" + title + " ？', '" + msg + "',");
        build.Append("function(r) {\r");
        build.Append("if(r){\r");
        build.Append(okCallback);
        build.Append("\r}\r");
        build.Append("else{\r");
        build.Append(cancelCallback);
        build.Append("\r}\r");
        build.Append("});");
        //build.Append("});\r");
        build.Append("});");
        build.Append("</script>");
        Page.ClientScript.RegisterStartupScript(this.GetType(), "confirm", build.ToString());
    }

    public void Show(string msg)
    {
        Show(msg, "消息提示");
    }

    public void Show(string msg, string title)
    {
        RegScripts("jquery-easyui/easyloader.js");
        System.Text.StringBuilder build = new System.Text.StringBuilder();
        build.Append("<script type=\"text/javascript\">\r");
        build.Append("$(document).ready(function() {\r");
        //build.Append("using(['messager'], function(){");
        build.Append("$.messager.show({");
        build.AppendFormat("title:'{0}',", title);
        build.AppendFormat("msg:'{0}',", msg);
        build.Append("timeout:3000,");
        build.Append("showType:'slide'");
        build.Append("});");
        //build.Append("});");
        build.Append("});");
        build.Append("</script>");
        Page.ClientScript.RegisterStartupScript(this.GetType(), "showmsg", build.ToString());
    }

    /// <summary>
    /// 注册scripts文件夹下的js文件到页面
    /// </summary>
    /// <param name="fileName"></param>
    public void RegScripts(params string[] fileNames)
    {
        foreach (string fileName in fileNames)
        {
            Page.ClientScript.RegisterClientScriptInclude(fileName, Request.ApplicationPath + "/Scripts/" + fileName);
        }
    }

    public void LoadjQuery()
    {
        RegScripts("jquery-1.6.1.min.js");
    }

    public void LoadMy97DatePicker()
    {
        RegScripts("My97DatePicker/WdatePicker.js");
    }

    public void LoadEasyUI()
    {
        RegScripts("jquery-easyui/easyloader.js");
    }

    public void LoadCss(string fullpath)
    {
        HtmlLink link = new HtmlLink();
        link.Href = fullpath;
        link.Attributes.Add("type", "text/css");
        link.Attributes.Add("rel", "stylesheet");
        Page.Header.Controls.Add(link);
    }
    #endregion

    public void AddConfirm(bool addResult, string redirectUrl)
    {
        OperateConfirm(addResult, string.Format("window.location.href='{0}'", redirectUrl), Operate.Add);
    }

    public void EditConfirm(bool editResult, string redirectUrl)
    {
        OperateConfirm(editResult, string.Format("window.location.href='{0}'", redirectUrl), Operate.Edit);
    }

    public void DelConfirm(bool delResult, string redirectUrl)
    {
        OperateConfirm(delResult, string.Format("window.location.href='{0}'", redirectUrl), Operate.Del);
    }

    public void AddConfirm(bool addResult)
    {
        OperateConfirm(addResult, null, Operate.Add);
    }

    public void EditConfirm(bool editResult)
    {
        OperateConfirm(editResult, null, Operate.Edit);
    }

    public void DelConfirm(bool delResult)
    {
        OperateConfirm(delResult, null, Operate.Del);
    }

    public void ExistChildConfirm()
    {
        jAlert("存在子记录无法删除！");
    }

    public void ExistCurrentTimeRecord()
    {
        jAlert("已经存在当前时间记录！");
    }

    public void OperateConfirm(bool result, string callback, Operate operate)
    {
        string confirmText = GetConfirmText(operate);
        if (result)
            jAlert(string.Format("{0}成功！", confirmText), callback);
        else
            jAlert(string.Format("{0}失败！", confirmText));
    }

    public string GetConfirmText(Operate operate)
    {
        string confirmText = "";
        switch (operate)
        {
            case Operate.Add:
                confirmText = "添加";
                break;
            case Operate.Edit:
                confirmText = "修改";
                break;
            case Operate.Del:
                confirmText = "删除";
                break;
            default:
                break;
        }
        return confirmText;
    }

    public string TextEncode(string text)
    {
        return text.Replace("\r\n", "<br/>");
    }

    public string TextDecode(string text)
    {
        return text.Replace("<br/>", "\r\n");
    }
}

[Flags]
public enum Action
{
    View = 0x00,
    Add = 0x01,
    Edit = 0x02,
    Delete = 0x04,
    Audit = 0x08
}

public enum Operate
{
    Add,
    Edit,
    Del
}

public enum IconType
{
    Infor,
    Warning,
    Question,
    Error
}