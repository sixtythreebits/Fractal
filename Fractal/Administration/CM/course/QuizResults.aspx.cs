using System;
using System.Text;
using Res = Core.Properties.Resources;

public partial class administration_CM_course_QuizResults : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        InitStartUp();
    }

    void InitStartUp()
    {
        Master.Master.JQueryUIEnabled = true;
        Master.Master.FancyBoxEnabled = true;
        Master.Master.FormsEnabled = true;        

        QuizResultsGrid.Settings.ShowFilterRow = false;
        QuizResultsGrid.Settings.ShowFilterRowMenu = false;
        QuizResultsGrid.SettingsBehavior.ColumnResizeMode = DevExpress.Web.ASPxClasses.ColumnResizeMode.Disabled;        
        QuizResultsGrid.SettingsText.EmptyDataRow = Res.InfoNoQuizResults;

        if (!IsPostBack)
        {
            QuizResultsGrid.SettingsPager.PageSize = 10;
        }
    }

    protected void QuizResultsGrid_BeforePerformDataSelect(object sender, EventArgs e)
    {
        var sb = new StringBuilder();
        sb.Append("<data>");
        sb.AppendFormat("<course_id>{0}</course_id>", Master.CourseObject.ID);

        if (!string.IsNullOrEmpty(D1TextBox.Text))
        {
            sb.AppendFormat("<d1>{0}</d1>", D1TextBox.Text);
        }

        if (!string.IsNullOrEmpty(D2TextBox.Text))
        {
            sb.AppendFormat("<d2>{0}</d2>", D2TextBox.Text);
        }

        if (!string.IsNullOrEmpty(SearchTextBox.Text))
        {
            sb.AppendFormat("<search_word>{0}</search_word>", SearchTextBox.Text);
        }
        sb.Append("</data>");
        

        QuizResultsDataSource.SelectParameters["filter"].DefaultValue = sb.ToString();
    }
}