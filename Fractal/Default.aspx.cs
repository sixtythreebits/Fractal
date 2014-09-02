using Core;
using Core.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Fractal
{
    public partial class Default : System.Web.UI.Page
    {
        public string AlgebraImage = "/images/0/book-home.png";
        public string GeometryImage = "/images/0/book-home.png";

        protected void Page_Load(object sender, EventArgs e)
        {
            InitCourses();
        }

        void InitCourses()
        {
            var C1 = new Course("algebra");
            var C2 = new Course("geometry");

            AlgebraImage = string.IsNullOrWhiteSpace(C1.Icon) ? AlgebraImage : AppSettings.UploadFolderHttpPath + C1.Icon;
            GeometryImage = string.IsNullOrWhiteSpace(C2.Icon) ? GeometryImage : AppSettings.UploadFolderHttpPath + C2.Icon;
            
        }
    }
}