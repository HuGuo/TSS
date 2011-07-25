<%@ Application Language="C#" %>

<script runat="server">

    void Application_Start(object sender, EventArgs e) 
    {
        // Code that runs on application startup
        AppLog.Init(Server.MapPath("~/log4net.config"));
        AppLog.Write("应用程序启动" , AppLog.LogMessageType.Info);
    }
    
    void Application_End(object sender, EventArgs e) 
    {
        //  Code that runs on application shutdown
        AppLog.Write("应用程序停止" , AppLog.LogMessageType.Info);
    }
        
    void Application_Error(object sender, EventArgs e) 
    { 
        // Code that runs when an unhandled error occurs
        Exception ex = Server.GetLastError();
        if (null != ex) {

            AppLog.Write("应用程序发生致命错误:" + ex.Message , AppLog.LogMessageType.Fatal , "" , ex);
        }
    }

    void Session_Start(object sender, EventArgs e) 
    {
        // Code that runs when a new session is started
        
    }

    void Session_End(object sender, EventArgs e) 
    {
        // Code that runs when a session ends. 
        // Note: The Session_End event is raised only when the sessionstate mode
        // is set to InProc in the Web.config file. If session mode is set to StateServer 
        // or SQLServer, the event is not raised.

    }
       
</script>
