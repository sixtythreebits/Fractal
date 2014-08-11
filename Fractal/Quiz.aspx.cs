using System;
using Core;
using Core.Properties;
using Lib;

namespace Fractal
{
    public partial class QuizPage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            InitStartup();
        }

        void InitStartup()
        {
            Master.DialogText = Resources.ConfirmSubmitQuiz;
            Master.QuizzesEnabled = true;
            Master.FancyBoxEnabled = true;
            var C = new Course(Request.QueryString["cid"]);
            var QuizID = Request.QueryString["id"].ToLong();


            QuizUserControl1.Visible = true;
            QuizUserControl1.UserID = Master.UserObject.ID;
            QuizUserControl1.CourseID = C.ID;
            QuizUserControl1.ShowQuizCaption = false;
            QuizUserControl1.QuizID = QuizID;
            QuizUserControl1.ViewMode = QuizViewMode.Pass;
            QuizUserControl1.QuizDataBound += delegate(object sender, EventArgs e)
            {
                var uc = (UserControls_QuizUserControl)sender;
                QuizCaptionLiteral.Text = uc.QuizObject.Caption;                
            };
        }
    }
}