using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TSS.Models;
using TSS.BLL;
public partial class Experiment_ExpList : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //RegScripts("scripts" , "jquery-1.6.1.min.js" , "jquery-easyui/jquery.easyui.min.js");
        if (!IsPostBack) {
            string tid = Request.QueryString["id"];
            string s = Request.QueryString["s"];
            ExpTemplate obj = RepositoryFactory<ExpTemplateRepository>.Get().Get(new Guid(tid));
            if (null !=obj) {
                rptList.DataSource = obj.Experiments;
                rptList.DataBind();

                goback.HRef = string.Format("categorylist.aspx?s={0}&category={1}" , s , obj.ExpCategoryId);

                //模板相关设备
                rptEqList.DataSource = obj.Equipments;
                rptEqList.DataBind();
            }
        }
    }
}