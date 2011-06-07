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
        if (!IsPostBack) {
            string categoryId = Request.QueryString["category"];
            string s = Request.QueryString["s"];
            if (!string.IsNullOrWhiteSpace(categoryId)) {
                //加载分类下设备
                rptEqipmentList.DataSource= new TSS.BLL.Equipments().GetAll(categoryId, s);
                rptEqipmentList.DataBind();

                //加载分类下所有设备所做过的实验
                Guid id = new Guid(categoryId);
                IList<Experiment> list = RepositoryFactory<ExperimentRepository>.Get().GetByEquipmentCategory(id, s);
                rptList.DataSource = list;
                rptList.DataBind();
            }

            //加载本专业模板
            
            if (!string.IsNullOrWhiteSpace(s)) {
                IList<TSS.Models.ExpTemplate> list = RepositoryFactory<ExpTemplateRepository>.Get().GetBySpecialty(s);
                rptTmpList.DataSource = list;
                rptTmpList.DataBind();
            }
        }
    }
}