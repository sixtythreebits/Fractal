using System;
using System.Text;
using Core.Properties;
using Lib;

namespace Fractal.Dashboard
{
    public partial class ChangePassword : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }        

        protected void ChangePasswordButton_Click(object sender, EventArgs e)
        {
            if (IsFormValid())
            {
                Master.Master.UserObject.TSP_Users(iud: 1,
                                                   ID: Master.Master.UserObject.ID,
                                                   Password: NewPasswordTextBox.Text);
                if (Master.Master.UserObject.IsError)
                {
                    SuccessErrorControl1.ShowError = true;
                    SuccessErrorControl1.Message = Resources.Abort;
                }
                else
                {
                    Master.Master.UserObject.SetAuthorizedCredentials("Password", NewPasswordTextBox.Text);
                    SuccessErrorControl1.ShowSuccess = true;
                    SuccessErrorControl1.Message = Resources.InformationPasswordUpdateSuccess;                    
                }
            }
        }

        bool IsFormValid()
        {
            var IsValid = true;
            var ScriptStr = new StringBuilder();

            if (Master.Master.UserObject.Password != CurrentPasswordTextBox.Text.MD5())
            {
                ScriptStr.AppendFormat(" SetInputFieldError('.validation-current-password', '{0}', 'CurrentPasswordError'); ", Resources.InvalidOldPassword);
            }

            if (string.IsNullOrEmpty(NewPasswordTextBox.Text))
            {
                ScriptStr.AppendFormat(" SetInputFieldError('.validation-password', '{0}', 'PasswordError'); ",Resources.RequiredPassword);
            }
            else if (NewPasswordTextBox.Text != RePasswordTextBox.Text)
            {
                ScriptStr.AppendFormat(" SetInputFieldError('.validation-repassword', '{0}', 'RePasswordError'); ", Resources.InvalidRePassword);
            }

            if (ScriptStr.Length > 0)
            {
                IsValid = false;
                ScriptsLiteral.Text = string.Format("<script> $(function(){{ {0} }}); </script>", ScriptStr.ToString());
            }

            return IsValid;
        }    
    }
}