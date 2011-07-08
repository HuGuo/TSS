using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class Helper
{
    private static readonly string[] FileTypeClass;
    public static readonly string[] LoginResult = { "SUCCESS" , "口令错误" , "用户不存在" , "用户帐号被禁用","FIRSTLOGIN" };
    static Helper() {
        System.IO.DirectoryInfo dir = new System.IO.DirectoryInfo(System.Web.HttpContext.Current.Server.MapPath("~/images/filetype/"));
        var query = from p in dir.EnumerateFiles()
                    select System.IO.Path.GetFileNameWithoutExtension(p.Name);
        List<string> result = new List<string>();
        query.ToList().ForEach(p => { result.AddRange(p.Split('_')); });
        FileTypeClass = result.ToArray();
    }
    public static readonly string NULLOBJECT = "对象不存在";
    
    public static readonly string EmptyData = "No Data";

    public static string GetEquipmentField(object equipmentId, string fieldName)
    {
        using (var equipmentDetails =
            TSS.BLL.RepositoryFactory<TSS.BLL.EquipmentDetails>.Get()) {
            return equipmentDetails.GetValue(equipmentId.ToString(), fieldName);
        }
    }

    public static string GetClassName(string extension) {        
        if (!extension.StartsWith(".")) {
            extension = System.IO.Path.GetExtension(extension);
        }
        if (extension=="" || extension=="folder") {
            return "folder";
        }
        string className = extension.ToLower().Replace(".","");
        if (className !="" && !FileTypeClass.Contains(className)) {
            className = "unknown";
        }
        return className;
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
        sb.Replace("\'", "&#39;");
        sb.Replace(" " , "&nbsp;");
        sb.Replace("\t" , "&nbsp;&nbsp;");
        sb.Replace("\r" , "");
        sb.Replace("\n" , "<br />");
        return sb.ToString();
    }
}