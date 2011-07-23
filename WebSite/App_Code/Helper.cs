using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class Helper
{
    private static readonly string[] FileTypeClass;
    public static readonly string[] LoginResult = { "SUCCESS" , "口令错误" , "用户不存在" , "用户帐号被禁用" , "FIRSTLOGIN" };
    /// <summary>
    /// QueryString 分页默认名称
    /// </summary>
    public static readonly string queryParam_pagination = "p";
    /// <summary>
    /// QueryString 专业默认名称
    /// </summary>
    public static readonly string queryParam_specialty = "s";
    static Helper() {
        System.IO.DirectoryInfo dir = new System.IO.DirectoryInfo(System.Web.HttpContext.Current.Server.MapPath("~/images/filetype/"));
        var query = from p in dir.EnumerateFiles()
                    select System.IO.Path.GetFileNameWithoutExtension(p.Name);
        List<string> result = new List<string>();
        query.ToList().ForEach(p => { result.AddRange(p.Split('_')); });
        FileTypeClass = result.ToArray();
    }

    public static readonly string NULLOBJECT = "对象不存在";

    public static readonly string EMPTY_DATA = "No Data";

    public static string GetEquipmentField(object equipmentId , string fieldName) {
        using (var equipments =
            TSS.BLL.RepositoryFactory<TSS.BLL.Equipments>.Get()) {
            return equipments.GetDetailValue(equipmentId.ToString() , fieldName);
        }
    }

    public static string GetClassName(string extension) {
        if (!extension.StartsWith(".")) {
            extension = System.IO.Path.GetExtension(extension);
        }
        if (extension == "" || extension == "folder") {
            return "folder";
        }
        string className = extension.ToLower().Replace("." , "");
        if (className != "" && !FileTypeClass.Contains(className)) {
            className = "unknown";
        }
        return className;
    }

    public static void SetAuthCookie(string userCode , bool remberme , System.Web.HttpContext context) {
        //获取用户权限信息
        TSS.Models.Employee entity = TSS.BLL.RepositoryFactory<TSS.BLL.Employees>.Get().GetByCode(userCode);
        var query = from p in entity.Roles
                    from m in p.Rights
                    select new { moduleId = m.ModuleId , permission = m.Permission };
        var result = from p in query
                     group p by p.moduleId into m
                     select m.Key + "=" + m.Max(p => p.permission);
        string ticketData = entity.Name + ";" + string.Join(";" , result.ToArray());

        System.Web.Security.FormsAuthenticationTicket ticket = new System.Web.Security.FormsAuthenticationTicket(1 , entity.Id.ToString() , DateTime.Now , DateTime.Now.AddHours(24) , false , ticketData , "/");

        string EncTick = System.Web.Security.FormsAuthentication.Encrypt(ticket);

        System.Web.HttpCookie userCookie = new System.Web.HttpCookie(System.Web.Security.FormsAuthentication.FormsCookieName , EncTick);

        userCookie.Expires = DateTime.Now.AddHours(24);
        context.Response.Cookies.Add(userCookie);

        //rember me
        if (remberme) {
            System.Web.HttpCookie cookieName = new System.Web.HttpCookie("name" , entity.Name);
            cookieName.Expires = DateTime.Now.AddDays(7);
            context.Response.Cookies.Add(cookieName);
        }
    }

    public static void ResponseEasyuiTreeHTML(System.Xml.XmlReader reader , System.Web.HttpContext context) {
        context.Response.ContentType = "text/xml; charset=UTF-8";
        System.Xml.Xsl.XslCompiledTransform xsl = new System.Xml.Xsl.XslCompiledTransform();
        xsl.Load(context.Server.MapPath("~/equipmentcategory.xslt"));

        System.Xml.Xsl.XsltArgumentList args = new System.Xml.Xsl.XsltArgumentList();
        //src 参数值带有?，&时需要在客户端编码
        string src = context.Request["src"];
        args.AddParam("src" , string.Empty , string.IsNullOrEmpty(src) ? "" : context.Server.UrlDecode(src));
        args.AddParam("target" , string.Empty , context.Request["target"] ?? "");

        args.AddParam("expendDeep" , string.Empty , context.Request["dp"] ?? "0");

        xsl.Transform(reader , args , context.Response.OutputStream);
    }


    public static int GetRealPageIndex(int pagesize,int recordcount,int pageindex) {
        if (pagesize<1 || recordcount <1 || pagesize<1) {
            return pageindex < 1 ? 1 : pageindex;
        }

        //calculate pagecount
        double pagesizef = Convert.ToDouble(pagesize);

        int pagecount = Convert.ToInt32(Math.Ceiling(recordcount / pagesizef));
        return Math.Min(pagecount , pageindex);
    }
}
//扩展字符串方法
public static class StringExtend
{
    public static string GetMD5(this string str) {
        //return System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(str , "MD5");

        char[] chars = str.ToCharArray();
        byte[] inBytes = new byte[chars.Length];
        for (int i = 0; i < inBytes.Length; i++)
            inBytes[i] = Convert.ToByte(chars[i]);

        System.Security.Cryptography.MD5CryptoServiceProvider md = new System.Security.Cryptography.MD5CryptoServiceProvider();
        byte[] outBytes = md.ComputeHash(inBytes);

        return BitConverter.ToString(outBytes);
    }

    public static string GetSHA1(this string str) {
        return System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(str , "SHA1");
    }

    public static string HtmlDecode(this string str) {
        StringBuilder sb = new StringBuilder(str);
        sb.Replace("<br />" , "\n");
        sb.Replace("<br/>" , "\n");
        //  sb.Replace("\r", "");
        sb.Replace("&nbsp;&nbsp;" , "\t");
        sb.Replace("&nbsp;" , " ");
        sb.Replace("&#39;" , "\'");
        sb.Replace("&quot;" , "\"");
        sb.Replace("&gt;" , ">");
        sb.Replace("&lt;" , "<");
        sb.Replace("&amp;" , "&");


        return sb.ToString();
    }

    public static string HtmlEncode(this string str) {
        StringBuilder sb = new StringBuilder(str);
        sb.Replace("&" , "&amp;");
        sb.Replace("<" , "&lt;");
        sb.Replace(">" , "&gt;");
        sb.Replace("\"" , "&quot;");
        sb.Replace("\'" , "&#39;");
        sb.Replace(" " , "&nbsp;");
        sb.Replace("\t" , "&nbsp;&nbsp;");
        sb.Replace("\r" , "");
        sb.Replace("\n" , "<br />");
        return sb.ToString();
    }
}