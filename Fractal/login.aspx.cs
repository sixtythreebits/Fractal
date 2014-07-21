using System;
using Core;
using Lib;

namespace Fractal
{
    public partial class login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void LoginButton_Click(object sender, EventArgs e)
        {
            var U = new User();
            if (U.Authenticate(UsernameTextBox.Text, PasswordTextBox.Text.MD5()))
            {
                Response.Redirect("~/administration/default.aspx");
            }
            else
            {
                ErrorLabel.Visible = true;
            }
        }
    }
}