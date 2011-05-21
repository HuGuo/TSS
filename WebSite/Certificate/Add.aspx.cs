using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TSS.Models;
using TSS.BLL;
public partial class Certificate_Add : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        Certificate certificate = new Certificate();
        certificate.Number = txtNumber.Text;
        certificate.EpmloyeeName = txtName.Text;
        certificate.CretificationAuthority = txtAuthor.Text;
        certificate.ExpireDateTime = DateTime.Parse(txtExpireDate.Text);
        certificate.Gender = ddlGender.SelectedValue;
        certificate.Project = txtProject.Text;
        certificate.ReceiveDateTime = DateTime.Parse(txtReceiveDate.Text);
        certificate.Remark = txtRemark.Text;
        //certificate.SpecialtyId
    }
}