using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Collections.Specialized;

public partial class BasePage:System.Web.UI.Page
{
    protected static string cackeKeyPrefix = "CACHE_";
    public BasePage() {
        DefaultAction = Action.None;
        RControls = new List<RControl>();
    }
    /// <summary>
    /// 本页面所属模块ID
    /// </summary>
    public int ModuleId { get; set; }
    
    /// <summary>
    /// 页面默认动作 (默认值Action.View)
    /// 添加,修改，删除数据页面 action=Action.CUD
    /// </summary>
    public Action DefaultAction { get; set; }
    /// <summary>
    /// 页面上需要进行权限控制的控件，
    /// 客户端控件 加上 runat="server" 属性
    /// </summary>
    public ICollection<RControl> RControls { get; set; }
    
    protected override void OnPreRenderComplete(EventArgs e) {
        if (DefaultAction != Action.None) {
            int val = GetIntValue(ModuleId , GetRights());
            bool deny = ((int)DefaultAction & val) != (int)DefaultAction;
            if (deny) {
                Response.Redirect("~/Accessdenied.htm" , true);
            }
            foreach (var item in RControls) {
                if (null==item) {
                    continue;
                }
                item.control.Visible = ((int)item.action & val) == (int)item.action;
            }
        }
        base.OnPreRenderComplete(e);
    }

    /// <summary>
    /// 当前用户对系统模块的操作权限值
    /// </summary>
    /// <returns></returns>
    protected NameValueCollection GetRights() {
        string key=cackeKeyPrefix + UserID;
        NameValueCollection col = CacheManager.GetCache(key) as NameValueCollection;
        if (null == col) {
            col = new NameValueCollection();
            if (User.Identity.IsAuthenticated) {
                FormsIdentity ticket = (FormsIdentity)User.Identity;
                string userData = ticket.Ticket.UserData;
                var query = from p in userData.Split(';').Skip(1)
                            let s = p.Split('=')
                            select new { id = s[0] , val = s[1] };
                foreach (var item in query) {
                    col.Add(item.id , item.val);
                }
            }
            CacheManager.SetCache(key , col , 15);
        }
        return col;
    }

    /// <summary>
    /// 是否具有指定模块的指定操作权限
    /// </summary>
    /// <param name="moduleId"></param>
    /// <param name="action"></param>
    /// <returns></returns>
    protected bool HasRight(int moduleId , Action action) {
        int val = GetIntValue(moduleId , GetRights());
        return ((int)action & val) == (int)action;
    }

    private int GetIntValue(int key,NameValueCollection col) 
    {
        int result = 0;
        string val = col.Get(key.ToString());
        if (null !=val) {
            int.TryParse(val , out result);
        }
        return result;
    }
}

[Flags]
public enum Action
{
    /// <summary>
    /// 页面不需要权限控制
    /// </summary>
    None=0x00,
    /// <summary>
    /// 浏览查看
    /// </summary>
    View = 0x01 ,
    /// <summary>
    /// 添加，修改，删除操作
    /// </summary>
    CUD = 0x02 ,
    /// <summary>
    /// 审核
    /// </summary>
    Audit = 0x04
}

public class RControl
{
    public RControl( Action action,System.Web.UI.Control control) {
        this.action = action;
        this.control = control;
    }
    public Action action { get; set; }
    public System.Web.UI.Control control { get; set; }

}
