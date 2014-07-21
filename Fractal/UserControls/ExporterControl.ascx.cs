using System;
using System.Web.UI.WebControls;
using System.ComponentModel;
using Core.Utilities.Enumerations;


public partial class UserControls_ExporterControl : System.Web.UI.UserControl
{
    public string GridID { set; get; }
    
    GridType _Type = GridType.GridView;
    GridExportTypes _GridExportType = GridExportTypes.XLSX;

    [Browsable(true)]
    public GridType Type
    {
        set { _Type = value; }
        get { return _Type; }
    }

    [Browsable(true)]
    public GridExportTypes GridExportType
    {
        set { _GridExportType = value; }
        get { return _GridExportType; }
    }

    [Browsable(true)]
    public string FileName { set; get; }

    [Browsable(true)]
    public bool IsClientHidden { set; get; }
    
    public LinkButton ExportLinkButton
    {
        set { ExporterButton1 = value; }
        get { return ExporterButton1; }
    }    

    protected void Page_Load(object sender, EventArgs e)
    {
        string Root = "http://" + Request.Url.Host + Request.ApplicationPath + "/";
        if (IsClientHidden)
        {
            ExporterButton1.CssClass += " hidden";
        }
    }

    protected void DoExport(object sender,EventArgs e)
    {
        try
        {
            if (Type == GridType.GridView)
            {
                Exporter.GridViewID = GridID;                
                FileName = FileName + "_" + DateTime.Now.ToString("dd.MM.yyyy hh:mm tt").Replace("/", ".").Replace(" ", "_").Replace(":", ".") + "";
                switch (GridExportType)
                {
                    case GridExportTypes.CSV:
                        {
                            Exporter.WriteCsvToResponse(FileName);
                            break;
                        }
                    case GridExportTypes.PDF:
                        {
                            Exporter.WritePdfToResponse(FileName);
                            break;
                        }
                    default:
                        {
                            Exporter.WriteXlsxToResponse(FileName);
                            break;
                        }
                }                
            }
            else
            {
                TreeExporter.TreeListID = GridID;
                FileName = FileName + "_" + DateTime.Now.ToString("dd.MM.yyyy hh:mm tt").Replace("/", ".").Replace(" ", "_").Replace(":", ".") + "";
                TreeExporter.WriteXlsxToResponse(FileName);
            }           
        }
        catch { }
    }
}

public enum GridType {GridView,TreeList,PivotGrid};
