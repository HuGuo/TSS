﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using TSS.BLL;
using Tm = TSS.Models;

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
        Tm.MaintenanceCycle maintenanceCycle = MaintenanceCycle.GetByEquipment(new
            Guid(ddlEquipment.SelectedValue));
        if (maintenanceCycle != null)
        {
            tbCycle.Text = maintenanceCycle.Cycle;
            tbModel.Text = maintenanceCycle.EquipmentModel;
            tbType.Text = maintenanceCycle.MaintenanceType;
            tbInstallTime.Text = maintenanceCycle.InstallTime.Value.ToString();
            ddlClass.SelectedValue = maintenanceCycle.MaintenanceCalss.Id.ToString();
        }
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
        foreach (Tm.Equipment equipment in new Equipments().GetAll())
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
        bool result = MaintenanceCycle.Add(new Tm.MaintenanceCycle
        {
            Cycle = tbCycle.Text,
            MaintenanceType = tbType.Text,
            InstallTime = string.IsNullOrEmpty(tbInstallTime.Text) ?
                (DateTime?)null : DateTime.Parse(tbInstallTime.Text),
            MaintenanceClassId = int.Parse(ddlClass.SelectedValue),
            EquipmentId = new Guid(ddlEquipment.SelectedValue),
            EquipmentModel = tbModel.Text
        });
        AddConfirm(result, "window.location.href='MaintenancCycle.aspx'");
    }
}