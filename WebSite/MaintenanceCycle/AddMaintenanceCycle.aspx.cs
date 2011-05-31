using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using TSS.BLL;
using Tm = TSS.Models;

public partial class MaintenanceCycle_AddMaintenanceCycle : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
            BindData();
    }

    protected void BindData()
    {
        BindEquipment();
        BindClass();
    }

    protected void BindCycle()
    {
        Tm.MaintenanceCycle maintenanceCycle = MaintenanceCycle.GetByEquipment(new
            Guid(ddlEquipment.SelectedValue));
        ddlClass.SelectedValue = maintenanceCycle.MaintenanceCalss.Id.ToString();
        tbCycle.Text = maintenanceCycle.Cycle;
        tbInstallTime.Text = maintenanceCycle.InstallTime.ToShortDateString();
        tbModel.Text = maintenanceCycle.EquipmentModel;
        tbType.Text = maintenanceCycle.MaintenanceType;
    }

    protected void BindClass()
    {
        foreach (Tm.MaintenanceClass maintenanceClass in MaintenanceClass.GetAll())
        {
            ddlClass.Items.Add(new ListItem(
                maintenanceClass.equipmentClassName, maintenanceClass.Id.ToString()));
        }
    }

    protected void BindEquipment()
    {
        foreach (Tm.Equipment equipment in Equipment.GetAll())
        {
            ddlEquipment.Items.Add(new
                ListItem(equipment.Id.ToString(), equipment.Name));
        }
    }

    protected void ddlEquipment_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindCycle();
    }
    protected void btnCancle_Click(object sender, EventArgs e)
    {
        Response.Redirect("MaintenanceCycle.aspx");
    }
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        Tm.MaintenanceCycle maintenanceCycle = new Tm.MaintenanceCycle();
        maintenanceCycle.Cycle = tbCycle.Text;
        maintenanceCycle.MaintenanceType = tbType.Text;
        maintenanceCycle.InstallTime = DateTime.Parse(tbInstallTime.Text);
        maintenanceCycle.MaintenanceClassId = int.Parse(ddlClass.SelectedValue);
        maintenanceCycle.EquipmentId = new Guid(ddlEquipment.SelectedValue);
    }
}