using System;
using System.Linq;
using Core;


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
            else
            {
                InitStartUp();
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            
        }

        void InitStartUp()
        {
            AdminPlaceHolder.Visible = Master.UserObject.IsAdmin;

            var C = new Course();
            var list = C.ListUserSubscribedCourses(Master.UserObject.ID, true);
            BonusQuizzesPlaceHolder.Visible = list.Where(c => c.Slug == "algebra" || c.Slug == "geometry").Count() > 1;
        }
    }
}