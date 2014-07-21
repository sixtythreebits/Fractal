using System;
using System.Web;
using System.Linq;
using System.Xml.Linq;
using Newtonsoft.Json;
using Core;
using Lib;
using Core.Utilities;
using Core.Properties;

/*
To process request for makeing any modification for existing quiz Request object should contain 
 
  1: UseID, who sends request - will be take from the session
  2: QuizID, that needs to be modified - will be taken from post parameters Request.Form["id"]
   
ProcessRequest() method will check wheter user has permission to modify quiz
 
 0 - Create new question
 1 - Get single question by ID
 2 - Save question properties
 3 - Update question positions
 4 - Remove question from quiz
 5 - Remove question from database permanently
 6 - Upload question file
 7 - Clear question image
 8 - Get VCode xml for question video answer player
 9 - Search questions in the bank
 10 - Apply question from the bank to the quiz
  
 11 - Create new answer
 12 - Gets single answer data by answer ID
 13 - Save answer properties
 14 - Updates answers sorting order
 15 - Delete answer
  
 16 - Update quiz caption
 17 - Update course graded quiz properties
 18 - Update asset quiz properties
*/

namespace Website
{
    public class QuizMaker
    {
        #region Properties
        long QuizID;
        HttpRequest Request;
        HttpResponse Response;
        User U;
        #endregion Properties

        #region Constructors
        public QuizMaker(HttpContext context)
        {
            Request = context.Request;
            Response = context.Response;
            U = new User();
        }
        #endregion Constructors

        #region Methods
        public void ProcessRequest()
        {
            try
            {
                if (U.GetAuthorizedCredentials() && long.TryParse(Request.Form["id"], out QuizID))
                {
                    var iud = Request.Form["iud"];
                    switch (iud)
                    {
                        case "0":
                            {
                                CreateNewQuestion();
                                break;
                            }
                        case "1":
                            {
                                GetSingleQuestion();
                                break;
                            }
                        case "2":
                            {
                                SaveQuestion();
                                break;
                            }
                        case "3":
                            {
                                UpdateQuestionSorting();
                                break;
                            }
                        case "4":
                            {
                                DisconnectQuestionFromQuiz();
                                break;
                            }
                        case "5":
                            {
                                DeleteQuestion();
                                break;
                            }
                        case "6":
                            {
                                UploadQuestionFile();
                                break;
                            }
                        case "7":
                            {
                                ClearQuestionImage();
                                break;
                            }
                        case "8":
                            {
                                GetQuestionVideoAnswerVCodeXml();
                                break;
                            }
                        case "9":
                            {
                                ListUserQuestionsFromTheBankByKeywords();
                                break;
                            }
                        case "10":
                            {
                                ApplyExistingQuestionToQuiz();
                                break;
                            }
                        case "11":
                            {
                                CreateNewAnswer();
                                break;
                            }
                        case "12":
                            {
                                GetSingleAnswer();
                                break;
                            }
                        case "13":
                            {
                                SaveAnswer();
                                break;
                            }
                        case "14":
                            {
                                UpdateAnswersSorting();
                                break;
                            }
                        case "15":
                            {
                                DeleteAnswer();
                                break;
                            }
                        case "16":
                            {
                                RenameQuiz();
                                break;
                            }
                        case "17":
                            {
                                SaveCourseQuiz();
                                break;
                            }
                        case "18":
                            {
                                SaveAssetQuiz();
                                break;
                            }
                    }
                }
            }
            catch (Exception ex)
            {
                string.Format("form data: {0} , \nmessage: {1}", Request.Form.ToString(), ex.Message).LogString();
            }
        }

        #region Question Stuff
        void CreateNewQuestion()
        {
            try
            {
                var Q = new Question();
                var x = Request.Form["data"];
                //x.LogString();
                Q.TX_Questions(0, x);
                if (!Q.IsError)
                {
                    var JSON = new
                    {
                        id = Q.Properties.Value.EncryptWeb()
                    };
                    Response.Write(JsonConvert.SerializeObject(JSON, Formatting.None));
                }
            }
            catch (Exception ex)
            {
                ex.Message.LogString();
            }
        }

        void GetSingleQuestion()
        {
            long QuestionID;
            if (long.TryParse(Request.Form["qid"].TrimEnd().DecryptWeb(), out QuestionID))
            {
                var Q = new Question();
                Q.GetSingleQuestion(QuestionID);

                var JSON = new
                {
                    id = Q.ID.ToString().EncryptWeb(),
                    question = Q.question,
                    hint = Q.Hint,
                    analysis = Q.Analysis,
                    score = string.Format("{0:n0}", Q.Score),
                    asset_id = Q.AssetID.HasValue ? Q.AssetID.ToString().EncryptWeb() : null
                };
                Response.Write(JsonConvert.SerializeObject(JSON, Formatting.None));
            }
        }

        void SaveQuestion()
        {
            var Q = new Question();
            var x = XElement.Parse(string.Format(Request.Form["data"], Request.Form["qid"].DecryptWeb()));
            if (x.Element("video_answer_id") != null)
            {
                x.Element("video_answer_id").Value = x.Element("video_answer_id").Value.DecryptWeb();
                x.Element("time").Value = TimeSpan.Parse(x.Element("time").Value).TotalSeconds.ToString();
            }
            Q.TX_Questions(1, x.ToString());
            if (!Q.IsError)
            {
                Response.Write("success");
            }
        }

        void UpdateQuestionSorting()
        {
            var x = XElement.Parse(Request.Form["data"]);
            x.Add(new XElement("quiz", new XElement("id", QuizID)));
            x.Element("questions").Elements().ToList().ForEach(e =>
            {
                e.Element("id").Value = e.Element("id").Value.DecryptWeb();
            });
            var Q = new Question();
            Q.TX_Questions(2, x.ToString());
            //x.ToString().LogString();
            if (!Q.IsError)
            {
                Response.Write("success");
            }
        }

        void DisconnectQuestionFromQuiz()
        {
            var Q = new Question();
            var x = string.Format(@"
        <data>
           <quiz_id>{0}</quiz_id>
           <question_id>{1}</question_id>
        </data>",
            QuizID,
            Request.Form["qid"].DecryptWeb()
            );
            Q.TX_Questions(3, x);
            if (Q.IsError)
            {
                var JSON = new
                {
                    result = "error",
                    error_message = Q.IsClient ? Q.ErrorMessage : Resources.Abort
                };
                Response.Write(JsonConvert.SerializeObject(JSON, Formatting.None));
            }
            else
            {
                var JSON = new
                {
                    result = "success"
                };
                Response.Write(JsonConvert.SerializeObject(JSON, Formatting.None));
            }
        }

        void DeleteQuestion()
        {
            var QuestionID = Request.Form["qid"].DecryptWeb().ToLong();
            var Q = new Question(QuestionID);
            var x = string.Format(@"
            <data>
               <quiz_id>{0}</quiz_id>
               <question_id>{1}</question_id>
            </data>",
            QuizID,
            QuestionID
            );

            if (Q.AssetID > 0)
            {
                new Asset().DeleteAsset(Q.AssetID, true);
            }
            
            Q.TX_Questions(4, x);
            if (Q.IsError)
            {
                var JSON = new
                {
                    result = "error",
                    error_message = Q.IsClient ? Q.ErrorMessage : Resources.Abort
                };
                Response.Write(JsonConvert.SerializeObject(JSON, Formatting.None));
            }
            else
            {
                var JSON = new
                {
                    result = "success"
                };
                Response.Write(JsonConvert.SerializeObject(JSON, Formatting.None));
            }
        }

        void UploadQuestionFile()
        {
            if (Request.Files.Count > 0)
            {
                var QuestionID = long.Parse(Request.Form["qid"].DecryptWeb());
                var Q = new Question(QuestionID);
                if (Q.AssetID.HasValue)
                {
                    var A = new Asset();
                    A.DeleteAsset(Q.AssetID.Value, true);
                }

                var f = Request.Files[0];
                var FileName = System.IO.Path.GetFileNameWithoutExtension(f.FileName);
                var FullName = f.FileName.ToAZ09Dash(true);
                var Size = f.ContentLength;
                var x = string.Format(@"
            <data>
                <quiz_id>{0}</quiz_id>
                <question_id>{1}</question_id>
                <filename><![CDATA[{2}]]></filename>
                <fullname><![CDATA[{3}]]></fullname>
                <size>{4}</size>
            </data>",
                QuizID,
                QuestionID,
                FileName,
                FullName,
                Size
                );

                Q.TX_Questions(5, x);
                if (!Q.IsError && Q.Properties != null)
                {
                    f.SaveAs(AppSettings.AssetFolderPhysicalPath + FullName);
                    var JSON = new
                    {
                        asset_id = Q.Properties.Value.EncryptWeb(),
                        caption = FileName
                    };
                    Response.Write(JsonConvert.SerializeObject(JSON, Formatting.None));
                }
            }
        }

        void ClearQuestionImage()
        {
            var A = new Asset();
            var Q = new Question(long.Parse(Request.Form["qid"].DecryptWeb()));
            A.DeleteAsset(Q.AssetID.Value, true);
            if (!A.IsError)
            {
                Response.Write("success");
            }
        }

        void GetQuestionVideoAnswerVCodeXml()
        {
            Response.Write(string.Format("<data><asset_id>{0}</asset_id><all_markers_disabled>1</all_markers_disabled><quizzes_disabled>1</quizzes_disabled></data>", Request.Form["asset_id"].DecryptWeb()).EncryptWeb());
        }

        void ListUserQuestionsFromTheBankByKeywords()
        {
            var Q = new Question();
            var Keywords = Request.Form["keywords"];
            var KeywordList = Keywords.Split(' ').Where(k => k.Length > 0).ToList();
            Keywords = string.Join(" OR ", KeywordList);
            var list = Q.ListUserQuestionsNotInQuiz(long.Parse(Request.Form["user_id"].DecryptWeb()), QuizID, Keywords);
            list.ForEach(q =>
            {
                KeywordList.ForEach(k =>
                {
                    q.question = q.question.SearchReplace(k, "<span class=\"highlight\">", "</span>");
                });
            });

            var JSON = list.Select(q => new
            {
                id = q.ID.ToString().EncryptWeb(),
                question = q.question,
                asset_id = q.AssetID.HasValue ? string.Format("<data><asset_id>{0}</asset_id><width>{1}</width><height>{2}</height></data>", q.AssetID, 113, 85).EncryptWeb() : null,
                tags = q.Properties == null ? null : q.Properties.Descendants("tag").Select(t => t.Value).ToArray(),
                preview_data = q.AssetID > 0 ? new Core.Utilities.PreviewImageData { AssetID = q.AssetID, Width = 113, Height = 85 }.ToEncripted() : null
            }).ToArray();

            Response.Write(JsonConvert.SerializeObject(JSON, Formatting.None));
        }

        void ApplyExistingQuestionToQuiz()
        {
            var Q = new Question();
            var x = XElement.Parse(Request.Form["data"]);
            x.Element("question_id").Value = x.Element("question_id").Value.DecryptWeb();
            Q.TX_Questions(6, x.ToString());
            if (!Q.IsError)
            {
                var JSON = new
                {
                    result = "success",
                    asset_caption = Q.Properties == null ? string.Empty : Q.Properties.ValueOf("asset_caption")
                };
                Response.Write(JsonConvert.SerializeObject(JSON));
            }
        }
        #endregion Question Stuff

        #region Answer Stuff
        void CreateNewAnswer()
        {
            var A = new Answer();
            var x = XElement.Parse(Request.Form["data"]);
            x.Element("question_id").Value = x.Element("question_id").Value.DecryptWeb();
            A.TX_Answers(0, x.ToString());
            if (!A.IsError && A.Properties != null)
            {
                var JSON = new
                {
                    id = A.Properties.Value.EncryptWeb()
                };
                Response.Write(JsonConvert.SerializeObject(JSON, Formatting.None));
            }
        }

        void GetSingleAnswer()
        {
            var A = new Answer();
            A.GetSingleAnswer(long.Parse(Request.Form["aid"].DecryptWeb()));
            if (!A.IsError)
            {
                var JSON = new
                {
                    id = A.ID,
                    answer = A.answer,
                    is_correct = A.IsCorrect ? "1" : "0",
                    explanation = A.Explanation
                };
                Response.Write(JsonConvert.SerializeObject(JSON));
            }
        }

        void SaveAnswer()
        {
            var A = new Answer();
            var x = XElement.Parse(Request.Form["data"]);
            x.Element("id").Value = x.Element("id").Value.DecryptWeb();
            A.TX_Answers(1, x.ToString());
            if (!A.IsError)
            {
                Response.Write("success");
            }
        }

        void UpdateAnswersSorting()
        {
            var A = new Answer();
            var x = XElement.Parse(Request.Form["data"]);
            x.Descendants("answer").ToList().ForEach(a =>
            {
                a.Element("id").Value = a.Element("id").Value.DecryptWeb();
            });
            A.TX_Answers(2, x.ToString());
            if (!A.IsError)
            {
                Response.Write("success");
            }
        }

        void DeleteAnswer()
        {
            var x = string.Format("<data><id>{0}</id></data>", Request.Form["aid"].DecryptWeb());
            var A = new Answer();
            A.TX_Answers(3, x);
            if (!A.IsError)
            {
                Response.Write("success");
            }
        }
        #endregion Answer Stuff

        #region Quiz Stuff
        void RenameQuiz()
        {
            var Q = new Quiz();
            Q.TSP_Quiz(1, QuizID, Request.Form["caption"]);
            if (!Q.IsError)
            {
                Response.Write("success");
            }
        }

        void SaveCourseQuiz()
        {
            var Q = new Quiz();
            Q.TX_Quizes(6, Request.Form["data"]);
            if (!Q.IsError)
            {
                Response.Write("success");
            }
        }

        void SaveAssetQuiz()
        {
            var Q = new Quiz();
            //System.IO.File.WriteAllText(AppDomain.CurrentDomain.BaseDirectory + "file.xml", Request.Form["data"]);
            Q.TX_Quizes(8, Request.Form["data"]);
            if (!Q.IsError)
            {
                Response.Write("success");
            }
        }
        #endregion Quiz Stuff
        #endregion Methods
    }
}
