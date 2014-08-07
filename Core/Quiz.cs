using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using System.Text;
using SystemBase;
using DB;
using Lib;
using Core.Utilities;

namespace Core
{
    public class Answer : ObjectBase
    {
        #region Properties
        public long? ID { set; get; }
        public long? QuestionID { set; get; }
        public short Number { set; get; }
        public string answer { set; get; }
        public string Explanation { set; get; }
        public bool IsCorrect { set; get; }
        public bool IsUserAnswer { set; get; }
        public int AnsweredCount { set; get; }
        public XElement Properties { set; get; }
        #endregion Properties

        #region Methods
        /// <summary>
        /// Gets single answer data by answer ID
        /// </summary>
        /// <param name="ID"></param>
        public void GetSingleAnswer(long ID)
        {
            TryExecute(string.Format("Core.Answer.GetSingleAnswer(ID = {0})", ID), delegate()
            {
                using (var db = ConnectionFactory.GetDBCoreDataContext())
                {
                    Properties = db.GetSingleAnswer(ID);
                    if (Properties == null)
                    {
                        IsError = true;
                        ErrorMessage = "Answer not found";
                    }
                    else
                    {
                        this.ID = long.Parse(Properties.Element("id").Value);
                        this.answer = Properties.Element("answer").Value;
                        this.Explanation = Properties.Element("explanation").Value;
                        this.IsCorrect = Properties.Element("is_correct").Value == "1";
                        this.Number = short.Parse(Properties.Element("number").Value);
                    }
                }
            });
        }

        /// <summary>
        /// Gets List of Answers
        /// </summary>
        /// <returns></returns>
        public List<Answer> ListAnswers(long? QuestionID)
        {
            return TryToGetList<Answer>(string.Format("Core.Answer.ListAnswers(QuestionID = {0})", QuestionID), delegate()
            {
                using (var db = ConnectionFactory.GetDBCoreDataContext())
                {
                    return db.List_Answers(QuestionID).OrderByDescending(Q => Q.CRTime).Select(Q => new Answer
                    {
                        ID = Q.AnswerID,
                        QuestionID = Q.QuestionID,
                        answer = Q.Answer,
                        Explanation = Q.Explanation,
                        IsCorrect = Q.IsCorrect,
                        CRTime = Q.CRTime
                    }).ToList();
                }
            });
        }

        /// <summary>
        /// Performs CRUD action on Answers table in database
        /// </summary>
        /// <param name="iud">Action ID</param>
        /// <param name="ID">Database uniq ID</param>
        /// <param name="QuestionID">Question ID</param>
        /// <param name="answer">Answer text</param>
        /// <param name="Explanation">Explanation text</param>
        /// <param name="IsCorrect">Whether answer is correct or not</param>
        /// <param name="AssetID">Asset ID that should be attached to the answer</param>
        public void TSP_Answer(byte? iud = null, long? ID = null, long? QuestionID = null, string answer = null, string Explanation = null, bool? IsCorrect = null, long? AssetID = null)
        {
            TryExecute(string.Format("Core.Answer.TSP_Answer(iud = {0}, ID = {1}, QuestionID = {2}, answer = {3}, Explanation = {4}, IsCorrect = {5})", iud, ID, QuestionID, answer, Explanation, IsCorrect), delegate()
            {
                using (var db = ConnectionFactory.GetDBCoreDataContext())
                {
                    long? NewID = ID;
                    db.tsp_Answers(iud, ref NewID, QuestionID, answer, Explanation, IsCorrect, AssetID);
                    this.ID = NewID.Value;
                }
            });
        }

        /// <summary>
        /// Performs some action with Answers and some related tables, based on iud parameter and xml values
        /// </summary>
        /// <param name="iud">Action ID</param>
        /// <param name="xml">Input parameters given as xml</param>
        public void TX_Answers(byte? iud = null, string xml = null)
        {
            TryExecute(string.Format("Core.Answer.TX_Answers(iud = {0}, xml = {1})", iud, xml), delegate()
            {
                using (var db = ConnectionFactory.GetDBCoreDataContext())
                {
                    XElement output = null;
                    db.tx_Answers(iud, XElement.Parse(xml), ref output);
                    this.Properties = output;
                }
            });
        }
        #endregion Methods
    }

    public class Question : ObjectBase
    {
        #region Properties
        public long? ID { set; get; }
        public long? QuizID { set; get; }
        public short? Number { set; get; }
        public string question { set; get; }
        public string Hint { set; get; }
        public string Analysis { set; get; }
        public decimal? Score { set; get; }
        public bool IsPublished { set; get; }
        public int CorrectAnswerCount { set; get; }
        public XElement Properties { set; get; }
        public List<Answer> Answers { set; get; }
        public string FilePath { get; set; }
        public long? FileID { get; set; }
        public long? AssetID { get; set; }
        public string AssetCaption { get; set; }
        public long? VideoAnswerID { get; set; }
        public string VideoAnswerCaption { set; get; }
        public string VideoPlayerData { set; get; }
        public int VideoAnswerTime { set; get; }
        public string FileType { get; set; }
        public string Tags { get; set; }
        #endregion Properties

        #region Constructors
        public Question() { }

        public Question(long? ID)
        {
            GetSingleQuestion(ID);
        }
        #endregion Constructors

        #region Methods
        /// <summary>
        /// Gets single question properties by question ID
        /// </summary>
        /// <param name="ID">Question ID</param>
        public void GetSingleQuestion(long? ID)
        {
            TryExecute(string.Format("Core.Question.GetSingleQuestion(ID = {0})", ID), delegate()
            {
                using (var db = ConnectionFactory.GetDBCoreDataContext())
                {
                    var x = db.GetSingleQuestion(ID);
                    if (x == null)
                    {
                        IsError = true;
                        ErrorMessage = "Question not found";
                    }
                    else
                    {
                        this.Properties = x;
                        this.ID = long.Parse(x.Element("id").Value);
                        this.question = x.Element("question").Value;
                        this.Hint = x.Element("hint").Value;
                        this.Analysis = x.Element("analysis").Value;
                        this.Score = decimal.Parse(x.Element("score").Value);
                        this.AssetID = long.Parse(x.Element("asset_id").Value);
                        this.AssetID = this.AssetID > 0 ? (long?)this.AssetID : null;                        
                    }
                }
            });
        }

        /// <summary>
        /// Gets list of user's questions, that are not in the current quiz and match the keywords
        /// </summary>
        /// <param name="UserID">User ID</param>
        /// <param name="QuizID">Quiz ID</param>
        /// <param name="KeyWords">Keywords</param>
        /// <returns>List of questions</returns>
        public List<Question> ListUserQuestionsNotInQuiz(long? UserID, long? QuizID, string KeyWords)
        {
            return TryToGetList<Question>(string.Format("Core.Question.ListUserQuestionsNotInQuiz(UserID = {0}, QuizID = {1}, KeyWords = {2})", UserID, QuizID, KeyWords), delegate()
            {
                using (var db = ConnectionFactory.GetDBCoreDataContext())
                {
                    return db.List_UserQuestionsNotInQuiz(UserID, QuizID, KeyWords).Select(Q => new Question
                    {
                        ID = Q.QuestionID,
                        question = Q.Question,
                        AssetID = Q.AssetID
                    }).ToList();
                }
            });
        }

        /// <summary>
        /// Performs CRUD action on Questions table in database
        /// </summary>
        /// <param name="iud">Action ID</param>
        /// <param name="ID">Database uniq ID</param>                     
        /// <param name="question">Question</param>
        /// <param name="Hint">Hint</param>
        /// <param name="Analysis">Analysis</param>
        /// <param name="Score">Score</param>
        /// <param name="IsPublished">Is Published?</param>
        /// <param name="AssetID">Asset ID that should be attached to the question</param>
        public void TSP_Questions(byte? iud = null, long? ID = null, string question = null, string Hint = null, string Analysis = null, decimal? Score = null, bool IsPublished = true, long? assetID = null)
        {
            TryExecute(string.Format("Core.Question.TSP_Questions(iud = {0}, ID = {1}, question = {2}, Hint = {3}, Analysis = {4}, Score = {5}, IsPublished = {6})", iud, ID, question, Hint, Analysis, Score, IsPublished), delegate()
            {
                using (var db = ConnectionFactory.GetDBCoreDataContext())
                {
                    long? NewID = ID;
                    db.tsp_Questions(iud, ref NewID, question, Hint, Analysis, Score, IsPublished, assetID);
                    this.ID = NewID.Value;
                }
            });
        }

        /// <summary>
        /// Performs action with question object based on iud parameter and xml values
        /// </summary>
        /// <param name="iud">Action ID</param>
        /// <param name="xml">Input parameters given as xml</param>
        public void TX_Questions(byte iud, string xml = null)
        {
            TryExecute(string.Format("Core.Question.TX_Questions(iud = {0}, xml = {1})", iud, xml), delegate()
            {
                using (var db = ConnectionFactory.GetDBCoreDataContext())
                {
                    XElement output = null;
                    db.tx_Questions(iud, XElement.Parse(xml), ref output);
                    this.Properties = output;
                }
            });
        }
        #endregion Methods
    }

    public class Quiz : ObjectBase
    {
        #region Properties
        public long? ID { set; get; }
        public long? RecordID { set; get; }
        public string Caption { set; get; }
        public long? CourseID { set; get; }
        public string CourseCaption { set; get; }
        public string CourseNavigationSlug { set; get; }
        public long? UserID { set; get; }
        public string UserFullName { set; get; }
        public DateTime? StartDate { set; get; }
        public DateTime? EndDate { set; get; }
        public DateTime? GradeReleaseDate { set; get; }

        public bool IsPractice { set; get; }
        public bool IsPublished { set; get; }
        public bool IsExpired { set; get; }
        public bool ShowAnswers { set; get; }
        public bool ShowHints { set; get; }
        public bool ShowAnalysis { set; get; }
        public bool AllowSkip { set; get; }
        public bool IsTaken { set; get; }
        public bool ShowOtherStudentAnswers { set; get; }
        public decimal? Score { set; get; }
        public decimal? MaxScore { set; get; }
        public List<Question> Questions { set; get; }
        public string unid { set; get; }
        public XElement Properties { set; get; }
        #endregion Properties

        #region Constructors
        public Quiz()
        {
        }

        public Quiz(string xml)
        {
            GetSingleQuiz(xml);
        }

        public Quiz(byte iud, XElement xml)
        {
            switch (iud)
            {
                case 0:
                    {
                        this.Caption = xml.Element("caption").Value;
                        this.ID = xml.Element("quiz_id") != null ? long.Parse(xml.Element("quiz_id").Value) : 0;
                        this.Questions = xml.Element("questions") == null ? new List<Question>() :
                        xml.Element("questions").Elements("question").Select(q => new Question
                        {
                            Number = short.Parse(q.Element("num").Value),
                            question = q.Element("question").Value,
                            Hint = q.Element("hint").Value,
                            Analysis = q.Element("analysis").Value,
                            Answers = q.Element("answers") == null ? new List<Answer>() :
                            q.Element("answers").Elements("answer").Select(a => new Answer
                            {
                                answer = a.Element("answer").Value,
                                Explanation = a.Element("explanation").Value,
                                IsCorrect = a.Element("is_correct").Value == "1",
                            }).ToList()
                        }).OrderBy(q => q.Number).ToList();
                        break;
                    }
            }
        }
        #endregion Constructors

        #region Methods
        /// <summary>
        /// Gets single quiz with its questions and answers
        /// </summary>        
        /// <param name="xml">Input values as xml</param>        
        public void GetSingleQuiz(string xml)
        {
            TryExecute(string.Format("Core.Quiz.GetSingleQuiz(xml = {0})", xml), delegate()
            {
                using (var db = ConnectionFactory.GetDBCoreDataContext())
                {
                    var x = XElement.Parse(xml);
                    XElement el = Properties = db.GetSingleQuiz(x);

                    if (el == null)
                    {
                        IsError = true;
                        ErrorMessage = "Quiz not found";
                    }
                    else
                    {
                        Properties = el;
                        ID = el.LongValueOf("id");
                        Caption = el.ValueOf("caption");
                        IsTaken = el.BooleanValueOf("is_passed") == true;
                        StartDate = el.DateTimeValueOf("start_date");
                        EndDate = el.DateTimeValueOf("end_date");
                        GradeReleaseDate = el.DateTimeValueOf("grade_date");
                        ShowHints = el.BooleanValueOf("show_hints") == true;
                        ShowAnalysis = el.BooleanValueOf("show_analysis") == true;
                        ShowAnswers = IsTaken && (GradeReleaseDate.HasValue && GradeReleaseDate.Value < DateTime.Now) || (el.BooleanValueOf("show_answers") == true);
                        ShowAnalysis = ShowAnalysis == false ? IsTaken && ShowAnswers : ShowAnalysis;
                        IsPublished = el.BooleanValueOf("is_published") == true;
                        IsExpired = EndDate.HasValue && EndDate < DateTime.Now;
                        IsPractice = el.BooleanValueOf("is_practice") == true;
                        AllowSkip = el.BooleanValueOf("allow_skip") == true;
                        ShowOtherStudentAnswers = el.BooleanValueOf("show_other_answers") == true;
                        unid = el.ValueOf("unid");

                        Questions = el.Element("questions") == null ? new List<Question>() :
                        el.Element("questions").Descendants("question").Select(q => new Question
                        {
                            ID = long.Parse(q.Element("id").Value),
                            Number = short.Parse(q.Element("num").Value),
                            question = q.Element("qtxt").Value,
                            Hint = q.Element("hint") == null ? "" : q.Element("hint").Value,
                            Analysis = q.Element("analysis") == null ? "" : q.Element("analysis").Value,
                            Score = q.Element("score") == null ? 0 : decimal.Parse(q.Element("score").Value),
                            FilePath = q.Element("fullname") == null ? "" : q.Element("fullname").Value,
                            FileID = q.Element("file_id") == null ? 0 : long.Parse(q.Element("file_id").Value),
                            FileType = q.Element("file_type") == null ? "" : q.Element("file_type").Value,
                            AssetID = q.Element("asset_id") == null ? (long?)null : long.Parse(q.Element("asset_id").Value),
                            AssetCaption = q.ValueOf("asset_caption"),
                            CorrectAnswerCount = q.Element("correct_answers") == null ? 0 : int.Parse(q.Element("correct_answers").Value),
                            VideoAnswerID = q.Element("video_answer_id") == null ? null : (long?)long.Parse(q.Element("video_answer_id").Value),
                            VideoAnswerTime = q.Element("video_answer_time") == null ? 0 : int.Parse(q.Element("video_answer_time").Value),
                            Tags = q.Element("tags") == null ? "" : q.Element("tags").Value,
                            Answers = q.Element("answers") == null ? new List<Answer>() :
                            q.Element("answers").Descendants("answer").Select(a => new Answer
                            {
                                ID = long.Parse(a.Element("id").Value),
                                Number = short.Parse(a.Element("number").Value),
                                answer = a.Element("atxt").Value,
                                Explanation = a.Element("explanation") == null ? "" : a.Element("explanation").Value,
                                IsCorrect = a.Element("correct") != null && a.Element("correct").Value == "1",
                                IsUserAnswer = a.Element("user_answer") != null && a.Element("user_answer").Value == "1",
                                AnsweredCount = a.Element("answered_count") == null ? 0 : int.Parse(a.Element("answered_count").Value)
                            }).ToList()
                        }).OrderBy(q => q.Number).ToList();
                    }
                }
            });
        }

        /// <summary>
        /// Gets single Course - Quiz object
        /// </summary>
        /// <param name="CourseQuizID">Uniq record ID</param>
        /// <param name="CourseID">Course ID</param>
        /// <param name="QuizID">Quiz ID</param>
        public void GetSingleCourseQuiz(long? CourseQuizID = null, long? CourseID = null, long? QuizID = null)
        {
            TryExecute(string.Format("Core.Quiz.GetSingleCourseQuiz(CourseQuizID = {0}, CourseID = {1}, QuizID = {2})", CourseQuizID, CourseID, QuizID), delegate()
            {
                using (var db = ConnectionFactory.GetDBCoreDataContext())
                {
                    var Single = db.GetSingleCourseQuiz(CourseQuizID, CourseID, QuizID).DefaultIfEmpty().Single();
                    if (Single != null)
                    {
                        this.RecordID = Single.RecordID;
                        this.CourseID = Single.CourseID;
                        this.ID = Single.QuizID;
                        this.Caption = Single.QuizCaption;
                        this.CourseCaption = Single.CourseCaption;
                        this.StartDate = Single.StartDate;
                        this.EndDate = Single.EndDate;
                        this.GradeReleaseDate = Single.GradeReleaseDate;
                        this.IsPractice = Single.IsPractice;
                        this.IsPublished = Single.IsPublished;
                        this.ShowAnswers = Single.ShowAnswers;
                    }
                }
            });
        }

        /// <summary>
        /// Gets if user already passed quiz for given course
        /// </summary>
        /// <param name="UserID">User ID</param>
        /// <param name="QuizID">Quiz ID</param>
        /// <returns>True or False</returns>
        public bool IsUserAlreadyPassedQuiz(long? UserID, long? QuizID)
        {
            return TryToReturn<bool>(string.Format("Core.Quiz.IsUserAlreadyPassedQuiz(UserID = {0}, QuizID = {1})", UserID, QuizID), delegate()
            {
                using (var db = ConnectionFactory.GetDBCoreDataContext())
                {
                    return db.IsUserAlreadyPassedQuiz(UserID, QuizID).Value;
                }
            });
        }

        /// <summary>
        /// Gets list of quizzes attached to the course
        /// </summary>
        /// <param name="CourseID">Course ID</param>
        /// <param name="IsPublished">Wether filter by published quizzes or not</param>
        /// <param name="IsPractice">Whether filter by practice quizzes or not</param>
        /// <param name="ConsiderDates">Wheter filter by publish dates or not</param>
        /// <param name="SectionID"> Section ID </param>
        /// <returns>List of quizzes</returns>
        public List<CourseQuiz> ListCourseQuizes(long CourseID, bool? IsPublished = null, bool? IsPractice = null, bool? ConsiderDates = null, long? SectionID = null)
        {
            return TryToGetList(string.Format("Core.Quiz.ListCourseQuizes(CourseID = {0}, IsPublished = {1}, IsPractice = {2}, ConsiderDates = {3}, SectionID = {4})", CourseID, IsPublished, IsPractice, ConsiderDates, SectionID), delegate()
            {
                using (var db = ConnectionFactory.GetDBCoreDataContext())
                {
                    return db.List_CourseQuizes(CourseID, IsPublished, IsPractice, ConsiderDates, SectionID)
                             .OrderByDescending(Q => Q.EndDate)
                             .Select(Q => new CourseQuiz
                             {
                                 RecordID = Q.RecordID,
                                 ID = Q.QuizID,
                                 Caption = Q.Caption,
                                 StartDate = Q.StartDate,
                                 EndDate = Q.EndDate,
                                 GradeReleaseDate = Q.GradeReleaseDate,
                                 IsPractice = Q.IsPractice,
                                 IsPublished = Q.IsPublished,
                                 ShowAnswers = Q.ShowAnswers,
                                 CRTime = Q.CRTime,
                                 SectionID = Q.SectionID,
                                 Section = Q.Section,
                                 MaxScore = Q.MaxScore
                             }).ToList();
                }
            });
        }

        /// <summary>
        /// Gets list of course quizzes with student results
        /// </summary>
        /// <param name="CourseID">CourseID</param>
        /// <param name="UserID">User ID</param>
        /// <returns></returns>
        public List<CourseQuizFront> ListCourseQuizzesWithUserResults(long? CourseID, long? UserID)
        {
            return TryToGetList<CourseQuizFront>(string.Format("Core.Quiz.ListCourseQuizzesWithUserResults(CourseID = {0}, UserID = {1})", CourseID, UserID), () =>
            {
                using (var db = ConnectionFactory.GetDBCoreDataContext())
                {
                    return db.List_CourseQuizzesWithUserResults(CourseID, UserID).Select(q => new CourseQuizFront
                    {
                        RecordID =q.RecordID,
                        ID = q.QuizID,
                        Caption = q.Caption,
                        MaxScore = q.MaxScore,
                        StudentScore = q.YourScore,
                        CRTime = q.CRTime
                    }).ToList();
                }
            });
        }

        /// <summary>
        /// Gets List of Quizes
        /// </summary>
        /// <returns></returns>
        public List<UserQuiz> ListQuizes()
        {
            return TryToGetList<UserQuiz>("Quiz.ListQuizes() - ", delegate()
            {
                using (var db = ConnectionFactory.GetDBCoreDataContext())
                {
                    return db.List_Quizes().OrderByDescending(Q => Q.CRTime).Select(Q => new UserQuiz
                    {
                        ID = Q.QuizID,
                        Caption = Q.Caption,
                        CRTime = Q.CRTime,
                        UserID = Q.UserID,
                        UserFullName = Q.Fname + " " + Q.Lname,
                        Username = Q.Username
                    }).ToList();
                }
            });
        }

        /// <summary>
        /// Gets list of user quiz pass attempts, filtered by iud parameter and filter
        /// <para>iud 0 - filter by user ID</para>
        /// <para>iud 1 - filter by quiz ID</para>
        /// <para>iud 2 - filter by attached to course and passed by user </para>
        /// </summary>
        /// <param name="iud">filter option</param>
        /// <param name="filter">filter value</param>
        /// <returns>List of quiz objects</returns>
        public List<Quiz> ListQuizAttempts(byte iud, object filter)
        {
            return TryToGetList<Quiz>(string.Format("Core.Quiz.ListQuizAttempts(iud = {0}, filter = {1})", iud, filter), delegate()
            {
                using (var db = ConnectionFactory.GetDBCoreDataContext())
                {
                    var query = db.List_QuizAttempts();
                    switch (iud)
                    {
                        case 0:
                            {
                                var UserID = (long)filter;
                                query = query.Where(A => A.UserID == UserID)
                                             .OrderByDescending(A => A.CRTime);
                                break;

                            }
                        case 1:
                            {
                                var QuizID = (long)filter;
                                query = query.Where(A => A.QuizID == QuizID);
                                break;
                            }
                        case 2:
                            {
                                var x = XElement.Parse(filter.ToString());
                                var CourseID = long.Parse(x.Element("course_id").Value);

                                query = query.Where(A => A.CourseID == CourseID);

                                if (x.Element("search_word") != null)
                                {
                                    var SearchWord = x.Element("search_word").Value;
                                    query = query.Where(A => A.UserFullName.Contains(SearchWord) || A.QuizCaption.Contains(SearchWord));
                                }

                                if (x.Element("d1") != null)
                                {
                                    var d1 = DateTime.Parse(x.Element("d1").Value);
                                    query = query.Where(A => A.CRTime >= d1);
                                }

                                if (x.Element("d2") != null)
                                {
                                    var d2 = DateTime.Parse(x.Element("d2").Value);
                                    query = query.Where(A => A.CRTime >= d2);
                                }

                                query = query.OrderByDescending(A => A.CRTime);
                                break;
                            }
                    }

                    return query.Select(A => new Quiz
                    {
                        RecordID = A.AttemptID,
                        UserID = A.UserID,
                        UserFullName = A.UserFullName,
                        ID = A.QuizID,
                        CourseID = A.CourseID,
                        CourseCaption = A.CourseCaption,
                        Caption = A.QuizCaption,
                        Score = A.Score,
                        MaxScore = A.MaxScore,
                        StartDate = A.StartDate,
                        EndDate = A.EndDate,
                        IsPublished = A.IsPublished == true,
                        GradeReleaseDate = A.GradeReleaseDate,
                        CRTime = A.CRTime
                    }).ToList();
                }
            });
        }

        /// <summary>
        /// Gets list of teacher's quizes
        /// </summary>
        /// <param name="UserID">Teacher user id</param>
        /// <returns>Quiz List</returns>
        public List<TeacherQuiz> ListTeacherQuizes(long? UserID)
        {
            return TryToGetList<TeacherQuiz>(string.Format("Core.Quiz.ListTeacherQuizes(UserID = {0})", UserID), () =>
            {
                using (var db = ConnectionFactory.GetDBCoreDataContext())
                {
                    return db.List_TeacherQuizes(UserID).OrderBy(Q => Q.Caption)
                    .Select(Q => new TeacherQuiz
                    {
                        ID = Q.QuizID,
                        Caption = Q.Caption,
                        CRTime = Q.CRTime,
                        QuestionsCount = Q.QuestionCount
                    }).ToList();
                }
            });
        }

        /// <summary>
        /// Gets list of teacher's quizes that are not attached to given course
        /// </summary>
        /// <param name="UserID">Teacher user ID</param>
        /// <param name="CourseID">Course ID</param>        
        /// <returns>Quiz List</returns>
        public List<Quiz> ListTeacherQuizesNotInCourse(long? UserID, long? CourseID)
        {
            return TryToGetList<Quiz>(string.Format("Core.Quiz.ListTeacherQuizesNotInCourse(UserID = {0}, CourseID = {1})", UserID, CourseID), () =>
            {
                using (var db = ConnectionFactory.GetDBCoreDataContext())
                {
                    return db.List_TeacherQuizesNotInCourse(UserID, CourseID).OrderBy(Q => Q.Caption)
                    .Select(Q => new Quiz
                    {
                        ID = Q.QuizID,
                        Caption = Q.Caption
                    }).ToList();
                }
            });
        }

        /// <summary>
        /// Gets List of user - quiz connections
        /// </summary>
        /// <returns>List of quizes</returns>
        public List<UserQuiz> ListUserQuizes()
        {
            return TryToGetList<UserQuiz>("Core.Quiz.ListQuizes() - ", () =>
            {
                using (var db = ConnectionFactory.GetDBCoreDataContext())
                {
                    return db.List_UserQuizes().OrderBy(Q => Q.Caption)
                    .Select(Q => new UserQuiz
                    {
                        RecordID = Q.RecordID,
                        ID = Q.QuizID,
                        Caption = Q.Caption,
                        UserID = Q.UserID,
                        Username = Q.Username,
                        UserFullName = Q.Fname + " " + Q.Lname,
                        View = Q.View,
                        Edit = Q.Edit,
                        Delete = Q.Delete,
                        CRTime = Q.CRTime
                    }).ToList();
                }
            });
        }

        /// <summary>
        /// Performs CRUD action on Quiz table in database
        /// </summary>
        /// <param name="iud">Action ID</param>
        /// <param name="ID">Database uniq ID</param>             
        /// <param name="Caption">Caption</param>        
        public void TSP_Quiz(byte? iud = null, long? ID = null, string Caption = null)
        {
            TryExecute(string.Format("Core.Quiz.TSP_Quiz(iud = {0}, ID = {1}, Caption = {2})", iud, ID, Caption), delegate()
            {
                using (var db = ConnectionFactory.GetDBCoreDataContext())
                {
                    long? NewID = ID;
                    db.tsp_Quizes(iud, ref NewID, Caption);
                    this.ID = NewID;
                }
            });
        }

        /// <summary>
        /// Performs CRUD operation with database CourseQuizes table
        /// </summary>
        /// <param name="iud">Action ID</param>
        /// <param name="RecordID">Database uniq ID</param>        
        /// <param name="CourseID">Course ID</param>
        /// <param name="QuizID">Quiz ID</param>
        /// <param name="StartDate">Start Time</param>
        /// <param name="EndDate">Finish Time</param>
        /// <param name="GradeReleaseDate">Grade Release Time</param>
        /// <param name="IsPublished">Published or not</param>
        /// <param name="IsPractice">Practice or not</param>
        /// <param name="SectionID">Section ID where practice quiz is attached</param>        
        public void TSP_CourseQuizes(byte? iud = null, long? RecordID = null, long? CourseID = null, long? QuizID = null, DateTime? StartDate = null, DateTime? EndDate = null, DateTime? GradeReleaseDate = null, bool? IsPublished = null, bool? IsPractice = null, long? SectionID = null)
        {
            TryExecute(string.Format("Core.Quiz.TSP_CourseQuizes(iud = {0}, RecordID = {1}, CourseID = {2}, QuizID = {3}, StartDate = {4}, EndDate = {5}, GradeReleaseDate = {6}, IsPublished = {7}, IsPractice = {8}, SectionID = {9})", iud, RecordID, CourseID, QuizID, StartDate, EndDate, GradeReleaseDate, IsPublished, IsPractice, SectionID), delegate()
            {
                using (var db = ConnectionFactory.GetDBCoreDataContext())
                {
                    db.tsp_CourseQuizes(iud, RecordID, CourseID, QuizID, StartDate, EndDate, GradeReleaseDate, IsPublished, IsPractice, SectionID);
                }
            });
        }

        /// <summary>
        /// Performs some action with Quizzes and some related tables, based on iud parameter and xml values
        /// </summary>
        /// <param name="iud">Action ID</param>
        /// <param name="xml">Input parameters given as xml</param>
        public void TX_Quizes(byte iud, string xml)
        {
            TryExecute(string.Format("Core.Quiz.TX_Quizes(iud = {0}, xml = {1})", iud, xml), delegate()
            {
                using (var db = ConnectionFactory.GetDBCoreDataContext())
                {
                    XElement _output = null;
                    db.tx_Quizes(iud, XElement.Parse(xml), ref _output);
                    this.Properties = _output;
                }
            });
        }
        #endregion Methods
    }

    public enum QuizViewMode
    {
        Preview,
        Pass,
        Passed,
        Study,
        PassedAnswers
    }
}
