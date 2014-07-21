using System;
using System.Linq;
using System.ComponentModel;
using System.Web.UI.WebControls;
using Lib;
using Core;

public partial class UserControls_QuizUserControl : System.Web.UI.UserControl
{
    #region Properties

    #region Public Properties
    public Quiz QuizObject = new Quiz();

    [Browsable(true)]
    public long? UserID { get; set; }
    [Browsable(true)]    
    public bool ShowQuizCaption
    {
        set { QuizTitlePlaceHolder.Visible = value; }
        get { return QuizTitlePlaceHolder.Visible; }
    }
    [Browsable(true)]
    public long? QuizID { set; get; }
    [Browsable(true)]
    public long? CourseID { set; get; }
    [Browsable(true)]
    public long? SectionID { set; get; }
    [Browsable(true)]
    public long? AssetID { set; get; }
    [Browsable(true)]
    public QuizViewMode ViewMode { set; get; }    
    #endregion Public Properties

    #region Private Properties
    Question CurrentQuestion;    
    string AnswerAdditionalClass = string.Empty;    
    #endregion Private Properties    
    #endregion Properties

    #region Events
    /// <summary>
    /// Some custom actions after data binding
    /// </summary>
    public event EventHandler QuizDataBound;
    #endregion Events

    protected virtual void OnQuizDataBound()
    {
        EventHandler handler = this.QuizDataBound;
        if (handler != null)
        {
            handler(this, EventArgs.Empty);
        }
    }

    protected void Page_Init(object sender, EventArgs e)
    {
        UserID = 0;
        QuizID = 0;
        CourseID = 0;
        AssetID = 0;
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        InitStartup();
    }

    protected void InitStartup()
    {
        string xml = string.Format("<data><quiz_id>{0}</quiz_id>{1}{2}{3}</data>",
                                   QuizID,
                                   UserID > 0 ? string.Format("<user_id>{0}</user_id>", UserID) : string.Empty,
                                   CourseID > 0 ? string.Format("<course_id>{0}</course_id>", CourseID) : string.Empty,
                                   SectionID > 0 ? string.Format("<section_id>{0}</section_id>", SectionID) : string.Empty,
                                   AssetID > 0 ? string.Format("<asset_id>{0}</asset_id>", AssetID) : string.Empty
                                  );
        //System.IO.File.WriteAllText(AppDomain.CurrentDomain.BaseDirectory + "file.xml", xml);
        QuizObject.GetSingleQuiz(xml);
        if (!QuizObject.IsError && (
            (QuizObject.IsPublished && !QuizObject.IsExpired && ViewMode == QuizViewMode.Pass) ||
            ViewMode != QuizViewMode.Pass
            )
        )
        {
            ViewMode = QuizObject.IsTaken && ViewMode == QuizViewMode.Pass ? QuizViewMode.Passed : ViewMode;
            QuizPlaceHolder.Visible = true;
            QuizCaptionLiteral.Text = QuizObject.Caption;
            QuestionsRepeater.DataSource = QuizObject.Questions;
            QuestionsRepeater.DataBind();

            HFData.Value = string.Format(
            "<data><quiz_id>{0}</quiz_id><user_id>{1}</user_id>{2}{3}</data>",
            QuizID,
            UserID,
            CourseID == 0 ? string.Empty : string.Format("<course_id>{0}</course_id>", CourseID),
            AssetID == 0 ? string.Empty : string.Format("<asset_id>{0}</asset_id>", AssetID)
            ).EncryptWeb();

            FinishQuizPlaceHolder.Visible = !QuizObject.IsTaken && ViewMode == QuizViewMode.Pass;
        }
        else
        {
            NotFoundAction();
        }
        OnQuizDataBound();
    }

    void NotFoundAction()
    {
        ShowQuizCaption = true;
        QuizCaptionLiteral.Text = "Quiz Not Found";
    }

    protected void QuestionsRepeater_ItemDataBound(object sender, System.Web.UI.WebControls.RepeaterItemEventArgs e)
    {
        CurrentQuestion = (Question)(e.Item.DataItem);

        ((PlaceHolder)e.Item.FindControl("QuestionAssetPlaceHolder")).Visible = CurrentQuestion.AssetID > 0;
        var ShowHintPlaceHolder = (PlaceHolder)e.Item.FindControl("ShowHintPlaceHolder");
        var HintPlaceHolder = (PlaceHolder)e.Item.FindControl("HintPlaceHolder");
        var ShowAnalysisPlaceHolder = (PlaceHolder)e.Item.FindControl("ShowAnalysisPlaceHolder");
        var AnalysisPlaceHolder = (PlaceHolder)e.Item.FindControl("AnalysisPlaceHolder");
        var ShowCorrectAnswerPlaceHolder = (PlaceHolder)e.Item.FindControl("ShowCorrectAnswerPlaceHolder");
        var ShowVideoExplanationPlaceHolder = (PlaceHolder)e.Item.FindControl("ShowVideoExplanationPlaceHolder");
        var VideoExplanationPlaceHolder = (PlaceHolder)e.Item.FindControl("VideoExplanationPlaceHolder");
        var PointsPlaceHolder = (PlaceHolder)e.Item.FindControl("PointsPlaceHolder");

        ShowHintPlaceHolder.Visible =
        HintPlaceHolder.Visible = !string.IsNullOrEmpty(CurrentQuestion.Hint) && QuizObject.ShowHints;

        //ShowAnalysisPlaceHolder.Visible =
        //AnalysisPlaceHolder.Visible = !string.IsNullOrEmpty(CurrentQuestion.Analysis) && (QuizObject.ShowAnalysis || ViewMode == QuizViewMode.Preview || ViewMode == QuizViewMode.Study);

        //ShowCorrectAnswerPlaceHolder.Visible = (ViewMode == QuizViewMode.Study || ViewMode == QuizViewMode.Preview) && CurrentQuestion.Answers.Count > 0;

        //ShowVideoExplanationPlaceHolder.Visible =
        //VideoExplanationPlaceHolder.Visible = (ViewMode == QuizViewMode.Study || QuizObject.IsTaken || ViewMode == QuizViewMode.Preview) && CurrentQuestion.VideoAnswerID.HasValue;

        PointsPlaceHolder.Visible = ViewMode != QuizViewMode.Study;
                                
        AnswerAdditionalClass = string.Empty;
        // Scan answers and check what additional class to apply to them
        foreach (var A in CurrentQuestion.Answers)
        {
            if (A.answer.Length > 18)
            {
                AnswerAdditionalClass = string.Empty;
                break;
            }
            else if (A.answer.Length > 0 && A.answer.Length < 18)
            {
                AnswerAdditionalClass = "medium";
            }
            else if (AnswerAdditionalClass != "medium" && string.IsNullOrEmpty(A.answer))
            {
                AnswerAdditionalClass = "small";
            }
        }

        var AnswersRepeater = (Repeater)e.Item.FindControl("AnswersRepeater");
        AnswersRepeater.DataSource = CurrentQuestion.Answers;
        AnswersRepeater.DataBind();

        //Show statistics of how other students gave their answer to the question
        if (QuizObject.ShowOtherStudentAnswers && QuizObject.IsTaken)
        {
            var StatsPlaceHolder = (PlaceHolder)e.Item.FindControl("StatsPlaceHolder");
            var StatsRepeater = (Repeater)e.Item.FindControl("StatsRepeater");

            StatsPlaceHolder.Visible = true;
            decimal TotalAnsweredCount = CurrentQuestion.Answers.Sum(a => a.AnsweredCount);

            StatsRepeater.DataSource = CurrentQuestion.Answers.Select((a, index) => new
            {
                Index = (char)(index + 65),
                Percent = string.Format("{0}%", Math.Round(Convert.ToDecimal(100 * a.AnsweredCount) / TotalAnsweredCount))
            }).ToList();
            StatsRepeater.DataBind();
        }        
    }

    protected void AnswersRepeater_ItemDataBound(object sender, System.Web.UI.WebControls.RepeaterItemEventArgs e)
    {
        var CheckBoxLiteral = (Literal)e.Item.FindControl("CheckBoxLiteral");
        var a = (Answer)e.Item.DataItem;
        //REMOVE THIS COMMENTED BLOCK IF ANSWER REALLY CANT CONTAIN FILES
        //if (a.FileID.HasValue && a.FileID.Value > 0)
        //{
        //    ((PlaceHolder)e.Item.FindControl("fileAnswer")).Visible = true;
        //}
        // In sections we dont need any user selected data
        if (ViewMode != QuizViewMode.Study)
        {
            CheckBoxLiteral.Text = string.Format("<input type='{0}' name='answ_{1}' {2} {3} />",
                                                 CurrentQuestion.CorrectAnswerCount > 1 ? "checkbox" : "radio",
                                                 CurrentQuestion.ID.ToString().EncryptWeb(),
                                                 (ViewMode == QuizViewMode.Passed || ViewMode == QuizViewMode.PassedAnswers) && a.IsUserAnswer ? "checked='checked'" : string.Empty,
                                                 QuizObject.IsTaken || (ViewMode == QuizViewMode.Passed || ViewMode == QuizViewMode.PassedAnswers) ? "disabled='disabled'" : string.Empty
                                                );
        }
    }
        
    /// <summary>
    /// Shows the correct answer
    /// </summary>
    /// <param name="isCorrect">Parameter from the DB to show the correctness</param>
    /// <param name="isUserAnswer">Parameter to quess if the answer was provided by user</param>
    /// <returns>Class name</returns>
    protected string GetAnswerClass(bool IsCorrect, bool IsUserAnswer)
    {
        // Show if the answer is correct
        if (ViewMode == QuizViewMode.Study || ViewMode == QuizViewMode.Preview)
        {
            return string.Format("{0}{1}", AnswerAdditionalClass, IsCorrect ? " cor" : string.Empty);
        }
        else if (
            (QuizObject.IsTaken && QuizObject.ShowAnswers && ViewMode == QuizViewMode.Passed) || // Student View
            (ViewMode == QuizViewMode.PassedAnswers) // Teacher view
        )
        {
            if (IsCorrect)
            {
                return string.Format("{0}{1}", AnswerAdditionalClass, " corect");
            }

            if (!IsCorrect && IsUserAnswer)
            {
                return string.Format("{0}{1}", AnswerAdditionalClass, " wrong");
            }
        }
        return AnswerAdditionalClass;
    }
}