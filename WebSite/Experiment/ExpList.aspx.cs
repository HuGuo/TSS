using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TSS.Models;
using TSS.BLL;
public partial class Experiment_ExpList : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //RegScripts("scripts" , "jquery-1.6.1.min.js" , "jquery-easyui/jquery.easyui.min.js");
        int p = PageIndex;
        int rowcount=0;
        if (!IsPostBack) {
            string tid = Request.QueryString["id"]; //模板id
            string s = Request.QueryString[Helper.queryParam_specialty]; //专业

            IList<Experiment> list = RepositoryFactory<ExperimentRepository>.Get().GetList(new Guid(tid) , null , p , PageSize , out rowcount);
            PageIndex = Helper.GetRealPageIndex(PageSize , rowcount , p);
            Pagination.PageSize = PageSize;
            Pagination.RecordCount = rowcount;

            rptList.DataSource = list;
            rptList.DataBind();

            goback.HRef = string.Format("categorylist.aspx?s={0}&category={1}" , s , Request.QueryString["category"]);
            linkAdd.HRef = string.Format("FillExperimentReport.aspx?s={0}&tid={1}&eqmId=" , s , tid);
        }
    }
}