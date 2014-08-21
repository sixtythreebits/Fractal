using System;
using System.Linq;
using Core;
using System.Web.UI.WebControls;


namespace Fractal.Dashboard
{
    public partial class BonusQuizzes : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            InitStartUp();            
        }

        void InitStartUp()
        {
            if (Lib.Service.FindControl<PlaceHolder>(Page,"BonusQuizzesPlaceHolder").Visible)
            {
                
                QuizzesRepeater.DataSource = new Quiz().ListBonusQuizzesWithUserResults(Master.Master.UserObject.ID);
                QuizzesRepeater.DataBind();
            }
            else
            {
                Response.Redirect("~/dashboard/");
            }
        }
    }
}