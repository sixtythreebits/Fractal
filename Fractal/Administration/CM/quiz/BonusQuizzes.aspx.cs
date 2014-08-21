using System;
using System.Linq;
using System.Collections.Generic;
using Core;

namespace Fractal.Administration.CM.quiz
{
    public partial class BonusQuizzes : System.Web.UI.Page
    {
        public long? ID { set; get; }
        protected void Page_Load(object sender, EventArgs e)
        {
            BonusQuizzesDataSource.SelectParameters["UserID"].DefaultValue =
            QuizzesDataSource.SelectParameters["UserID"].DefaultValue = Master.UserObject.ID.ToString();
        }
    }
}