using System;
using System.Configuration;

namespace Core
{
    /// <summary>
    /// Gets plugin javascript and css files
    /// </summary>
    public class Plugins
    {
        #region Methods
        public static string GetAdminForms()
        {
            return string.Format("<link type=\"text/css\" href=\"/plugins/FormAdmin/forms.css\" rel=\"stylesheet\" />{0}<script type=\"text/javascript\" src=\"/plugins/FormAdmin/input.js\"></script>{0}", Environment.NewLine);
        }

        public static string GetAdminFormsCssOnly()
        {
            return string.Format("<link type=\"text/css\" href=\"/plugins/FormAdmin/forms.css\" rel=\"stylesheet\" />{0}", Environment.NewLine);
        }

        public static string GetAdminInputExtension()
        {
            return string.Format("<script type='text/javascript' src='/scripts/input.js'></script>{0}", Environment.NewLine);
        }

        public static string GetAdminValidation()
        {
            return string.Format("<script type='text/javascript' src='/scripts/validation.js'></script>{0}", Environment.NewLine);
        }

        public static string GetAToolTip()
        {
            return string.Format("<link type=\"text/css\" href=\"/plugins/atooltip/atooltip.css\" rel=\"stylesheet\" />{0}<script type=\"text/javascript\" src=\"/plugins/atooltip/jquery.atooltip.min.js\"></script>{0}", Environment.NewLine);
        }

        public static string GetClock()
        {
            return string.Format("<link type=\"text/css\" href=\"/plugins/clock/css/analog.css\" rel=\"stylesheet\" />{0}<script type=\"text/javascript\" src=\"/plugins/clock/js/jquery.clock.js\"></script>{0}", Environment.NewLine);
        }

        public static string GetDateFormatter()
        {
            return string.Format("<script type=\"text/javascript\" src=\"/plugins/DateFormat/date.format.js\"></script>{0}", Environment.NewLine);
        }

        public static string GetDropzone()
        {
            return string.Format("<link href=\"/plugins/dropzone/dropzone.css\" rel=\"stylesheet\" />{0}<script type='text/javascript' src='/plugins/dropzone/dropzone.min.js'></script>{0}", Environment.NewLine);
        }

        public static string GetFancyBoxV2()
        {
            return string.Format("<link type=\"text/css\" href=\"/plugins/fancybox2.0/jquery.fancybox.css\" rel=\"stylesheet\" />{0}<script type=\"text/javascript\" src=\"/plugins/fancybox2.0/jquery.fancybox.js\"></script>{0}<script type=\"text/javascript\" src=\"/plugins/fancybox2.0/FancyMethods.js\"></script>{0}", Environment.NewLine);
        }

        public static string GetForms()
        {
            return string.Format("<link type=\"text/css\" href=\"/plugins/form/form.css\" rel=\"stylesheet\" />{0}<script type=\"text/javascript\" src=\"/plugins/form/form.js\"></script>{0}", Environment.NewLine);
        }

        public static string GetGMLoader()
        {
            return string.Format("<link type='text/css' href='/plugins/gm-loader/gm-loader.css' rel='stylesheet' />{0}<script type='text/javascript' src='/plugins/gm-loader/gm-loader.js'></script>{0}", Environment.NewLine);
        }

        public static string GetJQuery()
        {
            return ConfigurationManager.AppSettings["CDNJQuery"] == "true" ?
                   string.Format("<script type='text/javascript' src='//code.jquery.com/jquery-1.10.2.min.js'></script>{0}", Environment.NewLine) :
                   string.Format("<script type='text/javascript' src='/plugins/jquery/jquery-1.10.2.min.js'></script>{0}", Environment.NewLine);
        }

        public static string GetJQueryContextMenu()
        {
            return string.Format("<link type='text/css' href='/plugins/JqueryContextMenu/jquery.contextMenu.css' rel='stylesheet' />{0}<script type='text/javascript' src='/plugins/JqueryContextMenu/jquery.contextMenu.js'></script>{0}", Environment.NewLine);
        }

        public static string GetJQueryCyrcles()
        {
            return string.Format("<script type='text/javascript' src='/plugins/JqueryCyrcles/jquery.cycle2.min.js'></script>{0}", Environment.NewLine);
        }

        public static string GetJQueryNumeric()
        {
            return string.Format("<script type='text/javascript' src='/plugins/JqueryNumeric/numericInput.min.js'></script>{0}", Environment.NewLine);
        }

        public static string GetJQueryUI()
        {
            return ConfigurationManager.AppSettings["CDNJQueryUI"] == "true" ?
                   //string.Format("<link type='text/css' href='//code.jquery.com/ui/1.10.3/themes/blitzer/jquery-ui.min.css' rel='stylesheet' />{0}<link type='text/css' href='/plugins/jquery-ui/css/jquery-ui-system.css' rel='stylesheet' />{0}<script type='text/javascript' src='//code.jquery.com/ui/1.10.3/jquery-ui.min.js'></script>{0}", Environment.NewLine) :
                   //string.Format("<link type='text/css' href='/plugins/jquery-ui/css/jquery-ui-1.10.3.custom.min.css' rel='stylesheet' />{0}<link type='text/css' href='/plugins/jquery-ui/css/jquery-ui-system.css' rel='stylesheet' />{0}<script type='text/javascript' src='/plugins/jquery-ui/js/jquery-ui-1.10.3.custom.min.js'></script>{0}", Environment.NewLine);
                   string.Format("<link type='text/css' href='//code.jquery.com/ui/1.11.1/themes/south-street/jquery-ui.min.css' rel='stylesheet' />{0}<link type='text/css' href='/plugins/jquery-ui/css/jquery-ui-system.css' rel='stylesheet' />{0}<script type='text/javascript' src='//code.jquery.com/ui/1.11.1/jquery-ui.min.js'></script>{0}", Environment.NewLine) :
                   string.Format("<link type='text/css' href='/plugins/jquery-ui/css/jquery-ui.min.css' rel='stylesheet' />{0}<link type='text/css' href='/plugins/jquery-ui/css/jquery-ui-system.css' rel='stylesheet' />{0}<script type='text/javascript' src='/plugins/jquery-ui/js/jquery-ui.min.js'></script>{0}", Environment.NewLine);
                   
        }

        public static string GetJQueryUITimePicker()
        {
            return string.Format("<link type='text/css' href='/plugins/jquery-ui-timepicker/jquery-ui-timepicker-addon.css' rel='stylesheet' />{0}<script type='text/javascript' src='/plugins/jquery-ui-timepicker/jquery-ui-timepicker-addon.js'></script>{0}", Environment.NewLine);
        }

        public static string GetMaskedInput()
        {
            return string.Format("<script type='text/javascript' src='/plugins/maskedinput/jquery.maskedinput.min.js'></script>{0}", Environment.NewLine);
        }

        public static string GetQuizzes()
        {
            return string.Format("<link type='text/css' href='/plugins/QuizUserControl/QuizUserControl.ascx.css' rel='stylesheet' />{0}<script type='text/javascript' src='/plugins/QuizUserControl/QuizUserControl.ascx.js'></script>{0}", Environment.NewLine);
        }

        public static string GetQuizMaker()
        {
            return string.Format("<link type='text/css' href='/plugins/QuizMaker/QuizMakerControl.ascx.css' rel='stylesheet' />{0}<script type='text/javascript' src='/plugins/QuizMaker/QuizMakerControl.ascx.js'></script>{0}", Environment.NewLine);
        }

        public static string GetScrollTo()
        {
            return string.Format("<script type='text/javascript' src='/plugins/scroll-to/jquery.scrollTo-1.4.3.1-min.js'></script>{0}", Environment.NewLine);
        }

        public static string GetSuccessErrorMessageControlAdmin()
        {
            return string.Format("<link type='text/css' href='/plugins/SuccessErrorMessageAdmin/SuccessErrorControl.css' rel='stylesheet' />{0}<script type='text/javascript' src='/plugins/SuccessErrorMessageAdmin/SuccessErrorControl.js'></script>{0}", Environment.NewLine);
        }

        public static string GetTagsPlugin()
        {
            return string.Format("<link href=\"/plugins/TagManager3.0/tagmanager.css\" rel=\"stylesheet\">{0}<script type=\"text/javascript\" src=\"/plugins/TagManager3.0/tagmanager.js\"></script>{0}", Environment.NewLine);
        }

        public static string GetTinyMCE()
        {
            return string.Format("<script type='text/javascript' src='/plugins/tinymce/jquery.tinymce.js'></script>{0}<script type='text/javascript' src='/plugins/tinymce/tiny_mce.js'></script>{0}", Environment.NewLine);
        }

        public static string GetValidation()
        {
            return string.Format("<script type='text/javascript' src='/scripts/validation-2.0.js'></script>{0}", Environment.NewLine);
        }
        #endregion Methods
    }
}
