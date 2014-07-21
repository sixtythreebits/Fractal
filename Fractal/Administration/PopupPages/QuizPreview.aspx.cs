using System;
using Core;

public partial class administration_PopupPages_QuizPreview : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        InitStartUp();
    }

    void InitStartUp()
    {
        Master.QuizzesEnabled = true;
        Master.FancyBoxEnabled = true;

        long QuizID;
        long.TryParse(Request.QueryString["id"], out QuizID);

        QuizUserControl1.UserID = Master.UserObject.ID;
        QuizUserControl1.QuizID = QuizID;        
        QuizUserControl1.ShowQuizCaption = false;
        QuizUserControl1.ViewMode = QuizViewMode.Preview;
    }
}