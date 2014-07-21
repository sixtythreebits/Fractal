using System;
using Core;
using Core.Properties;

public partial class administration_CM_course_quizes : System.Web.UI.Page
{
    public Quiz Q = new Quiz();
    protected int QuizCount = 0;

    protected void Page_Load(object sender, EventArgs e)
    {
        InitStartUp();
    }

    void InitStartUp()
    {
        if (Request.Form["action"] == "delete")
        {
            DeleteQuiz();
        }
        else
        {
            Master.Master.DialogText = Resources.ConfirmDeleteCourseQuiz;
            Master.Master.FancyBoxEnabled = true;
            QuizesRepeater.DataSource = Q.ListCourseQuizes(Master.CourseObject.ID.Value);
            QuizesRepeater.DataBind();

            QuizCount = Q.ListTeacherQuizesNotInCourse(Master.Master.UserObject.ID, Master.CourseObject.ID.Value).Count;
        }
    }

    void DeleteQuiz()
    {
        Response.Clear();
        long ID;
        if (long.TryParse(Request.Form["id"], out ID))
        {
            string xml = string.Format(@"
            <data>
                <course>{0}</course>
                <quiz>{1}</quiz>
            </data>",
            Master.CourseObject.ID, ID);
                        
            Q.TX_Quizes(2, xml);
            if (!Q.IsError)
            {
                Response.Write("success");
            }            
        }
        Response.End();
    }
}