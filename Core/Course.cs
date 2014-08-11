using System;
using System.Collections.Generic;
using System.Linq;
using SystemBase;
using DB;
using Lib;
using System.Xml.Linq;
using Core.Utilities;

namespace Core
{
    public class Course : ObjectBase
    {
        #region Properties
        public long? ID { set; get; }
        public string Caption { set; get; }
        public string Description { set; get; }
        public string Icon { set; get; }
        public string PromoImage { get; set; }
        public int? SubjectID { set; get; }
        public string Subject { set; get; }
        public short? Year { set; get; }
        public int? SemesterID { set; get; }
        public string Semester { set; get; }
        public bool IsPublished { set; get; }
        public bool IsExpired { get; set; }
        public string Slug { set; get; }
        public string unid { set; get; }
        public XElement Properties { set; get; }
        #endregion Properties

        #region Constructors
        public Course() { }

        public Course(long? ID)
        {
            GetSingleCourse(ID);
        }

        public Course(string SlugOrID)
        {
            GetSingleCourse(ID: SlugOrID.ToLong(), slug: SlugOrID);
        }

        public Course(long? ID, string slug)
        {
            GetSingleCourse(ID: ID, slug: slug);
        }
        #endregion Constructors

        #region Methods

        /// <summary>
        /// Removes course completely, with it's sections, discussions, posts
        /// </summary>
        /// <param name="ID">Course ID</param>
        public void DeleteCourse(long? ID)
        {
            TryExecute(string.Format("Core.Course.DeleteCourse(ID = {0})", ID), () =>
            {
                using (var db = ConnectionFactory.GetDBCoreDataContext())
                {
                    GetSingleCourse(ID);
                    if (System.IO.File.Exists(AppSettings.UploadFolderPhysicalPath + Icon))
                    {
                        System.IO.File.Delete(AppSettings.UploadFolderPhysicalPath + Icon);
                    }
                    db.DeleteCourse(ID);
                }
            });
        }

        /// <summary>
        /// Gets user ID who created a course
        /// </summary>
        /// <param name="CourseID">Course ID</param>
        /// <returns>User ID</returns>
        public static long? GetCreatorUserID(long? CourseID)
        {
            return TryToReturnStatic<long>(string.Format("Core.Course.GetCourseCreatorUser(CourseID = {0})", CourseID), () =>
            {
                using (var db = ConnectionFactory.GetDBCoreDataContext())
                {
                    return db.GetCourseCreatorUserID(CourseID).Value;
                }
            });
        }

        /// <summary>
        /// Gets Single Course based on ID parameter
        /// </summary>
        /// <param name="ID">Course ID</param>
        public void GetSingleCourse(long? ID, string slug = null, byte iud = 0)
        {
            TryExecute(string.Format("Core.Course.GetSingleCourse(ID = {0}, slug = {1}, iud = {2})", ID, slug, iud), () =>
            {
                using (var db = ConnectionFactory.GetDBCoreDataContext())
                {
                    var x = db.GetSingleCourse(iud, ID, slug);
                    if (x == null)
                    {
                        IsError = true;
                        ErrorMessage = "Course Not Found";
                    }
                    else
                    {
                        switch (iud)
                        {
                            case 0:
                                {
                                    this.ID = long.Parse(x.Element("id").Value);
                                    this.Caption = x.Element("caption").Value;
                                    this.Description = x.Element("description").Value;
                                    this.SubjectID = x.Element("subject_id").Value == "0" ? null : (int?)int.Parse(x.Element("subject_id").Value);
                                    this.Subject = x.Element("subject").Value;
                                    this.Icon = x.Element("icon").Value;
                                    this.PromoImage = x.ValueOf("promo_image");
                                    this.SemesterID = x.Element("semester_id").Value == "0" ? null : (int?)int.Parse(x.Element("semester_id").Value);
                                    this.Semester = x.Element("semester").Value;
                                    this.Year = short.Parse(x.Element("year").Value);
                                    this.IsPublished = x.Element("is_published").Value == "1";
                                    this.Slug = x.ValueOf("slug");

                                    this.Properties = x.Element("teachers");
                                    break;
                                }
                            case 1:
                                {
                                    this.ID = x.LongValueOf("id");
                                    this.Caption = x.ValueOf("caption");
                                    this.Description = x.ValueOf("description");
                                    this.Icon = x.ValueOf("icon");
                                    this.PromoImage = x.ValueOf("promo_image");
                                    this.Properties = x;
                                    break;
                                }
                        }
                    }
                }
            });
        }

        /// <summary>
        /// Checkes whether course slug is uniq or not
        /// </summary>
        /// <param name="slug">Slug Word</param>
        /// <param name="CourseID">Course ID</param>
        /// <returns></returns>
        public bool IsCourseSlugUniq(string slug, long? CourseID = null)
        {
            return TryToReturn<bool>(string.Format("Core.Course.IsCourseSlugUniq(slug = {0}, CourseID = {1})", slug, CourseID), () =>
            {
                using (var db = ConnectionFactory.GetDBCoreDataContext())
                {
                    return db.IsCourseSlugUniq(slug, CourseID).Value;
                }
            });
        }

        /// <summary>
        /// Gets List of Courses
        /// </summary>        
        /// <returns></returns>
        public List<Course> ListCourses()
        {
            return TryToGetList<Course>(string.Format("Core.Course.ListCourses()"), () =>
            {
                using (var db = ConnectionFactory.GetDBCoreDataContext())
                {
                    var list = db.List_Courses().OrderBy(C => C.Caption).Select(C => new Course
                    {
                        ID = C.CourseID,
                        Caption = C.Caption,
                        Description = C.Description,
                        SubjectID = C.SubjectID,
                        Icon = C.Icon,
                        SemesterID = C.SemesterID,
                        Year = C.Year,
                        IsPublished = C.IsPublished,
                        CRTime = C.CRTime,
                        Slug = C.Slug
                    }).ToList();

                    return list;
                }
            });
        }

        /// <summary>
        /// Gets List of UserCourses
        /// </summary>
        /// <returns>List of courses</returns>
        public List<Course> ListUserCourses(long? UserID, bool? IsPublished = null)
        {
            return TryToGetList<Course>(string.Format("Course.ListUserCourses(UserID = {0}, IsPublished = {1})", UserID, IsPublished), () =>
            {
                using (var db = ConnectionFactory.GetDBCoreDataContext())
                {
                    return db.List_UserCourses(UserID, IsPublished)
                    .OrderByDescending(C => C.CRTime)
                    .Select(C => new Course
                    {
                        ID = C.CourseID,
                        Caption = C.Caption,
                        Description = C.Description,
                        Icon = C.Icon,
                        SubjectID = C.SubjectID,
                        Year = C.Year,
                        SemesterID = C.SemesterID,
                        Semester = C.Semester,
                        IsPublished = C.IsPublished,
                        CRTime = C.CRTime,
                        Slug = C.Slug
                    }).ToList();
                }
            });
        }

        /// <summary>
        /// Gets List of Subscribed Courses By User
        /// </summary>
        /// <param name="UserID">User ID</param>
        /// <returns></returns>
        public List<Course> ListUserSubscribedCourses(long? UserID, bool ActiveOnly = false)
        {
            return TryToGetList<Course>(string.Format("Core.Course.ListUserSubscribedCourses(UserID = {0}, ActiveOnly = {1})", UserID, ActiveOnly), () =>
            {
                using (var db = ConnectionFactory.GetDBCoreDataContext())
                {
                    return db.List_UserSubscribedCourses(UserID, ActiveOnly).Select(C => new Course
                    {
                        ID = C.CourseID,
                        Caption = C.Caption,
                        Icon = C.Icon,
                        CRTime = C.ExpDate,
                        IsExpired = C.IsExpired.Value,
                        Slug = C.Slug
                    }).ToList();
                }
            });
        }

        /// <summary>
        /// Performs CRUD action on Courses table in database
        /// </summary>
        /// <param name="iud">Action ID</param>
        /// <param name="ID">Database uniq ID</param>
        /// <param name="Caption">Course caption</param>
        /// <param name="Description">Course description</param>
        /// <param name="SubjectID">Subject ID</param>
        /// <param name="Icon">Icon</param>
        /// <param name="SemesterID">Semester ID</param>
        /// <param name="Year">Year</param>
        /// <param name="IsPublished">Is Course published?</param>
        /// <param name="Slug">Course url slug</param>
        public void TSP_Courses(byte? iud = null, long? ID = null, string Caption = null, string Description = null, int? SubjectID = null, string Icon = null, string PromoImage = null, int? SemesterID = null, short? Year = null, bool? IsPublished = null, string Slug = null)
        {
            TryExecute(string.Format("Core.Course.TSP_Courses(iud = {0}, ID = {1}, Caption = {2}, Description = {3}, SubjectID = {4}, Icon = {5}, PromoImage = {6}, SemesterID = {7}, Year = {8}, IsPublished = {9}, Slug = {10})", iud, ID, Caption, Description, SubjectID, Icon, PromoImage, SemesterID, Year, IsPublished, Slug), () =>
            {
                using (var db = ConnectionFactory.GetDBCoreDataContext())
                {
                    db.tsp_Courses(iud, ref ID, Caption, Description, SubjectID, Icon, PromoImage, SemesterID, Year, IsPublished, Slug);
                    this.ID = ID.Value;
                }
            });
        }

        /// <summary>
        /// Performs action with course table based on iud parameter and xml values
        /// </summary>
        /// <param name="iud">Action ID</param>
        /// <param name="xml">Input parameters given as xml</param>
        public void TX_Courses(byte? iud, string xml)
        {
            TryExecute(string.Format("Core.Course.tx_Courses(iud = {0}, xml = {1})", iud, xml), () =>
            {
                using (var db = ConnectionFactory.GetDBCoreDataContext())
                {
                    XElement output = null;
                    db.tx_Courses(iud, XElement.Parse(xml), ref output);
                    this.Properties = output;
                }
            });
        }
        #endregion Methods
    }

    public class CourseKeyGroup : ObjectBase
    {
        #region Properties
        public long ID { set; get; }
        public long CourseID { set; get; }
        public string CourseCaption { set; get; }
        public string Caption { set; get; }
        public int KeyCount { set; get; }
        public byte Months { set; get; }
        #endregion Properties

        #region Constructors
        public CourseKeyGroup() { }

        public CourseKeyGroup(long? ID)
        {
            GetSingleCourseKeyGroup(ID);
        }
        #endregion Constructors

        #region Methods
        /// <summary>
        /// Gets course key group properties by ID
        /// </summary>
        /// <param name="ID"></param>
        public void GetSingleCourseKeyGroup(long? ID)
        {
            TryExecute(string.Format("Core.Course.GetSingleCourseKeyGroup(ID = {0})", ID), () =>
            {
                using (var db = ConnectionFactory.GetDBCoreDataContext())
                {
                    var x = db.GetSingleCourseKeyGroup(ID);
                    if (x == null)
                    {
                        IsError = true;
                        ErrorMessage = "Group not found";
                    }
                    else
                    {
                        this.ID = long.Parse(x.Element("id").Value);
                        this.Caption = x.Element("caption").Value;
                        this.Months = byte.Parse(x.Element("months").Value);
                    }
                }
            });
        }

        /// <summary>
        /// Gets list of course key groups
        /// </summary>
        /// <returns>List of key groups</returns>
        public List<CourseKeyGroup> ListCourseKeyGroups(long? CourseID)
        {
            return TryToGetList<CourseKeyGroup>("Core.CourseKey.ListCourseKeyGroups()", () =>
            {
                using (var db = ConnectionFactory.GetDBCoreDataContext())
                {
                    return db.List_CourseKeyGroups(CourseID)
                    .OrderByDescending(g => g.CRTime)
                    .Select(g => new CourseKeyGroup
                    {
                        ID = g.GroupID,
                        Caption = g.Group,
                        KeyCount = g.KeyCount,
                        Months = g.MonthLifeTime
                    }).ToList();
                }
            });
        }

        /// <summary>
        /// Performs CRUD action with database CourseKeyGroups Table
        /// </summary>
        /// <param name="iud">Action ID</param>
        /// <param name="GroupID">Group ID</param>
        /// <param name="CourseID">Course ID</param>
        /// <param name="Caption">Group caption</param>
        /// <param name="LifeTimeMonthes">Key life time in monthes</param>
        public void TSP_CourseKeysGroup(byte? iud = null, long? ID = null, long? CourseID = null, string Caption = null, int? LifeTimeTypeID = null, byte? MonthLifeTime = null, byte? DayLifeTime = null, DateTime? ExpDate = null)
        {
            TryExecute(string.Format("Core.CourseKey.TSP_CourseKeys(iud = {0}, ID = {1}, CourseID = {2}, Caption = {3}, LifeTimeTypeID = {4}, MonthLifeTime = {5}, DayLifeTime = {6}, ExpDate = {7})", iud, ID, CourseID, Caption, LifeTimeTypeID, MonthLifeTime, DayLifeTime, ExpDate), () =>
            {
                using (var db = ConnectionFactory.GetDBCoreDataContext())
                {
                    db.tsp_CourseKeyGroups(iud, ref ID, CourseID, Caption, LifeTimeTypeID, MonthLifeTime, DayLifeTime, ExpDate);
                    this.ID = ID.Value;
                }
            });
        }
        #endregion Methods
    }

    public class CourseKey : ObjectBase
    {
        #region Properties
        public long ID { set; get; }
        public string Key { set; get; }
        public long? UserID { set; get; }
        public string Username { set; get; }
        public string UserFullName { set; get; }
        public bool IsUsed { set; get; }
        public DateTime? KeyUseTime { set; get; }
        #endregion Properties

        #region Methods
        /// <summary>
        /// Gets list of course keys by group ID
        /// </summary>
        /// <param name="GroupID">Course key group ID</param>
        /// <returns>Course keys</returns>
        public List<CourseKey> ListCourseKeysByGroupID(long GroupID)
        {
            return TryToGetList<CourseKey>("Core.CourseKey.ListCourseKeysByGroupID(GroupID = " + GroupID + ")", () =>
            {
                using (var db = ConnectionFactory.GetDBCoreDataContext())
                {
                    return db.List_CourseKeysByGroupID(GroupID).Select(k => new CourseKey
                    {
                        ID = k.KeyID,
                        Key = k.Key,
                        IsUsed = k.UserID.HasValue,
                        UserFullName = k.Fname + " " + k.Lname,
                        UserID = k.UserID,
                        KeyUseTime = k.UseTime
                    }).ToList();
                }
            });
        }

        /// <summary>
        /// Gets list of all course keys in databse
        /// </summary>
        /// <returns>List of Keys</returns>
        public List<CourseKey> ListCourseKeys()
        {
            return TryToGetList<CourseKey>("Core.CourseKey.ListCourseKeys()", () =>
            {
                using (var db = ConnectionFactory.GetDBCoreDataContext())
                {
                    return db.List_CourseKeys()
                            .OrderByDescending(CK => CK.RegTime)
                             .Select(CK => new CourseKey
                             {
                                 ID = CK.KeyID,
                                 Key = CK.Key,
                                 UserID = CK.UserID,
                                 IsUsed = CK.UserID.HasValue,
                                 Username = CK.Username,
                                 UserFullName = CK.Fname + " " + CK.Lname,
                                 CRTime = CK.RegTime,
                             }).ToList();
                }
            });
        }

        /// <summary>
        /// Performs CRUD action with database CourseKeys Table
        /// </summary>
        /// <param name="iud">Action ID</param>
        /// <param name="ID">Key ID</param>
        /// <param name="GroupID">Group ID</param>
        /// <param name="Key">Key string</param>
        public void TSP_CourseKeys(byte? iud = null, long? ID = null, long? GroupID = null, string Key = null)
        {
            TryExecute(string.Format("Core.CourseKey.TSP_CourseKeys(iud = {0}, ID = {1}, GroupID = {2}, Key = {3})", iud, ID, GroupID, Key), () =>
            {
                using (var db = ConnectionFactory.GetDBCoreDataContext())
                {
                    db.tsp_CourseKeys(iud, ref ID, GroupID, Key);
                    this.ID = ID.Value;
                }
            });
        }

        /// <summary>
        /// Performs action with CourseKey object based on iud parameter and xml values
        /// </summary>
        /// <param name="iud">Action ID</param>
        /// <param name="xml">Input parameters given as xml</param>
        public void TX_CourseKeys(byte iud, string xml)
        {
            TryExecute(string.Format("Core.CourseKey.TX_CourseKeys(iud = {0}, xml = {1})", iud, xml), () =>
            {
                using (var db = ConnectionFactory.GetDBCoreDataContext())
                {
                    db.tx_CourseKeys(iud, XElement.Parse(xml));
                }
            });
        }
        #endregion Methods
    }

    public class Subscription : ObjectBase
    {
        #region Properties
        public long ID { set; get; }
        public int TypeID { set; get; }
        public string TypeCode { set; get; }
        public string Type { set; get; }
        public long? TariffID { set; get; }
        public decimal? Price { set; get; }
        public DateTime? ExpDate { set; get; }
        public long CourseID { set; get; }
        public string Course { set; get; }
        public long UserID { set; get; }
        public string UserFullName { set; get; }
        public string Fname { set; get; }
        public string Lname { set; get; }
        public string Email { set; get; }
        public string Note { set; get; }
        public XElement Properties { set; get; }
        #endregion Properties

        #region Methods
        /// <summary>
        /// Gets List of only user most recent subscriptions
        /// </summary>
        /// <returns></returns>
        public List<Subscription> ListRecentSubscriptions(byte? iud = null, object filter = null)
        {
            return TryToGetList<Subscription>("Subscription.ListRecentSubscriptions() - ", () =>
            {
                using (var db = ConnectionFactory.GetDBCoreDataContext())
                {
                    var query = db.List_UserCourseRecentSubscriptions();

                    switch (iud)
                    {
                        case 0:
                            {
                                var x = XElement.Parse(filter.ToString());
                                long CourseID = long.Parse(x.Element("course_id").Value);
                                query = query.Where(S => S.CourseID == CourseID);

                                if (x.Element("d1") != null)
                                {
                                    var d1 = DateTime.Parse(x.Element("d1").Value);
                                    query = query.Where(S => S.ExpDate >= d1);
                                }

                                if (x.Element("d2") != null)
                                {
                                    var d2 = DateTime.Parse(x.Element("d2").Value);
                                    query = query.Where(S => S.ExpDate <= d2);
                                }

                                if (x.Element("crtime_d1") != null)
                                {
                                    var d1 = DateTime.Parse(x.Element("crtime_d1").Value);
                                    query = query.Where(S => S.CRTime >= d1);
                                }

                                if (x.Element("crtime_d2") != null)
                                {
                                    var d2 = DateTime.Parse(x.Element("crtime_d2").Value);
                                    query = query.Where(S => S.CRTime <= d2);
                                }

                                if (x.Element("user") != null)
                                {
                                    var user = x.Element("user").Value;
                                    query = query.Where(S => S.Fname.Contains(user) || S.Lname.Contains(user));
                                }

                                if (x.Element("type_id") != null)
                                {
                                    var TypeID = int.Parse(x.Element("type_id").Value);
                                    query = query.Where(S => S.Type == TypeID);
                                }


                                return query.OrderByDescending(S => S.CRTime).Select(S => new Subscription
                                {
                                    ID = S.SubscriptionID,
                                    UserID = S.UserID,
                                    Fname = S.Fname,
                                    Lname = S.Lname,
                                    UserFullName = S.Fname + " " + S.Lname,
                                    Email = S.Email,
                                    CourseID = S.CourseID,
                                    Course = S.Course,
                                    TypeCode = S.TypeCode,
                                    ExpDate = S.ExpDate,
                                    CRTime = S.CRTime
                                }).ToList();
                            }
                        default:
                            {
                                return query.OrderByDescending(S => S.CRTime).Select(S => new Subscription
                                {
                                    ID = S.SubscriptionID,
                                    UserID = S.UserID,
                                    Fname = S.Fname,
                                    Lname = S.Lname,
                                    UserFullName = S.Username + " - " + S.Fname + " " + S.Lname,
                                    Email = S.Email,
                                    CourseID = S.CourseID,
                                    Course = S.Course,
                                    ExpDate = S.ExpDate,
                                    CRTime = S.CRTime
                                }).ToList();
                            }
                    }
                }
            });
        }

        /// <summary>
        /// Gets list of subscription history on particular user - course combination
        /// </summary>
        /// <param name="UserID">User ID</param>
        /// <param name="CourseID">Course ID</param>
        /// <returns>List of subscription history</returns>
        public List<Subscription> ListUserCourseSubscriptionHistory(long? UserID, long? CourseID)
        {
            return TryToGetList<Subscription>(string.Format("Core.Subscription.ListUserCourseSubscriptionHistory(UserID = {0}, CourseID = {1})", UserID, CourseID), () =>
            {
                using (var db = ConnectionFactory.GetDBCoreDataContext())
                {
                    return db.List_UserCourseSubscriptionHistory(UserID, CourseID)
                             .OrderByDescending(S => S.CRTime)
                             .Select(S => new Subscription
                             {
                                 ID = S.SubscriptionID,
                                 UserID = S.UserID,
                                 UserFullName = S.FullName,
                                 CourseID = S.CourseID,
                                 Course = S.Course,
                                 ExpDate = S.ExpDate,
                                 CRTime = S.CRTime
                             }).ToList();
                }
            });
        }

        /// <summary>
        /// Performs action based on iud parameter with database Subscriptions Object
        /// </summary>
        /// <param name="iud">Action ID</param>
        /// <param name="ID">Database uniq ID</param>
        /// <param name="UserID">User ID</param>
        /// <param name="CourseID">Course ID</param>
        /// <param name="TypeCode">Type Code</param>
        /// <param name="Type">Type ID</param>
        /// <param name="TariffID">Tariff ID</param>
        /// <param name="ExpDate">Expiration Date</param>
        /// <param name="Note">Note</param>
        public void TSP_Subscription(byte? iud = null, long? ID = null, long? UserID = null, long? CourseID = null, string TypeCode = null, int? Type = null, long? TariffID = null, DateTime? ExpDate = null, string Note = null)
        {
            TryExecute(string.Format("Core.Subscription.TSP_Subscription(iud = {0}, ID = {1}, UserID = {2}, CourseID = {3}, TypeCode = {4}, Type = {5}, TariffID = {6}, ExpDate = {7}, Note = {8})", iud, ID, UserID, CourseID, TypeCode, Type, TariffID, ExpDate, Note), () =>
            {
                using (var db = ConnectionFactory.GetDBCoreDataContext())
                {
                    db.tsp_Subscriptions(iud, ID, UserID, CourseID, TypeCode, Type, TariffID, ExpDate, Note);
                }
            });
        }

        /// <summary>
        /// Performs action with user-course subscriptions based on iud parameter and xml values
        /// </summary>
        /// <param name="iud">Action ID</param>
        /// <param name="xml">Input parameters given as xml</param>
        public void TX_Subscriptions(byte iud, string xml)
        {
            XElement RetVal = null;
            TryExecute(string.Format("Core.Subscription.TX_Subscriptions(iud = {0}, xml = {1})", iud, xml), () =>
            {
                using (var db = ConnectionFactory.GetDBCoreDataContext())
                {
                    db.tx_Subscriptions(iud, XElement.Parse(xml), ref RetVal);
                }
            });
            Properties = RetVal;
        }
        #endregion Methods
    }
}
