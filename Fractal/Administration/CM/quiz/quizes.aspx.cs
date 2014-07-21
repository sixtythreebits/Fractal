using System;
using System.Linq;
using Core;
using Lib;
using Newtonsoft.Json;
using Core.Properties;
using Core.Utilities;


public partial class management_CM_quizes : System.Web.UI.Page
{
    string xml = string.Empty;
   
    protected void Page_Load(object sender, EventArgs e)
    {
        var action = Request.Form["action"];
        if (string.IsNullOrEmpty(action))
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
        Master.DialogText = Resources.ConfirmDeleteQuiz;

        var Q = new Quiz();
        var list = Q.ListTeacherQuizes(Master.UserObject.ID).Select(q=> new
        {
            ID = q.ID,
            IDEncoded = Utility.GetSimpleEncodedID(q.ID.ToString()),
            Caption = q.Caption,
            QuestionsCount = q.QuestionsCount,
            CRTime = q.CRTime.ToString(Resources.FormatDateTime)
        }).ToList();
        QuizRepeater.DataSource = list;
        QuizRepeater.DataBind();                
    }

    #region AJAX
    void ExecAjax(string Action)
    {
        Response.Clear();
        try
        {
            switch (Action)
            {
                case "create":
                    {
                        CreateQuiz();
                        break;
                    }
                case "rename":
                    {
                        RenameQuiz();
                        break;
                    }
                case "delete":
                    {
                        DeleteQuiz();
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

    void CreateQuiz()
    {
        var Q = new Quiz();
        var Caption = "New Quiz";
        var x = string.Format("<data><user>{0}</user><caption>{1}</caption></data>", Master.UserObject.ID, Caption);
        Q.TX_Quizes(0, x);
        if (!Q.IsError && Q.Properties != null)
        {
            var json = new
            {
                id = Q.Properties.Value,
                delete_id = Utility.GetSimpleEncodedID(Q.Properties.Value),
                caption = Caption,
                date = DateTime.Now.ToString(Resources.FormatDateTime)
            };
            Response.Write(JsonConvert.SerializeObject(json));
        }
    }

    void RenameQuiz()
    {
        var ID = Utility.GetSimpleDecodedID(Request.Form["id"]).ToLong();
        var Q = new Quiz();
        Q.TSP_Quiz(1, ID, Request.Form["name"]);
            
        if (!Q.IsError)
        {
            Response.Write("success");
        }
    }

    void DeleteQuiz()
    {
        var ID = Utility.GetSimpleDecodedID(Request.Form["id"]).ToLong();

        var x = string.Format(@"
            <data>
                <quiz_id>{0}</quiz_id>
            </data>
            ", ID);

        var Q = new Quiz();
        var A = new Asset();
        Q.GetSingleQuiz(string.Format("<data><quiz_id>{0}</quiz_id></data>", ID));
        if (Q.Questions != null)
        {
            Q.Questions.Where(q => q.AssetID > 0).ToList().ForEach(q =>
            {
                A.DeleteAsset(q.AssetID.Value);
            });
        }

        if (!A.IsError)
        {
            Q.TX_Quizes(1, x);

            if (!Q.IsError)
            {
                Response.Write("success");
            }
        }
    }
    #endregion AJAX
}