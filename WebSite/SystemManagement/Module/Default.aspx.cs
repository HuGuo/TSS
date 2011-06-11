﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using TSS.BLL;

public partial class SystemManagement_Module_Default : System.Web.UI.Page
{
    private Specialties specilties = RepositoryFactory<Specialties>.Get();

    protected void RemoveButton_Click(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(ListBox1.SelectedValue)) {
            specilties.RemoveModule(DropDownList1.SelectedValue,
                ListBox1.SelectedValue);

            ListBox1.DataBind();
            ListBox2.DataBind();
        }
    }

    protected void AddButton_Click(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(ListBox2.SelectedValue)) {
            specilties.AddModule(DropDownList1.SelectedValue,
                ListBox2.SelectedValue);

            ListBox1.DataBind();
            ListBox2.DataBind();
        }
    }
}