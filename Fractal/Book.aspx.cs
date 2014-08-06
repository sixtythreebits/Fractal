using Core;
using System;


namespace Fractal
{
    public partial class Book : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        void InitStartUp()
        {
            var C = new Course();
            //C.GetSingleCourse(slug: Request.QueryString["id"]);
        }
    }
}