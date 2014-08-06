using System;
using System.Text;
using Lib;
using Core.Properties;
using Core.Utilities;
using Core.Services;
using System.Text.RegularExpressions;

namespace Fractal
{
    public partial class signup : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            InitStartUp();
        }

        void InitStartUp()
        {
            CheckSession();
        }

        void CheckSession()
        {
            if (Session["signup_success"] != null)
            {
                Session.Remove("signup_success");
                SuccessErrorControl1.ShowSuccess = true;
                SuccessErrorControl1.Message = Resources.InformationRegistrationSuccess;
                FinishRegistrationButton.Visible = false;
            }
        }

        protected void FinishRegistrationButton_Click(object sender, EventArgs e)
        {
            if (IsFormValid())
            {
                var x = string.Format(@"
            <data>
                <username><![CDATA[{0}]]></username>
                <email><![CDATA[{0}]]></email>
                <password><![CDATA[{1}]]></password>
                <fname><![CDATA[{2}]]></fname>
                <lname><![CDATA[{3}]]></lname>                
                <city_id>{4}</city_id>
            </data>"
                , EmailTextBox.Text.Trim()
                , PasswordTextBox.Text.Trim().MD5()
                , FnameTextBox.Text.Trim()
                , LnameTextBox.Text.Trim()
                , CitiesCombo.SelectedValue
                );

                Master.UserObject.TX_Users(0, x);
                if (Master.UserObject.IsError || Master.UserObject.Properties == null)
                {
                    SuccessErrorControl1.ShowError = true;
                    SuccessErrorControl1.Message = Resources.Abort;
                }
                else
                {
                    var UserID = Master.UserObject.Properties.LongValueOf("user_id");
                    Master.UserObject.GetSingleUser(UserID);

                    var Url = string.Format("{0}activation/{1}/", AppSettings.WebsiteHttpFullPath, (Master.UserObject.ID + "@#@" + Guid.NewGuid().ToString()).EncryptWeb());
                    var Subject = "პროფილის აქტივაცია";
                    var Keys = new string[] { "[subject]", "[body]", "[link]", "[button_text]" };
                    var Values = new string[] { Subject, Resources.EmailTextRegistration, Url, Subject };
                    var Body = Utility.GetTplContent(AppDomain.CurrentDomain.BaseDirectory + "TPL\\EmailTemplateWithButton.htm", Keys, Values);
                    //System.IO.File.WriteAllText(AppDomain.CurrentDomain.BaseDirectory + "App_Data\\ErrorLog.txt", Body);                
                    var M = new Mail();
                    M.Send(Master.UserObject.Email, Subject, Body);

                    Session["signup_success"] = true;
                    Response.Redirect("/signup");                    
                }
            }
        }

        bool IsFormValid()
        {
            var IsValid = true;
            var ScriptStr = new StringBuilder();

            if (string.IsNullOrEmpty(EmailTextBox.Text))
            {
                ScriptStr.AppendFormat(" SetInputFieldError('.validation-email', '{0}', 'EmailError'); ", Resources.RequiredEmail);
            }
            else if (!new Regex(Resources.RegexEmail).IsMatch(EmailTextBox.Text))
            {
                ScriptStr.AppendFormat(" SetInputFieldError('.validation-email', '{0}', 'EmailError'); ", Resources.InvalidEmailFormat);
            }
            else if (!Master.UserObject.IsEmailUniq(EmailTextBox.Text))
            {
                ScriptStr.AppendFormat(" SetInputFieldError('.validation-email', '{0}', 'EmailError'); ", Resources.UniqEmail);
            }

            if (string.IsNullOrEmpty(FnameTextBox.Text))
            {
                ScriptStr.AppendFormat(" SetInputFieldError('.validation-fname', '{0}', 'FnameError'); ", Resources.RequiredFirstName);
            }

            if (string.IsNullOrEmpty(LnameTextBox.Text))
            {
                ScriptStr.AppendFormat(" SetInputFieldError('.validation-lname', '{0}', 'LnameError'); ", Resources.RequiredLastName);
            }

            if (string.IsNullOrEmpty(PasswordTextBox.Text))
            {
                ScriptStr.AppendFormat(" SetInputFieldError('.validation-password', '{0}', 'PasswordError'); ", Resources.RequiredPassword);
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