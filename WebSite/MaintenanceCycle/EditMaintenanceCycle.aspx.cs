using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using TSS.BLL;
using Tm = TSS.Models;

public partial class MaintenanceCycle_EditMaintenanceCycle : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
            BindData();
    }

    protected void BindData()
    {
        BindClass();
        BindCycle(MaintenanceCycle.Get(
            int.Parse(Request.QueryString["id"])));
    }

    protected void BindCycle(Tm.MaintenanceCycle maintenanceCycle)
    {
        tbCycle.Text = maintenanceCycle.Cycle;
        tbType.Text = maintenanceCycle.MaintenanceType;
        tbModel.Text = maintenanceCycle.EquipmentModel;
        tbInstallTime.Text = maintenanceCycle.InstallTime.Value.ToShortDateString();
        ddlEquipment.SelectedValue = maintenanceCycle.EquipmentId.ToString();
        ddlClass.SelectedValue = maintenanceCycle.MaintenanceClassId.ToString();
    }

    protected void BindClass()
    {
        foreach (Tm.MaintenanceClass maintenanceClass in MaintenanceClass.GetAll())
        {
            ddlClass.Items.Add(new ListItem(
                maintenanceClass.equipmentClassName, maintenanceClass.Id.ToString()));
        }
    }


    protected void btnCancle_Click(object sender, EventArgs e)
    {
        Response.Redirect("MaintenanceCycle.aspx");
    }

    //编辑成功与否要有提示框
    protected void btnEdit_Click(object sender, EventArgs e)
    {
        Tm.MaintenanceCycle maintenanceCycle = new Tm.MaintenanceCycle();
        maintenanceCycle.Cycle = tbCycle.Text;
        maintenanceCycle.MaintenanceType = tbType.Text;
        maintenanceCycle.InstallTime = DateTime.Parse(tbInstallTime.Text);
        maintenanceCycle.MaintenanceClassId = int.Parse(ddlClass.SelectedValue);
        maintenanceCycle.EquipmentId = new Guid(ddlEquipment.SelectedValue);
        MaintenanceCycle.Update(maintenanceCycle);
    }
}