using System;

namespace StreamingTutorsWebProject.UserControls
{

    public partial class SuccessErrorControl : System.Web.UI.UserControl
    {
        #region Properties
        bool _ShowSuccess;
        public bool ShowSuccess
        {
            set
            {
                _ShowSuccess = value;
                if (_ShowSuccess)
                {
                    SuccesErrorPanel.CssClass += "succes-error opened";
                }
            }
            get { return _ShowSuccess; }
        }

        bool _ShowError;
        public bool ShowError
        {
            set
            {
                _ShowError = value;
                if (_ShowError)
                {
                    SuccesErrorPanel.CssClass += "succes-error error opened";
                }
            }
        }
        public string Message
        {
            set
            {
                MessageLiteral.Text = value;
            }
            get
            {
                return MessageLiteral.Text;
            }
        }
        #endregion Properties

        protected void Page_Load(object sender, EventArgs e)
        {

        }
    }

}