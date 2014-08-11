using System;
using System.Text;
using Core;
using Core.Properties;

public partial class PopupPages_subscriptions : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            ExpirationDate.Date = DateTime.Now.AddMonths(1);
        }
    }

    protected void Callbacker_Callback(object source, DevExpress.Web.ASPxCallback.CallbackEventArgs e)
    {
        var SelectedUsers = UsersListBox.Items;
        var SelectedCourses = CoursesListBox.Items;
        int l1 = UsersListBox.Items.Count;
        int l2 = SelectedCourses.Count;

        if (l1 == 0 && l2 == 0)
        {
            throw new Exception(Resources.RequiredUsersAndCoursesSelection);
        }
        else if (l1 == 0)
        {
            throw new Exception(Resources.RequredUsersSelection);
        }
        else if (l2 == 0)
        {
            throw new Exception(Resources.RequiredCourseSelection);
        }
        else 
        {
            var sb = new StringBuilder();
            sb.Append("<data>");
            for (int i = 0; i < l1; i++)
            {
                for (int j = 0; j < l2; j++)
                {
                    sb.AppendFormat(@"<row>
                                       <user_id>{0}</user_id>
                                       <course_id>{1}</course_id>
                                       <subscription_expirationdate>{2}</subscription_expirationdate>
                                    </row>"
                                   , SelectedUsers[i].Value
                                   , SelectedCourses[j].Value
                                   , ExpirationDate.Value);
                }
            }
            sb.Append("</data>");

            //System.IO.File.WriteAllText(AppDomain.CurrentDomain.BaseDirectory + "file.xml", sb.ToString());
            var S = new Subscription();
            S.TX_Subscriptions(2, sb.ToString());
            if (S.IsError)
            {
                throw new Exception(S.IsClient ? S.ErrorMessage : Resources.Abort);
            }
        }        
    }
}