using System;
using System.Linq;
using Core;
using Core.Properties;
using System.Globalization;

namespace Fractal.Dashboard
{
    public partial class QuizResults : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            InitStartUp();
        }

        void InitStartUp()
        {
            var Q = new Quiz();
            QuizzesRepeater.DataSource = Q.ListCourseQuizzesAllowedForUser(Master.Master.UserObject.ID)
            .Where(q => q.CRTime.HasValue)
            .Select(q => new
            {
                ID = q.ID,
                Caption = q.Caption,
                MaxScore = q.MaxScore,
                StudentScore = q.StudentScore,
                CRTime = q.CRTime.Value.ToString(Resources.FormatDate, new CultureInfo("ka-ge"))
            });
            QuizzesRepeater.DataBind();
        }
    }
}