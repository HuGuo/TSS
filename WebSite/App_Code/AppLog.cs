using System;
using System.Web;
using log4net;
/**
 * 肖宏飞
 * 2011-7-24
 * log4net helper
 */
public class AppLog
{
    public AppLog() { }
    /// <summary>
    /// 初始化日志系统
    /// 在系统运行开始初始化
    /// Global.asax Application_Start内
    /// </summary>
    public static void Init() {
        log4net.Config.XmlConfigurator.Configure();
    }

    /// <summary>
    /// 初始化日志系统
    /// 在系统运行开始初始化
    /// Global.asax Application_Start内
    /// </summary>
    /// <param name="configPath">自定义配置文件路径</param>
    public static void Init(string configPath) {
        log4net.Config.XmlConfigurator.ConfigureAndWatch(new System.IO.FileInfo(configPath));
    }

    /// <summary>
    /// 写入日志
    /// </summary>
    /// <param name="message">日志信息</param>
    /// <param name="messageType">日志类型</param>
    public static void Write(string message , LogMessageType messageType) {
        DoLog(message , messageType , "" , null , Type.GetType("System.Object"));
    }

    /// <summary>
    /// 写入日志
    /// </summary>
    /// <param name="message">日志信息</param>
    /// <param name="messageType">日志类型</param>
    public static void Write(string message , LogMessageType messageType , string parameters) {
        DoLog(message , messageType,parameters , null , Type.GetType("System.Object"));
    }

    /// <summary>
    /// 写入日志
    /// </summary>
    /// <param name="message">日志信息</param>
    /// <param name="messageType">日志类型</param>
    /// <param name="type"></param>
    public static void Write(string message , LogMessageType messageType , string parameters , Type type) {
        DoLog(message , messageType,parameters , null , type);
    }

    /// <summary>
    /// 写入日志
    /// </summary>
    /// <param name="message">日志信息</param>
    /// <param name="messageType">日志类型</param>
    /// <param name="ex">异常</param>
    public static void Write(string message , LogMessageType messageType , string parameters , Exception ex) {
        DoLog(message , messageType,parameters , ex , Type.GetType("System.Object"));
    }

    /// <summary>
    /// 写入日志
    /// </summary>
    /// <param name="message">日志信息</param>
    /// <param name="messageType">日志类型</param>
    /// <param name="ex">异常</param>
    /// <param name="type"></param>
    public static void Write(string message , LogMessageType messageType , string parameters , Exception ex ,
                             Type type) {
        DoLog(message , messageType,parameters , ex , type);
    }

    /// <summary>
    /// 断言
    /// </summary>
    /// <param name="condition">条件</param>
    /// <param name="message">日志信息</param>
    public static void Assert(bool condition , string message) {
        Assert(condition , message , Type.GetType("System.Object"));
    }

    /// <summary>
    /// 断言
    /// </summary>
    /// <param name="condition">条件</param>
    /// <param name="message">日志信息</param>
    /// <param name="type">日志类型</param>
    public static void Assert(bool condition , string message , Type type) {
        if (condition == false)
            Write(message , LogMessageType.Info);
    }

    /// <summary>
    /// 保存日志
    /// </summary>
    /// <param name="message">日志信息</param>
    /// <param name="messageType">日志类型</param>
    /// <param name="ex">异常</param>
    /// <param name="type">日志类型</param>
    private static void DoLog(string message , LogMessageType messageType,string parameters , Exception ex ,Type type) {
        HttpContext context = HttpContext.Current;
        MoreInfo obj = new MoreInfo { 
            Message=message,
            Ip=context.Request.UserHostAddress,
            OperatorId = context.User == null ? "" : context.User.Identity.Name ,
            Browser=context.Request.UserAgent,
            Params=parameters
        };
        ILog log=LogManager.GetLogger(type);
        switch (messageType) {
            case LogMessageType.Debug:
                log.Debug(obj , ex);
                break;
            case LogMessageType.Info:
                log.Info(obj , ex);
                break;
            case LogMessageType.Warn:
                log.Warn(obj , ex);
                break;
            case LogMessageType.Error:
                log.Error(obj , ex);
                break;
            case LogMessageType.Fatal:
                log.Fatal(obj , ex);
                break;
        }
    }

    /// <summary>
    /// 日志类型
    /// </summary>
    public enum LogMessageType
    {
        /// <summary>
        /// 调试
        /// </summary>
        Debug ,
        /// <summary>
        /// 信息
        /// </summary>
        Info ,
        /// <summary>
        /// 警告
        /// </summary>
        Warn ,
        /// <summary>
        /// 错误
        /// </summary>
        Error ,
        /// <summary>
        /// 致命错误
        /// </summary>
        Fatal
    }

    public class MoreInfo
    {
        public MoreInfo() { }
        public string Message { get; set; }

        /// <summary>
        /// 操作用户id
        /// </summary>
        public string OperatorId { get; set; }
        /// <summary>
        /// ip
        /// </summary>
        public string Ip { get; set; }
        /// <summary>
        /// 参数
        /// </summary>
        public string Params { get; set; }

        /// <summary>
        /// 客户端浏览器
        /// </summary>
        public string Browser { get; set; }
    }
}