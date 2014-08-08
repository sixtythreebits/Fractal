using System;
using DevExpress.Web.ASPxGridView;
using Core;
using Core.Properties;

public partial class administration_general_subscriptions : System.Web.UI.Page
{
    public string FormatDateTime = Resources.FormatDateTime;
    protected void Page_Load(object sender, EventArgs e)
    {
        InitStartUp();
    }

    void InitStartUp()
    {
        Master.FancyBoxEnabled = true;
        ExporterControl1.GridID = "SubscriptionsGrid";

        var CRTimeField = (GridViewDataDateColumn)SubscriptionsGrid.Columns["CRTime"];
        var ExpDateField = (GridViewDataDateColumn)SubscriptionsGrid.Columns["ExpDate"];

        CRTimeField.PropertiesDateEdit.DisplayFormatString = Resources.FormatDateTime;

        ExpDateField.PropertiesDateEdit.DisplayFormatString = Resources.FormatDateTime;
        ExpDateField.PropertiesDateEdit.EditFormatString = Resources.FormatDate;

        SubscriptionsGrid.SettingsBehavior.AllowDragDrop = true;
        SubscriptionsGrid.SettingsText.ConfirmDelete = Resources.ConfirmDeleteUserCourseSubscription;
    }

    protected void SubscriptionsGrid_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
    {
        SubscriptionsDataSource.DeleteParameters["xml"].DefaultValue = string.Format(@"
        <data>
            <user_id>{0}</user_id>
            <course_id>{1}</course_id>
        </data>
        ", e.Values["UserID"], e.Values["CourseID"]);
        
        e.Values.Remove("ID");        
    }

    protected void SubscriptionsDataSource_Deleting(object sender, System.Web.UI.WebControls.ObjectDataSourceMethodEventArgs e)
    {
        e.InputParameters.RemoveAt(2);
    }

    protected void SubscriptionDetailsGrid_BeforePerformDataSelect(object sender, EventArgs e)
    {
        var SubscriptionDetailsGrid = (ASPxGridView)sender;
        var values = (object[])SubscriptionDetailsGrid.GetMasterRowFieldValues("UserID", "CourseID");
        SubscriptionHistoryDataSource.SelectParameters["UserID"].DefaultValue = values[0].ToString();
        SubscriptionHistoryDataSource.SelectParameters["CourseID"].DefaultValue = values[1].ToString();

        SubscriptionDetailsGrid.SettingsText.ConfirmDelete = Resources.ConfirmDeleteSubscriptionRecord;

        var ExpDateField = ((GridViewDataDateColumn)SubscriptionDetailsGrid.Columns["ExpDate"]);
        ExpDateField.PropertiesDateEdit.DisplayFormatString = Resources.FormatDateTime;
        ExpDateField.PropertiesDateEdit.EditFormatString = Resources.FormatDate;
    }    
}