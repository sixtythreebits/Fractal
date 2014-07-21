using Core.Properties;
using System;
using System.Text;
using System.Web.UI.WebControls;

public partial class administration_CM_course_subscriptions : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        InitStartUp();
    }

    void InitStartUp()
    {
        Master.Master.JQueryUIEnabled = true;
        Master.Master.FormsEnabled = true;

        SubscriptionsGrid.Settings.ShowFilterRow = false;
        SubscriptionsGrid.Settings.ShowFilterRowMenu = false;
        SubscriptionsGrid.SettingsBehavior.ColumnResizeMode = DevExpress.Web.ASPxClasses.ColumnResizeMode.Disabled;        
        SubscriptionsGrid.SettingsText.EmptyDataRow = Resources.InfoNoSubscriptions;
        if (!IsPostBack)
        {
            SubscriptionsGrid.SettingsPager.PageSize = 10;
        }
    }

    protected void SubscriptionTypesCombo_DataBound(object sender, EventArgs e)
    {
        SubscriptionTypesCombo.Items.Insert(0, new ListItem("All", ""));
    }

    protected void SubscriptionsGrid_BeforePerformDataSelect(object sender, EventArgs e)
    {
        var sb = new StringBuilder();
        sb.Append("<data>");

        sb.AppendFormat("<course_id>{0}</course_id>", Master.CourseObject.ID);

        if (!string.IsNullOrEmpty(D1TextBox.Text))
        {
            sb.AppendFormat("<crtime_d1>{0}</crtime_d1>", D1TextBox.Text);
        }
        if (!string.IsNullOrEmpty(D2TextBox.Text))
        {
            sb.AppendFormat("<crtime_d2>{0}</crtime_d2>", D2TextBox.Text);
        }
        if (!string.IsNullOrEmpty(ExpD1TextBox.Text))
        {
            sb.AppendFormat("<d1>{0}</d1>", ExpD1TextBox.Text);
        }
        if (!string.IsNullOrEmpty(ExpD2TextBox.Text))
        {
            sb.AppendFormat("<d2>{0}</d2>", ExpD2TextBox.Text);
        }
        if (!string.IsNullOrEmpty(SearchTextBox.Text))
        {
            sb.AppendFormat("<user>{0}</user>", SearchTextBox.Text);
        }
        if (!string.IsNullOrEmpty(SubscriptionTypesCombo.SelectedValue))
        {
            sb.AppendFormat("<type_id>{0}</type_id>", SubscriptionTypesCombo.SelectedValue);
        }
        sb.Append("</data>");
        SubscriptionsDataSource.SelectParameters["filter"].DefaultValue = sb.ToString();        
    }

    #region Helper Methods
    public string GetSubscriptionHtml(string SubscriptionTypeCode, decimal? Price)
    {
        switch (SubscriptionTypeCode)
        {
            case "1":
                {
                    return string.Format("<b>${0}</b><span>payed</span>", Math.Round(Price.Value,2));
                }
            case "2":
                {
                    return "<b>Key Activation</b><span>Subscription</span>";
                }
            default:
                {
                    return "<b>Manual</b><span>Subscription</span>";
                }
        }
    }
    #endregion Helper Methods                
}