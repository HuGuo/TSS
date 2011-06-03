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
        if (!IsPostBack) {
            string id = Request.QueryString["id"];
            if (!string.IsNullOrWhiteSpace(id)) {
                Certificate entity = RepositoryFactory<CertificateRepository>.Get().Get(new Guid(id));
                if (null != entity) {
                    Bind(entity);
                }
            }
        }
    }

    void Bind(Certificate obj) 
    {
        txtNumber.Text = obj.Number;
        txtName.Text = obj.EpmloyeeName;
        ddlGender.SelectedValue = obj.Gender;
        txtAuthor.Text = obj.CretificationAuthority;
        txtExpireDate.Text = obj.ExpireDateTime.ToString("yyyy-MM-dd");
        txtProject.Text = obj.Project;
        txtReceiveDate.Text = obj.ReceiveDateTime.ToString("yyyy-MM-dd");
        txtRemark.Text = obj.Remark;
        txtType.Text = obj.Type;
        

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
        certificate.Type = txtType.Text;
        certificate.SpecialtyId = Request.QueryString["s"];
        string id = Request.QueryString["id"];
        if (!string.IsNullOrWhiteSpace(id)) {
            certificate.Id = new Guid(id);
        } else {
            certificate.Id = System.Guid.NewGuid();
        }
        if (fileUp.HasFile) {
            string path = Server.MapPath(System.Configuration.ConfigurationManager.AppSettings["CertificateScan"]) + "\\";
            string extension= System.IO.Path.GetExtension(fileUp.FileName);
            string savePath = path + certificate.Id + extension;
            fileUp.SaveAs(savePath);
            certificate.ScanFilePath = certificate.Id + extension;
        }        
        if (string.IsNullOrWhiteSpace(id)) {
            RepositoryFactory<CertificateRepository>.Get().Add(certificate);
        } else {
            RepositoryFactory<CertificateRepository>.Get().Update(certificate);
        }
        ltmsg.Text = "<div class='success'>操作成功!</div>";
    }
}