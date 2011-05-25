using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TSS.Models;
using TSS.BLL;
public partial class Experiment_ExpList : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack) {
            string categoryId = Request.QueryString["category"];            
            Guid id;
            if (Guid.TryParse(categoryId, out id)) {
                ExperimentRepository repository = new ExperimentRepository();
                IList<Experiment> list = repository.GetByEquipmentCategory(id);
                Response.Write(list.Count);
            }
        }
    }
}