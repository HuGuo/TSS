using System.Reflection;
using log4net.Layout;
using log4net.Layout.Pattern;


/**
 * 肖宏飞
 * 2011-7-24
 * log4net 扩展 记录当前用户ID，ip，browser等信息
 */
public class MyPatternConverter : PatternLayoutConverter
{
    protected override void Convert(System.IO.TextWriter writer , log4net.Core.LoggingEvent loggingEvent) {
        if (null != Option) {
            WriteObject(writer , loggingEvent.Repository , LookupProperty(Option , loggingEvent));
        } else {
            WriteDictionary(writer , loggingEvent.Repository , loggingEvent.GetProperties());
        }
    }

    /// <summary>  
    /// 通过反射获取传入的日志对象的某个属性的值  
    /// </summary>  
    /// <param name="property"></param>  
    /// <returns></returns>  
    private object LookupProperty(string property , log4net.Core.LoggingEvent loggingEvent) {
        object propertyValue = string.Empty;
        PropertyInfo propertyInfo = loggingEvent.MessageObject.GetType().GetProperty(property);
        if (propertyInfo != null)
            propertyValue = propertyInfo.GetValue(loggingEvent.MessageObject , null);
        return propertyValue;
    }
}

public class MyLayout:PatternLayout
{
    public MyLayout() {
        AddConverter("property" , typeof(MyPatternConverter));
    }
}