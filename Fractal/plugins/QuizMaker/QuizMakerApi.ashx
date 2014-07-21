<%@ WebHandler Language="C#" Class="QuizMakerApi" %>

using System;
using System.Web;
using Website;

public class QuizMakerApi : IHttpHandler, System.Web.SessionState.IRequiresSessionState{
    
    public void ProcessRequest (HttpContext context) {

        var QM = new QuizMaker(context);
        QM.ProcessRequest();
    }
 
    public bool IsReusable {
        get {
            return false;
        }
    }

}