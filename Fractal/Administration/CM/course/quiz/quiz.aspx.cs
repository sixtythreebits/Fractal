using System;
using System.Web.UI.WebControls;
using Core;
using Core.Properties;

public partial class administration_CM_course_quiz_quiz : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        InitStartUp();
    }

    void InitStartUp()
    {
        Master.Master.DialogText = " ";
        Master.Master.DropzoneEnabled = true;
        Master.Master.QuizMakerEnabled = true;
        Master.Master.FormsEnabled = true;
        Master.Master.JQueryNumericEnabled = true;
        Master.Master.MaskedInputEnabled = true;
        Master.Master.ATooltipEnabled = true;
        Master.Master.TimePickerEnabled = true;
        Master.Master.FancyBoxEnabled = true;

        QuizMakerControl1.iud = 1;        
        QuizMakerControl1.Root = Master.Master.Root;
        QuizMakerControl1.UserObject = Master.Master.UserObject;
        QuizMakerControl1.QuizID = long.Parse(Request.QueryString["qid"]);
        QuizMakerControl1.ShowCaption = false;
        QuizMakerControl1.OnDataBoud += (s, e) =>
        {            
            var Q = (Quiz)s;
            var r = Lib.Service.FindControl<Repeater>(Page, "AdditionalMenuItemsRepeater");
            Page.Title = string.Format(Resources.FormatPageTitle, Q.Caption);
            r.DataSource = new string[] { Q.Caption };
            r.DataBind();
        };
    }
}