using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TSS.Models;
using TSS.BLL;
public partial class Certificate_Add : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //DefaultAction = Action.CUD;
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
        if (!string.IsNullOrWhiteSpace(obj.ScanFilePath)) {
            ltimg.Text = "<img width=\"448\" height=\"298\" class=\"rounded-img\" src=\""+ResolveUrl(obj.ScanFilePath)+"\" alt=\"效果图\" />";
        }
        Hscan.Value = obj.ScanFilePath;
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
        certificate.SpecialtyId = Request.QueryString[Helper.queryParam_specialty];
        string id = Request.QueryString["id"];
        if (!string.IsNullOrWhiteSpace(id)) {
            certificate.Id = new Guid(id);
        } else {
            certificate.Id = System.Guid.NewGuid();
        }
        certificate.ScanFilePath = Hscan.Value;
        if (fileUp.HasFile) {
            string configPath = System.Configuration.ConfigurationManager.AppSettings["CertificateScan"];
            string path = Server.MapPath(configPath) + "\\";
            string extension= System.IO.Path.GetExtension(fileUp.FileName);
            string savePath = path + certificate.Id + extension;
            fileUp.SaveAs(savePath);
            certificate.ScanFilePath = configPath+certificate.Id + extension;
        }
        
        if (string.IsNullOrWhiteSpace(id)) {
            RepositoryFactory<CertificateRepository>.Get().Add(certificate);
            //log
            AppLog.Write("添加资质证书" , AppLog.LogMessageType.Info , "Number="+certificate.Number , typeof(CertificateRepository));
        } else {
            RepositoryFactory<CertificateRepository>.Get().Update(certificate.Id,certificate);
            AppLog.Write("更新资质证书" , AppLog.LogMessageType.Info , "id="+certificate.Id.ToString() , typeof(CertificateRepository));
        }
        ltmsg.Text = "<div class='success'>操作成功!</div>";
    }
}