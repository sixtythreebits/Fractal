<%@ WebHandler Language="C#" Class="download" %>

using System;
using System.Web;
using System.Linq;
using System.IO;
using Lib;
using Core;
using Core.Utilities;
    
public class download : IHttpHandler,System.Web.SessionState.IReadOnlySessionState {
    
    public void ProcessRequest(HttpContext context)
    {
        try
        {
            long ID = long.Parse(context.Request.QueryString["id"].DecryptWeb());
            var U = new User();
            U.GetAuthorizedCredentials();
            
            if (Security.IsUserAllowedToViewAsset(U.ID, ID))
            {
                var A = new Asset(ID);
                if (A.IsError)
                {
                    
                }
                else
                {
                    context.Response.Clear();
                    context.Response.ClearContent();
                    context.Response.ClearHeaders();
                    context.Response.Buffer = true;

                    var Filename = A.Caption.TrimStart().TrimEnd().Replace(" ", "_").Replace(",", "_") + Path.GetExtension(A.FileName);
                    context.Response.AddHeader("Content-Disposition", "attachment;filename=" + Filename);
                    context.Response.ContentType = string.IsNullOrEmpty(A.ContentType) ? "application/octet-stream" : A.ContentType;
                    context.Response.TransmitFile(AppSettings.AssetFolderPhysicalPath + A.FileName);                                        
                }
            }
            else
            {
                ProceedGetOut(context);
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