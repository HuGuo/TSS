using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using TSS.BLL;
using TSS.Models;

public partial class MaintenanceCycle_EditMaintenanceCycle : BasePage
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
        BindCycle();
    }

    protected void BindCycle()
    {
       MaintenanceCycleRepository repository=new MaintenanceCycleRepository();
       MaintenanceCycle maintenanceCycle = repository.Get(int.Parse(Request.QueryString["id"]));
        tbCycle.Text = maintenanceCycle.Cycle;
        tbType.Text = maintenanceCycle.MaintenanceType;
        tbModel.Text = maintenanceCycle.EquipmentModel;
        ddlEquipment.SelectedValue = maintenanceCycle.EquipmentId.ToString();
        ddlClass.SelectedValue = maintenanceCycle.MaintenanceClassId.ToString();
        tbInstallTime.Text = !maintenanceCycle.InstallTime.HasValue ? "" :
            maintenanceCycle.InstallTime.Value.ToShortDateString();

    }

    protected void BindEquipment()
    {
        
    }

    protected void BindClass()
    {
        MaintenanceClassRepository repository = new MaintenanceClassRepository();
        foreach (MaintenanceClass maintenanceClass in repository.GetAll())
        {
            ddlClass.Items.Add(new ListItem(
                maintenanceClass.equipmentClassName, maintenanceClass.Id.ToString()));
        }
    }

    //编辑成功与否要有提示框
    protected void btnEdit_Click(object sender, EventArgs e)
    {
        MaintenanceCycleRepository repository = new MaintenanceCycleRepository();
        bool result = repository.Update(new MaintenanceCycle
        {
            Cycle = tbCycle.Text,
            MaintenanceType = tbType.Text,
            InstallTime = DateTime.Parse(tbInstallTime.Text),
            MaintenanceClassId = int.Parse(ddlClass.SelectedValue),
            EquipmentId = new Guid(ddlEquipment.SelectedValue),
        });
        EditConfirm(result, string.Format("MaintenanceClass.aspx?sepcialtyId=", Request.QueryString["specialtyId"]));
    }
}