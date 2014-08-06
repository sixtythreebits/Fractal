using System;
using Core;

namespace Fractal
{
    public partial class MasterPage : System.Web.UI.MasterPage
    {
        public User UserObject { set; get; }
        protected void Page_Init(object sender, EventArgs e)
        {
            UserObject = new User();
            UserObject.GetAuthorizedCredentials();

            form1.Action = "/" + (Request.ApplicationPath.Length > 1 ? Request.RawUrl.Remove(0, 1) : Request.RawUrl.Remove(0, 1));
        }

        protected void Page_Load(object sender, EventArgs e)
        {

        }
    }
}