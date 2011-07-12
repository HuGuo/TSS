using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using TSS.BLL;
using TSS.Models;

public partial class UserControl_MaintenanceCycleControl : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
            BindData();
    }

    private void BindData()
    {
        BindEquipment();
        BindClass();
    }

    private string equipmentId;
    private void BindEquipment()
    {
        using (var equipmentBll = RepositoryFactory<Equipments>.Get())
            foreach (Equipment equipment in equipmentBll.GetAll())
                ddlEquipment.Items.Add(new ListItem(equipment.Name, equipment.Id.ToString()));
        ddlEquipment.SelectedValue = equipmentId;
    }

    private string maintenanceClassId;
    private void BindClass()
    {
        using (var maintenanceClassBll = RepositoryFactory<MaintenanceClassRepository>.Get())
            foreach (MaintenanceClass maintenanceClass in maintenanceClassBll.GetAll())
                ddlClass.Items.Add(new ListItem(maintenanceClass.equipmentClassName, maintenanceClass.Id.ToString()));
        ddlClass.SelectedValue = maintenanceClassId;
    }

    public MaintenanceCycle MaintenanceCycle
    {
        get
        {
            MaintenanceCycle maintenanceCycle = new MaintenanceCycle();
            if (ViewState["maintenanceCycle"] != null)
                maintenanceCycle = (MaintenanceCycle)ViewState["maintenanceCycle"];
            maintenanceCycle.Cycle = tbCycle.Text;
            maintenanceCycle.EquipmentModel = tbModel.Text;
            maintenanceCycle.EquipmentId = new Guid(ddlEquipment.SelectedValue);
            maintenanceCycle.InstallTime = string.IsNullOrEmpty(tbInstallTime.Text) ?
                (DateTime?)null : DateTime.Parse(tbInstallTime.Text);
            maintenanceCycle.MaintenanceClassId = int.Parse(ddlClass.SelectedValue);
            maintenanceCycle.MaintenanceType = tbType.Text;
            return maintenanceCycle;
        }
        set
        {
            tbCycle.Text = value.Cycle;
            tbModel.Text = value.EquipmentModel;
            equipmentId = value.EquipmentId.ToString();
            ddlEquipment.SelectedValue = value.EquipmentId.ToString();
            tbInstallTime.Text = value.InstallTime.HasValue ? value.InstallTime.Value.ToString("yyyy-MM-dd") : "";
            maintenanceClassId = value.MaintenanceClassId.ToString();
            ddlClass.SelectedValue = value.MaintenanceClassId.ToString();
            tbType.Text = value.MaintenanceType.ToString();
            ViewState["maintenanceCycle"] = value;
        }
    }

    public void ReSet()
    {
        tbCycle.Text = "";
        tbModel.Text = "";
        ddlEquipment.SelectedValue = "";
        tbInstallTime.Text = "";
        ddlClass.SelectedValue = "";
        tbType.Text = "";
    }

}