using System;
using System.Linq;
using System.Collections.Generic;

public class Helper
{
    private static readonly string[] FileTypeClass;
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
        var equipmentDetails = 
            TSS.BLL.RepositoryFactory<TSS.BLL.EquipmentDetails>.Get();
        return equipmentDetails.GetValue(equipmentId.ToString(), fieldName);
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
            className = "unknow";
        }
        return className;
    }
}