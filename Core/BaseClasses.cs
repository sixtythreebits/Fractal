using System.Text;
using Core;

namespace Core
{
    public class AdminMasterBase : System.Web.UI.MasterPage
    {
        #region Properties
        #region General Properties
        public string Root { set; get; }        
        public User UserObject { set; get; }
        public StringBuilder HeaderInclude { set; get; }
        #endregion General Properties

        #region Plugins
        bool _ATooltipEnabled;
        public bool ATooltipEnabled
        {
            set
            {
                _ATooltipEnabled = value;
                if (_ATooltipEnabled)
                {
                    HeaderInclude.Append(Plugins.GetAToolTip());
                }
            }
            get { return _ATooltipEnabled; }
        }

        bool _ContextMenuEnabled;
        public bool ContextMenuEnabled
        {
            set
            {
                _ContextMenuEnabled = value;
                if (_ContextMenuEnabled)
                {
                    HeaderInclude.Append(Plugins.GetJQueryContextMenu());
                }
            }
            get { return _ContextMenuEnabled; }
        }

        bool _DropzoneEnabled;
        public bool DropzoneEnabled
        {
            set
            {
                _DropzoneEnabled = value;
                if (_DropzoneEnabled)
                {
                    HeaderInclude.Append(Plugins.GetDropzone());
                }
            }
            get { return _DropzoneEnabled; }
        }


        bool _FancyBoxEnabled;
        public bool FancyBoxEnabled
        {
            set
            {
                _FancyBoxEnabled = value;
                if (_FancyBoxEnabled)
                {
                    HeaderInclude.Append(Plugins.GetFancyBoxV2());
                }
            }
            get { return _FancyBoxEnabled; }
        }

        bool _FormsEnabled;
        public bool FormsEnabled
        {
            set
            {
                _FormsEnabled = value;
                if (_FormsEnabled)
                {
                    HeaderInclude.Append(Plugins.GetAdminForms());
                }
            }
            get { return _FormsEnabled; }
        }

        bool _FormsCssEnabled;
        public bool FormsCssEnabled
        {
            set
            {
                _FormsCssEnabled = value;
                if (_FormsCssEnabled)
                {
                    HeaderInclude.Append(Plugins.GetAdminFormsCssOnly());
                }
            }
            get { return _FormsCssEnabled; }
        }

        bool _JQueryNumericEnabled;
        public bool JQueryNumericEnabled
        {
            set
            {
                _JQueryNumericEnabled = value;
                if (_JQueryNumericEnabled)
                {
                    HeaderInclude.Append(Plugins.GetJQueryNumeric());
                }
            }
            get { return _JQueryNumericEnabled; }
        }

        bool _JQueryUIEnabled;
        public bool JQueryUIEnabled
        {
            set
            {
                _JQueryUIEnabled = value;
                if (_JQueryUIEnabled)
                {
                    HeaderInclude.Append(Plugins.GetJQueryUI());
                }
            }
            get { return _JQueryUIEnabled; }
        }

        bool _QuizzesEnabled;
        public bool QuizzesEnabled
        {
            set
            {
                _QuizzesEnabled = value;
                if (_QuizzesEnabled)
                {
                    HeaderInclude.Append(Plugins.GetQuizzes());
                }
            }
            get { return _QuizzesEnabled; }
        }

        bool _QuizMakerEnabled;
        public bool QuizMakerEnabled
        {
            set
            {
                _QuizMakerEnabled = value;
                if (_QuizMakerEnabled)
                {
                    HeaderInclude.Append(Plugins.GetQuizMaker());
                }
            }
            get { return _QuizMakerEnabled; }
        }

        bool _TimePickerEnabled;
        public bool TimePickerEnabled
        {
            set
            {
                _TimePickerEnabled = value;
                if (_TimePickerEnabled)
                {
                    HeaderInclude.Append(Plugins.GetJQueryUITimePicker());
                }
            }
            get { return _TimePickerEnabled; }
        }

        bool _MaskedInputEnabled;
        public bool MaskedInputEnabled
        {
            set
            {
                _MaskedInputEnabled = value;
                if (_MaskedInputEnabled)
                {
                    HeaderInclude.Append(Plugins.GetMaskedInput());
                }
            }
            get { return _MaskedInputEnabled; }
        }

        bool _ScrollToEnabled;
        public bool ScrollToEnabled
        {
            set
            {
                _ScrollToEnabled = value;
                if (_ScrollToEnabled)
                {
                    HeaderInclude.Append(Plugins.GetScrollTo());
                }
            }
            get { return _ScrollToEnabled; }
        }

        bool _TinyMceEnabled;
        public bool TinyMceEnabled
        {
            set
            {
                _TinyMceEnabled = value;
                if (_TinyMceEnabled)
                {
                    HeaderInclude.Append(Plugins.GetTinyMCE());
                }
            }
            get { return _TinyMceEnabled; }
        }
        
        bool _SuccessErrorMessageEnabled;
        public bool SuccessErrorMessageEnabled
        {
            set
            {
                _SuccessErrorMessageEnabled = value;
                if (_SuccessErrorMessageEnabled)
                {
                    HeaderInclude.Append(Plugins.GetSuccessErrorMessageControlAdmin());
                }
            }
            get { return _SuccessErrorMessageEnabled; }
        }

        bool _TagsEnabled;
        public bool TagsEnabled
        {
            set
            {
                _TagsEnabled = value;
                if (_TagsEnabled)
                {
                    HeaderInclude.Append(Plugins.GetTagsPlugin());
                }
            }
            get { return _TagsEnabled; }
        }
        #endregion Plugins
        #endregion Properties

        #region Constructors
        public AdminMasterBase()
        {
            Root = "/administration/";
            UserObject = new User();
            UserObject.GetAuthorizedCredentials();

            HeaderInclude = new StringBuilder();
            HeaderInclude.Append(Plugins.GetJQuery());
            HeaderInclude.Append(Plugins.GetGMLoader());
            HeaderInclude.Append(Plugins.GetClock());
        }

        #endregion Constructors
    }

}