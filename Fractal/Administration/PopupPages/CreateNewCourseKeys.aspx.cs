using System;
using System.Text;
using Core;
using Core.Properties;
using Lib;

public partial class PopupPages_Administration_CM_CreateNewCourseKeys : System.Web.UI.Page
{
    long? GroupID;
    long? CourseID;
    string TypeID = null;
    

    protected void Page_Load(object sender, EventArgs e)
    {
        InitStartUp();       
    }

    void InitStartUp()
    {
        Master.FormsEnabled = true;        
        Master.JQueryNumericEnabled = true;

        GroupID = Request.QueryString["id"].ToLong();
        CourseID = Request.QueryString["cid"].ToLong();
        if (CourseID > 0)
        {
            if (!IsPostBack)
            {
                if (GroupID == null)
                {
                    MonthsTextBox.Text = "1";
                    KeyCountTextBox.Text = "1";
                }
                else
                {
                    var G = new CourseKeyGroup(GroupID);
                    if (G.IsError)
                    {
                        SaveButton.Visible = false;
                    }
                    else
                    {
                        CaptionTextBox.Text = G.Caption;
                        MonthsTextBox.Text = G.Months.ToString();
                        KeyCountPlaceHolder.Visible = false;
                        
                    }
                }
            }
        }
        else
        {
            Response.Redirect("~/");
        }
    }

    protected void SaveButton_Click(object sender, EventArgs e)
    {
        bool IsError = false;
        if (GroupID == null)
        {
            var sb = new StringBuilder();
            sb.AppendFormat(@"
            <data>
                <course>{0}</course>
                <group><![CDATA[{1}]]></group>
                <months>{2}</months>
                <count>{3}</count>
            </data>"
            , CourseID
            , CaptionTextBox.Text
            , MonthsTextBox.Text
            , KeyCountTextBox.Text);
            var CK = new CourseKey();
            CK.TX_CourseKeys(0, sb.ToString());
            IsError = CK.IsError;
        }
        else
        {
            var CKG = new CourseKeyGroup();
            CKG.TSP_CourseKeysGroup(
                iud: 1,
                ID: GroupID,
                CourseID: CourseID,
                Caption: CaptionTextBox.Text,                
                MonthLifeTime: byte.Parse(MonthsTextBox.Text)
            );
            IsError = CKG.IsError;
        }

        if (IsError)
        {
            HeadLiteral.Text = "<script> $(document).ready(function(){ alert(Abort); }); </script>";                
        }
        else
        {
            HeadLiteral.Text = "<script> $(document).ready(function(){ window.parent.KeyGroupsGrid.Refresh(); ClosePopup(true); }); </script>";
        }
    }    
}