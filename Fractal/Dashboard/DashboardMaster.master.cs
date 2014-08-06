using System;


namespace Fractal.Dashboard
{
    public partial class DashboardMaster : System.Web.UI.MasterPage
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            if (!Master.UserObject.IsAuthorized)
            {
                Response.Redirect("~/");
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {

        }
    }
}