<%@ WebHandler Language="C#" Class="preview"  %>

using System;
using System.IO;
using System.Web;
using Lib;
using ImageResizer;
using Core;
using Core.Utilities;

/*
 part[0] - file id
 part[1] - width
 part[2] - height
*/

public class preview : IHttpHandler, System.Web.SessionState.IReadOnlySessionState
{

    int? Height;
    int? Width;
    long? AssetID;
    string Filename;
    string FilePath;
    string ContentType;
    string Extension;
    FitMode FitModeOption;

    public void ProcessRequest(HttpContext context)
    {        
        if (IsAssetDataExtracted(context) && IsAssetDataInitialized())
        {
            InitPreviewDownload(context);
        }
    }

    bool IsAssetDataExtracted(HttpContext context)
    {        
        try
        {
            var x = System.Xml.Linq.XElement.Parse(context.Request.QueryString["q"].DecryptWeb());
            Height = x.IntValueOf("height");
            Height = Height > 0 ? Height : 0;
            Width = x.IntValueOf("width");
            Width = Width > 0 ? Width : 0;
            AssetID = x.LongValueOf("asset_id");
            Filename = x.ValueOf("filename");
            
            if(x.Element("fit_mode") == null)
            {
                FitModeOption = FitMode.Crop;
            }
            else
            {                
                switch (x.Element("fit_mode").Value)
                {
                    case "max":
                        {
                            FitModeOption = FitMode.Max;
                            break;
                        }
                    case "none":
                        {
                            FitModeOption = FitMode.None;
                            break;
                        }
                    case "pad":
                        {
                            FitModeOption = FitMode.Pad;
                            break;
                        }
                    case "stretch":
                        {
                            FitModeOption = FitMode.Stretch;
                            break;
                        }
                }
            }
            

            return Width > 0 && (AssetID > 0 || Filename.Length > 0);                        
        }
        catch(Exception ex)
        {
            string.Format("page: {0} \n message: {1}\n", context.Request.UrlReferrer, ex.Message).LogString();            
            return false;
        }
    }

    bool IsAssetDataInitialized()
    {
        if (AssetID > 0)
        {
            var A = new Asset(AssetID);
            Filename = A.FileName;
        }
        
        Extension = System.IO.Path.GetExtension(Filename).ToLower();
        if (Utility.GetImageExtensions().Contains(Extension))
        {
            ContentType = string.Format("image/{0}", Extension.Replace(".", ""));
            Utility.GetUploadFileLocations().ForEach(f =>
            {
                if (System.IO.File.Exists(f + Filename))
                {
                    FilePath = f + Filename;
                    return;
                }
            });
            return !string.IsNullOrWhiteSpace(FilePath);            
        }

        return false;
    }

    void InitPreviewDownload(HttpContext context)
    {
        if (Height == 0)
        {
            var img = System.Drawing.Image.FromFile(FilePath);
            var OriginalHeight = img.Height;
            var OriginalWidth = img.Width;
            img.Dispose();
            Height = Utility.GetImageHeightForWidth(OriginalWidth, OriginalHeight, Width.Value);            
        }

        var ir = new ImageJob(FilePath, context.Response.OutputStream, new ResizeSettings(Width.Value, Height.Value, FitModeOption, Extension));
        ir.Build();
        context.Response.ContentType = ContentType;
        context.Response.AddHeader("Content-Disposition", "attachment;filename=preview" + Extension);
        context.Response.Flush();
        InitCache(ref context);
    }
    
    void InitCache(ref HttpContext context)
    {
        context.Response.Cache.SetExpires(DateTime.Now.AddMinutes(1));
        context.Response.Cache.SetMaxAge(TimeSpan.FromMinutes(1));
        context.Response.Cache.SetCacheability(HttpCacheability.Public);
        context.Response.CacheControl = HttpCacheability.Public.ToString();
        context.Response.Cache.SetValidUntilExpires(true);
    }

    public bool IsReusable
    {
        get
        {
            return false;
        }
    }

}