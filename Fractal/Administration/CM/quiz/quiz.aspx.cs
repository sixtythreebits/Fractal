using System;
using Core;
using Core.Properties;

public partial class administration_CM_quiz : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        InitStartUp();
    }

    void InitStartUp()
    {
        Master.PageTitle = " ";
        Master.DialogText = Resources.ConfirmDeleteQuestion;
        Master.DropzoneEnabled = true;
        Master.QuizMakerEnabled = true;
        Master.FormsEnabled = true;
        Master.JQueryNumericEnabled = true;
        Master.FancyBoxEnabled = true;
        Master.MaskedInputEnabled = true;
        Master.ATooltipEnabled = true;
        Master.TagsEnabled = true;
        
        QuizMakerControl1.Root = Master.Root;
        QuizMakerControl1.UserObject = Master.UserObject;
        QuizMakerControl1.QuizID = long.Parse(Request.QueryString["id"]);
        QuizMakerControl1.OnDataBoud += (s, e) =>
        {
            var Q = (Quiz)s;
            Page.Title = string.Format(Resources.FormatPageTitle, Q.Caption);
        };
    }
}