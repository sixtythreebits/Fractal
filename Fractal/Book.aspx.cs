using System;
using System.Linq;
using System.Text;
using Core;
using Core.Properties;
using Core.Utilities;
using System.Globalization;

namespace Fractal
{
    public partial class Book : System.Web.UI.Page
    {
        public Course C = new Course();
        protected void Page_Load(object sender, EventArgs e)
        {
            InitStartUp();
            InitSubscriptions();
        }

        void InitStartUp()
        {
            C = new Course(Request.QueryString["id"]);
            CourseCaptionLiteral.Text = C.Caption;
            DescriptionLiteral.Text = C.Description;
            BookImage.ImageUrl = string.IsNullOrWhiteSpace(C.Icon) ? AppSettings.NoCourseIconHttpPath : AppSettings.UploadFolderHttpPath + C.Icon;
        }

        void InitSubscriptions()
        {
            if (Security.IsUserAllowedToViewCourse(Master.UserObject.ID, C.ID))
            {
                SubscribePlaceHolder.Visible = false;
                QuizzesPlaceHolder.Visible = true;
                SectionLiteral.Text = "გამოცდები";                

                var Q = new Quiz();
                QuizzesRepeater.DataSource = Q.ListCourseQuizzesWithUserResults(C.ID, Master.UserObject.ID).Select(c => new
                {
                    ID = c.ID,
                    Caption = c.Caption,
                    MaxScore = c.MaxScore,
                    StudentScore = c.StudentScore > 0 ? c.StudentScore.ToString() : "-",
                    Date = c.CRTime.HasValue ? c.CRTime.Value.ToString(Resources.FormatDateTime, new CultureInfo("ka-ge")) : "-"
                }).ToList();
                QuizzesRepeater.DataBind();
            }
            else
            {
                SubscribePlaceHolder.Visible = true;
                QuizzesPlaceHolder.Visible = false;
                SectionLiteral.Text = "წიგნის აქტივაცია";
            }
        }

        protected void ActivateButton_Click(object sender, EventArgs e)
        {            
            var sb = new StringBuilder()
            .Append("<data>")
            .AppendFormat("<user_id>{0}</user_id>", Master.UserObject.ID)
            .AppendFormat("<course_id>{0}</course_id>", C.ID)                        
            .AppendFormat("<key><![CDATA[{0}]]></key>", KeyTextBox.Text.TrimStart().TrimEnd())
            .Append("</data>");

            var S = new Subscription();
            System.IO.File.WriteAllText(AppDomain.CurrentDomain.BaseDirectory + "file.xml", sb.ToString());
            S.TX_Subscriptions(0, sb.ToString());

            if (S.IsError)
            {
                
                ErrorPlaceHolder.Visible = true;
                if (S.IsClient)
                {
                    ErrorMessageLiteral.Text = S.ErrorMessage;
                }
                else
                {
                    ErrorMessageLiteral.Text = Resources.Abort;
                }
            }
            else
            {
                Response.Redirect(string.Format("/book/{0}/", C.Slug));
            }
        }
    }
}