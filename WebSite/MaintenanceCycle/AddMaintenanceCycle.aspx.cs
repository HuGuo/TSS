using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using TSS.BLL;
using TSS.Models;

public partial class MaintenanceCycle_AddMaintenanceCycle : BasePage
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
        MaintenanceCycleRepository repository = new MaintenanceCycleRepository();
        MaintenanceCycle maintenanceCycle = repository.GetByEquipment(
            new Guid(ddlEquipment.SelectedValue));
        if (maintenanceCycle != null)
        {
            tbCycle.Text = maintenanceCycle.Cycle;
            tbModel.Text = maintenanceCycle.EquipmentModel;
            tbType.Text = maintenanceCycle.MaintenanceType;
            tbInstallTime.Text = maintenanceCycle.InstallTime.HasValue ?
                maintenanceCycle.InstallTime.Value.ToString() : "";
            ddlClass.SelectedValue = maintenanceCycle.MaintenanceCalss.Id.ToString();
        }
    }

    protected void BindClass()
    {
        IList<MaintenanceClass> maintenanceCycles = new MaintenanceClassRepository().GetAll();
        foreach (MaintenanceClass maintenanceClass in maintenanceCycles)
        {
            ddlClass.Items.Add(new ListItem(
                maintenanceClass.equipmentClassName, maintenanceClass.Id.ToString()));
        }
    }

    protected void BindEquipment()
    {
        foreach (Equipment equipment in new Equipments().GetAll())
        {
            ddlEquipment.Items.Add(new
                ListItem(equipment.Name, equipment.Id.ToString()));
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
        MaintenanceCycleRepository repository = new MaintenanceCycleRepository();
        bool result = repository.Add(new MaintenanceCycle
        {
            Cycle = tbCycle.Text,
            MaintenanceType = tbType.Text,
            InstallTime = string.IsNullOrEmpty(tbInstallTime.Text) ?
                (DateTime?)null : DateTime.Parse(tbInstallTime.Text),
            MaintenanceClassId = int.Parse(ddlClass.SelectedValue),
            EquipmentId = new Guid(ddlEquipment.SelectedValue),
            EquipmentModel = tbModel.Text
        });
        AddConfirm(result, "MaintenanceCycle.aspx");
    }
}