using Core;
using DevExpress.Web.ASPxGridView;
using System;
using System.Linq;
using System.Text;
using Core.Properties;


public partial class administration_WebManagement_CourseKeys : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        InitStartUp();
    }

    void InitStartUp()
    {
        Master.Master.FancyBoxEnabled = true;
        Exporter.ExportLinkButton.Visible = true;

        var CRTimeColumn = (GridViewDataDateColumn)KeyGroupsGrid.Columns["CRTime"];
        CRTimeColumn.PropertiesDateEdit.DisplayFormatString = Resources.FormatDateTime;

        KeyGroupsDataSource.SelectParameters["CourseID"].DefaultValue = Master.CourseObject.ID.ToString();
    }    

    protected void KeysGrid_Init(object sender, EventArgs e)
    {
        var KeysGrid = (ASPxGridView)sender;
        CourseKeysDataSource.SelectParameters["GroupID"].DefaultValue = KeysGrid.GetMasterRowKeyValue().ToString();
    }

    protected void KeyGroupsGrid_CustomButtonCallback(object sender, ASPxGridViewCustomButtonCallbackEventArgs e)
    {
        var grid = (ASPxGridView)sender;
        var x = string.Format(@"
        <data>
            <group>
                <id>{0}</id>
            </group>
        </data>",
        grid.GetRowValues(e.VisibleIndex, "ID")
        );
        var CK = new CourseKey();
        CK.TX_CourseKeys(1, x);
        if (CK.IsError)
        {
            throw new Exception(CK.IsClient ? CK.ErrorMessage : Resources.Abort);
        }
        else
        {
            grid.DataBind();
        }
    }

    protected void KeysGrid_CustomButtonCallback(object sender, ASPxGridViewCustomButtonCallbackEventArgs e)
    {
        var grid = (ASPxGridView)sender;
        var x = string.Format(@"
        <data>
            <key>
                <id>{0}</id>            
           </key>
        </data>",
        grid.GetRowValues(e.VisibleIndex, "ID")
        );
        var CK = new CourseKey();
        CK.TX_CourseKeys(2, x);
        if (CK.IsError)
        {
            throw new Exception(CK.IsClient ? CK.ErrorMessage : Resources.Abort);
        }
        else
        {
            grid.DataBind();
        }
    }
    
}