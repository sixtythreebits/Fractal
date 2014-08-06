using System;
using System.Xml.Linq;
using Core.Properties;
using Lib;
using System.Text;

namespace Fractal
{
    public partial class PasswordReset : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            CheckID();
        }

        void CheckID()
        {
            try
            {
                var x = XElement.Parse(Request.QueryString["q"].DecryptWeb());
                if (x.DateTimeValueOf("date") > DateTime.Now)
                {
                    Master.UserObject.GetSingleUser(x.LongValueOf("user_id"));
                    if (Master.UserObject.ID == null)
                    {
                        Response.Redirect("~/");
                    }
                }
                else
                {
                    Response.Redirect("~/");
                }
            }
            catch
            {
                Response.Redirect("~/");
            }
        }

        protected void FinishResetPasswordButton_Click(object sender, EventArgs e)
        {
            if (IsFormValid())
            {
                Master.UserObject.TSP_Users(iud: 1,
                                            ID: Master.UserObject.ID,
                                            Password: PasswordTextBox.Text
                                            );
                if (Master.UserObject.IsError)
                {
                    ScriptsLiteral.Text = string.Format("<script>$(function() {{ alert('{0}'); }});</script>", Resources.Abort);
                }
                else
                {
                    var x = XElement.Parse(Request.QueryString["q"].DecryptWeb());
                    if(Master.UserObject.Authenticate(x.ValueOf("email"),PasswordTextBox.Text.MD5()))
                    {
                        Session["password_reset_success"] = true;
                        Response.Redirect("~/dashboard/profile/");
                    }
                    else
                    {
                        SuccessErrorControl1.ShowError=true;
                        SuccessErrorControl1.Message=Resources.Abort;
                    }
                }
            }
        }

        bool IsFormValid()
        {
            var IsValid = true;
            var ScriptStr = new StringBuilder();

            if (string.IsNullOrEmpty(PasswordTextBox.Text))
            {
                ScriptStr.AppendFormat(" SetInputFieldError('.validation-password', '{0}', 'PasswordError'); ", Resources.RequiredPassword);
            }
            else if (PasswordTextBox.Text != RePasswordTextBox.Text)
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