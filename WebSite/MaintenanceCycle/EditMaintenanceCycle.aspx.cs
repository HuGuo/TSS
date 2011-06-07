using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using TSS.BLL;
using Tm = TSS.Models;

public partial class MaintenanceCycle_EditMaintenanceCycle : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
            BindData();
    }

    protected void BindData()
    {
        BindClass();
        BindCycle();
    }

    protected void BindCycle()
    {
        Tm.MaintenanceCycle maintenanceCycle = MaintenanceCycle.Get(
            int.Parse(Request.QueryString["id"]));
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

    //编辑成功与否要有提示框
    protected void btnEdit_Click(object sender, EventArgs e)
    {
        bool result = MaintenanceCycle.Update(new Tm.MaintenanceCycle
        {
            Cycle = tbCycle.Text,
            MaintenanceType = tbType.Text,
            InstallTime = DateTime.Parse(tbInstallTime.Text),
            MaintenanceClassId = int.Parse(ddlClass.SelectedValue),
            EquipmentId = new Guid(ddlEquipment.SelectedValue),
        });
        EditConfirm(result, "window.location.href='MaintenanceCycle.aspx'");
    }
}