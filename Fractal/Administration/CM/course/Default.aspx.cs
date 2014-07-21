using System;

public partial class administration_CM_course_Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Master.InitCourseItems();
    }
}