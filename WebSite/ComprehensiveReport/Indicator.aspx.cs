using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using TSS.BLL;
using TSS.Models;

public partial class ComprehensiveReport_Indicator : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
            BindData();
    }

    public void BindData()
    {
        rptIndicator.DataSource = new IndicatorRepository()
            .GetBySpecialty(Request.QueryString["specialtyId"]);
        rptIndicator.DataBind();
        IndicatorControlAdd.SpecialtyId = Request.QueryString["specialtyId"];
    }
    protected void lbtnDel_Click(object sender, EventArgs e)
    {
        using (var repository = RepositoryFactory<IndicatorRepository>.Get())
            repository.Delete(int.Parse(((LinkButton)sender).CommandArgument));
        BindData();
    }
    protected void lbtnEdit_Click(object sender, EventArgs e)
    {
        using (var repository = RepositoryFactory<IndicatorRepository>.Get())
            IndicatorControlEdit.Indicator = repository.Get(int.Parse(((LinkButton)sender).CommandArgument));
    }

    protected void btnEdit_Click(object sender, EventArgs e)
    {
        using (var repository = RepositoryFactory<IndicatorRepository>.Get())
            repository.Update(IndicatorControlEdit.Indicator);
        IndicatorControlEdit.ReSet();
        BindData();
    }

    protected void btnAdd_Click(object sender, EventArgs e)
    {
        using (var repository = RepositoryFactory<IndicatorRepository>.Get())
            repository.Add(IndicatorControlAdd.Indicator);
        IndicatorControlAdd.ReSet();
        BindData();
    }

    protected void btnAddClose_Click(object sender, EventArgs e)
    {
        IndicatorControlAdd.ReSet();
    }

    protected void btnEditClose_Click(object sender, EventArgs e)
    {
        IndicatorControlEdit.ReSet();
    }
}