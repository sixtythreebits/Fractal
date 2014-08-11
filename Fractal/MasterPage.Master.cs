using System;
using Core;

namespace Fractal
{
    public partial class MasterPage : WebsiteMasterBase
    {
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

        protected void Page_Init(object sender, EventArgs e)
        {
            UserObject = new User();
            UserObject.GetAuthorizedCredentials();

            form1.Action = "/" + (Request.ApplicationPath.Length > 1 ? Request.RawUrl.Remove(0, 1) : Request.RawUrl.Remove(0, 1));
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            HeadLiteral.Text += HeaderInclude.ToString();
        }
    }
}