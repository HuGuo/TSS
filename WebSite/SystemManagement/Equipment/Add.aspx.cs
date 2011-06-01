using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class SystemManagement_Equipment_Add : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack) {
            XmlDataSource1.Data = (new TSS.BLL.EquipmentCategories()).GetXml().ToString();
        }
    }

    protected void addButton_Click(object sender, EventArgs e)
    {
        (new TSS.BLL.Equipments()).Add(new TSS.Models.Equipment {
            Id = Guid.NewGuid(),
            Name = TextBox1.Text,
            EquipmentCategoryId = Guid.Parse(TreeView1.SelectedValue),
            SpecialtyId = DropDownList1.SelectedValue
        });
    }
}