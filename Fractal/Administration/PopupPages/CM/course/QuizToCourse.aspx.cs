using System;
using System.Text;
using System.Web.UI.WebControls;
using Core;
using Core.Utilities;
using Core.Properties;
using Lib;

public partial class administration_PopupPages_CM_course_QuizToCourse : System.Web.UI.Page
{
    public Quiz Q = new Quiz();
    long? CourseID;
    long? RecordID;

    protected void Page_Load(object sender, EventArgs e)
    {
        InitStartUp();        
    }

    void InitStartUp()
    {
        Master.JQueryUIEnabled = true;
        Master.TimePickerEnabled = true;
        Master.FormsEnabled = true;

        CourseID = Request.QueryString["cid"].ToLong();
        RecordID = Request.QueryString["id"].ToLong();


        Q = new Quiz();
        Q.GetSingleCourseQuiz(RecordID);
        if (!Q.IsError && !IsPostBack)
        {
            IsPublishedHF.Value = Q.IsPublished.ToString().ToLower();
            StartDateTextBox.Text = Q.StartDate.HasValue ? Q.StartDate.Value.ToString(Resources.FormatDateTime) : string.Empty;
            EndDateTextBox.Text = Q.EndDate.HasValue ? Q.EndDate.Value.ToString(Resources.FormatDateTime) : string.Empty;            
        }


        QuizzesDataSource.SelectParameters["UserID"].DefaultValue = Course.GetCreatorUserID(CourseID).ToString();
        QuizzesDataSource.SelectParameters["CourseID"].DefaultValue = CourseID.ToString();
    }

    
    protected void QuizesCombo_DataBound(object sender, EventArgs e)
    {
        if (RecordID > 0)
        {
            var item = new ListItem(Q.Caption, Q.ID.ToString());
            item.Selected = true;
            QuizesCombo.Items.Insert(0, item);
        }
        else if(QuizesCombo.Items.Count>0)
        {
            QuizesCombo.Items[0].Selected = true;
        }
    }

    protected void SaveButton_Click(object sender, EventArgs e)
    {
        if (IsFormValid())
        {
            var sb = new StringBuilder();
            sb.Append("<data>")
                .Append("<iud>6</iud>")
                .AppendFormat("<course_id>{0}</course_id>", CourseID)
                .Append(NewQuizRadioButton.Checked ? null : string.Format("<quiz_id>{0}</quiz_id>", QuizesCombo.SelectedValue))
                .Append(NewQuizRadioButton.Checked ? string.Format("<caption>{0}</caption>", QuizCaptionTextBox.Text) : null)
                .Append(string.IsNullOrEmpty(StartDateTextBox.Text) ? string.Empty : string.Format("<start_date>{0}</start_date>", DateTime.Parse(StartDateTextBox.Text)))
                .Append(string.IsNullOrEmpty(EndDateTextBox.Text) ? string.Empty : string.Format("<end_date>{0}</end_date>", DateTime.Parse(EndDateTextBox.Text)))                                
                .Append("<is_practice>0</is_practice>")
                .AppendFormat("<is_published>{0}</is_published>", (IsPublishedHF.Value == "true" ? "1" : "0"))
                .Append("<show_answers>0</show_answers>")
            .Append("</data>");
            //System.IO.File.WriteAllText(AppDomain.CurrentDomain.BaseDirectory + "file.xml", sb.ToString());
            Q = new Quiz();
            Q.TX_Quizes(6, sb.ToString());
            if (Q.IsError)
            {
                HeadLiteral.Text = "<script> $(document).ready(function(){ alert(Abort); }); </script>";
            }
            else
            {
                var URL = NewQuizRadioButton.Checked ? string.Format("\"/CM/course/quiz/quiz.aspx?id={0}&qid={1}\"", CourseID, Q.Properties.Value) : "window.parent.document.URL";
                HeadLiteral.Text = string.Format("<script> ClosePopup(); window.parent.location = {0}; </script>", URL);
            }
        }
    }
    
    bool IsFormValid()
    {
        var IsValid = true;
        var ScriptStr = new StringBuilder();

        if (NewQuizRadioButton.Checked)
        {
            if (string.IsNullOrWhiteSpace(QuizCaptionTextBox.Text))
            {
                ScriptStr.Append("SetInputFieldError(\"quizes\", RequiredQuiz, \"QuizError\");");
            }
        }
        else
        {
            if (QuizesCombo.SelectedIndex == -1)
            {
                ScriptStr.Append("SetInputFieldError(\"quizes\", RequiredQuiz, \"QuizError\");");
            }
        }

        if (string.IsNullOrEmpty(StartDateTextBox.Text))
        {            
            ScriptStr.Append(" SetInputFieldError(\"start-date\", RequiredStartDate, \"StartDateError\"); \n");
        }

        if (string.IsNullOrEmpty(EndDateTextBox.Text))
        {            
            ScriptStr.Append(" SetInputFieldError(\"end-date\", RequiredEndDate, \"EndDateError\"); \n");
        }

        if (!string.IsNullOrEmpty(StartDateTextBox.Text) && !string.IsNullOrEmpty(EndDateTextBox.Text))
        {
            DateTime d1;
            DateTime d2;
            if (DateTime.TryParse(StartDateTextBox.Text, out d1) && DateTime.TryParse(EndDateTextBox.Text, out d2))
            {
                if (d1 > d2)
                {
                    ScriptStr.Append("SetInputFieldError(\"end-date\", StartGreaterThanEnd, \"EndDateError\"); \n");
                }
            }            
        }
                
        if (ScriptStr.Length > 0)
        {
            IsValid = false;
            HeadLiteral.Text = string.Format("<script> $(document).ready(function(){{ {0} }}); </script>", ScriptStr.ToString());
        }                        

        return IsValid;
    }    
}
