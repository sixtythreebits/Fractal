using System;
using Lib;
using Core;

namespace Fractal
{
    public partial class Activation : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            InitStartUp();
        }

        void InitStartUp()
        {
            try
            {
                var UserID = Request.QueryString["q"].DecryptWeb().SplitByString("@#@")[0].ToInt();
                var U = new User();
                U.GetSingleUser(UserID);
                if (!U.IsError)
                {
                    U.TSP_Users(iud: 1, ID: U.ID, IsActive:true);
                    U.Authenticate(U.Username, U.Password);
                    Session["activation_success"] = true;                    
                }
            }
            catch(Exception ex)
            {
                string.Format("activation.aspx - {0}", ex.Message).LogString();
                Response.Redirect("~/");
            }
            Response.Redirect("~/dashboard/profile/");
        }
    }
}