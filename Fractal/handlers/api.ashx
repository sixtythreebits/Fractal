<%@ WebHandler Language="C#" Class="api" %>

using System;
using System.Web;
using Lib;
using Core.Services;
    
public class api : IHttpHandler,System.Web.SessionState.IReadOnlySessionState {
    
    public void ProcessRequest(HttpContext context)
    {
        try
        {
            var A = new API(context);
            string s = A.GetResult();

            if (!string.IsNullOrEmpty(s))
            {
                context.Response.ContentType = "text/plain";
                context.Response.Write(s);
            }
        }
        catch
        {
            ProceedGetOut(context);
        }                                    
    }

    void ProceedGetOut(HttpContext context)
    {
        context.Response.AddHeader("Content-Disposition", "attachment;filename=missing.txt");
        context.Response.ContentType = "plain/text";
        context.Response.Write("File Not Found");
    }
 
    public bool IsReusable {
        get {
            return false;
        }
    }

}