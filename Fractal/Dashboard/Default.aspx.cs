using System;
using System.Linq;
using Core;
using Core.Properties;
using System.Globalization;

namespace Fractal.Dashboard
{
    public partial class Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            InitStartUp();
        }

        void InitStartUp()
        {
            InitSubscribedCourses();
            InitQuizzes();
        }

        void InitSubscribedCourses()
        {
            var C = new Course();
            CoursesRepeater.DataSource = C.ListUserSubscribedCourses(Master.Master.UserObject.ID, true).Select(c => new
            {
                Slug = c.Slug,
                Caption = c.Caption,
                CRTime = c.CRTime.Value.ToString(Resources.FormatDate,new CultureInfo("ka-ge"))
            }).ToList();
            CoursesRepeater.DataBind();
        }

        void InitQuizzes()
        {
            var Q = new Quiz();
            QuizzesRepeater.DataSource = Q.ListCourseQuizzesAllowedForUser(Master.Master.UserObject.ID)
            .Where(q => q.CRTime == null)
            .Select(q => new
            {
                ID = q.ID,
                Slug = q.CourseSlug,
                Caption = q.Caption,
                CourseCaption = q.CourseCaption,
                MaxScore = q.MaxScore,
                ExpDate = q.ExpDate.Value.ToString(Resources.FormatDate, new CultureInfo("ka-ge"))
            });
            QuizzesRepeater.DataBind();
        }
    }
}