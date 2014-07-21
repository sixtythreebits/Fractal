using System;
using System.ComponentModel;
using System.Web.UI.WebControls;
using Core;
using Core.Properties;

/*
iud property defines from which page user control is used and loads proper quiz properties.
0 - Quiz bank page
1 - Course graded quiz page
*/
public partial class plugins_QuizMaker_QuizMakerControl : System.Web.UI.UserControl
{
    #region Public Properties
    [Browsable(true)]
    public string Root { set; get; }

    [Browsable(true)]
    public string WebsiteRoot { set; get; }

    [Browsable(true)]
    public User UserObject { set; get; }

    [Browsable(true)]
    public long QuizID { set; get; }

    byte _iud = 0;
    [Browsable(true)]
    public byte iud
    {
        set { _iud = value; }
        get { return _iud; }
    }

    [Browsable(true)]
    public bool ShowCaption
    {
        set { CaptionPlaceHolder.Visible = value; }
        get { return CaptionPlaceHolder.Visible; }
    }
    #endregion Public Properties

    public event EventHandler OnDataBoud;

    public Quiz Q;
    protected long? UserID;
    protected long? OwnerID;

    protected void Page_Load(object sender, EventArgs e)
    {
        InitStartUp();
    }

    void InitStartUp()
    {
        Q = new Quiz(string.Format("<data><quiz_id>{0}</quiz_id></data>", QuizID));
        if (!Q.IsError)
        {
            UserID = UserObject.ID;
            OwnerID = Q.Properties.Element("owner_id") == null ? null : (long?)long.Parse(Q.Properties.Element("owner_id").Value);            

            InitQuizProperties();

            QuizCaptionLiteral.Text =
            QuizCaptionTextBox.Text = Q.Caption;
            QuestionsRepeater.DataSource = Q.Questions;
            QuestionsRepeater.DataBind();            
        }

        if (OnDataBoud != null)
        {
            OnDataBoud(Q, EventArgs.Empty);
        }
    }

    void InitQuizProperties()
    {
        switch (iud)
        {
            case 0:
                {
                    break;
                }
            case 1:
                {
                    InitCourseGradedQuizProperties();
                    break;
                }
            case 2:
                {
                    InitCourseSectionStudyQuizProperties();
                    break;
                }
        }
    }

    void InitCourseGradedQuizProperties()
    {
        var CQ = new Quiz();
        CQ.GetSingleCourseQuiz(CourseID: long.Parse(Request.QueryString["id"]), QuizID: long.Parse(Request.QueryString["qid"]));
        HFIsPublished.Value = CQ.IsPublished ? "1" : "0";
        CourseQuizStartDate.Text = CQ.StartDate.Value.ToString(Resources.FormatDateTime);
        CourseQuizEndDate.Text = CQ.EndDate.Value.ToString(Resources.FormatDateTime);
        //CourseQuizGradeDate.Text = CQ.GradeReleaseDate.Value.ToString(Resources.FormatDateTime);
        CourseQuizGradeDate.Text = DateTime.Now.ToShortDateString();

        IsPublishedControlPlaceHolder.Visible = true;        
        CourseQuizHFPlaceHolder.Visible = true;
        CourseQuizPropertiesPlaceHolder.Visible = true;
    }

    void InitCourseSectionStudyQuizProperties()
    {
        var CQ = new Quiz();
        CQ.GetSingleCourseQuiz(CourseID: long.Parse(Request.QueryString["id"]), QuizID: long.Parse(Request.QueryString["qid"]));
        HFIsPublished.Value = CQ.IsPublished ? "1" : "0";
        IsPublishedControlPlaceHolder.Visible = true;        
    }

    protected void QuestionsRepeater_ItemDataBound(object sender, System.Web.UI.WebControls.RepeaterItemEventArgs e)
    {
        var QuestionObject = (Question)e.Item.DataItem;
        var AnswersRepeater = (Repeater)e.Item.FindControl("AnswersRepeater");

        if (QuestionObject.Answers.Count > 0 && AnswersRepeater != null)
        {            
            AnswersRepeater.DataSource = QuestionObject.Answers;
            AnswersRepeater.DataBind();
        }
    }    
}