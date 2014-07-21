using Core.Utilities;
using System;
using System.ComponentModel;
using System.Web.UI.HtmlControls;

public partial class UserControls_SuccessErrorControl : System.Web.UI.UserControl
{
    [Browsable(true)]
    public bool IsAdmin = true;
    
    [Browsable(true)]
    public bool ShowSuccess
    {
        set { SuccessPlaceHolder.Visible = value; }
        get { return SuccessPlaceHolder.Visible; }
    }

    [Browsable(true)]
    public bool ShowError
    {
        set { ErrorPlaceHolder.Visible = value; }
        get { return ErrorPlaceHolder.Visible; }
    }

    [Browsable(true)]
    public string SuccessMessage
    {
        set { SuccessMessageLiteral.Text = value; }
        get { return SuccessMessageLiteral.Text; }
    }

    [Browsable(true)]
    public string ErrorMessage
    {
        set { ErrorMessageLiteral.Text = value; }
        get { return ErrorMessageLiteral.Text; }
    }

    [Browsable(true)]
    public bool IsClientHidden { set; get; }

    [Browsable(true)]
    public bool IsUploader { set; get; }

    protected void Page_Init(object sender, EventArgs e)
    {
    }

    protected void Page_Load(object sender, EventArgs e)
    {
       
    }
}