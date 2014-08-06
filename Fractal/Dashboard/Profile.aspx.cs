using System;
using Core;
using Core.Properties;
using System.Text;
using Lib;

namespace Fractal.Dashboard
{
    public partial class Profile : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            CheckSession();
            InitStartUp();
        }

        void InitStartUp()
        {
            Master.Master.UserObject.GetSingleUser(Master.Master.UserObject.ID);
            var U = Master.Master.UserObject;
            EmailTextBox.Text = U.Email;
            if (!IsPostBack)
            {
                FnameTextBox.Text = U.Fname;
                LnameTextBox.Text = U.Lname;
                CitiesCombo.DataBound += (object sender, EventArgs e) =>
                {
                    var item = CitiesCombo.Items.FindByValue(U.CityID.ToString());
                    if (item != null)
                    {
                        item.Selected = true;
                    }
                };
            }
        }

        void CheckSession()
        {
            if (Session["activation_success"] != null)
            {
                Session.Remove("activation_success");
                SuccessErrorControl1.ShowSuccess = true;
                SuccessErrorControl1.Message = Resources.InformationProfileActivationSuccess;
            }
            else if (Session["password_reset_success"] != null)                
            {
                Session.Remove("password_reset_success");
                SuccessErrorControl1.ShowSuccess = true;
                SuccessErrorControl1.Message = Resources.InformationPasswordUpdateSuccess;
            }
        }

        protected void SaveButton_Click(object sender, EventArgs e)
        {
            if (IsFormValid())
            {
                var x = string.Format(@"
                <data>                
                    <id>{0}</id>
                    <fname><![CDATA[{1}]]></fname>
                    <lname><![CDATA[{2}]]></lname>                
                    <city_id>{3}</city_id>
                </data>"
               , Master.Master.UserObject.ID
               , FnameTextBox.Text.Trim()
               , LnameTextBox.Text.Trim()
               , CitiesCombo.SelectedValue
               );
                //x.LogString();
                Master.Master.UserObject.TX_Users(1, x);
                if (Master.Master.UserObject.IsError)
                {
                    SuccessErrorControl1.ShowError = true;
                    SuccessErrorControl1.Message = Resources.Abort;
                }
                else
                {
                    SuccessErrorControl1.ShowSuccess = true;
                    SuccessErrorControl1.Message = Resources.SuccessInformationSave;
                }
            }
        }

        bool IsFormValid()
        {
            var IsValid = true;
            var ScriptStr = new StringBuilder();

            if (string.IsNullOrEmpty(FnameTextBox.Text))
            {
                ScriptStr.AppendFormat(" SetInputFieldError('.validation-fname', '{0}', 'FnameError'); ", Resources.RequiredFirstName);
            }

            if (string.IsNullOrEmpty(LnameTextBox.Text))
            {
                ScriptStr.AppendFormat(" SetInputFieldError('.validation-lname', '{0}', 'LnameError'); ", Resources.RequiredLastName);
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