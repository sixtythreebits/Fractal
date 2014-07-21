using System;
using System.Web;
using Core;

public partial class administration_PopupPages_MasterPage : AdminMasterBase
{
    #region Properties
    public string guid { set; get; }

    public string DialogText
    {
        set
        {
            DialogPlaceHolder.Visible = true;
            DialogTextLiteral.Text = value;
            JQueryUIEnabled = true;
            HeaderInclude.AppendFormat("<script type='text/javascript' src='/plugins/jquery-ui/js/dialog.js'></script>{0}", Environment.NewLine);

        }
        get { return DialogTextLiteral.Text; }
    }
    #endregion Properties

    protected void Page_Init(object sender, EventArgs e)
    {
        SetNoCache();
        InitStartUp();
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        HeadLiteral.Text = HeaderInclude.ToString();
    }

    void InitStartUp()
    {                
        if (UserObject.IsAuthorized)
        {
            guid = Guid.NewGuid().ToString();
            PageTitleLiteral.Text = Request.QueryString["title"];
        }
        else
        {
            Response.Redirect("~/");
        }
    }

    
    void SetNoCache()
    {
        Response.Cache.SetCacheability(HttpCacheability.NoCache);
        Response.Cache.SetExpires(DateTime.Now.AddHours(-1));
        Response.Cache.SetNoStore();
        Response.AppendHeader("Pragma", "no-cache");
    }
}
