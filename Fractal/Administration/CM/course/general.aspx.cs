using System;
using System.Text;
using Lib;
using Core;
using System.IO;
using File = System.IO.File;
using Res = Core.Properties.Resources;
using Core.Utilities;

public partial class administration_CM_course_general : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        var action = Request.Form["action"];
        if (string.IsNullOrWhiteSpace(action))
        {
            InitStartUp();
        }
        else
        {
            ExecAjax(action);
        }
    }

    void InitStartUp()
    {
        Master.Master.JQueryNumericEnabled = true;
        Master.Master.FormsEnabled = true;        
        Master.Master.SuccessErrorMessageEnabled = true;
        Master.Master.TinyMceEnabled = true;
        Master.Master.JQueryUIEnabled = true;

        if (Session["Course_General_Success"] != null)
        {
            Session.Remove("Course_General_Success");
            SuccessErrorControl1.ShowSuccess = true;
            SuccessErrorControl1.SuccessMessage = Res.SuccessInformationSave;            
        }

        SetStartUpValues();
    }

    void SetStartUpValues()
    {
        if (!IsPostBack)
        {
            CaptionTextBox.Text = Master.CourseObject.Caption;
            SlugTextBox.Text = Master.CourseObject.Slug;
            HFIsPublished.Value = Master.CourseObject.IsPublished.ToString().ToLower();
            DescriptionTextBox.Text = Master.CourseObject.Description;
        }
        if (!string.IsNullOrEmpty(Master.CourseObject.Icon))
        {
            ClearImageButton.Visible = true;
            ImageLiteral.Text = string.Format("<a><img src=\"{0}{1}\" alt=\"course image\"/></a>", AppSettings.UploadFolderHttpPath, Master.CourseObject.Icon);
        }
    }

    protected void SaveButton_Click(object sender, EventArgs e)
    {
        if (IsFormValid())
        {
            var Icon = Uploader.HasFile ? Uploader.FileName.ToAZ09Dash(true) : null;

            var sb = new StringBuilder()
            .Append("<course>")
            .AppendFormat("<id>{0}</id>", Master.CourseObject.ID);
            if (!string.IsNullOrWhiteSpace(SlugTextBox.Text))
            {
                sb.AppendFormat("<slug><![CDATA[{0}]]></slug>", SlugTextBox.Text);

            }
            sb.AppendFormat("<caption><![CDATA[{0}]]></caption>", CaptionTextBox.Text)
            .AppendFormat("<description><![CDATA[{0}]]></description>", DescriptionTextBox.Text);
            if (!string.IsNullOrEmpty(Icon))
            {
                sb.AppendFormat("<icon>{0}</icon>", Icon);
            }
            
            sb.AppendFormat("<published>{0}</published>", HFIsPublished.Value)            
            .Append("</course>");

            //File.WriteAllText(AppDomain.CurrentDomain.BaseDirectory + "file.xml", sb.ToString());
            Master.CourseObject.TX_Courses(5, sb.ToString());

            if (Master.CourseObject.IsError)
            {
                SuccessErrorControl1.ShowError = true;
                SuccessErrorControl1.ErrorMessage = Res.Abort;
            }
            else
            {
                if (Uploader.HasFile)
                {
                    if (!string.IsNullOrEmpty(Master.CourseObject.Icon))
                    {
                        File.Delete(AppSettings.UploadFolderPhysicalPath + Master.CourseObject.Icon);
                    }
                    Uploader.SaveAs(AppSettings.UploadFolderPhysicalPath + Icon);
                }
               
                Session["Course_General_Success"] = true;
                Response.Redirect(Request.Url.ToString());
            }
        }
    }

    bool IsFormValid()
    {
        bool IsValid = true;
        var ScriptStr = new StringBuilder();

        if (string.IsNullOrEmpty(CaptionTextBox.Text))
        {
            ScriptStr.Append(" SetInputFieldError(\"course-name\", RequiredName, \"NameError\"); \n");
        }

        if(!string.IsNullOrWhiteSpace(SlugTextBox.Text) && !IsSlugUniq(SlugTextBox.Text))
        {
            ScriptStr.Append(" SetInputFieldError(\"slug\", InvalidSlug, \"SlugError\"); \n");
        }

        if (Uploader.HasFile)
        {
            if (!Utility.GetImageExtensions().Contains(Path.GetExtension(Uploader.FileName).ToLower()))
            {
                ScriptStr.Append(" SetInputFieldError(\"course-icon\", WrongFileType, \"IconError\"); \n");
            }
        }
        
        if (ScriptStr.Length > 0)
        {
            IsValid = false;
            HeadLiteral.Text = string.Format("<script> $(document).ready(function(){{ {0} \n}}); </script>", ScriptStr.ToString());
        }

        return IsValid;
    }

    protected void ClearImageButton_Click(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(Master.CourseObject.Icon))
        {
            Master.CourseObject.TSP_Courses(iud: 1, ID: Master.CourseObject.ID, Icon: string.Empty);
            if (Master.CourseObject.IsError)
            {
                SuccessErrorControl1.ShowError = true;
                SuccessErrorControl1.ErrorMessage = Res.Abort;
            }
            else
            {
                System.IO.File.Delete(AppSettings.UploadFolderPhysicalPath + Master.CourseObject.Icon);
                Session["Course_General_Success"] = true;
                Response.Redirect(Request.Url.ToString());
            }
        }
    }

    protected void DeletePromoImageButton_Click(object sender, EventArgs e)
    {
        if (!string.IsNullOrWhiteSpace(Master.CourseObject.PromoImage))
        {
            var promoImagePath = AppSettings.UploadFolderPhysicalPath + Master.CourseObject.PromoImage;
            if (File.Exists(promoImagePath))
            {
                File.Delete(promoImagePath);
            }

            Master.CourseObject.TSP_Courses(1, Master.CourseObject.ID, PromoImage: string.Empty);

            Session["Course_General_Success"] = true;
            Response.Redirect(Request.Url.ToString());
        }
    }

    #region AJAX
    void ExecAjax(string Action)
    {
        Response.Clear();
        try
        {
            switch (Action)
            {
                case "validate_slug":
                    {
                        IsSlugUniq(Request.Form["slug"]);
                        break;
                    }
            }
        }
        catch (Exception ex)
        {
            (ex.Message).LogString();
        }

        Response.End();
    }

    bool IsSlugUniq(string slug,bool WriteToResponse = true)
    {
        var C = new Course();
        var IsUniq = C.IsCourseSlugUniq(slug, Master.CourseObject.ID);
        
        if(IsUniq && WriteToResponse)
        {
            Response.Write("success");
        }
        return IsUniq;
    }
    #endregion AJAX
}