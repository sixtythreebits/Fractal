using System;
using System.Xml.Linq;

namespace Core
{
    public class AdminUserCourse
    {
        #region Properties
        public long? RecordID { set; get; }
        public long UserID { set; get; }
        public long CourseID { set; get; }
        public string Caption { set; get; }
        public string Username { set; get; }
        public string UserFullName { set; get; }
        public string Avatar { set; get; }
        public bool IsCreator { set; get; }
        public string RoleCaption { set; get; }
        public string RoleCode { set; get; }
        public byte SortIndex { set; get; }
        public bool IsVisibleInTeacherList { set; get; }
        public DateTime? CRTime { set; get; }
        #endregion Properties
    }

    public struct AlertCourse
    {
        #region Properties
        public long? RecordID { set; get; }
        public long? CourseID { set; get; }
        public long? SectionID { set; get; }
        public string Course { set; get; }
        public string Section { set; get; }
        #endregion Properties
    }

    public struct AssetQuiz
    {
        #region Properties
        public long? RecordID { set; get; }
        public long ID { set; get; }
        public string Caption { set; get; }
        public string Time { set; get; }
        public bool IsPractice { set; get; }
        public bool ShowHints { set; get; }
        public bool ShowAnalysis { set; get; }
        public bool ShowOtherAnswers { set; get; }
        public bool AllowSkip { set; get; }
        #endregion Properties
    }

    public struct GradeComponent
    {
        #region Properties
        public long ID { set; get; }
        public string Caption { set; get; }
        public decimal MaxScore { set; get; }
        public string ComponentTypeCode { set; get; }
        public string css { set; get; }
        public string Comment { set; get; }
        public XElement Properties { set; get; }
        #endregion Properties
    }

    public struct SimpleIDValue
    {
        #region Properties
        public int ID { set; get; }
        public string Value { set; get; }
        #endregion Properties
    }

    public struct SimpleIDValue<T1, T2>
    {
        #region Properties
        public T1 ID { set; get; }
        public T2 Value { set; get; }
        #endregion Properties
    }

    public struct UserQuiz
    {
        #region Properties
        public long? RecordID { set; get; }
        public long? ID { set; get; }
        public string Caption { set; get; }
        public long? UserID { set; get; }
        public string Username { set; get; }
        public string UserFullName { set; get; }
        public bool View { set; get; }
        public bool Edit { set; get; }
        public bool Delete { set; get; }
        public DateTime? CRTime { set; get; }
        #endregion Properties
    }

    public struct UserAsset
    {
        #region Properties
        public long? RecordID { set; get; }
        public long? ID { set; get; }
        public string Caption { set; get; }
        public long UserID { set; get; }
        public string Username { set; get; }
        public string UserFullName { set; get; }
        public bool View { set; get; }
        public bool Edit { set; get; }
        public bool Delete { set; get; }
        public DateTime CRTime { set; get; }
        #endregion Properties
    }

    public struct CourseQuiz
    {
        #region Properties
        public long RecordID { get; set; }
        public long ID { get; set; }
        public string Caption { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public DateTime? GradeReleaseDate { get; set; }
        public bool IsPractice { get; set; }
        public bool IsPublished { get; set; }
        public bool ShowAnswers { get; set; }
        public DateTime CRTime { get; set; }
        public long? SectionID { get; set; }
        public string Section { get; set; }
        public decimal? MaxScore { get; set; }
        #endregion Properties
    }

    public struct CourseQuizFront
    {
        #region Properties
        public long RecordID { get; set; }
        public long ID { get; set; }
        public string Caption { get; set; }
        public int? MaxScore { set; get; }
        public int? StudentScore { set; get; }
        public long? CourseID { set; get; }
        public string CourseSlug { set; get; }
        public string CourseCaption { set; get; }
        public DateTime? ExpDate { set; get; }
        public DateTime? CRTime { set; get; }
        #endregion Properties
    }

    public struct TeacherQuiz
    {
        public long? ID { set;get; }
        public string Caption { set; get; }
        public int? QuestionsCount { set; get; }
        public DateTime CRTime  { set; get; }
    }
}